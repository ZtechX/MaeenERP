Imports Microsoft.VisualBasic

Public Class clsGeneralFunctions


    Public Shared Function DateFormat(ByVal Value As Object) As Date

        Return Date.ParseExact(Value, clsGeneralVariables.DataFormat, Nothing)

    End Function


End Class
