Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared
Public Class student_money_rep
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
            Dim ds As New student_moneyDS
            Dim query = "select tblusers.full_name as full_name ,tblusers.user_indenty as user_indenty from tblusers where tblusers.id=" + diplome_user
            Dim query1 = "select amount as amount,acd_payments.date_hj as date_hj,tbllock_up.description as payment,acd_payments.approved as approved from acd_payments join tbllock_up on acd_payments.payment_type=tbllock_up.id where acd_payments.type=2 and acd_payments.course_id=" + diplome_id.ToString + " and acd_payments.student_id =" + diplome_user.ToString
            dt2 = DBManager.Getdatatable(query)
            dt3 = DBManager.Getdatatable(query1)
            Dim row = 0
            If dt2.Rows.Count <> 0 Then
                ds.Tables("student_money").Rows.Add()
                ds.Tables("student_money").Rows(0).Item("student") = dt2.Rows(0).Item("full_name").ToString
                ds.Tables("student_money").Rows(0).Item("user_identy") = dt2.Rows(0).Item("user_indenty").ToString
            End If

            If dt3.Rows.Count <> 0 Then
                Dim rowsCount = dt3.Rows.Count - 1
                Dim approved = ""
                For index As Integer = 0 To rowsCount
                    If dt3.Rows(row).Item("approved").ToString = 0 Then
                        approved = "قيد المراجعة"
                    ElseIf dt3.Rows(row).Item("approved").ToString = 1 Then
                        approved = "تمت الموافقة"
                    Else
                        approved = " مرفوضة"
                    End If
                    ds.Tables("student_money").Rows.Add()
                    ds.Tables("student_money").Rows(row).Item("amount") = dt3.Rows(row).Item("amount").ToString
                    ds.Tables("student_money").Rows(row).Item("payment_type") = dt3.Rows(row).Item("payment").ToString
                    ds.Tables("student_money").Rows(row).Item("date") = dt3.Rows(row).Item("date_hj").ToString
                    ds.Tables("student_money").Rows(row).Item("status") = approved.ToString
                    row = row + 1
                Next

            End If

            rdoc.Load(Server.MapPath("student_money.rpt"))
            'rdoc.SetDataSource(ds.Tables("degrees"))
            rdoc.SetDataSource(ds.Tables("student_money"))
            'If dt1.Rows.Count <> 0 Then
            '    rdoc.SetParameterValue("img_header_URL", dt1.Rows(0)("img_header").ToString)
            '    rdoc.SetParameterValue("img_footer_URL", dt1.Rows(0)("img_footer").ToString)
            'Else
            '    rdoc.SetParameterValue("img_header_URL", "")
            '    rdoc.SetParameterValue("img_footer_URL", "")

            'End If
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