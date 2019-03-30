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
    Public Function check_userData(ByVal id As String, ByVal user_indenty As String, ByVal User_PhoneNumber As String) As Boolean

        Try
            Dim dt As New DataTable
            If id = "" Then
                dt = DBManager.Getdatatable("Select * from TblUsers where user_indenty='" + user_indenty + "' or User_PhoneNumber='" + User_PhoneNumber + "'")
            Else
                dt = DBManager.Getdatatable("Select * from TblUsers where ( user_indenty='" + user_indenty + "' OR  User_PhoneNumber='" + User_PhoneNumber + "')  and  Id!='" + id + "'")
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
            Dim CompAdmin_indenty = dictCompAdminDataJson("user_indenty")
            Dim CompAdmin_Phone = dictCompAdminDataJson("User_PhoneNumber")
            If check_userData(CompAdmin_id, CompAdmin_indenty, CompAdmin_Phone) Then
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
                    dictCompDataJson.Add("deal_start_date_m", contractdictDataJson("deal_end_date_m"))
                    dictCompDataJson.Add("deal_start_date_hj", contractdictDataJson("deal_start_date_hj"))
                    dictCompDataJson.Add("deal_end_date_m", contractdictDataJson("deal_end_date_m"))
                    dictCompDataJson.Add("deal_end_date_hj", contractdictDataJson("deal_end_date_hj"))
                    If Convert.ToBoolean(dictCompDataJson("maintainance")) Then
                        dictCompDataJson.Add("maintainance_start_date_m", contractdictDataJson("maintainance_start_date_m"))
                        dictCompDataJson.Add("maintainance_start_date_hj", contractdictDataJson("maintainance_start_date_hj"))
                        dictCompDataJson.Add("maintainance_end_date_m", contractdictDataJson("maintainance_end_date_m"))
                        dictCompDataJson.Add("maintainance_end_date_hj", contractdictDataJson("maintainance_end_date_hj"))
                    End If
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

                        If DBManager.ExcuteQueryTransaction("insert into tbllock_up (Description,Type,Comp_id,RelatedId) values('" + comp_permission_name.ToString + "','PG'," + Comp_id + ",1)", _sqlconn, _sqltrans) = -1 Then
                            success = False
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Names.Add("لم يتم الحفظ")
                        Else
                            comp_group_permission_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans).ToString
                        End If
                        'insert new group for advisors
                        Dim advisor_permission_name As String = "صلاحيات المستشارين"

                        If DBManager.ExcuteQueryTransaction("insert into tbllock_up (Description,Type,Comp_id,RelatedId) values('" + advisor_permission_name.ToString + "','PG'," + Comp_id + ",2)", _sqlconn, _sqltrans) = -1 Then
                            success = False
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Names.Add("لم يتم الحفظ")
                        Else
                            Dim dic_advisor_permission As New Dictionary(Of String, Object)
                            dic_advisor_permission.Add("group_id", PublicFunctions.GetIdentity(_sqlconn, _sqltrans).ToString)
                            dic_advisor_permission.Add("form_id", 2156)
                            dic_advisor_permission.Add("f_add", 1)
                            dic_advisor_permission.Add("f_update", 1)
                            dic_advisor_permission.Add("f_access", 1)
                            dic_advisor_permission.Add("f_delete", 1)

                            If Not PublicFunctions.TransUpdateInsert(dic_advisor_permission, "tblgroup_permissons", "", _sqlconn, _sqltrans) Then
                                success = False
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Names.Add("لم يتم الحفظ")
                            Else
                                dic_advisor_permission("form_id") = 3179
                                If Not PublicFunctions.TransUpdateInsert(dic_advisor_permission, "tblgroup_permissons", "", _sqlconn, _sqltrans) Then
                                    success = False
                                    _sqltrans.Rollback()
                                    _sqlconn.Close()
                                    Names.Add("لم يتم الحفظ")
                                End If
                            End If
                        End If
                        'insert new group for Beneficiaries
                        Dim Beneficiaries_permission_name As String = "صلاحيات المستفدين"

                        If DBManager.ExcuteQueryTransaction("insert into tbllock_up (Description,Type,Comp_id,RelatedId) values('" + Beneficiaries_permission_name.ToString + "','PG'," + Comp_id + ",3)", _sqlconn, _sqltrans) = -1 Then
                            success = False
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Names.Add("لم يتم الحفظ")
                        Else
                            Dim dic_Beneficiaries_permission As New Dictionary(Of String, Object)
                            dic_Beneficiaries_permission.Add("group_id", PublicFunctions.GetIdentity(_sqlconn, _sqltrans).ToString)
                            dic_Beneficiaries_permission.Add("form_id", 3184)
                            dic_Beneficiaries_permission.Add("f_add", 1)
                            dic_Beneficiaries_permission.Add("f_update", 1)
                            dic_Beneficiaries_permission.Add("f_access", 1)
                            dic_Beneficiaries_permission.Add("f_delete", 1)

                            If Not PublicFunctions.TransUpdateInsert(dic_Beneficiaries_permission, "tblgroup_permissons", "", _sqlconn, _sqltrans) Then
                                success = False
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Names.Add("لم يتم الحفظ")
                            Else
                                dic_Beneficiaries_permission("form_id") = 2156
                                If Not PublicFunctions.TransUpdateInsert(dic_Beneficiaries_permission, "tblgroup_permissons", "", _sqlconn, _sqltrans) Then
                                    success = False
                                    _sqltrans.Rollback()
                                    _sqlconn.Close()
                                    Names.Add("لم يتم الحفظ")
                                End If
                            End If
                        End If

                        'insert new group for Students
                        Dim Student_permission_name As String = "صلاحيات الطلاب"

                        If DBManager.ExcuteQueryTransaction("insert into tbllock_up (Description,Type,Comp_id,RelatedId) values('" + Student_permission_name.ToString + "','PG'," + Comp_id + ",4)", _sqlconn, _sqltrans) = -1 Then
                            success = False
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Names.Add("لم يتم الحفظ")
                        Else
                            Dim dic_Beneficiaries_permission As New Dictionary(Of String, Object)
                            dic_Beneficiaries_permission.Add("group_id", PublicFunctions.GetIdentity(_sqlconn, _sqltrans).ToString)
                            dic_Beneficiaries_permission.Add("form_id", 3177)
                            dic_Beneficiaries_permission.Add("f_add", 1)
                            dic_Beneficiaries_permission.Add("f_update", 1)
                            dic_Beneficiaries_permission.Add("f_access", 1)
                            dic_Beneficiaries_permission.Add("f_delete", 1)

                            If Not PublicFunctions.TransUpdateInsert(dic_Beneficiaries_permission, "tblgroup_permissons", "", _sqlconn, _sqltrans) Then
                                success = False
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Names.Add("لم يتم الحفظ")
                            Else
                                dic_Beneficiaries_permission("form_id") = 3181
                                If Not PublicFunctions.TransUpdateInsert(dic_Beneficiaries_permission, "tblgroup_permissons", "", _sqlconn, _sqltrans) Then
                                    success = False
                                    _sqltrans.Rollback()
                                    _sqlconn.Close()
                                    Names.Add("لم يتم الحفظ")
                                End If
                            End If
                        End If
                        'insert new group for Students
                        Dim Trainner_permission_name As String = "صلاحيات المدرب"

                        If DBManager.ExcuteQueryTransaction("insert into tbllock_up (Description,Type,Comp_id,RelatedId) values('" + Trainner_permission_name.ToString + "','PG'," + Comp_id + ",5)", _sqlconn, _sqltrans) = -1 Then
                            success = False
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Names.Add("لم يتم الحفظ")
                        Else
                            Dim dic_Beneficiaries_permission As New Dictionary(Of String, Object)
                            dic_Beneficiaries_permission.Add("group_id", PublicFunctions.GetIdentity(_sqlconn, _sqltrans).ToString)
                            dic_Beneficiaries_permission.Add("form_id", 3177)
                            dic_Beneficiaries_permission.Add("f_add", 1)
                            dic_Beneficiaries_permission.Add("f_update", 1)
                            dic_Beneficiaries_permission.Add("f_access", 1)
                            dic_Beneficiaries_permission.Add("f_delete", 1)

                            If Not PublicFunctions.TransUpdateInsert(dic_Beneficiaries_permission, "tblgroup_permissons", "", _sqlconn, _sqltrans) Then
                                success = False
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Names.Add("لم يتم الحفظ")
                            Else
                                dic_Beneficiaries_permission("form_id") = 3181
                                If Not PublicFunctions.TransUpdateInsert(dic_Beneficiaries_permission, "tblgroup_permissons", "", _sqlconn, _sqltrans) Then
                                    success = False
                                    _sqltrans.Rollback()
                                    _sqlconn.Close()
                                    Names.Add("لم يتم الحفظ")
                                End If
                            End If
                        End If

                    Else
                        Dim dt As New DataTable
                        dt = DBManager.Getdatatable("select group_id from tblUsers where User_Type=2 and comp_id=" + formId)
                        If dt.Rows.Count <> 0 Then
                            comp_group_permission_id = dt.Rows(0)(0).ToString
                        End If
                        DBManager.ExcuteQueryTransaction("delete  from tblgroup_permissons where group_id=" + comp_group_permission_id.ToString, _sqlconn, _sqltrans)

                    End If
                    ' insert  comp modules 

                    For Each ModulesJSON As Dictionary(Of String, Object) In arrDataJson(2)
                                Dim dictBasicDataJson As Dictionary(Of String, Object) = ModulesJSON
                                Dim id = 0
                                dictBasicDataJson.Add("comp_id", Comp_id.ToString)
                                dictBasicDataJson.Add("group_id", comp_group_permission_id.ToString)
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
                    Dim AcAdmin_indenty = dictAcaAdminDataJson("user_indenty")
                    Dim AcAdmin_Phone = dictAcaAdminDataJson("User_PhoneNumber")
                    If check_userData(AcAdmin_id, AcAdmin_indenty, AcAdmin_Phone) And AcAdmin_indenty <> CompAdmin_indenty And AcAdmin_Phone <> CompAdmin_Phone Then
                        Dim dictCenAdminDataJson As Dictionary(Of String, Object) = arrDataJson(5)
                        Dim CenAdmin_id = dictCenAdminDataJson("id")
                        Dim CenAdmin_indenty = dictCenAdminDataJson("user_indenty")
                        Dim CenAdmin_Phone = dictCenAdminDataJson("User_PhoneNumber")
                        If check_userData(CenAdmin_id, CenAdmin_indenty, CenAdmin_Phone) And CenAdmin_indenty <> AcAdmin_indenty And CenAdmin_indenty <> CompAdmin_indenty And CenAdmin_Phone <> AcAdmin_Phone And CenAdmin_Phone <> CompAdmin_Phone Then
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
                            Names.Add("رقم الهوية او التلفون لمدير مركز التدريب مُستخدم")
                        End If
                    Else
                        Names.Add("رقم الهوية او التلفون لمدير الاكاديمية مُستخدم")
                    End If
                End If
            Else
                Names.Add("رقم الهوية او التلفون لمدير الجهة مُستخدم")
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
            dt_companies = DBManager.Getdatatable("select * from tblcompanies   where id =" + editItemId.ToString())
            If dt_companies.Rows.Count <> 0 Then
                Dim str2 = PublicFunctions.ConvertDataTabletoString(dt_companies)
                Names.Add("1")
                Names.Add(str2)
                Dim dt_compAdmin As New DataTable
                dt_compAdmin = DBManager.Getdatatable("select * from tblUsers where User_Type=2 and  comp_id=" + editItemId.ToString())
                If dt_compAdmin.Rows.Count <> 0 Then
                    Dim str3 = PublicFunctions.ConvertDataTabletoString(dt_compAdmin)
                    Names.Add(str3)
                Else
                    Names.Add("")
                End If
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