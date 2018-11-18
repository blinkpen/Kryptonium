Public Class Form6
    Dim newheight As Integer
    Dim newwidth As Integer


    Private Class MyRenderer : Inherits ToolStripProfessionalRenderer



        Protected Overrides Sub OnRenderMenuItemBackground(ByVal e As System.Windows.Forms.ToolStripItemRenderEventArgs)
            If e.Item.Selected Then
                Dim rc As New Rectangle(Point.Empty, e.Item.Size)

                'Set the highlight color
                e.Graphics.FillRectangle(Brushes.OrangeRed, rc)
                e.Graphics.DrawRectangle(Pens.Black, 1, 0, rc.Width - 2, rc.Height - 1)

            Else
                'Dim rc As New Rectangle(Point.Empty, e.Item.Size)
                ''Set the default color
                'e.Graphics.FillRectangle(Brushes.Gray, rc)
                'e.Graphics.DrawRectangle(Pens.Gray, 1, 0, rc.Width - 2, rc.Height - 1)

                Dim rc2 As New Rectangle(Point.Empty, e.Item.Size)
                Dim brushy As Brush
                brushy = New Drawing.SolidBrush(Color.FromArgb(39, 39, 39))
                e.Graphics.FillRectangle(brushy, rc2)
            End If


        End Sub

        Protected Overrides Sub OnRenderButtonBackground(ByVal e As ToolStripItemRenderEventArgs)
            If Not e.Item.Selected Then
                MyBase.OnRenderButtonBackground(e)
            Else
                Dim rectangle As Rectangle = New Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1)
                e.Graphics.FillRectangle(Brushes.OrangeRed, rectangle)
                e.Graphics.DrawRectangle(Pens.Black, rectangle)
            End If
            If TypeOf e.Item Is ToolStripButton Then
                If DirectCast(e.Item, ToolStripButton).Checked Then
                    Dim clientRect = New Rectangle(0, 0, e.Item.Width, e.Item.Height)

                    e.Graphics.FillRectangle(Brushes.OrangeRed, clientRect)

                End If
            End If

        End Sub

        Protected Overrides Sub OnRenderdropdownButtonBackground(ByVal e As ToolStripItemRenderEventArgs)
            If Not e.Item.Selected Then
                MyBase.OnRenderDropDownButtonBackground(e)
                Dim brushy As Brush
                brushy = New Drawing.SolidBrush(Color.FromArgb(39, 39, 39))
                Dim rectangle As Rectangle = New Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1)
                e.Graphics.FillRectangle(brushy, rectangle)
            Else
                Dim rectangle As Rectangle = New Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1)
                e.Graphics.FillRectangle(Brushes.OrangeRed, rectangle)
                e.Graphics.DrawRectangle(Pens.Black, rectangle)
            End If
        End Sub



        Protected Overrides Sub OnRenderImageMargin(e As System.Windows.Forms.ToolStripRenderEventArgs)
            ' by disabling the line below the image margin is now transparent. (Win7 test only)
            'MyBase.OnRenderImageMargin(e)
        End Sub

        Protected Overrides Sub OnRenderSeparator(e As ToolStripSeparatorRenderEventArgs)
            If e.Vertical OrElse TryCast(e.Item, ToolStripSeparator) Is Nothing Then
                'MyBase.OnRenderSeparator(e)

                'Dim tsBounds As Rectangle = New Rectangle(Point.Empty, e.Item.Size)
                Dim tsbounds As New Rectangle(1, 0, 2, 30)

                Dim myColor As Color = Color.FromArgb(26, 26, 26)
                Dim myBrush As Brush
                myBrush = New SolidBrush(myColor)
                e.Graphics.FillRectangle(myBrush, tsbounds)

                'Dim LineY As Integer = tsbounds.Bottom - (tsbounds.Height / 2) - 1
                'Dim LineLeft As Integer = tsbounds.Left + 3
                'Dim LineRight As Integer = tsbounds.Right
                'e.Graphics.DrawLine(New Pen(Brushes.Red), LineLeft, LineY, LineRight, LineY)
            Else
            End If
        End Sub




    End Class




    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DoubleBuffered = True

        Dim thrd As System.Threading.Thread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf LoadBackgroundImage))
        thrd.Start()

        ToolStrip1.Renderer = New MyRenderer
        If TypeOf ToolStrip1.Renderer Is ToolStripProfessionalRenderer Then
            CType(ToolStrip1.Renderer, ToolStripProfessionalRenderer).RoundedEdges = False
        End If

        Try
            ToolStripLabel1.Text = "Dimensions: " & Clipboard.GetImage.Width & "x" & Clipboard.GetImage.Height
            PictureBox1.BackgroundImage = Clipboard.GetImage
        Catch ex As Exception
            MsgBox("There is no image on the clipboard.")
        End Try





        If Clipboard.GetImage.Width <= 362 Or Clipboard.GetImage.Height <= 281 Then
            PictureBox1.Width = 362
            PictureBox1.Height = 281
            PictureBox1.BackgroundImageLayout = ImageLayout.Center
            'PictureBox1.Anchor = AnchorStyles.Bottom
            'PictureBox1.Anchor = AnchorStyles.Left
            'PictureBox1.Anchor = AnchorStyles.Right
            'PictureBox1.Anchor = AnchorStyles.Top
        Else
            PictureBox1.BackgroundImageLayout = ImageLayout.None
            PictureBox1.Width = Clipboard.GetImage.Width
            PictureBox1.Height = Clipboard.GetImage.Height
            'PictureBox1.Anchor = AnchorStyles.None
            'PictureBox1.Anchor = AnchorStyles.Top
            'PictureBox1.Anchor = AnchorStyles.Left
        End If

        Dim maxsized As New Size(Clipboard.GetImage.Width, Clipboard.GetImage.Height)
        Dim asssized As New Size(PictureBox1.Width + 33, PictureBox1.Height + 64)
        'Me.MaximumSize = asssized

        PictureBox1.Size = maxsized
        PictureBox1.Top = (Me.ClientSize.Height / 2) - (PictureBox1.Height / 2)
        PictureBox1.Left = (Me.ClientSize.Width / 2) - (PictureBox1.Width / 2)

        'MsgBox("ASSSIZE: " & asssized.ToString & " MAXSIZE: " & maxsized.ToString & " BITMAPSIZE: " & Clipboard.GetImage.Size.ToString)
    End Sub

    Private Sub LoadBackgroundImage()
        Dim img As Image = My.Resources.alpha
        Panel1.BackgroundImage = img
    End Sub


    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Clipboard.SetImage(PictureBox1.BackgroundImage)
        MsgBox("The image was successfully copied to the clipboard!")
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        PictureBox1.BackgroundImage.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub Form6_Scroll(sender As Object, e As ScrollEventArgs) Handles Me.Scroll
        ToolStrip1.Dock = DockStyle.Top
    End Sub

    Private Sub Form6_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        PictureBox1.Top = (Me.ClientSize.Height / 2) - (PictureBox1.Height / 2)
        PictureBox1.Left = (Me.ClientSize.Width / 2) - (PictureBox1.Width / 2)
    End Sub

    'Private Sub Form6_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    '    If Panel1.VerticalScroll.Visible = False Then
    '        newheight = Me.Height
    '        Me.Height = newheight
    '    End If
    '    If Panel1.HorizontalScroll.Visible = False Then
    '        newwidth = Me.Width
    '        Me.Width = newwidth
    '    End If

    'End Sub

    'Private Sub Form6_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd


    'End Sub

    'Private Sub Form6_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
    '    'Me.MaximumSize = New Size(newwidth, newheight)
    'End Sub
End Class