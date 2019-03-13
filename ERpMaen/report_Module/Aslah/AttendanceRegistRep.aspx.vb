Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class AttendanceRegistRep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        Dim Case_id = Request.QueryString("Case_id")
        Dim session_id = Request.QueryString("session_id")
        If String.IsNullOrWhiteSpace(Case_id) Then
            Dim script As String = "<script type='text/javascript' defer='defer'> alert('لا يوجد بيانات متاحة للعرض');</script>"
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
            ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)

        Else
            getReportData(Case_id, session_id)
        End If

    End Sub
    Private Sub getReportData(ByVal Case_id As String, ByVal session_id As String)
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            Dim dt1 As New DataTable
            'dt1 = DBManager.Getdatatable("SELECT img_header,img_footer FROM tbl_company_info")
            Dim dt2 As New DataTable
            Dim dt3 As New DataTable
            Dim dt4 As New DataTable
            Dim ds As New AttendanceRegistDS
            Dim ds1 As New AttendanceRegistDS
            Dim query = "SELECT ash_cases.code,ash_cases.date_h as 'case_dt',childrens_no
      ,instrument_no,instrument_date_h as 'instrument_dt',persons1.name as 'person_from',persons1.indenty as 'FPerson_num'
      ,persons2.name as 'person_against',persons2.indenty as 'AgPerson_num',tbllock_up.Description as 'service_type'       
  FROM ash_cases left join ash_case_persons persons1 on ash_cases.person1_id=persons1.id 
  left join ash_case_persons persons2 on ash_cases.person2_id=persons2.id 
  left join tbllock_up on tbllock_up.id=ash_cases.depart where ash_cases.id=" + Case_id
            dt2 = DBManager.Getdatatable(query)

            If dt2.Rows.Count <> 0 Then
                Dim pfrom = ""
                Dim pagainst = ""
                query = "select name,age from ash_case_childrens where case_id=" + Case_id
                dt3 = DBManager.Getdatatable(query)
                Dim row = 0
                Dim rowsCount = dt3.Rows.Count - 1
                If dt3.Rows.Count <> 0 Then

                    For index As Integer = 0 To rowsCount Step 2
                        ds.Tables(0).Rows.Add()
                        ds.Tables("Details").Rows(row).Item("code") = dt2.Rows(0).Item("code").ToString
                        ds.Tables("Details").Rows(row).Item("case_dt") = dt2.Rows(0).Item("case_dt").ToString
                        ds.Tables("Details").Rows(row).Item("instrument_no") = dt2.Rows(0).Item("instrument_no").ToString
                        ds.Tables("Details").Rows(row).Item("instrument_dt") = dt2.Rows(0).Item("instrument_dt").ToString
                        pfrom = dt2.Rows(0).Item("person_from").ToString
                        pagainst = dt2.Rows(0).Item("person_against").ToString
                        ds.Tables("Details").Rows(row).Item("person_from") = pfrom
                        ds.Tables("Details").Rows(row).Item("FPerson_num") = dt2.Rows(0).Item("FPerson_num").ToString
                        ds.Tables("Details").Rows(row).Item("person_against") = pagainst
                        ds.Tables("Details").Rows(row).Item("AgPerson_num") = dt2.Rows(0).Item("AgPerson_num").ToString
                        ds.Tables("Details").Rows(row).Item("service_type") = dt2.Rows(0).Item("service_type").ToString
                        ds.Tables("Details").Rows(row).Item("childrens_no") = dt2.Rows(0).Item("childrens_no").ToString
                        ds.Tables("Details").Rows(row).Item("child1") = dt3.Rows(index).Item("name").ToString
                        ds.Tables("Details").Rows(row).Item("Age_child1") = dt3.Rows(index).Item("age").ToString
                        If (index + 1) <= rowsCount Then
                            ds.Tables("Details").Rows(row).Item("child2") = dt3.Rows(index + 1).Item("name").ToString
                            ds.Tables("Details").Rows(row).Item("Age_child2") = dt3.Rows(index + 1).Item("age").ToString
                        End If

                        row = row + 1

                    Next

                End If

                query = "SELECT persons1.name  as 'Pfrom' ,persons2.name as 'PAgainst',date_h ,entry_time,exite_time
  FROM ash_case_sessions left join ash_case_persons persons1 on ash_case_sessions.owner_id=persons1.id 
  left join ash_case_persons persons2 on ash_case_sessions.second_party_id=persons2.id
  where  ash_case_sessions.id=" + session_id
                dt4 = DBManager.Getdatatable(query)
                Dim row_index = 0
                If dt4.Rows.Count <> 0 Then

                    Dim rowsCount1 = dt4.Rows.Count - 1
                    For index As Integer = 0 To rowsCount1 Step 1

                        ds1.Tables(0).Rows.Add()
                        ds1.Tables("Details").Rows(row_index).Item("person_from") = pfrom
                        ds1.Tables("Details").Rows(row_index).Item("person_against") = pagainst
                        ds1.Tables("Details").Rows(row_index).Item("session_num") = (row_index + 1)
                        ds1.Tables("Details").Rows(row_index).Item("Pfrom") = "طالب التنفيذ : " + dt4.Rows(index).Item("Pfrom").ToString
                        ds1.Tables("Details").Rows(row_index).Item("PAgainst") = "المنفذ ضده : " + dt4.Rows(index).Item("PAgainst").ToString
                        ds1.Tables("Details").Rows(row_index).Item("session_dt") = dt4.Rows(index).Item("date_h").ToString
                        ds1.Tables("Details").Rows(row_index).Item("entry_time") = dt4.Rows(index).Item("entry_time").ToString
                        ds1.Tables("Details").Rows(row_index).Item("exite_time") = dt4.Rows(index).Item("exite_time").ToString

                        row_index = row_index + 1

                    Next


                End If
                If row = 0 And row_index = 0 Then
                    ds.Tables(0).Rows.Add()
                    ds.Tables("Details").Rows(0).Item("code") = dt2.Rows(0).Item("code").ToString
                    ds.Tables("Details").Rows(0).Item("case_dt") = dt2.Rows(0).Item("case_dt").ToString
                    ds.Tables("Details").Rows(0).Item("instrument_no") = dt2.Rows(0).Item("instrument_no").ToString
                    ds.Tables("Details").Rows(0).Item("instrument_dt") = dt2.Rows(0).Item("instrument_dt").ToString
                    pfrom = dt2.Rows(0).Item("person_from").ToString
                    pagainst = dt2.Rows(0).Item("person_against").ToString
                    ds.Tables("Details").Rows(row).Item("person_from") = pfrom
                    ds.Tables("Details").Rows(row).Item("person_against") = pagainst
                    ds.Tables("Details").Rows(0).Item("FPerson_num") = dt2.Rows(0).Item("FPerson_num").ToString
                    ds.Tables("Details").Rows(0).Item("AgPerson_num") = dt2.Rows(0).Item("AgPerson_num").ToString
                    ds.Tables("Details").Rows(0).Item("service_type") = dt2.Rows(0).Item("service_type").ToString
                    ds.Tables("Details").Rows(0).Item("childrens_no") = dt2.Rows(0).Item("childrens_no").ToString
                End If
                rdoc.Load(Server.MapPath("AttendanceRegist.rpt"))
                rdoc.SetDataSource(ds.Tables("Details"))
                rdoc.OpenSubreport("subReportAttendance.rpt").SetDataSource(ds1)


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