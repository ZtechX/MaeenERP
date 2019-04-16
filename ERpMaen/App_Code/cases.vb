#Region "Import"
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Web.Script.Services
Imports BusinessLayer.BusinessLayer
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic
Imports System
Imports ERpMaen

#End Region

'Imports System.Xml
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<System.Web.Script.Services.ScriptService()>
Public Class cases
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
    Dim sendSMS As New Boolean

#End Region

#Region "check_userData"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function check_userData(ByVal related_id As String, ByVal user_indenty As String, ByVal User_PhoneNumber As String) As Boolean

        Try
            Dim dt As New DataTable
            If related_id = "" Then
                dt = DBManager.Getdatatable("Select * from TblUsers where user_indenty='" + user_indenty + "' or User_PhoneNumber='" + User_PhoneNumber + "'")
            Else
                dt = DBManager.Getdatatable("Select * from TblUsers where ( user_indenty='" + user_indenty + "' OR  User_PhoneNumber='" + User_PhoneNumber + "')  and User_Type != 9 and related_id !='" + related_id + "'")
            End If

            If dt.Rows.Count = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

    Private Function getadvisorUser_data(ByVal related_id As String) As String
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select isNull(id,0) id,isNull(User_PhoneNumber,0) User_PhoneNumber from tblUsers where User_Type=6 and related_id=" + related_id)

            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0).Item("id").ToString + "|" + dt.Rows(0).Item("User_PhoneNumber").ToString
            End If
            Return ""

        Catch ex As Exception
            Return ""

        End Try
    End Function

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Save(ByVal id As String, ByVal basicDataJson As Dictionary(Of String, Object), ByVal persons_owner As Dictionary(Of String, Object), ByVal person_against As Dictionary(Of String, Object), ByVal children As Dictionary(Of String, Object), ByVal status As Dictionary(Of String, Object), ByVal tabs As List(Of Object), ByVal attch_file_DataJsonList As List(Of Object)) As String
        Try
            sendSMS = LoginInfo.SendSMS()
            Dim group_id As String = "0"
            Dim group_id_dt As New DataTable
            group_id_dt = DBManager.Getdatatable("select id from tbllock_up where RelatedId=3 and Type='PG' and Comp_id=" + LoginInfo.GetComp_id())
            If group_id_dt.Rows.Count <> 0 Then
                group_id = group_id_dt.Rows(0)(0).ToString
            End If

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dictchildren As Dictionary(Of String, Object) = children
            Dim dictstatus As Dictionary(Of String, Object) = status
            Dim dictperson_owner As Dictionary(Of String, Object) = persons_owner
            Dim dictperson_against As Dictionary(Of String, Object) = person_against
            Dim dt_person_cases As DataTable

            Dim person1_id As String = ""
            Dim person2_id As String = ""
            If (dictperson_owner("indenty").ToString = dictperson_against("indenty").ToString) Or (dictperson_owner("phone").ToString = dictperson_against("phone").ToString) Then
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return "false|رقم الهوية أو رقم الجوال للمنفذ والمنفذ ضدده متشابهان"
            End If
            Dim old_advisor_phone = "0"
            Dim New_advisor_phone = "0"
            Dim new_advisor As String = "0"
            Dim old_advisor As String = "0"
            Dim res = getadvisorUser_data(dictstatus("advisor_id").ToString())
            If res <> "" Then
                Dim arr = res.Split("|")
                new_advisor = arr(0)
                New_advisor_phone = arr(1)
            End If


            Dim oldNotification As String = ""
            If id <> "" Then
                DBManager.ExcuteQuery("delete from ash_case_tabs where case_id=" + id)
                dt_person_cases = DBManager.Getdatatable("Select person1_id,person2_id,isNull(tblUsers.id,'0') as 'advisor_id',isNull(User_PhoneNumber,0) User_PhoneNumber from ash_cases  left join tblUsers" +
                                                         " on tblUsers.related_id=ash_cases.advisor_id and tblUsers.User_Type = 6  where ash_cases.id=" + id.ToString())
                person1_id = dt_person_cases.Rows(0).Item("person1_id")
                person2_id = dt_person_cases.Rows(0).Item("person2_id")
                old_advisor = dt_person_cases.Rows(0).Item("advisor_id")
                old_advisor_phone = dt_person_cases.Rows(0).Item("User_PhoneNumber")
                If old_advisor <> "0" And new_advisor <> old_advisor Then
                    Dim dt_not As New DataTable
                    dt_not = DBManager.Getdatatable("select id from  tblNotifications  where Deleted !=1 and RefCode=" + id + " and AssignedTo=" + old_advisor + " and NotTitle='إسناد حالة'")
                    If dt_not.Rows.Count <> 0 Then
                        oldNotification = dt_not.Rows(0)(0).ToString()
                    End If
                End If
            End If

            If Not check_userData(person1_id, dictperson_owner("indenty").ToString, dictperson_owner("phone").ToString) Then
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return "false|رقم الهوية أو رقم الجوال للمنفذ مُستخدم"
            End If
            If Not check_userData(person2_id, dictperson_against("indenty").ToString, dictperson_against("phone").ToString) Then
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return "false|رقم الهوية أو رقم جوال للمنفذ ضده مُستخدم"
            End If
            Dim user_person1_id As String = ""
            Dim user_person2_id As String = ""
            If person1_id <> "" Then
                Dim dt As New DataTable
                dt = DBManager.Getdatatable("select * from tblUsers where related_id=" + person1_id)
                If dt.Rows.Count <> 0 Then
                    user_person1_id = dt.Rows(0).Item("id").ToString
                End If
            End If
            If person2_id <> "" Then
                Dim dt As New DataTable
                dt = DBManager.Getdatatable("select * from tblUsers where related_id=" + person2_id)
                If dt.Rows.Count <> 0 Then
                    user_person2_id = dt.Rows(0).Item("id").ToString
                End If
            End If
            Dim login_user As String = LoginInfo.GetUser__Id()
            persons_owner.Add("user_id", login_user)
            persons_owner.Add("case_id", id)
            If Not PublicFunctions.TransUpdateInsert(persons_owner, "ash_case_persons", person1_id, _sqlconn, _sqltrans) Then
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return "False|لم يتم الحفظ"
            Else
                If person1_id = "" Then
                    person1_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                End If
                Dim dict As New Dictionary(Of String, Object)
                dict.Add("User_Password", persons_owner("phone").ToString)
                dict.Add("User_PhoneNumber", persons_owner("phone").ToString)
                dict.Add("full_name", persons_owner("name").ToString)
                dict.Add("user_indenty", persons_owner("indenty").ToString)
                dict.Add("related_id", person1_id)
                dict.Add("User_Type", "9")
                dict.Add("Active", 1)
                dict.Add("group_id", group_id)
                dict.Add("comp_id", LoginInfo.GetComp_id())
                If Not PublicFunctions.TransUpdateInsert(dict, "tblUsers", user_person1_id, _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return "False|لم يتم الحفظ"
                Else
                    If user_person1_id = "" Then
                        user_person1_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                    End If
                End If
            End If

            person_against.Add("user_id", login_user)
            person_against.Add("case_id", id)
            If Not PublicFunctions.TransUpdateInsert(person_against, "ash_case_persons", person2_id, _sqlconn, _sqltrans) Then
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return "False|لم يتم الحفظ"
            Else
                If person2_id = "" Then
                    person2_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                End If
                Dim dict As New Dictionary(Of String, Object)
                dict.Add("User_Password", person_against("phone").ToString)
                dict.Add("User_PhoneNumber", person_against("phone").ToString)
                dict.Add("full_name", person_against("name").ToString)
                dict.Add("user_indenty", person_against("indenty").ToString)
                dict.Add("related_id", person2_id)
                dict.Add("User_Type", "9")
                dict.Add("Active", 1)
                dict.Add("group_id", group_id)
                dict.Add("comp_id", LoginInfo.GetComp_id())
                If Not PublicFunctions.TransUpdateInsert(dict, "tblUsers", user_person2_id, _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return "False|لم يتم الحفظ"
                Else
                    If user_person2_id = "" Then
                        user_person2_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                    End If
                End If
            End If

            dictBasicDataJson.Add("comp_id", LoginInfo.GetComp_id())
            dictBasicDataJson.Add("person1_id", person1_id)
            dictBasicDataJson.Add("person2_id", person2_id)
            dictBasicDataJson.Add("boys_no", dictchildren("boys_no"))
            dictBasicDataJson.Add("girls_no", dictchildren("girls_no"))
            dictBasicDataJson.Add("childrens_no", dictchildren("childrens_no"))
            dictBasicDataJson.Add("status", dictstatus("status"))
            dictBasicDataJson.Add("depart", dictstatus("depart"))
            dictBasicDataJson.Add("advisor_id", dictstatus("advisor_id"))
            dictBasicDataJson.Add("court_details", dictstatus("court_details"))
            dictBasicDataJson.Add("details", dictstatus("details"))
            dictBasicDataJson.Add("user_id", login_user)

            Dim child_custody = dictchildren("child_custody").ToString
            If child_custody = dictperson_owner("name").ToString Then
                dictBasicDataJson.Add("child_custody", person1_id)
            ElseIf child_custody = dictperson_against("name").ToString Then
                dictBasicDataJson.Add("child_custody", person2_id)
            End If


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_cases", id, _sqlconn, _sqltrans) Then
                Dim case_id As String = ""
                If id <> "" Then
                    case_id = id
                    Dim dictNotification As New Dictionary(Of String, Object)
                    If old_advisor <> new_advisor Then
                        dictNotification.Add("RefCode", id)
                        dictNotification.Add("NotTitle", "إسناد حالة")
                        dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                        dictNotification.Add("AssignedTo", new_advisor)
                        dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                        dictNotification.Add("Remarks", "حالة")
                        dictNotification.Add("FormUrl", "Aslah_Module/cases.aspx?id=" + id)
                        If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Return "False|لم يتم الحفظ"
                        End If
                        If old_advisor <> "0" Then
                            ' DBManager.ExcuteQuery("update tblNotifications set  Deleted=1 where RefCode=" + id + " and AssignedTo=" + old_advisor + " and NotTitle='إسناد حالة'")
                            dictNotification("NotTitle") = "إلغاءإسناد حالة"
                            dictNotification("AssignedTo") = old_advisor
                            dictNotification("FormUrl") = "Aslah_Module/cases.aspx"
                            If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                            Dim dict_deleteNot As New Dictionary(Of String, Object)
                            dict_deleteNot.Add("Deleted", 1)
                            If Not PublicFunctions.TransUpdateInsert(dict_deleteNot, "tblNotifications", oldNotification, _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If

                        End If
                    End If
                Else
                    case_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                End If

                If id = "" Then
                    Dim dictNotification As New Dictionary(Of String, Object)
                    dictNotification.Add("RefCode", case_id)
                    dictNotification.Add("NotTitle", "")
                    dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                    dictNotification.Add("AssignedTo", "")
                    dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictNotification.Add("Remarks", "")
                    dictNotification.Add("FormUrl", "Aslah_Module/cases.aspx?id=" + case_id)
                    If old_advisor <> new_advisor Then
                        dictNotification("NotTitle") = "إسناد حالة"
                        dictNotification("AssignedTo") = new_advisor
                        dictNotification("Remarks") = "حالة"
                        If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Return "False|لم يتم الحفظ"
                        End If

                    End If
                    dictNotification("Remarks") = "إنشاء حالة"
                    dictNotification("NotTitle") = "تم إنشاء حالة خاصة بك"
                    dictNotification("AssignedTo") = user_person1_id
                    If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return "False|لم يتم الحفظ"
                    End If
                    dictNotification("NotTitle") = "تم إنشاء حالة خاصة لك"
                    dictNotification("AssignedTo") = user_person2_id
                    If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return "False|لم يتم الحفظ"
                    End If
            Dim dictPerson As New Dictionary(Of String, Object)
                    dictPerson.Add("case_id", case_id)

                    If Not PublicFunctions.TransUpdateInsert(dictPerson, "ash_case_persons", person1_id, _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return "False|لم يتم الحفظ"
                    End If
                    If Not PublicFunctions.TransUpdateInsert(dictPerson, "ash_case_persons", person2_id, _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return "False|لم يتم الحفظ"
                    End If

                End If

                For Each tab As Object In tabs
                    Dim dict_tabs As Dictionary(Of String, Object) = tab
                    dict_tabs.Add("case_id", case_id)
                    If Not PublicFunctions.TransUpdateInsert(dict_tabs, "ash_case_tabs", "", _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return "False|لم يتم الحفظ"

                    End If
                Next


                Dim SMSResult = ""
                If sendSMS Then

                    Try
                        Dim dic_sms_archive As New Dictionary(Of String, Object)
                        dic_sms_archive.Add("user_id", LoginInfo.GetUser__Id())
                        dic_sms_archive.Add("Message", "")
                        dic_sms_archive.Add("Send_To", "")
                        dic_sms_archive.Add("date_m", DateTime.Now.ToString("dd/MM/yyyy"))
                        dic_sms_archive.Add("event_id", case_id)
                        dic_sms_archive.Add("Type", "case")
                        dic_sms_archive.Add("comp_id", LoginInfo.GetComp_id())
                        Dim smsres = ""
                        If old_advisor <> new_advisor And new_advisor <> "0" Then
                            dic_sms_archive("Message") = "تم إسناد حالة جديدةإليك"
                            dic_sms_archive("Send_To") = New_advisor_phone
                            If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                            smsres = PublicFunctions.DoSendSMS(New_advisor_phone, "تم إسناد حالة جديدةإليك", PublicFunctions.GetIdentity(_sqlconn, _sqltrans))
                            SMSResult = SMSResult + smsres
                        End If
                        SMSResult = SMSResult + "#$"
                        If old_advisor <> "0" And id <> "" Then
                            dic_sms_archive("Message") = "تم إلغاء اسناد حالة منك"
                            dic_sms_archive("Send_To") = old_advisor
                            If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                            smsres = PublicFunctions.DoSendSMS(old_advisor, "تم إلغاء اسناد حالة منك", PublicFunctions.GetIdentity(_sqlconn, _sqltrans))
                            SMSResult = SMSResult + smsres
                        End If
                        SMSResult = SMSResult + "#$"
                        If id = "" Then
                            dic_sms_archive("Message") = "تم إنشاء حالة خاصة بك يمكنك تحميل التطبيق لمتابعتنا من خلاله"
                            dic_sms_archive("Send_To") = dictperson_owner("phone").ToString()
                            If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                            smsres = PublicFunctions.DoSendSMS(dictperson_owner("phone").ToString(), "تم إنشاء حالة خاصة بك يمكنك تحميل التطبيق لمتابعتنا من خلاله", PublicFunctions.GetIdentity(_sqlconn, _sqltrans))
                            SMSResult = SMSResult + smsres
                            SMSResult = SMSResult + "#$"
                            dic_sms_archive("Message") = "تم إنشاء حالة لك يمكنك تحميل التطبيق لمتابعتنا من خلاله"
                            dic_sms_archive("Send_To") = dictperson_against("phone").ToString()
                            If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                            smsres = PublicFunctions.DoSendSMS(dictperson_against("phone").ToString(), "تم إنشاء حالة لك يمكنك تحميل التطبيق لمتابعتنا من خلاله", PublicFunctions.GetIdentity(_sqlconn, _sqltrans))
                            SMSResult = SMSResult + smsres
                            SMSResult = SMSResult + "#$"
                        End If

                    Catch ex As Exception

                    End Try
                End If
                _sqltrans.Commit()
                _sqlconn.Close()
                Return "True|" + case_id + "|" + SMSResult
            End If
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return "False|لم يتم الحفظ"
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return "False|لم يتم الحفظ"
        End Try
    End Function
#End Region

#Region "new_person"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Save_person(ByVal basicDataJson As Dictionary(Of String, Object), ByVal id As String, ByVal case_id As String) As Integer
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson

            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUser__Id())
            If Not PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_persons", "", _sqlconn, _sqltrans) Then
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return 0
            End If
            Dim person_id As String = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)

            _sqltrans.Commit()
            _sqlconn.Close()
            Return person_id

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return 0
        End Try
    End Function
#End Region

#Region "save_children"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_children(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Integer
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_cases As New DataTable
            Dim boys = 0
            Dim girls = 0
            Dim children = 1
            Dim gender = dictBasicDataJson("gender").ToString
            dt_cases = DBManager.Getdatatable("Select * from ash_cases where  id=" + case_id.ToString())
            If id = "" Then
                Dim dt_childrens As New DataTable
                dt_childrens = DBManager.Getdatatable(" Select isNull((Select count(id) from ash_case_childrens where gender=1 And case_id=" + case_id.ToString() + " ),0) As boy,isNull((Select count(id) from ash_case_childrens where gender= 2 And case_id = " + case_id.ToString() + "),0) As girl from ash_case_childrens")
                If dt_childrens.Rows.Count <> 0 Then
                    boys = dt_childrens.Rows(0).Item("boy").ToString
                    girls = dt_childrens.Rows(0).Item("girl").ToString
                End If
                If gender = 1 Then
                    If boys = dt_cases.Rows(0).Item("boys_no").ToString Then
                        children = -1
                    Else
                    End If
                ElseIf gender = 2 Then
                    If girls = dt_cases.Rows(0).Item("girls_no").ToString Then
                        children = -2
                    End If
                End If
            End If


            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUser__Id())

            If children <> -1 And children <> -2 Then

                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_childrens", id, _sqlconn, _sqltrans) Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return children
                Else
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return -3
                End If
            Else
                Return children
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return -3
        End Try

    End Function
#End Region
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GET_delivery_Reciever_Data(ByVal case_id As String) As String
        Dim deliverer_id As String = ""
        Dim reciever_id As String = ""
        Dim advisor As String = ""
        Dim advisor_phone As String = ""
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select person1_id,person2_id ," &
" child_custody,advisor_id,advisor.User_PhoneNumber as advisor_phone from ash_cases" &
" left join tblUsers advisor on advisor.related_id=advisor_id and advisor.User_Type=6 where ash_cases.id=" + case_id)
            If dt.Rows.Count <> 0 Then
                deliverer_id = dt.Rows(0).Item("child_custody").ToString()
                advisor = dt.Rows(0).Item("advisor_id").ToString()
                advisor_phone = dt.Rows(0).Item("advisor_phone").ToString()
                Dim p1 = dt.Rows(0).Item("person1_id").ToString()
                Dim p2 = dt.Rows(0).Item("person2_id").ToString()
                If deliverer_id = p1 Then
                    reciever_id = p2
                Else
                    reciever_id = p1
                End If
            End If
            Return deliverer_id & "|" & reciever_id & "|" & advisor & "|" & advisor_phone
        Catch ex As Exception
            Return deliverer_id & "|" & reciever_id & "|" & advisor & "|" & advisor_phone
        End Try
    End Function
#Region "save_children_receive"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_children_receive(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object), ByVal arr_recive As List(Of Object), ByVal basicPeriod As Boolean) As String
        sendSMS = LoginInfo.SendSMS()
        _sqlconn.Open()
        _sqltrans = _sqlconn.BeginTransaction
        Dim expected_dates = ""
        Dim condation As String = ""
        Try

            Dim str_dates = ""
            If basicPeriod Then
                Dim dt_dates As New DataTable
                dt_dates = DBManager.Getdatatable("select date_m  from ash_case_receiving_delivery_details where isNULL(deleted,0) != 1 And case_id = " + case_id.ToString + " And Type = 1 ")
                If dt_dates.Rows.Count <> 0 Then
                    str_dates = PublicFunctions.ConvertDataTabletoString(dt_dates)
                End If
            End If

            Dim dt As New DataTable
            If id <> "" Then
                Dim delete_res = delete_Period(id, False)
                If delete_res = "False|لا يمكن حذف الفترة لوجود ارتباطات" Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return "False|لا يمكن تعديل الفترة لوجود ارتباطات"
                ElseIf delete_res = "False|لم يتم الحذف" Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return "False|لم يتم الحفظ"
                End If

            End If

            Dim childrens As New List(Of Object)

            Dim curr_dt = ""
            Dim basic As New Dictionary(Of String, Object)
            Dim saved = ""
            For Each obj As Object In arr_recive
                basic = obj
                If basicPeriod Then
                    curr_dt = basic("date_m").ToString
                    If Not str_dates.IndexOf(curr_dt) = -1 Then
                        expected_dates = expected_dates & curr_dt & " ــ  "
                        Continue For
                    End If
                End If
                saved = save_delivery_details("", case_id, obj, childrens, False)
                If saved = "0" Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return "False|لم يتم الحفظ"
                End If

            Next
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("case_id", case_id)
            If basicPeriod Then
                dictBasicDataJson.Add("basic_period", 1)
            End If

            dictBasicDataJson.Add("user_id", LoginInfo.GetUser__Id())
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_receiving_delivery_basic", id, _sqlconn, _sqltrans) Then
                _sqltrans.Commit()
                _sqlconn.Close()
                Return "True|" & expected_dates
            End If

            _sqltrans.Rollback()
            _sqlconn.Close()
            Return "False|لم يتم الحفظ"
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return "False|لم يتم الحفظ"
        End Try

    End Function
#End Region

#Region "save_delivery_details"
    <WebMethod(True)>
                                                                                                                                                                                                                    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_delivery_details(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object), ByVal childrens As List(Of Object), ByVal Access As Boolean) As String
        Dim Names As New List(Of String)(10)

        Try
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim advisor_id As String = ""
            Dim advisor_phone As String = ""
            If Access Then
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                Dim dt As New DataTable
                Dim advisor As String = ""

                dt = DBManager.Getdatatable("Select advisor_id ,advisor.id as user_id,User_PhoneNumber from ash_cases " +
" left join tblUsers advisor on advisor.related_id=advisor_id and advisor.User_Type=6 where ash_cases.id=" + case_id)
                If dt.Rows.Count <> 0 Then
                    advisor = dt.Rows(0).Item("advisor_id").ToString()
                    advisor_id = dt.Rows(0).Item("user_id").ToString()
                    advisor_phone = dt.Rows(0).Item("User_PhoneNumber").ToString()
                End If

                Dim dt_date As New DataTable
                If id = "" Then
                    dt_date = DBManager.Getdatatable("Select * from ash_case_receiving_delivery_details where deleted != 1 And case_id=" + case_id.ToString + " And type=" + dictBasicDataJson("type").ToString + " And date_m=" + PublicFunctions.ConvertDatetoNumber(dictBasicDataJson("date_m").ToString).ToString())
                    If dt_date.Rows.Count <> 0 Then
                        _sqltrans.Commit()
                        _sqlconn.Close()
                        Return "-100"
                    End If
                End If
                dictBasicDataJson.Add("advisor", advisor)
            Else
                advisor_id = LoginInfo.getadvisorUser_id(dictBasicDataJson("advisor").ToString())
                advisor_phone = dictBasicDataJson("advisor_phone").ToString
            End If


            dictBasicDataJson.Add("user_id", LoginInfo.GetUser__Id())

            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_receiving_delivery_details", id, _sqlconn, _sqltrans) Then
                Dim details_id As String = ""
                If id <> "" Then
                    details_id = id
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return details_id.ToString + "|" + dictBasicDataJson("type").ToString
                Else
                    details_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                    Dim deliverer_Data = LoginInfo.getperson_data(dictBasicDataJson("deliverer_id"))
                    Dim reciever_Data = LoginInfo.getperson_data(dictBasicDataJson("reciever_id"))
                    Dim deliverer_arr = deliverer_Data.Split("|")
                    Dim reciever_arr = reciever_Data.Split("|")
                    Dim advisor_arr As New List(Of String)
                    advisor_arr.Add(advisor_id)
                    advisor_arr.Add(advisor_phone)
                    Dim day = dictBasicDataJson("date_m").ToString
                    If Not PublicFunctions.save_recieve_not_SMS(_sqlconn, _sqltrans, day, details_id, deliverer_arr, reciever_arr, advisor_arr.ToArray()) Then
                        If Access Then
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                        End If
                        Return 0
                    End If
                    DBManager.ExcuteQuery("delete  from ash_case_children_receiving_details  where details_id=" + details_id.ToString)
                    For Each children As Object In childrens
                        id = 0
                        Dim dict_children As Dictionary(Of String, Object) = children
                        dict_children.Add("details_id", details_id)
                        PublicFunctions.TransUpdateInsert(dict_children, "ash_case_children_receiving_details", id, _sqlconn, _sqltrans)
                    Next

                    If Access Then
                        _sqltrans.Commit()
                        _sqlconn.Close()
                    End If

                    Return details_id.ToString + "|" + dictBasicDataJson("type").ToString

                End If

            Else
                If Access Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                End If
                Return 0
            End If

        Catch ex As Exception
            If Access Then
                _sqltrans.Rollback()
                _sqlconn.Close()
            End If
            Return 0

        End Try

    End Function
#End Region
#Region "save_conciliation"
    <WebMethod(True)>
                                                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function save_conciliation(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("comp_id", LoginInfo.GetComp_id())
            dictBasicDataJson.Add("user_id", LoginInfo.GetUser__Id())
            dictBasicDataJson.Add("conciliation_Time", String.Format("{0:hh:mm:ss tt}", DateTime.Now))
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_conciliation", id, _sqlconn, _sqltrans) Then
                _sqltrans.Commit()
                _sqlconn.Close()
                Return True
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try

    End Function
#End Region

#Region "save_correspondences"
    <WebMethod(True)>
                                                                                                                                                                                                                                                <System.Web.Script.Services.ScriptMethod()>
    Public Function save_correspondences(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As String
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_date As New DataTable
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If id = "" Then
                dt_date = DBManager.Getdatatable("select * from ash_case_correspondences where case_id=" + case_id.ToString + " and date_h='" + dictBasicDataJson("date_h").ToString + "'")
                If dt_date.Rows.Count <> 0 Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return -100
                End If
            End If
            Dim dt As New DataTable
            Dim advisor As String = ""
            dt = DBManager.Getdatatable("select advisor_id from ash_cases where id=" + case_id)
            If dt.Rows.Count <> 0 Then
                advisor = dt.Rows(0)(0).ToString()
            End If
            dictBasicDataJson.Add("advisor", advisor)
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_correspondences", id, _sqlconn, _sqltrans) Then

                Dim details_id = "0"
                If id <> "" Then
                    details_id = id
                Else
                    details_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                End If
                _sqltrans.Commit()
                _sqlconn.Close()
                Return details_id
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return 0
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return 0
        End Try

    End Function
#End Region

#Region "save_sessions"
    <WebMethod(True)>
                                                                                                                                                                                                                                                                    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_sessions(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object), ByVal childrens As List(Of Object), ByVal persons As List(Of Object), ByVal owner_id As String, ByVal second_party_id As String) As Integer
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_date As New DataTable
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("owner_id", owner_id)
            dictBasicDataJson.Add("second_party_id", second_party_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If id = "" Then
                dt_date = DBManager.Getdatatable("select * from ash_case_correspondences where case_id=" + case_id.ToString + " and date_h='" + dictBasicDataJson("date_h").ToString + "'")
                If dt_date.Rows.Count <> 0 Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return -100
                End If
            End If
            Dim dt As New DataTable
            Dim advisor As String = ""
            dt = DBManager.Getdatatable("select advisor_id from ash_cases where id=" + case_id)
            If dt.Rows.Count <> 0 Then
                advisor = dt.Rows(0)(0).ToString()
            End If
            dictBasicDataJson.Add("advisor", advisor)
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_sessions", id, _sqlconn, _sqltrans) Then
                Dim session_id = 0
                If id <> "" Then
                    session_id = id
                Else
                    session_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                End If
                DBManager.ExcuteQuery("delete  from ash_session_children  where session_id=" + session_id.ToString)
                For Each children As Object In childrens
                    id = 0
                    Dim dict_children As Dictionary(Of String, Object) = children
                    dict_children.Add("session_id", session_id)
                    PublicFunctions.TransUpdateInsert(dict_children, "ash_session_children", id, _sqlconn, _sqltrans)
                Next

                DBManager.ExcuteQuery("delete  from ash_session_companions  where session_id=" + session_id.ToString)
                For Each person As Object In persons
                    id = 0
                    Dim dict_persons As Dictionary(Of String, Object) = person
                    dict_persons.Add("session_id", session_id)
                    PublicFunctions.TransUpdateInsert(dict_persons, "ash_session_companions", id, _sqlconn, _sqltrans)
                Next
                _sqltrans.Commit()
                _sqlconn.Close()
                Return session_id
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return 0
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return 0
        End Try

    End Function
#End Region
#Region "save_expense_basic"
    <WebMethod(True)>
                                                                                                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function save_expense_basic(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_expense_basic", id, _sqlconn, _sqltrans) Then
                Dim basic_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)

                _sqltrans.Commit()
                    _sqlconn.Close()
                    Return True
                Else
                    _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try

    End Function
#End Region
#Region "save_expense_details"
    <WebMethod(True)>
                                                                                                                                                                                                                                                                                                <System.Web.Script.Services.ScriptMethod()>
    Public Function save_expense_details(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object), ByVal new_date_m As String, ByVal new_date_h As String) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_expenses_details", id, _sqlconn, _sqltrans) Then
                If dictBasicDataJson("done") = True Then
                    id = 0
                    Dim dict = New Dictionary(Of String, Object)
                    dict.Add("date_m", PublicFunctions.ConvertDatetoNumber(new_date_m))
                    dict.Add("date_h", new_date_h)
                    dict.Add("case_id", case_id)
                    PublicFunctions.TransUpdateInsert(dict, "ash_case_expenses_details", id, _sqlconn, _sqltrans)

                End If
                _sqltrans.Commit()
                _sqlconn.Close()
                Return True
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return True
        End Try

    End Function
#End Region

#Region "save_apprisal"
    <WebMethod(True)>
                                                                                                                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function save_apprisal(ByVal id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            DBManager.ExcuteQuery("delete from ash_appraisal where detail_id=" + dictBasicDataJson("detail_id").ToString)
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_appraisal", id, _sqlconn, _sqltrans) Then

                Dim basic_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                _sqltrans.Commit()
                _sqlconn.Close()
                Return True
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try

    End Function
#End Region
#Region "Get Serial"
    <WebMethod(True)>
                                                                                                                                                                                                                                                                                                                <System.Web.Script.Services.ScriptMethod()>
    Public Function getSerial() As Integer
        Dim dtm As New DataTable
        dtm = DBManager.Getdatatable("Select isNull(max(code) + 1,1) FROM ash_cases where comp_id=" + LoginInfo.GetComp_id())
        If dtm.Rows.Count <> 0 Then
            Return dtm.Rows(0)(0)
        End If
        Return 1
    End Function
#End Region

#Region "Get Serial_conciliation"
    <WebMethod(True)>
                                                                                                                                                                                                                                                                                                                            <System.Web.Script.Services.ScriptMethod()>
    Public Function getSerial_conciliation() As Integer
        Dim dtm As New DataTable
        dtm = DBManager.Getdatatable("Select isNull(max(code)+1,1) FROM ash_case_conciliation where comp_id=" + LoginInfo.GetComp_id)
        If dtm.Rows.Count <> 0 Then
            Return dtm.Rows(0)(0)
        End If
        Return 1
    End Function
#End Region

#Region "Get getSerial_correspondencesn"
    <WebMethod(True)>
                                                                                                                                                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function getSerial_correspondencesn() As Integer
        Dim dtm As New DataTable
        dtm = DBManager.Getdatatable("Select isNull(max(code)+1,1) FROM ash_case_correspondences")
        If dtm.Rows.Count <> 0 Then
            Return dtm.Rows(0)(0)
        End If
        Return 1
    End Function
#End Region

#Region "getSerial_sessions"
    <WebMethod(True)>
                                                                                                                                                                                                                                                                                                                                                    <System.Web.Script.Services.ScriptMethod()>
    Public Function getSerial_sessions() As Integer
        Dim dtm As New DataTable
        dtm = DBManager.Getdatatable("Select isNull(max(code)+1,1) FROM ash_case_sessions")
        If dtm.Rows.Count <> 0 Then
            Return dtm.Rows(0)(0)
        End If
        Return 1
    End Function
#End Region

#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                            <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                <System.Web.Script.Services.ScriptMethod()>
    Public Function Edit(ByVal editItemId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("tblcontacts_groups", editItemId)
            Names.Add("1")
            Names.Add(str)
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "Delete"
    ''' <summary>
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                    <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function Delete(ByVal deleteItems As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItems, "tblcontacts_groups") Then
                Names.Add("1")
                Names.Add("تم الحذف بنجاح!")
            Else
                Names.Add("2")
                Names.Add("لا يمكن الحذف!")
            End If
            Return Names.ToArray
        Catch
            Names.Add("2")
            Names.Add("لا يمكن الحذف!")
            Return Names.ToArray
        End Try
    End Function
#End Region
#Region "save_file"
    ''' <summary>"
    ''' Save About images into db 
    ''' </summary>
    Private Function Savea_ttch_file(ByVal attch_file_DataJsonList As List(Of Object), ByVal letter_Id As String) As Boolean
        Try
            DBManager.ExcuteQuery("delete from tblImages where Source_id=" + letter_Id.ToString + " and Source='cases' ")
            For Each file_JSON As Object In attch_file_DataJsonList

                Dim dictfile As Dictionary(Of String, Object) = file_JSON
                Dim Source_id = letter_Id
                Dim Source = "cases"
                Dim Image_path = dictfile("url").ToString
                Dim Image_name = dictfile("Name").ToString
                If dictfile("Name").ToString <> "" Then
                    Image_name = dictfile("Name").ToString.Substring(0, Math.Min(dictfile("Name").ToString.Length, 50))
                End If

                DBManager.ExcuteQuery("insert into tblImages(Source_id,Source,Image_path,Image_name) values(" + Source_id.ToString + ",'" + Source.ToString + "','" + Image_path.ToString + "','" + Image_name.ToString + "')")



            Next

            Return True

        Catch ex As Exception
            DBManager.ExcuteQuery("delete from tblImages where Source_id=" + letter_Id.ToString + " and Source='out_letter' ")
            Return False
        End Try
    End Function
#End Region
#Region "get_tabs"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                <WebMethod(True)>
                                                                                                                                                                                                                                                                                                                                                                                    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_tabs() As String()
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str1 = ""
        Try
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            Dim query As String = "select * from ash_tabs"
            Dim query1 As String = "select * from ash_case_childrens"

            dt = DBManager.Getdatatable(query)
            If dt.Rows.Count <> 0 Then
                str = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
            Else
                Names.Add("")
                Return Names.ToArray
            End If
            If dt1.Rows.Count <> 0 Then
                str1 = PublicFunctions.ConvertDataTabletoString(dt1)
                Names.Add(str1)
            Else
                Names.Add("")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region


#Region "get_checked_tab"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                <WebMethod(True)>
                                                                                                                                                                                                                                                                                                                                                                                                    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_checked_tab(ByVal id As String) As String()
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str1 = ""
        Try
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            Dim query As String = "select * from ash_case_tabs where case_id=" + id.ToString

            dt = DBManager.Getdatatable(query)
            If dt.Rows.Count <> 0 Then
                str = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
            Else
                Names.Add("")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region

#Region "get_dates"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                            <WebMethod(True)>
                                                                                                                                                                                                                                                                                                                                                                                                                <System.Web.Script.Services.ScriptMethod()>
    Public Function get_dates(ByVal new_date As String) As String()
        Dim Names As New List(Of String)(10)

        Try
            Names.Add("0")
            Names.Add("0")
            Names.Add("0")
            Dim related_id As String = LoginInfo.getrelatedId()


            Dim dt As New DataTable
            Dim dt1 As New DataTable
            Dim dt2 As New DataTable
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            Dim condation As String = ""
            If LoginInfo.getUserType = "9" Then
                condation = " and (deliverer_id = " + related_id + " or reciever_id= " + related_id + " )"
            End If
            If LoginInfo.getUserType = "6" Then
                condation = " and advisor = " + related_id
            End If

            Dim query As String = "SELECT  * FROM   ash_case_receiving_delivery_details  where deleted !=1 and convert(varchar,date_m) like '" + new_date.ToString + "__' and case_id in (select id from ash_cases where comp_id=" + LoginInfo.GetComp_id() + ")" + condation + " order by date_m"
            ' Dim query As String = "SELECT  * FROM   ash_case_receiving_delivery_details  where deleted !=1 and date_h LIKE '___" + new_date.ToString + "%' and case_id in (select id from ash_cases where comp_id=" + LoginInfo.GetComp_id() + ")" + condation + " order by date_m"

            dt = DBManager.Getdatatable(Query)
            If dt.Rows.Count <> 0 Then
                Names(0) = PublicFunctions.ConvertDataTabletoString(dt)
            End If
            If LoginInfo.getUserType = "9" Then
                Return Names.ToArray
            End If
            Dim query1 As String = "SELECT * FROM   ash_case_sessions  where convert(varchar,date_m) like '" + new_date.ToString + "__' and case_id in (select id from ash_cases where comp_id=" + LoginInfo.GetComp_id() + ")" + condation
            Dim query2 As String = "SELECT * FROM   ash_case_correspondences  where convert(varchar,date_m) like '" + new_date.ToString + "__' and case_id in (select id from ash_cases where comp_id=" + LoginInfo.GetComp_id() + ")" + condation
            'Dim query1 As String = "SELECT * FROM   ash_case_sessions  where date_h LIKE '___" + new_date.ToString + "%' and case_id in (select id from ash_cases where comp_id=" + LoginInfo.GetComp_id() + ")" + condation
            'Dim query2 As String = "SELECT * FROM   ash_case_correspondences  where date_h LIKE '___" + new_date.ToString + "%' and case_id in (select id from ash_cases where comp_id=" + LoginInfo.GetComp_id() + ")" + condation

            dt1 = DBManager.Getdatatable(query1)
            dt2 = DBManager.Getdatatable(query2)

            If dt1.Rows.Count <> 0 Then
                Names(1) = PublicFunctions.ConvertDataTabletoString(dt1)
            End If
            If dt2.Rows.Count <> 0 Then
                Names(2) = PublicFunctions.ConvertDataTabletoString(dt2)
            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "get_cases"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                <WebMethod(True)>
                                                                                                                                                                                                                                                                                                                                                                                                                                    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_cases(ByVal date_m As String) As String()
        Dim Names As New List(Of String)(10)
        Names.Add("0")
        Names.Add("0")
        Names.Add("0")
        Try
            Dim _date As String = PublicFunctions.ConvertDatetoNumber(date_m)
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            Dim dt2 As New DataTable
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            Dim related_id As String = LoginInfo.getrelatedId()
            Dim condation As String = ""
            If LoginInfo.getUserType = "9" Then
                condation = " and (deliverer_id = " + related_id + " or reciever_id= " + related_id + " )"
            End If
            If LoginInfo.getUserType = "6" Then
                condation = " and advisor = " + related_id
            End If
            Dim query As String = "SELECT details.date_m as 'selected_d', details.id as id,ash_cases.code as cases,ash_case_persons.indenty as num,ash_case_persons.name as person,details.type as type FROM  ash_case_receiving_delivery_details details join ash_cases on details.case_id= ash_cases.id  join ash_case_persons on ash_cases.person1_id=ash_case_persons.id  where details.deleted !=1 and details.date_m = '" + _date + "' And details.case_id In (Select id from ash_cases where comp_id=" + LoginInfo.GetComp_id() + ") " + condation
            dt = DBManager.Getdatatable(query)
            If dt.Rows.Count <> 0 Then
                Names(0) = PublicFunctions.ConvertDataTabletoString(dt)
            End If
            If LoginInfo.getUserType = "9" Then
                Return Names.ToArray
            End If
            Dim query1 As String = "SELECT ash_case_sessions.id as id,ash_cases.code as cases,ash_case_persons.indenty as num,ash_case_persons.name as person FROM  ash_case_sessions join ash_cases on ash_case_sessions.case_id= ash_cases.id  join ash_case_persons on ash_cases.person1_id=ash_case_persons.id  where ash_case_sessions.date_m = '" + _date + "' And ash_case_sessions.case_id In (Select id from ash_cases where comp_id=" + LoginInfo.GetComp_id() + ")"
            Dim query2 As String = "SELECT ash_case_correspondences.id as id,ash_cases.code as cases,ash_case_persons.indenty as num,ash_case_persons.name as person  FROM  ash_case_correspondences join ash_cases on ash_case_correspondences.case_id= ash_cases.id  join ash_case_persons on ash_cases.person1_id=ash_case_persons.id  where ash_case_correspondences.date_m = '" + _date + "' And ash_case_correspondences.case_id In (Select id from ash_cases where comp_id=" + LoginInfo.GetComp_id() + ")"

            dt1 = DBManager.Getdatatable(query1)
            dt2 = DBManager.Getdatatable(query2)


            If dt1.Rows.Count <> 0 Then
                Names(1) = PublicFunctions.ConvertDataTabletoString(dt1)
            End If

            If dt2.Rows.Count <> 0 Then
                Names(2) = PublicFunctions.ConvertDataTabletoString(dt2)

            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region


#Region "get_option_cases"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                    <WebMethod(True)>
                                                                                                                                                                                                                                                                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function get_option_cases() As String

        Try
            Dim dt As New DataTable
            Dim condation As String = ""
            If LoginInfo.getUserType = "6" Then
                condation = " and ash_cases.advisor_id =" + LoginInfo.getrelatedId()
            End If
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            Dim query As String = "Select ash_cases.id As case_id,ash_cases.code As cases,ash_case_persons.indenty As num,ash_case_persons.name As person from ash_cases join ash_case_persons On ash_cases.person1_id=ash_case_persons.id where ash_cases.comp_id=" + LoginInfo.GetComp_id() + condation

            dt = DBManager.Getdatatable(query)
            If dt.Rows.Count <> 0 Then
                Return PublicFunctions.ConvertDataTabletoString(dt)
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region

#Region "show_all"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    <System.Web.Script.Services.ScriptMethod()>
    Public Function show_all(ByVal printItemId As String, ByVal type As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_cases As New DataTable
            Dim dt_person_owner As New DataTable
            Dim dt_person_against As New DataTable
            Dim dt_children As New DataTable
            Dim dt_receiving_delivery_basic As New DataTable
            Dim dt_receiving_delivery_details As New DataTable
            Dim dt_conciliation As New DataTable
            Dim dt_correspondences As New DataTable
            Dim dt_sessions As New DataTable
            Dim dt_expenses As New DataTable
            Dim dt_expenses_details As New DataTable

            dt_cases = DBManager.Getdatatable("Select * from ash_cases  where  ash_cases.id = " + printItemId)
            dt_person_owner = DBManager.Getdatatable("Select ash_case_persons.id As id, ash_case_persons.name As name,ash_case_persons.indenty As indenty,ash_case_persons.relationship_id As relationship_id,ash_case_persons.authorization_no As authorization_no,ash_case_persons.phone As phone from ash_cases join ash_case_persons On ash_cases.person1_id=ash_case_persons.id where ash_cases.id=" + printItemId)
            dt_person_against = DBManager.Getdatatable("Select ash_case_persons.id As id, ash_case_persons.name As name,ash_case_persons.indenty As indenty,ash_case_persons.relationship_id As relationship_id,ash_case_persons.authorization_no As authorization_no,ash_case_persons.phone As phone from ash_cases join ash_case_persons On ash_cases.person2_id=ash_case_persons.id where ash_cases.id=" + printItemId)
            dt_children = DBManager.Getdatatable("Select * from ash_case_childrens where  ash_case_childrens.case_id = " + printItemId)
            dt_receiving_delivery_basic = DBManager.Getdatatable("Select * from ash_case_receiving_delivery_basic where basic_period = 1 and ash_case_receiving_delivery_basic.case_id = " + printItemId)
            If type = 1 Then
                dt_receiving_delivery_details = DBManager.Getdatatable("Select * from ash_case_receiving_delivery_details where deleted !=1 and  id = " + printItemId)
            End If
            dt_conciliation = DBManager.Getdatatable("Select * from ash_case_conciliation where  case_id = " + printItemId)
            If type = 4 Then
                dt_correspondences = DBManager.Getdatatable("Select  * from ash_case_correspondences where  id = " + printItemId)
            End If
            If type = 3 Then
                dt_sessions = DBManager.Getdatatable("Select  * from ash_case_sessions where  id = " + printItemId)
            End If
            dt_expenses = DBManager.Getdatatable("Select  * from ash_case_expense_basic where  case_id = " + printItemId)
            dt_expenses_details = DBManager.Getdatatable("Select  * from ash_case_expenses_details where  case_id = " + printItemId)
            '           dt_finance = DBManager.Getdatatable("Select tblproject_finance.id As 'finance_id', * from tblproject_finance left join  tbllock_up on tblproject_finance.payment_id=tbllock_up.id  where  project_id = " + printItemId)
            '           dt_prposal = DBManager.Getdatatable("SELECT tblproject_proposals.id as 'propsal_id', * from tblproject_proposals join  tbllock_up on tblproject_proposals.send_type=tbllock_up.id  where type='ST' and  project_id = " + printItemId)
            '           dt_info = DBManager.Getdatatable("SELECT isNull(servicePrice,0) servicePrice,tblproject_info.id,project_id,description as info_name" +
            '    " ,info_details,alarm,alarm_date,alarm_date_hj,user_id ,entry_date ,entry_date_h,image" +
            '" FROM tblproject_info  left join tbllock_up on info_name=tbllock_up.id where  project_id = " + printItemId)
            '           dt_workers = DBManager.Getdatatable("SELECT tblworker.name_ar as worker,tblworker.id as id,tblproject_workers.deal_date_hj as deal_date_hj,tblproject_workers.start_date_hj as start_date_hj,tblproject_workers.end_date_hj as end_date_hj,tblproject_workers.total_budget as total_budget from tblproject_workers join tblprojects on tblprojects.id=tblproject_workers.project_id join tblworker on tblworker.id=tblproject_workers.worker_id  where tblprojects.id = " + printItemId)
            '           dt_archive = DBManager.Getdatatable("SELECT * from tblProject_archive  where Project_id = " + printItemId)

            If dt_cases.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt_cases)
                Names.Add(str)
            Else
                Names.Add("0")
            End If
            If dt_person_owner.Rows.Count <> 0 Then
                Dim str2 As String = PublicFunctions.ConvertDataTabletoString(dt_person_owner)
                Names.Add(str2)
            Else
                Names.Add("0")
            End If
            If dt_person_against.Rows.Count <> 0 Then
                Dim str3 As String = PublicFunctions.ConvertDataTabletoString(dt_person_against)
                Names.Add(str3)
            Else
                Names.Add("0")
            End If

            If dt_children.Rows.Count <> 0 Then
                Dim str4 As String = PublicFunctions.ConvertDataTabletoString(dt_children)
                Names.Add(str4)
            Else
                Names.Add("0")
            End If

            If dt_receiving_delivery_basic.Rows.Count <> 0 Then
                Dim str5 As String = PublicFunctions.ConvertDataTabletoString(dt_receiving_delivery_basic)
                Names.Add(str5)
            Else
                Names.Add("0")
            End If

            If dt_receiving_delivery_details.Rows.Count <> 0 Then
                Dim str6 As String = PublicFunctions.ConvertDataTabletoString(dt_receiving_delivery_details)
                Names.Add(str6)
            Else
                Names.Add("0")
            End If
            If dt_conciliation.Rows.Count <> 0 Then
                Dim str7 As String = PublicFunctions.ConvertDataTabletoString(dt_conciliation)
                Names.Add(str7)
            Else
                Names.Add("0")
            End If

            If dt_correspondences.Rows.Count <> 0 Then
                Dim str8 As String = PublicFunctions.ConvertDataTabletoString(dt_correspondences)
                Names.Add(str8)
            Else
                Names.Add("0")
            End If

            If dt_sessions.Rows.Count <> 0 Then
                Dim str9 As String = PublicFunctions.ConvertDataTabletoString(dt_sessions)
                Names.Add(str9)
            Else
                Names.Add("0")
            End If
            If dt_expenses.Rows.Count <> 0 Then
                Dim str10 As String = PublicFunctions.ConvertDataTabletoString(dt_expenses)
                Names.Add(str10)
            Else
                Names.Add("0")
            End If
            If dt_expenses_details.Rows.Count <> 0 Then
                Dim str11 As String = PublicFunctions.ConvertDataTabletoString(dt_expenses_details)
                Names.Add(str11)
            Else
                Names.Add("0")
            End If




            'If dt_finance.Rows.Count <> 0 Then
            '    Dim str1 As String = PublicFunctions.ConvertDataTabletoString(dt_finance)
            '    Names.Add(str1)
            'Else
            '    Names.Add("0")
            'End If
            'If dt_prposal.Rows.Count <> 0 Then
            '    Dim str2 As String = PublicFunctions.ConvertDataTabletoString(dt_prposal)
            '    Names.Add(str2)
            'Else
            '    Names.Add("0")
            'End If
            'If dt_info.Rows.Count <> 0 Then
            '    Dim str3 As String = PublicFunctions.ConvertDataTabletoString(dt_info)
            '    Names.Add(str3)
            'Else
            '    Names.Add("0")
            'End If
            'If dt_workers.Rows.Count <> 0 Then
            '    Dim str4 As String = PublicFunctions.ConvertDataTabletoString(dt_workers)
            '    Names.Add(str4)
            'Else
            '    Names.Add("0")
            'End If

            'If dt_archive.Rows.Count <> 0 Then
            '    Dim str5 As String = PublicFunctions.ConvertDataTabletoString(dt_archive)
            '    Names.Add(str5)
            'Else
            '    Names.Add("0")
            'End If
            'Dim index = InStr(str1, ",")
            'Dim substr = str1.Substring(index - 1, str1.Length - index - 1)
            'Dim Result = str.Insert(str.Length - 2, substr)


            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region
#Region "show_cases_details"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <System.Web.Script.Services.ScriptMethod()>
    Public Function show_cases_details(ByVal printItemId As String, ByVal type As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable
            Dim dt_details_childrens As New DataTable
            Dim dt_details_persons As New DataTable
            If type = 1 Then
                dt_details = DBManager.Getdatatable("select * from ash_case_childrens  where id=" + printItemId.ToString)
            ElseIf type = 2 Then
                dt_details = DBManager.Getdatatable("SELECT  * from ash_case_receiving_delivery_details where deleted!=1 and id= " + printItemId.ToString)
                dt_details_childrens = DBManager.Getdatatable("SELECT  * from ash_case_children_receiving_details where details_id= " + printItemId.ToString)
            ElseIf type = 3 Then
                dt_details = DBManager.Getdatatable("SELECT * from ash_case_correspondences  where   id=" + printItemId.ToString)
            ElseIf type = 4 Then
                dt_details = DBManager.Getdatatable("SELECT  * from ash_case_sessions where id= " + printItemId.ToString)
                dt_details_childrens = DBManager.Getdatatable("SELECT  * from ash_session_children where session_id= " + printItemId.ToString)
                dt_details_persons = DBManager.Getdatatable("SELECT  * from ash_session_companions where session_id= " + printItemId.ToString)
            ElseIf type = 5 Then
                dt_details = DBManager.Getdatatable("SELECT  * from ash_case_expenses_details where id= " + printItemId.ToString)
            End If
            If dt_details.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt_details)

                Names.Add(str)
            Else
                Names.Add("0")
            End If

            If dt_details_childrens.Rows.Count <> 0 Then
                Dim str1 As String = PublicFunctions.ConvertDataTabletoString(dt_details_childrens)

                Names.Add(str1)
            Else
                Names.Add("0")
            End If
            If dt_details_persons.Rows.Count <> 0 Then
                Dim str2 As String = PublicFunctions.ConvertDataTabletoString(dt_details_persons)

                Names.Add(str2)
            Else
                Names.Add("0")
            End If
            'Dim index = InStr(str1, ",")
            'Dim substr = str1.Substring(index - 1, str1.Length - index - 1)
            'Dim Result = str.Insert(str.Length - 2, substr)


            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "delete_children"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <System.Web.Script.Services.ScriptMethod()>
    Public Function delete_children(ByVal printItemId As String, ByVal case_id As String) As Boolean

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable


            If DBManager.ExcuteQueryTransaction("delete from ash_case_childrens  where id=" + printItemId.ToString + " and  case_id=" + case_id, _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If

            Return True
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function

#End Region

#Region "get_date_delivery"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function get_date_delivery(ByVal case_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable
            Dim dt_finance As New DataTable
            Dim dt_prposal As New DataTable
            dt_details = DBManager.Getdatatable("select TOP 1 [date_m] as date from ash_case_receiving_delivery_details where deleted !=1 and receiving_delivery_done=0 and case_id=" + case_id.ToString + " ORDER BY ID DESC")
            If dt_details.Rows.Count <> 0 Then
                Dim str = PublicFunctions.ConvertDataTabletoString(dt_details)
                Names.Add(str)
            Else
                Names.Add("0")
            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "get_date_expenses"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_date_expenses(ByVal case_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable
            Dim dt_finance As New DataTable
            Dim dt_prposal As New DataTable
            dt_details = DBManager.Getdatatable("select TOP 1 [date_m] as date from ash_case_expenses_details where done=0 and case_id=" + case_id.ToString + " ORDER BY ID DESC")
            If dt_details.Rows.Count <> 0 Then
                Dim str = PublicFunctions.ConvertDataTabletoString(dt_details)
                Names.Add(str)
            Else
                Names.Add("0")
            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "get_persons"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <System.Web.Script.Services.ScriptMethod()>
    Public Function get_persons(ByVal id As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable
            dt_details = DBManager.Getdatatable("select ash_case_persons.id as person_id,ash_case_persons.name as person_name from ash_case_persons join ash_cases on ash_case_persons.case_id=ash_cases.id where ash_cases.id=" + id)
            If dt_details.Rows.Count <> 0 Then
                Dim str = PublicFunctions.ConvertDataTabletoString(dt_details)
                Names.Add(str)
            Else
                Names.Add("0")
            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "get_date_children"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <System.Web.Script.Services.ScriptMethod()>
    Public Function get_date_children(ByVal case_id As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            Dim dt_children As New DataTable
            dt_children = DBManager.Getdatatable("SELECT * from ash_case_childrens where  ash_case_childrens.case_id = " + case_id)
            If dt_children.Rows.Count <> 0 Then
                Dim str = PublicFunctions.ConvertDataTabletoString(dt_children)
                Names.Add(str)
            Else
                Names.Add("0")
            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "getPreviousBenef_Action"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function getPreviousBenef_Action(ByVal id As String) As String()
        Dim Names As New List(Of String)(10)
        Names.Add("")
        Names.Add("")
        Dim related_id As String = LoginInfo.getrelatedId()
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select * from ash_case_receiving_delivery_details where deleted!=1 and id=" + id)

            If dt.Rows.Count <> 0 Then
                'Dim curr_dt As Integer = PublicFunctions.ConvertDatetoNumber(DateTime.Now.ToString("dd/MM/yyyy"))
                'Dim select_dt As Integer = Convert.ToInt32(dt.Rows(0).Item("date_m"))

                If dt.Rows(0).Item("deliverer_id").ToString = related_id Then
                    If Convert.ToBoolean(dt.Rows(0).Item("deliverer_accept")) Then
                        Names(0) = "1"
                        Return Names.ToArray
                    End If
                ElseIf dt.Rows(0).Item("reciever_id").ToString = related_id Then
                    If Convert.ToBoolean(dt.Rows(0).Item("reciever_accept")) Then
                        Names(0) = "1"
                        Return Names.ToArray
                    End If
                End If
            End If
            dt = DBManager.Getdatatable("select * from ash_orders where event_id=" + id + "and owner_id=" + LoginInfo.GetUser__Id())
            If dt.Rows.Count <> 0 Then
                Names(0) = "2"
                Names(1) = PublicFunctions.ConvertDataTabletoString(dt)
            End If

            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "saveBenef_Action"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function saveBenef_Action(ByVal basicDataJson As Object) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim id As String = dictBasicDataJson("id").ToString()
            Dim event_id As String = dictBasicDataJson("event_id").ToString()

            Dim related_id As String = LoginInfo.getrelatedId()
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select deliverer_id,reciever_id,case_id ,isNull(p1_id.id,0) as p1_id,isNull(p2_id.id,0) as p2_id" +
" , isNull(advisor.id, 0) as advisor_id,advisor.User_PhoneNumber as 'advisor_phone' from ash_case_receiving_delivery_details details" +
" left join ash_cases on ash_cases.id=details.case_id" +
" Left join tblUsers p1_id on p1_id.related_id=details.deliverer_id And p1_id.User_Type=9" +
" left join tblUsers p2_id on p2_id.related_id=details.reciever_id and p2_id.User_Type=9 " +
" left join tblUsers advisor on advisor.related_id=ash_cases.advisor_id and advisor.User_Type=6 " +
" where details.deleted !=1 and details.id=" + event_id)
            If dt.Rows.Count <> 0 Then
                Dim OtherPerson = ""
                Dim column_nm As String = ""
                If dt.Rows(0).Item("deliverer_id").ToString = related_id Then
                    column_nm = "deliverer_accept"
                    OtherPerson = dt.Rows(0).Item("reciever_id").ToString
                ElseIf dt.Rows(0).Item("reciever_id").ToString = related_id Then
                    column_nm = "reciever_accept"
                    OtherPerson = dt.Rows(0).Item("deliverer_id").ToString
                End If
                If column_nm = "" Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
                Dim dic As New Dictionary(Of String, Object)
                If dictBasicDataJson("type") = 3 Then
                    dic.Add(column_nm, 1)
                    If Not PublicFunctions.TransUpdateInsert(dic, "ash_case_receiving_delivery_details", event_id, _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return False
                    End If
                Else
                    Dim Message = "يرجى "
                    Dim type As String = dictBasicDataJson("type")
                    If type = 1 Then
                        Message = Message + "تاجيل المعاد"
                    ElseIf type = 2 Then
                        Message = Message + "إلغاء المعاد"
                    End If
                    dictBasicDataJson.Add("case_id", dt.Rows(0).Item("case_id").ToString)
                    dictBasicDataJson.Add("comp_id", LoginInfo.GetComp_id())
                    dictBasicDataJson.Add("owner_id", LoginInfo.GetUser__Id())
                    dictBasicDataJson.Add("otherP_Accept", "")
                    If Not PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_orders", "", _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return False
                    End If
                    Dim order_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                    Dim dictNotification As New Dictionary(Of String, Object)
                    dictNotification.Add("RefCode", order_id)
                    dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                    dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictNotification.Add("Remarks", Message)
                    dictNotification.Add("FormUrl", "Aslah_Module/orders.aspx?id=" + order_id)
                    dictNotification.Add("NotTitle", "طلب لتاجيل/إلغاء معاد")
                    dictNotification.Add("AssignedTo", dt.Rows(0).Item("advisor_id"))
                    If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return "False|لم يتم الحفظ"
                    End If

                    If LoginInfo.SendSMS() Then
                        Dim dic_sms_archive As New Dictionary(Of String, Object)

                        dic_sms_archive.Add("user_id", LoginInfo.GetUser__Id())
                        dic_sms_archive.Add("Message", "")
                        dic_sms_archive.Add("Send_To", "")
                        dic_sms_archive.Add("date_m", DateTime.Now.ToString("dd/MM/yyyy"))
                        dic_sms_archive.Add("event_id", order_id)
                        dic_sms_archive.Add("Type", "orders")
                        dic_sms_archive.Add("comp_id", LoginInfo.GetComp_id())
                        dic_sms_archive("Message") = "طلب لتاجيل/إلغاء معاد"
                        dic_sms_archive("Send_To") = dt.Rows(0).Item("advisor_phone")
                        If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Return "False|لم يتم الحفظ"
                        End If

                    End If
                End If
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If
            _sqltrans.Commit()
            _sqlconn.Close()
            Return True
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function

#End Region

#Region "get_case_expense_basic"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <WebMethod(True)>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_case_expense_basic(ByVal case_id As String) As String

        Try
            Dim dt As New DataTable

            Dim query As String = "SELECT isNull(amount,'') FROM ash_case_expense_basic where case_id=" + case_id

            dt = DBManager.Getdatatable(query)
            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)(0).ToString()
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region

#Region "getLast_recieve"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <System.Web.Script.Services.ScriptMethod()>
    Public Function getLast_recieve(ByVal case_id As String) As String

        Try
            Dim dt_last As New DataTable
            dt_last = DBManager.Getdatatable("SELECT date_m from ash_case_receiving_delivery_details where deleted !=1 and type=1 and case_id = " + case_id + " order by date_m")
            Dim count As Integer = dt_last.Rows.Count
            If count <> 0 Then
                Dim res = PublicFunctions.ConvertNumbertoDate(dt_last.Rows(count - 1)(0).ToString)
                res = res + "#" + GET_delivery_Reciever_Data(case_id)
                Return res
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region


#Region "delete_Time"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <System.Web.Script.Services.ScriptMethod()>
    Public Function delete_Time(ByVal id As String, ByVal tabel_nm As String) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            If tabel_nm = "ash_case_receiving_delivery_details" Then
                If DBManager.ExcuteQueryTransaction("delete from tblsms_archive where Type='recieve_delivery' and event_id = " + id, _sqlconn, _sqltrans) Then
                    If DBManager.ExcuteQueryTransaction("delete from tblNotifications where RefType in(1,2,3,4) and RefCode=" + id, _sqlconn, _sqltrans) Then
                        Dim dict As New Dictionary(Of String, Object)
                        dict.Add("deleted", 1)
                        If PublicFunctions.TransUpdateInsert(dict, "ash_case_receiving_delivery_details", id, _sqlconn, _sqltrans) Then
                            _sqltrans.Commit()
                            _sqlconn.Close()
                            Return True
                        End If
                    End If
                End If
            Else
                If PublicFunctions.DeleteFromTable(id, tabel_nm) Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return True
                End If
            End If
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function

#End Region

#Region "delete_Period"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function delete_Period(ByVal id As String, ByVal access As Boolean) As String

        Try

            Dim dt As New DataTable
            Dim condation As String = ""

            dt = DBManager.Getdatatable("select case_id,first_date_m as start_period,endPeriod_date_m as end_period from ash_case_receiving_delivery_basic where id=" + id)

            If dt.Rows.Count <> 0 Then
                Dim case_id = dt.Rows(0).Item("case_id").ToString()
                condation = " type = 1 and isNULL(deleted,0) != 1 and case_id=" + case_id + "  and date_m between " + dt.Rows(0).Item("start_period").ToString() + " and " + dt.Rows(0).Item("end_period").ToString()

                If DBManager.Getdatatable("select id from ash_case_receiving_delivery_details where " + condation + " and id  in (select event_id from ash_orders where case_id= " + case_id + ")").Rows.Count <> 0 Then
                    Return "False|لا يمكن حذف الفترة لوجود ارتباطات"
                End If
                If DBManager.Getdatatable("select id from ash_case_receiving_delivery_details where " + condation + " and (isNULL(deliverer_accept,0) =1 or isNull(reciever_accept,0) =1 )").Rows.Count <> 0 Then
                    Return "False|لا يمكن حذف الفترة لوجود ارتباطات"
                End If
                If access Then
                    _sqlconn.Open()
                    _sqltrans = _sqlconn.BeginTransaction()

                End If
                If DBManager.ExcuteQueryTransaction("delete tblNotifications  where RefType in (1,2,3,4) and RefCode in (select id from ash_case_receiving_delivery_details where " + condation + ")", _sqlconn, _sqltrans) Then
                    If DBManager.ExcuteQueryTransaction("delete tblsms_archive  where Type = 'recieve_delivery' and event_id in (select id from ash_case_receiving_delivery_details where " + condation + ")", _sqlconn, _sqltrans) Then
                        If DBManager.ExcuteQueryTransaction("update ash_case_receiving_delivery_details set deleted = 1 where id in (select id from ash_case_receiving_delivery_details where " + condation + ")", _sqlconn, _sqltrans) Then
                            If DBManager.ExcuteQueryTransaction("delete  from ash_case_receiving_delivery_basic where id=" + id) Then
                                If access Then
                                    _sqltrans.Commit()
                                    _sqlconn.Close()
                                End If
                                Return "True"

                            End If
                        End If
                    End If
                End If
            End If
            If access Then
                _sqltrans.Rollback()
                _sqlconn.Close()
            End If
            Return "False|لم يتم الحذف"
        Catch ex As Exception
            If access Then
                _sqltrans.Rollback()
                _sqlconn.Close()
            End If
            Return "False|لم يتم الحذف"
        End Try
    End Function

#End Region

#Region "get_other_periods"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <System.Web.Script.Services.ScriptMethod()>
    Public Function get_other_periods(ByVal case_id As String) As String
        Try
            Dim dt_period As New DataTable
            dt_period = DBManager.Getdatatable("SELECT * from ash_case_receiving_delivery_basic where isNUll(basic_period,0) !=1 And case_id = " + case_id)
            If dt_period.Rows.Count <> 0 Then
                Return PublicFunctions.ConvertDataTabletoString(dt_period)
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region

End Class