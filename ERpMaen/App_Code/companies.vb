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
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<System.Web.Script.Services.ScriptService()>
Public Class companies
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region
#Region "check_userData"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function check_userData(ByVal id As String, ByVal User_Name As String, ByVal User_Email As String, ByVal User_PhoneNumber As String) As Boolean

        Try
            Dim dt As New DataTable
            If id = "" Then
                dt = DBManager.Getdatatable("Select * from TblUsers where User_Name='" + User_Name + "' OR  User_Email='" + User_Email + "' or User_PhoneNumber='" + User_PhoneNumber + "'")
            Else
                dt = DBManager.Getdatatable("Select * from TblUsers where ( User_Name='" + User_Name + "' OR User_Email='" + User_Email + "' or User_PhoneNumber='" + User_PhoneNumber + "')  and  Id!='" + id + "'")
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
#Region "save_companies"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_companies(ByVal formId As String, ByVal arrDataJson As Object(), ByVal date_m As String, ByVal data_hj As String) As String()
        Dim login_user = LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString()
        Dim Names As New List(Of String)(10)
        Dim success As Boolean = False

        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction

            Dim dictCompAdminDataJson As Dictionary(Of String, Object) = arrDataJson(1)
            Dim CompAdmin_id = dictCompAdminDataJson("id")
            Dim CompAdmin_User_n = dictCompAdminDataJson("User_Name")
            Dim CompAdmin_email = dictCompAdminDataJson("User_Email")
            Dim CompAdmin_Phone = dictCompAdminDataJson("User_PhoneNumber")
            If check_userData(CompAdmin_id, CompAdmin_User_n, CompAdmin_email, CompAdmin_Phone) Then
                '///////////////////////////////////////////////////////////////////////////////////////////////////////////
                ' if super admin
                If login_user = "1" Then

                    '//////////////////////////////////////////////////////////////////////////////////////
                    ' save comp basic data
                    Dim addboard As Boolean


                    Dim contractdictDataJson As Dictionary(Of String, Object) = arrDataJson(3)

                    Dim dictCompDataJson As Dictionary(Of String, Object) = arrDataJson(0)
                    dictCompDataJson.Add("person", dictCompAdminDataJson("full_name"))
                    dictCompDataJson.Add("email", dictCompAdminDataJson("User_Email"))
                    dictCompDataJson.Add("deal_start_date_m", PublicFunctions.ConvertDatetoNumber(contractdictDataJson("deal_start_date_m")))
                    dictCompDataJson.Add("deal_end_date_m", PublicFunctions.ConvertDatetoNumber(contractdictDataJson("deal_end_date_m")))
                    dictCompDataJson.Add("maintainance_start_date_m", PublicFunctions.ConvertDatetoNumber(contractdictDataJson("maintainance_start_date_m")))
                    dictCompDataJson.Add("maintainance_end_date_m", PublicFunctions.ConvertDatetoNumber(contractdictDataJson("maintainance_end_date_m")))



                    Dim Comp_id = dictCompDataJson("id")
                    If PublicFunctions.TransUpdateInsert(dictCompDataJson, "tblcompanies", Comp_id, _sqlconn, _sqltrans) Then
                        addboard = True
                        If Comp_id = "" Then
                            Comp_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                        Else
                            addboard = False
                        End If

                        success = True

                    Else
                        success = False
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Names.Add("لم يتم الحفظ")
                    End If
                    '////////////////////////////////////////////////////////////////////////////////////////
                    ' save comp board
                    If addboard Then
                        Dim dict = New Dictionary(Of String, Object)
                        dict.Add("name", "جمعية عمومية")
                        dict.Add("members_number", "200")
                        dict.Add("comp_id", Comp_id)
                        dict.Add("active", 1)
                        dict.Add("type", 1)
                        If Not PublicFunctions.TransUpdateInsert(dict, "tblboards", "", _sqlconn, _sqltrans) Then
                            success = False
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Names.Add("لم يتم الحفظ")
                        End If
                    End If
                    '///////////////////////////////////////////////////////////////////////////////////////////
                    'save comp modules

                    ' delete old modules if exist
                    If DBManager.ExcuteQueryTransaction("delete  from tblcomp_modules where comp_id=" + Comp_id + " ", _sqlconn, _sqltrans) = -1 Then
                        success = False
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Names.Add("لم يتم الحفظ")
                    End If

                    Dim comp_group_permission_id = 0
                    If formId = "" Then
                        '//////////////////////////////////////////////////
                        'insert new group for comp
                        Dim comp_permission_name As String = "صلاحيات" + "  " + dictCompDataJson("name_ar")

                        If DBManager.ExcuteQueryTransaction("insert into tbllock_up (Description,Type,Comp_id) values('" + comp_permission_name.ToString + "','PG'," + Comp_id + ")", _sqlconn, _sqltrans) = -1 Then
                            success = False
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Names.Add("لم يتم الحفظ")
                        Else
                            comp_group_permission_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans).ToString
                        End If
                    Else
                        DBManager.ExcuteQueryTransaction("delete  from tblgroup_permissons where group_id=" + comp_group_permission_id.ToString, _sqlconn, _sqltrans)

                    End If

                    ' insert  comp modules 

                    For Each ModulesJSON As Dictionary(Of String, Object) In arrDataJson(2)
                        Dim dictBasicDataJson As Dictionary(Of String, Object) = ModulesJSON
                        Dim id = 0
                        dictBasicDataJson.Add("comp_id", Comp_id.ToString)
                        '     dictBasicDataJson.Add("user_id", Comp_id.ToString)
                        If Not PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblcomp_modules", id, _sqlconn, _sqltrans) Then
                            success = False
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Names.Add("لم يتم الحفظ")
                        End If

                        'add module forms permissions
                        Dim dt2 As DataTable = DBManager.Getdatatable("Select * from tblForms where type=1 And MenueId=" + dictBasicDataJson("module_id").ToString)
                        For Each row2 As DataRow In dt2.Rows
                            Dim dict = New Dictionary(Of String, Object)
                            dict.Add("group_id", comp_group_permission_id.ToString)
                            dict.Add("form_id", row2.Item("Id").ToString)
                            dict.Add("f_add", 1)
                            dict.Add("f_update", 1)
                            dict.Add("f_access", 1)
                            dict.Add("f_delete", 1)

                            PublicFunctions.TransUpdateInsert(dict, "tblgroup_permissons", id, _sqlconn, _sqltrans)
                        Next


                    Next

                    '///////////////////////////////////////////////////////////////////////////////////////////
                    ' save comp admin
                    dictCompAdminDataJson.Add("User_Type", 2)
                    dictCompAdminDataJson.Add("group_id", comp_group_permission_id)
                    dictCompAdminDataJson.Add("Active", dictCompDataJson("active"))
                    dictCompAdminDataJson.Add("comp_id", Comp_id)

                    If PublicFunctions.TransUpdateInsert(dictCompAdminDataJson, "tblUsers", CompAdmin_id, _sqlconn, _sqltrans) Then
                        success = True

                    Else
                        success = False
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Names.Add("لم يتم الحفظ")
                    End If

                Else

                    Dim dictAcaAdminDataJson As Dictionary(Of String, Object) = arrDataJson(3)
                    Dim AcAdmin_id = dictAcaAdminDataJson("id")
                    Dim AcAdmin_User_n = dictAcaAdminDataJson("User_Name")
                    Dim AcAdmin_email = dictAcaAdminDataJson("User_Email")
                    Dim AcAdmin_Phone = dictAcaAdminDataJson("User_PhoneNumber")
                    If check_userData(AcAdmin_id, AcAdmin_User_n, AcAdmin_email, AcAdmin_Phone) And AcAdmin_User_n <> CompAdmin_User_n And AcAdmin_email <> CompAdmin_email And AcAdmin_Phone <> CompAdmin_Phone Then
                        Dim dictCenAdminDataJson As Dictionary(Of String, Object) = arrDataJson(5)
                        Dim CenAdmin_id = dictCenAdminDataJson("id")
                        Dim CenAdmin_User_n = dictCenAdminDataJson("User_Name")
                        Dim CenAdmin_email = dictCenAdminDataJson("User_Email")
                        Dim CenAdmin_Phone = dictCenAdminDataJson("User_PhoneNumber")
                        If check_userData(CenAdmin_id, CenAdmin_User_n, CenAdmin_email, CenAdmin_Phone) And CenAdmin_User_n <> AcAdmin_User_n And CenAdmin_User_n <> CompAdmin_User_n And CenAdmin_email <> AcAdmin_email And CenAdmin_email <> CompAdmin_email And CenAdmin_Phone <> AcAdmin_Phone And CenAdmin_Phone <> CompAdmin_Phone Then
                            Dim dictCompDataJson As Dictionary(Of String, Object) = arrDataJson(0)
                            dictCompDataJson.Add("person", dictCompAdminDataJson("full_name"))
                            dictCompDataJson.Add("email", dictCompAdminDataJson("User_Email"))
                            Dim Comp_id = dictCompDataJson("id")
                            If PublicFunctions.TransUpdateInsert(dictCompDataJson, "tblcompanies", Comp_id, _sqlconn, _sqltrans) Then
                                Dim addboard As Boolean = True
                                If Comp_id = "" Then
                                    Comp_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                                Else
                                    addboard = False
                                End If
                                dictCompAdminDataJson.Add("User_Type", 2)
                                dictCompAdminDataJson.Add("group_id", 120)
                                dictCompAdminDataJson.Add("Active", dictCompDataJson("active"))
                                dictCompAdminDataJson.Add("comp_id", Comp_id)

                                If PublicFunctions.TransUpdateInsert(dictCompAdminDataJson, "tblUsers", CompAdmin_id, _sqlconn, _sqltrans) Then

                                    dictAcaAdminDataJson.Add("User_Type", 7)
                                    dictAcaAdminDataJson.Add("group_id", 122)
                                    dictAcaAdminDataJson.Add("comp_id", Comp_id)
                                    If PublicFunctions.TransUpdateInsert(dictAcaAdminDataJson, "tblUsers", AcAdmin_id, _sqlconn, _sqltrans) Then
                                        If AcAdmin_id = "" Then
                                            AcAdmin_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                                        End If
                                        Dim dictAcaDataJson As Dictionary(Of String, Object) = arrDataJson(2)
                                        Dim Ac_id = dictAcaDataJson("id")
                                        dictAcaDataJson.Add("date_m", date_m)
                                        dictAcaDataJson.Add("date_hj", data_hj)
                                        dictAcaDataJson.Add("admin", AcAdmin_id)
                                        dictAcaDataJson.Add("comp_id", Comp_id)
                                        dictAcaDataJson.Add("add_by", login_user)
                                        If PublicFunctions.TransUpdateInsert(dictAcaDataJson, "acd_acadmies", Ac_id, _sqlconn, _sqltrans) Then
                                            dictCenAdminDataJson.Add("User_Type", 7)
                                            dictCenAdminDataJson.Add("group_id", 121)
                                            dictCenAdminDataJson.Add("comp_id", Comp_id)
                                            If PublicFunctions.TransUpdateInsert(dictCenAdminDataJson, "tblUsers", CenAdmin_id, _sqlconn, _sqltrans) Then
                                                If CenAdmin_id = "" Then
                                                    CenAdmin_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                                                End If
                                                Dim dictCenDataJson As Dictionary(Of String, Object) = arrDataJson(4)
                                                Dim Cen_id = dictCenDataJson("id")

                                                dictCenDataJson.Add("date_m", date_m)
                                                dictCenDataJson.Add("date_hj", data_hj)
                                                dictCenDataJson.Add("admin", CenAdmin_id)
                                                dictCenDataJson.Add("comp_id", Comp_id)
                                                dictCenDataJson.Add("add_by", login_user)
                                                If PublicFunctions.TransUpdateInsert(dictCenDataJson, "acd_training_centers", Cen_id, _sqlconn, _sqltrans) Then
                                                    If addboard Then
                                                        Dim dict = New Dictionary(Of String, Object)
                                                        dict.Add("name", "جمعية عمومية")
                                                        dict.Add("members_number", "200")
                                                        dict.Add("comp_id", Comp_id)
                                                        dict.Add("active", 1)
                                                        dict.Add("type", 1)
                                                        If PublicFunctions.TransUpdateInsert(dict, "tblboards", "", _sqlconn, _sqltrans) Then
                                                            success = True
                                                        End If
                                                    Else
                                                        success = True
                                                    End If

                                                End If

                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            Names.Add("اسم المستخدم او البريد الالكترونى او التلفون لمدير مركز التدريب مستخدم")
                        End If
                    Else
                        Names.Add("اسم المستخدم او البريد الالكترونى او التلفون لمدير الاكاديمية مستخدم")
                    End If
                End If
            Else
                Names.Add("اسم المستخدم او البريد الالكترونى او التلفون لمدير الجهة مستخدم")
            End If
            If success Then
                _sqltrans.Commit()
                _sqlconn.Close()
                Names.Add("1")
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Names.Add("لم يتم الحفظ")
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Names.Add("لم يتم الحفظ")
        End Try
        Return Names.ToArray
    End Function


#End Region

#Region "save_contract"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_contract(ByVal id As String, ByVal basicDataJson As Object, ByVal attch_file_DataJsonList As List(Of Object)) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction

            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson("deal_start_date_m") = PublicFunctions.ConvertDatetoNumber(dictBasicDataJson("deal_start_date_m"))
            dictBasicDataJson("deal_end_date_m") = PublicFunctions.ConvertDatetoNumber(dictBasicDataJson("deal_end_date_m"))
            dictBasicDataJson("maintainance_start_date_m") = PublicFunctions.ConvertDatetoNumber(dictBasicDataJson("maintainance_start_date_m"))
            dictBasicDataJson("maintainance_end_date_m") = PublicFunctions.ConvertDatetoNumber(dictBasicDataJson("maintainance_end_date_m"))

            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblcompanies", id, _sqlconn, _sqltrans) Then
                Dim letter_id = 0
                If id <> "" Then
                    letter_id = id
                Else
                    letter_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                End If

                If Savea_ttch_file(attch_file_DataJsonList, letter_id) Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return True
                Else
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
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
            Return False
        End Try
    End Function
#End Region



#Region "save_modules"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
                                                                                                                                                                                                <System.Web.Script.Services.ScriptMethod()>
    Public Function save_modules(ByVal id As String, ByVal comp_name As String, ByVal userId As String, ByVal comp_group_permission_id As String, ByVal basicDataJson As List(Of Object)) As Boolean
        Try
            _sqlconn.Close()
            _sqlconn.Open()

            _sqltrans = _sqlconn.BeginTransaction
            Dim result = 0
            Dim dt As DataTable
            Dim dt2 As DataTable
            Dim dt3 As DataTable
            Dim DetailsJSON1 As Dictionary(Of String, Object) = basicDataJson(0)

            Dim comp_permission_name As String = "صلاحيات" + "  " + comp_name

            If comp_group_permission_id = "" Or comp_group_permission_id = "0" Then

                If DBManager.ExcuteQueryTransaction("insert into tbllock_up (Description,Type) values('" + comp_permission_name.ToString + "','PG')", _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                Else
                    comp_group_permission_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans).ToString
                End If


                If DBManager.ExcuteQueryTransaction("update tblUsers set group_id=" + comp_group_permission_id.ToString + " where id=" + userId.ToString, _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If

            End If

            If DBManager.ExcuteQueryTransaction("delete  from tblcomp_modules where comp_id=" + DetailsJSON1("comp_id").ToString + " and group_id=" + comp_group_permission_id.ToString + " ", _sqlconn, _sqltrans) = -1 Then
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If


            For Each DetailsJSON As Dictionary(Of String, Object) In basicDataJson
                Dim dictBasicDataJson As Dictionary(Of String, Object) = DetailsJSON
                id = 0
                dictBasicDataJson.Add("group_id", comp_group_permission_id.ToString)
                PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblcomp_modules", id, _sqlconn, _sqltrans)
                result = 1
            Next
            If result = 1 Then
                _sqltrans.Commit()
                _sqlconn.Close()
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                dt = DBManager.Getdatatable("Select * from tblcomp_modules where comp_id=" + DetailsJSON1("comp_id").ToString + " and group_id=" + comp_group_permission_id.ToString)
                'dt3 = DBManager.Getdatatable("Select * from tblUsers where comp_id=" + DetailsJSON1("comp_id").ToString + " And User_Type=2")
                'DBManager.ExcuteQueryTransaction("delete  from tblPermissions where UserId=" + dt3.Rows(0).Item("id").ToString, _sqlconn, _sqltrans)

                DBManager.ExcuteQueryTransaction("delete  from tblgroup_permissons where group_id=" + comp_group_permission_id.ToString, _sqlconn, _sqltrans)
                For Each Row As DataRow In dt.Rows
                    dt2 = DBManager.Getdatatable("Select * from tblForms where type=1 And MenueId=" + Row.Item("module_id").ToString)
                    For Each row2 As DataRow In dt2.Rows
                        Dim dict = New Dictionary(Of String, Object)
                        dict.Add("group_id", comp_group_permission_id.ToString)
                        dict.Add("form_id", row2.Item("Id").ToString)
                        dict.Add("f_add", 1)
                        dict.Add("f_update", 1)
                        dict.Add("f_access", 1)
                        dict.Add("f_delete", 1)

                        PublicFunctions.TransUpdateInsert(dict, "tblgroup_permissons", id, _sqlconn, _sqltrans)
                    Next
                Next
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

#Region "save_info"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
                                                                                                                                                                                                    <WebMethod(True)>
                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function save_info(ByVal id As String, ByVal basicDataJson As Object, ByVal imagePath As String) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction

            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_details As DataTable
            dictBasicDataJson("alarm_date") = PublicFunctions.ConvertDatetoNumber(dictBasicDataJson("alarm_date"))
            dictBasicDataJson.Add("image", imagePath)
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblproject_info", id, _sqlconn, _sqltrans) Then
                If dictBasicDataJson("alarm") = 1 Then
                    dictBasicDataJson("RefCode") = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                    If id <> "0" And id <> "" Then
                        dt_details = DBManager.Getdatatable("Select * from tblNotifications  where  RefType = " + dictBasicDataJson("project_id").ToString + " And RefCode=" + id.ToString)
                        id = dt_details.Rows(0).Item("ID")
                    End If
                    If Not PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblNotifications", id, _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return False
                    End If
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
            Return False
        End Try
    End Function
#End Region
#Region "Get data"
    <WebMethod(True)>
                                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function Get_data() As String()
        Dim dtm As New DataTable
        Dim dtm2 As New DataTable
        Dim dtm3 As New DataTable
        Dim Names As New List(Of String)(10)
        dtm = DBManager.Getdatatable("Select * from tbllock_up where ISNUll(deleted, 0) = 0 And Type ='PPT'")

                Dim str As String = PublicFunctions.ConvertDataTabletoString(dtm)
        If dtm.Rows.Count <> 0 Then
            Names.Add(str)
        Else
            Names.Add("1")
        End If
        Return Names.ToArray
    End Function
#End Region

    '#Region "Edit"
    '    ''' <summary>
    '    ''' get  Type data from db when update
    '    ''' </summary>
    '    <WebMethod()>
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function Edit(ByVal editItemId As String) As String()
    '        Dim unitsId = editItemId
    '        Dim Names As New List(Of String)(10)
    '        Try
    '            Dim str As String = PublicFunctions.GetDataForUpdate("tblprojects", editItemId)
    '            Names.Add("1")
    '            Names.Add(str)
    '            Return Names.ToArray
    '        Catch ex As Exception
    '            Names.Add("0")
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        End Try
    '    End Function

    '#End Region
#Region "Delete"
    ''' <summary>
    ''' </summary>
                                                                                                                                                                                                                                        <WebMethod()>
                                                                                                                                                                                                                                            <System.Web.Script.Services.ScriptMethod()>
    Public Function Delete(ByVal deleteItems As String) As String()
        Dim Names As New List(Of String)(10)
        Dim dt As DataTable
        Try
            dt = DBManager.Getdatatable("select * from tblitems where unit_id=" + deleteItems.ToString)
            If dt.Rows.Count = 0 Then

                If PublicFunctions.DeleteFromTable(deleteItems, "tblunits") Then
                    Names.Add("1")
                    Names.Add("تم الحذف بنجاح!")
                Else
                    Names.Add("2")
                    Names.Add("لا يمكن الحذف!")
                End If
            Else
                Names.Add("3")
            End If
            Return Names.ToArray
        Catch
            Names.Add("2")
            Names.Add("لا يمكن الحذف!")
            Return Names.ToArray
        End Try
    End Function
#End Region

#Region "show_all"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                <WebMethod()>
                                                                                                                                                                                                                                                    <System.Web.Script.Services.ScriptMethod()>
    Public Function show_all(ByVal printItemId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_companies As New DataTable
            Dim dt_modules As New DataTable
            Dim dtattch_file As New DataTable
            Dim dt_user As New DataTable



            dt_companies = DBManager.Getdatatable("SELECT * from tblcompanies  where  tblcompanies.id = " + printItemId)
            dt_modules = DBManager.Getdatatable("SELECT  * from tblcomp_modules  where  comp_id = " + printItemId)
            dtattch_file = DBManager.Getdatatable("Select * from tblImages where Source='images_contact' and isnull(deleted,0)=0  and Source_id=" + printItemId.ToString())
            dt_user = DBManager.Getdatatable("select * from tblUsers where comp_id=" + printItemId + " and User_Type=2")
            If dt_companies.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt_companies)
                Names.Add(str)
            Else
                Names.Add("0")
            End If

            If dt_modules.Rows.Count <> 0 Then
                Dim str1 As String = PublicFunctions.ConvertDataTabletoString(dt_modules)
                Names.Add(str1)
            Else
                Names.Add("0")
            End If
            If dtattch_file.Rows.Count <> 0 Then
                Dim str2 = PublicFunctions.ConvertDataTabletoString(dtattch_file)
                Names.Add(str2)
            Else
                Names.Add("")
            End If

            If dt_user.Rows.Count <> 0 Then
                Dim str3 = PublicFunctions.ConvertDataTabletoString(dt_user)
                Names.Add(str3)
            Else
                Names.Add("")
            End If


            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "show_project_details"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                        <WebMethod()>
                                                                                                                                                                                                                                                                            <System.Web.Script.Services.ScriptMethod()>
    Public Function show_project_details(ByVal printItemId As String, ByVal project_id As String, ByVal type As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable
            Dim dt_finance As New DataTable
            Dim dt_prposal As New DataTable
            If type = 1 Then
                dt_details = DBManager.Getdatatable("select * from tblproject_workers  where tblproject_workers.worker_id=" + printItemId.ToString + " and  tblproject_workers.project_id=" + project_id.ToString)
                dt_finance = DBManager.Getdatatable("select * from tblproject_worker_finance  where project_worker_id=" + printItemId.ToString + " and  project_id=" + project_id.ToString)
            ElseIf type = 2 Then
                dt_details = DBManager.Getdatatable("SELECT tblproject_proposals.id as proposals_id, * from tblproject_proposals join tbllock_up on tblproject_proposals.send_type=tbllock_up.id where type='ST' and project_id = " + project_id + " and tblproject_proposals.id =" + printItemId.ToString)
            Else
                dt_details = DBManager.Getdatatable("SELECT * from tblproject_info  where  project_id = " + project_id + " and id=" + printItemId.ToString)
            End If
            If dt_details.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt_details)

                Names.Add(str)
            Else
                Names.Add("0")
            End If
            If dt_finance.Rows.Count Then
                Dim str1 As String = PublicFunctions.ConvertDataTabletoString(dt_finance)
                Names.Add(str1)
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

#Region "delete_details"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                    <WebMethod()>
                                                                                                                                                                                                                                                                                        <System.Web.Script.Services.ScriptMethod()>
    Public Function delete_details(ByVal printItemId As String, ByVal project_id As String, ByVal type As String) As Boolean

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable
            Dim dt_finance As New DataTable
            Dim dt_prposal As New DataTable
            Dim result = 0
            If type = 1 Then
                If DBManager.ExcuteQueryTransaction("delete from tblproject_workers  where tblproject_workers.worker_id=" + printItemId.ToString + " and  tblproject_workers.project_id=" + project_id.ToString, _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
                If DBManager.ExcuteQueryTransaction("delete from tblproject_worker_finance  where project_worker_id=" + printItemId.ToString + " and  project_id=" + project_id.ToString, _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
            ElseIf type = 2 Then
                If DBManager.ExcuteQueryTransaction("delete from tblproject_proposals  where  project_id = " + project_id + " and id=" + printItemId.ToString, _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
            Else
                If DBManager.ExcuteQueryTransaction("delete from tblproject_info  where  project_id = " + project_id + " and id=" + printItemId.ToString, _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function

#End Region
#Region "get_main_menu"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                            <WebMethod()>
                                                                                                                                                                                                                                                                                                <System.Web.Script.Services.ScriptMethod()>
    Public Function get_main_menu(ByVal editItemId As String) As String()
        Dim UserId = editItemId
        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("SELECT  * from tblMenus ")
            If dt.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
                Return Names.ToArray
            Else
                Names.Add("0")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "get_main_menu_for_edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                        <WebMethod()>
                                                                                                                                                                                                                                                                                                            <System.Web.Script.Services.ScriptMethod()>
    Public Function get_main_menu_for_edit(ByVal comp_Id As String, ByVal group_Id As String) As String()
        '  Dim UserId = editItemId
        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            Dim dt2 As New DataTable
            dt = DBManager.Getdatatable("SELECT  * from tblMenus ")
            dt2 = DBManager.Getdatatable("SELECT  * from tblcomp_modules where comp_id=" + comp_Id.ToString + " and group_id=" + group_Id.ToString)
            If dt.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
                Dim str2 As String = PublicFunctions.ConvertDataTabletoString(dt2)
                Names.Add(str2)
                Return Names.ToArray
            Else
                Names.Add("0")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
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
            DBManager.ExcuteQuery("delete from tblImages where Source_id=" + letter_Id.ToString + " and Source='images_contact' ")
            For Each file_JSON As Object In attch_file_DataJsonList

                Dim dictfile As Dictionary(Of String, Object) = file_JSON
                Dim Source_id = letter_Id
                Dim Source = "images_contact"
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
#Region "get_admin"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
                                                                                                                                                                                                                                                                                                                        <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_admin() As String()
        Dim dt_company As DataTable
        Dim dt_center As DataTable
        Dim dt_acadyme As DataTable
        Dim dt_compAdmin As DataTable
        Dim dt_cenAdmin As DataTable
        Dim dt_acAdmin As DataTable
        Dim Names As New List(Of String)(10)
        Dim condation = ""

        Dim compAdmin_id = LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString()

        If compAdmin_id <> 1 Then
            condation = " and User_Type = 2"
            Try
                dt_compAdmin = DBManager.Getdatatable("select * from tblUsers where  id=" + compAdmin_id + condation)
                If dt_compAdmin.Rows.Count <> 0 Then
                    Dim str3 = PublicFunctions.ConvertDataTabletoString(dt_compAdmin)
                    Names.Add(str3)

                    dt_company = DBManager.Getdatatable("SELECT * from tblcompanies  where  id= " + dt_compAdmin.Rows(0).Item("comp_id").ToString())
                    If dt_company.Rows.Count <> 0 Then
                        Dim str2 = PublicFunctions.ConvertDataTabletoString(dt_company)
                        Names.Add(str2)
                        dt_acAdmin = DBManager.Getdatatable("SELECT * from tblUsers  where group_id=122 and comp_id= " + dt_compAdmin.Rows(0).Item("comp_id").ToString())
                        If dt_acAdmin.Rows.Count <> 0 Then
                            Dim str4 = PublicFunctions.ConvertDataTabletoString(dt_acAdmin)
                            Names.Add(str4)
                            dt_acadyme = DBManager.Getdatatable("SELECT * from acd_acadmies  where admin=" + dt_acAdmin.Rows(0).Item("id").ToString() + " and  comp_id= " + dt_compAdmin.Rows(0).Item("comp_id").ToString())
                            If dt_acadyme.Rows.Count <> 0 Then
                                Dim str5 = PublicFunctions.ConvertDataTabletoString(dt_acadyme)
                                Names.Add(str5)
                            Else
                                Names.Add("0")
                            End If
                        Else
                            Names.Add("0")
                            Names.Add("0")
                        End If
                        dt_cenAdmin = DBManager.Getdatatable("SELECT * from tblUsers  where group_id=121 and comp_id= " + dt_compAdmin.Rows(0).Item("comp_id").ToString())
                        If dt_cenAdmin.Rows.Count <> 0 Then
                            Dim str6 = PublicFunctions.ConvertDataTabletoString(dt_cenAdmin)
                            Names.Add(str6)
                            dt_center = DBManager.Getdatatable("SELECT * from acd_training_centers  where admin=" + dt_cenAdmin.Rows(0).Item("id").ToString() + " and  comp_id= " + dt_compAdmin.Rows(0).Item("comp_id").ToString())
                            If dt_center.Rows.Count <> 0 Then
                                Dim str7 = PublicFunctions.ConvertDataTabletoString(dt_center)
                                Names.Add(str7)
                            Else
                                Names.Add("0")
                            End If
                        Else
                            Names.Add("0")
                            Names.Add("0")
                        End If
                    Else
                        Names.Add("0")
                    End If
                Else
                    Names.Add("0")
                    Names.Add("0")
                End If


            Catch ex As Exception
                Names.Add("0")
                Return Names.ToArray
            End Try
        Else
            Names.Add("admin")
        End If
        Return Names.ToArray
    End Function

#End Region

#Region "check_user"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
                                                                                                                                                                                                                                                                                                                                                                <System.Web.Script.Services.ScriptMethod()>
    Public Function check_user(ByVal user_name As String) As Boolean
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str2 = ""
        Try
            Dim dt As New DataTable
            Dim dt2 As New DataTable
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where User_Name='" + user_name.ToString + "'")
            If dt_user.Rows.Count <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
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
        Dim dt_companies As DataTable
        Try
            dt_companies = DBManager.Getdatatable("select tblcompanies.id as 'id' ,tblUsers.id as'user_id', * from tblcompanies inner join tblUsers on tblUsers.comp_id=tblcompanies.id  where tblcompanies.id =" + editItemId.ToString())
            If dt_companies.Rows.Count <> 0 Then
                Dim str2 = PublicFunctions.ConvertDataTabletoString(dt_companies)
                Names.Add("1")
                Names.Add(str2)
            Else
                Names.Add("0")
            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function
#End Region




End Class