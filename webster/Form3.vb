Imports System.ComponentModel
Imports System.IO






Public Class Form3
    Public filepath = Form1.filepath
    Dim bone As Boolean = False



    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
        filllistboxwithcolors()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Select Case bone
            Case False
                filepath = filepath & "\" & TextBox1.Text & ComboBox1.SelectedItem
                If TextBox1.Text = Nothing Then
                    MsgBox("No filename was specified.")
                ElseIf System.IO.File.Exists(filepath) Then
                    MsgBox("This file already exists!")
                Else
                    System.IO.File.Create(filepath).Dispose()
                    Me.Close()
                    filepath = ""
                End If

            Case True
                filepath = filepath & "\" & TextBox1.Text & ComboBox1.SelectedItem
                If TextBox1.Text = Nothing Then
                    MsgBox("No filename was specified.")
                ElseIf System.IO.File.Exists(filepath) Then
                    MsgBox("This file already exists!")
                Else
                    Dim sw As StreamWriter

                    If ComboBox1.SelectedItem = ".html" Then
                        Dim createText() As String = {html.Text}
                        File.WriteAllLines(filepath, createText)
                    ElseIf ComboBox1.SelectedItem = ".css" Then
                        Dim createText() As String = {css.Text}
                        File.WriteAllLines(filepath, createText)
                    ElseIf ComboBox1.SelectedItem = ".js" Then
                        Dim createText() As String = {js.Text}
                        File.WriteAllLines(filepath, createText)
                    ElseIf ComboBox1.SelectedItem = ".php" Then
                        Dim createText() As String = {php.Text}
                        File.WriteAllLines(filepath, createText)
                    End If
                    'System.IO.File.Create(filepath).Dispose()
                    Me.Close()
                    filepath = ""
                End If

        End Select



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Form3_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Form1.REFRESHTREE()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            bone = True
        ElseIf CheckBox1.Checked = False Then
            bone = False
        End If
    End Sub


    Private Sub filllistboxwithcolors()
        Me.ComboBox1.DrawMode = DrawMode.OwnerDrawFixed
    End Sub

    Private Sub ComboBox1_DrawItem(ByVal sender As Object,
    ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles ComboBox1.DrawItem

        If e.Index < 0 Then Exit Sub
        Dim rect As Rectangle = e.Bounds
        If e.State And DrawItemState.Selected Then
            e.Graphics.FillRectangle(Brushes.OrangeRed, rect) 'change the selected color you like
        Else
            Dim myColor As Color = Color.FromArgb(39, 39, 39)
            Dim myBrush As Brush

            myBrush = New SolidBrush(myColor)
            e.Graphics.FillRectangle(myBrush, rect)
        End If
        Dim colorname As String = ComboBox1.Items(e.Index)
        Dim b As New SolidBrush(Color.FromName(colorname))
        Dim b2 As Brush = Brushes.White 'add one
        e.Graphics.DrawString(colorname, Me.ComboBox1.Font, b2, rect.X, rect.Y)
    End Sub
End Class