Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class MeasurementSatisfactionRep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        getReportData()


    End Sub
    Private Sub getReportData()
        Try
            Dim dt1 As New DataTable
            dt1 = DBManager.Getdatatable("SELECT isNull(header_img,'') img_header,isNull(footer_img,'') img_footer FROM tblreport_settings where isNull(deleted,0) !=1 and comp_id=" + LoginInfo.GetComp_id())

            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            rdoc.Load(Server.MapPath("MeasurementSatisfaction.rpt"))

            If dt1.Rows.Count <> 0 Then
                rdoc.SetParameterValue("img_header_URL", dt1.Rows(0)("img_header").ToString)
                rdoc.SetParameterValue("img_footer_URL", dt1.Rows(0)("img_footer").ToString)
            Else
                rdoc.SetParameterValue("img_header_URL", "")
                rdoc.SetParameterValue("img_footer_URL", "")

            End If
            CrystalReportViewer1.ReportSource = rdoc

            CrystalReportViewer1.DataBind()

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
    End Sub

End Class