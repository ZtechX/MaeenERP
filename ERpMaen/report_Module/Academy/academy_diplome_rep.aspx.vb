Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared
Public Class academy_diplome_rep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim diplome_id = Request.QueryString("diplome_id")
        Dim diplome_user = Request.QueryString("diplome_user")
        Dim ddlsemster = Request.QueryString("semster_id")
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
            Dim dt5 As New DataTable
            Dim ds As New academy_diplomeDS
            Dim query1 = "select tblusers.full_name as full_name ,tblusers.user_indenty as user_indenty from tblusers where tblusers.id=" + diplome_user
            Dim query2 = "select acd_diplomes.name as diplome from acd_diplomes where acd_diplomes.id=" + diplome_id
            Dim query3 = "select acd_diplome_subjects.semster_id as semster_id,acd_semester.name as semster,tbllock_up.Description as subject,(acd_student_degrees.activity_degree+acd_student_degrees.final_degree) as total_degree,acd_diplome_subjects.code as code,acd_diplome_subjects.sub_code as symbol,acd_diplome_subjects.Units_Num as unit,period.Description as year from acd_diplome_subjects join acd_diplomes on acd_diplome_subjects.diplome_id=acd_diplomes.id  join tbllock_up on acd_diplome_subjects.subject_id=tbllock_up.id join acd_student_degrees on acd_diplome_subjects.id=acd_student_degrees.course_id join acd_semester on acd_semester.id=acd_diplome_subjects.semster_id join tbllock_up as period on acd_semester.year_id=period.id  where acd_student_degrees.approved=1 and acd_student_degrees.student_id=" + diplome_user.ToString + " and acd_diplome_subjects.diplome_id=" + diplome_id.ToString
            Dim query4 = "select acd_diplome_subjects.semster_id as semster_id, sum(acd_diplome_subjects.Units_Num) as unit,sum(acd_student_degrees.activity_degree+acd_student_degrees.final_degree) as total_degree from acd_diplome_subjects join acd_diplomes on acd_diplome_subjects.diplome_id=acd_diplomes.id  join tbllock_up on acd_diplome_subjects.subject_id=tbllock_up.id join acd_student_degrees on acd_diplome_subjects.id=acd_student_degrees.course_id join acd_semester on acd_semester.id=acd_diplome_subjects.semster_id join tbllock_up as period on acd_semester.year_id=period.id  where acd_student_degrees.student_id=" + diplome_user.ToString + " and acd_diplome_subjects.diplome_id=" + diplome_id.ToString + " group by semster_id"
            dt2 = DBManager.Getdatatable(query1)
            dt3 = DBManager.Getdatatable(query2)
            dt4 = DBManager.Getdatatable(query3)
            dt5 = DBManager.Getdatatable(query4)
            'dt5.Columns.Add("TotalUnits", System.String())
            Dim row = 0
            Dim code = ""
            Dim total_units As Double = 0
            Dim total_points As Double = 0
            Dim points_final As Double = 0
            Dim units_final As Double = 0
            dt4.Columns.Add("total_unit", Type.GetType("System.String"))
            dt4.Columns.Add("total_point", Type.GetType("System.String"))
            dt4.Columns.Add("points", Type.GetType("System.String"))
            dt4.Columns.Add("appreciation", Type.GetType("System.String"))
            Dim rowsCount2 = dt4.Rows.Count - 1
            Dim rowsCount3 = dt5.Rows.Count - 1
            If dt4.Rows.Count <> 0 Then
                For index1 As Integer = 0 To rowsCount3
                    total_units = 0
                    total_points = 0

                    For index As Integer = 0 To rowsCount2
                        If dt5.Rows(index1).Item("semster_id").ToString = dt4.Rows(index).Item("semster_id").ToString Then
                            total_units = total_units + Convert.ToInt32(dt4.Rows(index).Item("unit").ToString)
                            total_points = total_points + (((dt4.Rows(index).Item("total_degree").ToString) / ((dt4.Rows(index).Item("unit").ToString) * 10)) * (dt4.Rows(index).Item("unit").ToString))
                        End If
                    Next
                    ' Dim newrow As DataRow = dt4.NewRow()

                    For index As Integer = 0 To rowsCount2
                        If dt5.Rows(index1).Item("semster_id").ToString = dt4.Rows(index).Item("semster_id").ToString Then
                            dt4.Rows(index).Item("total_unit") = total_units
                            dt4.Rows(index).Item("total_point") = Math.Round(total_points, 2, MidpointRounding.AwayFromZero)
                            dt4.Rows(index).Item("points") = Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero)
                            If Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) >= 4.75 Then
                                dt4.Rows(index).Item("appreciation") = "ممتاز مرتفع"
                            ElseIf Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) <= 4.75 And Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) > 4.5 Then
                                dt4.Rows(index).Item("appreciation") = "ممتاز "
                            ElseIf Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) <= 4.5 And Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) > 4.0 Then
                                dt4.Rows(index).Item("appreciation") = "جيد جدا مرتفع "
                            ElseIf Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) <= 4.0 And Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) > 3.5 Then
                                dt4.Rows(index).Item("appreciation") = "جيد جداا "
                            ElseIf Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) < 3.5 And Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) > 3 Then
                                dt4.Rows(index).Item("appreciation") = "جيد مرتفع "
                            ElseIf Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) <= 3 And Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) > 2.5 Then
                                dt4.Rows(index).Item("appreciation") = "جيد "
                            ElseIf Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) <= 2.5 And Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) > 2 Then
                                dt4.Rows(index).Item("appreciation") = "مقبول مرتفع "
                            ElseIf Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) <= 2 And Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) > 1 Then
                                dt4.Rows(index).Item("appreciation") = "مقبول  "

                            ElseIf Math.Round(total_points / total_units, 2, MidpointRounding.AwayFromZero) < 1 Then
                                dt4.Rows(index).Item("appreciation") = "راسب  "
                            End If
                        End If
                    Next
                    'newrow("total_point") = total_units
                    'newrow("semster_id") = dt5.Rows(index1).Item("semster_id").ToString
                    'dt4.Rows.Add(newrow)
                    'ds.Tables("accadmy_diplome").Rows(index1).Item("total_points") = total_points
                    'ds.Tables("accadmy_diplome").Rows(index1).Item("total_units") = total_units
                Next
                Dim rowsCount = dt4.Rows.Count - 1
                Dim rowsCount1 = dt5.Rows.Count - 1
                For index As Integer = 0 To rowsCount
                    If dt4.Rows(index).Item("total_degree").ToString <> "" Then

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
                    End If
                    ds.Tables("accadmy_diplome").Rows.Add()
                    If dt4.Rows(index).Item("subject").ToString <> "" Then
                        ds.Tables("accadmy_diplome").Rows(row).Item("course") = dt4.Rows(index).Item("subject").ToString
                        ds.Tables("accadmy_diplome").Rows(row).Item("semster") = dt4.Rows(index).Item("semster").ToString
                        ds.Tables("accadmy_diplome").Rows(row).Item("year") = dt4.Rows(index).Item("year").ToString
                        ds.Tables("accadmy_diplome").Rows(row).Item("semster_id") = dt4.Rows(index).Item("semster_id").ToString
                        ds.Tables("accadmy_diplome").Rows(row).Item("final_degree") = Convert.ToInt32(dt4.Rows(index).Item("total_degree").ToString)
                        ds.Tables("accadmy_diplome").Rows(row).Item("code") = code
                        ds.Tables("accadmy_diplome").Rows(row).Item("symbol") = dt4.Rows(index).Item("symbol").ToString
                        ds.Tables("accadmy_diplome").Rows(row).Item("unit") = Convert.ToInt32(dt4.Rows(index).Item("unit").ToString)
                    End If
                    If dt4.Rows(index).Item("total_unit").ToString <> "" And dt4.Rows(index).Item("total_unit").ToString <> "" Then
                        ds.Tables("accadmy_diplome").Rows(row).Item("total_units") = dt4.Rows(index).Item("total_unit").ToString
                        ds.Tables("accadmy_diplome").Rows(row).Item("total_points") = dt4.Rows(index).Item("total_point").ToString
                        ds.Tables("accadmy_diplome").Rows(row).Item("points") = dt4.Rows(index).Item("points").ToString
                        ds.Tables("accadmy_diplome").Rows(row).Item("appreciation") = dt4.Rows(index).Item("appreciation").ToString
                    End If
                    units_final = units_final + Convert.ToInt32(dt4.Rows(index).Item("unit").ToString)
                    points_final = points_final + ((Convert.ToInt32(dt4.Rows(index).Item("total_degree").ToString) / (Convert.ToInt32(dt4.Rows(index).Item("unit").ToString) * 10)) * Convert.ToInt32(dt4.Rows(index).Item("unit").ToString))

                    row = row + 1

                Next




                'ds.Tables("accadmy_session").Rows(row).Item("total_units") = total_units
                'ds.Tables("accadmy_session").Rows(row).Item("total_points") = total_points

            End If



            rdoc.Load(Server.MapPath("academy_diplome.rpt"))
            'rdoc.SetDataSource(ds.Tables("degrees"))
            rdoc.SetDataSource(ds.Tables("accadmy_diplome"))
            Dim appreciation = ""

            If Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) >= 4.75 Then
                appreciation = "ممتاز مرتفع"
            ElseIf Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) <= 4.75 And Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) > 4.5 Then
                appreciation = "ممتاز "
            ElseIf Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) <= 4.5 And Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) > 4.0 Then
                appreciation = "جيد جدا مرتفع "
            ElseIf Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) <= 4.0 And Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) > 3.5 Then
                appreciation = "جيد جداا "
            ElseIf Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) < 3.5 And Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) > 3 Then
                appreciation = "جيد مرتفع "
            ElseIf Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) <= 3 And Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) > 2.5 Then
                appreciation = "جيد "
            ElseIf Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) <= 2.5 And Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) > 2 Then
                appreciation = "مقبول مرتفع "
            ElseIf Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) <= 2 And Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) > 1 Then
                appreciation = "مقبول  "

            ElseIf Math.Round(points_final / units_final, 2, MidpointRounding.AwayFromZero) < 1 Then
                appreciation = "راسب  "
            End If

            'rdoc.SetParameterValue("total_degree", Math.Round((total_points / total_units), 2))
            If dt4.Rows.Count <> 0 Then
                rdoc.SetParameterValue("total_units", units_final)
                rdoc.SetParameterValue("total_points", points_final)
                rdoc.SetParameterValue("total_degree", Math.Round((points_final / units_final), 2))
                rdoc.SetParameterValue("appreciation", appreciation)
            End If
            'If dt1.Rows.Count <> 0 Then
            '    rdoc.SetParameterValue("img_header_URL", dt1.Rows(0)("img_header").ToString)
            '    rdoc.SetParameterValue("img_footer_URL", dt1.Rows(0)("img_footer").ToString)
            'Else
            '    rdoc.SetParameterValue("img_header_URL", "")
            '    rdoc.SetParameterValue("img_footer_URL", "")

            'End If

            If dt2.Rows.Count <> 0 Then
                rdoc.SetParameterValue("student", dt2.Rows(0).Item("full_name").ToString)
                rdoc.SetParameterValue("id_number", dt2.Rows(0).Item("user_indenty").ToString)
            End If

            If dt3.Rows.Count <> 0 Then
                rdoc.SetParameterValue("diplome", dt3.Rows(0).Item("diplome").ToString)
            End If
            CrystalReportViewer1.ReportSource = rdoc

            CrystalReportViewer1.DataBind()
            'Try
            '    Dim objDS As New DataSet
            '    Dim dfdoFile As New CrystalDecisions.Shared.DiskFileDestinationOptions
            '    Dim strServerPath As String
            '    Dim szFileName As String

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