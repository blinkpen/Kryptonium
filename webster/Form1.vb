Option Explicit On

Imports System.IO
Imports FastColoredTextBoxNS
Imports System.Drawing.Text
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports Awesomium.Core
Imports System.Net
Imports System
Imports System.Threading
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D


#Region "Custom Font Support"
Module CustomFont

    Public Fonta As Object = My.Resources.consola

    'PRIVATE FONT COLLECTION TO HOLD THE DYNAMIC FONT
    Private _pfc As PrivateFontCollection = Nothing



    Public ReadOnly Property GetInstance(ByVal Size As Single,
                                         ByVal style As FontStyle) As Font
        Get
            'IF THIS IS THE FIRST TIME GETTING AN INSTANCE
            'LOAD THE FONT FROM RESOURCES
            If _pfc Is Nothing Then LoadFont()

            'RETURN A NEW FONT OBJECT BASED ON THE SIZE AND STYLE PASSED IN
            Return New Font(_pfc.Families(0), Size, style)

        End Get
    End Property

    Private Sub LoadFont()
        Try
            'INIT THE FONT COLLECTION
            _pfc = New PrivateFontCollection

            'LOAD MEMORY POINTER FOR FONT RESOURCE
            Dim fontMemPointer As IntPtr =
                Marshal.AllocCoTaskMem(
                Fonta.Length)

            'COPY THE DATA TO THE MEMORY LOCATION
            Marshal.Copy(Fonta,
                         0, fontMemPointer,
                         Fonta.Length)

            'LOAD THE MEMORY FONT INTO THE PRIVATE FONT COLLECTION
            _pfc.AddMemoryFont(fontMemPointer,
                               Fonta.Length)

            'FREE UNSAFE MEMORY
            Marshal.FreeCoTaskMem(fontMemPointer)
        Catch ex As Exception
            'ERROR LOADING FONT. HANDLE EXCEPTION HERE
        End Try

    End Sub



End Module

Module CustomFont2


    Public Fontb As Object = My.Resources.corbel

    'PRIVATE FONT COLLECTION TO HOLD THE DYNAMIC FONT
    Private _pfc As PrivateFontCollection = Nothing



    Public ReadOnly Property GetInstance(ByVal Size As Single,
                                         ByVal style As FontStyle) As Font
        Get
            'IF THIS IS THE FIRST TIME GETTING AN INSTANCE
            'LOAD THE FONT FROM RESOURCES
            If _pfc Is Nothing Then LoadFont()

            'RETURN A NEW FONT OBJECT BASED ON THE SIZE AND STYLE PASSED IN
            Return New Font(_pfc.Families(0), Size, style)

        End Get
    End Property

    Private Sub LoadFont()
        Try
            'INIT THE FONT COLLECTION
            _pfc = New PrivateFontCollection

            'LOAD MEMORY POINTER FOR FONT RESOURCE
            Dim fontMemPointer As IntPtr =
                Marshal.AllocCoTaskMem(
                Fontb.Length)

            'COPY THE DATA TO THE MEMORY LOCATION
            Marshal.Copy(Fontb,
                         0, fontMemPointer,
                         Fontb.Length)

            'LOAD THE MEMORY FONT INTO THE PRIVATE FONT COLLECTION
            _pfc.AddMemoryFont(fontMemPointer,
                               Fontb.Length)

            'FREE UNSAFE MEMORY
            Marshal.FreeCoTaskMem(fontMemPointer)
        Catch ex As Exception
            'ERROR LOADING FONT. HANDLE EXCEPTION HERE
        End Try

    End Sub



End Module
#End Region


Public Class Form1
    Dim Lang_Sel As String = ""
    Dim papers As New List(Of FastColoredTextBox)
    Dim tabs As New List(Of FarsiLibrary.Win.FATabStripItem)
    Dim selectedfile As String = ""
    Dim tabcount As Integer = 0
    Dim pagecount As Integer = 0
    Dim T1 As Integer = 0
    Dim CURRENTVIEW As String = ""
    Dim CURRENTEDIT As String = ""
    Dim bmp As Bitmap
    Public filepath As String = ""
    Dim defaultcurse As Cursor
    Dim screenshot As System.Drawing.Bitmap
    Dim SavedCursor As Icon
    Public firsty As String
    Public MouseX As Integer = CInt(Cursor.Position.X.ToString())
    Public MouseY As Integer = CInt(Cursor.Position.Y.ToString())
    Private tabColor As Color = SystemColors.Window
    Private selectColor As Color = SystemColors.Control
    Public SPLITC3 As Integer = 0
    Dim fctbn As New FastColoredTextBox
    Dim wallpaper As String
    Dim x As Integer
    Dim y As Integer
    Dim pic As Bitmap = Nothing
    Dim bmpNew As Bitmap = Nothing
    Dim MARK As Color = Color.OrangeRed
    Dim GRID As Color = Color.Black
    Dim defaultdirectory As String = My.Application.Info.DirectoryPath
    Dim img As Image
    Dim GRIDTOGGLE As Boolean = True
    Dim MAINDIR As String
    Dim GETROOT As List(Of TreeNode)
    Dim SMOOTHSAILOR As Boolean = False

#Region "Visual Styles Rendering"
    Private Class MyRenderer : Inherits ToolStripProfessionalRenderer

        Protected Overrides Sub OnRenderMenuItemBackground(ByVal e As System.Windows.Forms.ToolStripItemRenderEventArgs)
            If e.Item.Text = "Clear all recent projects" Then
                If e.Item.Selected Then
                    Dim rc As New Rectangle(Point.Empty, e.Item.Size)
                    'Set the highlight color
                    e.Graphics.FillRectangle(Brushes.Red, rc)
                    e.Graphics.DrawRectangle(Pens.Black, 1, 0, rc.Width - 2, rc.Height - 1)
                    e.Item.ForeColor = Color.White
                Else
                    'Dim rc As New Rectangle(Point.Empty, e.Item.Size)
                    ''Set the default color
                    'e.Graphics.FillRectangle(Brushes.Gray, rc)
                    'e.Graphics.DrawRectangle(Pens.Gray, 1, 0, rc.Width - 2, rc.Height - 1)
                    Dim rc2 As New Rectangle(Point.Empty, e.Item.Size)
                    Dim brushy As Brush
                    brushy = New Drawing.SolidBrush(Color.FromArgb(39, 39, 39))
                    e.Graphics.FillRectangle(brushy, rc2)
                    e.Item.ForeColor = Color.Red
                End If
            Else
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

        Protected Overrides Sub OnRenderDropDownButtonBackground(ByVal e As ToolStripItemRenderEventArgs)
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

        Protected Overrides Sub OnRenderArrow(e As ToolStripArrowRenderEventArgs)

            e.ArrowColor = Color.White
            MyBase.OnRenderArrow(e)
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
#End Region

#Region "MouseHooking"

    Private Structure MSLLHOOKSTRUCT
        Public pt As Point
        Public mouseData As Int32
        Public flags As Int32
        Public time As Int32
        Public extra As IntPtr
    End Structure

    Private mHook As IntPtr = IntPtr.Zero
    Private Const WH_MOUSE_LL As Int32 = &HE
    Private Const WM_RBUTTONDOWN As Int32 = &H204
    Private Const WM_LBUTTONDOWN As Int32 = &H201
    Private Const WM_MOUSEHOVER As Int32 = &H2A1
    <MarshalAs(UnmanagedType.FunctionPtr)> Private mProc As MouseHookDelegate
    Private Declare Function SetWindowsHookExW Lib "user32.dll" (ByVal idHook As Int32, ByVal HookProc As MouseHookDelegate, ByVal hInstance As IntPtr, ByVal wParam As Int32) As IntPtr
    Private Declare Function UnhookWindowsHookEx Lib "user32.dll" (ByVal hook As IntPtr) As Boolean
    Private Declare Function CallNextHookEx Lib "user32.dll" (ByVal idHook As Int32, ByVal nCode As Int32, ByVal wParam As IntPtr, ByRef lParam As MSLLHOOKSTRUCT) As Int32
    Private Declare Function GetModuleHandleW Lib "kernel32.dll" (ByVal fakezero As IntPtr) As IntPtr
    Private Delegate Function MouseHookDelegate(ByVal nCode As Int32, ByVal wParam As IntPtr, ByRef lParam As MSLLHOOKSTRUCT) As Int32

    Public Function SetHookMouse() As Boolean
        If mHook = IntPtr.Zero Then
            mProc = New MouseHookDelegate(AddressOf MouseHookProc)
            mHook = SetWindowsHookExW(WH_MOUSE_LL, mProc, GetModuleHandleW(IntPtr.Zero), 0)
        End If
        Return mHook <> IntPtr.Zero
    End Function

    Public Sub UnHookMouse()
        If mHook = IntPtr.Zero Then Return
        UnhookWindowsHookEx(mHook)
        mHook = IntPtr.Zero
    End Sub

    Private Function MouseHookProc(ByVal nCode As Int32, ByVal wParam As IntPtr, ByRef lParam As MSLLHOOKSTRUCT) As Int32
        'Label1.Text = "Message=" & wParam.ToInt32.ToString & "  X=" & lParam.pt.X.ToString & "  Y=" & lParam.pt.Y.ToString
        If wParam.ToInt32 = WM_LBUTTONDOWN Then
            getcolor()
            Return 1
        End If

        If wParam.ToInt32 = WM_MOUSEHOVER Then

            'MsgBox("poop")
            'Return 1
        End If
        'If wParam.ToInt32 = WM_RBUTTONDOWN Then
        '    If CheckBox_StopRight.Checked = True Then Return 1
        'End If





        Return CallNextHookEx(WH_MOUSE_LL, nCode, wParam, lParam)
    End Function


#End Region


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        DoubleBuffered = True
        PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage
        TreeView1.Font = CustomFont2.GetInstance(12, FontStyle.Regular)
        FTS.BackgroundImage = My.Resources.spire
        ToolStripTextBox1.Text = ColorDialog1.Color.A & ", " & ColorDialog1.Color.R & ", " & ColorDialog1.Color.G & ", " & ColorDialog1.Color.B
        FTS.BackColor = Color.FromArgb(39, 40, 34)
        SplitContainer3.Panel2Collapsed = True

        'fctb.Font = CustomFont.GetInstance(15, FontStyle.Regular)
        'fctb.SelectionColor = Color.White
        'fctb.LineNumberColor = Color.OrangeRed
        'fctb.CaretColor = Color.White

        'Custom Style Rendering Initiations
        ToolStripDropDownButton1.DropDownItems.Item(0).Image = ImageList2.Images(ImageList2.Images.Keys(0))
        ToolStripDropDownButton1.DropDown.BackColor = Color.FromArgb(39, 39, 39)
        ToolStripDropDownButton1.DropDown.ForeColor = Color.White
        ToolStripDropDownButton2.DropDown.BackColor = Color.FromArgb(39, 39, 39)
        ToolStripDropDownButton2.DropDown.ForeColor = Color.White
        NoneToolStripMenuItem.ForeColor = Color.White
        CustomToolStripMenuItem.ForeColor = Color.White
        KryptoniumLogoToolStripMenuItem.ForeColor = Color.White
        DarkCrownToolStripMenuItem.ForeColor = Color.White
        KryptoniumIconToolStripMenuItem.ForeColor = Color.White
        DarkCrownsToolStripMenuItem.ForeColor = Color.White
        ShadowCrownsToolStripMenuItem.ForeColor = Color.White
        SquaresToolStripMenuItem.ForeColor = Color.White
        ShadowSquaresToolStripMenuItem.ForeColor = Color.White
        DissolveToolStripMenuItem.ForeColor = Color.White
        Dissolve2ToolStripMenuItem.ForeColor = Color.White
        KrissKrossToolStripMenuItem.ForeColor = Color.White
        KrissKrossDissolvedToolStripMenuItem.ForeColor = Color.White
        PlaidToolStripMenuItem.ForeColor = Color.White
        ContextMenuStrip1.BackColor = Color.FromArgb(39, 39, 39)
        ContextMenuStrip1.ForeColor = Color.White
        CopyFilePathToolStripMenuItem.DropDown.BackColor = Color.FromArgb(39, 39, 39)
        CopyFilePathToolStripMenuItem.DropDown.ForeColor = Color.White
        ContextMenuStrip2.BackColor = Color.FromArgb(39, 39, 39)
        ContextMenuStrip2.ForeColor = Color.White
        ContextMenuStrip3.BackColor = Color.FromArgb(39, 39, 39)
        ContextMenuStrip3.ForeColor = Color.White
        ContextMenuStrip1.Renderer = New MyRenderer
        ToolStrip1.Renderer = New MyRenderer
        ToolStrip2.Renderer = New MyRenderer
        ToolStrip3.Renderer = New MyRenderer
        ContextMenuStrip2.Renderer = New MyRenderer
        ContextMenuStrip3.Renderer = New MyRenderer

        If TypeOf ToolStrip1.Renderer Is ToolStripProfessionalRenderer Then
            CType(ToolStrip1.Renderer, ToolStripProfessionalRenderer).RoundedEdges = False
        End If

        If TypeOf ToolStrip2.Renderer Is ToolStripProfessionalRenderer Then
            CType(ToolStrip2.Renderer, ToolStripProfessionalRenderer).RoundedEdges = False
        End If

        If TypeOf ToolStrip3.Renderer Is ToolStripProfessionalRenderer Then
            CType(ToolStrip3.Renderer, ToolStripProfessionalRenderer).RoundedEdges = False
        End If

        ''Menu Rendering
        'ContextMenuStrip1.Renderer = New MenuRender
        'For Each component As IComponent In Me.components.Components
        '    If CType(component, Object).GetType() Is GetType(ContextMenuStrip) Then
        '        CType(component, ContextMenuStrip).Renderer = New MenuRender()
        '    End If
        'Next component

        'My.Settings.RECENT_PROJECTS.Clear()

        If My.Settings.RECENT_PROJECTS Is Nothing Then
            My.Settings.RECENT_PROJECTS = New Specialized.StringCollection
        Else
            REGEN_RECENTPROJ()
        End If




        For Each paper In papers
            paper.BackgroundImage = My.Resources.ResourceManager.GetObject(My.Settings.wally)
        Next

    End Sub

    Private Sub ToolStripSeparator14_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles ToolStripSeparator14.Paint
        e.Graphics.DrawLine(Pens.DimGray, 0, 0, ToolStripSeparator14.Width, 0)
    End Sub

    Private Sub ToolStripSeparator15_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles ToolStripSeparator15.Paint
        e.Graphics.DrawLine(Pens.DimGray, 0, 0, ToolStripSeparator15.Width, 0)
    End Sub

    Private Sub ClearProjects()
        Dim result As Integer = MessageBox.Show("Are you sure that you want to clear all recent projects? This can not be undone.", "Warning", MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Cancel Then
        ElseIf result = DialogResult.No Then
        ElseIf result = DialogResult.Yes Then
            My.Settings.RECENT_PROJECTS.Clear()
            ToolStripDropDownButton2.DropDownItems.Clear()

        End If
    End Sub

    Private Sub ManageProjects()
        Form9.Show()
    End Sub

    Public Sub REGEN_RECENTPROJ()
        ToolStripDropDownButton2.DropDownItems.Clear()

        Try
            For Each proj As String In My.Settings.RECENT_PROJECTS
                ToolStripDropDownButton2.DropDownItems.Add(proj, My.Resources.web_icong, AddressOf MenuItem_Click)
            Next
            TreeView1.Nodes(0).Expand()

        Catch ex As Exception

        End Try

        FTS.Items.Clear()
        'WebControl2.Source = New Uri("file:///")

        If My.Settings.RECENT_PROJECTS.Count = 0 Then
            ToolStripDropDownButton2.DropDownItems.Clear()
        Else
            ToolStripDropDownButton2.DropDownItems.Add(ToolStripSeparator15())
            ToolStripDropDownButton2.DropDownItems.Add("Manage recent projects..", My.Resources.tools, AddressOf ManageProjects)
            ToolStripDropDownButton2.DropDownItems.Add("Clear all recent projects", My.Resources.bin15, AddressOf ClearProjects)

        End If
    End Sub



    Private Sub MenuItem_Click(sender As Object, e As EventArgs)
        Dim MenuItem = DirectCast(sender, ToolStripMenuItem)

        Try
            MAINDIR = MenuItem.Text


            Dim di As New DirectoryInfo(MAINDIR)
            Dim fiArr As FileInfo() = di.GetFiles()
            Dim fri As FileInfo

            'ListView1.Columns(0).Text = di.ToString
            'For Each fri In fiArr
            '    ListView1.Items.Add(fri.ToString)
            'Next


            TreeView1.Nodes.Clear()
            TreeView1.Nodes.Add(MAINDIR)
            'Populate this root node
            'BackgroundWorker1.RunWorkerAsync()
            'PopTree()
            PopulateTreeView(MAINDIR, TreeView1.Nodes(0))

            If My.Settings.RECENT_PROJECTS.Contains(MAINDIR.ToString) Then
            Else
                My.Settings.RECENT_PROJECTS.Add(MAINDIR.ToString)
                My.Settings.Save()
            End If
            FolderBrowserDialog1.SelectedPath = MenuItem.Text

            REGEN_RECENTPROJ()

        Catch ex As Exception
            Dim result As Integer = MessageBox.Show("That directory has either been moved, changed, or deleted. Would you like to remove it from the list?", "Kryptonium - 404", MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Cancel Then

            ElseIf result = DialogResult.No Then

            ElseIf result = DialogResult.Yes Then
                Try
                    For Each proj In My.Settings.RECENT_PROJECTS
                        If proj = MenuItem.Text Then
                            My.Settings.RECENT_PROJECTS.Remove(proj)
                            My.Settings.Save()
                        End If
                    Next
                Catch ex2 As Exception

                End Try

                REGEN_RECENTPROJ()


            End If



        End Try
        FTS.Items.Clear()
    End Sub

    Private Sub Language_Selector()
        For Each paper In papers
            If Lang_Sel = "html" Then
                paper.Language = Language.HTML
            ElseIf Lang_Sel = "css" Then
                paper.Language = Language.HTML
            ElseIf Lang_Sel = "js" Then
                paper.Language = Language.JS
            ElseIf Lang_Sel = "php" Then
                paper.Language = Language.PHP
            End If
        Next
    End Sub


    'Private Sub fctb_TextChanged(sender As Object, e As TextChangedEventArgs) Handles fctb.TextChanged
    '    If Lang_Sel = "css" Then
    '        CSS_Active(sender, e)
    '    End If
    '    Timer1.Stop()
    '    Timer1.Start()
    'End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


        CURRENTEDIT = FTS.SelectedItem.Name

        For Each paper In papers
            If paper.Name = CURRENTEDIT Then
                'IO.File.WriteAllText(TreeView1.SelectedNode.FullPath, paper.Text)
                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter(CURRENTEDIT, False)
                file.Write(paper.Text)
                file.Close()
            Else

            End If
        Next

        'Using session As WebSession = WebCore.CreateWebSession(New WebPreferences())
        '    session.ClearCache()
        '    Using view As WebView = WebCore.CreateWebView(1280, 960, session)
        '    End Using
        'End Using



        'If Not webcontrol2.Source = New Uri("about:blank") Then
        '    webcontrol2.Refresh()
        'End If
        'webcontrol2.LoadHTML(fctb.Text)
        Dim loader As String = "file:///"
        WebControl2.Source = New Uri(loader & CURRENTVIEW)
        WebControl2.Reload(True)
        Form5.WebControl1.Source = New Uri(loader & CURRENTVIEW)
        Form5.WebControl1.Reload(True)

        Timer1.Stop()

    End Sub



    Private Sub PopulateTreeView(ByVal dir As String, ByVal parentNode As TreeNode)

        Dim folder As String = String.Empty
        Try
            'Add folders to treeview
            Dim folders() As String = IO.Directory.GetDirectories(dir)
            If folders.Length <> 0 Then
                Dim folderNode As TreeNode = Nothing
                Dim folderName As String = String.Empty


                For Each folder In folders
                    folderName = IO.Path.GetFileName(folder)
                    folderNode = parentNode.Nodes.Add(folderName)
                    folderNode.Tag = "folder"
                    folderNode.Name = "folder"
                    PopulateTreeView(folder, folderNode)
                Next
            End If


            'Add the files to treeview
            Dim files() As String = IO.Directory.GetFiles(dir)
            TreeView1.Nodes(0).Tag = "folder"
            TreeView1.Nodes(0).Name = "folder"
            If files.Length <> 0 Then
                Dim fileNode As TreeNode = Nothing
                For Each file As String In files
                    fileNode = parentNode.Nodes.Add(IO.Path.GetFileName(file))
                    fileNode.Tag = "file"
                    If file.Contains(".html") Or file.Contains(".htm") Or file.Contains(".HTML") Or file.Contains(".HTM") Then
                        fileNode.ImageKey = ImageList1.Images.Keys(1)
                        fileNode.SelectedImageKey = ImageList1.Images.Keys(1)
                        fileNode.Name = "html"
                    ElseIf file.Contains(".css") Or file.Contains(".CSS") Then
                        fileNode.ImageKey = ImageList1.Images.Keys(2)
                        fileNode.SelectedImageKey = ImageList1.Images.Keys(2)
                        fileNode.Name = "css"
                    ElseIf file.Contains(".js") Or file.Contains(".JS") Then
                        fileNode.ImageKey = ImageList1.Images.Keys(3)
                        fileNode.SelectedImageKey = ImageList1.Images.Keys(3)
                        fileNode.Name = "js"
                    ElseIf file.Contains(".php") Or file.Contains(".PHP") Then
                        fileNode.ImageKey = ImageList1.Images.Keys(4)
                        fileNode.SelectedImageKey = ImageList1.Images.Keys(4)
                        fileNode.Name = "php"
                    ElseIf file.Contains(".png") Or file.Contains(".PNG") Then
                        fileNode.ImageKey = ImageList1.Images.Keys(5)
                        fileNode.SelectedImageKey = ImageList1.Images.Keys(5)
                        fileNode.Name = "png"
                    ElseIf file.Contains(".bmp") Or file.Contains(".BMP") Then
                        fileNode.ImageKey = ImageList1.Images.Keys(6)
                        fileNode.SelectedImageKey = ImageList1.Images.Keys(6)
                        fileNode.Name = "bmp"
                    ElseIf file.Contains(".gif") Or file.Contains(".GIF") Then
                        fileNode.ImageKey = ImageList1.Images.Keys(7)
                        fileNode.SelectedImageKey = ImageList1.Images.Keys(7)
                        fileNode.Name = "gif"
                    ElseIf file.Contains(".jpg") Or file.Contains(".jpeg") Or file.Contains(".JPG") Or file.Contains(".JPEG") Then
                        fileNode.ImageKey = ImageList1.Images.Keys(8)
                        fileNode.SelectedImageKey = ImageList1.Images.Keys(8)
                        fileNode.Name = "jpg"
                    ElseIf file.Contains(".txt") Or file.Contains(".TXT") Then
                        fileNode.ImageKey = ImageList1.Images.Keys(9)
                        fileNode.SelectedImageKey = ImageList1.Images.Keys(9)
                        fileNode.Name = "txt"
                    ElseIf file.Contains(".ttf") Or file.Contains(".TTF") Or file.Contains(".otf") Or file.Contains(".OTF") Then
                        fileNode.ImageKey = ImageList1.Images.Keys(10)
                        fileNode.SelectedImageKey = ImageList1.Images.Keys(10)
                        fileNode.Name = "font"
                    Else
                        fileNode.ImageKey = ImageList1.Images.Keys(11)
                        fileNode.SelectedImageKey = ImageList1.Images.Keys(11)
                        fileNode.Name = "other"
                    End If

                Next

            End If

        Catch ex As UnauthorizedAccessException
            parentNode.Nodes.Add("Access Denied")
        End Try


    End Sub


    Private Sub TreeView1_NodeMouseClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        If e.Button = MouseButtons.Right Then
            TreeView1.SelectedNode = e.Node
        End If
    End Sub

    Private Sub TreeView1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeView1.MouseClick
        If e.Button = MouseButtons.Right Then


            If TreeView1.SelectedNode.Tag = "file" Then
                ContextMenuStrip1.Items.Clear()
                Dim treepath As String = TreeView1.Nodes(0).ToString.Replace("TreeNode: ", "")
                Dim webbypath As String = TreeView1.SelectedNode.FullPath
                Dim webpath As String = webbypath.Replace(treepath, "")
                ContextMenuStrip1.Items.Insert(0, New ToolStripLabel(webpath))
                ContextMenuStrip1.Items.Item(0).Name = "label1"
                ContextMenuStrip1.Items.Item(0).ForeColor = Color.OrangeRed
                Dim font As New Font("Segoe UI", 14, FontStyle.Bold, GraphicsUnit.Pixel)
                ContextMenuStrip1.Items.Item(0).Font = font

                Dim getimg As Integer = ImageList1.Images.Keys.IndexOf(TreeView1.SelectedNode.ImageKey)
                If getimg = -1 Then
                    getimg = 0
                End If
                ContextMenuStrip1.Items.Item(0).Image = ImageList1.Images(ImageList1.Images.Keys(getimg))
                ContextMenuStrip1.Items.Item(0).ImageAlign = Drawing.ContentAlignment.MiddleCenter
                ContextMenuStrip1.Items.Item(0).ImageScaling = ToolStripItemImageScaling.SizeToFit
                ContextMenuStrip1.Items.Insert(1, New ToolStripSeparator())
                ContextMenuStrip1.Items.Insert(2, CopyFilePathToolStripMenuItem)
                ContextMenuStrip1.Items.Insert(3, ViewInExplorerToolStripMenuItem)


                If TreeView1.SelectedNode.Text.Contains(".css") Then
                    ContextMenuStrip1.Items.Insert(4, New ToolStripSeparator())
                    ContextMenuStrip1.Items.Insert(5, CreateHTMLlinkRelAttributeToolStripMenuItem)
                End If

                If TreeView1.SelectedNode.Text.Contains(".js") Then
                    ContextMenuStrip1.Items.Insert(4, New ToolStripSeparator())
                    ContextMenuStrip1.Items.Insert(5, CreateJavascriptlinkTagToolStripMenuItem)
                End If

                ContextMenuStrip1.Show(TreeView1, e.Location)

            ElseIf TreeView1.SelectedNode.Tag = "folder" Then
                ContextMenuStrip3.Items.Clear()
                Dim treepath As String = TreeView1.Nodes(0).ToString.Replace("TreeNode: ", "")
                Dim webbypath As String = TreeView1.SelectedNode.FullPath

                Dim webpath As String = webbypath.Replace(treepath, "")
                ContextMenuStrip3.Items.Insert(0, New ToolStripLabel(webpath))
                ContextMenuStrip3.Items.Item(0).Name = "label1"
                ContextMenuStrip3.Items.Item(0).ForeColor = Color.OrangeRed
                Dim font As New Font("Segoe UI", 14, FontStyle.Bold, GraphicsUnit.Pixel)
                ContextMenuStrip3.Items.Item(0).Font = font

                Dim getimg As Integer = ImageList1.Images.Keys.IndexOf(TreeView1.SelectedNode.ImageKey)
                If getimg = -1 Then
                    getimg = 0
                End If
                ContextMenuStrip3.Items.Item(0).Image = ImageList1.Images(ImageList1.Images.Keys(getimg))
                ContextMenuStrip3.Items.Item(0).ImageAlign = Drawing.ContentAlignment.MiddleCenter
                ContextMenuStrip3.Items.Item(0).ImageScaling = ToolStripItemImageScaling.SizeToFit
                ContextMenuStrip3.Items.Insert(1, New ToolStripSeparator())
                ContextMenuStrip3.Items.Insert(2, ExplorerHereToolStripMenuItem)
                ContextMenuStrip3.Items.Insert(3, CopyPathToolStripMenuItem)
                ContextMenuStrip3.Items.Insert(4, AddNewFileToolStripMenuItem)
                ContextMenuStrip3.Show(TreeView1, e.Location)

            End If



        End If
    End Sub

    Private Sub TreeView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeView1.MouseDoubleClick
        Try
            If TreeView1.SelectedNode Is Nothing Then
                MsgBox("There is nothing selected.")
            End If

            ' Get the count of the child tree nodes contained in the SelectedNode.
            Dim myNodeCount As Integer = TreeView1.SelectedNode.GetNodeCount(True)
            Dim myChildPercentage As Decimal = CDec(myNodeCount) /
               CDec(TreeView1.GetNodeCount(True)) * 100

            ' Display the tree node path and the number of child nodes it and the tree view have.
            'MessageBox.Show(("The '" + TreeView1.SelectedNode.FullPath + "' node has " _
            '   + myNodeCount.ToString() + " child nodes." + Microsoft.VisualBasic.ControlChars.Lf _
            '   + "That is " + String.Format("{0:###.##}", myChildPercentage) _
            '   + "% of the total tree nodes in the tree view control."))

            If TreeView1.SelectedNode.Tag = "file" Then
                If TreeView1.SelectedNode.Text.Contains(".html") Or TreeView1.SelectedNode.Text.Contains(".css") Or TreeView1.SelectedNode.Text.Contains(".js") Or TreeView1.SelectedNode.Text.Contains(".php") Or TreeView1.SelectedNode.Text.Contains(".txt") Or TreeView1.SelectedNode.Text.Contains(".bat") Or TreeView1.SelectedNode.Text.Contains(".HTML") Or TreeView1.SelectedNode.Text.Contains(".htm") Or TreeView1.SelectedNode.Text.Contains(".HTM") Or TreeView1.SelectedNode.Text.Contains(".CSS") Or TreeView1.SelectedNode.Text.Contains(".JS") Or TreeView1.SelectedNode.Text.Contains(".PHP") Or TreeView1.SelectedNode.Text.Contains(".BAT") Or TreeView1.SelectedNode.Text.Contains(".TXT") Then
                    'Dim fctb As New FastColoredTextBox
                    CURRENTVIEW = TreeView1.SelectedNode.FullPath
                    Dim fileReader As System.IO.StreamReader
                    fileReader =
                    My.Computer.FileSystem.OpenTextFileReader(CURRENTVIEW)
                    Dim stringReader As String
                    stringReader = fileReader.ReadToEnd()
                    'fctbn.Text = stringReader
                    NewFCTB(stringReader)
                    fileReader.Close()
                    ToolStripLabel2.Text = TreeView1.SelectedNode.Text
                ElseIf TreeView1.SelectedNode.Text.Contains(".png") Or TreeView1.SelectedNode.Text.Contains(".bmp") Or TreeView1.SelectedNode.Text.Contains(".jpg") Or TreeView1.SelectedNode.Text.Contains(".gif") Or TreeView1.SelectedNode.Text.Contains(".PNG") Or TreeView1.SelectedNode.Text.Contains(".BMP") Or TreeView1.SelectedNode.Text.Contains(".JPG") Or TreeView1.SelectedNode.Text.Contains(".JPEG") Or TreeView1.SelectedNode.Text.Contains(".jpeg") Or TreeView1.SelectedNode.Text.Contains(".GIF") Then
                    Process.Start(TreeView1.SelectedNode.FullPath)
                ElseIf TreeView1.SelectedNode.Text.Contains(".wav") Or TreeView1.SelectedNode.Text.Contains(".mp3") Or TreeView1.SelectedNode.Text.Contains(".WAV") Or TreeView1.SelectedNode.Text.Contains(".MP3") Then
                    Process.Start(TreeView1.SelectedNode.FullPath)
                Else
                    Process.Start(TreeView1.SelectedNode.FullPath)
                End If

            End If
        Catch ex As Exception
            MsgBox("The file was not found. It must have been deleted." & vbNewLine & "The project explorer will now be refreshed.")
            REFRESHTREE()
        End Try


    End Sub

    Private Sub NewFCTB(ByVal text)

        Dim FT As New FarsiLibrary.Win.FATabStripItem
        FT.Title = TreeView1.SelectedNode.Text
        FT.Name = TreeView1.SelectedNode.FullPath



        'If FTS.Items.Contains(FT) Then
        '    If FT.Name = TreeView1.SelectedNode.FullPath Then
        '        MsgBox("it exists")
        '    Else
        '        MsgBox("doesnot")

        '    End If
        'End If
        Dim bResult As Boolean = False

        For Each elem As Control In FTS.Items
            If elem.Name = TreeView1.SelectedNode.FullPath Then
                bResult = True

                Exit For
            End If
        Next

        If bResult = True Then

        ElseIf bResult = False Then

            FTS.Items.Add(FT)
            FTS.Tag = tabcount


            If FT.Name.Contains(".html") Then
                FT.ForeColor = Color.OrangeRed
            ElseIf FT.Name.Contains(".css") Then
                FT.ForeColor = Color.Green
            End If





            Dim fctbn As New FastColoredTextBox

            papers.Add(fctbn)
            fctbn.Tag = pagecount
            fctbn.BackColor = Color.FromArgb(39, 40, 34)
            fctbn.ForeColor = Color.White
            fctbn.Dock = DockStyle.Fill
            fctbn.Name = TreeView1.SelectedNode.FullPath
            fctbn.CaretColor = Color.OrangeRed
            fctbn.BackgroundImageLayout = ImageLayout.Tile



            If My.Settings.wally = "custom" Then
                img = Image.FromFile(defaultdirectory & "\custom.png")
                fctbn.BackgroundImage = img
            Else
                fctbn.BackgroundImage = My.Resources.ResourceManager.GetObject(My.Settings.wally)
            End If





            'Dim myBitmap As New System.Drawing.Bitmap(filename:="C:\Users\BuddyRoach\Desktop\codebackdrop.png")
            'Dim myGraphics As Graphics

            ''myGraphics.DrawImage(myBitmap, 400, 600)
            'fctbn.BackgroundImage = myBitmap


            If fctbn.Name.Contains(".html") Then
                fctbn.Language = Language.HTML
            ElseIf fctbn.Name.Contains(".css") Then

            ElseIf fctbn.Name.Contains(".php") Then
                fctbn.Language = Language.PHP
            ElseIf fctbn.Name.Contains(".js") Then
                fctbn.Language = Language.JS
            End If



            fctbn.LineNumberColor = Color.OrangeRed
            fctbn.IndentBackColor = Color.FromArgb(39, 39, 39)
            fctbn.Text = text
            fctbn.Select()
            fctbn.SelectionColor = Color.White
            'fctbn.Font = CustomFont.GetInstance(12, FontStyle.Regular)
            fctbn.Font = CustomFont.GetInstance(15, FontStyle.Regular)
            FT.Controls.Add(fctbn)
            FTS.SelectedItem = FT
            FTS.Font = CustomFont2.GetInstance(12, FontStyle.Regular)


            AddHandler fctbn.TextChanged, AddressOf fctbn_textchanged
            AddHandler fctbn.MouseClick, AddressOf fctbn_mouseclick
            AddHandler fctbn.MouseDoubleClick, AddressOf fctbn_mousedoubleclick
            AddHandler fctbn.Paint, AddressOf fctbn_PaintBackground
            AddHandler fctbn.KeyDown, AddressOf fctbn_keydown


        End If

        tabcount = tabcount + 1
        pagecount = pagecount + 1


        Dim loader As String = "file:///"

        WebControl2.Source = New Uri(loader & TreeView1.SelectedNode.FullPath)
        Form5.WebControl1.Source = New Uri(loader & TreeView1.SelectedNode.FullPath)

    End Sub

    Private Sub FTS_MouseClick(sender As Object, e As MouseEventArgs) Handles FTS.MouseClick



    End Sub










    Private Sub fctbn_PaintBackground(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        'Dim fctbn = DirectCast(sender, FastColoredTextBox)
        'Dim x As Integer = fctbn.Width
        'Dim y As Integer = fctbn.Height


        'Dim ass As Image = My.Resources.ResourceManager.GetObject("codebackdrop")

        'Dim pbImage As New Bitmap(ass)
        'Dim bmp As New Bitmap(fctbn.ClientSize.Width, fctbn.ClientSize.Height)
        'Using g As Graphics = Graphics.FromImage(bmp)
        '    'g.Clear(PictureBox1.BackColor)
        '    g.DrawImage(pbImage, x - 330, y - 130, 300, 100)
        'End Using
        'fctbn.BackgroundImage = bmp

    End Sub

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        ''MyBase.OnPaintBackground(e)
        'For Each paper In papers
        '    e.Graphics.DrawImage(bmp, New Rectangle(paper.Width - bmp.Width - 20, paper.Height - bmp.Height - 20, 300, 100), New Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel)
        'Next
    End Sub

    Private Sub fctbn_keydown(sender As Object, e As KeyEventArgs)
        Dim fctbn = DirectCast(sender, FastColoredTextBox)
        Try
            If (e.KeyCode = Keys.Oemplus AndAlso e.Modifiers = Keys.Control) Then
                fctbn.Font = New Font(fctbn.Font.FontFamily, fctbn.Font.Size + 1,
                 fctbn.Font.Style)
            End If

            If (e.KeyCode = Keys.OemMinus AndAlso e.Modifiers = Keys.Control) Then
                fctbn.Font = New Font(fctbn.Font.FontFamily, fctbn.Font.Size - 1,
                 fctbn.Font.Style)
            End If

            If (e.KeyCode = Keys.NumPad0 AndAlso e.Modifiers = Keys.Control) Then
                fctbn.Font = New Font(fctbn.Font.FontFamily, 15,
                 fctbn.Font.Style)
            End If
        Catch ex As Exception
            MsgBox("You have reached your font size limit.")
        End Try

    End Sub


    Private Sub fctbn_textchanged(sender As Object, e As EventArgs)
        Timer1.Stop()
        Timer1.Start()
    End Sub

    Private Sub fctbn_mouseclick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        CURRENTEDIT = FTS.SelectedItem.Name

        Dim fctbn = DirectCast(sender, FastColoredTextBox)
        If e.Button = MouseButtons.Right Then
            ContextMenuStrip2.Items.Clear()

            ContextMenuStrip2.Items.Insert(0, New ToolStripLabel(FTS.SelectedItem.Title))
            ContextMenuStrip2.Items.Item(0).Name = "label1"
            ContextMenuStrip2.Items.Item(0).BackColor = Color.FromArgb(39, 39, 39)
            ContextMenuStrip2.Items.Item(0).ForeColor = Color.OrangeRed
            Dim font As New Font("Segoe UI", 14, FontStyle.Bold, GraphicsUnit.Pixel)
            ContextMenuStrip2.Items.Item(0).Font = font
            If CURRENTEDIT.Contains(".html") Then
                ContextMenuStrip2.Items.Item(0).Image = ImageList2.Images(ImageList2.Images.Keys(1))
            ElseIf CURRENTEDIT.Contains(".css") Then
                ContextMenuStrip2.Items.Item(0).Image = ImageList2.Images(ImageList2.Images.Keys(2))
            End If

            ContextMenuStrip2.Items.Item(0).ImageAlign = Drawing.ContentAlignment.MiddleCenter
            ContextMenuStrip2.Items.Item(0).ImageScaling = ToolStripItemImageScaling.SizeToFit

            ContextMenuStrip2.Items.Insert(1, New ToolStripSeparator())
            ContextMenuStrip2.Items.Insert(2, LoadThisPageToBrowserToolStripMenuItem)
            ContextMenuStrip2.Items.Insert(3, New ToolStripSeparator())
            ContextMenuStrip2.Items.Insert(4, SelectAllToolStripMenuItem)
            ContextMenuStrip2.Items.Insert(5, CutToolStripMenuItem)
            ContextMenuStrip2.Items.Insert(6, CopyToolStripMenuItem)
            ContextMenuStrip2.Items.Insert(7, PasteToolStripMenuItem)

            ContextMenuStrip2.Show(fctbn, e.Location)

            If Not Clipboard.ContainsText Then
                ContextMenuStrip2.Items(7).Enabled = False
            Else
                ContextMenuStrip2.Items(7).Enabled = True
            End If

            If fctbn.SelectedText.Any Then
                ContextMenuStrip2.Items(5).Enabled = True
                ContextMenuStrip2.Items(6).Enabled = True
            Else
                ContextMenuStrip2.Items(5).Enabled = False
                ContextMenuStrip2.Items(6).Enabled = False
            End If

            If Clipboard.ContainsImage Then
                ContextMenuStrip2.Items.Insert(8, YouHaveABitmapOnTheClipboardToolStripMenuItem)
                ContextMenuStrip2.Items(8).Image = My.Resources.kS9Kf
                ContextMenuStrip2.Items(8).Font = New Font(ContextMenuStrip2.Items(8).Font, FontStyle.Bold)
                AddHandler(ContextMenuStrip2.Items(8).Click), AddressOf Clippy_Click
            End If



        End If





    End Sub

    Private Sub Clippy_Click()
        Form6.BringToFront()
    End Sub


    Private Sub fctbn_mousedoubleclick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim fctbn = DirectCast(sender, FastColoredTextBox)

    End Sub



    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        FolderBrowserDialog1.SelectedPath = Nothing
        FolderBrowserDialog1.ShowDialog()

        Try
            MAINDIR = FolderBrowserDialog1.SelectedPath



            Dim di As New DirectoryInfo(MAINDIR)
            Dim fiArr As FileInfo() = di.GetFiles()
            Dim fri As FileInfo

            'ListView1.Columns(0).Text = di.ToString
            'For Each fri In fiArr
            '    ListView1.Items.Add(fri.ToString)
            'Next


            TreeView1.Nodes.Clear()
            TreeView1.Nodes.Add(MAINDIR)
            'Populate this root node
            'BackgroundWorker1.RunWorkerAsync()
            'PopTree()
            PopulateTreeView(MAINDIR, TreeView1.Nodes(0))

            If My.Settings.RECENT_PROJECTS.Contains(MAINDIR.ToString) Then
            Else
                My.Settings.RECENT_PROJECTS.Add(MAINDIR.ToString)
                My.Settings.Save()
            End If
        Catch ex As Exception
            MsgBox("No directory was selected.")
        End Try

        REGEN_RECENTPROJ()

    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        If SplitContainer1.Orientation = Orientation.Horizontal Then
            SplitContainer1.Orientation = Orientation.Vertical
            SplitContainer2.Orientation = Orientation.Horizontal
            SplitContainer1.SplitterDistance = Me.Width / 3 * 2
            SplitContainer2.SplitterDistance = Me.Height / 4
        Else
            SplitContainer1.Orientation = Orientation.Horizontal
            SplitContainer2.Orientation = Orientation.Vertical
            SplitContainer1.SplitterDistance = Me.Height / 3 * 2
            SplitContainer2.SplitterDistance = Me.Width / 3
        End If

    End Sub

    Private Sub CSSToolbarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CSSToolbarToolStripMenuItem.Click
        If SplitContainer3.Panel2Collapsed = True Then
            SplitContainer3.Panel2Collapsed = False
        Else
            SplitContainer3.Panel2Collapsed = True
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        For Each paper In papers
            If ToolStripButton1.Checked = True Then
                paper.CurrentLineColor = ColorDialog2.Color
            Else
                paper.CurrentLineColor = Color.FromArgb(39, 39, 39)
            End If
        Next
    End Sub

    Private Sub ColorPickerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColorPickerToolStripMenuItem.Click
        ColorDialog1.ShowDialog()

        Dim CodeCodeInHex As String = ColorDialog1.Color.ToArgb().ToString("X")
        CodeCodeInHex = CodeCodeInHex.Remove(0, 2)
        BGCOLOR.Text = "#" & CodeCodeInHex
        ToolStripButton7.BackColor = ColorDialog1.Color

        ToolStripTextBox1.Text = ColorDialog1.Color.A & ", " & ColorDialog1.Color.R & ", " & ColorDialog1.Color.G & ", " & ColorDialog1.Color.B
        Form7.gen_color()
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        ColorDialog2.ShowDialog()
        For Each paper In papers
            If ToolStripButton1.Checked = True Then
                paper.CurrentLineColor = ColorDialog2.Color
            Else
                paper.CurrentLineColor = Color.FromArgb(39, 39, 39)
            End If
        Next
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        For Each paper In papers
            If ToolStripButton5.Checked = True Then
                paper.ShowLineNumbers = True
            Else
                paper.ShowLineNumbers = False
            End If
        Next
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        For Each paper In papers
            If ToolStripButton6.Checked = True Then
                paper.AutoCompleteBrackets = True
            Else
                paper.AutoCompleteBrackets = False
            End If
        Next

    End Sub

    Private Sub FTS_TabStripItemSelectionChanged(e As FarsiLibrary.Win.TabStripItemChangedEventArgs) Handles FTS.TabStripItemSelectionChanged
        Try
            selectedfile = FTS.SelectedItem.Title
        Catch ex As Exception

        End Try




    End Sub








    'Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs)
    '    'Dim fileReader As System.IO.StreamReader
    '    'fileReader =
    '    '        My.Computer.FileSystem.OpenTextFileReader(TreeView1.SelectedNode.FullPath)
    '    'Dim stringReader As String
    '    'stringReader = fileReader.ReadToEnd()
    '    ''fctbn.Text = stringReader
    '    'MsgBox(TreeView1.SelectedNode.FullPath)
    '    'webcontrol2.LoadHTML(stringReader)


    'End Sub


    Private Sub webcontrol2_LoadingFrameComplete(sender As Object, e As FrameEventArgs) Handles WebControl2.LoadingFrameComplete
        ToolStripButton8.Image = My.Resources.bulletking
        Try
            Dim webclient As New WebClient
            Dim IconUrl As New Uri(WebControl2.Source.ToString)
            Dim MemoryStream As New MemoryStream(webclient.DownloadData("http://" & IconUrl.Host & "/favicon.ico"))
            Dim webicon As New Icon(MemoryStream)
            ToolStripButton8.Image = webicon.ToBitmap
        Catch ex As Exception
            ToolStripButton8.Image = My.Resources.orange_error_icon_0
        End Try
        ToolStripLabel3.Text = WebControl2.Title



        Dim newstring As String = WebControl2.Source.ToString.Replace("%20", " ")
        Dim newstring2 As String = newstring.Replace("file:///", "")

        Dim fileName As String = newstring2
        Dim pathname As String = FolderBrowserDialog1.SelectedPath & "\"
        Dim result As String

        result = Path.GetFileName(fileName)


        Dim SavePath As String = System.IO.Path.Combine(pathname, fileName) 'combines the saveDirectory and the filename to get a fully qualified path.

        If System.IO.File.Exists(SavePath) Then
            'The file exists
            ToolStripLabel2.Text = result
            'MsgBox("filename:" & fileName & vbNewLine & "pathname:" & pathname & vbNewLine & "result:" & result & vbNewLine & "savepath:" & SavePath)
        Else
            'the file doesn't exist
            ToolStripLabel2.Text = "(OUTSIDE SOURCE)"
        End If

        If SplitContainer1.Panel1Collapsed = True Then
        Else
            Form5.WebControl1.Source = WebControl2.Source
        End If

        'Form5.Text = "Kryptonium " & FolderBrowserDialog1.SelectedPath

        Cursor = Cursors.Default
    End Sub

    Private Sub webcontrol2_LoadingFrame(sender As Object, e As LoadingFrameEventArgs) Handles WebControl2.LoadingFrame
        ToolStripButton8.Image = My.Resources.star
        Cursor = Cursors.WaitCursor
    End Sub


    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        WebControl2.GoBack()
    End Sub

    Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs) Handles ToolStripButton10.Click
        WebControl2.GoForward()
    End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs) Handles ToolStripButton11.Click
        WebControl2.Stop()
    End Sub

    Private Sub ToolStripButton12_Click(sender As Object, e As EventArgs) Handles ToolStripButton12.Click
        WebControl2.Reload(True)
    End Sub









    Private Sub ToolStripButton13_Click(sender As Object, e As EventArgs) Handles ToolStripButton13.Click
        If SplitContainer2.RightToLeft = RightToLeft.Yes Then
            SplitContainer2.RightToLeft = RightToLeft.No
        ElseIf SplitContainer2.RightToLeft = RightToLeft.No Then
            SplitContainer2.RightToLeft = RightToLeft.Yes
        End If
        ToolStrip1.RightToLeft = RightToLeft.No
        ToolStrip2.RightToLeft = RightToLeft.No
        FTS.RightToLeft = RightToLeft.No

    End Sub

    Private Sub ToolStripButton14_Click(sender As Object, e As EventArgs) Handles ToolStripButton14.Click
        If SplitContainer1.Panel2Collapsed = False Then
            SplitContainer1.Panel2Collapsed = True
            ToolStripButton14.Image = My.Resources.eyesclosed
        ElseIf SplitContainer1.Panel2Collapsed = True Then
            SplitContainer1.Panel2Collapsed = False
            ToolStripButton14.Image = My.Resources.eyesopened
        End If
    End Sub

    Private Sub AddNewFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewFileToolStripMenuItem.Click
        'MsgBox(TreeView1.SelectedNode.FullPath)
        filepath = TreeView1.SelectedNode.FullPath

        Form3.Show()

    End Sub

    Public Sub REFRESHTREE()

        Try
            MAINDIR = FolderBrowserDialog1.SelectedPath

            Dim di As New DirectoryInfo(MAINDIR)
            Dim fiArr As FileInfo() = di.GetFiles()
            Dim fri As FileInfo

            'ListView1.Columns(0).Text = di.ToString
            'For Each fri In fiArr
            '    ListView1.Items.Add(fri.ToString)
            'Next
            TreeView1.Nodes.Clear()
            TreeView1.Nodes.Add(MAINDIR)
            'Populate this root node
            PopulateTreeView(MAINDIR, TreeView1.Nodes(0))
            TreeView1.Nodes(0).Expand()
        Catch ex As Exception
            MsgBox("No directory was selected.")
        End Try
    End Sub
    'Private Sub ToolStripButton15_Click(sender As Object, e As EventArgs)

    '    If SplitContainer1.RightToLeft = RightToLeft.No Then
    '        SplitContainer1.RightToLeft = RightToLeft.Yes
    '    ElseIf SplitContainer1.RightToLeft = RightToLeft.Yes Then
    '        SplitContainer1.RightToLeft = RightToLeft.No
    '    End If

    'End Sub

    Private Sub LoadThisPageToBrowserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadThisPageToBrowserToolStripMenuItem.Click
        Dim loader As String = "file:///"
        WebControl2.Source = New Uri(loader & CURRENTEDIT)
        Form5.WebControl1.Source = New Uri(loader & CURRENTEDIT)
        ToolStripLabel2.Text = FTS.SelectedItem.Title

    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        Try
            For Each paper In papers
                If paper.Name = CURRENTEDIT Then
                    Clipboard.Clear()
                    Clipboard.SetText(paper.SelectedText)
                    paper.SelectedText = ""
                End If
            Next
        Catch ex As Exception

        End Try


    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        'Try
        For Each paper In papers
            If paper.Name = CURRENTEDIT Then
                Clipboard.Clear()
                Clipboard.SetText(paper.SelectedText)
            End If
        Next
        'Catch ex As Exception

        'End Try

        'MsgBox(papers.Item(CURRENTEDIT))
        'Clipboard.Clear()
        'Clipboard.SetText(CURRENTEDIT.SelectedText)

    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        'Try
        For Each paper In papers
            If paper.Name = CURRENTEDIT Then
                paper.SelectedText = Clipboard.GetText

            End If
        Next
        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub ExplorerHereToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExplorerHereToolStripMenuItem.Click
        Process.Start(TreeView1.SelectedNode.FullPath)
    End Sub

    Private Sub CopyPathToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyPathToolStripMenuItem.Click
        Clipboard.SetText(TreeView1.SelectedNode.FullPath)
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        'Try
        For Each paper In papers
            If paper.Name = CURRENTEDIT Then
                paper.SelectAll()
            End If
        Next
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub FileNameOnlyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileNameOnlyToolStripMenuItem.Click
        Clipboard.SetText(TreeView1.SelectedNode.Text)
    End Sub

    Private Sub FullPathAndFileNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullPathAndFileNameToolStripMenuItem.Click
        Clipboard.SetText(TreeView1.SelectedNode.FullPath)
    End Sub

    Private Sub TreeView1_NodeMouseHover(sender As Object, e As TreeNodeMouseHoverEventArgs) Handles TreeView1.NodeMouseHover

        'Dim messageBoxVB As New System.Text.StringBuilder()
        'messageBoxVB.AppendFormat("{0} = {1}", "Node", e.Node)
        'messageBoxVB.AppendLine()
        'MessageBox.Show(messageBoxVB.ToString(), "NodeMouseHover Event")

        'Try
        '    Dim xpos As New Integer
        '    Dim ypos As New Integer
        '    xpos = Cursor.Position.X
        '    ypos = Cursor.Position.Y
        '    Dim pos As New Point
        '    pos = MousePosition
        '    pos.X = pos.X - xpos
        '    pos.Y = pos.Y - ypos


        '    Dim LOCATOR As String = e.Node.FullPath
        '    Dim bittymap As New Bitmap(e.Node.FullPath)
        '    Dim VIEWER As New PictureBox
        '    VIEWER.Width = bittymap.Width
        '    VIEWER.Height = bittymap.Height

        '    VIEWER.Image = bittymap
        '    Me.Controls.Add(VIEWER)
        '    VIEWER.Location = pos
        '    VIEWER.BringToFront()
        '    Me.Controls.Remove(VIEWER)
        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        ColorDialog1.ShowDialog()

        Dim CodeCodeInHex As String = ColorDialog1.Color.ToArgb().ToString("X")
        CodeCodeInHex = CodeCodeInHex.Remove(0, 2)
        BGCOLOR.Text = "#" & CodeCodeInHex
        ToolStripButton7.BackColor = ColorDialog1.Color
    End Sub

    <DllImport("user32.dll")>
    Private Shared Function SetSystemCursor(ByVal hCursor As IntPtr, ByVal id As Integer) As Boolean
    End Function

    <DllImport("user32.dll")> Private Shared Function LoadCursorFromFile(ByVal fileName As String) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Public Shared Function CopyIcon(ByVal hIcon As IntPtr) As IntPtr
    End Function

    Const OCR_NORMAL As UInteger = 32512

    Dim eyedroppa94 As IntPtr

    Private Sub GetColorFromScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetColorFromScreenToolStripMenuItem.Click
        Panel2.Visible = True
        SplitContainer3.Panel2Collapsed = False
        SplitContainer3.SplitterDistance = SplitContainer3.Width - 297



        'SplitContainer3.Panel2.Width = 297
        Timer2.Start()


        'Dim Image1 As New Bitmap(canvaseditor.Image, canvaseditor.Width, canvaseditor.Height)

        'Using g As Graphics = Graphics.FromImage(Image1)
        '    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
        '    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
        '    g.DrawImage(BackgroundImage1, 0, 0, Image1.Width, Image1.Height)
        'End Using




    End Sub


    'Private Sub main()
    '    MouseX = CInt(Cursor.Position.X.ToString())
    '    MouseY = CInt(Cursor.Position.Y.ToString())
    '    Form4.Left = MouseX - Form4.Width / 2
    '    Form4.Top = MouseY - Form4.Height / 2

    'End Sub

    'Private Sub dickless()
    '    Do While Timer2.Enabled = True
    '        If MouseX > Form4.Left Then
    '            main()
    '        End If
    '        If MouseX <Form4.Right Then
    '            main()
    '        End If
    '        If MouseY > Form4.Top Then
    '            main()
    '        End If
    '        If MouseY < Form4.Bottom Then
    '            main()
    '        End If
    '    Loop
    'End Sub


    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick


        'If SplitContainer3.Panel2.Height < 316 Then
        '    SplitContainer1.SplitterDistance -= 500
        '    If SplitContainer3.Panel2.Height > 316 Then
        '        SplitContainer1.SplitterDistance = 1000
        '    End If
        'End If



        'Form4.Location = New System.Drawing.Point(MousePosition.X + 5 - Form4.Width / 2, MousePosition.Y + 5 - Form4.Height / 2)
        MouseX = CInt(Cursor.Position.X.ToString())
        MouseY = CInt(Cursor.Position.Y.ToString())


        Dim cur As Icon
        cur = My.Resources.eyedroppa94
        defaultcurse = New Cursor(cur.Handle)
        Cursor = defaultcurse


        Form4.Label1.Left = Form4.Width / 2 - Form4.Label1.Width / 2
        Label3.Left = Panel3.Right - Label3.Width
        Try
            'Dim bounds As Rectangle

            'Dim graph As Graphics
            'bounds = Screen.PrimaryScreen.Bounds
            'screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb)
            'graph = Graphics.FromImage(screenshot)
            'graph.CopyFromScreen(0, 0, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)

            Using bmp2 As New Bitmap(1, 1)
                Using g As Graphics = Graphics.FromImage(bmp2)
                    g.CopyFromScreen(Cursor.Position,
                                          New Point(0, 0), New Size(1, 1))
                End Using

                firsty = bmp2.GetPixel(0, 0).ToArgb
                ToolStripButton7.BackColor = Color.FromArgb(firsty)
                Form4.PictureBox1.BackColor = Color.FromArgb(firsty)
                Form4.PictureBox5.BackColor = Color.FromArgb(firsty)
                Form4.PictureBox4.BackColor = Color.FromArgb(firsty)
                Form4.PictureBox3.BackColor = Color.FromArgb(firsty)
                Panel3.BackColor = Color.FromArgb(firsty)



                Dim firstx As Color = ToolStripButton7.BackColor
                ToolStripTextBox1.Text = firstx.A & ", " & firstx.R & ", " & firstx.G & ", " & firstx.B




                'LIVE VIEW OF COLORS AND VALUES IN FORM7
                If Form7.CheckBox1.Checked = True Then


                    Form7.PictureBox1.BackColor = Color.FromArgb(firsty)
                    Dim F7color1 As Color = Color.FromArgb(firsty)
                    Dim F7color2 As Color

                    Dim F7ass As Color = F7color1
                    F7ass = Color.FromArgb(255, 255 - F7ass.R, 255 - F7ass.G, 255 - F7ass.B)
                    F7color2 = F7ass
                    Form7.PictureBox1.BackColor = Color.FromArgb(firsty)
                    Form7.PictureBox2.BackColor = F7color2

                    Form7.TextBox1.Text = Color.FromArgb(firsty).R & ", " & Color.FromArgb(firsty).G & ", " & Color.FromArgb(firsty).B
                    Form7.TextBox2.Text = Form7.PictureBox2.BackColor.R & ", " & Form7.PictureBox2.BackColor.G & ", " & Form7.PictureBox2.BackColor.B

                    Dim F7CodeCodeInHex As String = F7color1.ToArgb().ToString("X")
                    F7CodeCodeInHex = F7CodeCodeInHex.Remove(0, 2)
                    Form7.TextBox3.Text = "#" & F7CodeCodeInHex

                    Dim F7CodeCodeInHex2 As String = F7color2.ToArgb().ToString("X")
                    F7CodeCodeInHex2 = F7CodeCodeInHex2.Remove(0, 2)
                    Form7.TextBox4.Text = "#" & F7CodeCodeInHex2

                End If

                Me.Invalidate()

                Dim CodeCodeInHex As String = bmp2.GetPixel(0, 0).ToArgb().ToString("X")
                CodeCodeInHex = CodeCodeInHex.Remove(0, 2)
                BGCOLOR.Text = "#" & CodeCodeInHex
                Form4.Label1.Text = "#" & CodeCodeInHex
                Label3.Text = "#" & CodeCodeInHex
            End Using

            'ColorDialog1.Color = screenshot.GetPixel(MouseX, MouseY)
            'ToolStripButton7.BackColor = ColorDialog1.Color


            Form4.Show()
            Form4.TopMost = True

            Form4.Width = 100
            Form4.Height = 100
            Form4.Left = MouseX - Form4.Width / 2
            Form4.Top = MouseY - Form4.Height / 2


            Using pic As New Bitmap(Form4.PictureBox2.Width, Form4.PictureBox2.Height)
                'Dim pixeled As New Bitmap(pic, New Size(512, 512))
                bmpNew = New Bitmap(pic, 252, 252)

                Dim gfx As Graphics = Graphics.FromImage(pic)
                gfx.CopyFromScreen(New Point(Form4.Location.X + Form4.PictureBox2.Location.X, Form4.Location.Y + Form4.PictureBox2.Location.Y), Point.Empty, pic.Size)

                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(pic, 0, 0, 252, 252)
                    PictureBox2.BackgroundImage = bmpNew
                End Using
            End Using



            'Dim ANIMAL As String = Color.FromArgb(firsty).GetBrightness.ToString()

            'If ANIMAL < 0.5 Then
            '    MARK = Color.Blue
            'Else
            '    MARK = Color.OrangeRed
            'End If

            'Dim r4, g4, b4 As Integer
            'MARK = Color.FromArgb((r4 + 128) Mod 256, (g4 + 128) Mod 256, (b4 + 128) Mod 256)


            'Translate color to INVERTED
            Dim ass As Color = Color.FromArgb(firsty)
            ass = Color.FromArgb(255, 255 - ass.R, 255 - ass.G, 255 - ass.B)
            MARK = ass



            ''load and draw the image(s) once
            'BackgroundImage1 = New Bitmap(TL14.Image)
            'bmpnew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
            'Using g As Graphics = Graphics.FromImage(bmpnew)
            '    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
            '    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
            '    g.DrawImage(BackgroundImage1, 0, 0, bmpnew.Width, bmpnew.Height)
            'End Using
            'canvaseditor.Focus()
            'GroupBox13.Focus()
            'ComboBox2.SelectedItem = "14 Slope A"
            'Me.CenterToScreen()





            'Dim gr As Graphics
            '' Create pen.
            'Dim blackPen As New Pen(Color.Black, 50)

            '' Create rectangle.
            'Dim rect As New Rectangle(x:=Form4.Left + 20, y:=Form4.Top + 20, width:=Form4.Width - 20, height:=Form4.Height - 20)

            '' Draw rectangle to screen.
            'gr.DrawRectangle(blackPen, rect)


            'SnapToControl(Panel1)


            SetHookMouse()


            Select Case MouseButtons
                Case MouseButtons.Left

                    'Cursor = Cursors.Default
                    'Form4.Close()
                    'SplitContainer3.SplitterDistance = SplitContainer3.Width - SPLITC3
                    'Me.TopMost = True

                    'ColorDialog1.Color = Color.FromArgb(firsty)
                    'ToolStripTextBox1.Text = ColorDialog1.Color.A & ", " & ColorDialog1.Color.R & ", " & ColorDialog1.Color.G & ", " & ColorDialog1.Color.B
                    'Panel2.Visible = False
                    'If CSSToolbarToolStripMenuItem.Checked = True Then

                    'Else
                    '    SplitContainer3.Panel2Collapsed = True
                    'End If
                    'Timer2.Stop()
                    'Me.TopMost = False
                   'SendKeys.Send("{ENTER}")
                Case MouseButtons.Right

                Case MouseButtons.Middle

                Case MouseButtons.Left + MouseButtons.Right

            End Select



        Catch ex As Exception

        End Try




    End Sub

    Private Sub getcolor()
        Cursor = Cursors.Default
        Form4.Close()
        SplitContainer3.SplitterDistance = SplitContainer3.Width - SPLITC3
        'Me.TopMost = True

        ColorDialog1.Color = Color.FromArgb(firsty)
        ToolStripTextBox1.Text = ColorDialog1.Color.A & ", " & ColorDialog1.Color.R & ", " & ColorDialog1.Color.G & ", " & ColorDialog1.Color.B
        Panel2.Visible = False
        If CSSToolbarToolStripMenuItem.Checked = True Then

        Else
            SplitContainer3.Panel2Collapsed = True
        End If
        Timer2.Stop()
        PictureBox2.BackgroundImage = Nothing
        'Me.TopMost = False

        Form7.gen_color()
    End Sub


    Private Sub PictureBox2_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox2.Paint
        If GRIDTOGGLE = True Then
            PictureBox2.BorderStyle = BorderStyle.None

            Dim g As Graphics = e.Graphics
            Dim pn As New Pen(GRID) '~~~ color of the lines

            Dim x As Integer
            Dim y As Integer

            Dim intSpacing As Integer = 6 '~~~ spacing between adjacent lines


            '~~~ Draw the horizontal lines
            x = PictureBox2.Width
            For y = 0 To PictureBox2.Height Step intSpacing
                g.DrawLine(pn, New Point(0, y), New Point(x, y))
            Next

            '~~~ Draw the vertical lines
            y = PictureBox2.Height
            For x = 0 To PictureBox2.Width Step intSpacing
                g.DrawLine(pn, New Point(x, 0), New Point(x, y))
            Next



            Dim gr As Graphics = e.Graphics
            Dim pen As New Pen(MARK)     '   ~~~ color Of the lines

            Dim intSpacing2 As Integer = 6 '~~~ spacing between adjacent lines
            '~~~ Draw the horizontal lines
            x = 126
            For y = 126 To 132 Step intSpacing2
                gr.DrawLine(pen, New Point(132, y), New Point(x, y))
            Next

            '~~~ Draw the vertical lines
            y = 126
            For x = 126 To 132 Step intSpacing2
                gr.DrawLine(pen, New Point(x, 132), New Point(x, y))
            Next
        Else
            PictureBox2.BorderStyle = BorderStyle.FixedSingle

            Dim g As Graphics = e.Graphics
            Dim pn As New Pen(GRID) '~~~ color of the lines

            Dim x As Integer
            Dim y As Integer

            Dim intSpacing As Integer = 6 '~~~ spacing between adjacent lines

            Dim gr As Graphics = e.Graphics
            Dim pen As New Pen(MARK)     '   ~~~ color Of the lines

            Dim intSpacing2 As Integer = 6 '~~~ spacing between adjacent lines
            '~~~ Draw the horizontal lines
            x = 126
            For y = 126 To 132 Step intSpacing2
                gr.DrawLine(pen, New Point(132, y), New Point(x, y))
            Next

            '~~~ Draw the vertical lines
            y = 126
            For x = 126 To 132 Step intSpacing2
                gr.DrawLine(pen, New Point(x, 132), New Point(x, y))
            Next
        End If


    End Sub

    'Public Sub SnapToControl(ByVal Control As Control)
    '    ' Snaps the cursor to the bottom middle of the passed control
    '    Dim objPoint As Point = Control.PointToScreen(New Point(0, 0))
    '    objPoint.X += (Control.Width / 2)
    '    objPoint.Y += ((Control.Height / 4) * 3)
    '    Cursor.Position = objPoint
    'End Sub








    Private Sub ViewInExplorerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewInExplorerToolStripMenuItem.Click
        Dim myPath As String = TreeView1.SelectedNode.Parent.FullPath
        Dim myfile As String = TreeView1.SelectedNode.Text
        Dim myfilepath As String = TreeView1.SelectedNode.FullPath
        Dim all As String = TreeView1.SelectedNode.FullPath
        'Process.Start(myPath)


        Dim i = Shell("explorer /select, " & myfilepath, AppWinStyle.NormalFocus)

        'ShellExecute(Me.hwnd, vbNullString, “explorer”, “/select,” & myPath, vbNullString, 1)
        'Process.Start(“explorer”, “/select,” & myfile)

    End Sub

    Private Sub Splitter1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles Splitter1.SplitterMoved

    End Sub

    Private Sub CreateHTMLlinkRelAttributeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateHTMLlinkRelAttributeToolStripMenuItem.Click
        Clipboard.SetText(TextBox2.Text & TreeView1.SelectedNode.Text & TextBox3.Text)
    End Sub

    Private Sub CreateJavascriptlinkTagToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateJavascriptlinkTagToolStripMenuItem.Click
        Clipboard.SetText(TextBox4.Text & TreeView1.SelectedNode.Text & TextBox5.Text)
    End Sub

    Private Sub ToolStripButton15_Click(sender As Object, e As EventArgs) Handles ToolStripButton15.Click
        REFRESHTREE()
    End Sub

    Private Sub ToolStripButton16_Click(sender As Object, e As EventArgs) Handles ToolStripButton16.Click
        SplitContainer1.Panel1Collapsed = True
        Form5.Show()
    End Sub



    Private Sub Awesomium_Windows_Forms_WebControl_ShowCreatedWebView(sender As Object, e As ShowCreatedWebViewEventArgs) Handles WebControl2.ShowCreatedWebView

    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect

    End Sub

    Private Sub KryptoniumLogoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KryptoniumLogoToolStripMenuItem.Click
        wallpaper = "kryptangle"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.kryptangle
        Next
    End Sub

    Private Sub NoneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoneToolStripMenuItem.Click
        wallpaper = ""
        For Each paper In papers
            paper.BackgroundImage = Nothing
        Next
    End Sub


    Private Sub Form1_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        Cursor = defaultcurse
    End Sub

    Private Sub SplitContainer3_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer3.SplitterMoved
        SPLITC3 = SplitContainer3.Panel2.Width
    End Sub

    Private Sub CustomToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomToolStripMenuItem.Click
        OpenFileDialog1.ShowDialog()

    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As CancelEventArgs) Handles OpenFileDialog1.FileOk
        wallpaper = "custom"
        For Each paper In papers
            paper.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)
        Next
        My.Settings.wally = wallpaper

        Try
            img.Dispose()
            Dim savebmp As Bitmap = Image.FromFile(OpenFileDialog1.FileName)
            savebmp.Save(defaultdirectory & "\custom.png", System.Drawing.Imaging.ImageFormat.Png)
        Catch ex As Exception

        End Try


    End Sub

    Private Sub DarkCrownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DarkCrownToolStripMenuItem.Click
        wallpaper = "krydarkcrown"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.krydarkcrown
        Next
        My.Settings.wally = wallpaper
    End Sub

    Private Sub KryptoniumIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KryptoniumIconToolStripMenuItem.Click
        wallpaper = "krydark"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.krydark
        Next
        My.Settings.wally = wallpaper
    End Sub

    Private Sub DarkCrownsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DarkCrownsToolStripMenuItem.Click
        wallpaper = "krdarkcrowns"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.krdarkcrowns
        Next
        My.Settings.wally = wallpaper
    End Sub

    Private Sub ShadowCrownsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShadowCrownsToolStripMenuItem.Click
        wallpaper = "krdarkcrownsshad"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.krdarkcrownsshad
        Next
        My.Settings.wally = wallpaper
    End Sub

    Private Sub SquaresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SquaresToolStripMenuItem.Click
        wallpaper = "squares"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.squares
        Next
        My.Settings.wally = wallpaper
    End Sub

    Private Sub ShadowSquaresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShadowSquaresToolStripMenuItem.Click
        wallpaper = "shadowsquares"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.shadowsquares
        Next
        My.Settings.wally = wallpaper
    End Sub

    Private Sub DissolveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DissolveToolStripMenuItem.Click
        wallpaper = "krdisolved"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.krdisolved
        Next
        My.Settings.wally = wallpaper
    End Sub

    Private Sub Dissolve2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Dissolve2ToolStripMenuItem.Click
        wallpaper = "dissolve2"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.dissolve2
        Next
        My.Settings.wally = wallpaper
    End Sub

    Private Sub KrissKrossToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KrissKrossToolStripMenuItem.Click
        wallpaper = "crisscross"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.crisscross
        Next
        My.Settings.wally = wallpaper
    End Sub

    Private Sub KrissKrossDissolvedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KrissKrossDissolvedToolStripMenuItem.Click
        wallpaper = "crisscrossdissolved"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.crisscrossdissolved
        Next
        My.Settings.wally = wallpaper
    End Sub

    Private Sub PlaidToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlaidToolStripMenuItem.Click
        wallpaper = "krplad"
        For Each paper In papers
            paper.BackgroundImage = My.Resources.krplad
        Next
        My.Settings.wally = wallpaper
    End Sub

    Private Sub YouHaveABitmapOnTheClipboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YouHaveABitmapOnTheClipboardToolStripMenuItem.Click
        Form6.Show()
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    End Sub


    Public Class NodeSorter
        Implements IComparer

        Public Shared ReadOnly DefaultSorter As New NodeSorter

        Public Function Compare(x As Object, y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim treeX As TreeNode = TryCast(x, TreeNode)
            Dim treeY As TreeNode = TryCast(y, TreeNode)

            'If the tags are equal then compare by the text, otherwise use the tag value.
            If CInt(treeX.Name) = CInt(treeY.Name) Then
                Return String.Compare(treeX.Text, treeY.Text)
            Else
                Return CInt(treeX.Name).CompareTo(CInt(treeY.Name))
            End If

        End Function
    End Class


    Private Sub ToolStripButton17_Click(sender As Object, e As EventArgs) Handles ToolStripButton17.Click


        'local variables used to group nodes
        Dim myTreeView As New TreeView
        Dim HTML As New TreeNode
        Dim CSS As New TreeNode
        Dim JS As New TreeNode
        Dim PHP As New TreeNode
        Dim FOLDER As New TreeNode
        Dim FILE As New TreeNode



        'add child nodes to relevant group
        For Each n As TreeNode In TreeView1.Nodes


            If n.Name = "folder" Then

                For Each f As TreeNode In n.Nodes
                    If f.Name = "html" Then
                        n.Nodes.Add(f.Clone)
                    End If
                Next

                For Each f As TreeNode In n.Nodes
                    If f.Name = "css" Then
                        n.Nodes.Add(f.Clone)
                    End If
                Next

                For Each f As TreeNode In n.Nodes
                    If f.Name = "js" Then
                        n.Nodes.Add(f.Clone)
                    End If
                Next


                For Each f As TreeNode In n.Nodes
                    If f.Name = "php" Then
                        n.Nodes.Add(f.Clone)
                    End If
                Next


                For Each f As TreeNode In n.Nodes
                    If f.Name = "png" Then
                        n.Nodes.Add(f.Clone)
                    End If
                Next

                For Each f As TreeNode In n.Nodes
                    If f.Name = "bmp" Then
                        n.Nodes.Add(f.Clone)
                    End If
                Next

                For Each f As TreeNode In n.Nodes
                    If f.Name = "gif" Then
                        n.Nodes.Add(f.Clone)
                    End If
                Next

                For Each f As TreeNode In n.Nodes
                    If f.Name = "jpg" Then
                        n.Nodes.Add(f.Clone)
                    End If
                Next

                For Each f As TreeNode In n.Nodes
                    If f.Name = "txt" Then
                        n.Nodes.Add(f.Clone)
                    End If
                Next

                For Each f As TreeNode In n.Nodes
                    If f.Name = "font" Then
                        n.Nodes.Add(f.Clone)
                    End If
                Next



            End If
            FOLDER.Nodes.Add(n.Clone)
        Next


        'add our groups to a temporary treeview for sorting
        myTreeView.Nodes.Add(FOLDER)


        'myTreeView.Nodes.Add(HTML)
        'myTreeView.Nodes.Add(CSS)

        'sort alphabetically
        'myTreeView.Sort()

        'clear nodes from original treeview
        TreeView1.Nodes.Clear()

        'add sorted nodes back into the treeview
        For Each n As TreeNode In myTreeView.Nodes
            For Each j As TreeNode In n.Nodes
                TreeView1.Nodes.Add(j)
            Next
        Next











































        ''local variables used to group nodes
        'Dim myTreeView As New TreeView
        'Dim html As New TreeNode
        'Dim css As New TreeNode

        ''add child nodes to relevant group
        'For Each n As TreeNode In TreeView1.Nodes
        '    If n.Name = "html" Then
        '        html.Nodes.Add(n.Clone)
        '    ElseIf n.Name = "css" Then
        '        css.Nodes.Add(n.Clone)
        '    End If
        'Next

        ''add our groups to a temporary treeview for sorting
        'myTreeView.Nodes.Add(html)
        'myTreeView.Nodes.Add(css)

        '''sort alphabetically
        ''myTreeView.Sort()

        '''clear nodes from original treeview
        ''TreeView1.Nodes.Clear()

        '''add sorted nodes back into the treeview
        ''For Each n As TreeNode In myTreeView.Nodes
        ''    For Each j In n.Nodes
        ''        TreeView1.Nodes.Add(j)
        ''    Next
        ''Next





        'For Each tnode In TreeView1.Nodes
        '    'TreeView1.Nodes.CopyTo(SortedList)
        'Next

        'TreeView1.Nodes.Clear()
        'TreeView1.Nodes.Add(MAINDIR)

        'For Each node In TreeView1.Nodes(0).Nodes
        '    MsgBox(node.ToString & " : " & node.name & " : " & node.tag)
        'Next


        ''Populate this root node
        'PopulateTreeView(MAINDIR, TreeView1.Nodes(0))


        'Dim files() As String = IO.Directory.GetFiles(Dir)
        'TreeView1.Nodes(0).Tag = "folder"
        'If files.Length <> 0 Then
        '    Dim fileNode As TreeNode = Nothing
        '    For Each file As String In files
        '        fileNode = parentNode.Nodes.Add(IO.Path.GetFileName(file))
        '        fileNode.Tag = "file"
        '        If file.Contains(".html") Or file.Contains(".htm") Or file.Contains(".HTML") Or file.Contains(".HTM") Then
        '            fileNode.ImageKey = ImageList1.Images.Keys(1)
        '            fileNode.SelectedImageKey = ImageList1.Images.Keys(1)
        '        ElseIf file.Contains(".css") Or file.Contains(".CSS") Then
        '            fileNode.ImageKey = ImageList1.Images.Keys(2)
        '            fileNode.SelectedImageKey = ImageList1.Images.Keys(2)
        '        ElseIf file.Contains(".js") Or file.Contains(".JS") Then
        '            fileNode.ImageKey = ImageList1.Images.Keys(3)
        '            fileNode.SelectedImageKey = ImageList1.Images.Keys(3)
        '        ElseIf file.Contains(".php") Or file.Contains(".PHP") Then
        '            fileNode.ImageKey = ImageList1.Images.Keys(4)
        '            fileNode.SelectedImageKey = ImageList1.Images.Keys(4)
        '        ElseIf file.Contains(".png") Or file.Contains(".PNG") Then
        '            fileNode.ImageKey = ImageList1.Images.Keys(5)
        '            fileNode.SelectedImageKey = ImageList1.Images.Keys(5)
        '        ElseIf file.Contains(".bmp") Or file.Contains(".BMP") Then
        '            fileNode.ImageKey = ImageList1.Images.Keys(6)
        '            fileNode.SelectedImageKey = ImageList1.Images.Keys(6)
        '        ElseIf file.Contains(".gif") Or file.Contains(".GIF") Then
        '            fileNode.ImageKey = ImageList1.Images.Keys(7)
        '            fileNode.SelectedImageKey = ImageList1.Images.Keys(7)
        '        ElseIf file.Contains(".jpg") Or file.Contains(".jpeg") Or file.Contains(".JPG") Or file.Contains(".JPEG") Then
        '            fileNode.ImageKey = ImageList1.Images.Keys(8)
        '            fileNode.SelectedImageKey = ImageList1.Images.Keys(8)
        '        ElseIf file.Contains(".txt") Or file.Contains(".TXT") Then
        '            fileNode.ImageKey = ImageList1.Images.Keys(9)
        '            fileNode.SelectedImageKey = ImageList1.Images.Keys(9)
        '        ElseIf file.Contains(".ttf") Or file.Contains(".TTF") Or file.Contains(".otf") Or file.Contains(".OTF") Then
        '            fileNode.ImageKey = ImageList1.Images.Keys(10)
        '            fileNode.SelectedImageKey = ImageList1.Images.Keys(10)
        '        Else
        '            fileNode.ImageKey = ImageList1.Images.Keys(11)
        '            fileNode.SelectedImageKey = ImageList1.Images.Keys(11)
        '        End If
        '    Next
        'End If
        ''Add folders to treeview
        'Dim folders() As String = IO.Directory.GetDirectories(Dir)
        'If folders.Length <> 0 Then
        '    Dim folderNode As TreeNode = Nothing
        '    Dim folderName As String = String.Empty
        '    For Each folder In folders
        '        folderName = IO.Path.GetFileName(folder)
        '        folderNode = parentNode.Nodes.Add(folderName)
        '        folderNode.Tag = "folder"
        '        PopulateTreeView(folder, folderNode)
        '    Next
        'End If
        'Catch ex As UnauthorizedAccessException
        'parentNode.Nodes.Add("Access Denied")
        'End Try
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        SplitContainer3.Panel2Collapsed = True
        CSSToolbarToolStripMenuItem.Checked = False
    End Sub

    Private Sub ViewGridToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewGridToolStripMenuItem.Click
        If GRIDTOGGLE = True Then
            GRIDTOGGLE = False
            ViewGridToolStripMenuItem.Checked = False
        Else
            GRIDTOGGLE = True
            ViewGridToolStripMenuItem.Checked = True
        End If
    End Sub

    Private Sub FolderBrowserDialog1_HelpRequest(sender As Object, e As EventArgs) Handles FolderBrowserDialog1.HelpRequest

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ColorComplementaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColorComplementaryToolStripMenuItem.Click
        Form7.Show()
        If Form7.Visible = True Then
            Form7.BringToFront()
        End If
    End Sub



    Private Sub ToolStripDropDownButton1_MouseDown(sender As Object, e As MouseEventArgs) Handles ToolStripDropDownButton1.MouseDown
        If FTS.Items.Count > 0 Then
            BackgroundToolStripMenuItem.Enabled = True
        Else
            BackgroundToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub ContextMenuStrip3_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip3.Opening

    End Sub

    Private Sub ToolStripButton18_Click(sender As Object, e As EventArgs) Handles ToolStripButton18.Click
        Form8.Show()
        If Form8.Visible = True Then
            Form8.BringToFront()
        End If
    End Sub



    Private Sub SplitContainer1_MouseDown(sender As Object, e As MouseEventArgs) Handles SplitContainer1.MouseDown
        If SMOOTHSAILOR = True Then
            CType(sender, SplitContainer).IsSplitterFixed = True
        Else

        End If

    End Sub

    Private Sub SplitContainer1_MouseMove(sender As Object, e As MouseEventArgs) Handles SplitContainer1.MouseMove
        If SMOOTHSAILOR = True Then
            If CType(sender, SplitContainer).IsSplitterFixed Then

                If e.Button.Equals(MouseButtons.Left) Then

                    If CType(sender, SplitContainer).Orientation.Equals(Orientation.Vertical) Then

                        If e.X > 0 AndAlso e.X < CType(sender, SplitContainer).Width Then
                            CType(sender, SplitContainer).SplitterDistance = e.X
                            CType(sender, SplitContainer).Refresh()

                        End If
                    Else

                        If e.Y > 0 AndAlso e.Y < (CType(sender, SplitContainer)).Height Then
                            CType(sender, SplitContainer).SplitterDistance = e.Y
                            CType(sender, SplitContainer).Refresh()

                        End If
                    End If
                Else
                    CType(sender, SplitContainer).IsSplitterFixed = False
                End If
            End If
        Else

        End If



    End Sub

    Private Sub SplitContainer1_MouseUp(sender As Object, e As MouseEventArgs) Handles SplitContainer1.MouseUp
        CType(sender, SplitContainer).IsSplitterFixed = False
    End Sub



    Class SurroundingClass
        Private Sub SplitContainer1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)

        End Sub

        Private Sub SplitContainer1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)

        End Sub

        Private Sub SplitContainer1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)

        End Sub
    End Class






    Private Sub SplitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer1.SplitterMoved

    End Sub

    Private Sub SplitContainer1_SplitterMoving(sender As Object, e As SplitterCancelEventArgs) Handles SplitContainer1.SplitterMoving

    End Sub

    Private Sub ToolStripButton19_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripDropDownButton2_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton2.Click

    End Sub

    Private Sub TreeView1_AfterExpand(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterExpand
        PictureBox3.Visible = False
    End Sub


    'Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
    '    PopulateTreeView(MAINDIR, TreeView1.Nodes(0))
    'End Sub

    'Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
    '    PictureBox3.Visible = True
    'End Sub

    'Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
    '    PictureBox3.Visible = False
    'End Sub

















    'Private Sub ToolStripButton15_Click(sender As Object, e As EventArgs) Handles ToolStripButton15.Click
    '    Dim tot As Integer = SplitContainer1.Height
    '    Dim half1 As Integer = SplitContainer1.Panel1.Height
    '    Dim half2 As Integer = SplitContainer1.Panel2.Height

    '    Dim perc2 As Integer = half2 * 100 / tot
    '    'MsgBox(perc2)
    '    'MsgBox(tot & vbNewLine & half2)

    '    SplitContainer1.SplitterDistance = perc2
    'End Sub














    'Private Structure MSLLHOOKSTRUCT
    '    Public pt As Point
    '    Public mouseData As Int32
    '    Public flags As Int32
    '    Public time As Int32
    '    Public extra As IntPtr
    'End Structure
    'Private mHook As IntPtr = IntPtr.Zero
    'Private Const WH_MOUSE_LL As Int32 = &HE
    'Private Const WM_RBUTTONDOWN As Int32 = &H204
    'Private Const WM_LBUTTONDOWN As Int32 = &H201
    '<MarshalAs(UnmanagedType.FunctionPtr)> Private mProc As MouseHookDelegate
    'Private Declare Function SetWindowsHookExW Lib "user32.dll" (ByVal idHook As Int32, ByVal HookProc As MouseHookDelegate, ByVal hInstance As IntPtr, ByVal wParam As Int32) As IntPtr
    'Private Declare Function UnhookWindowsHookEx Lib "user32.dll" (ByVal hook As IntPtr) As Boolean
    'Private Declare Function CallNextHookEx Lib "user32.dll" (ByVal idHook As Int32, ByVal nCode As Int32, ByVal wParam As IntPtr, ByRef lParam As MSLLHOOKSTRUCT) As Int32
    'Private Declare Function GetModuleHandleW Lib "kernel32.dll" (ByVal fakezero As IntPtr) As IntPtr
    'Private Delegate Function MouseHookDelegate(ByVal nCode As Int32, ByVal wParam As IntPtr, ByRef lParam As MSLLHOOKSTRUCT) As Int32

    'Private Function SetHookMouse() As Boolean
    '    If mHook = IntPtr.Zero Then
    '        mProc = New MouseHookDelegate(AddressOf MouseHookProc)
    '        mHook = SetWindowsHookExW(WH_MOUSE_LL, mProc, GetModuleHandleW(IntPtr.Zero), 0)
    '    End If
    '    Return mHook <> IntPtr.Zero
    'End Function

    'Private Sub UnHookMouse()
    '    If mHook = IntPtr.Zero Then Return
    '    UnhookWindowsHookEx(mHook)
    '    mHook = IntPtr.Zero
    'End Sub

    'Private Function MouseHookProc(ByVal nCode As Int32, ByVal wParam As IntPtr, ByRef lParam As MSLLHOOKSTRUCT) As Int32

    '    If wParam.ToInt32 = WM_LBUTTONDOWN Then
    '        Return 1
    '    End If
    '    If wParam.ToInt32 = WM_RBUTTONDOWN Then
    '        Return 1
    '    End If
    '    Return CallNextHookEx(WH_MOUSE_LL, nCode, wParam, lParam)
    'End Function

    'Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    '    UnHookMouse() 'Unhook the mouse when form closes
    'End Sub


    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Fonta = My.Resources.clacon
    '    For Each paper In papers
    '        paper.Font = CustomFont.GetInstance(14, FontStyle.Regular)

    '    Next

    'End Sub



End Class





