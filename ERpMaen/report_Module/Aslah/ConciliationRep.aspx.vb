﻿Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class ConciliationRep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        Dim Conc_id = Request.QueryString("Conc_id")

        If String.IsNullOrWhiteSpace(Conc_id) Then
            Dim script As String = "<script type='text/javascript' defer='defer'> alert('لا يوجد بيانات متاحة للعرض');</script>"
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
            ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)

        Else
            getReportData(Conc_id)
        End If

    End Sub
    Private Sub getReportData(ByVal Conc_id As String)
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim dt1 As New DataTable
            dt1 = DBManager.Getdatatable("SELECT isNull(header_img,'') img_header,isNull(footer_img,'') img_footer FROM tblreport_settings where isNull(deleted,0) !=1 and comp_id=" + LoginInfo.GetComp_id())
            Dim dt2 As New DataTable

            Dim query = "SELECT  code,persons1.name  as 'Pfrom' ,
persons2.name as 'PAgainst',date_h,notes,
(select courts.Description FROM ash_cases left join tbllock_up courts on ash_cases.court_id= courts.id
 where ash_cases.id=ash_case_conciliation.case_id) as 'from'
 FROM ash_case_conciliation 
  left join ash_case_persons persons1 on ash_case_conciliation.owner_id=persons1.id 
  left join ash_case_persons persons2 on ash_case_conciliation.second_party_id=persons2.id
  where ash_case_conciliation.id=" + Conc_id
            dt2 = DBManager.Getdatatable(query)
            If dt2.Rows.Count <> 0 Then
                Dim ds As New ConciliationDS
                ds.Tables(0).Rows.Add()
                ds.Tables("Details").Rows(0).Item("Pfrom") = dt2.Rows(0).Item("Pfrom").ToString
                ds.Tables("Details").Rows(0).Item("code") = dt2.Rows(0).Item("code").ToString
                ds.Tables("Details").Rows(0).Item("from") = dt2.Rows(0).Item("from").ToString
                ds.Tables("Details").Rows(0).Item("PAgainst") = dt2.Rows(0).Item("PAgainst").ToString
                ds.Tables("Details").Rows(0).Item("date_h") = dt2.Rows(0).Item("date_h").ToString
                ds.Tables("Details").Rows(0).Item("notes") = dt2.Rows(0).Item("notes").ToString

                rdoc.Load(Server.MapPath("Conciliation.rpt"))
                rdoc.SetDataSource(ds.Tables("Details"))
                If dt1.Rows.Count <> 0 Then
                    rdoc.SetParameterValue("img_header_URL", dt1.Rows(0)("img_header").ToString)
                    rdoc.SetParameterValue("img_footer_URL", dt1.Rows(0)("img_footer").ToString)
                Else
                    rdoc.SetParameterValue("img_header_URL", "")
                    rdoc.SetParameterValue("img_footer_URL", "")

                End If
                CrystalReportViewer1.ReportSource = rdoc

                CrystalReportViewer1.DataBind()
                Try
                    Dim objDS As New DataSet
                    Dim dfdoFile As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim strServerPath As String
                    Dim szFileName As String
                    'Create dataset as per requirement
                    szFileName = Session.SessionID & ".pdf"         ' rptDailyCalls.pdf
                    strServerPath = MapPath("~") & "\Report\"        ' Here the pdf file will be saved.   
                    File.Delete(strServerPath & "\" & szFileName)    ' Delete file first
                    dfdoFile.DiskFileName = strServerPath & "\" & szFileName
                    With rdoc
                        .ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                        .ExportOptions.DestinationOptions = dfdoFile
                        .Export()
                    End With
                    ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "", "showpdf('" + Session.SessionID + "');", True)
                Catch ex As Exception
                End Try
            Else
                Dim script As String = "<script type='text/javascript' defer='defer'> alert('" + message + "');</script>"
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
                ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class