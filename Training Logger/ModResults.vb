Module ModResults

    'set variables
    Public activites() As String = {"Swimming", "Cycling", "Running"}
    Dim allResults() As result
    Dim hasLoaded As Boolean = False

    'set as %appdata%/JLC/TrainingLogger/results
    Dim resultsLocation As String = My.Computer.FileSystem.CombinePath(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JLC\TrainingLog\results")

    Public Class result
        'set variables
        Dim resultID As Integer
        Dim userID As Integer
        Dim timestamp As Date
        Dim time As Decimal
        Dim sport As String

        'accept arguments when the object is being created
        Public Sub New(resultID As Integer, userID As Integer, timestamp As Date, time As Decimal, sport As String)
            Me.resultID = resultID
            Me.userID = userID
            Me.timestamp = timestamp
            Me.time = time
            Me.sport = sport
        End Sub

        'return variables when asked for them
        Public Function getTime() As Decimal
            Return Me.time
        End Function

        Public Function getSport() As String
            Return Me.sport
        End Function

        Public Function getTimestamp() As Date
            Return Me.timestamp
        End Function

        Public Function getUserID() As Integer
            Return Me.userID
        End Function

        Public Function getResultID() As Integer
            Return Me.getResultID
        End Function

    End Class


    Private Sub loadResults()
        'set variables
        Dim resultsLines = My.Computer.FileSystem.ReadAllText(ModResults.resultsLocation).Split(vbLf)
        Dim lineItems() As String
        Dim maxUserID As Integer = 0

        'resize the results array 
        Array.Resize(allResults, resultsLines.Length - 1)

        'scan each line for data and add it to the array
        For lineIndex As Integer = 0 To resultsLines.Length - 1
            lineItems = resultsLines(lineIndex).Split(",")
            If lineItems.Length = 4 Then
                allResults(lineIndex) = New result(lineIndex, lineItems(0), DateTime.Parse(lineItems(1)), lineItems(2), lineItems(3))
            End If
        Next lineIndex
        hasLoaded = False
    End Sub

    Public Function getResults(userID As Integer) As Array
        'load the results if they aren't already loaded
        If Not hasLoaded Then
            loadResults()
        End If
        'create a variable containing data to return
        Dim retArray() As result = {}

        'search through results for results for a userID
        For resultID As Integer = 0 To allResults.Length - 1
            If allResults(resultID).getUserID() = userID Then
                Array.Resize(retArray, retArray.Length + 1)
                retArray(retArray.Length - 1) = allResults(resultID)
            End If
        Next
        Return retArray
    End Function

    'save a result to file
    Public Sub saveResult(userID As Integer, timestamp As DateTime, time As Decimal, sport As String)
        My.Computer.FileSystem.WriteAllText(resultsLocation, userID & "," & timestamp.ToString() & "," & time & "," & sport & vbLf, True)
    End Sub

    'check if the file exists already
    Public Function FileExists() As Boolean
        Return My.Computer.FileSystem.FileExists(resultsLocation)
    End Function
End Module

