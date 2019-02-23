Imports System.Data
Imports BusinessLayer.BusinessLayer

Public Class clsFillMenu

    Private dtDataSource As DataTable
    Private dvDataSource As DataView

    Private dtMainMenu As DataTable
    Private dtMenu As DataTable
    Private dtForms As DataTable

    Private _mmDet As MenuDetails
    Private _mDet As MenuDetails
    Private _FDet As MenuDetails

    Dim liParentMenu As New LiteralControl
    Dim liMenu As New LiteralControl
    Dim liForm As New LiteralControl


    Structure MenuDetails

        Dim Id As Integer
        Dim MenuName As String
        Dim ArMenuName As String
        Dim Icon As String
        Dim OPeration As String
        Dim RefName As String
        Dim IsReport As Boolean
        Dim ParentMenuId As Integer
        Dim Url As String
        Dim GroupId As Integer
    End Structure

    Enum ReturnMenu

        MainMenu = 0
        Menu = 1
        Form = 2

    End Enum

    Sub New()

    End Sub

    Sub New(ByVal userid As String)
        SetDataSource(userid)
    End Sub

    Public Sub SetDataSource(ByVal UserId As Integer)

        dtDataSource = DBManager.Getdatatable(GetQuery(UserId))
        dvDataSource = New DataView(dtDataSource)

    End Sub

    Public Sub FillMenus(ByRef UIMenu As Object)
        Try
            dtMainMenu = GetMenuSource(ReturnMenu.MainMenu, 0)
            For Each dr As DataRow In dtMainMenu.Rows
                _mmDet.Id = dr.Item("Id")
                _mmDet.MenuName = dr.Item("MenuName").ToString
                _mmDet.Icon = dr.Item("Icon").ToString

                AddParentLiteralControl(_mmDet, UIMenu)

                FillSubMenus(_mmDet.Id, UIMenu)

                AddCloseParentLiteralControl(UIMenu)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillSubMenus(ByVal MenuId As Integer, ByRef UIMenu As Object)
        Try
            dtMenu = GetMenuSource(ReturnMenu.Menu, MenuId)
            For Each dr As DataRow In dtMenu.Rows
                _mDet.Id = dr.Item("Id")
                _mDet.MenuName = dr.Item("MenuName").ToString
                _mDet.Icon = dr.Item("Icon").ToString


                AddMenuLiteralControl(_mDet, UIMenu)

                FillForms(_mDet.Id, UIMenu)

                AddCloseMenuLiteralControl(UIMenu)
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FillForms(ByVal MenuId As String, ByRef UIMenu As Object)
        Try
            dtForms = GetMenuSource(ReturnMenu.Form, MenuId)

            For Each dr As DataRow In dtForms.Rows

                _FDet.Id = dr.Item("Id")
                _FDet.MenuName = dr.Item("MenuName").ToString
                _FDet.Icon = dr.Item("Icon").ToString
                _FDet.Url = dr.Item("Url").ToString

                AddFormLiteralControl(_FDet, UIMenu)

                AddCloseFormLiteralControl(UIMenu)
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Function GetQuery(ByVal UserId As Integer)

        Return "SELECT     Id, MenuName, ArMenuName, Icon,  ParentMenuId,'' as Url,-1 as GroupId,'' as OPeration,'' as RefName,Report as IsReport,orderid as OID ,0 as IsParent" & _
               " FROM         tblMenus where ParentMenuId =0" & _
               " union ALL" & _
               " SELECT     Id, MenuName, ArMenuName, Icon,  ParentMenuId,'' as Url,-1 as GroupId,'' as OPeration,'' as RefName,Report as IsReport ,orderid as OID ,1 as IsParent" & _
               " FROM         tblMenus where ParentMenuId <>0" & _
               " union ALL" & _
               " SELECT     Id,FormTitle as MenuName, ArFormTitle as ArMenuName, Icon ,MenueId,FormUrl as URL, GroupId,  OPeration ,FormName as RefName,0 as IsReport ,1000 as OID ,2 as IsParent" & _
               " FROM tblForms " & _
               " where FormName not in ('frmLeadSalesProperty','frmLeadRentProperty','frmSellingPaymentSchedule')" & _
               " and ID in(select FormId from TblPermissions where PAccess =1 and UserId=" & UserId & ")" & _
               " order by IsParent,OID,ID"

    End Function

    Private Function GetMenuSource(ByVal RType As ReturnMenu, ByVal ParentId As Integer) As DataTable

        Select Case RType

            Case ReturnMenu.MainMenu
                dvDataSource.RowFilter = "IsParent=0 and ParentMenuId =" & ParentId & ""
            Case ReturnMenu.Menu
                dvDataSource.RowFilter = "IsParent=1 and ParentMenuId =" & ParentId & ""
            Case ReturnMenu.Form
                dvDataSource.RowFilter = "IsParent=2 and ParentMenuId =" & ParentId & ""
        End Select

        Return dvDataSource.ToTable

    End Function

    Private Sub AddParentLiteralControl(ByVal Det As MenuDetails, ByRef UlMenu As Object)

        liParentMenu = New LiteralControl("<li  class='has-sub hastip' title='" + Det.MenuName + "'>" & _
                                    "<a href='#'><i class='" + Det.Icon + "'></i>" & _
                                    "<span>" + Det.MenuName + "</span></a> " & _
                                    "<ul class='background0" + Det.Icon.Substring(Det.Icon.Length - 1).ToString + "'>")
        UlMenu.Controls.Add(liParentMenu)
    End Sub
    Private Sub AddCloseParentLiteralControl(ByRef UlMenu As Object)
        UlMenu.Controls.Add(New LiteralControl("</li></ul>"))
    End Sub

    Private Sub AddMenuLiteralControl(ByVal Det As MenuDetails, ByRef UlMenu As Object)

        liMenu = New LiteralControl("<li class='has-sub'><a href='#'><span>" + Det.MenuName + "</span></a> <ul>")
        UlMenu.Controls.Add(liMenu)
    End Sub
    Private Sub AddCloseMenuLiteralControl(ByRef UlMenu As Object)
        UlMenu.Controls.Add(New LiteralControl("</ul></li>"))
    End Sub

    Private Sub AddFormLiteralControl(ByVal Det As MenuDetails, ByRef UlMenu As Object)
        liForm = New LiteralControl("<li>")
        UlMenu.Controls.Add(liForm)

        Dim lb As New HyperLink
        lb.ID = "FRM" & Det.Url & Det.Id
        lb.ClientIDMode = UI.ClientIDMode.Static

        Dim spanName As New LiteralControl("<span>" + Det.MenuName + "</span>")
        lb.Controls.Add(spanName)
        lb.Attributes.Add("onclick", "SetFrame('" & Det.Url & "',this);return false;")

        If Det.OPeration = "Add" Then
            lb.Attributes.Add("onclick", "SetFrame('" & Det.Url & "?Operation=Add" & "',this);return false;")
        End If
        If Det.OPeration = "Addc" Then
            lb.Attributes.Add("onclick", "SetFrame('" & Det.Url & "?Operation=Add&type=Client" & "',this);return false;")
        End If
        If Det.OPeration = "Addo" Then
            lb.Attributes.Add("onclick", "SetFrame('" & Det.Url & "?Operation=Add&type=Owner" & "',this);return false;")
        End If

        UlMenu.Controls.Add(lb)
    End Sub
    Private Sub AddCloseFormLiteralControl(ByRef UlMenu As Object)
        UlMenu.Controls.Add(New LiteralControl("</li>"))
    End Sub

    Public Sub ChangeForm(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        '    If txtSearch.Text <> "" Then
        '        Dim dtForms As DataTable = DBManager.Getdatatable("select * from tblForms where FormTitle ='" + txtSearch.Text + "'")
        '        If dtForms.Rows.Count > 0 Then
        '            Dim FormUrl As String = dtForms.Rows(0).Item("FormUrl").ToString
        '            Dim operation As String = dtForms.Rows(0).Item("operation").ToString
        '            myFrame.Attributes.Add("src", FormUrl)
        '            If operation = "Add" Then
        '                myFrame.Attributes.Add("src", FormUrl & "?Operation=Add")
        '            End If
        '            If operation = "Addc" Then
        '                myFrame.Attributes.Add("src", FormUrl & "?Operation=Add&type=Client")
        '            End If
        '            If operation = "Addo" Then
        '                myFrame.Attributes.Add("src", FormUrl & "?Operation=Add&type=Owner")
        '            End If
        '        End If
        '    Else
        '        myFrame.Attributes.Add("src", "Dashboard.aspx")
        '    End If

        'Catch ex As Exception

        'End Try

    End Sub


End Class
