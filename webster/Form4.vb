Option Explicit On
Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.Form
Imports Microsoft.VisualBasic.Devices

Public Class Form4
    Dim defaultcurse As Cursor
    Public MouseX = Form1.MouseX
    Public MouseY = Form1.MouseY


    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cur As Icon
        cur = My.Resources.eyedroppa94
        defaultcurse = New Cursor(cur.Handle)
        Cursor = defaultcurse

        Me.Left = MouseX - Me.Width / 2
        Me.Top = MouseY - Me.Height / 2


    End Sub



    Private Sub Form4_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Left Then
            Form1.Timer2.Stop()
            Me.Close()
        End If
    End Sub

    Private Sub endit()
        Form1.Timer2.Stop()
        Form1.UnHookMouse()
        Form1.ToolStripButton7.BackColor = Form1.ColorDialog1.Color

        Dim CodeCodeInHex As String = Form1.ColorDialog1.Color.ToArgb().ToString("X")
        CodeCodeInHex = CodeCodeInHex.Remove(0, 2)
        Form1.BGCOLOR.Text = "#" & CodeCodeInHex
        Form1.Panel2.Visible = False
        If Form1.CSSToolbarToolStripMenuItem.Checked = True Then
        Else
            Form1.SplitContainer3.Panel2Collapsed = True
        End If

        Form1.SplitContainer3.SplitterDistance = Form1.SplitContainer3.Width - Form1.SPLITC3


        Form7.PictureBox1.BackColor = Form1.ColorDialog1.Color
        Form7.gen_color()


        Me.Close()

    End Sub

    Private Sub acceptit()

        Form1.Timer2.Stop()
        Form1.UnHookMouse()
        Form1.ColorDialog1.Color = Form1.ToolStripButton7.BackColor

        Dim CodeCodeInHex As String = Form1.ToolStripButton7.BackColor.ToArgb().ToString("X")
        CodeCodeInHex = CodeCodeInHex.Remove(0, 2)
        Form1.BGCOLOR.Text = "#" & CodeCodeInHex
        Form1.Panel2.Visible = False

        If Form1.CSSToolbarToolStripMenuItem.Checked = True Then
        Else
            Form1.SplitContainer3.Panel2Collapsed = True
        End If

        Form1.ColorDialog1.Color = Color.FromArgb(Form1.firsty)
        Form1.ToolStripTextBox1.Text = Form1.ColorDialog1.Color.A & ", " & Form1.ColorDialog1.Color.R & ", " & Form1.ColorDialog1.Color.G & ", " & Form1.ColorDialog1.Color.B
        Form1.SplitContainer3.SplitterDistance = Form1.SplitContainer3.Width - Form1.SPLITC3
        Me.Close()


    End Sub

    Private Sub PictureBox2_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseDown
        If e.Button = MouseButtons.Left Then
            Form1.Timer2.Stop()
            Me.Close()
        End If
    End Sub

    Private Sub PictureBox2_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseMove
        'dipshit(sender, e)
    End Sub

    Private Sub dipshit(sender As Object, e As MouseEventArgs)
        Do Until e.Button = MouseButtons.Left = True

            If MouseX > Me.Left Then
                main()
            End If
            If MouseX < Me.Right Then
                main()
            End If
            If MouseY > Me.Top Then
                main()
            End If
            If MouseY < Me.Bottom Then
                main()
            End If
            Me.Show()
            System.Threading.Thread.Sleep(1)
        Loop

    End Sub




    Private Sub main()
        'MouseX = CInt(Cursor.Position.X.ToString())
        'MouseY = CInt(Cursor.Position.Y.ToString())
        Me.Left = MouseX - Me.Width / 2
        Me.Top = MouseY - Me.Height / 2

    End Sub

    Private Sub dickless()
        Do While Form1.Timer2.Enabled = True
            'If MouseX > Me.Left Then
            '    main()
            'End If
            'If MouseX < Me.Right Then
            '    main()
            'End If
            'If MouseY > Me.Top Then
            '    main()
            'End If
            'If MouseY < Me.Bottom Then
            '    main()
            'End If
        Loop
    End Sub

    Private Sub Form4_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Dim mX As Integer = MousePosition.X
        Dim mY As Integer = MousePosition.Y

        Select Case e.KeyCode
            Case Keys.Down
                Cursor.Position = New Point(mX, mY + 1)
            Case Keys.Up
                Cursor.Position = New Point(mX, mY - 1)
            Case Keys.Right
                Cursor.Position = New Point(mX + 1, mY)
            Case Keys.Left
                Cursor.Position = New Point(mX - 1, mY)
        End Select

        If e.KeyCode = Keys.Escape Then
            endit()
        End If

        If e.KeyCode = Keys.Enter Then
            acceptit()
        End If

    End Sub

    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Form1.UnHookMouse()

    End Sub


End Class