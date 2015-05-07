'import requirements
Imports System.Text
Imports System.Security.Cryptography

Public Module Login

    'create variables
    Dim passwordHash() As String
    Public username() As String
    Public users() As User

    'links to %appdata%/JLC/TrainingLogger/login
    Dim loginLocation As String = My.Computer.FileSystem.CombinePath(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JLC\TrainingLog\login")

    Public Class User
        'create varibales for the user
        Public id As Integer
        Public username As String
        Public passwordHash As String
        Public firstName As String
        Public lastName As String
        Public contact As String
        Public rank As Decimal

        'accept arguments to set users.
        Public Sub New(userID As Integer, username As String, passwordHash As String, firstName As String, lastName As String, contact As String)
            Me.id = userID
            Me.username = username
            Me.passwordHash = passwordHash
            If Not (IsNothing(firstName) Or IsNothing(lastName) Or IsNothing(contact)) Then
                Me.firstName = firstName
                Me.lastName = lastName
                Me.contact = contact
            End If
        End Sub


        Public Function getRank(Optional recalculate As Boolean = True) As Decimal
            'don't recalculate the value if it's not needed
            If Not IsNothing(Me.rank) And Not recalculate Then
                Return Me.rank
            End If

            'create variables and reset rank
            Dim results = ModResults.getResults(Me.id)
            Dim rankValue(ModResults.activites.Length) As Decimal
            Dim sportResults() As Decimal = {}
            Dim mean As Decimal = 0
            Dim sd As Decimal = 0
            Me.rank = 0


            'calculate for each sport
            For sportIndex As Integer = 0 To ModResults.activites.Length - 1
                Console.WriteLine("Calculating `" & ModResults.activites(sportIndex) & "` ranking for `" & Me.username & "`.")
                For resultIndex As Integer = 0 To results.Length - 1 'search through results for activity
                    If results(resultIndex).getSport() = ModResults.activites(sportIndex) Then
                        Array.Resize(sportResults, sportResults.Length + 1)
                        sportResults(sportResults.Length - 1) = results(resultIndex).getTime() 'add it to a temporary list
                    End If
                Next
                Try
                    'calculate the mean
                    For x As Integer = 0 To sportResults.Length - 1
                        mean += sportResults(x)
                    Next
                    mean = mean / sportResults.Length

                    'calculate the standard deviation
                    For x As Integer = 0 To sportResults.Length - 1
                        sd += (sportResults(x) - mean) ^ 2
                    Next
                    sd = sd / sportResults.Length
                    sd = Math.Sqrt(sd)

                    'add both values to the rank
                    rankValue(sportIndex) = sd + mean - sportResults.Length
                    Me.rank += rankValue(sportIndex)

                Catch e As System.DivideByZeroException
                    'if there are no results, add 10000 to the rank
                    Me.rank += 10000
                End Try
            Next


            'return the rank
            Return Me.rank
        End Function
    End Class

    'create the directory
    Public Sub createDirectory()
        My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.GetParentPath(loginLocation))
    End Sub

    'save the login to file
    Public Sub saveLogin(username As String, password As String, firstName As String, lastName As String, contact As String)
        My.Computer.FileSystem.WriteAllText(Login.loginLocation, username & "," & Login.GenerateHash(password) & "," & firstName & "," & lastName & "," & contact & vbLf, True)
    End Sub

    Public Function loadLogin()
        Try
            'create variables
            Dim loginFile = My.Computer.FileSystem.ReadAllText(Login.loginLocation)
            Dim users = loginFile.Split(vbLf)
            Dim split() As String

            'resize all arrays
            Array.Resize(Login.username, users.Length)
            Array.Resize(Login.passwordHash, users.Length)
            Array.Resize(Login.users, users.Length)

            'go through each line
            For userID As Integer = 0 To users.Length - 2
                split = users(userID).Split(",")
                If split.Length = 5 Then 'only add if it has enough fields
                    Login.username(userID) = split(0) '
                    Login.passwordHash(userID) = split(1)
                    Login.users(userID) = New User(userID, split(0), split(1), split(2), split(3), split(4))
                End If
            Next userID
        Catch exception As System.IO.DirectoryNotFoundException
            Login.createDirectory() 'create a directory and cause the source to create the initial user
            Return False
        Catch exception As System.IO.FileNotFoundException
            Return False 'create the first user
        End Try
        Return True

    End Function

    'generate SHA-256 hashes for passwords.
    Private Function GenerateHash(ByVal SourceText As String) As String
        Dim Ue As New UnicodeEncoding()
        Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
        Dim sha As New SHA256CryptoServiceProvider()
        Dim ByteHash() As Byte = sha.ComputeHash(ByteSourceText)
        Return Convert.ToBase64String(ByteHash)
    End Function


    Public Function checkLogin(username As String, password As String)
        'if the username is in the list of usernames
        If Login.username.Contains(username) Then
            'get the index of the username (the userID)
            Dim userID = Array.IndexOf(Login.username, username)

            'check the password hashes are the same
            If Login.passwordHash(userID) = GenerateHash(password) Then
                Return Login.users(userID)
            Else
            End If
        End If
        'if it does not return already, return an invalid user.
        Return New User(-1, "", "", "", "", "")
    End Function

    'return the user object specified by the userID
    Public Function getUser(userID As Integer) As User
        Return users(userID)
    End Function


    Public Sub editLogin(userID As Integer, username As String, password As String, firstName As String, lastName As String, contact As String)
        'copy the users file to memory and store the user being editted
        Dim lines = My.Computer.FileSystem.ReadAllText(loginLocation).Split(vbLf)
        Dim oldUser = users(userID)

        'change the username if it is not blank
        If username = "" Then
            username = oldUser.username
        End If

        'change the password if it is not blank
        If password = "" Then
            password = oldUser.passwordHash
        Else
            password = GenerateHash(password)
        End If

        'change the firstname if it is not blank
        If firstName = "" Then
            firstName = oldUser.firstName
        End If

        'change the last name if it is not blank
        If lastName = "" Then
            lastName = oldUser.lastName
        End If

        'change the contact if it is not blank
        If contact = "" Then
            contact = oldUser.contact
        End If

        'replace the line in the file store in memory
        lines(userID) = username & "," & password & "," & firstName & "," & lastName & "," & contact & vbLf
        My.Computer.FileSystem.WriteAllText(loginLocation, Join(lines, vbLf), False) 'write over the old file with the new contents.
    End Sub
End Module