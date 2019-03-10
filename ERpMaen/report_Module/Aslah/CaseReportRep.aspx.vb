Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class CaseReportRep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        ' Dim Case_id = Request.QueryString("Case_id")
        Dim Case_id = "32"
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
            'dt1 = DBManager.Getdatatable("SELECT img_header,img_footer FROM tbl_company_info")
            Dim dt2 As New DataTable

            Dim query = "SELECT ash_cases.code,ash_cases.date_h as 'case_dt',ash_courts.name as 'from'
      ,persons1.name as 'person_from',persons1.indenty as 'FPerson_num'
      ,persons2.name as 'person_against',persons2.indenty as 'AgPerson_num'     
  FROM ash_cases left join ash_courts on ash_cases.court_id= ash_courts.id
  left join ash_case_persons persons1 on ash_cases.person1_id=persons1.id 
  left join ash_case_persons persons2 on ash_cases.person2_id=persons2.id  where ash_cases.id=" + Case_id
            dt2 = DBManager.Getdatatable(query)
            If dt2.Rows.Count <> 0 Then
                Dim ds As New CaseReportDS
                ds.Tables(0).Rows.Add()
                ds.Tables("Details").Rows(0).Item("code") = dt2.Rows(0).Item("code").ToString
                ds.Tables("Details").Rows(0).Item("case_dt") = dt2.Rows(0).Item("case_dt").ToString
                ds.Tables("Details").Rows(0).Item("from") = dt2.Rows(0).Item("from").ToString
                ds.Tables("Details").Rows(0).Item("AgPerson_num") = dt2.Rows(0).Item("AgPerson_num").ToString
                ds.Tables("Details").Rows(0).Item("FPerson_num") = dt2.Rows(0).Item("FPerson_num").ToString
                ds.Tables("Details").Rows(0).Item("person_from") = dt2.Rows(0).Item("person_from").ToString
                ds.Tables("Details").Rows(0).Item("person_against") = dt2.Rows(0).Item("person_against").ToString

                rdoc.Load(Server.MapPath("CaseReport.rpt"))
                rdoc.SetDataSource(ds.Tables("Details"))
                ' rdoc.SetParameterValue("img_header_URL", dt1.Rows(0)("img_header").ToString)
                ' rdoc.SetParameterValue("img_footer_URL", dt1.Rows(0)("img_footer").ToString)
                CrystalReportViewer1.ReportSource = rdoc
                Dim connectInfo As ConnectionInfo = New ConnectionInfo()
                connectInfo.ServerName = "172.107.166.215\sa, 1985"
                connectInfo.DatabaseName = "ERPDB"
                connectInfo.UserID = "sa"
                connectInfo.Password = "ZTechX@admin.com"
                rdoc.SetDatabaseLogon("sa", "ZTechX@admin.com")
                For Each tbl As CrystalDecisions.CrystalReports.Engine.Table In rdoc.Database.Tables
                    tbl.LogOnInfo.ConnectionInfo = connectInfo
                    tbl.ApplyLogOnInfo(tbl.LogOnInfo)
                Next
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