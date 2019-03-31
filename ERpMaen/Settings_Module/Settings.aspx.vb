#Region "Signature"
'################################### Signature ####################################
'############# Version: v1.0
'############# Start Date:
'############# Form Name: Contacts 
'############# Your Name:  Ahmed Nayl
'############# Create date and time 21-01-2019 10:00 AM
'############# Form Description:  
'################################ End of Signature ################################
#End Region

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
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports AjaxControlToolkit
Imports Newtonsoft.Json
#End Region


Public Class Settings
    Inherits System.Web.UI.Page

#Region "Global_Varaibles"
    Dim pf As New PublicFunctions
    Dim query As New ExecuteQuery
    Dim SalesManCode As String
    Dim UserId As String
    Dim compId As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblRes.Visible = False
        Dim da As New Tbllock_upFactory
        UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
        compId = LoginInfo.GetComp_id()
        If Page.IsPostBack = False Then
            Try
                FillGrid_DataTypes()
            Catch ex As Exception
                clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
            End Try
        End If
    End Sub




#Region "FillGrid"
    Private Sub FillGrid_DataTypes()
        Dim condition As String = " where isNull(editable,1) =1 "
        If txtSearchAll.Text <> "" Then
            If txtSearchAll.Text.Split("|").Length = 2 Then
                condition = " and tbllock_up.Type like '%" + txtSearchAll.Text.Split("|")(0) + "%' and name like '%" + txtSearchAll.Text.Split("|")(1) + "%'"
            Else
                condition = " and name like '%" + txtSearchAll.Text + "%'"
            End If
        End If

        Dim dtDataTypes As DataTable = DBManager.Getdatatable("Select distinct(tbllockup_settinges.Type) as Type , name as Name from tbllock_up right join tbllockup_settinges on tbllock_up.Type Collate DATABASE_DEFAULT= tbllockup_settinges.Type Collate DATABASE_DEFAULT " + condition + " order by Name")

        If dtDataTypes.Rows.Count > 0 Then
            ViewState("dtDataTypes") = dtDataTypes
            ViewState("DataTypesSortExp") = "Type DESC"
            BindGrid_DataTypes()
            gvValues.Visible = True
        Else
            GVDataType.DataSource = Nothing
            GVDataType.DataBind()
            GVDataType.Visible = True
            gvValues.Visible = False
            lblValueRes.Visible = True
        End If
    End Sub

    Private Sub BindGrid_DataTypes()
        Try
            If ViewState("dtDataTypes") IsNot Nothing Then
                ' Get the DataTable from ViewState.
                Dim dtDataTypes As DataTable = DirectCast(ViewState("dtDataTypes"), DataTable)

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtDataTypes)

                ' Set the sort column and sort order.
                dv.Sort = ViewState("DataTypesSortExp").ToString()

                ' Bind the GridView control.
                GVDataType.DataSource = dv
                GVDataType.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub

    Protected Sub gv_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvValues.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Color As String = CType(e.Row.FindControl("lblColor"), Label).Text
            If Color <> vbNullString Then
                CType(e.Row.FindControl("pnlColor"), Panel).BackColor = System.Drawing.ColorTranslator.FromHtml("#" + Color)
            End If
        End If
    End Sub

    Private Sub FillGrid_Values()
        Dim condition = ""
        If Not isSuperAdmin() Then
            condition = "  comp_id=" + compId + "and "
        End If
        Dim dtValues As DataTable
        If ddlTypes.Visible = True Then
            If ddlTypes.Items.Count <> 0 Then
                dtValues = DBManager.Getdatatable("Select * from tbllock_up where " + condition + " Type='" + lblType.Text + "'")
            Else
                dtValues = DBManager.Getdatatable("Select * from tbllock_up where " + condition + " Type='" + lblType.Text + "' and RelatedID='" + ddlTypes.SelectedValue + "'")
            End If
        Else
            dtValues = DBManager.Getdatatable("Select * from tbllock_up where " + condition + " Type='" + lblType.Text + "'")
        End If

        If dtValues.Rows.Count > 0 Then
            Dim i As Integer = 0
            For Each r As DataRow In dtValues.Rows
                If r.Item("Icon").ToString = vbNullString Then
                    dtValues.Rows(i).Item("Icon") = "App_Themes/images/add-icon.jpg"
                End If
                i += 1
            Next

            ViewState("dtValues") = dtValues
            ViewState("ValuesSortExp") = "OrderNo ASC"
            BindGrid_Values()
            gvValues.Visible = True
            lblValueRes.Visible = False
        Else
            gvValues.DataSource = Nothing
            gvValues.DataBind()
            'gvValues.Visible = False
            lblValueRes.Visible = True
        End If
    End Sub

    Private Sub BindGrid_Values()
        Try
            If ViewState("dtValues") IsNot Nothing Then
                ' Get the DataTable from ViewState.
                Dim dtValues As DataTable = DirectCast(ViewState("dtValues"), DataTable)

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtValues)

                ' Set the sort column and sort order.
                dv.Sort = ViewState("ValuesSortExp").ToString()

                ' Bind the GridView control.
                gvValues.DataSource = dv
                gvValues.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub
#End Region

    Protected Sub NewType(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            Dim condition = ""
            If Not isSuperAdmin() Then
                condition = " and comp_id=" + compId
            End If
            LookupId.Text = ""
            lblRes.Visible = False
            lbNewType.Visible = False
            pnlNewType.Visible = True
            pnlTypes.Visible = False

            pf.ClearAll(pnlNewType)
            txtOrderNo.Text = PublicFunctions.GetMaxLockup(lblType.Text)
            FillGrid_DataTypes()
            If lblType.Text = "VILL" Or lblType.Text = "BIO" Then
                pnlRType.Visible = False
                pnlRType2.Visible = True
                Dim dtValues = DBManager.Getdatatable("Select * from tbllock_up where Type='CTY'" + condition)
                If dtValues.Rows.Count > 0 Then
                    ddlRType1.Items.Clear()
                    ddlRType1.DataSource = dtValues
                    ddlRType1.DataTextField = "Description"
                    ddlRType1.DataValueField = "Id"
                    ddlRType1.DataBind()
                End If
                dtValues = DBManager.Getdatatable("Select * from tbllock_up where Type='CEN'" + condition)
                If dtValues.Rows.Count > 0 Then
                    ddlRType2.Items.Clear()
                    ddlRType2.DataSource = dtValues
                    ddlRType2.DataTextField = "Description"
                    ddlRType2.DataValueField = "Id"
                    ddlRType2.DataBind()
                End If
            Else
                pnlRType2.Visible = False

                Dim dtRType = DBManager.Getdatatable("Select * from tbllockup_settinges where Type='" + lblType.Text + "' and RelatedType is not null")
                If dtRType.Rows.Count > 0 Then
                    pnlRType.Visible = True
                    Dim RType As String = dtRType.Rows(0)("RelatedType").ToString()
                    Dim dtValues = DBManager.Getdatatable("Select * from tbllock_up where Type='" + RType + "'" + condition)
                    If dtValues.Rows.Count > 0 Then
                        ddlRType.Items.Clear()
                        ddlRType.DataSource = dtValues
                        ddlRType.DataTextField = "Description"
                        ddlRType.DataValueField = "Id"
                        ddlRType.DataBind()
                        ddlRType.SelectedValue = ddlTypes.SelectedValue
                    End If
                Else
                    pnlRType.Visible = False
                End If
            End If


        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub

    Protected Sub CancelType(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            LookupId.Text = ""
            lblRes.Visible = False
            lbNewType.Visible = True
            pnlNewType.Visible = False
            pnlTypes.Visible = True
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub

    Protected Sub ShowType(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim TypeEng As String
        If Sender.ID = "txtSearchAll" Then
            TypeEng = Sender.Text.ToString.Split("|")(0)
        Else
            TypeEng = Sender.commandargument.ToString.Replace(" ", "")
        End If
        Dim da As New Tbllock_upFactory
        pnlTypes.Visible = False
        pnlNewType.Visible = False
        pnlOps.Visible = True
        pnlValue.Visible = True
        lbNewType.Visible = True
        Dim condition = ""
        If Not isSuperAdmin() Then
            condition = " and comp_id=" + compId
        End If
        Try
            LookupId.Text = ""
            lblRes.Visible = False
            lblType.Text = TypeEng
            lblTypesName.Text = Sender.CommandName.ToString
            Dim dt As DataTable = DBManager.Getdatatable("Select * from tbllock_up inner join tbllockup_settinges on tbllock_up.Type Collate DATABASE_DEFAULT= tbllockup_settinges.Type Collate DATABASE_DEFAULT  where tbllock_up.Type='" + TypeEng + "'" + condition + " order by orderNo,Name ")

            Dim dtRType = DBManager.Getdatatable("Select * from tbllockup_settinges where Type='" + lblType.Text + "' and RelatedType is not null")
            If dtRType.Rows.Count > 0 Then
                pnlTypes.Visible = True
                Dim RType As String = dtRType.Rows(0)("RelatedType").ToString()
                ddlTypes.Visible = True
                Dim dtValues = DBManager.Getdatatable("Select * from tbllock_up where Type='" + RType + "'" + condition)
                If dtValues.Rows.Count > 0 Then
                    ddlTypes.AppendDataBoundItems = False
                    ddlRType.Items.Clear()
                    ddlTypes.DataSource = dtValues
                    ddlTypes.DataTextField = "Description"
                    ddlTypes.DataValueField = "Id"
                    ddlTypes.DataBind()
                    FillGrid_Values()
                End If
            Else
                FillGrid_Values()
                pnlTypes.Visible = True
                ddlTypes.Visible = False
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub

    Protected Sub DeleteValue(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim Id As String = Sender.commandargument.ToString
        LookupId.Text = Id
        Try
            If LookupId.Text = 18 Then
                clsMessages.ShowSuccessMessgage(lblRes, "Record can't deleted !", Me)
            Else
                DBManager.ExcuteQuery("delete from tbllock_up where id= '" + LookupId.Text + "'")
                FillGrid_Values()
                clsMessages.ShowSuccessMessgage(lblRes, "Record has been deleted successfully!", Me)
            End If

        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
        LookupId.Text = ""
    End Sub

    Protected Sub UpdateValue(ByVal Sender As Object, ByVal e As System.EventArgs)

        Dim ID As String = Sender.commandargument
        LookupId.Text = ID.ToString
        Try
            If LookupId.Text = 18 Then
                clsMessages.ShowSuccessMessgage(lblRes, "Record can't edite !", Me)
            Else

                Dim dt As New DataTable
                dt = DBManager.Getdatatable("SELECT * From tbllock_up where  Id ='" + LookupId.Text + "'")
                If dt.Rows.Count <> 0 Then
                    txtDescription.Value = dt.Rows(0).Item("Description").ToString.Replace(" ", "'")
                    If dt.Rows(0).Item("OrderNo").ToString <> vbNullString Then
                        txtOrderNo.Text = dt.Rows(0).Item("OrderNo").ToString
                    Else
                        txtOrderNo.Text = PublicFunctions.GetMaxLockup(lblType.Text)
                    End If
                    If dt.Rows(0).Item("Icon").ToString <> vbNullString Then
                        Dim Icon = dt.Rows(0).Item("Icon").ToString
                        Dim str As String = "Admin_Module/"
                        Dim count = str.Length
                        Icon = Icon.Remove(0, count)
                        'imgItemURL.ImageUrl = Icon
                    End If
                    pnlNewType.Visible = True
                    Dim condition = ""
                    If Not isSuperAdmin() Then
                        condition = " and comp_id=" + compId
                    End If
                    If lblType.Text = "VILL" Or lblType.Text = "BIO" Then
                        pnlRType.Visible = False
                        pnlRType2.Visible = True
                        Dim dtValues = DBManager.Getdatatable("Select * from tbllock_up where Type='CTY'" + condition)
                        If dtValues.Rows.Count > 0 Then
                            ddlRType1.Items.Clear()
                            ddlRType1.DataSource = dtValues
                            ddlRType1.DataTextField = "Description"
                            ddlRType1.DataValueField = "Id"
                            ddlRType1.DataBind()
                        End If
                        dtValues = DBManager.Getdatatable("Select * from tbllock_up where Type='CEN'" + condition)
                        If dtValues.Rows.Count > 0 Then
                            ddlRType2.Items.Clear()
                            ddlRType2.DataSource = dtValues
                            ddlRType2.DataTextField = "Description"
                            ddlRType2.DataValueField = "Id"
                            ddlRType2.DataBind()
                        End If
                        dtValues = DBManager.Getdatatable("Select type from tbllock_up where id='" + dt.Rows(0).Item("RelatedID").ToString + "'")
                        If dtValues.Rows.Count <> 0 Then
                            Dim type_val = dtValues.Rows(0)(0).ToString
                            rbrelateTo.SelectedValue = type_val
                            rbrelateTo_SelectedIndexChanged(Sender, e)
                            If type_val = "CTY" Then
                                ddlRType1.SelectedValue = dt.Rows(0).Item("RelatedID").ToString
                            Else
                                ddlRType2.SelectedValue = dt.Rows(0).Item("RelatedID").ToString
                            End If

                        End If
                    Else
                        pnlRType2.Visible = False

                        Dim dtRType = DBManager.Getdatatable("Select * from tbllockup_settinges where Type='" + lblType.Text + "' and RelatedType is not null")
                        If dtRType.Rows.Count > 0 Then
                            pnlRType.Visible = True
                            Dim RType As String = dtRType.Rows(0)("RelatedType").ToString()
                            Dim dtValues = DBManager.Getdatatable("Select * from tbllock_up where Type='" + RType + "'" + condition)
                            If dtValues.Rows.Count > 0 Then
                                ddlRType.Items.Clear()
                                ddlRType.DataSource = dtValues
                                ddlRType.DataTextField = "Description"
                                ddlRType.DataValueField = "Id"
                                ddlRType.DataBind()
                                ddlRType.SelectedValue = dt.Rows(0).Item("RelatedID").ToString
                            End If
                        Else
                            pnlRType.Visible = False
                        End If
                    End If



                End If
                pnlTypes.Visible = False
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub

    Protected Sub SaveType(ByVal Sender As Object, ByVal e As System.EventArgs)
        txtDescription.Attributes("class").Replace("error", " ")
        Dim desc = txtDescription.Value

        If Not String.IsNullOrEmpty(desc) And Not String.IsNullOrWhiteSpace(desc) Then

            Dim daLookup As New Tbllock_upFactory
            Dim dtLookup As New Tbllock_up
            Dim dtType As DataTable = New DataTable()
            Dim Photo As String = ""
            Dim Color As String = ""
            If Not Session("UserPhoto") Is Nothing Then
                Photo = Session("UserPhoto").ToString
            End If
            'Color = txtColor.Text
            Try
                dtLookup.Comp_id = compId
                dtLookup.Description = txtDescription.Value.Replace("'", " ")
                Dim order As Integer
                If Integer.TryParse(txtOrderNo.Text, order) Then
                    dtLookup.OrderNo = order
                End If

                'dtLookup.OrderNo = txtOrderNo.Text
                '   dtLookup.ADescription = txtDescriptionA.Text.Replace("'", " ")
                dtLookup.Type = lblType.Text
                If pnlRType.Visible = True Then
                    '    dtLookup.RelatedId = ddlRType.SelectedValue
                    Dim dtrelaedtype = DBManager.Getdatatable("select type from tbllock_up where id='" + ddlRType.SelectedValue + "'")
                    If dtrelaedtype.Rows.Count <> 0 Then
                        '     dtLookup.RelatedType = dtrelaedtype.Rows(0).Item(0).ToString
                        dtLookup.RelatedId = ddlRType.SelectedValue
                    Else
                        clsMessages.ShowErrorMessgage(lblRes, "Select Valid Type From DropDown List", Me)
                        Exit Sub
                    End If
                End If
                If pnlRType2.Visible = True Then
                    If rbrelateTo.SelectedValue = "CTY" Then
                        If ddlRType1.SelectedIndex = -1 Then
                            clsMessages.ShowErrorMessgage(lblRes, "من فضلك أختر المحافظة", Me)
                            Exit Sub
                        Else
                            dtLookup.RelatedId = ddlRType1.SelectedValue
                        End If
                    Else
                        If ddlRType2.SelectedIndex = -1 Then
                            clsMessages.ShowErrorMessgage(lblRes, "من فضلك أختر المركز", Me)
                            Exit Sub
                        Else
                            dtLookup.RelatedId = ddlRType2.SelectedValue
                        End If
                    End If
                End If
                Dim condition = ""
                If Not isSuperAdmin() Then
                    condition = " and comp_id=" + compId
                End If
                If LookupId.Text = String.Empty Then
                    dtType = DBManager.Getdatatable("select * from tbllock_up where Description ='" + txtDescription.Value.Replace("'", " ") + "'and Type ='" + lblType.Text + "' and RelatedId='" + ddlRType.SelectedValue + "' " + condition)
                    If dtType.Rows.Count >= 1 Then
                        clsMessages.ShowErrorMessgage(lblRes, "This Description Used. Try another", Me)
                        Exit Sub
                    End If

                    Dim dtb As New Tbllockup_settinges

                    Dim temp As New DataTable
                    temp = DBManager.Getdatatable("Select Max(CONVERT(int,ID)) from tbllock_up")
                    Dim lockupId As String = temp.Rows(0).Item(0) + 1
                    dtLookup.Id = lockupId
                    daLookup.Insert(dtLookup)
                    pf.ClearAll(pnlNewType)
                    clsMessages.ShowSuccessMessgage(lblRes, "Record has been added successfully!", Me)
                    pnlNewType.Visible = False
                    pnlTypes.Visible = True
                    FillGrid_Values()
                    lbNewType.Visible = True
                Else
                    dtType = DBManager.Getdatatable("select * from tbllock_up where Description ='" + txtDescription.Value.Replace("'", " ") + "'and Type ='" + lblType.Text + "' and ID !='" + LookupId.Text + "' and RelatedId='" + ddlRType.SelectedValue + "'" + condition)
                    If dtType.Rows.Count >= 1 Then
                        clsMessages.ShowErrorMessgage(lblRes, "This Description Used. Try another", Me)
                        Exit Sub
                    End If
                    Dim Id As String = LookupId.Text
                    'Dim UserKey As New TblUsersKeys(Id)
                    If Id <> 0 Then
                        dtLookup.Id = Id
                        dtLookup.Color = Color
                        dtLookup.ICON = Photo
                        ' dtLookup.OrderNo = txtOrderNo.Text

                        If Integer.TryParse(txtOrderNo.Text, order) Then
                            dtLookup.OrderNo = order
                        End If
                        daLookup.Update(dtLookup)
                        pf.ClearAll(pnlNewType)
                        clsMessages.ShowSuccessMessgage(lblRes, "Record has been updated successfully!", Me)
                        LookupId.Text = String.Empty
                        pnlNewType.Visible = False
                        pnlTypes.Visible = True
                        FillGrid_Values()
                        lbNewType.Visible = True
                    Else
                        clsMessages.ShowErrorMessgage(lblRes, "Enter a valid Item to update", Me)
                        Exit Sub
                    End If
                End If
                Session("UserPhoto") = Nothing
            Catch ex As Exception
                clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
                Session("UserPhoto") = Nothing
            End Try
            LookupId.Text = ""
        Else
            txtDescription.Attributes("class") += " error"


        End If

    End Sub

    Protected Sub ddlTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlTypes.SelectedIndexChanged
        FillGrid_Values()
    End Sub


#Region "Upload_Icon"
    Protected Sub PhotoUploaded(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim x As String = ""
        'Dim rnd As New Random
        'Dim namer As String = ""
        'Dim i As Integer = 0
        'Dim Path As String = ""
        'Dim fu As New AjaxControlToolkit.AsyncFileUpload
        'Try
        '    i = rnd.Next(10000, 99999)
        '    '  namer = i.ToString
        '    Select Case sender.id.ToString
        '        Case "fuPhoto1"
        '            fu = fuPhoto1
        '            Path = "Admin_Module/Settings_Icons/"
        '            Prepare_Sheet(fu)
        '            Dim PostedPhoto As System.Drawing.Image = System.Drawing.Image.FromStream(fu.PostedFile.InputStream)
        '            Dim ImgHeight As Integer = PostedPhoto.Height
        '            Dim ImgWidth As Integer = PostedPhoto.Width
        '            x = CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, ImgWidth, ImgHeight, ImgWidth, ImgHeight, "Employees", namer)
        '            Session("UserPhoto") = x
        '    End Select
        'Catch ex As Exception
        '    clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        'End Try
    End Sub

    Private Function Prepare_Sheet(ByVal f As AjaxControlToolkit.AsyncFileUpload) As Boolean
        Dim filename As String = System.IO.Path.GetFileName(f.FileName)
        Dim FileLength As Integer = f.PostedFile.ContentLength
        Dim MyFile As HttpPostedFile = f.PostedFile
        Session("fu2Name") = f.FileName
        Session("fu2File") = f.PostedFile
        Session("FileType") = System.IO.Path.GetExtension(f.FileName).ToLower()
        Dim MyImageData2(FileLength) As Byte
        ' byte[] myData = new Byte[nFileLen]
        MyFile.InputStream.Read(MyImageData2, 0, FileLength)
        Session("FileArray") = MyImageData2
        Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
        Dim FileAppend As Integer = 0
        Dim i As Integer = 0
        Session("FileType") = f.PostedFile.ContentType
        Return True
    End Function
#End Region

#Region "Paging"
    Protected Sub GVDataType_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            GVDataType.PageIndex = e.NewPageIndex
            FillGrid_DataTypes()
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub
    Protected Sub GVValues_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            gvValues.PageIndex = e.NewPageIndex
            FillGrid_Values()
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub
    Protected Sub txtpager_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpager.TextChanged
        Try
            FillGrid_DataTypes()
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub
#End Region

#Region "Sorting"
    ''' <summary>
    ''' Sorting gvDataTypes
    ''' </summary>
    Protected Sub gvDataTypes_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim strSortExpression As String() = ViewState("DataTypesSortExp").ToString().Split(" "c)

            ' If the sorting column is the same as the previous one, then change the sort order.
            If strSortExpression(0) = e.SortExpression Then
                If strSortExpression(1) = "ASC" Then
                    ViewState("DataTypesSortExp") = Convert.ToString(e.SortExpression) & " " & "DESC"
                Else
                    ViewState("DataTypesSortExp") = Convert.ToString(e.SortExpression) & " " & "ASC"
                End If
            Else
                ' If sorting column is another column, then specify the sort order to "Ascending".
                ViewState("DataTypesSortExp") = Convert.ToString(e.SortExpression) & " " & "DESC"
            End If

            ' Rebind the GridView control to show sorted data.
            BindGrid_DataTypes()
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub

    ''' <summary>
    ''' Sorting gvValues
    ''' </summary>
    Protected Sub gvValues_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim strSortExpression As String() = ViewState("ValuesSortExp").ToString().Split(" "c)

            ' If the sorting column is the same as the previous one, 
            ' then change the sort order.
            If strSortExpression(0) = e.SortExpression Then
                If strSortExpression(1) = "ASC" Then
                    ViewState("ValuesSortExp") = Convert.ToString(e.SortExpression) & " " & "DESC"
                Else
                    ViewState("ValuesSortExp") = Convert.ToString(e.SortExpression) & " " & "ASC"
                End If
            Else
                ' If sorting column is another column, 
                ' then specify the sort order to "Ascending".
                ViewState("ValuesSortExp") = Convert.ToString(e.SortExpression) & " " & "DESC"
            End If

            ' Rebind the GridView control to show sorted data.
            BindGrid_Values()
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub
#End Region

#Region "Search"
    Protected Sub ValidateCode()
        Try
            FillGrid_DataTypes()
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, ex.Message, Me)
        End Try
    End Sub
#End Region

    Protected Sub rbrelateTo_SelectedIndexChanged(sender As Object, e As EventArgs)
        If rbrelateTo.SelectedValue = "CTY" Then
            ddlRType1.Visible = True
            ddlRType2.Visible = False
        Else
            ddlRType1.Visible = False
            ddlRType2.Visible = True
        End If
    End Sub

    Protected Function isSuperAdmin() As Boolean
        Dim dt As DataTable = DBManager.Getdatatable("Select isNull(superAdmin,0) as superAdmin from tblUsers where id=" + UserId)
        Return dt.Rows(0)(0)
    End Function



End Class