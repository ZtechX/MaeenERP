Imports Microsoft.VisualBasic

Public Class clsMessages

    Shared Sub ShowErrorMessgage(ByRef lblRes As Label, ByVal message As String, ByVal page As Page)
        Try
            ShowMessgage(lblRes, message, "res-label-error", page)
        Catch ex As Exception
        End Try
    End Sub

    Shared Sub ShowAlertMessgage(ByRef lblRes As Label, ByVal message As String, ByVal page As Page)
        Try
            ShowMessgage(lblRes, message, "res-label-info", page)
        Catch ex As Exception
        End Try
    End Sub

    Shared Sub ShowSuccessMessgage(ByRef lblRes As Label, ByVal message As String, ByVal page As Page)
        Try
            ShowMessgage(lblRes, message, "res-label-success", page)
        Catch ex As Exception
        End Try
    End Sub

    Shared Sub ShowInfoMessgage(ByRef lblRes As Label, ByVal message As String, ByVal page As Page)
        Try
            ShowMessgage(lblRes, message, "res-label-info", page)
        Catch ex As Exception
        End Try
    End Sub

    Private Shared Sub ShowMessgage(ByRef lblRes As Label, ByVal message As String, ByVal cssClass As String, ByVal page As Page)
        Try
            lblRes.Text = message.ToString
            lblRes.Visible = True
            lblRes.CssClass = cssClass
            If cssClass <> "res-label-error" Then
                If cssClass = "res-label-info" Then
                    ScriptManager.RegisterClientScriptBlock(page, page.[GetType](), "", "$('.res-label-success').fadeIn(3000);", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(page, page.[GetType](), "", "$('.res-label-success').fadeIn(3000);$('.res-label-success').fadeOut(3000);", True)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class