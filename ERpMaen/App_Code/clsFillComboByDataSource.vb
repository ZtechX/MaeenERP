Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports BusinessLayer.BusinessLayer

Public Class clsFillComboByDataSource
    Private _SqlQuery As String = ""
    Private ComboText As String = ""
    Private ComboTag As String = ""
    Private ComboFirstItemText As String = "--Select--"
    Private DataSourceFilter As String = ""
    Private _DataSourceFilterValue As String = ""
    Private _AddFirstItem As Boolean = False
    Private _AllowAddNew As Boolean = False
    Private ComboAddNewText As String = "---New---"
    Private _AllowDataSourceFilter As Boolean = True

    Dim dtDataSource As New DataTable
    Dim dvDataSource As DataView
    Dim dvReturnValues As DataView
    Private _sqlConnection As New SqlConnection(DBManager.GetConnectionString)


    Property __SqlConnection As SqlConnection
        Get
            Return _sqlConnection
        End Get
        Set(value As SqlConnection)
            _sqlConnection = value
        End Set
    End Property
    Property ColumnNameasComboText() As String
        Get
            Return ComboText
        End Get
        Set(ByVal value As String)
            ComboText = value
        End Set
    End Property
    Property ColumnNameasComboTag() As String
        Get
            Return ComboTag
        End Get
        Set(ByVal value As String)
            ComboTag = value
        End Set
    End Property
    Property ColumnNameasFilterDataSource() As String
        Get
            Return DataSourceFilter
        End Get
        Set(ByVal value As String)
            DataSourceFilter = value
        End Set
    End Property


    Property TextUserItem() As String
        Get
            Return ComboFirstItemText
        End Get
        Set(ByVal value As String)
            ComboFirstItemText = value
        End Set
    End Property
    Property AddNewText() As String
        Get
            Return ComboAddNewText
        End Get
        Set(ByVal value As String)
            ComboAddNewText = value
        End Set
    End Property
    Property AllowDataSourceFilter() As Boolean
        Get
            Return _AllowDataSourceFilter
        End Get
        Set(ByVal value As Boolean)
            _AllowDataSourceFilter = value
        End Set
    End Property
    Property SqlQuery() As String
        Get
            Return _SqlQuery
        End Get
        Set(ByVal value As String)
            _SqlQuery = value
        End Set
    End Property
    Property DataSourceFilterValue() As String
        Get
            Return _DataSourceFilterValue
        End Get
        Set(ByVal value As String)
            _DataSourceFilterValue = value
        End Set
    End Property

    Sub New(ByVal SqlQuery As String, ByVal ColumnNameasComboText As String, ByVal ColumnNameasComboTag As String, ByVal ColumnNameasFilterDataSource As String)

        _SqlQuery = SqlQuery
        Me.ColumnNameasComboText = ColumnNameasComboText
        Me.ColumnNameasComboTag = ColumnNameasComboTag
        Me.ColumnNameasFilterDataSource = ColumnNameasFilterDataSource
        SetDataSource()
    End Sub


#Region "Functions"

    Public Shared Function ExecuteQueryAndReturnDataTable(ByVal sqlQuery As String, ByRef _SqlConnection As SqlClient.SqlConnection) As DataTable
        Dim DT As New DataTable
        If sqlQuery Is Nothing Then Return Nothing
        DT.Clear()
        Try
            Dim SqlDadpt As New SqlClient.SqlDataAdapter(sqlQuery, _SqlConnection)
            SqlDadpt.Fill(DT)
        Catch ex As Exception
            Throw ex
        Finally
            Close_Connection(_SqlConnection)
        End Try
        Return DT
    End Function
    Public Shared Sub Close_Connection(ByRef _SqlConnection As SqlClient.SqlConnection)
        Try
            If _SqlConnection.State = ConnectionState.Open Then
                _SqlConnection.Close()
            End If
        Catch ex As Exception
            'MiniSoftMsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub SetDataSource()
        dtDataSource = ExecuteQueryAndReturnDataTable(_SqlQuery, _sqlConnection)
    End Sub

    Public Sub ClearDataSource(ByRef CMB As DropDownList, ByVal FilterDataSourceValue As String, Optional ByVal AddFirstItemAsSpace As Boolean = True, Optional ByVal FirstItem As String = "--", Optional ByVal AllowAddNew As Boolean = True)
        dtDataSource.Rows.Clear()
        SetComboItems1(CMB, FilterDataSourceValue, AddFirstItemAsSpace, FirstItem, AllowAddNew)
    End Sub
    Public Sub SetComboItems(ByRef CMB As DropDownList, ByVal FilterDataSourceValue As String, Optional ByVal AddFirstItemAsSpace As Boolean = True, Optional ByVal FirstItem As String = "-- Select --", Optional ByVal AllowAddNew As Boolean = True)
        'If AddFirstItemAsSpace = True Then AddFirstItemASRow(FirstItem)
        _AllowAddNew = AllowAddNew
        _AddFirstItem = AddFirstItemAsSpace
        DataSourceFilterValue = FilterDataSourceValue
        TextUserItem = FirstItem
        'dvDataSource = New DataView(dtDataSource)
        'dvDataSource.RowFilter = "Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)='" & FilterDataSourceValue & "' or Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)=''"
        RefreshDataViewONDataSource(CMB)
        CMB.SelectedValue = 0
    End Sub
    Public Sub SetComboItems1(ByRef CMB As DropDownList, ByVal FilterDataSourceValue As String, Optional ByVal AddFirstItemAsSpace As Boolean = True, Optional ByVal FirstItem As String = "--", Optional ByVal AllowAddNew As Boolean = True)
        'If AddFirstItemAsSpace = True Then AddFirstItemASRow(FirstItem)
        _AllowAddNew = AllowAddNew
        _AddFirstItem = AddFirstItemAsSpace
        DataSourceFilterValue = FilterDataSourceValue
        TextUserItem = FirstItem
        'dvDataSource = New DataView(dtDataSource)
        'dvDataSource.RowFilter = "Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)='" & FilterDataSourceValue & "' or Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)=''"
        RefreshDataViewONDataSource(CMB)
        CMB.SelectedIndex = 0
    End Sub
    Private Sub SetComboDataSource(ByRef CMB As DropDownList)
        CMB.DataSource = dvDataSource
        CMB.DataTextField = ColumnNameasComboText
        CMB.DataValueField = ColumnNameasComboTag
        CMB.DataBind()
        ' If CMB.Items.Count > 0 Then CMB.Text = CMB.Items(0).row.item(ColumnNameasComboText).ToString 'CMB.SelectionStart = 0 'CMB.SelectedIndex = 0
        'CMB.Tag = GetComboTag(CMB)
    End Sub
    Private Sub RefreshDataViewONDataSource(ByRef CMB As DropDownList)

        If _AddFirstItem = True Then AddFirstItemASRow(TextUserItem)
        If _AllowAddNew = True Then AddLastItemAsAddNewRow(TextUserItem)
        dvDataSource = New DataView(dtDataSource)
        If AllowDataSourceFilter = True Then
            If DataSourceFilterValue = "" Then
                'dvDataSource.RowFilter = "Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)<>'" & DataSourceFilterValue & "' or Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)<>''"
            Else
                dvDataSource.RowFilter = "Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)='" & DataSourceFilterValue & "' or Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)=''"
            End If
        End If
        SetComboDataSource(CMB)
    End Sub
    'Public Sub SetComboItems(ByRef CMBDgv As DataGridViewDropDownListColumn, ByRef DGV As DataGridView, ByVal FilterDataSourceValue As String, ByVal ColumnIndex As Integer, Optional ByVal AddFirstItemAsSpace As Boolean = True, Optional ByVal FirstItem As String = "")
    '    ' If AddFirstItemAsSpace = True Then AddFirstItemASRow(FirstItem)
    '    _AddFirstItem = AddFirstItemAsSpace
    '    DataSourceFilterValue = FilterDataSourceValue
    '    dvDataSource = New DataView(dtDataSource)
    '    If AllowDataSourceFilter = True Then dvDataSource.RowFilter = "Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)='" & FilterDataSourceValue & "' or Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)=''"
    '    CMBDgv.DataSource = dvDataSource
    '    CMBDgv.DisplayMember = ColumnNameasComboText
    '    CMBDgv.ValueMember = ColumnNameasComboTag

    '    If CMBDgv.Items.Count > 0 Then DGV.Rows(0).Cells(ColumnIndex).Value = CMBDgv.Items.Item(0)


    '    TextUserItem = FirstItem
    'End Sub
    'Public Function GetComboTag(ByRef DropDownListRef As AjaxControlToolkit.ComboBox) As String
    '    If DropDownListRef.Text = vbNullString Then Return ""
    '    If IsDBNull(DropDownListRef.SelectedValue) Then
    '        DropDownListRef.Tag = ""
    '        Return ""
    '    Else
    '        If DropDownListRef.SelectedValue.ToString = "System.Data.DataRowView" Then
    '            DropDownListRef.Tag = ""
    '            Return ""
    '        Else
    '            DropDownListRef.Tag = DropDownListRef.SelectedValue.ToString
    '            Return DropDownListRef.SelectedValue.ToString
    '        End If
    '    End If

    'End Function
    Public Sub ChangeDataSourceFilter(ByVal FilterDataSourceValue As String)
        DataSourceFilterValue = FilterDataSourceValue
        If AllowDataSourceFilter = True Then dvDataSource.RowFilter = "Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)='" & FilterDataSourceValue & "' or Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)=''"
    End Sub
    Public Function GetDataForColumn(ByVal ColumnName As String, ByVal FilterDataSourceValue As String, ByVal ComboTag As String) As String
        DataSourceFilterValue = FilterDataSourceValue
        dvReturnValues = New DataView(dtDataSource)
        If AllowDataSourceFilter = False OrElse ColumnNameasFilterDataSource = "" Then
            dvReturnValues.RowFilter = "" & Me.ColumnNameasComboTag & "='" & ComboTag & "'"
        Else
            ' If AllowDataSourceFilter = True Then dvReturnValues.RowFilter = "Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)='" & FilterDataSourceValue & "' and " & Me.ColumnNameasComboTag & "='" & ComboTag & "'"
            dvReturnValues.RowFilter = "Convert(" & Me.ColumnNameasFilterDataSource & ",System.String)='" & FilterDataSourceValue & "' and " & Me.ColumnNameasComboTag & "='" & ComboTag & "'"
        End If
        If dvReturnValues.Count > 0 Then
            Return dvReturnValues.Item(0).Item(ColumnName)
        Else
            Return ""
        End If
    End Function
    Public Function GetDataForColumn(ByVal FirstConditionColumnName As String, ByVal FirstConditionColumnValue As String, ByVal SecondConditionColumnName As String, ByVal SecondConditionColumnValue As String) As DataRowView
        dvReturnValues = New DataView(dtDataSource)
        If FirstConditionColumnName = "" And SecondConditionColumnName = "" Then Return Nothing
        Dim rowfilter As String
        If FirstConditionColumnName <> "" Then

            rowfilter = "" & FirstConditionColumnName & "='" & FirstConditionColumnValue & "' "
        ElseIf SecondConditionColumnName <> "" Then
            rowfilter = " " & SecondConditionColumnName & "='" & SecondConditionColumnValue & "'"
        Else
            rowfilter = "" & FirstConditionColumnName & "='" & FirstConditionColumnValue & "' and " & SecondConditionColumnName & "='" & SecondConditionColumnValue & "'"
        End If

        dvReturnValues.RowFilter = rowfilter
        If dvReturnValues.Count > 0 Then Return dvReturnValues.Item(0) Else Return Nothing


    End Function
    Private Sub AddFirstItemASRow(ByVal FirstItem As String)
        Dim dr As DataRow
        dr = dtDataSource.NewRow

        'For i As Integer = 0 To dtDataSource.Columns.Count - 1
        '    If dtDataSource.Columns(i).ColumnName = ColumnNameasComboText Then
        dr.Item(ColumnNameasComboText) = FirstItem
        dr.Item(ComboTag) = 0
        If ColumnNameasFilterDataSource <> "" Then dr.Item(ColumnNameasFilterDataSource) = ""
        '    End If
        'Next
        dtDataSource.Rows.InsertAt(dr, 0)
        ' dtDataSource.Rows.Add(dr)

    End Sub
    Private Sub AddLastItemAsAddNewRow(ByVal FirstItem As String)
        Dim dr As DataRow
        dr = dtDataSource.NewRow

        'For i As Integer = 0 To dtDataSource.Columns.Count - 1
        '    If dtDataSource.Columns(i).ColumnName = ColumnNameasComboText Then
        dr.Item(ColumnNameasComboText) = ComboAddNewText
        If ColumnNameasFilterDataSource <> "" Then dr.Item(ColumnNameasFilterDataSource) = ""
        '    End If
        'Next
        dtDataSource.Rows.Add(dr)
    End Sub
    Public Sub RefreshDataSource(ByRef CMB As DropDownList)
        dtDataSource = ExecuteQueryAndReturnDataTable(_SqlQuery, _sqlConnection)

        RefreshDataViewONDataSource(CMB)

    End Sub


#End Region

End Class