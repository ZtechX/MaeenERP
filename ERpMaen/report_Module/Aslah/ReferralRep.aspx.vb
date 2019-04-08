Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class ReferralRep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim Case_id = Request.QueryString("Case_id")

        If String.IsNullOrWhiteSpace(Case_id) Then
            Dim script As String = "<script type='text/javascript' defer='defer'> alert('لا يوجد بيانات متاحة للعرض');</script>"
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
            ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)

        Else
            getReportData(Case_id)
        End If

    End Sub
    Private Sub getReportData(ByVal Case_id As String)
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim dt1 As New DataTable
            dt1 = DBManager.Getdatatable("SELECT isNull(header_img,'') img_header,isNull(footer_img,'') img_footer FROM tblreport_settings where isNull(deleted,0) !=1 and comp_id=" + LoginInfo.GetComp_id())
            Dim dt2 As New DataTable

            Dim query = "SELECT ash_cases.code,ash_cases.date_h as 'case_dt'
      ,persons1.indenty as 'person_from_num',persons1.name as 'person_from'
      ,persons2.name as 'person_against',persons2.indenty as 'person_against_num'
      ,ash_cases.details as 'case_desc' FROM ash_cases 
  left join ash_case_persons persons1 on ash_cases.person1_id=persons1.id 
  left join ash_case_persons persons2 on ash_cases.person2_id=persons2.id where ash_cases.id=" + Case_id
            dt2 = DBManager.Getdatatable(query)
            If dt2.Rows.Count <> 0 Then
                Dim ds As New ReferralDS
                ds.Tables(0).Rows.Add()
                ds.Tables("Details").Rows(0).Item("code") = dt2.Rows(0).Item("code").ToString
                ds.Tables("Details").Rows(0).Item("case_dt") = dt2.Rows(0).Item("case_dt").ToString
                ds.Tables("Details").Rows(0).Item("person_from_num") = dt2.Rows(0).Item("person_from_num").ToString
                ds.Tables("Details").Rows(0).Item("person_against_num") = dt2.Rows(0).Item("person_against_num").ToString
                ds.Tables("Details").Rows(0).Item("person_from") = dt2.Rows(0).Item("person_from").ToString
                ds.Tables("Details").Rows(0).Item("person_against") = dt2.Rows(0).Item("person_against").ToString
                ds.Tables("Details").Rows(0).Item("case_desc") = dt2.Rows(0).Item("case_desc").ToString
                rdoc.Load(Server.MapPath("Referral.rpt"))
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