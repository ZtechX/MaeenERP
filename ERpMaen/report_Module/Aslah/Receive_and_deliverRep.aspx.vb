﻿Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class Receive_and_deliverRep
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
            dt1 = DBManager.Getdatatable("SELECT isNull(header_img,'') img_header,isNull(footer_img,'') img_footer FROM tblreport_settings where isNull(deleted,0) !=1 and comp_id=" + LoginInfo.GetComp_id())
            Dim dt2 As New DataTable
            Dim dt3 As New DataTable
            Dim dt4 As New DataTable
            Dim ds As New Receive_and_deliverDS
            Dim ds1 As New Receive_and_deliverDS
            Dim query = "SELECT ash_cases.code,ash_cases.date_h as 'case_dt',childrens_no
      ,instrument_no,instrument_date_h as 'instrument_dt',persons1.name as 'person_from',persons1.indenty as 'FPerson_num'
      ,persons2.name as 'person_against',persons2.indenty as 'AgPerson_num',tbllock_up.Description as 'service_type'       
  FROM ash_cases left join ash_case_persons persons1 on ash_cases.person1_id=persons1.id 
  left join ash_case_persons persons2 on ash_cases.person2_id=persons2.id 
  left join tbllock_up on tbllock_up.id=ash_cases.depart where ash_cases.id=" + Case_id
            dt2 = DBManager.Getdatatable(query)

            If dt2.Rows.Count <> 0 Then


                query = "SELECT  name,age FROM ash_case_children_receiving_details left join ash_case_childrens
on ash_case_childrens.id=ash_case_children_receiving_details.children_id where details_id=" + details_id
                dt3 = DBManager.Getdatatable(query)
                Dim row = 0
                If dt3.Rows.Count <> 0 Then

                    Dim rowsCount = dt3.Rows.Count - 1
                    For index As Integer = 0 To rowsCount Step 2
                        ds.Tables(0).Rows.Add()
                        ds.Tables("Details").Rows(row).Item("code") = dt2.Rows(0).Item("code").ToString
                        ds.Tables("Details").Rows(row).Item("case_dt") = dt2.Rows(0).Item("case_dt").ToString
                        ds.Tables("Details").Rows(row).Item("instrument_no") = dt2.Rows(0).Item("instrument_no").ToString
                        ds.Tables("Details").Rows(row).Item("instrument_dt") = dt2.Rows(0).Item("instrument_dt").ToString
                        ds.Tables("Details").Rows(row).Item("person_from") = dt2.Rows(0).Item("person_from").ToString
                        ds.Tables("Details").Rows(row).Item("FPerson_num") = dt2.Rows(0).Item("FPerson_num").ToString
                        ds.Tables("Details").Rows(row).Item("person_against") = dt2.Rows(0).Item("person_against").ToString
                        ds.Tables("Details").Rows(row).Item("AgPerson_num") = dt2.Rows(0).Item("AgPerson_num").ToString
                        ds.Tables("Details").Rows(row).Item("service_type") = dt2.Rows(0).Item("service_type").ToString
                        ds.Tables("Details").Rows(row).Item("childrens_no") = dt2.Rows(0).Item("childrens_no").ToString
                        ds.Tables("Details").Rows(row).Item("child1") = dt3.Rows(index).Item("name").ToString
                        ds.Tables("Details").Rows(row).Item("Age_child1") = dt3.Rows(index).Item("age").ToString
                        If (index + 1) <= rowsCount Then
                            ds.Tables("Details").Rows(row).Item("child2") = dt3.Rows(index + 1).Item("name").ToString
                            ds.Tables("Details").Rows(row).Item("Age_child2") = dt3.Rows(index + 1).Item("age").ToString
                        End If
                        row = row + 1
                    Next
                Else
                    ds.Tables(0).Rows.Add()
                    ds.Tables("Details").Rows(row).Item("code") = dt2.Rows(0).Item("code").ToString
                    ds.Tables("Details").Rows(row).Item("case_dt") = dt2.Rows(0).Item("case_dt").ToString
                    ds.Tables("Details").Rows(row).Item("instrument_no") = dt2.Rows(0).Item("instrument_no").ToString
                    ds.Tables("Details").Rows(row).Item("instrument_dt") = dt2.Rows(0).Item("instrument_dt").ToString
                    ds.Tables("Details").Rows(row).Item("person_from") = dt2.Rows(0).Item("person_from").ToString
                    ds.Tables("Details").Rows(row).Item("FPerson_num") = dt2.Rows(0).Item("FPerson_num").ToString
                    ds.Tables("Details").Rows(row).Item("person_against") = dt2.Rows(0).Item("person_against").ToString
                    ds.Tables("Details").Rows(row).Item("AgPerson_num") = dt2.Rows(0).Item("AgPerson_num").ToString
                    ds.Tables("Details").Rows(row).Item("service_type") = dt2.Rows(0).Item("service_type").ToString
                    ds.Tables("Details").Rows(row).Item("childrens_no") = dt2.Rows(0).Item("childrens_no").ToString

                End If

                query = "SELECT delivery.name as 'deliver_nm',delivery.indenty as 'deliver_num',receive.name as 'Receive_nm'
  ,receive.indenty as 'Receive_num',date_h as 'receive_dt',delivery_time,receiving_time
  FROM ash_case_receiving_delivery_details details left join ash_case_receiving_delivery_basic 
  on ash_case_receiving_delivery_basic.case_id=details.case_id
  left join ash_case_persons delivery on details.deliverer_id=delivery.id
  left join ash_case_persons receive on  details.reciever_id=receive.id
  where details.deleted !=1 and details.id=" + details_id
                dt4 = DBManager.Getdatatable(query)

                If dt4.Rows.Count <> 0 Then
                    ds1.Tables(0).Rows.Add()
                    ds1.Tables("Details").Rows(0).Item("Receive_nm") = dt4.Rows(0).Item("Receive_nm").ToString
                    ds1.Tables("Details").Rows(0).Item("Receive_num") = dt4.Rows(0).Item("Receive_num").ToString
                    ds1.Tables("Details").Rows(0).Item("deliver_nm") = dt4.Rows(0).Item("deliver_nm").ToString
                    ds1.Tables("Details").Rows(0).Item("deliver_num") = dt4.Rows(0).Item("deliver_num").ToString
                    ds1.Tables("Details").Rows(0).Item("receive_dt") = dt4.Rows(0).Item("receive_dt").ToString
                    ds1.Tables("Details").Rows(0).Item("delivery_time") = dt4.Rows(0).Item("delivery_time").ToString
                    ds1.Tables("Details").Rows(0).Item("receiving_time") = dt4.Rows(0).Item("receiving_time").ToString

                End If

                rdoc.Load(Server.MapPath("Receive_and_deliver.rpt"))
                rdoc.SetDataSource(ds.Tables("Details"))

                rdoc.OpenSubreport("subReceive_and_deliver.rpt").SetDataSource(ds1)


                If dt1.Rows.Count <> 0 Then
                    rdoc.SetParameterValue("img_header_URL", dt1.Rows(0)("img_header").ToString)
                    rdoc.SetParameterValue("img_footer_URL", dt1.Rows(0)("img_footer").ToString, "subReceive_and_deliver.rpt")
                Else
                    rdoc.SetParameterValue("img_header_URL", "")
                    rdoc.SetParameterValue("img_footer_URL", "", "subReceive_and_deliver.rpt")

                End If
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
            Else
                Dim script As String = "<script type='text/javascript' defer='defer'> alert('" + message + "');</script>"
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
                ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class