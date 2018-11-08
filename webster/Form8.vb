Imports System.Threading

Public Class Form8


    Dim T1 As Integer = 0
    Dim T2 As Integer = 0

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        T1 = T1 + 1

        If T1 >= 200 Then
            Panel2.VerticalScroll.Value = Panel2.VerticalScroll.Value + 1
        End If

        If T1 >= 800 Then
            Timer1.Stop()
            T1 = 0
            Timer2.Start()
        End If

    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub





    Private Sub Panel2_MouseWheel(sender As Object, e As MouseEventArgs) Handles Panel2.MouseWheel


        Timer1.Stop()
        T1 = 0
        If Panel2.AutoScrollPosition.Y = 0 Then
            T1 = 10
            Timer1.Start()
        End If



        'If e.Delta > 0 Then
        '    Panel2.VerticalScroll.Value = Panel2.AutoScrollPosition.Y - 1
        'Else
        '    Panel2.VerticalScroll.Value = Panel2.AutoScrollPosition.Y + 1
        'End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        T2 = T2 + 1


        If T2 = 1 Then
            Do Until Panel2.AutoScrollPosition.Y = 0
                Panel2.VerticalScroll.Value = Panel2.VerticalScroll.Value - 1
            Loop
        End If




        If T2 >= 10 Then
            Timer2.Stop()
            T2 = 0
            Panel2.VerticalScroll.Value = 0
            Timer1.Start()
        End If

    End Sub

End Class