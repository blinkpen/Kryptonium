Imports System.IO
Imports System.Text

Public Class Form7
    Dim txtbox As Control
    Dim txt As String
    Dim ORIGINAL As String
    Dim T1 As Integer = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.ColorDialog1.ShowDialog()

        gen_color()
    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ORIGINAL = TextBox3.Text

        PictureBox1.BackColor = Form1.ColorDialog1.Color
        gen_color()
    End Sub

    Public Sub gen_color()
        Dim color1 As Color = Color.FromArgb(Form1.ColorDialog1.Color.ToArgb)
        Dim color2 As Color

        Dim ass As Color = color1
        ass = Color.FromArgb(255, 255 - ass.R, 255 - ass.G, 255 - ass.B)
        color2 = ass
        PictureBox1.BackColor = Form1.ColorDialog1.Color
        PictureBox2.BackColor = color2

        TextBox1.Text = PictureBox1.BackColor.R & ", " & PictureBox1.BackColor.G & ", " & PictureBox1.BackColor.B
        TextBox2.Text = PictureBox2.BackColor.R & ", " & PictureBox2.BackColor.G & ", " & PictureBox2.BackColor.B

        Dim CodeCodeInHex As String = color1.ToArgb().ToString("X")
        CodeCodeInHex = CodeCodeInHex.Remove(0, 2)
        TextBox3.Text = "#" & CodeCodeInHex

        Dim CodeCodeInHex2 As String = color2.ToArgb().ToString("X")
        CodeCodeInHex2 = CodeCodeInHex2.Remove(0, 2)
        TextBox4.Text = "#" & CodeCodeInHex2

        ORIGINAL = TextBox3.Text


        Dim CodeCodeInHexZ As String = Form1.ColorDialog1.Color.ToArgb().ToString("X")
        CodeCodeInHexZ = CodeCodeInHexZ.Remove(0, 2)
        Form1.BGCOLOR.Text = "#" & CodeCodeInHexZ
        Form1.ToolStripButton7.BackColor = Form1.ColorDialog1.Color

        Form1.ToolStripTextBox1.Text = Form1.ColorDialog1.Color.A & ", " & Form1.ColorDialog1.Color.R & ", " & Form1.ColorDialog1.Color.G & ", " & Form1.ColorDialog1.Color.B
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Timer2.Start()
        Form1.Panel2.Visible = True
        Form1.SplitContainer3.Panel2Collapsed = False
        Form1.SplitContainer3.SplitterDistance = Form1.SplitContainer3.Width - 297
    End Sub

    Private Sub TextFix(ByRef txtbox)
        Timer1.Stop()
        T1 = 0
        Label7.Visible = False

        If txtbox Is TextBox1 Then
            txtbox.text = txt

        ElseIf txtbox Is TextBox3 Then

            If Not txt.Contains("#") Then
                If txt.Length = 3 OrElse txt.Length = 6 Then
                    txtbox.text = "#" & txt
                    summarize()
                Else
                    Timer1.Start()
                    TextBox3.Text = ORIGINAL
                End If

            Else

                If txt.Length = 4 OrElse txt.Length = 7 Then
                    txtbox.text = txt
                    summarize()
                Else
                    Timer1.Start()
                    TextBox3.Text = ORIGINAL
                End If


            End If



            Try
                Form1.ColorDialog1.Color = ColorTranslator.FromHtml(txtbox.text)
            Catch ex As Exception
                gen_color()
                Timer1.Start()
            End Try

        End If

        'FINAL
        Try
            gen_color()
            txtbox = Nothing
        Catch ex As Exception
            gen_color()
            Timer1.Start()
        End Try


    End Sub

    Private Sub summarize()
        Dim first As String
        Dim second As String
        Dim third As String
        Dim fourth As String
        Dim fifth As String
        Dim sixth As String

        Dim s As String = TextBox3.Text
        For Each c As Char In s

            If s.IndexOf(c) = 1 Then
                first = c
            End If

            If s.IndexOf(c) = 2 Then
                second = c
            End If

            If s.IndexOf(c) = 3 Then
                third = c
            End If

            If s.IndexOf(c) = 4 Then
                fourth = c
            End If

            If s.IndexOf(c) = 5 Then
                fifth = c
            End If

            If s.IndexOf(c) = 6 Then
                sixth = c
            End If
        Next
        'TextBox5.Text = first & second & third & fourth & fifth



    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        txtbox = TextBox1
        txt = txtbox.Text
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        txtbox = TextBox2
        txt = txtbox.Text
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        txtbox = TextBox3
        txt = txtbox.Text
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        txtbox = TextBox4
        txt = txtbox.Text
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        TextFix(txtbox)
    End Sub

    Private Sub TextBox2_LostFocus(sender As Object, e As EventArgs) Handles TextBox2.LostFocus
        TextFix(txtbox)
    End Sub

    Private Sub TextBox3_LostFocus(sender As Object, e As EventArgs) Handles TextBox3.LostFocus
        TextFix(txtbox)
    End Sub

    Private Sub TextBox4_LostFocus(sender As Object, e As EventArgs) Handles TextBox4.LostFocus
        TextFix(txtbox)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        T1 = T1 + 1
        If T1 = 1 Then
            Label7.Visible = True
        ElseIf T1 = 3 Then
            Label7.Visible = False
        ElseIf T1 = 5 Then
            Label7.Visible = True
        ElseIf T1 = 7 Then
            Label7.Visible = False
        ElseIf T1 = 9 Then
            Label7.Visible = True
        ElseIf T1 = 11 Then
            Label7.Visible = False
        ElseIf T1 = 12 Then
            Timer1.Stop()
            T1 = 0
        End If
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            TextFix(txtbox)
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub
End Class