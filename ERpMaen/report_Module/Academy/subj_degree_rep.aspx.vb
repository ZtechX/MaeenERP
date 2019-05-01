Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared
Public Class subj_degree_rep1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim diplome_subject = Request.QueryString("diplome_subject")
        getReportData(diplome_subject)
    End Sub
    Private Sub getReportData(diplome_subject)
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim dt1 As New DataTable
            dt1 = DBManager.Getdatatable("SELECT isNull(header_img,'') img_header,isNull(footer_img,'') img_footer FROM tblreport_settings where isNull(deleted,0) !=1 and comp_id=" + LoginInfo.GetComp_id())
            Dim dt2 As New DataTable
            Dim dt3 As New DataTable
            Dim ds As New subj_degDS
            Dim query = "select tblusers.full_name as name,acd_student_degrees.activity_degree as activity_degree,acd_student_degrees.final_degree as final_degree  from acd_student_degrees join tblusers on acd_student_degrees.student_id=tblusers.id where type=2 and course_id=" + diplome_subject.ToString
            Dim query1 = "select acd_diplomes.name as diplome,sub.Description as subject,acd_semester.name as semster,tblusers.full_name as username,period.Description as year from acd_diplome_subjects join tbllock_up as sub on acd_diplome_subjects.subject_id=sub.id join acd_diplomes on acd_diplome_subjects.diplome_id=acd_diplomes.id join tblusers on acd_diplome_subjects.trainer_id=tblusers.id join acd_semester on acd_semester.id=acd_diplome_subjects.semster_id join tbllock_up as period on acd_semester.year_id=period.id   where acd_diplome_subjects.id=" + diplome_subject.ToString
            dt2 = DBManager.Getdatatable(query)
            dt3 = DBManager.Getdatatable(query1)
            Dim row = 0


            If dt2.Rows.Count <> 0 Then
                Dim rowsCount = dt2.Rows.Count - 1
                For index As Integer = 0 To rowsCount
                    ds.Tables("basic_report").Rows.Add()
                    ds.Tables("basic_report").Rows(row).Item("all") = row + 1
                    ds.Tables("basic_report").Rows(row).Item("student") = dt2.Rows(index).Item("name").ToString
                    ds.Tables("basic_report").Rows(row).Item("midterm") = dt2.Rows(index).Item("activity_degree").ToString
                    ds.Tables("basic_report").Rows(row).Item("final") = dt2.Rows(index).Item("final_degree").ToString
                    ds.Tables("basic_report").Rows(row).Item("total") = Convert.ToInt32(dt2.Rows(index).Item("final_degree").ToString) + Convert.ToInt32(dt2.Rows(index).Item("activity_degree").ToString)
                    row = row + 1
                Next

            End If
            If dt3.Rows.Count <> 0 Then
                ' ds.Tables("basic_report").Rows.Add()
                ds.Tables("basic_report").Rows(0).Item("diplome") = dt3.Rows(0).Item("diplome").ToString
                ds.Tables("basic_report").Rows(0).Item("course") = dt3.Rows(0).Item("subject").ToString
                ds.Tables("basic_report").Rows(0).Item("semster") = dt3.Rows(0).Item("semster").ToString
                ds.Tables("basic_report").Rows(0).Item("year") = dt3.Rows(0).Item("year").ToString
                ds.Tables("basic_report").Rows(0).Item("trainer") = dt3.Rows(0).Item("username").ToString
                row = 1
            End If
            rdoc.Load(Server.MapPath("subj_degree.rpt"))
            'rdoc.SetDataSource(ds.Tables("degrees"))
            rdoc.SetDataSource(ds.Tables("basic_report"))
            If dt1.Rows.Count <> 0 Then
                rdoc.SetParameterValue("img_header_URL", dt1.Rows(0)("img_header").ToString)
                rdoc.SetParameterValue("img_footer_URL", dt1.Rows(0)("img_footer").ToString)
            Else
                rdoc.SetParameterValue("img_header_URL", "")
                rdoc.SetParameterValue("img_footer_URL", "")

            End If
            rdoc.SetParameterValue("trainer_name", dt3.Rows(0).Item("username").ToString)
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
            'Else
            'Dim script As String = "<script type='text/javascript' defer='defer'> alert('" + message + "');</script>"
            'ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
            'ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)
            'End If
        Catch ex As Exception
        End Try
    End Sub
End Class