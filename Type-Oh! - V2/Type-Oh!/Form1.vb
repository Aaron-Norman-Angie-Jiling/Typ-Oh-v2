Imports System.Net
Public Class Form1
    Dim length As Integer = 0
    Dim inputLength As Integer = 0
    Dim finalScore As String
    Dim copyTxt = " "
    Dim input = " "
    Dim score As Integer = 0
    Dim wordsMissed As Integer
    Dim timeInt As Integer = 0
    Dim time As Double = 0
    Dim quake As New Random
    Dim rating As String

    Function countWords()
        copyTxt = Split(lblCopytxt.Text, " ") 'Everytime there is a space in the label, a new word in the array would be created
        length = UBound(copyTxt) - LBound(copyTxt) + 1 'Gets the number of words in the label
        input = Split((txtInput.Text), " ") 'Everytime there is a space in the textbox, a new word in the array would be created
        inputLength = UBound(input) - LBound(input) + 1 'Gets the number of words typed by user
    End Function

    Function rateScore()
        Select Case score / length
            Case = 1
                rating = "SS"
                MessageBox.Show("You typed perfectly! Well done!")
            Case > 0.95
                rating = "S"
                MessageBox.Show("You extremely well!")
            Case < 0.5
                MessageBox.Show("you did bad")
        End Select
    End Function
    Function wordPlace()
        Dim i As Integer = 0

        For i = 0 To inputLength - 1

            'If i = inputLength - 1 Then
            'Exit For
            'End If
            If copyTxt(i) = input(i) Then
                score = score + 1
            End If
            'MessageBox.Show(x(i))
            'MessageBox.Show(copyTxt(0), input(0))
        Next

    End Function

    Public Shared value As Integer = 5
    Dim numWrongWords As Integer = 0
    Dim yea As Integer = value

    Private Sub btnNewText_Click(sender As Object, e As EventArgs) Handles btnNewText.Click
        txtInput.Enabled = True
        btnCheck.Enabled = True
        Dim txtID As Integer
        txtID = Int((10 - 2) * Rnd())
        Select Case txtID
            Case 1
                lblCopytxt.Text = ""
        End Select
        'copyTxt = Split(Label1.Text, " ")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = (My.Computer.Screen.WorkingArea.Height / 2) - (Me.Height / 2)
        Me.Left = (My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtInput.TextChanged
        If txtInput.Text <> "" Then
            tmrTime.Enabled = True
            If quakeStatus = True Then
                tmrShake.Enabled = True
            End If
            Dim ranNum1 As Integer
            Dim ranNum2 As Integer
            Dim ranNum3 As Integer
            ranNum1 = Int((255 - 2) * Rnd())
            ranNum2 = Int((255 - 2) * Rnd())
            ranNum3 = Int((255 - 2) * Rnd())
            Me.BackColor = Color.FromArgb((ranNum1), (ranNum2), (ranNum3))
        End If
        countWords()
        wordsMissed = (length - inputLength)
        If wordsMissed < 0 Then
            lblNotAt.Text = ("You typed " + CStr(Math.Abs(wordsMissed)) + " extra words")
        Else
            lblNotAt.Text = ("Words left: " + CStr(wordsMissed))
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtInput.KeyDown
        If e.KeyCode = Keys.Enter Then
            wordPlace()
            tmrShake.Enabled = False
        End If
    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        If txtInput.Text = "" Then
            MessageBox.Show("Type sompthing")
        End If
        'input = Split((txtInput.Text), " ")
        wordPlace()
        MessageBox.Show(score)
        finalScore = ("Score: " + CStr(score))
        lblScore.Text = finalScore
        'MessageBox.Show(length, inputLength)
        wordsMissed = (length - inputLength)
        rateScore()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        End
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtInput.Text = ""
    End Sub

    Private Sub btnGetApi_Click(sender As Object, e As EventArgs) Handles btnGetApi.Click
        'Code sourced from https://www.youtube.com/watch?v=FE4PdSnox1w
        txtInput.Enabled = True
        Try
            Dim Request As HttpWebRequest = HttpWebRequest.Create("Http://numbersapi.com/random/trivia")
            Request.Proxy = Nothing 'speeds up request
            'Request.UserAgent = "Test"

            Dim response As HttpWebResponse = Request.GetResponse
            Dim responsestream As System.IO.Stream = response.GetResponseStream

            Dim streamReader As New System.IO.StreamReader(responsestream)
            Dim data As String = streamReader.ReadToEnd
            streamReader.Close()

            lblCopytxt.Text = data
        Catch ex As Exception
        End Try
        lblCopytxt.Left = (Me.Width / 2) - (lblCopytxt.Width / 2)
        txtInput.Text = ""
        countWords()
        lblNotAt.Text = ("Words left: " + CStr(length))
        'copyTxt = Split(Label1.Text, " ")
        lblInstructions.Visible = False
        btnCheck.Enabled = True

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Form2.Show()
        Me.Hide()
        lblCopytxt.Text = ""
        score = 0
        time = 0
        wordsMissed = 0
    End Sub
    Private Sub tmrTime_Tick(sender As Object, e As EventArgs) Handles tmrTime.Tick
        timeInt = (timeInt + 1)
        time = timeInt / 10

        lblTime.Text = CStr(time) + " seconds"
    End Sub
    Private Sub tmrShake_Tick(sender As Object, e As EventArgs) Handles tmrShake.Tick 'code inspired from https://www.youtube.com/watch?v=pCR-EkOx-WA
        Me.Top = (My.Computer.Screen.WorkingArea.Height / 2) - (Me.Height / 2) + quake.Next(-40, 40)
        Me.Left = (My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2) + quake.Next(-40, 40)
        If quakeStatus = False Then
            tmrShake.Enabled = False
            Me.Top = (My.Computer.Screen.WorkingArea.Height / 2) - (Me.Height / 2)
            Me.Left = (My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2)
        End If
    End Sub

End Class
