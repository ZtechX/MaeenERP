Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared
Public Class academy_session_rep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim diplome_id = Request.QueryString("diplome_id")
        Dim diplome_user = Request.QueryString("diplome_user")
        getReportData(diplome_id, diplome_user)
    End Sub
    Private Sub getReportData(diplome_id, diplome_user)
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim dt1 As New DataTable
            dt1 = DBManager.Getdatatable("SELECT isNull(header_img,'') img_header,isNull(footer_img,'') img_footer FROM tblreport_settings where isNull(deleted,0) !=1 and comp_id=" + LoginInfo.GetComp_id())
            Dim dt2 As New DataTable
            Dim dt3 As New DataTable
            Dim dt4 As New DataTable
            Dim ds As New academy_sessionDS
            Dim query1 = "select tblusers.full_name as full_name ,tblusers.user_indenty as user_indenty from tblusers where tblusers.id=" + diplome_user
            Dim query2 = "select acd_diplomes.name as diplome from acd_diplomes where acd_diplomes.id=" + diplome_id
            Dim query3 = "select tbllock_up.Description as subject,(acd_student_degrees.activity_degree+acd_student_degrees.final_degree) as total_degree,acd_diplome_subjects.code as code,acd_diplome_subjects.sub_code as symbol,acd_diplome_subjects.Units_Num as unit from acd_diplome_subjects join acd_diplomes on acd_diplome_subjects.diplome_id=acd_diplomes.id  join tbllock_up on acd_diplome_subjects.subject_id=tbllock_up.id join acd_student_degrees on acd_diplome_subjects.id=acd_student_degrees.course_id   where acd_student_degrees.student_id=" + diplome_user.ToString + " and acd_diplome_subjects.diplome_id=" + diplome_id.ToString
            dt2 = DBManager.Getdatatable(query1)
            dt3 = DBManager.Getdatatable(query2)
            dt4 = DBManager.Getdatatable(query3)
            Dim row = 0
            Dim code = ""
            Dim total_units = 0
            Dim total_points = 0
            If dt4.Rows.Count <> 0 Then
                Dim rowsCount = dt4.Rows.Count - 1
                For index As Integer = 0 To rowsCount
                    If dt4.Rows(index).Item("total_degree").ToString <= 100 And dt4.Rows(index).Item("total_degree").ToString >= 95 Then
                        code = "+ا"
                    ElseIf dt4.Rows(index).Item("total_degree").ToString < 95 And dt4.Rows(index).Item("total_degree").ToString >= 90 Then
                        code = "ا"
                    ElseIf dt4.Rows(index).Item("total_degree").ToString < 90 And dt4.Rows(index).Item("total_degree").ToString >= 85 Then
                        code = "+ب"

                    ElseIf dt4.Rows(index).Item("total_degree").ToString < 85 And dt4.Rows(index).Item("total_degree").ToString >= 80 Then
                        code = "ب"

                    ElseIf dt4.Rows(index).Item("total_degree").ToString < 80 And dt4.Rows(index).Item("total_degree").ToString >= 75 Then
                        code = "+ج"
                    ElseIf dt4.Rows(index).Item("total_degree").ToString < 75 And dt4.Rows(index).Item("total_degree").ToString >= 70 Then
                        code = "ج"
                    ElseIf dt4.Rows(index).Item("total_degree").ToString < 70 And dt4.Rows(index).Item("total_degree").ToString >= 65 Then
                        code = "+د"

                    ElseIf dt4.Rows(index).Item("total_degree").ToString < 65 And dt4.Rows(index).Item("total_degree").ToString >= 60 Then
                        code = "د"

                    ElseIf dt4.Rows(index).Item("total_degree").ToString < 60 Then
                        code = "د"
                    End If
                    ds.Tables("accadmy_session").Rows.Add()
                    ds.Tables("accadmy_session").Rows(row).Item("course") = dt4.Rows(index).Item("subject").ToString
                    ds.Tables("accadmy_session").Rows(row).Item("final_degree") = Convert.ToInt32(dt4.Rows(index).Item("total_degree").ToString)
                    ds.Tables("accadmy_session").Rows(row).Item("code") = code
                    ds.Tables("accadmy_session").Rows(row).Item("symbol") = dt4.Rows(index).Item("symbol").ToString
                    ds.Tables("accadmy_session").Rows(row).Item("unit") = Convert.ToInt32(dt4.Rows(index).Item("unit").ToString)
                    total_units = total_units + Convert.ToInt32(dt4.Rows(index).Item("unit").ToString)
                    total_points = total_points + ((Convert.ToInt32(dt4.Rows(index).Item("total_degree").ToString) / (Convert.ToInt32(dt4.Rows(index).Item("unit").ToString) * 10)) * Convert.ToInt32(dt4.Rows(index).Item("unit").ToString))
                    row = row + 1
                Next
                'ds.Tables("accadmy_session").Rows(row).Item("total_units") = total_units
                'ds.Tables("accadmy_session").Rows(row).Item("total_points") = total_points

            End If

            If dt2.Rows.Count <> 0 Then
                ds.Tables("accadmy_session").Rows(0).Item("student") = dt2.Rows(0).Item("full_name").ToString
                ds.Tables("accadmy_session").Rows(0).Item("id_number") = dt2.Rows(0).Item("user_indenty").ToString
            End If

            If dt3.Rows.Count <> 0 Then
                ds.Tables("accadmy_session").Rows(0).Item("diplome") = dt3.Rows(0).Item("diplome").ToString
            End If

            rdoc.Load(Server.MapPath("academy_session.rpt"))
            'rdoc.SetDataSource(ds.Tables("degrees"))
            rdoc.SetDataSource(ds.Tables("accadmy_session"))
            Dim appreciation = ""
            If Convert.ToInt32(total_points / total_units) >= 4.75 Then
                appreciation = "ممتاز مرتفع"
            ElseIf Convert.ToInt32(total_points / total_units) <= 4.75 And Convert.ToInt32(total_points / total_units) > 4.5 Then
                appreciation = "ممتاز "
            ElseIf Convert.ToInt32(total_points / total_units) <= 4.5 And Convert.ToInt32(total_points / total_units) > 4.0 Then
                appreciation = "جيد جدا مرتفع "
            ElseIf Convert.ToInt32(total_points / total_units) <= 4.0 And Convert.ToInt32(total_points / total_units) > 3.5 Then
                appreciation = "جيد جداا "
            ElseIf Convert.ToInt32(total_points / total_units) < 3.5 And Convert.ToInt32(total_points / total_units) > 3 Then
                appreciation = "جيد مرتفع "
            ElseIf Convert.ToInt32(total_points / total_units) <= 3 And Convert.ToInt32(total_points / total_units) > 2.5 Then
                appreciation = "جيد "
            ElseIf Convert.ToInt32(total_points / total_units) <= 2.5 And Convert.ToInt32(total_points / total_units) > 2 Then
                appreciation = "مقبول مرتفع "
            ElseIf Convert.ToInt32(total_points / total_units) <= 2 And Convert.ToInt32(total_points / total_units) > 1 Then
                appreciation = "مقبول  "

            ElseIf Convert.ToInt32(total_points / total_units) < 1 Then
                appreciation = "راسب  "
            End If
            If dt4.Rows.Count <> 0 Then
                rdoc.SetParameterValue("total_units", total_units)
                rdoc.SetParameterValue("total_points", total_points)
                rdoc.SetParameterValue("total_degree", Math.Round((total_points / total_units), 2))
                rdoc.SetParameterValue("appreciation", appreciation)
            End If
            'If dt1.Rows.Count <> 0 Then
            '    rdoc.SetParameterValue("img_header_URL", dt1.Rows(0)("img_header").ToString)
            '    rdoc.SetParameterValue("img_footer_URL", dt1.Rows(0)("img_footer").ToString)
            'Else
            '    rdoc.SetParameterValue("img_header_URL", "")
            '    rdoc.SetParameterValue("img_footer_URL", "")

            'End If
            CrystalReportViewer1.ReportSource = rdoc

            'CrystalReportViewer1.DataBind()
            'Try
            '    Dim objDS As New DataSet
            '    Dim dfdoFile As New CrystalDecisions.Shared.DiskFileDestinationOptions
            '    Dim strServerPath As String
            '    Dim szFileName As String
            '    Create DataSet as per requirement
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