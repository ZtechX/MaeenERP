Imports System.IO
Imports BusinessLayer.BusinessLayer
Imports CrystalDecisions.Shared

Public Class consultationRep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Consult_id = Request.QueryString("Consult_id")
        If String.IsNullOrWhiteSpace(Consult_id) Then
            Dim script As String = "<script type='text/javascript' defer='defer'> alert('لا يوجد بيانات متاحة للعرض');</script>"
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
            ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)

        Else
            getReportData(Consult_id)
        End If

    End Sub
    Private Sub getReportData(ByVal Consult_id As String)
        Try
            Dim message As String = " لا يوجد بيانات متاحة للعرض"
            Dim rdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim dt1 As New DataTable
            dt1 = DBManager.Getdatatable("SELECT isNull(header_img,'') img_header,isNull(footer_img,'') img_footer FROM tblreport_settings where isNull(deleted,0) !=1 and comp_id=" + LoginInfo.GetComp_id())
            Dim dt2 As New DataTable

            Dim query = "SELECT ash_consultings.code,start_date_hj,source.Description as 'source',type.Description as 'type',
ash_consultings.name,nat.Description as nationality_id,identiy,gender.Description as'gender',dob_date_hj,
marital_stat.Description as'marital_status',wifes_no,(girls_no+boys_no) childerns_num ,girls_no,boys_no
,respnosible_name,Home.Description as 'home',T_ownership.Description as 'Type_ownership'
,edu_level.Description as 'edu_level',income_stat.Description as 'income_status'
,house_tele,tel2 as'phone_num',isNull(income_notes,'') as 'income_notes' ,isNull(consult_notes,'') as 'consult_note',isNull(ash_advisors.name,'') as 'advisor_nm'
  FROM ash_consultings left join tbllock_up source on source.id=ash_consultings.source_id
  left join tbllock_up type on type.id=ash_consultings.category_id
  left join tbllock_up gender on gender.id=ash_consultings.gender
  left join tbllock_up marital_stat on marital_stat.id=ash_consultings.marital_status
  left join tbllock_up Home on Home.id=ash_consultings.house_type
   left join tbllock_up T_ownership on T_ownership.id=ash_consultings.type_of_ownership
  left join tbllock_up edu_level on edu_level.id=ash_consultings.education_level
   left join tbllock_up income_stat on income_stat.id=ash_consultings.income_status
left join tbllock_up nat on nat.id=ash_consultings.nationality_id
   left join ash_advisors on ash_advisors.id= ash_consultings.advisor_id where ash_consultings.id=" + Consult_id
            dt2 = DBManager.Getdatatable(query)
            If dt2.Rows.Count <> 0 Then
                Dim ds As New consultationDS
                ds.Tables(0).Rows.Add()
                ds.Tables("Details").Rows(0).Item("code") = dt2.Rows(0).Item("code").ToString
                ds.Tables("Details").Rows(0).Item("start_date_hj") = dt2.Rows(0).Item("start_date_hj").ToString
                Dim type = dt2.Rows(0).Item("type").ToString
                If type = "عنف" Then
                    ds.Tables("Details").Rows(0).Item("violence") = 1
                ElseIf type = "حضانة" Then
                    ds.Tables("Details").Rows(0).Item("Incubation") = 1
                ElseIf type = "نفقة" Then
                    ds.Tables("Details").Rows(0).Item("expense") = 1
                ElseIf type = "خلع" Then
                    ds.Tables("Details").Rows(0).Item("stripped_off") = 1
                ElseIf type = "آخرى" Then
                    ds.Tables("Details").Rows(0).Item("other_type") = 1
                End If
                ds.Tables("Details").Rows(0).Item("full_name") = dt2.Rows(0).Item("name").ToString
                ds.Tables("Details").Rows(0).Item("nationality") = dt2.Rows(0).Item("nationality_id").ToString
                ds.Tables("Details").Rows(0).Item("identiy") = dt2.Rows(0).Item("identiy").ToString
                Dim gender = dt2.Rows(0).Item("gender").ToString
                If gender = "ذكر" Then
                    ds.Tables("Details").Rows(0).Item("male") = 1
                ElseIf gender = "أنثى" Then
                    ds.Tables("Details").Rows(0).Item("female") = 1
                End If
                ds.Tables("Details").Rows(0).Item("dob_date_hj") = dt2.Rows(0).Item("dob_date_hj").ToString
                Dim marital_status = dt2.Rows(0).Item("marital_status").ToString
                If marital_status = "اعزب" Then
                    ds.Tables("Details").Rows(0).Item("violence") = 1
                ElseIf marital_status = "متزوج" Then
                    ds.Tables("Details").Rows(0).Item("Incubation") = 1
                ElseIf marital_status = "مطلق" Then
                    ds.Tables("Details").Rows(0).Item("expense") = 1
                ElseIf marital_status = "ارملـ/ــة" Then
                    ds.Tables("Details").Rows(0).Item("stripped_off") = 1

                End If
                ds.Tables("Details").Rows(0).Item("wifes_no") = dt2.Rows(0).Item("wifes_no").ToString
                ds.Tables("Details").Rows(0).Item("childerns_num") = dt2.Rows(0).Item("childerns_num").ToString
                ds.Tables("Details").Rows(0).Item("girls_no") = dt2.Rows(0).Item("girls_no").ToString
                ds.Tables("Details").Rows(0).Item("boys_no") = dt2.Rows(0).Item("boys_no").ToString
                ds.Tables("Details").Rows(0).Item("respnosible_name") = dt2.Rows(0).Item("respnosible_name").ToString
                Dim home = dt2.Rows(0).Item("home").ToString
                If home = "شقة" Then
                    ds.Tables("Details").Rows(0).Item("Flat") = 1
                ElseIf home = "منزل شعبى" Then
                    ds.Tables("Details").Rows(0).Item("popular_house") = 1
                ElseIf home = "فيلا" Then
                    ds.Tables("Details").Rows(0).Item("villa") = 1
                ElseIf home = "قصر" Then
                    ds.Tables("Details").Rows(0).Item("palace") = 1
                ElseIf home = "دور" Then
                    ds.Tables("Details").Rows(0).Item("door") = 1
                ElseIf home = "آخرى" Then
                    ds.Tables("Details").Rows(0).Item("other_home") = 1
                End If
                Dim Type_ownership = dt2.Rows(0).Item("Type_ownership").ToString
                If Type_ownership = "ملك" Then
                    ds.Tables("Details").Rows(0).Item("owner") = 1
                ElseIf Type_ownership = "إيجار" Then
                    ds.Tables("Details").Rows(0).Item("rent") = 1
                End If
                Dim edu_level = dt2.Rows(0).Item("edu_level").ToString
                If edu_level = "لا يقرأ ولا يكتب" Then
                    ds.Tables("Details").Rows(0).Item("without_edu") = 1
                ElseIf edu_level = "متوسط" Then
                    ds.Tables("Details").Rows(0).Item("min_edu") = 1
                ElseIf edu_level = "جامعى فما فوق" Then
                    ds.Tables("Details").Rows(0).Item("top_edue") = 1
                ElseIf edu_level = "ثانوى" Then
                    ds.Tables("Details").Rows(0).Item("sec_edu") = 1
                ElseIf edu_level = "ابتدائى" Then
                    ds.Tables("Details").Rows(0).Item("low_edu") = 1
                End If
                Dim income_status = dt2.Rows(0).Item("income_status").ToString
                If income_status = "ضعيف" Then
                    ds.Tables("Details").Rows(0).Item("low_income") = 1
                ElseIf income_status = "متوسط" Then
                    ds.Tables("Details").Rows(0).Item("min_income") = 1
                ElseIf income_status = "ممتاز" Then
                    ds.Tables("Details").Rows(0).Item("exc_income") = 1
                End If
                ds.Tables("Details").Rows(0).Item("house_tele") = dt2.Rows(0).Item("house_tele").ToString
                ds.Tables("Details").Rows(0).Item("phone_num") = dt2.Rows(0).Item("phone_num").ToString
                ds.Tables("Details").Rows(0).Item("income_notes") = dt2.Rows(0).Item("income_notes").ToString
                ds.Tables("Details").Rows(0).Item("consult_note") = dt2.Rows(0).Item("consult_note").ToString
                ds.Tables("Details").Rows(0).Item("advisor_nm") = dt2.Rows(0).Item("advisor_nm").ToString
                rdoc.Load(Server.MapPath("consultation.rpt"))
                rdoc.SetDataSource(ds.Tables("Details"))
                If dt1.Rows.Count <> 0 Then
                    rdoc.SetParameterValue("img_header_URL", dt1.Rows(0)("img_header").ToString)
                    rdoc.SetParameterValue("img_footer_URL", dt1.Rows(0)("img_footer").ToString)
                Else
                    rdoc.SetParameterValue("img_header_URL", "")
                    rdoc.SetParameterValue("img_footer_URL", "")

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