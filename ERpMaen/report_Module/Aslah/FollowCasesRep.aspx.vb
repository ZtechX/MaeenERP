Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class FollowCasesRep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        getReportData()


    End Sub
    Private Sub getReportData()
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim dt1 As New DataTable
            'dt1 = DBManager.Getdatatable("SELECT img_header,img_footer FROM tbl_company_info")
            Dim dt2 As New DataTable

            Dim query = "SELECT ash_cases.code,ash_cases.date_h as 'case_dt',ash_courts.name as 'from', court_details
,persons1.name as 'person_from' ,isNull((select count(ash_case_sessions.id) from ash_case_sessions where ash_case_sessions.case_id=ash_cases.id) ,0) as 'sessions_num',
isNull((select count(ash_case_persons.id) from ash_case_persons where ash_case_persons.case_id=ash_cases.id) ,0)+isNull(ash_cases.childrens_no,0)  as 'beneficiaries_num'
  FROM ash_cases left join ash_courts on ash_cases.court_id= ash_courts.id
   left join tbllock_up on tbllock_up.id=ash_cases.depart  
  left join ash_case_persons persons1 on ash_cases.person1_id=persons1.id where ash_cases.comp_id is not null"
            dt2 = DBManager.Getdatatable(query)
            Dim index = 0
            Dim ds As New StatisticsofcasesDS
            If dt2.Rows.Count <> 0 Then

                For Each row As DataRow In dt2.Rows

                    ds.Tables(0).Rows.Add()
                    ds.Tables("Details").Rows(index).Item("code") = row("code").ToString
                    ds.Tables("Details").Rows(index).Item("from") = row("from").ToString
                    ds.Tables("Details").Rows(index).Item("case_dt") = row("case_dt").ToString
                    ds.Tables("Details").Rows(index).Item("court_details") = row("court_details").ToString
                    ds.Tables("Details").Rows(index).Item("person_from") = row("person_from").ToString
                    ds.Tables("Details").Rows(index).Item("row_num") = (index + 1)
                    ds.Tables("Details").Rows(index).Item("sessions_num") = row("sessions_num").ToString
                    ds.Tables("Details").Rows(index).Item("beneficiaries_num") = row("beneficiaries_num").ToString
                    index = index + 1
                Next
                rdoc.Load(Server.MapPath("FollowCases.rpt"))
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