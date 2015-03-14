Imports System.Text
Imports System.Security.Cryptography

Module Login

    Dim passwordHash As String
    Dim username As String
    Dim loginLocation As String = My.Computer.FileSystem.CombinePath(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JLC\TrainingLog\login")

    Public Sub createDirectory()
        My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.GetParentPath(loginLocation))
    End Sub

    Public Sub saveLogin(password As String, username As String)
        My.Computer.FileSystem.WriteAllText(Login.loginLocation, username & vbCrLf & Login.GenerateHash(password), False)
    End Sub

    Public Function loadLogin()
        Try
            Dim loginFile = My.Computer.FileSystem.ReadAllText(Login.loginLocation)
            Dim temp = loginFile.Split(vbCrLf)

            Console.WriteLine(temp.Length)
            Login.username = temp(0)
            Login.passwordHash = temp(1).Substring(1, temp(1).Length - 1)
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
        If username = Login.username And GenerateHash(password) = Login.passwordHash Then
            Return True
        Else
            Console.WriteLine(username & " = " & Login.username)
            Console.WriteLine(GenerateHash(password) & " = " & Login.passwordHash)
            Return False
        End If

    End Function



End Module





