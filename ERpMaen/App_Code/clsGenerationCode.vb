
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer

Public Class clsGenerattionCode

    Dim _sqltransaction As SqlTransaction

    Public _sqlConnection As New SqlConnection(DBManager.GetConnectionString)


    Property __SqlConnection As SqlConnection
        Get
            Return _sqlConnection
        End Get
        Set(value As SqlConnection)
            _sqlConnection = value
        End Set
    End Property

    Public Function GenerationCodeForAll(ByVal FirstCharsArray() As String, ByVal TableName As String, ByVal FinalLength As Integer, ByVal ColmunName As String) As Object

        Dim FirstChars As String = ""
        If FirstCharsArray.Length > 0 Then FirstChars = FirstCharsArray(0)

        Dim FirstCLength As Integer = FirstChars.Length

        Dim GCode As String = vbNullString

        Dim strSql As String

        Dim MinusLength As Integer = 0

        If FirstChars.Length = 0 Then MinusLength = 0 Else MinusLength = 1

        strSql = " Select max(cast(substring(" & ColmunName & "," & FirstChars.Length + 1 & ",(len(" & ColmunName & ")-" & MinusLength & "))as int) )" & _
                 " from  " & TableName & " " & _
                 " where len(" & ColmunName & ")=" & FinalLength & " " & _
                 " and ( ( " & ColmunName & " like '" & FirstChars & "%' )  " & GetOtherFirstCharsConditions(ColmunName, FirstCharsArray) & " )"
        Dim sqlCommand As New SqlCommand(strSql, _sqlConnection)
        Dim Result As String

        Try
            _sqlConnection.Open()
            Result = sqlCommand.ExecuteScalar
        Catch ex As Exception
            Result = vbNullString
        Finally
            _sqlConnection.Close()
        End Try


        If Result = vbNullString Then
            GCode = UCase(FirstChars) + "1".PadLeft(FinalLength - FirstCLength, "0")
            Return GCode
        Else

            Dim maxvalue As String = (CInt(Result.Substring(0, (Result.Length))) + 1).ToString
            GCode = UCase(FirstChars) + maxvalue.PadLeft(FinalLength - FirstCLength, "0")

        End If
        Return GCode
    End Function

    Public Shared Function GenerationCodeForAll(ByVal _SqlConnection As SqlConnection, ByVal _SqlTransaction As SqlTransaction, ByVal FirstCharsArray() As String, ByVal TableName As String, ByVal FinalLength As Integer, ByVal ColmunName As String) As Object

        Dim FirstChars As String = ""
        If FirstCharsArray.Length > 0 Then FirstChars = FirstCharsArray(0)

        Dim FirstCLength As Integer = FirstChars.Length

        Dim GCode As String = vbNullString
        Dim strSql As String
        Dim MinusLength As Integer = 0

        If FirstChars.Length = 0 Then MinusLength = 0 Else MinusLength = 1


        strSql = " Select max(cast(substring(" & ColmunName & "," & FirstChars.Length + 1 & ",(len(" & ColmunName & ")-" & MinusLength & "))as int) )" & _
                 " from  " & TableName & " " & _
                 " where len(" & ColmunName & ")=" & FinalLength & " " & _
                 " and ( ( " & ColmunName & " like '" & FirstChars & "%' )  " & GetOtherFirstCharsConditions(ColmunName, FirstCharsArray) & " ) "


        Dim sqlCommand As New SqlCommand(strSql, _SqlConnection, _SqlTransaction)
        Dim Result As String

        Try
            Result = sqlCommand.ExecuteScalar
        Catch ex As Exception
            Result = vbNullString
        End Try
        If Result = vbNullString Then


            GCode = UCase(FirstChars) + "1".PadLeft(FinalLength - FirstCLength, "0")

            Return GCode
        Else

            Dim maxvalue As String = (CInt(Result.Substring(0, (Result.Length))) + 1).ToString

            GCode = UCase(FirstChars) + maxvalue.PadLeft(FinalLength - FirstCLength, "0")

        End If
        Return GCode
    End Function

    Public Function GenerationCodeForAll(ByVal FirstChars As String, ByVal TableName As String, ByVal FinalLength As Integer, ByVal ColmunName As String) As Object

        Dim FirstCLength As Integer = FirstChars.Length

        Dim GCode As String = vbNullString

        Dim strSql As String

        Dim MinusLength As Integer = 0

        If FirstChars.Length = 0 Then MinusLength = 0 Else MinusLength = 1

        strSql = " Select max(cast(substring(" & ColmunName & "," & FirstChars.Length + 1 & ",(len(" & ColmunName & ")-" & MinusLength & "))as int) )" & _
                 " from  " & TableName & " " & _
                 " where len(" & ColmunName & ")=" & FinalLength & " " & _
                 " and (  " & ColmunName & " like '" & FirstChars & "%'   )"
        Dim sqlCommand As New SqlCommand(strSql, _sqlConnection)
        Dim Result As String

        Try
            _sqlConnection.Open()
            Result = sqlCommand.ExecuteScalar
        Catch ex As Exception
            Result = vbNullString
        Finally
            _sqlConnection.Close()
        End Try


        If Result = vbNullString Then
            GCode = UCase(FirstChars) + "1".PadLeft(FinalLength - FirstCLength, "0")
            Return GCode
        Else

            Dim maxvalue As String = (CInt(Result.Substring(0, (Result.Length))) + 1).ToString
            GCode = UCase(FirstChars) + maxvalue.PadLeft(FinalLength - FirstCLength, "0")

        End If
        Return GCode
    End Function

    Public Shared Function GenerationCodeForAll(ByVal _SqlConnection As SqlConnection, ByVal _SqlTransaction As SqlTransaction, ByVal FirstChars As String, ByVal TableName As String, ByVal FinalLength As Integer, ByVal ColmunName As String) As Object

    

        Dim FirstCLength As Integer = FirstChars.Length

        Dim GCode As String = vbNullString
        Dim strSql As String
        Dim MinusLength As Integer = 0

        If FirstChars.Length = 0 Then MinusLength = 0 Else MinusLength = 1


        strSql = " Select max(cast(substring(" & ColmunName & "," & FirstChars.Length + 1 & ",(len(" & ColmunName & ")-" & MinusLength & "))as int) )" & _
                 " from  " & TableName & " " & _
                 " where len(" & ColmunName & ")=" & FinalLength & " " & _
                 " and ( " & ColmunName & " like '" & FirstChars & "%' ) ) "


        Dim sqlCommand As New SqlCommand(strSql, _SqlConnection, _SqlTransaction)
        Dim Result As String

        Try
            Result = sqlCommand.ExecuteScalar
        Catch ex As Exception
            Result = vbNullString
        End Try
        If Result = vbNullString Then
            GCode = UCase(FirstChars) + "1".PadLeft(FinalLength - FirstCLength, "0")

            Return GCode
        Else

            Dim maxvalue As String = (CInt(Result.Substring(0, (Result.Length))) + 1).ToString

            GCode = UCase(FirstChars) + maxvalue.PadLeft(FinalLength - FirstCLength, "0")

        End If
        Return GCode
    End Function


    Private Shared Function GetOtherFirstCharsConditions(ColmunName As String, FirstCharsArray() As String) As String

        Dim Query As String = ""

        For i As Integer = 1 To FirstCharsArray.Length - 1
            Query += " or  ( '" & FirstCharsArray(i) & "' <>'' and " & ColmunName & " like '" & FirstCharsArray(i) & "%' )  "
        Next

        Return Query

    End Function



End Class