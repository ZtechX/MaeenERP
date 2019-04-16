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
Public Class userPorfile
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
    Public Function SaveUser(ByVal UserId As String, ByVal basicDataJson As Object, ByVal image As String) As String
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim success As Boolean = False
            Dim Researcher As Boolean = False

            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("User_Image", image)
            Dim dtcheckemailphone As DataTable
            If LoginInfo.getUserType() = "9" Then
                dictBasicDataJson.Add("password_changed", 1)
            End If
            dtcheckemailphone = DBManager.Getdatatable("Select * from TblUsers where ( user_indenty='" + dictBasicDataJson("user_indenty") + "' or User_PhoneNumber='" + dictBasicDataJson("User_PhoneNumber") + "')  and  Id!='" + UserId.ToString + "'")
            If dtcheckemailphone.Rows.Count = 0 Then
                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblUsers", UserId, _sqlconn, _sqltrans) Then
                    success = True
                Else
                    success = False
                End If
            Else
                Return "False|رقم الهوية او التلفون مُستخدم"
            End If

            If success Then
                _sqltrans.Commit()
                _sqlconn.Close()
                Return "True|تم الحفظ بنجاح"
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return "False|لم يتم الحفظ"
            End If
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return "False|لم يتم الحفظ"
        End Try
    End Function
#End Region

#Region "GetUserData"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetUserData() As String()
        Dim UserId = LoginInfo.GetUser__Id()
        Dim Names As New List(Of String)(10)
        Names.Add("")
        Names.Add("")
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("SELECT tblUsers.id,full_name ,User_Email,Active ,Researcher,User_Password,User_PhoneNumber,User_Image," +
 " User_Type,user_indenty,isNull(name,'') as userType, isNull(password_changed,0) as 'password_changed' " +
 " FROM tblUsers left join tblUser_Type on tblUser_Type.id=User_Type where tblUsers.id  =" + UserId)
            If dt.Rows.Count <> 0 Then
                Names(0) = PublicFunctions.ConvertDataTabletoString(dt)
                If Convert.ToBoolean(dt.Rows(0).Item("Researcher")) Then
                    dt = DBManager.Getdatatable("select * from tblusers_area where user_id =" + UserId)
                    If dt.Rows.Count <> 0 Then
                        Names(1) = PublicFunctions.ConvertDataTabletoString(dt)
                    End If
                End If
            End If
            Return Names.ToArray
        Catch ex As Exception
            Return Names.ToArray
        End Try
    End Function

#End Region

End Class