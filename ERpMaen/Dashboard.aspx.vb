﻿#Region "Imports"
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
    Dim group_id As String = ""
    Dim UserId = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            group_id = LoginInfo.Getgroup_id()
            UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select * from TblUsers where Id ='" + UserId + "'")
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    lblphone.Text = dt.Rows(0).Item("User_PhoneNumber").ToString
                    lblUserName.Text = dt.Rows(0).Item("full_name").ToString
                    imgUser1.ImageUrl = dt.Rows(0).Item("User_Image").ToString
                End If
            End If
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

            Dim dtMenu As DataTable = DBManager.Getdatatable("SELECT  TF.Id as FormID, TF.FormName as FormName, TF.FormTitle as FormTitle, TF.ArFormTitle as ArFormTitle, TF.FormUrl as FormUrl, TF.GroupId as FormGroupId , TF.Icon as FormIcon, TF.OPeration as FormOperation" &
                        " , TM.Id as MenuID, TM.MenuName, TM.ArMenuName as 'MenuNameAr', TM.OrderId as MenuOrderID, TM.Icon as MenuIcon,TM.ParentMenuId as MenuParentId, TM.Report as MenuReport" &
                        " ,TMP.Id as ParentMenuID, TMP.MenuName as ParentMenuName, TMP.ArMenuName as ParentArMenuName, TMP.OrderId as ParentMenuOrderID, TMP.Icon as ParentMenuIcon, TMP.Report as ParentMenuReport" &
                        " FROM  tblforms AS TF" &
                        " left outer join tblMenus AS TM  on(TF.MenueId  = TM.ID )" &
                        " left outer join tblMenus AS TMP on(TM.ParentMenuId =TMP .ID )" &
                            " inner join tblgroup_permissons TP on(TP.form_id =TF.Id )" &
                      " where TP.group_id='" & group_id & "' and TP.f_access =1 and TF.FormName not in ('LeadSalesProperty','LeadRentProperty','SellingPaymentSchedule','MenuSetup')" &
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
                If SubMenuId = 8 Then
                    lb.Target = "_blank"
                End If
                'UlMenu.Controls.Add(lb)
                Dim liFormClose As New LiteralControl("</li>")
                If SubMenuId = 1 Then
                    settingsUL.Controls.Add(liForm)
                    settingsUL.Controls.Add(lb)
                    settingsUL.Controls.Add(liFormClose)
                ElseIf SubMenuId = 3 Then
                    bordsUL.Controls.Add(liForm)
                    bordsUL.Controls.Add(lb)
                    bordsUL.Controls.Add(liFormClose)
                ElseIf SubMenuId = 6 Then
                    FamilyConcUL.Controls.Add(liForm)
                    FamilyConcUL.Controls.Add(lb)
                    FamilyConcUL.Controls.Add(liFormClose)
                ElseIf SubMenuId = 4 Then
                    SMSUL.Controls.Add(liForm)
                    SMSUL.Controls.Add(lb)
                    SMSUL.Controls.Add(liFormClose)
                ElseIf SubMenuId = 7 Then
                    AcadymicUL.Controls.Add(liForm)
                    AcadymicUL.Controls.Add(lb)
                    AcadymicUL.Controls.Add(liFormClose)
                ElseIf SubMenuId = 8 Then
                    ReportUL.Controls.Add(liForm)
                    ReportUL.Controls.Add(lb)
                    ReportUL.Controls.Add(liFormClose)
                ElseIf SubMenuId = 5 Then
                    ManagementCallsUl.Controls.Add(liForm)
                    ManagementCallsUl.Controls.Add(lb)
                    ManagementCallsUl.Controls.Add(liFormClose)
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
                If SubMenuId = 8 Then
                    lbH.Target = "_blank"
                End If

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