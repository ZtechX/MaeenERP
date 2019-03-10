Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class TechRepForMembersRep
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

            Dim query = "select advisor_id,name ,Description,count(status) as 'count'
from ash_cases left join ash_advisors on ash_advisors.id=ash_cases.advisor_id
left join tbllock_up on tbllock_up.id=ash_cases.status
group by advisor_id, name,Description order by name"
            dt2 = DBManager.Getdatatable(query)
            Dim index = -1
            Dim ds As New TechRepForMembersDS
            If dt2.Rows.Count <> 0 Then
                Dim advisor_id = ""
                Dim desc = ""
                Dim sum = 0
                Dim count = ""
                For Each row As DataRow In dt2.Rows

                    If advisor_id <> row("advisor_id").ToString() Then
                        advisor_id = row("advisor_id").ToString()
                        ds.Tables(0).Rows.Add()
                        index = index + 1
                        ds.Tables("Details").Rows(index).Item("Row_num") = (index + 1)
                        sum = 0
                    End If
                    ds.Tables("Details").Rows(index).Item("Advisor") = row("name").ToString
                    desc = row("Description").ToString
                    count = row("count").ToString
                    sum = sum + Convert.ToUInt32(count)
                    ds.Tables("Details").Rows(index).Item("Total") = sum
                    If desc = "تحت الاجراء" Then
                        ds.Tables("Details").Rows(index).Item("underProcedure") = count
                    ElseIf desc = "أنتهت بما نص عليه الحكم" Then
                        ds.Tables("Details").Rows(index).Item("Judgment") = count
                    ElseIf desc = "أنتهت بالصلح" Then
                        ds.Tables("Details").Rows(index).Item("conciliation") = count
                    ElseIf desc = "أنتهت بغير الصلح" Then
                        ds.Tables("Details").Rows(index).Item("without_conciliation") = count
                    ElseIf desc = "أنتهت بالتنازل" Then
                        ds.Tables("Details").Rows(index).Item("waiver") = count
                    ElseIf desc = "أحيلت لجهة آخرى" Then
                        ds.Tables("Details").Rows(index).Item("Referred") = count
                    ElseIf desc = "عدم حضور أحد الاطراف" Then
                        ds.Tables("Details").Rows(index).Item("No_attend") = count
                    End If

                Next
                rdoc.Load(Server.MapPath("TechRepForMembers.rpt"))
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