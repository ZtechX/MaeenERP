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
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            rdoc.Load(Server.MapPath("MeasurementSatisfaction.rpt"))

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