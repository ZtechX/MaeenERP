Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared
Public Class course_attendence_rep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim diplome_id = Request.QueryString("diplome_id")
        Dim diplome_subject = Request.QueryString("diplome_subject")
        Dim diplome_lecture = Request.QueryString("diplome_lecture")
        getReportData(diplome_id, diplome_lecture, diplome_subject)
    End Sub
    Private Sub getReportData(diplome_id, diplome_lecture, diplome_subject)
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim dt1 As New DataTable
            dt1 = DBManager.Getdatatable("SELECT isNull(header_img,'') img_header,isNull(footer_img,'') img_footer FROM tblreport_settings where isNull(deleted,0) !=1 and comp_id=" + LoginInfo.GetComp_id())
            Dim dt2 As New DataTable
            Dim dt3 As New DataTable
            Dim dt4 As New DataTable
            Dim dt5 As New DataTable
            Dim ds As New course_attendenceDS
            Dim query = "  select acd_diplomes.name as diplome,tbllock_up.description as subject from acd_diplome_subjects join acd_diplomes on acd_diplome_subjects.diplome_id=acd_diplomes.id join tbllock_up on acd_diplome_subjects.subject_id=tbllock_up.id where acd_diplome_subjects.id=" + diplome_subject
            Dim query1 = "select tblusers.id as user_id,tblusers.full_name as full_name from acd_courses_students join tblusers on acd_courses_students.student_id=tblusers.id where acd_courses_students.checked=1 and acd_courses_students.approved=1 and acd_courses_students.type=2 and acd_courses_students.course_id=" + diplome_id.ToString
            Dim query2 = "select count(tblusers.id) as user_abbsence,tblusers.id as  user_id,tblusers.full_name as full_name  from acd_absence join tblusers on acd_absence.student_id=tblusers.id where  acd_absence.type=2 and acd_absence.course_id=" + diplome_subject.ToString + " group by full_name,tblusers.id"
            Dim query3 = "select count(id) as lecture_num from acd_lectures where type=2 and course_id=" + diplome_subject.ToString
            dt2 = DBManager.Getdatatable(query)
            dt3 = DBManager.Getdatatable(query1)
            dt4 = DBManager.Getdatatable(query2)
            dt5 = DBManager.Getdatatable(query3)
            Dim row = 0

            Dim workTable As DataTable = New DataTable("StudentDegree")
            workTable.Columns.Add("student", Type.GetType("System.String"))
            workTable.Columns.Add("student_id", Type.GetType("System.String"))
            workTable.Columns.Add("abbsence_num", Type.GetType("System.String"))
            workTable.Columns.Add("attend_num", Type.GetType("System.String"))
            Dim attend_num = 0
            Dim abbsence_num = 0
            If dt3.Rows.Count <> 0 Then
                Dim rowsCount = dt3.Rows.Count - 1
                If dt4.Rows.Count <> 0 Then
                    Dim rowsCount2 = dt4.Rows.Count - 1
                    For index As Integer = 0 To rowsCount
                        For index2 As Integer = 0 To rowsCount2
                            attend_num = dt5.Rows(0).Item("lecture_num").ToString
                            abbsence_num = 0
                            If dt3.Rows(index).Item("user_id") = dt4.Rows(index2).Item("user_id") Then
                                attend_num = Convert.ToInt32(dt5.Rows(0).Item("lecture_num").ToString) - Convert.ToInt32(dt4.Rows(index2).Item("user_abbsence").ToString)
                                abbsence_num = dt4.Rows(index2).Item("user_abbsence").ToString
                                Exit For
                            End If
                        Next
                        Dim newrow As DataRow = workTable.NewRow()
                        newrow("student") = dt3.Rows(index).Item("full_name")
                        newrow("student_id") = dt3.Rows(index).Item("user_id")
                        newrow("attend_num") = attend_num
                        newrow("abbsence_num") = abbsence_num
                        workTable.Rows.Add(newrow)
                    Next
                End If

            End If
            Dim attend_count As Integer = 0
            Dim abbsence_count As Integer = 0
            If workTable.Rows.Count <> 0 Then
                Dim attend_abbsence = ""

                For Each row1 As DataRow In workTable.Rows
                    ds.Tables("course_attendence").Rows.Add()
                    ds.Tables("course_attendence").Rows(row).Item("all") = row + 1
                    ds.Tables("course_attendence").Rows(row).Item("student_id") = row1("student_id").ToString
                    ds.Tables("course_attendence").Rows(row).Item("student") = row1("student").ToString
                    ds.Tables("course_attendence").Rows(row).Item("attend_num") = row1("attend_num").ToString
                    ds.Tables("course_attendence").Rows(row).Item("abbsence_num") = row1("abbsence_num").ToString
                    row = row + 1
                Next
            End If





            rdoc.Load(Server.MapPath("course_attendence.rpt"))
            'rdoc.SetDataSource(ds.Tables("degrees"))
            rdoc.SetDataSource(ds.Tables("course_attendence"))
            'If dt1.Rows.Count <> 0 Then
            '    rdoc.SetParameterValue("img_header_URL", dt1.Rows(0)("img_header").ToString)
            '    rdoc.SetParameterValue("img_footer_URL", dt1.Rows(0)("img_footer").ToString)
            'Else
            '    rdoc.SetParameterValue("img_header_URL", "")
            '    rdoc.SetParameterValue("img_footer_URL", "")

            'End If
            If dt2.Rows.Count <> 0 Then
                ' ds.Tables("basic_report").Rows.Add()
                rdoc.SetParameterValue("diplome", dt2.Rows(0).Item("diplome").ToString)
                rdoc.SetParameterValue("course", dt2.Rows(0).Item("subject").ToString)
            Else
                rdoc.SetParameterValue("diplome", "")
                rdoc.SetParameterValue("course", "")

            End If
            CrystalReportViewer1.ReportSource = rdoc

            'CrystalReportViewer1.DataBind()
            'Try
            '    Dim objDS As New DataSet
            '    Dim dfdoFile As New CrystalDecisions.Shared.DiskFileDestinationOptions
            '    Dim strServerPath As String
            '    Dim szFileName As String
            '    'Create dataset as per requirement
            '    szFileName = Session.SessionID & ".pdf"         ' rptDailyCalls.pdf
            '    strServerPath = MapPath("~") & "\Report\"        ' Here the pdf file will be saved.   
            '    File.Delete(strServerPath & "\" & szFileName)    ' Delete file first
            '    dfdoFile.DiskFileName = strServerPath & "\" & szFileName
            '    With rdoc
            '        .ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            '        .ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            '        .ExportOptions.DestinationOptions = dfdoFile
            '        .Export()
            '    End With
            '    ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "", "showpdf('" + Session.SessionID + "');", True)
            'Catch ex As Exception
            'End Try
            'Else
            'Dim script As String = "<script type='text/javascript' defer='defer'> alert('" + message + "');</script>"
            'ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
            'ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)
            'End If
        Catch ex As Exception
        End Try
    End Sub
End Class