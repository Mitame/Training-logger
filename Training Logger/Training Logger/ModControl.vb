Imports System.Text
Imports System.Security.Cryptography

Module ModControl

    Public Class Login
        Shared passwordHash As String
        Shared username As String
        Shared loginLocation As String = My.Computer.FileSystem.CombinePath(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JLC\TrainingLog\login")

        Public Shared Sub createDirectory()
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.GetParentPath(loginLocation))
        End Sub

        Public Shared Sub saveLogin(password As String, username As String)
            My.Computer.FileSystem.WriteAllText(Login.loginLocation, username & vbCrLf & Login.GenerateHash(password), False)
        End Sub

        Public Shared Function loadLogin()
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
        Private Shared Function GenerateHash(ByVal SourceText As String) As String
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

        Public Shared Function checkLogin(username As String, password As String)
            If username = Login.username And GenerateHash(password) = Login.passwordHash Then
                Return True
            Else
                Console.WriteLine(username & " = " & Login.username)
                Console.WriteLine(GenerateHash(password) & " = " & Login.passwordHash)
                Return False
            End If

        End Function


    End Class

    
End Module





