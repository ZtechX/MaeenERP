Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class ReportRep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim Consult_id = Request.QueryString("Consult_id")

        If String.IsNullOrWhiteSpace(Consult_id) Then
            Dim script As String = "<script type='text/javascript' defer='defer'> alert('لا يوجد بيانات متاحة للعرض');</script>"
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
            ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)

        Else
            getReportData(Consult_id)
        End If

    End Sub
    Private Sub getReportData(ByVal Consult_id As String)
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim dt1 As New DataTable
            dt1 = DBManager.Getdatatable("SELECT isNull(header_img,'') img_header,isNull(footer_img,'') img_footer FROM tblreport_settings where isNull(deleted,0) !=1 and comp_id=" + LoginInfo.GetComp_id())
            Dim dt2 As New DataTable

            Dim query = "SELECT ash_consultings.code,start_date_hj,type.Description as 'type',
ash_consultings.name,nat.Description as nationality_id,identiy,house_tele,tel2 as'phone_num'
  FROM ash_consultings 
  left join tbllock_up type on type.id=ash_consultings.category_id
left join tbllock_up nat on nat.id=ash_consultings.nationality_id
where ash_consultings.id=" + Consult_id
            dt2 = DBManager.Getdatatable(query)
            If dt2.Rows.Count <> 0 Then
                Dim ds As New DSReport
                ds.Tables(0).Rows.Add()
                ds.Tables("Details").Rows(0).Item("code") = dt2.Rows(0).Item("code").ToString
                ds.Tables("Details").Rows(0).Item("consul_date") = dt2.Rows(0).Item("start_date_hj").ToString
                Dim type = dt2.Rows(0).Item("type").ToString
                If type = "عنف" Then
                    ds.Tables("Details").Rows(0).Item("violence") = 1
                ElseIf type = "حضانة" Then
                    ds.Tables("Details").Rows(0).Item("Incubation") = 1
                ElseIf type = "نفقة" Then
                    ds.Tables("Details").Rows(0).Item("expense") = 1
                ElseIf type = "خلع" Then
                    ds.Tables("Details").Rows(0).Item("strippedoff") = 1
                ElseIf type = "آخرى" Then
                    ds.Tables("Details").Rows(0).Item("otherType") = 1
                End If
                ds.Tables("Details").Rows(0).Item("person_nm") = dt2.Rows(0).Item("name").ToString
                ds.Tables("Details").Rows(0).Item("person_nation") = dt2.Rows(0).Item("nationality_id").ToString
                ds.Tables("Details").Rows(0).Item("person_identity") = dt2.Rows(0).Item("identiy").ToString

                ds.Tables("Details").Rows(0).Item("house_num") = dt2.Rows(0).Item("house_tele").ToString
                ds.Tables("Details").Rows(0).Item("person_phone") = dt2.Rows(0).Item("phone_num").ToString

                rdoc.Load(Server.MapPath("Report.rpt"))
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