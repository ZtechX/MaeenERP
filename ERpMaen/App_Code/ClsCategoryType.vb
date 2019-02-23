Public Class ClsCategoryType
    Public Function ReturnLetter(ByVal Category As String)
        Select Case Category.Trim.ToLower
            Case "villa"
                Return "V"
            Case "building"
                Return "B"
            Case "apartment"
                Return "A"
            Case "store"
                Return "S"
            Case "show room"
                Return "R"
            Case "ware house"
                Return "W"
            Case "labor camp"
                Return "C"
            Case "hotel"
                Return "H"
            Case "shop"
                Return "P"
            Case "office"
                Return "F"
            Case "land"
                Return "L"
            Case "studio"
                Return "T"
            Case "flat"
                Return "A"
            Case "apartment/flat"
                Return ("AP")
        End Select
    End Function
End Class
