Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data
Imports System.Web.UI.WebControls
Imports BusinessLayer.BusinessLayer
Imports System.Data.SqlClient
Imports System.Net
Imports System.Web.UI
Imports System
Imports System.Web
Imports ERpMaen

Public Class LoginInfo
    Public Shared Function CheckPermisions(ByRef grid As GridView, ByRef Add As LinkButton, ByRef Update As LinkButton, ByRef Delete As LinkButton, ByRef Print As LinkButton, ByRef Search As TextBox, ByVal page As Page, ByVal UserId As String, ByRef FormName As Label) As String
        Dim pageName As String = PublicFunctions.GetPageName(page.Request.Url.ToString)
        Dim dtLeadsProperty As New TblPermissions
        Dim pf As New PublicFunctions
        ' Dim SalesManCode = page.Request.Cookies("UserInfo")("SalesmanCode")
        'pf.GetUserType(SalesManCode)
        Dim usertype = clsGeneralVariables.usertype

        Dim PageOperation As String = PublicFunctions.GetPageOperation(page.Request.Url.ToString)
        Dim dt As New DataTable
        If PageOperation.Contains("Add") Then
            dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select FormId from tblForms where FormName='" + pageName + "' and Operation ='" + PageOperation + "') ")
        Else
            dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select FormId from tblForms where FormName='" + pageName + "' and (Operation is null or Operation ='')) ")
        End If


        'dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select id from tblForms where FormName='" + pageName + "')")
        If dt.Rows.Count <> 0 Then
            Dim dtform As DataTable
            If PageOperation.Contains("Add") Then
                dtform = DBManager.Getdatatable("select FormTitle from tblForms where FormName='" + pageName + "' and Operation ='" + PageOperation + "'")
            Else
                dtform = DBManager.Getdatatable("select FormTitle from tblForms where FormName='" + pageName + "' and (Operation is null or Operation ='')")
            End If

            FormName.Text = dtform.Rows(0).Item("FormTitle").ToString
            Add.Visible = dt.Rows(0).Item("PAdd").ToString
            Update.Visible = dt.Rows(0).Item("PUpdate").ToString

            Delete.Visible = dt.Rows(0).Item("PDelete").ToString
            If usertype <> "Admin" Then
                Delete.Visible = False
            End If
            Print.Visible = False
            Search.Visible = dt.Rows(0).Item("PSearch").ToString
            Dim PUpdate As String = dt.Rows(0).Item("PUpdate").ToString
            Dim PDelete As String = dt.Rows(0).Item("PDelete").ToString
            If usertype <> "Admin" Then
                PDelete = False
            End If
            If grid.Columns(grid.Columns.Count - 2).HeaderText = "Edit" Then
                grid.Columns(grid.Columns.Count - 2).Visible = PUpdate
            End If
            If grid.Columns(grid.Columns.Count - 1).HeaderText = "Delete" Then
                grid.Columns(grid.Columns.Count - 1).Visible = PDelete
            End If
            If (dt.Rows(0).Item("PAccess").ToString) Then
                Return True
            Else
                page.Response.Redirect("AccessDenied.aspx")
            End If
        Else
            page.Response.Redirect("AccessDenied.aspx")
        End If
    End Function

    Public Shared Function CheckPermisions(ByRef Add As LinkButton, ByRef Update As LinkButton, ByRef Delete As LinkButton, ByRef Print As LinkButton, ByRef Search As TextBox, ByVal page As Page, ByVal UserId As String, ByRef FormName As Label) As String
        Dim pageName As String = PublicFunctions.GetPageName(page.Request.Url.ToString)
        Dim dtLeadsProperty As New TblPermissions
        Dim pf As New PublicFunctions
        'Dim SalesManCode = page.Request.Cookies("UserInfo")("SalesmanCode")
        'pf.GetUserType(SalesManCode)
        Dim usertype = clsGeneralVariables.usertype

        Dim PageOperation As String = PublicFunctions.GetPageOperation(page.Request.Url.ToString)
        Dim dt As New DataTable
        If PageOperation.Contains("Add") Then
            dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select FormId from tblForms where FormName='" + pageName + "' and Operation ='" + PageOperation + "') ")
        Else
            dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select FormId from tblForms where FormName='" + pageName + "' and (Operation is null or Operation ='')) ")
        End If


        'dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select id from tblForms where FormName='" + pageName + "')")
        If dt.Rows.Count <> 0 Then
            Dim dtform As DataTable
            If PageOperation.Contains("Add") Then
                dtform = DBManager.Getdatatable("select FormTitle from tblForms where FormName='" + pageName + "' and Operation ='" + PageOperation + "'")
            Else
                dtform = DBManager.Getdatatable("select FormTitle from tblForms where FormName='" + pageName + "' and (Operation is null or Operation ='')")
            End If

            FormName.Text = dtform.Rows(0).Item("FormTitle").ToString
            Add.Visible = dt.Rows(0).Item("PAdd").ToString
            Update.Visible = dt.Rows(0).Item("PUpdate").ToString

            Delete.Visible = dt.Rows(0).Item("PDelete").ToString
            If usertype <> "Admin" Then
                Delete.Visible = False
            End If
            Print.Visible = False
            Search.Visible = dt.Rows(0).Item("PSearch").ToString
            Dim PUpdate As String = dt.Rows(0).Item("PUpdate").ToString
            Dim PDelete As String = dt.Rows(0).Item("PDelete").ToString
            If usertype <> "Admin" Then
                PDelete = False
            End If

            If (dt.Rows(0).Item("PAccess").ToString) Then
                Return True
            Else
                page.Response.Redirect("AccessDenied.aspx")
            End If
        Else
            page.Response.Redirect("AccessDenied.aspx")
        End If
    End Function

    Public Shared Function CheckPermisionsSearch(ByVal page As Page, ByVal UserId As String) As Boolean
        Try
            Dim pageName As String = PublicFunctions.GetPageName(page.Request.Url.ToString)
            Dim dt As New DataTable
            Dim dtLeadsProperty As New TblPermissions
            dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select id from tblForms where FormName='" + pageName + "')")
            If dt.Rows.Count <> 0 Then
                If (dt.Rows(0).Item("PAccess").ToString) Then
                    Return True
                Else
                    page.Response.Redirect("AccessDenied.aspx")
                End If
            Else
                page.Response.Redirect("AccessDenied.aspx")
            End If
        Catch ex As Exception
            page.Response.Redirect("AccessDenied.aspx")
        End Try
    End Function

    Public Shared Function Login(ByVal UserId As String) As String
        Dim dt As New DataTable
        ' dt = DBManager.Getdatatable("select * from TblUsers where Email ='" + UserName + "' and Password='" + Pass + "'")
        dt = DBManager.Getdatatable("select * from TblUsers where id ='" + UserId + "'")
        If dt.Rows.Count <> 0 Then
            Return dt.Rows(0).Item("Id").ToString
        Else
            Return String.Empty
        End If
    End Function


    Public Shared Function CheckPermisionsNew(ByRef Add As LinkButton, ByRef Update As LinkButton, ByRef Delete As LinkButton, ByVal page As Page, ByVal UserId As String, ByRef FormName As Label, ByRef dynamictable As UserControl) As String
        Dim pageName As String = PublicFunctions.GetPageName(page.Request.Url.ToString)
        'Dim dt As New DataTable
        Dim dtLeadsProperty As New TblPermissions
        Dim pf As New PublicFunctions
        pf.GetUserType(UserId)
        Dim usertype = clsGeneralVariables.usertype
        Dim PageOperation As String = PublicFunctions.GetPageOperation(page.Request.Url.ToString)
        Dim dt As New DataTable
        If PageOperation.Contains("Add") Then
            dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select id from tblForms where FormName='" + pageName + "' and Operation ='" + PageOperation + "') ")
        Else
            dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select id from tblForms where FormName='" + pageName + "' and (Operation is null or Operation ='')) ")
        End If
        'dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select id from tblForms where FormName='" + pageName + "')")
        If dt.Rows.Count <> 0 Then
            'Dim dtform = DBManager.Getdatatable("select FormTitle from tblForms where FormName='" + pageName + "'")
            Dim dtform As DataTable
            If PageOperation.Contains("Add") Then
                dtform = DBManager.Getdatatable("select ArFormTitle from tblForms where FormName='" + pageName + "' and Operation ='" + PageOperation + "'")
            Else
                dtform = DBManager.Getdatatable("select ArFormTitle from tblForms where FormName='" + pageName + "' and (Operation is null or Operation ='')")
            End If
            FormName.Text = dtform.Rows(0).Item("ArFormTitle").ToString
            Add.Visible = dt.Rows(0).Item("PAdd").ToString
            Update.Visible = dt.Rows(0).Item("PUpdate").ToString

            Delete.Visible = dt.Rows(0).Item("PDelete").ToString
            Dim PUpdate As String = dt.Rows(0).Item("PUpdate").ToString
            Dim PDelete As String = dt.Rows(0).Item("PDelete").ToString
            If (dt.Rows(0).Item("PAccess").ToString) Then
                Return True
            Else
                page.Response.Redirect("AccessDenied.aspx")
            End If
        Else
            page.Response.Redirect("AccessDenied.aspx")
        End If
    End Function

    Public Shared Function CheckPermisionsNew1(ByRef Add As LinkButton, ByRef Update As LinkButton, ByRef Delete As LinkButton, ByVal page As Page, ByVal UserId As String, ByRef FormName As Label, ByRef dynamictable As UserControl) As String
        Dim pageName As String = PublicFunctions.GetPageName(page.Request.Url.ToString)
        'Dim dt As New DataTable
        Dim dtLeadsProperty As New TblPermissions
        Dim pf As New PublicFunctions
        pf.GetUserType(UserId)
        Dim usertype = clsGeneralVariables.usertype
        Dim PageOperation As String = PublicFunctions.GetPageOperation(page.Request.Url.ToString)
        Dim dt As New DataTable
        If PageOperation.Contains("Add") Then
            dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select FormId from tblForms where FormName='" + pageName + "' and Operation ='" + PageOperation + "') ")
        Else
            dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select FormId from tblForms where FormName='" + pageName + "' and (Operation is null or Operation ='')) ")
        End If
        'dt = DBManager.Getdatatable("select * from TblPermissions where UserId='" + UserId + "' and FormId in (select id from tblForms where FormName='" + pageName + "')")
        If dt.Rows.Count <> 0 Then
            'Dim dtform = DBManager.Getdatatable("select FormTitle from tblForms where FormName='" + pageName + "'")
            Dim dtform As DataTable
            If PageOperation.Contains("Add") Then
                dtform = DBManager.Getdatatable("select FormTitle from tblForms where FormName='" + pageName + "' and Operation ='" + PageOperation + "'")
            Else
                dtform = DBManager.Getdatatable("select FormTitle from tblForms where FormName='" + pageName + "' and (Operation is null or Operation ='')")
            End If
            FormName.Text = dtform.Rows(0).Item("FormTitle").ToString
            Add.Visible = dt.Rows(0).Item("PAdd").ToString
            Update.Visible = dt.Rows(0).Item("PUpdate").ToString

            Delete.Visible = dt.Rows(0).Item("PDelete").ToString
            Dim PUpdate As String = dt.Rows(0).Item("PUpdate").ToString
            Dim PDelete As String = dt.Rows(0).Item("PDelete").ToString
            If (dt.Rows(0).Item("PAccess").ToString) Then
                Return True
            Else
                page.Response.Redirect("AccessDenied.aspx")
            End If
        Else
            page.Response.Redirect("AccessDenied.aspx")
        End If
    End Function

    Public Shared Function GetUserId(ByVal cookies As Object, ByVal page As Page) As String
        Try
            If HttpContext.Current.Request.Cookies.Get("UserInfo") IsNot Nothing Then
                Dim UserId As String = HttpContext.Current.Request.Cookies("UserInfo")("UserId")
                Dim dt As DataTable = DBManager.Getdatatable("select * from tblUsers where id='" + UserId + "' and ISNull(Active,0)=1")
                If dt.Rows.Count > 0 Then
                    Return UserId
                Else
                    page.Response.Redirect("~/login/login.aspx")
                    Return String.Empty
                End If
            Else
                page.Response.Redirect("~/login/login.aspx")
                Return String.Empty
            End If
        Catch ex As Exception
            If HttpContext.Current.Request.Cookies.Get("UserInfo") IsNot Nothing Then
                If HttpContext.Current.Request.Cookies.Get("UserInfo").Value IsNot Nothing Then
                    Dim RememberMeCookies As New HttpCookie("UserInfo")
                    RememberMeCookies.Expires = DateTime.Now.AddDays(-1D)
                    HttpContext.Current.Request.Cookies.Add(RememberMeCookies)
                End If
            End If
            page.Response.Redirect("~/login/login.aspx")
            Return String.Empty
        End Try
    End Function

    Public Shared Function GetUserCode(ByVal cookies As Object) As String
        Try
            If HttpContext.Current.Request.Cookies.Get("UserInfo") IsNot Nothing Then
                Dim User As String = HttpContext.Current.Request.Cookies("UserInfo")("UserId")
                Return User
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Public Shared Function GetComp_id() As String
        Try
            If HttpContext.Current.Request.Cookies.Get("UserInfo") IsNot Nothing Then
                Dim comp_id As String = HttpContext.Current.Request.Cookies("UserInfo")("comp_id")
                Return comp_id
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Shared Function GetUser__Id() As String
        Try
            If HttpContext.Current.Request.Cookies.Get("UserInfo") IsNot Nothing Then
                Dim UserId As String = HttpContext.Current.Request.Cookies("UserInfo")("UserId")
                Return UserId
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Public Shared Function Getgroup_id() As String
        Try
            If HttpContext.Current.Request.Cookies.Get("UserInfo") IsNot Nothing Then
                Dim group_id As String = HttpContext.Current.Request.Cookies("UserInfo")("group_id")
                Return group_id
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Shared Function isSuperAdmin() As Boolean
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select isNull(superAdmin,0) from tblUsers where id=" + GetUser__Id())
            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Return dt.Rows(0)(0).ToString
                End If

            End If
            Return False

        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function getUserType() As String
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select isNull(User_Type,0) from tblUsers where id=" + GetUser__Id())
            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Return dt.Rows(0)(0).ToString
                End If

            End If
            Return False

        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function getrelatedId() As String
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select isNull(related_id,0) from tblUsers where id=" + GetUser__Id())
            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Return dt.Rows(0)(0).ToString
                End If

            End If
            Return False

        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function ConsultationSuperAdmin() As Boolean
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select * from tblUsers where User_Type=2 and comp_id=" + LoginInfo.GetComp_id() + " and id=" + LoginInfo.GetUser__Id())
            If dt.Rows.Count <> 0 Then
                Return True
            End If
            Return False

        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
