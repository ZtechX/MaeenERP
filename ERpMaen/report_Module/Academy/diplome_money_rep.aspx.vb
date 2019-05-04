Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared
Public Class diplome_money_rep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim diplome_id = Request.QueryString("diplome_id")
        getReportData(diplome_id)
    End Sub
    Private Sub getReportData(diplome_id)
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim dt1 As New DataTable
            dt1 = DBManager.Getdatatable("SELECT isNull(header_img,'') img_header,isNull(footer_img,'') img_footer FROM tblreport_settings where isNull(deleted,0) !=1 and comp_id=" + LoginInfo.GetComp_id())
            Dim dt2 As New DataTable
            Dim dt3 As New DataTable
            Dim ds As New diplome_moneyDS
            Dim query = "select acd_diplomes.name as diplome,acd_diplomes.price as price from acd_diplomes where acd_diplomes.id=" + diplome_id
            Dim query1 = "  select tblusers.full_name as full_name,sum(amount) as amount from acd_payments join tblusers on tblusers.id=acd_payments.student_id   where approved=1 and acd_payments.course_id=" + diplome_id.ToString + " and type=2 group by  tblusers.full_name"
            dt2 = DBManager.Getdatatable(query)
            dt3 = DBManager.Getdatatable(query1)
            Dim row = 0


            If dt3.Rows.Count <> 0 Then
                Dim rowsCount = dt3.Rows.Count - 1
                For index As Integer = 0 To rowsCount
                    ds.Tables("diplome_money").Rows.Add()
                    ds.Tables("diplome_money").Rows(row).Item("student") = dt3.Rows(row).Item("full_name").ToString
                    ds.Tables("diplome_money").Rows(row).Item("paid") = dt3.Rows(row).Item("amount").ToString
                    ds.Tables("diplome_money").Rows(row).Item("amount") = Convert.ToInt32(dt2.Rows(0).Item("price").ToString) - Convert.ToInt32(dt3.Rows(row).Item("amount").ToString)
                    row = row + 1
                Next

            End If

            If dt2.Rows.Count <> 0 Then
                ds.Tables("diplome_money").Rows(0).Item("diplome") = dt2.Rows(0).Item("diplome").ToString
                ds.Tables("diplome_money").Rows(0).Item("price") = dt2.Rows(0).Item("price").ToString
            End If

            rdoc.Load(Server.MapPath("diplome_money.rpt"))
            'rdoc.SetDataSource(ds.Tables("degrees"))
            rdoc.SetDataSource(ds.Tables("diplome_money"))
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