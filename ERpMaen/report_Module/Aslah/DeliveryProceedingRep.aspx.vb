Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class DeliveryProceedingRep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Case_id = Request.QueryString("Case_id")
        Dim details_id = Request.QueryString("details_id")
        If String.IsNullOrWhiteSpace(Case_id) Or String.IsNullOrWhiteSpace(details_id) Then
            Dim script As String = "<script type='text/javascript' defer='defer'> alert('لا يوجد بيانات متاحة للعرض');</script>"
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
            ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)

        Else
            getReportData(details_id, Case_id)
        End If

    End Sub
    Private Sub getReportData(ByVal details_id As String, ByVal Case_id As String)
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim dt1 As New DataTable
            'dt1 = DBManager.Getdatatable("SELECT img_header,img_footer FROM tbl_company_info")
            ' Dim dt2 As New DataTable
            Dim dt3 As New DataTable
            Dim dt4 As New DataTable
            Dim ds As New Receive_and_deliverDS
            Dim query = ""
            '           query = "SELECT ash_cases.code,ash_cases.date_h as 'case_dt',childrens_no
            '  '  ,instrument_no,instrument_date_h as 'instrument_dt',persons1.name as 'person_from',persons1.indenty as 'FPerson_num'
            '    ,persons2.name as 'person_against',persons2.indenty as 'AgPerson_num',tbllock_up.Description as 'service_type'       
            'FROM ash_cases left join ash_case_persons persons1 on ash_cases.person1_id=persons1.id 
            'left join ash_case_persons persons2 on ash_cases.person2_id=persons2.id 
            'left join tbllock_up on tbllock_up.id=ash_cases.depart where ash_cases.id=" + Case_id
            '          dt2 = DBManager.Getdatatable(query)

            '          If dt2.Rows.Count <> 0 Then


            query = "SELECT  name,age FROM ash_case_children_receiving_details left join ash_case_childrens
on ash_case_childrens.id=ash_case_children_receiving_details.children_id where details_id=" + details_id
            dt3 = DBManager.Getdatatable(query)
            'Dim row = 0
            If dt3.Rows.Count <> 0 Then
                query = "SELECT delivery.name as 'deliver_nm',delivery.indenty as 'deliver_num',receive.name as 'Receive_nm'
  ,receive.indenty as 'Receive_num',date_h as 'receive_dt',delivery_time,receiving_time
  FROM ash_case_receiving_delivery_details left join ash_case_receiving_delivery_basic 
  on ash_case_receiving_delivery_basic.case_id=ash_case_receiving_delivery_details.case_id
  left join ash_case_persons delivery on ash_case_receiving_delivery_details.deliverer_id=delivery.id
  left join ash_case_persons receive on  ash_case_receiving_delivery_details.reciever_id=receive.id
  where ash_case_receiving_delivery_details.id=" + details_id
                dt4 = DBManager.Getdatatable(query)
                Dim rowsCount = dt3.Rows.Count - 1
                For index As Integer = 0 To rowsCount Step 1
                    ds.Tables(0).Rows.Add()
                    If dt4.Rows.Count <> 0 Then
                        ds.Tables("Details").Rows(index).Item("Receive_nm") = dt4.Rows(0).Item("Receive_nm").ToString
                        ds.Tables("Details").Rows(index).Item("Receive_num") = dt4.Rows(0).Item("Receive_num").ToString
                        ds.Tables("Details").Rows(index).Item("deliver_nm") = dt4.Rows(0).Item("deliver_nm").ToString
                        ds.Tables("Details").Rows(index).Item("deliver_num") = dt4.Rows(0).Item("deliver_num").ToString
                        ds.Tables("Details").Rows(index).Item("receive_dt") = dt4.Rows(0).Item("receive_dt").ToString
                        ds.Tables("Details").Rows(index).Item("delivery_time") = dt4.Rows(0).Item("delivery_time").ToString
                        ds.Tables("Details").Rows(index).Item("receiving_time") = dt4.Rows(0).Item("receiving_time").ToString
                    End If
                    ds.Tables("Details").Rows(index).Item("child1") = dt3.Rows(index).Item("name").ToString

                Next

                ' End If

                rdoc.Load(Server.MapPath("DeliveryProceeding.rpt"))
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