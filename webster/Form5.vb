Imports System.IO
Imports FastColoredTextBoxNS
Imports System.Text
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles
Imports System.Drawing.Text
Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.ComponentModel
Imports System.Threading
Imports System.Diagnostics
Imports System.Linq
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Windows.Forms.Design
Imports WindowsApplication1.MenuComponent_Style
Imports webster.MenuComponent_Style
Imports Awesomium.Core
Imports System.Reflection
Imports System.Net
Imports webster.My.Resources
Imports my.resources
Imports FarsiLibrary.Win


Public Class Form5


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





    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DoubleBuffered = True
        ToolStrip1.Renderer = New MyRenderer
        If TypeOf ToolStrip1.Renderer Is ToolStripProfessionalRenderer Then
            CType(ToolStrip1.Renderer, ToolStripProfessionalRenderer).RoundedEdges = False
        End If
        WebControl1.Source = Form1.WebControl2.Source


    End Sub

    Private Sub Form5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Form1.SplitContainer1.Panel1Collapsed = False
    End Sub



    Private Sub WebControl1_LoadingFrame(sender As Object, e As LoadingFrameEventArgs) Handles WebControl1.LoadingFrame
        ToolStripButton5.Image = My.Resources.star

    End Sub

    Private Sub WebControl1_LoadingFrameComplete(sender As Object, e As FrameEventArgs) Handles WebControl1.LoadingFrameComplete
        ToolStripButton5.Image = My.Resources.bulletking
        Try
            Dim webclient As New WebClient
            Dim IconUrl As New Uri(WebControl1.Source.ToString)
            Dim MemoryStream As New MemoryStream(webclient.DownloadData("http://" & IconUrl.Host & "/favicon.ico"))
            Dim webicon As New Icon(MemoryStream)
            ToolStripButton5.Image = webicon.ToBitmap
        Catch ex As Exception
            ToolStripButton5.Image = My.Resources.orange_error_icon_0
        End Try
        ToolStripLabel1.Text = WebControl1.Title



        Dim newstring As String = WebControl1.Source.ToString.Replace("%20", " ")
        Dim newstring2 As String = newstring.Replace("file:///", "")

        Dim fileName As String = newstring2
        Dim pathname As String = Form1.FolderBrowserDialog1.SelectedPath & "\"
        Dim result As String

        result = Path.GetFileName(fileName)


        Dim SavePath As String = System.IO.Path.Combine(pathname, fileName) 'combines the saveDirectory and the filename to get a fully qualified path.

        If System.IO.File.Exists(SavePath) Then
            'The file exists
            Form1.ToolStripLabel2.Text = result
            'MsgBox("filename:" & fileName & vbNewLine & "pathname:" & pathname & vbNewLine & "result:" & result & vbNewLine & "savepath:" & SavePath)
        Else
            'the file doesn't exist
            Form1.ToolStripLabel2.Text = "(OUTSIDE SOURCE)"
        End If


        If Form1.SplitContainer1.Panel1Collapsed = True Then
            Form1.WebControl2.Source = WebControl1.Source
        Else
        End If


    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        WebControl1.GoBack()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        WebControl1.GoForward()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        WebControl1.Stop()
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        WebControl1.Reload(True)
    End Sub

    Private Sub Awesomium_Windows_Forms_WebControl_ShowCreatedWebView(sender As Object, e As ShowCreatedWebViewEventArgs) Handles WebControl1.ShowCreatedWebView

    End Sub
End Class