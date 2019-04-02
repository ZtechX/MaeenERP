Public Class Notification1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblRes.Visible = False
            UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
            If Not Page.IsPostBack Then
                'If (LoginInfo.CheckPermisions(gvNotification, cmdAdd, cmdUpdate, cmdDelete, cmdPrint, txtSearch, Me.Page, UserId, lblFormName)) Then
                '    cmdPrint.Visible = False
                '    LoadFormfromOutSide(QueryStringModule.CheckqueryString(Page.Request.QueryString))
                'End If
                FillGrid()
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub
#Region "FillGrid"
    ''' <summary>
    ''' Collect condition string to fill gridview
    ''' </summary>
    Public Function CollectCondition() As String
        Dim condition As String = ""
        If txtSearch.Text <> "" Then
            condition += " and (dbo.tblNotifications.NotTitle like '%" + txtSearch.Text + "%'" +
            " or dbo.tblNotifications.RefType like '%" + txtSearch.Text + "%')"
        End If
        Return condition
    End Function

    ''' <summary>
    ''' Fill gridview with data from tblNotifications.
    ''' </summary>
    Private Sub FillGrid()
        Try
            Dim dt As DataTable
            dt = DBManager.Getdatatable("select tblNotifications.ID, RefCode, RefType, NotTitle, Remarks, Date as NotDate, case IsSeen when 1 then 'تم المشاهدة' else 'لم يتم المشاهدة' end as 'IsSeen', AssignedTo, (select Name from tblUsers where id = tblNotifications.CreatedBy) as AssignedBy from tblNotifications " +
                                        "where AssignedTo='" + UserId.ToString + "' and tblNotifications.Status='1' " + CollectCondition() + " order by date desc")
            If dt.Rows.Count > 0 Then
                gvNotification.Visible = True
                ViewState("dtNotification") = dt
                ' Initialize the sorting expression.
                ViewState("SortExpression") = "Id ASC"
                ' Populate the GridView.
                BindGridView()
            Else
                gvNotification.DataSource = Nothing
                gvNotification.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub

    ''' <summary>
    ''' Load notification data from tblNotifications into the gridview.
    ''' </summary>
    Private Sub BindGridView()
        Try
            If ViewState("dtNotification") IsNot Nothing Then
                ' Get the DataTable from ViewState.
                Dim dtStaff As DataTable = DirectCast(ViewState("dtNotification"), DataTable)

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtStaff)

                ' Set the sort column and sort order.
                dv.Sort = ViewState("SortExpression").ToString()

                ' Bind the GridView control.
                gvNotification.DataSource = dv
                gvNotification.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub

    ''' <summary>
    ''' Handle DataBound event of gvNotification.
    ''' </summary>
    Protected Sub gvNotifications_DataBound()
        Try
            'For Each row As TableRow In gvNotification.Rows
            '    If CType(row.FindControl("lblStatus"), Label).Text = "Completed" Then
            '        CType(row.FindControl("lbNotTitle"), HyperLink).Enabled = False
            '        CType(row.FindControl("lbNotTitle"), HyperLink).Attributes.Add("disabled", "disabled")
            '    End If
            '    If CType(row.FindControl("lblDate"), Label).Text <> "" Then
            '        CType(row.FindControl("lblDate"), Label).Text = Convert.ToDateTime(CType(row.FindControl("lblDate"), Label).Text).ToShortDateString()
            '    End If
            'Next
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub
#End Region

#Region "Search"
    ''' <summary>
    ''' filter gvNotification
    ''' </summary>
    Protected Sub Search(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            FillGrid()
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub
#End Region

#Region "Paging"
    ''' <summary>
    ''' Paging function
    ''' </summary>
    Protected Sub GvItems_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            gvNotification.PageIndex = e.NewPageIndex
            BindGridView()
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub

    ''' <summary>
    ''' Set Number of rows at every page
    ''' </summary>
    Protected Sub Txtpager_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            BindGridView()
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub
#End Region

#Region "Sorting"
    ''' <summary>
    ''' Sorting gvNotifications
    ''' </summary>
    Protected Sub Gv_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim strSortExpression As String() = ViewState("SortExpression").ToString().Split(" "c)

            ' If the sorting column is the same as the previous one, then change the sort order.
            If strSortExpression(0) = e.SortExpression Then
                If strSortExpression(1) = "ASC" Then
                    ViewState("SortExpression") = Convert.ToString(e.SortExpression) & " " & "DESC"
                Else
                    ViewState("SortExpression") = Convert.ToString(e.SortExpression) & " " & "ASC"
                End If
            Else
                ' If sorting column is another column, then specify the sort order to "Ascending".
                ViewState("SortExpression") = Convert.ToString(e.SortExpression) & " " & "DESC"
            End If

            ' Rebind the GridView control to show sorted data.
            BindGridView()
        Catch ex As Exception
        End Try
    End Sub
#End Region

End Class