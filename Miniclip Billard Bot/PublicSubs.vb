Public Class PublicSubs

    Public Shared Sub ActionIndicatorTest(ByVal ProgressAction As BunifuCircleProgressbar)
        Dim i = 0
        While i < 800
            ProgressAction.Value = i
            ProgressAction.Update()
            i += 1
        End While
        ProgressAction.Value = 0
    End Sub




End Class

Class Shortcut


    'Function to save to a log
    Public Shared Sub LogIt(ByVal LogList As ListBox)
        Dim dateAsString = DateTime.Now.ToString("MMM-d-yyyy")
        Dim SplitTime = dateAsString.ToString().Split("-")
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\logs\" & dateAsString.ToString() & ".txt") Then
            FileOpen(1, My.Application.Info.DirectoryPath & "\logs\" & dateAsString.ToString() & ".txt", OpenMode.Append)
            PrintLine(1, Environment.NewLine + "Time:" + DateTime.Now.ToString("HH:mm:ss"))
            For i = 0 To LogList.Items.Count - 1
                PrintLine(1, LogList.Items(i))
            Next
        Else
            FileOpen(1, My.Application.Info.DirectoryPath & "\logs\" & dateAsString.ToString() & ".txt", OpenMode.Output)
            PrintLine(1, Environment.NewLine + "Time:" + DateTime.Now.ToString("HH:mm:ss"))
            For i = 0 To LogList.Items.Count - 1
                PrintLine(1, LogList.Items(i))
            Next

        End If
        FileClose()
    End Sub

End Class


Public Module ShortcutModule


    Public Sub SetWinPos()
        Dim x As Integer
        Dim y As Integer
        x = Screen.PrimaryScreen.WorkingArea.Width - 582
        y = Screen.PrimaryScreen.WorkingArea.Height - 1050
        Form1.Location = New Point(x, y)
    End Sub



    Public Sub KillBsMsgBox()

        Dim GetDate As String = My.Computer.Clock.LocalTime
        Dim ZxOrange = Color.FromArgb(255, 128, 0)
        Dim ZxLogRegular = Color.FromArgb(0, 163, 163)
        Dim ZxBlue = Color.FromArgb(0, 112, 224)
        Dim ZxLightBlue = Color.FromArgb(70, 163, 255)
        Dim ZxPinkRed = Color.FromArgb(255, 91, 91)

        If MessageBox.Show("Are you sure? BlueStacks processes will be terminated ", "Terminate Bluestacks", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

            For Each p6 As Process In Process.GetProcesses()
                If p6.ProcessName = "HD-Agent" Then
                    p6.Kill()

                    Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                    Form1.RichTextBox1.SelectionColor = Color.Beige
                    Form1.RichTextBox1.AppendText(GetDate + " : ")
                    Form1.RichTextBox1.SelectionColor = ZxPinkRed
                    Form1.RichTextBox1.AppendText("TERMINATED: HD-Agent.exe" + vbNewLine)
                    Form1.RichTextBox1.ScrollToCaret()

                End If
            Next

            For Each p6 As Process In Process.GetProcesses()
                If p6.ProcessName = "HD-LogRotatorService" Then
                    p6.Kill()

                    Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                    Form1.RichTextBox1.SelectionColor = Color.Beige
                    Form1.RichTextBox1.AppendText(GetDate + " : ")
                    Form1.RichTextBox1.SelectionColor = ZxPinkRed
                    Form1.RichTextBox1.AppendText("TERMINATED: HD-LogRotatorService.exe" + vbNewLine)
                    Form1.RichTextBox1.ScrollToCaret()


                End If
            Next

            For Each p6 As Process In Process.GetProcesses()
                If p6.ProcessName = "HD-OBS" Then
                    p6.Kill()

                    Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                    Form1.RichTextBox1.SelectionColor = Color.Beige
                    Form1.RichTextBox1.AppendText(GetDate + " : ")
                    Form1.RichTextBox1.SelectionColor = ZxPinkRed
                    Form1.RichTextBox1.AppendText("TERMINATED: HD-OBS.exe" + vbNewLine)
                    Form1.RichTextBox1.ScrollToCaret()

                End If
            Next

            For Each p6 As Process In Process.GetProcesses()
                If p6.ProcessName = "HD-Plus-Frontend" Then
                    p6.Kill()

                    Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                    Form1.RichTextBox1.SelectionColor = Color.Beige
                    Form1.RichTextBox1.AppendText(GetDate + " : ")
                    Form1.RichTextBox1.SelectionColor = ZxPinkRed
                    Form1.RichTextBox1.AppendText("TERMINATED: HD-Plus-Frontend.exe" + vbNewLine)
                    Form1.RichTextBox1.ScrollToCaret()

                End If
            Next

            For Each p6 As Process In Process.GetProcesses()
                If p6.ProcessName = "HD-Plus-Service" Then
                    p6.Kill()

                    Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                    Form1.RichTextBox1.SelectionColor = Color.Beige
                    Form1.RichTextBox1.AppendText(GetDate + " : ")
                    Form1.RichTextBox1.SelectionColor = ZxPinkRed
                    Form1.RichTextBox1.AppendText("TERMINATED: HD-Plus-Service.exe" + vbNewLine)
                    Form1.RichTextBox1.ScrollToCaret()

                End If
            Next

            For Each p7 As Process In Process.GetProcesses()
                If p7.ProcessName = "BlueStacks" Then
                    p7.Kill()

                    Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                    Form1.RichTextBox1.SelectionColor = Color.Beige
                    Form1.RichTextBox1.AppendText(GetDate + " : ")
                    Form1.RichTextBox1.SelectionColor = ZxPinkRed
                    Form1.RichTextBox1.AppendText("TERMINATED: BlueStacks.exe" + vbNewLine)
                    Form1.RichTextBox1.ScrollToCaret()

                End If
            Next

            For Each p8 As Process In Process.GetProcesses()
                If p8.ProcessName = "HD-UpdaterService" Then
                    p8.Kill()

                    Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                    Form1.RichTextBox1.SelectionColor = Color.Beige
                    Form1.RichTextBox1.AppendText(GetDate + " : ")
                    Form1.RichTextBox1.SelectionColor = ZxPinkRed
                    Form1.RichTextBox1.AppendText("TERMINATED: HD-UpdaterService.exe" + vbNewLine)
                    Form1.RichTextBox1.ScrollToCaret()

                End If
            Next


        End If
    End Sub



    Public Sub KillBs()

        Dim GetDate As String = My.Computer.Clock.LocalTime
        Dim ZxOrange = Color.FromArgb(255, 128, 0)
        Dim ZxLogRegular = Color.FromArgb(0, 163, 163)
        Dim ZxBlue = Color.FromArgb(0, 112, 224)
        Dim ZxLightBlue = Color.FromArgb(70, 163, 255)
        Dim ZxPinkRed = Color.FromArgb(255, 91, 91)


        For Each p6 As Process In Process.GetProcesses()
            If p6.ProcessName = "HD-Agent" Then
                p6.Kill()

                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = ZxPinkRed
                Form1.RichTextBox1.AppendText("TERMINATED: HD-Agent.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()

            End If
        Next

        For Each p6 As Process In Process.GetProcesses()
            If p6.ProcessName = "HD-LogRotatorService" Then
                p6.Kill()

                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = ZxPinkRed
                Form1.RichTextBox1.AppendText("TERMINATED: HD-LogRotatorService.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()


            End If
        Next

        For Each p6 As Process In Process.GetProcesses()
            If p6.ProcessName = "HD-OBS" Then
                p6.Kill()

                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = ZxPinkRed
                Form1.RichTextBox1.AppendText("TERMINATED: HD-OBS.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()

            End If
        Next

        For Each p6 As Process In Process.GetProcesses()
            If p6.ProcessName = "HD-Plus-Frontend" Then
                p6.Kill()

                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = ZxPinkRed
                Form1.RichTextBox1.AppendText("TERMINATED: HD-Plus-Frontend.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()

            End If
        Next

        For Each p6 As Process In Process.GetProcesses()
            If p6.ProcessName = "HD-Plus-Service" Then
                p6.Kill()

                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = ZxPinkRed
                Form1.RichTextBox1.AppendText("TERMINATED: HD-Plus-Service.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()

            End If
        Next

        For Each p7 As Process In Process.GetProcesses()
            If p7.ProcessName = "BlueStacks" Then
                p7.Kill()

                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = ZxPinkRed
                Form1.RichTextBox1.AppendText("TERMINATED: BlueStacks.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()

            End If
        Next

        For Each p8 As Process In Process.GetProcesses()
            If p8.ProcessName = "HD-UpdaterService" Then
                p8.Kill()

                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = ZxPinkRed
                Form1.RichTextBox1.AppendText("TERMINATED: HD-UpdaterService.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()

            End If
        Next


    End Sub


    Public Sub RestartBluestacks()
        Dim GetDate As String = My.Computer.Clock.LocalTime
        Dim ZxOrange = Color.FromArgb(255, 128, 0)
        Dim ZxLogRegular = Color.FromArgb(0, 163, 163)
        Dim ZxBlue = Color.FromArgb(0, 112, 224)
        Dim ZxLightBlue = Color.FromArgb(70, 163, 255)
        Dim ZxPinkRed = Color.FromArgb(255, 91, 91)

        For Each p5 As Process In Process.GetProcesses()
            'If p5.ProcessName = "BlueStacks" Then
            'p5.Kill()

            'Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
            'Form1.RichTextBox1.SelectionColor = Color.Beige
            'Form1.RichTextBox1.AppendText(GetDate + " : ")
            'Form1.RichTextBox1.SelectionColor = ZxPinkRed
            'Form1.RichTextBox1.AppendText("TERMINATED: BlueStacks.exe" + vbNewLine)
            'Form1.RichTextBox1.ScrollToCaret()


            If p5.ProcessName = "HD-RunApp" Then
                p5.Kill()

                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = ZxPinkRed
                Form1.RichTextBox1.AppendText("TERMINATED: HD-RunApp.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()

            ElseIf p5.ProcessName = "HD-RunAppTemp" Then
                p5.Kill()

                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = ZxPinkRed
                Form1.RichTextBox1.AppendText("TERMINATED: HD-RunAppTemp.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()


            ElseIf p5.ProcessName = "HD-UpdateInstaller" Then
                p5.Kill()

                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = ZxPinkRed
                Form1.RichTextBox1.AppendText("TERMINATED: HD-UpdateInstaller.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()

            ElseIf p5.ProcessName = "HD-UpdateService" Then
                p5.Kill()

                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = ZxPinkRed
                Form1.RichTextBox1.AppendText("TERMINATED: HD-UpdateService.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()

            Else

                Process.Start("C:\ProgramData\BlueStacksGameManager\BlueStacks.exe")
            End If
        Next


    End Sub
    Public Sub FocusBluestacks()
        On Error Resume Next
        Dim notepadID As Integer
        AppActivate("BlueStacks App Player")
    End Sub
    Public Sub CheckBsIfFalseThenStart()
        Dim GetDate As String = My.Computer.Clock.LocalTime
        'Dim ZxPinkRed = Color.FromArgb(255, 91, 91)

        For Each p5 As Process In Process.GetProcesses()
            If p5.ProcessName = "BlueStacks" Then
                'DO NOTHING
            Else
                Form1.RichTextBox1.Select(Form1.RichTextBox1.TextLength, 0)
                Form1.RichTextBox1.SelectionColor = Color.Beige
                Form1.RichTextBox1.AppendText(GetDate + " : ")
                Form1.RichTextBox1.SelectionColor = Color.SeaGreen
                Form1.RichTextBox1.AppendText("STARTED: BlueStacks.exe" + vbNewLine)
                Form1.RichTextBox1.ScrollToCaret()

                Process.Start("C:\ProgramData\BlueStacksGameManager\BlueStacks.exe")
            End If
        Next
    End Sub
End Module