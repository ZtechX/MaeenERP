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
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<System.Web.Script.Services.ScriptService()>
Public Class group_permissons
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveUser(ByVal group_id As String, ByVal permDataJsonList As List(Of Object)) As String
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim success As Boolean = False
            Dim Researcher As Boolean = False


            If save_permtion(permDataJsonList, group_id, "Insert") Then

                If Not PublicFunctions.TransUsers_logs("2157", "tblgroup_permissons", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If

            If success Then

                _sqltrans.Commit()
                _sqlconn.Close()
                Return "True|"
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return "False|"
            End If
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return "False|"
        End Try
    End Function
#End Region

#Region "save_permtion"
    ''' <summary>"
    ''' Save About images into db 
    ''' </summary>
    Private Function save_permtion(ByVal permDataJsonList As List(Of Object), ByVal group_id As String, ByVal operation As String) As Boolean
        Try
            DBManager.ExcuteQuery("delete from tblgroup_permissons where  group_id ='" + group_id.ToString + "'")
            For Each permJSON As Object In permDataJsonList

                Dim dictperm As Dictionary(Of String, Object) = permJSON
                Dim id = ""
                dictperm("group_id") = group_id
                If Not PublicFunctions.TransUpdateInsert(dictperm, "tblgroup_permissons", id, _sqlconn, _sqltrans) Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_data(ByVal comp_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select * from tblgroup_permissonss where comp_id=" + comp_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
        Names.Add("0")
        Names.Add(" No Results were Found!")
        Return Names.ToArray
    End Function

#End Region

#Region "Check user"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function checkUser() As String

        Try
            Dim dt As New DataTable
            Dim UserId As String = HttpContext.Current.Request.Cookies("UserInfo")("UserId")
            dt = DBManager.Getdatatable("select isNull(comp_id,'0') comp_id,User_Type  from tblUsers where id=" + UserId)
            If dt.Rows.Count <> 0 Then

                If dt.Rows(0)("User_Type").ToString = "13" Then
                    Return "Superadmin"
                End If
                Return dt.Rows(0)("comp_id").ToString
            End If

        Catch ex As Exception

            Return ""
        End Try
        Return ""
    End Function

#End Region


#Region "Edit_permissons"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Edit_permissons(ByVal editItemId As String) As String()
        Dim UserId = editItemId
        Dim Researcher = False
        Dim Names As New List(Of String)(10)
        Try
            'Dim dt As New DataTable
            'dt = DBManager.Getdatatable("SELECT  id,full_name ,User_Name,User_Password,User_Email" +
            ' ",User_PhoneNumber,Active,User_Type,User_Image,Deleted,related_id" +
            ' ",managment_id,comp_id,jop_id,isNull(Researcher,0) Researcher,superAdmin from TblUsers where id  =" + UserId)
            'If dt.Rows.Count <> 0 Then
            'Researcher = dt.Rows(0).Item("Researcher")
            '    Dim str As String = PublicFunctions.ConvertDataTabletoString(dt)
            '    Names.Add("1")
            '    Names.Add(str)
            Dim dtPerm = DBManager.Getdatatable("Select id as'ID',form_id as 'formid',f_access as 'paccess',f_add as 'padd',f_update as 'PEdite',f_delete as 'pdelete' from tblgroup_permissons where group_id=" + editItemId.ToString)
            If dtPerm.Rows.Count <> 0 Then
                    Dim s1 = PublicFunctions.ConvertDataTabletoString(dtPerm)
                Names.Add("1")
                Names.Add(s1)
            Else
                Names.Add("0")

            End If

                Dim dtmenu = DBManager.Getdatatable("Select Id from tblMenus ")
                If dtmenu.Rows.Count <> 0 Then
                    Dim s2 = PublicFunctions.ConvertDataTabletoString(dtmenu)
                    Names.Add(s2)
                Else
                    Names.Add("0")
                End If
            'If Researcher Then
            '    dt = DBManager.Getdatatable("select * from tblusers_area where user_id =" + UserId)
            '    If dt.Rows.Count <> 0 Then
            '        Names.Add(PublicFunctions.ConvertDataTabletoString(dt))
            '    Else
            '        Names.Add("0")
            '    End If
            'End If
            Return Names.ToArray
            'Else
            '    Names.Add("0")
            '    Names.Add(" No Results were Found!")
            '    Return Names.ToArray
            'End If
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("tblgroup_permissonss", editItemId)
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
    Public Function Delete(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "tblgroup_permissonss") Then
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

#Region "get_main_menu"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_main_menu() As String()
        Dim user_id = LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            Dim super = superAdmin()
            If super Then
                dt = DBManager.Getdatatable("SELECT  * from tblMenus  ")
            Else
                dt = DBManager.Getdatatable("SELECT * FROM tblMenus" +
                " where id in (SELECT module_id FROM tblcomp_modules " +
               " where comp_id = " + LoginInfo.GetComp_id() + " )")
            End If

            If dt.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt)
                Dim str2 As String = ""
                Dim dt2 As New DataTable
                If super Then
                    dt2 = DBManager.Getdatatable("SELECT  * from  tblForms ")
                Else
                    dt2 = DBManager.Getdatatable("SELECT tblForms.Id,FormName,FormTitle ,ArFormTitle,FormUrl" +
                " ,FormQuery,GroupId,Icon,OPeration,MenueId,tblForms.Deleted,FormQueryAr" +
                " FROM tblgroup_permissons left join tblForms on tblgroup_permissons.form_id =tblForms.Id" +
                " where   group_id = " + LoginInfo.Getgroup_id())
                End If


                If dt2.Rows.Count <> 0 Then
                    str2 = PublicFunctions.ConvertDataTabletoString(dt2)
                End If
                Names.Add("1")
                Names.Add(str)
                Names.Add(str2)
                Return Names.ToArray
            Else
                Names.Add("0")
                Names.Add(" No Results were Found!")
                Names.Add(" No Results were Found!")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "superAdmin"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function superAdmin() As Boolean
        Dim user_id = LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString()
        Dim dt As New DataTable
        dt = DBManager.Getdatatable("SELECT  isNull(superAdmin,0)  superAdmin from tblUsers where id=  " + user_id)
        If dt.Rows.Count <> 0 Then
            Return dt.Rows(0)("superAdmin")
        End If
        Return False
    End Function
#End Region

#Region "menulength"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function menulength(ByVal editItemId As String) As String()
        Dim UserId = editItemId
        Dim Names As New List(Of String)(10)
        Try
            Dim dtm As New DataTable
            dtm = DBManager.Getdatatable("SELECT  Id from  tblMenus")
            If dtm.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dtm)
                Names.Add("1")
                Names.Add(str)


                Return Names.ToArray
            Else
                Names.Add("0")
                Names.Add(" No Results were Found!")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region
#Region "Save Group"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function savegroup(ByVal group_nm As String) As String
        Try

            Dim Comp_id = LoginInfo.GetComp_id()
            If DBManager.Getdatatable("select * from tbllock_up where Description='" + group_nm + "' and Comp_id=" + Comp_id).Rows.Count <> 0 Then
                Return "False|اسم المجموعة موجود بالفعل"
            Else
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                Dim dictBasicDataJson As New Dictionary(Of String, Object)
                dictBasicDataJson.Add("Description", group_nm)
                dictBasicDataJson.Add("Type", "PG")
                dictBasicDataJson.Add("Comp_id", Comp_id)
                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tbllock_up", "", _sqlconn, _sqltrans) Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return "True"
                End If

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

End Class