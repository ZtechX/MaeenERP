Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer

Public Class Notification1
    Inherits System.Web.UI.Page

#Region "Global_Variables"
    'declare global variables
    Dim UserId As Integer
    ' Dim daLookup As New TblLockupFactory
    Dim pf As New PublicFunctions
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class