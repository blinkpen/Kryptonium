Imports System
Imports System.IO

Public Class Form9
    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReloadList()
    End Sub

    Private Sub ReloadList()
        ListBox1.Items.Clear()

        For Each item In My.Settings.RECENT_PROJECTS
            ListBox1.Items.Add(item)
        Next

        Form1.REGEN_RECENTPROJ()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.RECENT_PROJECTS.Remove(ListBox1.SelectedItem)
        ReloadList()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class