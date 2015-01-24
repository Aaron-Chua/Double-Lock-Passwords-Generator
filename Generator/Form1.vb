Imports System.IO
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As DialogResult = SaveFileDialog1.ShowDialog()
        SaveFileDialog1.CheckPathExists = True
        SaveFileDialog1.AutoUpgradeEnabled = True
        If result = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = SaveFileDialog1.FileName
        End If
    End Sub
    Private Sub combin()
        Dim possible As Integer
        If CheckBox1.Checked = False Then
            possible = Val(ComboBox1.Text) * Val(ComboBox2.Text)
        Else
            possible = arrangement(Val(ComboBox1.Text) + Val(ComboBox2.Text), 2)
        End If
        Label5.Text = "Number of possible password combination is : " + Str(possible)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call combin()
    End Sub
    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        Call combin()
    End Sub
    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        Call combin()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Call combin()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim lockALength As Integer
        Dim lockBLength As Integer
        Dim lockACand As Integer
        Dim lockBCand As Integer
        Dim disorder As Boolean
        Dim path As String
        lockALength = Val(ComboBox3.Text)
        lockBLength = Val(ComboBox4.Text)
        lockACand = Val(ComboBox1.Text)
        lockBCand = Val(ComboBox2.Text)
        disorder = CheckBox1.Checked
        path = TextBox1.Text
        Dim chooseA As Integer
        Dim chooseB As Integer
        Dim temp As String
        Label7.Text = ""
        Label8.Text = ""
        Label4.Visible = False
        Label6.Visible = False
        Label7.Visible = False
        Label8.Visible = False
        If lockALength > 9 Or lockALength < 1 Or lockBLength > 9 Or lockALength < 1 Then
            MsgBox("Password length must between 1 to 9.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        ElseIf lockACand > 50 Or lockACand < 1 Or lockBCand > 50 Or lockBCand < 1 Then
            MsgBox("Number of candidate passwords must between 1 to 50 for each lock.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        ElseIf TextBox1.Text = "Please select a target file from browse button." Then
            MsgBox("No vaild output file specified.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Else
            If Not disorder Then
                Dim lockA(110, 10) As Integer
                Dim lockB(110, 10) As Integer
                Dim file As New FileStream(path, FileMode.Append)
                Dim out As StreamWriter = New StreamWriter(file)
                out.WriteLine(DateString + "  " + TimeString)
                out.WriteLine("Description: " + TextBox2.Text)
                out.WriteLine("====Lock A====")
                out.WriteLine("No." + vbTab + vbTab + "password")
                For j = 1 To lockACand
                    temp = ""
                    For i = 1 To lockALength
                        Randomize(TimeOfDay.ToOADate())
                        lockA(j, i) = CInt(Math.Ceiling(Rnd() * 9))
                        temp = temp + Str(lockA(j, i)).Substring(1)
                    Next
                    out.WriteLine(Str(j) + vbTab + vbTab + temp)
                Next
                out.WriteLine("====Lock B====")
                out.WriteLine("No." + vbTab + vbTab + "password")
                For j = 1 To lockBCand
                    temp = ""
                    For i = 1 To lockBLength
                        Randomize(TimeOfDay.ToOADate())
                        lockB(j, i) = CInt(Math.Ceiling(Rnd() * 9))
                        temp = temp + Str(lockB(j, i)).Substring(1)
                    Next
                    out.WriteLine(Str(j) + vbTab + vbTab + temp)
                Next
                Randomize(TimeOfDay.ToOADate())
                chooseA = CInt(Math.Ceiling(Rnd() * lockACand - 1)) + 1
                Randomize(TimeOfDay.ToOADate())
                chooseB = CInt(Math.Ceiling(Rnd() * lockBCand - 1)) + 1
                For i = 1 To lockALength
                    Label7.Text = Label7.Text + Str(lockA(chooseA, i)).Substring(1)
                Next
                For i = 1 To lockBLength
                    Label8.Text = Label8.Text + Str(lockB(chooseB, i)).Substring(1)
                Next
                out.WriteLine()
                out.Close()
                file.Close()
                Label4.Visible = True
                Label6.Visible = True
                Label7.Visible = True
                Label8.Visible = True
            Else
                If lockALength <> lockBLength Then
                    MsgBox("Password length of lock A and B must be same in strongly disorder mode (otherwise will be useless).", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                Else
                    Dim lock(110, 10) As Integer
                    Dim file As New FileStream(path, FileMode.Append)
                    Dim out As StreamWriter = New StreamWriter(file)
                    out.WriteLine(DateString + "  " + TimeString)
                    out.WriteLine("Description: " + TextBox2.Text)
                    out.WriteLine("====Lock A and B====")
                    out.WriteLine("No." + vbTab + vbTab + "password")
                    For j = 1 To (lockACand + lockBCand)
                        temp = ""
                        For i = 1 To Math.Max(lockALength, lockBLength)
                            Randomize(TimeOfDay.ToOADate())
                            lock(j, i) = CInt(Math.Ceiling(Rnd() * 9))
                            temp = temp + Str(lock(j, i)).Substring(1)
                        Next
                        out.WriteLine(Str(j) + vbTab + vbTab + temp)
                    Next
                    Randomize(TimeOfDay.ToOADate())
                    chooseA = CInt(Math.Ceiling(Rnd() * (lockACand + lockBCand) - 1)) + 1
                    Randomize(TimeOfDay.ToOADate())
                    chooseB = CInt(Math.Ceiling(Rnd() * (lockACand + lockBCand) - 1)) + 1
                    While chooseA = chooseB
                        Randomize(TimeOfDay.ToOADate())
                        chooseB = CInt(Math.Ceiling(Rnd() * (lockACand + lockBCand) - 1)) + 1
                    End While
                    For i = 1 To lockALength
                        Label7.Text = Label7.Text + Str(lock(chooseA, i)).Substring(1)
                    Next
                    For i = 1 To lockBLength
                        Label8.Text = Label8.Text + Str(lock(chooseB, i)).Substring(1)
                    Next
                    out.WriteLine()
                    out.Close()
                    file.Close()
                    Label4.Visible = True
                    Label6.Visible = True
                    Label7.Visible = True
                    Label8.Visible = True
                End If
            End If
        End If
    End Sub
    Private Function arrangement(ByVal big As Integer, ByVal small As Integer) As Integer
        Dim result As Integer
        result = 1
        For i = big - small + 1 To big
            result = result * i
        Next
        Return result
    End Function
End Class
