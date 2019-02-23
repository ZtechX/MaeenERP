#Region "Imports"
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports System.Data.OleDb
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Data.SqlTypes
Imports System.Net
Imports System.Net.Mail
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports AjaxControlToolkit
#End Region
Public Class Dashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillMenu()
        Catch ex As Exception
        End Try
    End Sub

#Region "Menu"
    ''' <summary>
    ''' Get Menus, sub menus and forms.
    ''' </summary>
    Sub fillMenu()
        Try
            Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
            Dim dtMenu As DataTable = DBManager.Getdatatable("SELECT  TF.Id as FormID, TF.FormName as FormName, TF.FormTitle as FormTitle, TF.ArFormTitle as ArFormTitle, TF.FormUrl as FormUrl, TF.GroupId as FormGroupId , TF.Icon as FormIcon, TF.OPeration as FormOperation" &
                        " , TM.Id as MenuID, TM.MenuName, TM.ArMenuName as 'MenuNameAr', TM.OrderId as MenuOrderID, TM.Icon as MenuIcon,TM.ParentMenuId as MenuParentId, TM.Report as MenuReport" &
                        " ,TMP.Id as ParentMenuID, TMP.MenuName as ParentMenuName, TMP.ArMenuName as ParentArMenuName, TMP.OrderId as ParentMenuOrderID, TMP.Icon as ParentMenuIcon, TMP.Report as ParentMenuReport" &
                        " FROM  tblforms AS TF" &
                        " left outer join tblMenus AS TM  on(TF.MenueId  = TM.ID )" &
                        " left outer join tblMenus AS TMP on(TM.ParentMenuId =TMP .ID )" &
                            " inner join TblPermissions TP on(TP.FormId =TF.Id )" &
                      " where TP.UserId='" & UserId & "' and TP.PAccess =1 and TF.FormName not in ('LeadSalesProperty','LeadRentProperty','SellingPaymentSchedule','MenuSetup')" &
                                        " order by TMP.OrderId,tm.OrderId,TF.Id")
            Dim dtParentMenu As New DataTable
            Dim dtSubMenu As New DataTable
            Dim dtForms As DataTable

            dtParentMenu = dtMenu.Copy : GetParentMenudatatable(dtParentMenu)
            dtParentMenu = RemoveNullColumnFromDataTable(dtParentMenu)

            dtSubMenu = dtMenu.Copy : GetMenudatatable(dtSubMenu)
            dtSubMenu = RemoveNullColumnFromDataTable(dtSubMenu)

            dtForms = dtMenu.Copy


            fillParentMenus(dtParentMenu, dtSubMenu, dtForms)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Draw Parent Menus.
    ''' </summary>
    Sub fillParentMenus(ByVal dtParentMenu As DataTable, ByVal dtSubMenu As DataTable, ByVal dtForms As DataTable)
        Try
            For Each dr As DataRow In dtParentMenu.Rows

                Dim ParentMenuId As String = dr.Item("MenuId").ToString
                Dim MenuName As String = dr.Item("ARMenuName").ToString
                Dim MenuIcon As String = dr.Item("MenuIcon").ToString

                ''''''''''''''''''''''' V Menu''''''''''''''''''''''
                Dim liMenu As New LiteralControl("<li  class='has-sub hastip' title='" + MenuName + "'><i class='" + MenuIcon + "'></i><span>" + MenuName + "</span> <ul class='" + MenuIcon.Substring(MenuIcon.Length - 1).ToString + "'>")
                'UlMenu.Controls.Add(liMenu)
                ''''''''''''''''''''''' End ''''''''''''''''''''''


                '''''''''''''''''''''' H Menu''''''''''''''''''''''
                'Dim liHMenu As New LiteralControl("<li  class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false' ><i class='" + MenuIcon.Replace("color" + MenuIcon.Substring(MenuIcon.Length - 1), "") + "'></i>" + MenuName + "<span class='caret'></span></a> <ul class='dropdown-menu'>")
                'HlMenu.Controls.Add(liHMenu)
                ''''''''''''''''''''''' End ''''''''''''''''''''''


                fillSubMenus(ParentMenuId, dtSubMenu, dtForms)

                ''''''''''''''''''''''' V Menu''''''''''''''''''''''
                Dim ulSubMenuClose As New LiteralControl("</ul></li>")
                'UlMenu.Controls.Add(ulSubMenuClose)
                ''''''''''''''''''''''' End''''''''''''''''''''''


                ''''''''''''''''''''''' H Menu''''''''''''''''''''''
                'Dim ulHSubMenuClose As New LiteralControl("</ul></li>")
                'HlMenu.Controls.Add(ulHSubMenuClose)
                ''''''''''''''''''''''' End''''''''''''''''''''''
            Next
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Draw Sub Menus.
    ''' </summary>
    Sub fillSubMenus(ByVal ParentMenuId As String, ByVal dtSubMenu As DataTable, ByVal dtForms As DataTable)
        Try
            Dim dvSubMenu As New DataView(dtSubMenu)
            dvSubMenu.RowFilter = "ParentId = '" & ParentMenuId & "'"
            For Each rv As DataRowView In dvSubMenu
                Dim SubMenuName = rv.Item("NameAR").ToString
                Dim SubMenuId = rv.Item("Id").ToString
                Dim dvForms As New DataView(dtForms)
                dvForms.RowFilter = "MenuId = '" & SubMenuId & "'"
                If dvSubMenu.Count > 1 Then
                    If dvForms.Count > 1 Then
                        ''''''''''''''''''''''' V Menu''''''''''''''''''''''
                        Dim liSubMenu As New LiteralControl("<li class='has-sub'><a href='#'><span>" + SubMenuName + "</span></a> <ul>")
                        'UlMenu.Controls.Add(liSubMenu)
                        ''''''''''''''''''''''' End''''''''''''''''''''''

                        ''''''''''''''''''''''' H Menu''''''''''''''''''''''
                        'Dim liHSubMenu As New LiteralControl("<li class='menu-item dropdown dropdown-submenu'><a  href='#' class='dropdown-toggle' data-toggle='dropdown'>" + SubMenuName + "</a> <ul class='dropdown-menu'>")
                        'HlMenu.Controls.Add(liHSubMenu)
                        ''''''''''''''''''''''' End''''''''''''''''''''''


                        fillForms(SubMenuId, dtForms)


                        ''''''''''''''''''''''' V Menu''''''''''''''''''''''
                        Dim liSubMenuClose As New LiteralControl("</ul></li>")
                        'UlMenu.Controls.Add(liSubMenuClose)
                        ''''''''''''''''''''''' End''''''''''''''''''''''

                        ''''''''''''''''''''''' H Menu''''''''''''''''''''''
                        'Dim liHSubMenuClose As New LiteralControl("</ul></li>")
                        'HlMenu.Controls.Add(liHSubMenuClose)
                        ''''''''''''''''''''''' End''''''''''''''''''''''
                    Else
                        fillForms(SubMenuId, dtForms)
                    End If
                Else
                    fillForms(SubMenuId, dtForms)
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' Draw Forms name and its links.
    ''' </summary>
    Sub fillForms(ByVal SubMenuId As String, ByVal dtForms As DataTable)
        Try
            Dim dvForms As New DataView(dtForms)
            dvForms.RowFilter = "MenuId = '" & SubMenuId & "'"
            For Each fv As DataRowView In dvForms
                Dim FormName = fv.Item("ARFormTitle").ToString
                Dim FormId = fv.Item("FormId").ToString
                Dim FormUrl = fv.Item("formUrl").ToString

                'Dim liForm As New LiteralControl("<li><a href='" + FormUrl + "'><span>" + FormName + "</span></a></li>")
                ''''''''''''''''''''''' V Menu''''''''''''''''''''''
                Dim liForm As New LiteralControl("<li>")
                'UlMenu.Controls.Add(liForm)
                Dim lb As New HyperLink
                lb.ID = "FRM" & FormName & FormId
                lb.ClientIDMode = UI.ClientIDMode.Static
                Dim operation As String = ""
                If fv.Item("FormOPeration").ToString <> "" Then
                    operation = fv.Item("FormOPeration")
                End If
                Dim spanName As New LiteralControl("<span>" + FormName + "</span>")
                lb.Controls.Add(spanName)
                lb.NavigateUrl = "~/" + FormUrl
                'UlMenu.Controls.Add(lb)
                Dim liFormClose As New LiteralControl("</li>")
                If SubMenuId = 8 Then
                    communicteUL.Controls.Add(liForm)
                    communicteUL.Controls.Add(lb)
                    communicteUL.Controls.Add(liFormClose)
                End If
                If SubMenuId = 7 Then
                    hallUL.Controls.Add(liForm)
                    hallUL.Controls.Add(lb)
                    hallUL.Controls.Add(liFormClose)
                End If
                If SubMenuId = 1 Then
                    storeUL.Controls.Add(liForm)
                    storeUL.Controls.Add(lb)
                    storeUL.Controls.Add(liFormClose)
                End If
                If SubMenuId = 9 Then
                    custodyUL.Controls.Add(liForm)
                    custodyUL.Controls.Add(lb)
                    custodyUL.Controls.Add(liFormClose)
                End If

                If SubMenuId = 10 Then
                    contactsUL.Controls.Add(liForm)
                    contactsUL.Controls.Add(lb)
                    contactsUL.Controls.Add(liFormClose)
                End If
                If FormId = 15 Then
                    settingsUL.Controls.Add(liForm)
                    settingsUL.Controls.Add(lb)
                    settingsUL.Controls.Add(liFormClose)
                End If
                If FormId = 6 Then
                    usersUL.Controls.Add(liForm)
                    usersUL.Controls.Add(lb)
                    usersUL.Controls.Add(liFormClose)
                End If
                If SubMenuId = 4 Then
                    reportsUL.Controls.Add(liForm)
                    reportsUL.Controls.Add(lb)
                    reportsUL.Controls.Add(liFormClose)
                End If

                ''''''''''''''''''''''' End''''''''''''''''''''''

                ''''''''''''''''''''''' H Menu''''''''''''''''''''''
                'Dim liHForm As New LiteralControl("<li><a href='" + FormUrl + "'>" + FormName + "</a></li>")
                Dim liHForm As New LiteralControl("<li>")
                'HlMenu.Controls.Add(liHForm)
                Dim lbH As New HyperLink
                lbH.ID = "FRMh" & FormName & FormId
                lbH.ClientIDMode = UI.ClientIDMode.Static
                lbH.NavigateUrl = "~/" + FormUrl
                lbH.Text = FormName

                'HlMenu.Controls.Add(lbH)
                Dim liHFormClose As New LiteralControl("</li>")
                'HlMenu.Controls.Add(liHFormClose)
                ''''''''''''''''''''''' End''''''''''''''''''''''


            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Return Datatable of parent menus.
    ''' </summary>
    Private Function GetParentMenudatatable(ByRef dt As DataTable) As DataTable
        For i As Integer = dt.Columns.Count - 1 To 0 Step -1
            If Not dt.Columns(i).ColumnName.StartsWith("Parent") Then
                dt.Columns.RemoveAt(i)
            End If
        Next
        For i As Integer = dt.Columns.Count - 1 To 0 Step -1
            dt.Columns(i).ColumnName = dt.Columns(i).ColumnName.Replace("Parent", "")
        Next
        Dim dv As New DataView(dt)
        dt = dv.ToTable(True)
        Return dt
    End Function
    ''' <summary>
    ''' Return Datatable of sub menus.
    ''' </summary>
    Private Function GetMenudatatable(ByRef dt As DataTable) As DataTable
        For i As Integer = dt.Columns.Count - 1 To 0 Step -1
            If Not dt.Columns(i).ColumnName.StartsWith("Menu") Then
                dt.Columns.RemoveAt(i)
            End If
        Next
        For i As Integer = dt.Columns.Count - 1 To 0 Step -1
            dt.Columns(i).ColumnName = dt.Columns(i).ColumnName.Replace("Menu", "")
        Next
        Dim dv As New DataView(dt)
        dt = dv.ToTable(True)
        Return dt
    End Function
    ''' <summary>
    ''' Return Datatable of without null coulmns.
    ''' </summary>
    Public Shared Function RemoveNullColumnFromDataTable(ByRef dt As DataTable) As DataTable
        For i As Integer = dt.Rows.Count - 1 To 0 Step -1
            If dt.Rows(i)(0).ToString = "" Then
                dt.Rows(i).Delete()
            End If
        Next
        Return dt
    End Function
#End Region
End Class