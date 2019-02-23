#Region "Import"
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Web.Script.Services
Imports BusinessLayer.BusinessLayer
Imports System.Data.SqlClient
Imports System.IO
Imports ERpMaen

#End Region

'Imports System.Xml
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<System.Web.Script.Services.ScriptService()> _
Public Class User
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
    Dim daUser As New TblUsers
    Dim df As New TblUsersFactory
#End Region

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveUser(ByVal UserId As String, ByVal basicDataJson As Object, ByVal permDataJsonList As List(Of Object), ByVal researchAreaJsonList As List(Of Object), ByVal image As String) As String
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim success As Boolean = False
            Dim Researcher As Boolean = False

            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("User_Image", image)
            dictBasicDataJson.Add("User_Type", 13)
            Researcher = dictBasicDataJson("Researcher")
            Dim dtcheckemailphone As DataTable
            If UserId = "" Then
                dtcheckemailphone = DBManager.Getdatatable("Select * from TblUsers where User_Name='" + dictBasicDataJson("User_Name") + "' OR  User_Email='" + dictBasicDataJson("User_Email") + "' or User_PhoneNumber='" + dictBasicDataJson("User_PhoneNumber") + "'")
            Else
                dtcheckemailphone = DBManager.Getdatatable("Select * from TblUsers where ( User_Name='" + dictBasicDataJson("User_Name") + "' OR User_Email='" + dictBasicDataJson("User_Email") + "' or User_PhoneNumber='" + dictBasicDataJson("User_PhoneNumber") + "')  and  Id!='" + UserId.ToString + "'")
            End If


            If dtcheckemailphone.Rows.Count = 0 Then
                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblUsers", UserId, _sqlconn, _sqltrans) Then
                    success = True
                Else
                    success = False
                End If
            Else
                Return "False|اسم المستخدم او البريد الالكترونى او التلفون مستخدم"
            End If

            If UserId = "" And success Then
                Dim Id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                If save_permtion(permDataJsonList, Id, "Insert") Then
                    If Researcher Then
                        If save_researchArea(researchAreaJsonList, Id) Then
                            success = True
                        Else
                            success = False
                        End If
                    Else
                        success = True
                    End If
                Else
                    success = False
                End If
            ElseIf UserId <> "" And success Then

                If save_permtion(permDataJsonList, UserId, "Update") Then
                    If Researcher Then
                        If save_researchArea(researchAreaJsonList, UserId) Then
                            success = True
                        Else
                            success = False
                        End If
                    Else
                        DBManager.ExcuteQuery("delete from tblusers_area where  user_id ='" + UserId + "'")
                        success = True
                    End If

                Else
                    success = False
                End If
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
    Private Function save_permtion(ByVal permDataJsonList As List(Of Object), ByVal usertId As String, ByVal operation As String) As Boolean
        Try
            For Each permJSON As Object In permDataJsonList

                Dim dictperm As Dictionary(Of String, Object) = permJSON
                Dim id = ""
                dictperm("UserId") = usertId
                If operation = "Insert" Then
                    id = ""
                ElseIf operation = "Update" Then
                    id = dictperm("permid")
                End If
                If Not PublicFunctions.TransUpdateInsert(dictperm, "tblPermissions", id, _sqlconn, _sqltrans) Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "get_main_menu"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()> _
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
                " where id in (SELECT MenueId FROM tblPermissions left join tblForms on tblPermissions.FormId =tblForms.Id" +
               " where PAccess = 1 And userID = " + user_id + ")")
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
                " FROM tblPermissions left join tblForms on tblPermissions.FormId =tblForms.Id" +
                " where PAccess=1 and  UserId = " + user_id)
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

#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()> _
    <System.Web.Script.Services.ScriptMethod()>
    Public Function EditUser(ByVal editItemId As String) As String()
        Dim UserId = editItemId
        Dim Researcher = False
        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("SELECT  id,full_name ,User_Name,User_Password,User_Email,group_id" +
             ",User_PhoneNumber,Active,User_Type,User_Image,Deleted,related_id" +
             ",managment_id,comp_id,jop_id,isNull(Researcher,0) Researcher,superAdmin from TblUsers where id  =" + UserId)
            If dt.Rows.Count <> 0 Then
                Researcher = dt.Rows(0).Item("Researcher")
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add("1")
                Names.Add(str)
                Dim dtPerm = DBManager.Getdatatable("Select ID, formid,paccess,padd,PEdite,pdelete from TblPermissions where UserId=" + editItemId.ToString)
                If dtPerm.Rows.Count <> 0 Then
                    Dim s1 = PublicFunctions.ConvertDataTabletoString(dtPerm)
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
                If Researcher Then
                    dt = DBManager.Getdatatable("select * from tblusers_area where user_id =" + UserId)
                    If dt.Rows.Count <> 0 Then
                        Names.Add(PublicFunctions.ConvertDataTabletoString(dt))
                    Else
                        Names.Add("0")
                    End If
                End If
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

#Region "menulength"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()> _
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

#Region "Delete"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Delete(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "tblUsers") Then
                DBManager.ExcuteQuery("delete from tblPermissions where  UserId ='" + deleteItem.ToString + "'")
                DBManager.ExcuteQuery("delete from tblusers_area where  user_id ='" + deleteItem + "'")
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

    <WebMethod(True)> _
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

    <WebMethod(True)> _
  <System.Web.Script.Services.ScriptMethod()>
    Public Function GetPlaces() As String()
        Dim comp_id = LoginInfo.GetComp_id()
        Dim Names As New List(Of String)(10)
        Dim dt As New DataTable
        dt = DBManager.Getdatatable("SELECT  id,description from tbllock_up where type='CTY' and comp_id=  " + comp_id)
        If dt.Rows.Count <> 0 Then
            Names.Add(PublicFunctions.ConvertDataTabletoString(dt))
        Else
            Names.Add("")
        End If
        dt = DBManager.Getdatatable("SELECT  id,description from tbllock_up where type='CEN' and comp_id=  " + comp_id)
        If dt.Rows.Count <> 0 Then
            Names.Add(PublicFunctions.ConvertDataTabletoString(dt))
        Else
            Names.Add("")
        End If
        dt = DBManager.Getdatatable("SELECT  id,description from tbllock_up where type='VILL' and comp_id=  " + comp_id)
        If dt.Rows.Count <> 0 Then
            Names.Add(PublicFunctions.ConvertDataTabletoString(dt))
        Else
            Names.Add("")
        End If
        dt = DBManager.Getdatatable("SELECT  id,description from tbllock_up where type='BIO' and comp_id=  " + comp_id)
        If dt.Rows.Count <> 0 Then
            Names.Add(PublicFunctions.ConvertDataTabletoString(dt))
        Else
            Names.Add("")
        End If
        Return Names.ToArray
    End Function


#Region "save_researchArea"
    ''' <summary>"
    ''' Save About images into db 
    ''' </summary>
    Private Function save_researchArea(ByVal researchAreaJsonList As List(Of Object), ByVal usertId As String) As Boolean
        Try
            DBManager.ExcuteQuery("delete from tblusers_area where  user_id ='" + usertId + "'")

            For Each areaJSON As Object In researchAreaJsonList
                Dim dictarea As Dictionary(Of String, Object) = areaJSON
                dictarea.Add("user_id", usertId)
                If Not PublicFunctions.TransUpdateInsert(dictarea, "tblusers_area", "", _sqlconn, _sqltrans) Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

    <WebMethod(True)> _
<System.Web.Script.Services.ScriptMethod()>
    Public Function getRelatedTo(ByVal relatedto As String) As String
        Dim dt As DataTable = New DataTable()
        dt = DBManager.Getdatatable("select id,type from tbllock_up where RelatedId =" + relatedto)
        If dt.Rows.Count <> 0 Then
            Return PublicFunctions.ConvertDataTabletoString(dt)
        End If
        Return ""
    End Function
End Class