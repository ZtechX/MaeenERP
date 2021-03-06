﻿Imports System.Data
Imports BusinessLayer.BusinessLayer
Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Login_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Login.Click
        Try
            '#####################################Remember me Start   ############################################
            If cklogin.Checked = True Then
                Dim dt As New DataTable
                dt = DBManager.Getdatatable("select * ,isNull(password_changed,0) as 'pass_changed' from tblUsers where (User_PhoneNumber ='" + txtUserName.Value + "' or user_indenty='" + txtUserName.Value + "')  and User_Password = '" + txtPassword.Text + "' and isNULL(Deleted,0)  !=1 ")
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("active") = False Then
                        lblFail.Visible = True
                        lblFail.Text = "This user not Active"
                        lblFail.ForeColor = Drawing.Color.Red
                        Exit Sub
                    End If
                    Dim userCookie As New HttpCookie("UserInfo")
                    userCookie.Expires = DateTime.Now.AddDays(30)
                    userCookie("User") = txtUserName.Value
                    userCookie("UserId") = dt.Rows(0).Item("id")
                    userCookie("User_Type") = dt.Rows(0).Item("User_Type").ToString
                    userCookie("comp_id") = dt.Rows(0).Item("comp_id").ToString
                    userCookie("group_id") = dt.Rows(0).Item("group_id").ToString
                    Session("UserId") = dt.Rows(0).Item("id").ToString
                    Session("User_Type") = dt.Rows(0).Item("User_Type").ToString
                    Session("comp_id") = dt.Rows(0).Item("comp_id").ToString
                    Session("group_id") = dt.Rows(0).Item("group_id").ToString
                    userCookie("date_m") = date_m.Value
                    Session("date_m") = date_m.Value
                    userCookie("date_h") = date_h.Value
                    Session("date_h") = date_h.Value
                    Response.Cookies.Add(userCookie)
                    If dt.Rows(0).Item("User_Type").ToString = "9" Then
                        If Convert.ToBoolean(dt.Rows(0).Item("pass_changed")) Then
                            Response.Redirect("~/main.aspx")
                        Else
                            Response.Redirect("userPorfile.aspx")
                        End If
                    Else
                        Response.Redirect("~/main.aspx")
                    End If
                Else
                    lblFail.Visible = True
                    lblFail.Text = "Login failed, Please try again"
                    lblFail.ForeColor = Drawing.Color.Yellow
                End If
                'End If
                '############################################## Remember Me  end   #######################################
            Else
                Dim dt As New DataTable
                dt = DBManager.Getdatatable("select * ,isNull(password_changed,0) as 'pass_changed' from tblUsers where (User_PhoneNumber ='" + txtUserName.Value + "' or user_indenty='" + txtUserName.Value + "')  and User_Password = '" + txtPassword.Text + "' and isNULL(Deleted,0)  != 1")
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("active") = False Then
                        lblFail.Visible = True
                        lblFail.Text = "This user not Active"
                        lblFail.ForeColor = Drawing.Color.Red
                        Exit Sub
                    End If
                    lblFail.Visible = False
                    Session("UserId") = dt.Rows(0).Item("id").ToString
                    Session("UserType") = dt.Rows(0).Item("User_Type").ToString
                    Session("comp_id") = dt.Rows(0).Item("comp_id").ToString
                    Session("group_id") = dt.Rows(0).Item("group_id").ToString
                    Dim userCookie As New HttpCookie("UserInfo")
                    userCookie("User") = txtUserName.Value
                    userCookie("UserId") = dt.Rows(0).Item("id")
                    Response.Cookies.Add(userCookie)
                    userCookie("UserType") = dt.Rows(0).Item("User_Type").ToString
                    userCookie("comp_id") = dt.Rows(0).Item("comp_id").ToString
                    userCookie("group_id") = dt.Rows(0).Item("group_id").ToString
                    userCookie("date_m") = date_m.Value
                    Session("date_m") = date_m.Value
                    userCookie("date_h") = date_h.Value
                    Session("date_h") = date_h.Value
                    If dt.Rows(0).Item("User_Type").ToString = "9" Then
                        If Convert.ToBoolean(dt.Rows(0).Item("pass_changed")) Then
                            Response.Redirect("~/main.aspx")
                        Else
                            Response.Redirect("userPorfile.aspx")
                        End If
                    Else
                        Response.Redirect("~/main.aspx")
                    End If
                Else
                    lblFail.Visible = True
                    lblFail.Text = "Login failed, please try again"
                    lblFail.ForeColor = Drawing.Color.Yellow
                End If
                'End If
            End If
        Catch ex As Exception
            lblFail.Text = "Fail " + ex.Message.ToString
            lblFail.Visible = True
            lblFail.CssClass = "res-label-error"
        End Try
    End Sub

End Class