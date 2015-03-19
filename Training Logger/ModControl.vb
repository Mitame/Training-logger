Imports System.Text
Imports System.Security.Cryptography

Module Login

    Dim passwordHash() As String
    Dim username() As String
    Dim loginLocation As String = My.Computer.FileSystem.CombinePath(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JLC\TrainingLog\login")

    Public Class User
        Public id As Integer
        Public username As String
        Public passwordHash As String

        Public Sub New(userID As Integer, username As String, passwordHash As String)
            Me.id = userID
            Me.username = username
            Me.passwordHash = passwordHash
        End Sub


    End Class

    Public Sub createDirectory()
        My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.GetParentPath(loginLocation))
    End Sub

    Public Sub saveLogin(password As String, username As String)
        My.Computer.FileSystem.WriteAllText(Login.loginLocation, username & vbCrLf & Login.GenerateHash(password), False)
    End Sub

    Public Function loadLogin()
        Try
            Dim loginFile = My.Computer.FileSystem.ReadAllText(Login.loginLocation)
            Dim users = loginFile.Split(vbCrLf)
            Array.Resize(Login.username, users.Length)
            Array.Resize(Login.passwordHash, users.Length)
            Dim split() As String
            For userID As Integer = 0 To users.Length - 1
                If userID <> 0 Then
                    split = (users(userID).Substring(1, users(userID).Length - 1)).Split(",")
                Else
                    split = users(userID).Split(",")
                End If

                Login.username(userID) = split(0)
                Login.passwordHash(userID) = split(1)
                Console.WriteLine(userID & ", " & Login.username(userID) & ", " & Login.passwordHash(userID))
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
        'Create an encoding object to ensure the encoding standard for the source text
        Dim Ue As New UnicodeEncoding()
        'Retrieve a byte array based on the source text
        Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
        'Instantiate an MD5 Provider object
        Dim sha As New SHA256CryptoServiceProvider()
        'Compute the hash value from the source
        Dim ByteHash() As Byte = sha.ComputeHash(ByteSourceText)
        'And convert it to String format for return
        Return Convert.ToBase64String(ByteHash)
    End Function

    Public Function checkLogin(username As String, password As String)
        If Login.username.Contains(username) Then
            Dim userID = Array.IndexOf(Login.username, username)
            If Login.passwordHash(userID) = GenerateHash(password) Then
                Return New User(userID, Login.username(userID), Login.passwordHash(userID))
            Else
            End If
        End If
        Return New User(-1, "", "")
    End Function



End Module





