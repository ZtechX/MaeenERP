Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class ProceduresRep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Case_id = Request.QueryString("Case_id")
        If String.IsNullOrWhiteSpace(Case_id) Then
            Dim script As String = "<script type='text/javascript' defer='defer'> alert('لا يوجد بيانات متاحة للعرض');</script>"
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
            ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)

        Else
            getReportData(Case_id)
        End If

    End Sub
    Private Sub getReportData(ByVal Case_id As String)
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim dt1 As New DataTable
            'dt1 = DBManager.Getdatatable("SELECT img_header,img_footer FROM tbl_company_info")
            Dim dt2 As New DataTable

            Dim query = "SELECT Description,date_h
  FROM ash_case_correspondences left join tbllock_up on tbllock_up.id=ash_case_correspondences.type_correspondences 
  where case_id=" + Case_id + " order by date_h"
            dt2 = DBManager.Getdatatable(query)
            If dt2.Rows.Count <> 0 Then
                Dim ds As New ProceduresDS
                Dim Index = -1
                Dim procedure_dt = ""
                Dim desc = ""
                For Each row As DataRow In dt2.Rows
                    If procedure_dt <> row("date_h").ToString() Then
                        procedure_dt = row("date_h").ToString()
                        ds.Tables(0).Rows.Add()
                        Index = Index + 1
                    End If
                    ds.Tables("Details").Rows(Index).Item("date") = procedure_dt
                    desc = row("Description").ToString
                    If desc = "محادثة المدعى هاتفيا" Then
                        ds.Tables("Details").Rows(Index).Item("Call_owner") = 1
                    ElseIf desc = "محادثة المدعى هاتفيا" Then
                        ds.Tables("Details").Rows(Index).Item("Call_against") = 1
                    ElseIf desc = "تم استدعاء المدعى عليه" Then
                        ds.Tables("Details").Rows(Index).Item("Call_up_aginst") = 1
                    ElseIf desc = "الاجتماع بالمدعى" Then
                        ds.Tables("Details").Rows(Index).Item("Meet_owner") = 1
                    ElseIf desc = "الاجتماع بالمدعى عليه" Then
                        ds.Tables("Details").Rows(Index).Item("Meet_aginst") = 1
                    ElseIf desc = "إعداد مسودة محضر صلح" Then
                        ds.Tables("Details").Rows(Index).Item("Prepare_conciliation") = 1
                    ElseIf desc = "هاتف المدعى مغلق" Then
                        ds.Tables("Details").Rows(Index).Item("Owner_phon_Close") = 1
                    ElseIf desc = "هاتف المدعى عليه مغلق" Then
                        ds.Tables("Details").Rows(Index).Item("Aginst_phon_Close") = 1
                    ElseIf desc = "إعداد مسودة تقرير" Then
                        ds.Tables("Details").Rows(Index).Item("Prepare_Rep") = 1
                    ElseIf desc = "المدعى لا يجيب الاتصال" Then
                        ds.Tables("Details").Rows(Index).Item("Owner_not_answer") = 1
                    ElseIf desc = "المدعى عليه لا يجيب الاتصال" Then
                        ds.Tables("Details").Rows(Index).Item("Against_Not_answer") = 1
                    ElseIf desc = "العمل فى اجراءات القضية"Then
                        ds.Tables("Details").Rows(Index).Item("Action_on_case") = 1
                    End If

                Next
                rdoc.Load(Server.MapPath("Procedures.rpt"))
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