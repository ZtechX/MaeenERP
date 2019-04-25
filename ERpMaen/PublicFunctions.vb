Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data
Imports System.Web.UI.WebControls
Imports BusinessLayer.BusinessLayer
Imports System.Data.SqlClient
Imports System.Net
Imports BusinessLayer
Imports System.Security.Cryptography
Imports System.IO

Public Class PublicFunctions
    Public Shared DataFormat As String = "dd/MM/yyyy"
    Public Shared QueryString As String = "KOKO"



    Public Shared Function GetAppRootUrl() As String
        Dim dt As New DataTable
        dt = DBManager.Getdatatable("select url from tblcompany")
        If dt.Rows.Count <> 0 Then
            Return dt.Rows(0).Item("url").ToString
        Else
            Dim host As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
            Dim appRootUrl As String = HttpContext.Current.Request.ApplicationPath
            If Not appRootUrl.EndsWith("/") Then
                'a virtual
                appRootUrl += "/"
            End If
            appRootUrl = appRootUrl.Substring(0, appRootUrl.Length - 1)
            Return host & appRootUrl
        End If
    End Function

    ''' <summary>
    ''' Convert datatable to string as json format; if dt is empty then return empty string
    ''' </summary>
    Public Shared Function ConvertDataTabletoString(ByVal dt As DataTable) As String
        If dt.Rows.Count = 0 Then
            Return String.Empty
        Else
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim rows As New List(Of Dictionary(Of String, Object))()
            Dim row As Dictionary(Of String, Object)
            For Each dr As DataRow In dt.Rows
                row = New Dictionary(Of String, Object)()
                For Each col As DataColumn In dt.Columns

                    If col.ColumnName.ToString.Contains("date") Then


                        If col.ColumnName.ToString.Contains("h") Or dr(col).ToString = "" Then


                            row.Add(col.ColumnName, dr(col))
                        Else
                            row.Add(col.ColumnName, ConvertNumbertoDate(dr(col).ToString))
                        End If

                    Else
                        row.Add(col.ColumnName, dr(col))
                    End If

                Next
                rows.Add(row)
            Next
            Return serializer.Serialize(rows)
        End If
    End Function

    '''' <summary>
    '''' Convert datatable to string as json format; if dt is empty then return empty string
    '''' </summary>
    'Public Shared Function ConvertDataTabletoString(ByVal dt As DataTable) As String
    '    If dt.Rows.Count = 0 Then
    '        Return String.Empty
    '    Else
    '        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim rows As New List(Of Dictionary(Of String, Object))()
    '        Dim row As Dictionary(Of String, Object)
    '        For Each dr As DataRow In dt.Rows
    '            row = New Dictionary(Of String, Object)()
    '            For Each col As DataColumn In dt.Columns
    '                row.Add(col.ColumnName, dr(col))
    '            Next
    '            rows.Add(row)
    '        Next
    '        Return serializer.Serialize(rows)
    '    End If
    'End Function

    Public Shared Function GetIdentity(ByRef _SqlConnection As SqlConnection, ByRef _SqlTransaction As SqlTransaction) As String
        If _SqlConnection.State <> ConnectionState.Open Then
            _SqlConnection.Open()
        End If
        Dim dt As DataTable = ExecuteQuery.ExecuteQueryAndReturnDataTable("select @@Identity as identityId", _SqlConnection, _SqlTransaction)
        If dt.Rows.Count > 0 Then
            Dim identity As String = dt.Rows(0).Item("identityId").ToString
            Return identity
        Else
            _SqlTransaction.Rollback()
            _SqlConnection.Close()
            Return ""
        End If
    End Function


    ''' <summary>
    ''' Remove HTML tags.
    ''' </summary>
    Public Shared Function RemoveHtmlTags(ByVal html As String) As String
        ' Remove HTML tags.
        Return Regex.Replace(html, "<.*?>", "")
    End Function

    Public Shared Function GetMaxId(ByVal FieldName As String, ByVal TableName As String, ByRef _SqlConnection As SqlConnection, ByRef _SqlTransaction As SqlTransaction) As String
        If _SqlConnection.State <> ConnectionState.Open Then
            _SqlConnection.Open()
        End If
        Dim dt As DataTable = ExecuteQuery.ExecuteQueryAndReturnDataTable("select Max(convert(integer,ID,0))+1 as 'MaxId' from " + TableName + "", _SqlConnection, _SqlTransaction)
        If dt.Rows.Count > 0 Then
            Dim MaxId As String = dt.Rows(0).Item("MaxId").ToString
            Return MaxId
        Else
            _SqlTransaction.Rollback()
            _SqlConnection.Close()
            Return ""
        End If
    End Function

    Public Shared Function DateFormat(ByVal Value As Object) As Object
        If Value = "" OrElse Value Is Nothing OrElse Value = vbNullString Then
            Return "NULL"
        Else
            Return Date.ParseExact(Value, "dd/MM/yyyy", Nothing)
        End If
    End Function
    Public Shared Function ValidateDateValue(ByVal txtValue As String) As Boolean
        Try
            Dim dt As DateTime = txtValue
            dt = dt.ToShortDateString()
            dt = Date.ParseExact(dt, "dd/MM/yyyy", Nothing)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function GetPageName(ByVal Url As String) As String
        Dim pageName As String = Url.ToString.Split("/").Last.Split("?")(0)
        If pageName <> "" Then
            pageName = pageName.Replace(".aspx", "")
            Return pageName
        Else
            Return String.Empty
        End If
    End Function
    Public Function ChecksCountIsNotZero(ByRef pnl As Panel, ByVal GV As GridView, ByRef cmdDelete As LinkButton, ByRef cmdUpdate As LinkButton) As Boolean
        Dim i As Integer = 0
        Dim Count As Integer = 0
        Try
            While i < GV.Rows.Count
                If CType(GV.Rows(i).FindControl("chkSelect"), CheckBox).Checked = True Then
                    Count += 1
                End If
                i += 1
            End While
            If Count <> 0 Then
                cmdDelete.Enabled = True
                If Count = 1 Then
                    cmdUpdate.Enabled = True
                Else
                    cmdUpdate.Enabled = False
                End If
            Else
                cmdDelete.Enabled = False
                cmdUpdate.Enabled = False
            End If
            pnl.Enabled = True
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function RandomNumber(ByVal MaxNumber As Integer, _
Optional ByVal MinNumber As Integer = 0) As Integer

        'initialize random number generator
        Dim r As New Random(System.DateTime.Now.Millisecond)

        'if passed incorrect arguments, swap them
        'can also throw exception or return 0

        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return r.Next(MinNumber, MaxNumber)

    End Function
    Public Function ClearAll(ByRef Container As Control) As Boolean
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim h As Integer = 0
        While Container.Controls.Count > i
            Dim O As Control = Container.Controls(i)
            If TypeOf O Is TextBox Then
                CType(O, TextBox).Text = ""
            ElseIf TypeOf O Is GridView Then
                CType(O, GridView).DataSource = Nothing
                CType(O, GridView).DataBind()
            ElseIf TypeOf O Is CheckBox Then
                CType(O, CheckBox).Checked = False
            ElseIf TypeOf O Is RadioButton Then
                CType(O, RadioButton).Checked = False

            Else
                While O.Controls.Count > j
                    Dim B As Control = O.Controls(j)
                    If TypeOf B Is TextBox Then
                        CType(B, TextBox).Text = ""
                    ElseIf TypeOf B Is GridView Then
                        CType(B, GridView).DataSource = Nothing
                        CType(B, GridView).DataBind()
                        'ElseIf TypeOf B Is DropDownList Then
                        '    CType(B, DropDownList).SelectedValue = "0"
                    ElseIf TypeOf O Is CheckBox Then
                        CType(O, CheckBox).Checked = False

                    Else
                        While B.Controls.Count > k
                            Dim C As Control = B.Controls(k)
                            If TypeOf C Is TextBox Then
                                CType(C, TextBox).Text = ""
                                'ElseIf TypeOf C Is DropDownList Then
                                '    CType(C, DropDownList).SelectedValue = "0"
                            ElseIf TypeOf O Is CheckBox Then
                                CType(O, CheckBox).Checked = False
                            ElseIf TypeOf C Is GridView Then
                                CType(C, GridView).DataSource = Nothing
                                CType(C, GridView).DataBind()
                            Else
                                While C.Controls.Count > h
                                    Dim D As Control = C.Controls(h)
                                    If TypeOf D Is TextBox Then
                                        CType(D, TextBox).Text = ""
                                    ElseIf TypeOf D Is GridView Then
                                        CType(D, GridView).DataSource = Nothing
                                        CType(D, GridView).DataBind()
                                        'ElseIf TypeOf D Is DropDownList Then
                                        '    CType(D, DropDownList).SelectedValue = "0"
                                    ElseIf TypeOf O Is CheckBox Then
                                        CType(O, CheckBox).Checked = False
                      
                                    End If
                                    h += 1
                                End While
                            End If
                            k += 1
                        End While
                    End If
                    j += 1
                End While
            End If
            i += 1
            j = 0
            k = 0
            h = 0
        End While
    End Function
    Shared Function HasQuery(ByVal _Page As Page) As Boolean
        Try
            Return Not (_Page.Request.QueryString(QueryString)) Is Nothing
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function GetLockupId(Description As String, Type As String) As String
        Try
            Dim dt As DataTable = DBManager.Getdatatable("select * from TblLockup where Type ='" + Type + "' and Description='" + Description + "'")
            If dt.Rows.Count > 0 Then
                Dim LockupId As String = dt.Rows(0).Item("Id").ToString
                Return LockupId
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Public Shared Function GetLockupValue(LockupId As String) As String
        Try
            Dim dt As DataTable = DBManager.Getdatatable("select * from TblLockup where Id ='" + LockupId + "'")
            If dt.Rows.Count > 0 Then
                Dim Description As String = dt.Rows(0).Item("Description").ToString
                Return Description
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Public Shared Function GetMaxLockup(Type As String) As String
        Try
            Dim dt As DataTable = DBManager.Getdatatable("select isnull(max(orderNo),0) as orderNoMax from TblLockup where Type ='" + Type + "'")
            If dt.Rows.Count > 0 Then
                Dim orderMax As Integer = dt.Rows(0).Item("orderNoMax") + 1
                Return orderMax
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Public Shared Function GetLockupValuepublish(ByVal LockupId As String) As String
        Try
            Dim dt As DataTable = DBManager.Getdatatable("select * from tblLockupPublish where LoockupId ='" + LockupId + "'")
            If dt.Rows.Count > 0 Then
                Dim Description As String = dt.Rows(0).Item("Name").ToString
                Return Description
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Function GetQueryString(url As String, lbText As String, CodeValue As String) As String
        Dim enc As String
        If lbText = "New" Then
            'enc = QueryStringModule.Encrypt("Operation=Add&code=" + CodeValue + "")
            enc = "Operation=Add&code=" + CodeValue + ""
        Else
            'enc = QueryStringModule.Encrypt("Operation=Search&code=" + CodeValue + "")
            enc = "Operation=Search&code=" + CodeValue + ""
        End If
        url += "?" + enc
        Return url
    End Function
    Public Function GetUserType(ByVal UserCode As String) As String
        Dim usertype As String = "Driver"
        Dim UserTypeID As String = 2
        Try
            Dim dtUser As New DataTable
            dtUser = DBManager.Getdatatable("select UserType from  TblUsers where id=" + UserCode)
            If dtUser.Rows.Count <> 0 Then
                UserTypeID = dtUser.Rows(0).Item("UserType").ToString
                Return UserTypeID
            End If
            clsGeneralVariables.usertype = usertype
            Return usertype
        Catch ex As Exception
            clsGeneralVariables.usertype = usertype
            Return usertype
        End Try
    End Function
    Public Shared Function GetUsedContactsQuery(ByVal ContactCode As String) As String
        Dim statment As String = "select SUM(cnt)as cnt from (select  COUNT(*) as Cnt from tblLeads    where tblLeads .Owner  ='" + ContactCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from tblLeads    where tblLeads   .Owner  ='" + ContactCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from TblProperties where TblProperties.OwnerAccount  ='" + ContactCode + "' and (deleted is null or deleted ='False') " & _
                               "union all select COUNT(*) as Cnt from TblCorrespondence    where TblCorrespondence.ClientId  ='" + ContactCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from TblContracts    where TblContracts.ClientID   ='" + ContactCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from TblContracts    where TblContracts.ClientId2  ='" + ContactCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from TblContracts    where TblContracts.Landloard  ='" + ContactCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from tblCRMRentAndResContract     where tblCRMRentAndResContract .ClientCode  ='" + ContactCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from tblCRMRentAndResContract     where tblCRMRentAndResContract .LandLOard  ='" + ContactCode + "'and (deleted is null or deleted ='False') )" & _
                               "as Temp"
        Return statment
    End Function
    Public Shared Function GetUsedAgentQuery(ByVal AgentCode As String) As String
        Dim statment As String = "select SUM(cnt)as cnt from (select  COUNT(*) as Cnt from tblLeads    where tblLeads .Salesman  ='" + AgentCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from tblLeads    where tblLeads   .Salesman  ='" + AgentCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from TblPropertiesDetails where TblPropertiesDetails.SalesMan  ='" + AgentCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from TblContacts    where TblContacts.Salesman  ='" + AgentCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from TblContracts    where TblContracts.SalesmanCode   ='" + AgentCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select COUNT(*) as Cnt from tblCRMRentAndResContract     where tblCRMRentAndResContract .Salesman  ='" + AgentCode + "' and (deleted is null or deleted ='False')" & _
                               "union all select count(*) as Cnt  from TblCorrespondence where  TblCorrespondence.UserId =(select assignuserid from TblUsers where code ='" + AgentCode + "' ))" & _
                               "as Temp"
        Return statment
    End Function
    Public Shared Function IsNotUsed(ByVal Code As String, ByVal type As String) As Boolean
        Dim statment As String = ""
        If type = "Contact" Then
            statment = GetUsedContactsQuery(Code)
        ElseIf type = "Agent" Then
            statment = GetUsedAgentQuery(Code)
        End If
        Dim dt As DataTable = DBManager.Getdatatable(statment)
        If dt.Rows.Count <> 0 Then
            If dt.Rows(0).Item(0) <> 0 Then
                Return False
            Else
                Return True
            End If
        End If
    End Function
    Public Shared Sub Update_user_status(ByVal user_id As String, ByVal status As String)
        DBManager.ExcuteQuery("update tblusers set On_Off ='" + status.ToString + "' where id =" + user_id.ToString)
    End Sub
    Public Shared Function GetPageOperation(ByVal Url As String) As String
        Dim Operation As String = Url.ToString.Split("/").Last
        If Operation <> "" And Operation.Contains("?") Then
            If Operation.Split("?")(1).Split("=")(0) = "Operation" Then
                If Operation.Contains("&") Then
                    Operation = Operation.Split("?")(1).Split("=")(1).Split("&")(0) + Operation.Split("?")(1).Split("=")(2).Trim.Substring(0, 1)
                Else
                    Operation = Operation.Split("?")(1).Split("=")(1)
                End If
                Return Operation
            Else
                Return String.Empty
            End If

        Else
            Return String.Empty
        End If
    End Function
    Public Shared Function GetFormName(ByVal page As Page) As String
        Try
            Dim pageName As String = PublicFunctions.GetPageName(page.Request.Url.ToString)

            Dim PageOperation As String = PublicFunctions.GetPageOperation(page.Request.Url.ToString)
            Dim dt As New DataTable
            If PageOperation.Contains("Add") Then
                dt = DBManager.Getdatatable("select FormTitle from tblForms where FormName='" + pageName + "' and Operation ='" + PageOperation + "'")
            Else
                dt = DBManager.Getdatatable("select FormTitle from tblForms where FormName='" + pageName + "' and (Operation is null or Operation ='')")
            End If
            Return dt.Rows(0).Item("FormTitle").ToString
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    ''' <summary>
    ''' This function to check the null values in the retrieved data from data table and set it to empty string
    ''' </summary>
    Public Shared Function Check_DT_Values(ByVal dtValue As String, ByVal dtLookup As Boolean) As String
        Try
            If dtLookup Then
                Return PublicFunctions.GetLockupValue(dtValue)
            ElseIf dtValue <> vbNullString And dtValue <> String.Empty Then
                Return dtValue
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function Encrypt(clearText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99010"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function

    Public Shared Function Decrypt(cipherText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99010"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function

    Public Shared Function GetWebSitesDataTable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("WebSiteName")
        dt.Columns.Add("WebSiteXML")
        Dim dr As DataRow = dt.NewRow
        dr(0) = "Bayut"
        dr(1) = "Bayut.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "Dubizzle"
        dr(1) = "Dubizzle.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "Property Finder"
        dr(1) = "PropertyFinder.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "CasaYou.com"
        dr(1) = "CasaYou.com.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "99 Acres"
        dr(1) = "99Acres.com.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "7days"
        dr(1) = "7days.ae.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "AqarMap"
        dr(1) = "AqarMap.com.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "JustLanded.com"
        dr(1) = "JustLanded.com.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "ArabianCommercial"
        dr(1) = "ArabianCommercial.com.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "Dubizzle Setup"
        dr(1) = "DubizzleSetup.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "HomeIn.com"
        dr(1) = "HomeIn.com.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "GulfNews.com"
        dr(1) = "GulfNews.com.xml"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = "Generic2"
        dr(1) = "Generic2.xml"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Public Shared Function TransUpdateInsert(ByVal dictBasicDataJson As Dictionary(Of String, Object), ByVal table_name As String, id As String, ByRef con As SqlConnection, ByRef trans As SqlTransaction) As Boolean
        Dim strquer As String
        Dim strvalues As String
        Dim finalquery As String
        Dim dtFields = getTableFields(table_name)
        If id = "" Or id = "0" Then
            strquer = "insert into " + table_name + " ("
            strvalues = " values ("
            If dtFields.Rows.Count <> 0 Then
                For Each row As DataRow In dtFields.Rows
                    If row.Item("PrimaryKey") = False Then
                        Dim field_name = row.Item("ColumnName").ToString.ToLower
                        Dim field_name1 = row.Item("ColumnName").ToString
                        Dim datatype = row.Item("Datatype").ToString
                        Try

                            If dictBasicDataJson.ContainsKey(field_name) Then
                                If Not String.IsNullOrWhiteSpace(dictBasicDataJson(field_name)) Then
                                    Dim field_value = dictBasicDataJson(field_name)
                                    If field_name.Contains("date") And datatype <> "bit" Then
                                        If field_name.Contains("h") Then
                                            field_value = field_value
                                        Else
                                            field_value = ConvertDatetoNumber(field_value)
                                        End If

                                    End If
                                    strquer = strquer + " " + field_name.ToString + ", "
                                    If datatype = "float" Or datatype = "money" Or datatype = "int" Then
                                        strvalues = strvalues + field_value.ToString + " , "
                                    ElseIf datatype = "datetime" Then
                                        strvalues = strvalues.ToString + ConvertDatetoNumber(field_value.ToString).ToString + " , "
                                    Else
                                        strvalues = strvalues + "'" + field_value.ToString + "' , "
                                    End If
                                End If
                            ElseIf dictBasicDataJson.ContainsKey(field_name1) Then
                                If Not String.IsNullOrWhiteSpace(dictBasicDataJson(field_name1)) Then
                                    Dim field_value = dictBasicDataJson(field_name1)
                                    If field_name.Contains("date") Then
                                        If field_name.Contains("h") Then
                                            field_value = field_value
                                        Else
                                            field_value = ConvertDatetoNumber(field_value)
                                        End If

                                    End If
                                    strquer = strquer + " " + field_name1.ToString + ", "
                                    If datatype = "float" Or datatype = "money" Or datatype = "int" Then
                                        strvalues = strvalues + field_value.ToString + " , "
                                    Else
                                        strvalues = strvalues + "'" + field_value.ToString + "' , "
                                    End If
                                End If
                            Else
                                If field_name <> "serial_no" Then
                                    strquer = strquer + " " + field_name.ToString + ", "
                                    If datatype = "float" Or datatype = "money" Or datatype = "int" Then
                                        strvalues = strvalues + "0 , "
                                    ElseIf field_name = "ssma_timestamp" Then
                                        strvalues = strvalues + "" + Now.Date + " , "
                                    Else
                                        strvalues = strvalues + "'0' , "
                                    End If
                                End If
                            End If
                        Catch ex As Exception
                            Return False
                        End Try
                    End If
                Next
            End If
            strquer = strquer.Substring(0, strquer.Length - 2) + " )"
            strvalues = strvalues.Substring(0, strvalues.Length - 2) + ")"
            finalquery = strquer + strvalues
        Else
            strquer = "update  " + table_name + " set"
            strvalues = "where  "
            For Each row As DataRow In dtFields.Rows
                Dim field_name = row.Item("ColumnName").ToString.ToLower
                Dim field_name1 = row.Item("ColumnName").ToString
                If row.Item("PrimaryKey") Then
                    strvalues = strvalues + "  " + field_name + " =" + id.ToString
                Else

                    If dictBasicDataJson.ContainsKey(field_name) Then
                        If Not String.IsNullOrWhiteSpace(dictBasicDataJson(field_name)) Then
                            Dim field_value = dictBasicDataJson(field_name)
                            If field_name.Contains("date") Then
                                If Not field_name.Contains("h") Then
                                    field_value = ConvertDatetoNumber(field_value)
                                End If

                            End If
                            If field_value.ToString <> "" Then

                                strquer = strquer + " " + field_name.ToString + "= '" + field_value.ToString + "' , "

                            End If
                        End If
                    Else
                        If dictBasicDataJson.ContainsKey(field_name1) Then
                            If Not String.IsNullOrWhiteSpace(dictBasicDataJson(field_name1)) Then
                                Dim field_value = dictBasicDataJson(field_name1)
                                If field_name.Contains("date") Then
                                    If Not field_name.Contains("h") Then
                                        field_value = ConvertDatetoNumber(field_value)
                                    End If

                                End If
                                If field_value.ToString <> "" Then
                                    strquer = strquer + " " + field_name1.ToString + "= '" + field_value.ToString + "' , "
                                End If
                            End If
                        End If
                    End If
                End If
            Next
            finalquery = strquer.Substring(0, strquer.Length - 2) + "" + strvalues
        End If
        If DBManager.ExcuteQueryTransaction(finalquery, con, trans) <> -1 Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Shared Function get_primary_key(ByVal dtfields As DataTable) As String
        Dim primary_key As String = ""
        If dtfields.Rows.Count <> 0 Then
            For Each row As DataRow In dtfields.Rows
                If row.Item("PrimaryKey") Then
                    primary_key = row.Item("ColumnName").ToString
                    Exit For
                End If
            Next
        End If
        Return primary_key
    End Function

    ''' <summary>
    ''' Get All fields in table and its type
    ''' </summary>
    ''' <param name="table_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getTableFields(ByVal table_name As String) As DataTable
        Dim dt As New DataTable
        ' dt = db.select("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('" + table_name.ToString + "')")
        Dim strquery = "SELECT c.name 'ColumnName', t.Name 'Datatype', c.max_length 'MaxLength',  c.precision, c.scale, c.is_nullable, ISNULL(i.is_primary_key, 0) 'PrimaryKey' " &
        "FROM sys.columns c  INNER Join  sys.types t ON c.user_type_id = t.user_type_id LEFT OUTER JOIN  sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id " &
        "LEFT OUTER JOIN  sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('" + table_name.ToString + "')"
        dt = DBManager.Getdatatable(strquery)
        Return dt
    End Function

    Public Shared Function GetDataForUpdate(ByVal table_name As String, ByVal id As String) As String
        Dim dt As New DataTable
        Dim str As String = ""
        dt = DBManager.Getdatatable("SELECT * from " + table_name.ToString + " where  id ='" + id + "'")
        If dt.Rows.Count <> 0 Then
            str = PublicFunctions.ConvertDataTabletoString(dt)
        End If
        Return str
    End Function


    Public Shared Function ConvertDatetoNumber(ByVal xDate As String) As Long
        Try
            Dim Apt() As String
            Dim TempConvert As String
            Dim ConvertResult As Long
            Apt = xDate.Split("/")
            TempConvert = Apt(2) & Apt(1) & Apt(0)

            If TempConvert.Length = 8 Then
                ConvertResult = Long.Parse(TempConvert)
            Else
                ConvertResult = 0
            End If

            Return ConvertResult
        Catch ex As Exception
            Return 0
        End Try

    End Function

    ' Purpose: Used to convert date to numbers
    Public Shared Function ConvertNumbertoDate(ByVal str_Date As Long, Optional ByVal xSplit As String = "") As String
        Try
            Dim TempConvert As String
            Dim ConvertResult As String

            TempConvert = str_Date.ToString.Substring(6, 2) & "/" & str_Date.ToString.Substring(4, 2) & "/" & str_Date.ToString.Substring(0, 4)

            If Len(xSplit) = 0 Then
                ConvertResult = String.Format(TempConvert, "MM/dd/yyyy")
            Else
                ConvertResult = String.Format(TempConvert, "MM" & Trim(xSplit) & "dd" & Trim(xSplit) & "yyyy")
            End If
            Return ConvertResult
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function TransUsers_logs(ByVal form_id As String, ByVal table_name As String, ByVal operation As String, ByRef con As SqlConnection, ByRef trans As SqlTransaction) As Boolean
        Try
            Dim strquer As String

            Dim h = Date.Now.Hour.ToString
            Dim t = Date.Now.Minute.ToString

            If h < 10 Then
                h = "0" + h.ToString
            End If
            If t < 10 Then
                t = "0" + t.ToString
            End If
            Dim time_m = h + "" + t
            Dim date_m = ConvertDatetoNumber(Date.Now.ToShortDateString.ToString)

            Dim comp_id = LoginInfo.GetComp_id()
            Dim user_id = LoginInfo.GetUser__Id()
            Dim details = "تفاصيل" + operation.ToString + " في " + table_name.ToString + " من الشاشة " + form_id.ToString + " بواسطة" + user_id.ToString + " التابع " + comp_id.ToString + " بتاريخ " + date_m.ToString + " الساعة" + time_m.ToString

            strquer = "insert into users_logs(comp_id,user_id,form_id ,table_name ,operation,details,date_m,time)values(" + comp_id.ToString + "," + user_id.ToString + "," + form_id.ToString + ",'" + table_name.ToString + "','" + operation.ToString + "','" + details.ToString + "' ," + date_m.ToString + ",'" + time_m + "')"

            If DBManager.ExcuteQueryTransaction(strquer, con, trans) <> -1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function


    'Public Shared Function ConvertDatetoNumber(ByVal xDate As String) As Long
    '    Try
    '        Dim Apt() As String
    '        Dim TempConvert As String
    '        Dim ConvertResult As Long

    '        Apt = xDate.Split("/")
    '        TempConvert = Apt(2) & Apt(1) & Apt(0)

    '        If TempConvert.Length = 8 Then
    '            ConvertResult = Long.Parse(TempConvert)
    '        Else
    '            ConvertResult = 0
    '        End If

    '        Return ConvertResult
    '    Catch ex As Exception
    '        Return 0
    '    End Try

    'End Function

    ' Purpose: Used to convert date to numbers
    'Public Shared Function ConvertNumbertoDate(ByVal str_Date As Long, Optional ByVal xSplit As String = "") As String
    '    Try
    '        Dim TempConvert As String
    '        Dim ConvertResult As String

    '        TempConvert = str_Date.ToString.Substring(6, 2) & "/" & str_Date.ToString.Substring(4, 2) & "/" & str_Date.ToString.Substring(0, 4)

    '        If Len(xSplit) = 0 Then
    '            ConvertResult = String.Format(TempConvert, "MM/dd/yyyy")
    '        Else
    '            ConvertResult = String.Format(TempConvert, "MM" & Trim(xSplit) & "dd" & Trim(xSplit) & "yyyy")
    '        End If
    '        Return ConvertResult
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function


#Region "Delete"
    Public Shared Function DeleteFromTable(ByVal deleteItems As String, ByVal table_name As String) As Boolean
        Dim dtFields = PublicFunctions.getTableFields(table_name)
        Dim primary = PublicFunctions.get_primary_key(dtFields)
        Dim query = "Delete from " + table_name + " where " + primary + " = " + deleteItems
        If DBManager.ExcuteQuery(query) <> -1 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
#Region "SMS"
    Shared Function DoSendSMS(ByVal MobilNumbers As String, ByVal MobileMessage As String, ByVal id As String, ByVal dt As DataTable) As String
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        ' Dim dt As New DataTable
        Try
            ' dt = DBManager.Getdatatable("select * from tblsms_settings where comp_id=" + "(select isNULL(comp_id,0)  from tblUsers where isNull(deleted,0) != 1 and User_PhoneNumber ='" + MobilNumbers + "')")
            If dt.Rows.Count <> 0 Then
                Dim UserName As String
                UserName = dt.Rows(0).Item("user_name").ToString()
                Dim UserPassword As String
                UserPassword = dt.Rows(0).Item("password").ToString()
                Dim Sender_name As String
                Sender_name = dt.Rows(0).Item("company_name").ToString()
                Dim STR_url As String = ""
                STR_url = dt.Rows(0).Item("url").ToString()
                Dim url As String = STR_url & "?username=" & UserName & "&password=" & UserPassword & "&numbers=" &
                    MobilNumbers & "&sender=" & Sender_name & "&message=" & MobileMessage & "&unicode=E&return=string"
                Try
                    request = DirectCast(WebRequest.Create(url), HttpWebRequest)
                    response = DirectCast(request.GetResponse(), HttpWebResponse)
                    reader = New StreamReader(response.GetResponseStream())
                    DoSendSMS = reader.ReadToEnd
                Catch
                    DoSendSMS = Err.Description
                End Try
                If DoSendSMS = "تم استلام الارقام بنجاح" Then
                    If id <> "" Then
                        DBManager.ExcuteQuery("update tblsms_archive set Send=1 where id=" + id)
                    End If
                End If
                If Not IsNumeric(DoSendSMS) Then
                    DoSendSMS = DoSendSMS
                    Exit Function
                Else
                    If DoSendSMS = 1 Then
                        DoSendSMS = "Êã ÈäÌÇÍ"
                    End If
                End If

                Return DoSendSMS
            End If

            Return "يرجى إعادة ضبط إعدادات رسائل الجوال"
        Catch ex As Exception
            Return "يرجى إعادة ضبط إعدادات رسائل الجوال"
        End Try


    End Function

#End Region
#Region "save recieve notification and SMS"
    Public Shared Function save_recieve_not_SMS(ByRef _sqlconn As SqlConnection, ByRef _sqltrans As SqlTransaction,
ByVal day As String, ByVal RefCode As String, ByVal deliverer_data As String(), ByVal reciever_data As String(), ByVal advisor_data As String()) As Boolean

        Dim dayBefore = (DateTime.ParseExact(day, "dd/MM/yyyy", Nothing)).AddDays(-1)
        Dim dictNot As New Dictionary(Of String, Object)
        dictNot.Add("RefCode", RefCode)
        dictNot.Add("RefType", 2)
        dictNot.Add("NotTitle", "تاكيداستلام وتسليم")
        dictNot.Add("Date", day)
        dictNot.Add("AssignedTo", deliverer_data(1))
        dictNot.Add("CreatedBy", LoginInfo.GetUser__Id())
        dictNot.Add("Remarks", "اليوم لديك موعد تسليم برجاء التاكد")
        dictNot.Add("FormUrl", "Aslah_Module/Calender.aspx?id=" + RefCode)

        If Not PublicFunctions.TransUpdateInsert(dictNot, "tblNotifications", "", _sqlconn, _sqltrans) Then
            Return False
        End If
        dictNot("AssignedTo") = reciever_data(1)
        dictNot("Remarks") = "اليوم لديك موعداستلام برجاء التاكد"
        If Not PublicFunctions.TransUpdateInsert(dictNot, "tblNotifications", "", _sqlconn, _sqltrans) Then
            Return False
        End If
        dictNot("Date") = dayBefore
        dictNot("RefType") = 1
        dictNot("NotTitle") = "تأكيد جاهزيةاستلام وتسليم"
        dictNot("Remarks") = "نذكرك ب معاداستلام غدا برجاء التاكد"
        If Not PublicFunctions.TransUpdateInsert(dictNot, "tblNotifications", "", _sqlconn, _sqltrans) Then
            Return False
        End If
        dictNot("AssignedTo") = deliverer_data(1)
        dictNot("Remarks") = "نذكرك ب معاد تسليم غدا برجاء التاكد"
        If Not PublicFunctions.TransUpdateInsert(dictNot, "tblNotifications", "", _sqlconn, _sqltrans) Then
            Return False
        End If
        dictNot("AssignedTo") = advisor_data(0)
        dictNot("RefType") = 4
        dictNot("Remarks") = "تسليم واستلام غدا " + deliverer_data(0) + " و " + reciever_data(0)
        If Not PublicFunctions.TransUpdateInsert(dictNot, "tblNotifications", "", _sqlconn, _sqltrans) Then
            Return False
        End If
        dictNot("Date") = day
        dictNot("RefType") = 3
        dictNot("NotTitle") = "تاكيداستلام وتسليم"
        dictNot("Remarks") = "تسليم واستلام اليوم " + deliverer_data(0) + " و " + reciever_data(0)
        If Not PublicFunctions.TransUpdateInsert(dictNot, "tblNotifications", "", _sqlconn, _sqltrans) Then
            Return False
        End If

        Dim dic_sms_archive As New Dictionary(Of String, Object)
        If LoginInfo.SendSMS() Then
            dic_sms_archive.Add("user_id", LoginInfo.GetUser__Id())
            dic_sms_archive.Add("Message", "")
            dic_sms_archive.Add("Send_To", "")
            dic_sms_archive.Add("date_m", day)
            dic_sms_archive.Add("event_id", RefCode)
            dic_sms_archive.Add("Type", "recieve_delivery")
            dic_sms_archive.Add("comp_id", LoginInfo.GetComp_id())
            dic_sms_archive("Message") = "عزيزي المستفيد اليوم لديكم موعد تسليم واستلام لأطفالكم للمزيد ادخل على الرابط http://apps.maaen.org.sa الاصلاح الأسري جمعية معين"
            dic_sms_archive("Send_To") = deliverer_data(2)
            If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                Return False
            End If
            dic_sms_archive("Send_To") = reciever_data(2)
            If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                Return False
            End If
            dic_sms_archive("Message") = "تسليم واستلام اليوم " + deliverer_data(0) + " و " + reciever_data(0)
            dic_sms_archive("Send_To") = advisor_data(1)
            If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                Return False
            End If
            dic_sms_archive("date_m") = dayBefore
            dic_sms_archive("Message") = "عزيزي المستفيد غدا لديكم موعد تسليم واستلام لأطفالكم للمزيد ادخل على الرابط http://apps.maaen.org.sa الاصلاح الأسري جمعية معين"
            dic_sms_archive("Send_To") = deliverer_data(2)
            If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                Return False
            End If
            dic_sms_archive("Send_To") = reciever_data(2)
            If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                Return False
            End If
            dic_sms_archive("Message") = "تسليم واستلام غدا " + deliverer_data(0) + " و " + reciever_data(0)
            dic_sms_archive("Send_To") = advisor_data(1)
            If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                Return False
            End If
        End If
        Return True
    End Function
#End Region

End Class