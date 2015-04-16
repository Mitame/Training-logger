Imports System.Text
Imports System.Security.Cryptography

Public Module Login

    Dim passwordHash() As String
    Public username() As String
    Public users() As User
    Dim loginLocation As String = My.Computer.FileSystem.CombinePath(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JLC\TrainingLog\login")

    Public Class User
        Public id As Integer
        Public username As String
        Public passwordHash As String
        Public firstName As String
        Public lastName As String
        Public contact As String

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
    End Class

    Public Sub createDirectory()
        My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.GetParentPath(loginLocation))
    End Sub

    Public Sub saveLogin(username As String, password As String, firstName As String, lastName As String, contact As String)
        My.Computer.FileSystem.WriteAllText(Login.loginLocation, username & "," & Login.GenerateHash(password) & "," & firstName & "," & lastName & "," & contact & vbLf, True)
    End Sub

    Public Function loadLogin()
        Try
            Dim loginFile = My.Computer.FileSystem.ReadAllText(Login.loginLocation)
            Dim users = loginFile.Split(vbLf)
            Array.Resize(Login.username, users.Length)
            Array.Resize(Login.passwordHash, users.Length)
            Array.Resize(Login.users, users.Length)
            Dim split() As String
            For userID As Integer = 0 To users.Length - 2
                'If userID <> 0 Then
                'split = (users(userID).Substring(1, users(userID).Length - 1)).Split(",")
                'Else
                split = users(userID).Split(",")
                'End If
                If split.Length = 5 Then
                    Login.username(userID) = split(0)
                    Login.passwordHash(userID) = split(1)
                    Login.users(userID) = New User(userID, split(0), split(1), split(2), split(3), split(4))
                End If
            Next userID
        Catch exception As System.IO.DirectoryNotFoundException
            Login.createDirectory()
            Return False
        Catch exception As System.IO.FileNotFoundException
            Return False
        End Try
        Return True

    End Function

    Private Function GenerateHash(ByVal SourceText As String) As String
        Dim Ue As New UnicodeEncoding()
        Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
        Dim sha As New SHA256CryptoServiceProvider()
        Dim ByteHash() As Byte = sha.ComputeHash(ByteSourceText)
        Return Convert.ToBase64String(ByteHash)
    End Function

    Public Function checkLogin(username As String, password As String)
        If Login.username.Contains(username) Then
            Dim userID = Array.IndexOf(Login.username, username)
            If Login.passwordHash(userID) = GenerateHash(password) Then
                Return Login.users(userID)
            Else
            End If
        End If
        Return New User(-1, "", "", "", "", "")
    End Function

    Public Function getUser(userID As Integer) As User
        Return users(userID)
    End Function

    Public Sub editLogin(userID As Integer, username As String, password As String, firstName As String, lastName As String, contact As String)
        Dim lines = My.Computer.FileSystem.ReadAllText(loginLocation).Split(vbLf)
        Dim oldUser = users(userID)

        If username = "" Then
            username = oldUser.username
        End If

        If password = "" Then
            password = oldUser.passwordHash
        Else
            password = GenerateHash(password)
        End If

        If firstName = "" Then
            firstName = oldUser.firstName
        End If

        If lastName = "" Then
            lastName = oldUser.lastName
        End If

        If contact = "" Then
            contact = oldUser.contact
        End If

        lines(userID) = username & "," & password & "," & firstName & "," & lastName & "," & contact & vbLf
        My.Computer.FileSystem.WriteAllText(loginLocation, Join(lines, vbLf), False)
    End Sub
End Module





