Imports System.Drawing.Text
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Collections
Imports System.Diagnostics
Imports System.IO
Imports System.Text
Imports System.Net
Imports System
Imports System.Timers
Imports System.ComponentModel
Imports System.Diagnostics.Process
Imports System.Runtime.CompilerServices 'This is needed to define the aggressive inlining constant


Public Class Form1
    ' PublicSubs.ActionIndicatorTest(ProgressBarActionComplete)
    Public Property IsReadOnly As Boolean
    Dim sec As Integer = My.Settings.Seconds
    Dim min As Integer  = My.Settings.Minutes
    'Dim GetDate As String = My.Computer.Clock.LocalTime
    Dim GetDate As String = ("$BILLARDBOT")

    Dim ev1 As New Threading.Thread(AddressOf event1)
    Dim ev2 As New Threading.Thread(AddressOf event2)


    Dim AutoitBot() As Process

    Dim tCounter = 0
    ' Dim rCounter = 0

    Dim ZxOrange = Color.FromArgb(255, 128, 0)
    Dim ZxLogRegular = Color.FromArgb(0, 163, 163)
    Dim ZxBlue = Color.FromArgb(0, 112, 224)
    Dim ZxLightBlue = Color.FromArgb(70, 163, 255)
    Dim ZxPinkRed = Color.FromArgb(255, 91, 91)

    Dim BlockList() As String = {"miniclip", "androidinactive", "billardinactive1", "billardinactive2", "billardinactive3", "bluestacksexit",
        "exit1", "exit2", "exit3", "exit4", "exit5", "exit6", "exit7", "exit8", "exit9", "exit10", "exit11",
        "exit12", "exit13", "exit14", "exit15", "exit16", "exit17", "exit18", "exit19", "exit20", "exit21",
        "exit22", "exit23", "exit24", "exit25", "exit26", "exit27", "exit28", "exit29", "exit30", "exit31",
        "exit32", "exit33", "exit34", "exit35", "exit36", "exit37", "exit38", "exit39", "exit40", "exit41",
        "exit42", "exit43", "exit44", "exit45", "exit46", "billardactive1", "billardactive2"}

    'This Blocklist Will allow us to search for only the exit buttons
    Dim BlockList2() As String = {"billardinactive1""billardinactive2", "billardinactive3", "bluestacksexit",
        "eFreecoins1", "eFreecoins2", "eFreecoins3", "eFreecoins4", "exitoffer", "exitspin", "eYes", "facebook",
        "miniclip", "androidinactive", "billardactive1", "billardactive2"}

    Dim BlockList3() As String = {"miniclip", "bluestacksexit",
        "exit1", "exit2", "exit3", "exit4", "exit5", "exit6", "exit7", "exit8", "exit9", "exit10", "exit11",
        "exit12", "exit13", "exit14", "exit15", "exit16", "exit17", "exit18", "exit19", "exit20", "exit21",
        "exit22", "exit23", "exit24", "exit25", "exit26", "exit27", "exit28", "exit29", "exit30", "exit31",
        "exit32", "exit33", "exit34", "exit35", "exit36", "exit37", "exit38", "exit39", "exit40", "exit41",
        "exit42", "exit43", "exit44", "exit45", "exit46"}



#Region "Section - Declarations"

    Public Declare Function GetKeyPress Lib "user32" Alias "GetAsyncKeyState" (ByVal key As Integer) As Integer
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short
    Private Const mouseclickup = 4
    Private Const mouseclickdown = 2
    Public progress As Integer = 0
    Public countdown As Integer = 0
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Long) As Integer
    Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    Private Const MOUSEEVENTF_ABSOLUTE = &H8000 ' absolute move
    Private Const MOUSEEVENTF_LEFTDOWN = &H2 ' left button down
    Private Const MOUSEEVENTF_LEFTUP = &H4 ' left button up
    Private Const MOUSEEVENTF_MOVE = &H1 ' mouse move
    Private Const MOUSEEVENTF_MIDDLEDOWN = &H20
    Private Const MOUSEEVENTF_MIDDLEUP = &H40
    Private Const MOUSEEVENTF_RIGHTDOWN = &H8
    Private Const MOUSEEVENTF_RIGHTUP = &H10
    Declare Function SetCursorPos& Lib "user32" (ByVal p As Point)



#End Region

#Region "Section - DLL Imports/Mouse Events"
    <DllImport("kernel32.dll")>
    Private Overloads Shared Function WritePrivateProfileString(
ByVal lpApplicationName As String,
ByVal lpKeyName As String,
ByVal lpString As String,
ByVal lpFileName As String) As Integer
    End Function
    <DllImport("kernel32.dll")>
    Private Overloads Shared Function GetPrivateProfileString(
    ByVal lpApplicationName As String,
    ByVal lpKeyName As String,
    ByVal lpDefault As String,
    ByVal lpReturnedString As StringBuilder,
    ByVal nSize As Integer,
    ByVal lpFileName As String) As Integer
    End Function


#End Region

#Region "Section - Public Subs / Functions"
    ''' <summary>
    '''     #ActionIndicator
    '''     ** Progresses the value of a progressbar by 1
    '''     ** When it reaches 800 then it will reset back too 0
    '''     ** I have this too create a little cool effect using
    '''     ** the Bunifu ProgresControls when an action is performed.
    ''' 
    '''     #Delay
    '''     ** Simple time delay in vb.net without application Freezing
    '''     ** If I don't use this and use System.Threading.Thread.Sleep() 
    '''     ** the application becomes unresponsive. So I'm using this function
    '''     ** to let me pause
    ''' </summary>

    Public Sub ResetLblCount()
        lblMin.Text = txtMin.Text
        lblSec.Text = txtSec.Text
    End Sub

    Public Sub PositionMouseBottomRight()
        System.Windows.Forms.Cursor.Position = New System.Drawing.Point(1911, 1024)
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
    End Sub

    Public Sub SaveCountDownWatch()
        My.Settings.Minutes = txtMin.Text
        My.Settings.Seconds = txtSec.Text
        My.Settings.Save()


        RichTextBox1.SelectionColor = Color.Beige ' Color for Next Line = date
        RichTextBox1.AppendText(GetDate + ": ")
        RichTextBox1.SelectionColor = ZxLogRegular
        RichTextBox1.AppendText("SETTING: TIME/BOTTING saved!" + vbNewLine)




    End Sub


    Public Sub StopBotSub()
        'QUICKSEARCH: zxpwdstop
        On Error Resume Next



        If cboxEnableVbBot.Checked = True Then


#Disable Warning BC40000
            ev1.Suspend()
#Enable Warning BC40000
#Disable Warning BC40000
            ev2.Suspend()
#Enable Warning BC40000
#Disable Warning BC40000
#Enable Warning BC40000
            ImgSrchProgress.Value = 0
            ImgSrchProgress2.Value = 0
            ImgSrchProgress3.Value = 0
            ResetLblCount()
            tmrCount.Stop()
            tmrProcesses.Stop()
            tmrWatchDog.Stop()
            min = txtMin.Text
            sec = txtSec.Text
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxPinkRed
            RichTextBox1.AppendText("VB-BOT: Botting Stopped!" + vbNewLine)

            '''''''''''''''''''''''''''''''''''''''''''
            indicator.BackColor = Color.FromArgb(0, 112, 222)
            indicator.Location = New Point(-4, 65)
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            ' pnlHome.Location = New Point(51, 99)
            pnlHome.Show()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            'pnlSettings.Location = New Point(51, 99)
            pnlSettings.Hide()
            ' pnlSettings2.Location = New Point(51, 99)
            pnlSettings2.Hide()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            'pnlBotLog.Location = New Point(51, 99)
            pnlBotLog.Hide()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            ' pnlAutoitBot.Location = New Point(51, 99)
            pnlAutoitBot.Hide()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

        ElseIf cboxEnableAutoitBot.Checked = True Then

            For Each p1 As Process In Process.GetProcesses()
                If p1.ProcessName = "Bot" Then
                    p1.Kill()
                    'ListBox1.Items.Add("TERMINATED: Bot.exe")
                    RichTextBox1.SelectionColor = Color.Beige
                    RichTextBox1.AppendText(GetDate + ": ")
                    RichTextBox1.SelectionColor = ZxPinkRed
                    RichTextBox1.AppendText("TERMINATED: Bot.exe" + vbNewLine)
                End If
            Next

            tmrProcesses.Stop()
            tmrWatchDog.Stop()
            tmrCount.Stop()
            ResetLblCount()
            min = txtMin.Text
            sec = txtSec.Text
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxPinkRed
            RichTextBox1.AppendText("AUTOIT-BOT: Botting Stopped!" + vbNewLine)
            lblAutoitProcessCheck.Visible = False ' Show running processes. [BOT COUNT]
            tmrProcesses.Stop() ' Start the timer to check for the processes

            '''''''''''''''''''''''''''''''''''''''''''
            indicator.BackColor = Color.FromArgb(0, 112, 222)
            indicator.Location = New Point(-4, 65)
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            ' pnlHome.Location = New Point(51, 99)
            pnlHome.Show()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            'pnlSettings.Location = New Point(51, 99)
            pnlSettings.Hide()
            ' pnlSettings2.Location = New Point(51, 99)
            pnlSettings2.Hide()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            'pnlBotLog.Location = New Point(51, 99)
            pnlBotLog.Hide()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            ' pnlAutoitBot.Location = New Point(51, 99)
            pnlAutoitBot.Hide()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

        End If
    End Sub


    Public Sub StartBotSub()
        On Error Resume Next
        'QUICKSERACH: zxpwdstart

        If cboxEnableVbBot.Checked = True Then 'If VBBOT checkbox is checked then start the VB bot.
            FocusBluestacks()
            ImgSrchProgress3.Value = 0 ' Set the imagesearch at 0
            If tCounter = 0 Then
                ev1.IsBackground = True ' Start imagesearch timer 1
                ev1.Start() ' Start imagesearch timer 1
                ev2.IsBackground = True ' Start imagesearch timer 2
                ev2.Start() ' Start imagesearch timer 2
                tCounter += 1
                tmrWatchDog.Start() ' Start WatchDog to monitor the countdown timer.
                tmrCount.Start() ' Start the count down timer.

                RichTextBox1.SelectionColor = Color.Beige
                RichTextBox1.AppendText(GetDate + ": ")
                RichTextBox1.SelectionColor = Color.SeaGreen
                RichTextBox1.AppendText("VB-BOT: Botting Started!" + vbNewLine)

                '''''''''''''''''''''''''''''''''''''''''''
                indicator.BackColor = Color.FromArgb(0, 112, 222)
                indicator.Location = New Point(-4, 65)
                ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
                ' pnlHome.Location = New Point(51, 99)
                pnlHome.Hide()
                ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
                'pnlSettings.Location = New Point(51, 99)
                pnlSettings.Hide()
                ' pnlSettings2.Location = New Point(51, 99)
                pnlSettings2.Hide()
                ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
                pnlBotLog.Location = New Point(51, 99)
                pnlBotLog.Show()
                ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
                ' pnlAutoitBot.Location = New Point(51, 99)
                pnlAutoitBot.Hide()
                ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

                'CheckBsIfFalseThenStart()

            Else
#Disable Warning BC40000
                ev1.Resume()
#Enable Warning BC40000
#Disable Warning BC40000
                ev2.Resume()
#Enable Warning BC40000
            End If
        ElseIf cboxEnableAutoitBot.Checked = True Then 'If AutoIT checkbox is checked then start the autoit bot.
            FocusBluestacks()
            If AutoitBot.Count > 0 Then 'A) If theres a bot running before you start it then
                Process.GetProcessesByName("Bot")(0).Kill() ' B) Kill the process before starting a new one.
            End If
            PositionMouseBottomRight()
            Process.Start("bots\Bot.exe") 'C) START the AutoIT Bot
            tmrWatchDog.Start() ' Start WatchDog to monitor the countdown timer.
            tmrCount.Start() ' Start the count down timer.

            lblAutoitProcessCheck.Visible = True ' Show running processes. [BOT COUNT]
            tmrProcesses.Start() ' Start the timer to check for the processes

            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = Color.SeaGreen
            RichTextBox1.AppendText("AUTOIT-BOT: Botting Started!" + vbNewLine)

            '''''''''''''''''''''''''''''''''''''''''''
            indicator.Location = New Point(-4, 165)
            indicator.BackColor = Color.FromArgb(0, 112, 222)
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            ' pnlHome.Location = New Point(51, 99)
            pnlHome.Hide()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            'pnlSettings.Location = New Point(51, 99)
            pnlSettings.Hide()
            ' pnlSettings2.Location = New Point(51, 99)
            pnlSettings2.Hide()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            pnlBotLog.Location = New Point(51, 99)
            pnlBotLog.Show()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            ' pnlAutoitBot.Location = New Point(51, 99)
            pnlAutoitBot.Hide()
            ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

            ' CheckBsIfFalseThenStart()

        End If



    End Sub



    Public Sub Delay(ByVal DelayInSeconds As Integer)
        Dim ts As TimeSpan
        Dim targetTime As DateTime = DateTime.Now.AddSeconds(DelayInSeconds)
        Do
            ts = targetTime.Subtract(DateTime.Now)
            Application.DoEvents() ' keep app responsive
            System.Threading.Thread.Sleep(50) ' reduce CPU usage
        Loop While ts.TotalSeconds > 0
    End Sub

    Public Sub BotTimeSubs()
        txtMin.Text = My.Settings.Minutes.ToString
        txtSec.Text = My.Settings.Seconds.ToString
        My.Settings.Save()

    End Sub

    Public Sub CheckFilesFolders()


        If Not Directory.Exists("logs") Then
            Directory.CreateDirectory("logs")
            RichTextBox1.SelectionColor = Color.Beige ' Color for Next Line = date
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("First Run Complete! logs folder created!" + vbNewLine)
            If Not Directory.Exists("bots") Then
                Directory.CreateDirectory("bots")
                RichTextBox1.SelectionColor = Color.Beige ' Color for Next Line = date
                RichTextBox1.AppendText(GetDate + ": ")
                RichTextBox1.SelectionColor = ZxLogRegular
                RichTextBox1.AppendText("First Run Complete! bots folder created!" + vbNewLine)
            End If
        End If


    End Sub

    Public Sub UtilizeEverything()
        CheckFilesFolders() ' First run check. [FOLDER: logs + bots]
        RichTextBox1.ResetText()


        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlHome.Location = New Point(51, 99)
        pnlHome.Show()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlSettings.Location = New Point(51, 99)
        pnlSettings.Hide()
        pnlSettings2.Location = New Point(51, 99)
        pnlSettings2.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlBotLog.Location = New Point(51, 99)
        pnlBotLog.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlAutoitBot.Location = New Point(51, 99)
        pnlAutoitBot.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

        If My.Settings.varWhichBot = 1 Then '1=vbbot 0=autoitbot
            cboxEnableVbBot.Checked = True
        ElseIf My.Settings.varWhichBot = 0 Then
            cboxEnableAutoitBot.Checked = True
        End If

        If My.Settings.varAutoBot = 1 Then '1=Enable 0=Disable
            cboxEnableAutoBot.Checked = True
        ElseIf My.Settings.varAutoBot = 0 Then
            cboxDisableAutoBot.Checked = True

        End If

        If My.Settings.WinPos = 1 Then '1=Enable 0=Disable
            SetWinPos()
            cboxSetWinPos.Checked = True
        ElseIf My.Settings.varAutoBot = 0 Then
            cboxSetWinPos.Checked = False
            'Do nothing
        End If

        If My.Settings.AlwaysOnTop = 1 Then '1=Enable 0=Disable
            Me.TopMost = True
            cboxBotOnTop.Checked = True
        ElseIf My.Settings.varAutoBot = 0 Then
            Me.TopMost = False
            cboxBotOnTop.Checked = False
        End If


        ''''------------------------------------
        ''''-- #Load Min+Sec from My.Settings --
        RichTextBox1.Select(RichTextBox1.TextLength, 0)
        txtMin.Text = My.Settings.Minutes.ToString
        txtSec.Text = My.Settings.Seconds.ToString

        RichTextBox1.SelectionColor = Color.Beige
        RichTextBox1.AppendText(GetDate + ": ")
        RichTextBox1.SelectionColor = ZxOrange
        RichTextBox1.AppendText("****** MY SETTINGS ******" + vbNewLine)

        RichTextBox1.SelectionColor = Color.Beige
        RichTextBox1.AppendText(GetDate + ": ")
        RichTextBox1.SelectionColor = ZxLogRegular
        RichTextBox1.AppendText("SETTING: Minutes = " + txtMin.Text + vbNewLine)

        RichTextBox1.SelectionColor = Color.Beige
        RichTextBox1.AppendText(GetDate + ": ")
        RichTextBox1.SelectionColor = ZxLogRegular
        RichTextBox1.AppendText("SETTING: Seconds = " + txtSec.Text + vbNewLine)

        If cboxEnableAutoBot.Checked = True Then
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("SETTING: ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("Automatic Botting = ")
            RichTextBox1.SelectionColor = Color.SeaGreen
            RichTextBox1.AppendText("[ENABLED] " + vbNewLine)

        ElseIf cboxDisableAutoBot.Checked = True Then
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("SETTING: ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("Automatic Botting = ")
            RichTextBox1.SelectionColor = ZxPinkRed
            RichTextBox1.AppendText("[DISABLED] " + vbNewLine)

            ' Do nothing

        End If

        If cboxEnableVbBot.Checked = True Then
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("SETTING: ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("VB.NET BOT =  ")
            RichTextBox1.SelectionColor = Color.SeaGreen
            RichTextBox1.AppendText("[ENABLED]" + vbNewLine)
        ElseIf cboxEnableAutoitBot.Checked = True Then
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("SETTING: ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("AUTOIT BOT =  ")
            RichTextBox1.SelectionColor = Color.SeaGreen
            RichTextBox1.AppendText("[ENABLED]" + vbNewLine)
        End If
        If cboxEnableAutoitBot.Checked = False Then
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("SETTING: ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("AUTOIT BOT =  ")
            RichTextBox1.SelectionColor = ZxPinkRed
            RichTextBox1.AppendText("[DISABLED]" + vbNewLine)
        End If
        If cboxEnableVbBot.Checked = False Then
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("SETTING: ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("VB.NET BOT =  ")
            RichTextBox1.SelectionColor = ZxPinkRed
            RichTextBox1.AppendText("[DISABLED]" + vbNewLine)
        End If
        If cboxSetWinPos.Checked = True Then
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("SETTING: ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("WINPOS TOPRIGHT =  ")
            RichTextBox1.SelectionColor = Color.SeaGreen
            RichTextBox1.AppendText("[ENABLED]" + vbNewLine)
        Else
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("SETTING: ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("WINPOS TOPRIGHT =  ")
            RichTextBox1.SelectionColor = ZxPinkRed
            RichTextBox1.AppendText("[DISABLED]" + vbNewLine)
        End If
        If cboxBotOnTop.Checked = True Then
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("SETTING: ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("ALWAYS ON TOP =  ")
            RichTextBox1.SelectionColor = Color.SeaGreen
            RichTextBox1.AppendText("[ENABLED]" + vbNewLine)
        Else
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("SETTING: ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("ALWAYS ON TOP =  ")
            RichTextBox1.SelectionColor = ZxPinkRed
            RichTextBox1.AppendText("[DISABLED]" + vbNewLine)
        End If


        RichTextBox1.SelectionColor = Color.Beige
        RichTextBox1.AppendText(GetDate + ": ")
        RichTextBox1.SelectionColor = Color.FromArgb(255, 128, 0) ' ORANGE
        RichTextBox1.AppendText("****** END OF MY SETTINGS ******" + vbNewLine)



        RichTextBox1.SelectionColor = Color.Beige
        RichTextBox1.AppendText(GetDate + ": ")
        RichTextBox1.SelectionColor = Color.Beige
        RichTextBox1.AppendText("Settings Utilized Everything!" + vbNewLine)

        If cboxEnableAutoBot.Checked = True Then
            StartBotSub() 'START AUTOMATIC BOTTING CODE HERE
            indicator.Location = New Point(-4, 165)
        ElseIf cboxDisableAutoBot.Checked = True Then
            ' Do nothing
        End If

        ''''--        #END of Min+Sec         --
        ''''------------------------------------
        RichTextBox1.ScrollToCaret()
        'RichTextBox1.SelectionStart = 0

        lblMin.Text = txtMin.Text
        lblSec.Text = txtSec.Text




    End Sub

#End Region

#Region "Section - Buttons"

    Private Sub btnAutoitBot_Click(sender As Object, e As EventArgs) Handles btnAutoitBot.Click

        indicator.Location = New Point(-4, 455)
        indicator.BackColor = Color.FromArgb(0, 112, 222)

        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlHome.Location = New Point(51, 99)
        pnlHome.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlSettings.Location = New Point(51, 99)
        pnlSettings.Hide()
        pnlSettings2.Location = New Point(51, 99)
        pnlSettings2.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlBotLog.Location = New Point(51, 99)
        pnlBotLog.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlAutoitBot.Location = New Point(51, 99)
        pnlAutoitBot.Show()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

    End Sub

    Private Sub btnKillAll_Click(sender As Object, e As EventArgs) Handles btnKillAll.Click
        KillBsMsgBox()
    End Sub

    Private Sub btnWinPosition_Click(sender As Object, e As EventArgs) Handles btnWinPosition.Click
        SetWinPos()
    End Sub


    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Environment.Exit(0)
    End Sub
    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        RichTextBox1.Select(RichTextBox1.TextLength, 0)
        RichTextBox1.SelectionColor = Color.Beige
        RichTextBox1.AppendText(GetDate + ": ")
        RichTextBox1.SelectionColor = Color.White
        RichTextBox1.AppendText(TextBox1.Text + vbNewLine)
        RichTextBox1.SelectionColor = Color.Beige
        RichTextBox1.Update()


    End Sub
    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        indicator.BackColor = Color.FromArgb(0, 112, 222)
        indicator.Location = New Point(-4, 65)


        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlHome.Location = New Point(51, 99)
        pnlHome.Show()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlSettings.Location = New Point(51, 99)
        pnlSettings.Hide()
        pnlSettings2.Location = New Point(51, 99)
        pnlSettings2.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlBotLog.Location = New Point(51, 99)
        pnlBotLog.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlAutoitBot.Location = New Point(51, 99)
        pnlAutoitBot.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;


    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        indicator.Location = New Point(-4, 116)
        indicator.BackColor = Color.FromArgb(0, 112, 222)

        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlHome.Location = New Point(51, 99)
        pnlHome.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlSettings.Location = New Point(51, 99)
        pnlSettings.Show()
        pnlSettings2.Location = New Point(51, 99)
        pnlSettings2.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlBotLog.Location = New Point(51, 99)
        pnlBotLog.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlAutoitBot.Location = New Point(51, 99)
        pnlAutoitBot.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

    End Sub

    Private Sub btnLogs_Click(sender As Object, e As EventArgs) Handles btnLogs.Click
        indicator.Location = New Point(-4, 165)
        indicator.BackColor = Color.FromArgb(0, 112, 222)
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlHome.Location = New Point(51, 99)
        pnlHome.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlSettings.Location = New Point(51, 99)
        pnlSettings.Hide()
        pnlSettings2.Location = New Point(51, 99)
        pnlSettings2.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlBotLog.Location = New Point(51, 99)
        pnlBotLog.Show()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlAutoitBot.Location = New Point(51, 99)
        pnlAutoitBot.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;



    End Sub

    Private Sub btnSaveAll_Click(sender As Object, e As EventArgs) Handles btnSaveAll.Click
        indicatorSave.Visible = True
        tmrSave.Enabled = True ' When enabled = false it will side the green saving indicator
        '''''''''''''''''''''

        SaveCountDownWatch() 'Save Settings for Minutes + Seconds

    End Sub


    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub btnMenu_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub BunifuCustomLabel28_Click(sender As Object, e As EventArgs) Handles BunifuCustomLabel28.Click
        indicator.BackColor = Color.FromArgb(0, 112, 222)
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlHome.Location = New Point(51, 99)
        pnlHome.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlSettings.Location = New Point(51, 99)
        pnlSettings.Show()
        pnlSettings2.Location = New Point(51, 99)
        pnlSettings2.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlBotLog.Location = New Point(51, 99)
        pnlBotLog.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlAutoitBot.Location = New Point(51, 99)
        pnlAutoitBot.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
    End Sub



    Private Sub BunifuCustomLabel27_Click(sender As Object, e As EventArgs) Handles BunifuCustomLabel27.Click
        indicator.BackColor = Color.FromArgb(0, 255, 255)

        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlHome.Location = New Point(51, 99)
        pnlHome.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlSettings.Location = New Point(51, 99)
        pnlSettings.Hide()
        pnlSettings2.Location = New Point(51, 99)
        pnlSettings2.Show()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlBotLog.Location = New Point(51, 99)
        pnlBotLog.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        pnlAutoitBot.Location = New Point(51, 99)
        pnlAutoitBot.Hide()
        ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
    End Sub



    Private Sub btnStartBot_Click(sender As Object, e As EventArgs) Handles btnStartBot.Click
        StartBotSub()
    End Sub

    Private Sub btnStopBot_Click(sender As Object, e As EventArgs) Handles btnStopBot.Click
        StopBotSub()
    End Sub

#End Region

#Region "Section - Timers"


    Dim hotkey0 As Boolean
    Private Sub tmrHotkey_Tick(sender As Object, e As EventArgs) Handles tmrHotkey.Tick
        If (GetAsyncKeyState(Keys.NumPad0) And 1) > 0 Then
            hotkey0 = Not hotkey0
            If hotkey0 Then
                StopBotSub()
                tmrReset.Stop()


            Else
                StopBotSub()
                tmrReset.Stop()


            End If
        End If


    End Sub

    Private Sub tmrVbBot_Tick(sender As Object, e As EventArgs) Handles tmrVbBot.Tick

        lblBotStatus.Text = "Searching for Ads X buttons [Large List]"
        LoopAllResourceImages("wintest.png", ListBox1, BlockList, ImgSrchProgress)
        LoopAllResourceImages("wintest.png", ListBox1, BlockList, ImgSrchProgress)
        LoopAllResourceImages("wintest.png", ListBox1, BlockList, ImgSrchProgress)
        LoopAllResourceImages("wintest.png", ListBox1, BlockList, ImgSrchProgress) '
    End Sub




    Private Sub tmrSave_Tick(sender As Object, e As EventArgs) Handles tmrSave.Tick
        tmrSave.Enabled = False
        indicatorSave.Visible = False 'hiding the indicator for saving, :;;; just a neat little FX


    End Sub

    Private Sub tmrCount_Tick(sender As Object, e As EventArgs) Handles tmrCount.Tick
        If min = 0 And sec = 0 Then
            tmrCount.Stop()
            Exit Sub
        End If
        If sec > 0 Then
            sec = sec - 1
        ElseIf sec <= 0 Then
            min = min - 1
            sec = 59
        End If
        lblMin.Text = min.ToString
        If sec < 10 Then
            lblSec.Text = "0" & sec.ToString
        Else
            lblSec.Text = sec.ToString
        End If
        lblSec.Text = sec.ToString
    End Sub



#End Region

#Region "Section - CheckBoxes and switches"

    Private Sub cboxSetWinPos_OnChange(sender As Object, e As EventArgs) Handles cboxSetWinPos.OnChange
        If cboxSetWinPos.Checked = True Then
            My.Settings.WinPos = 1
            My.Settings.Save()
        Else
            My.Settings.WinPos = 0
            My.Settings.Save()
        End If
    End Sub

    Private Sub cboxBotOnTop_OnChange(sender As Object, e As EventArgs) Handles cboxBotOnTop.OnChange
        If cboxBotOnTop.Checked = True Then
            My.Settings.AlwaysOnTop = 1
            My.Settings.Save()
        Else
            My.Settings.AlwaysOnTop = 0
            My.Settings.Save()
        End If
    End Sub
    Private Sub cboxEnableVbBot_OnChange(sender As Object, e As EventArgs) Handles cboxEnableVbBot.OnChange
        If cboxEnableVbBot.Checked = True Then
            cboxEnableAutoitBot.Checked = False
            My.Settings.varWhichBot = 1 '1=vbbot 0=autoitbot
            My.Settings.Save()
        ElseIf cboxEnableAutoitBot.Checked = True Then
            cboxEnableVbBot.Checked = False
            My.Settings.varWhichBot = 0 '1=vbbot 0=autoitbot
            My.Settings.Save()
        End If

        If cboxEnableAutoitBot.Checked = False Then
            cboxEnableVbBot.Checked = True
        End If
    End Sub

    Private Sub cboxEnableAutoitBot_OnChange(sender As Object, e As EventArgs) Handles cboxEnableAutoitBot.OnChange
        If cboxEnableAutoitBot.Checked = True Then
            cboxEnableVbBot.Checked = False
            My.Settings.varWhichBot = 0 '1=vbbot 0=autoitbot
            My.Settings.Save()
        ElseIf cboxEnableVbBot.Checked = True Then
            cboxEnableAutoitBot.Checked = False
            My.Settings.varWhichBot = 1 '1=vbbot 0=autoitbot
            My.Settings.Save()
        End If

        If cboxEnableVbBot.Checked = False Then
            cboxEnableAutoitBot.Checked = True
        End If
    End Sub


    Private Sub cbxEnableAutomaticStart_OnChange(sender As Object, e As EventArgs) Handles cboxEnableAutoBot.OnChange

        If cboxEnableAutoBot.Checked = True Then
            cboxDisableAutoBot.Checked = False
            My.Settings.varAutoBot = 1 '0=Disable 1=Enable
            My.Settings.Save()
        ElseIf cboxDisableAutoBot.Checked = True Then
            cboxEnableAutoBot.Checked = False
            My.Settings.varAutoBot = 0 '0=Disable 1=Enable
            My.Settings.Save()
        End If

        If cboxEnableAutoBot.Checked = False Then
            cboxDisableAutoBot.Checked = True
        End If
    End Sub
    Private Sub cboxDisableAutomaticBotting_OnChange(sender As Object, e As EventArgs) Handles cboxDisableAutoBot.OnChange

        If cboxDisableAutoBot.Checked = True Then
            cboxEnableAutoBot.Checked = False
            My.Settings.varAutoBot = 0 '0=Disable 1=Enable
            My.Settings.Save()
        ElseIf cboxEnableAutoBot.Checked = True Then
            cboxDisableAutoBot.Checked = False
            My.Settings.varAutoBot = 1 '0=Disable 1=Enable
            My.Settings.Save()
        End If

        If cboxDisableAutoBot.Checked = False Then
            cboxEnableAutoBot.Checked = True
        End If

    End Sub

#End Region

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tmrHotkey.Start()
        indicator.Location = New Point(-4, 65) 'Indicator start at home
        lblForm1Title.Font = New Font("LUZRO", 11, FontStyle.Regular) 'Use a TTS Font LUZRO for the title of the application
        Me.Size = New System.Drawing.Size(582, 546) ' Set the size of the application on startup
        ' txtMin.Text = My.Settings.Minutes
        'txtSec.Text = My.Settings.Seconds

        BotTimeSubs() ' This loads the Min+Sec from My.Settings to the textboxes on the program.
        UtilizeEverything()


        If cboxEnableAutoBot.Checked = True Then
            StopBotSub() ' STOP bot on Form_Load if it is automatically on
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLogRegular
            RichTextBox1.AppendText("Form_Load: Automatic Botting was stopped" + vbNewLine)

        End If




    End Sub





    Dim SmallList = 1
    Dim LargeList = 1


    Private Sub event1()
        While SmallList = 1
            Debug.WriteLine("EV1 " & DateTime.Now.ToString)
            LoopAllResourceImages("wintest.png", ListBox1, BlockList, ImgSrchProgress) ' THIS IS WHERE IT SEARCHES FOR IMG BUT I GET AN ERROR
            Threading.Thread.Sleep(1000) 'simulate work
        End While
    End Sub

    Private Sub event2()
        While LargeList = 1
            Debug.WriteLine("EV2 " & DateTime.Now.ToString)
            LoopAllResourceImages("wintest2.png", ListBox1, BlockList2, ImgSrchProgress2)

            Threading.Thread.Sleep(1500) 'simulate work
        End While
    End Sub

    Public Sub ResetCount()
        min = My.Settings.Minutes
        sec = My.Settings.Seconds
        txtMin.Text = min
        txtSec.Text = sec
    End Sub


    ' ...


    Private Sub tmrWatchDog_Tick(sender As Object, e As EventArgs) Handles tmrWatchDog.Tick

        '''''VBBOT
        'If the cboxEnableVbBot is checked and when the minute timer hits 0
        'Then whats going to happen is it will Kill all of the bluestacks processes
        'Reset the timer count, Restart Bluestacks, And stop the WatchDog timer.

        '''''AUTOIT-BOT
        'If the cboxEnableAutoitBot is checked and when the minute timer hits 0
        'Then whats going to happen is it will Kill all of the bluestacks processes
        'Reset the timer count, Restart Bluestacks, And stop the WatchDog timer.


        If cboxEnableVbBot.Checked = True Then
            If lblMin.Text = "0" Then
                KillBs()
                tmrCount.Stop()
                ResetCount()
                RestartBluestacks()
                tmrReset.Start()
                tmrWatchDog.Stop()


            Else
                'lblDebugger.Text = ("Status: Bot is currently active!") 'TELLS ME WHEN THE TIMER IS ACTIVE
            End If
        ElseIf cboxEnableAutoitBot.Checked = True Then
            If lblMin.Text = "0" Then
                KillBs()
                tmrCount.Stop()
                ResetCount()
                RestartBluestacks()
                tmrReset.Start() '
                tmrWatchDog.Stop()

                '''''''''''''''''''''''''''''''''''''''''''
                indicator.BackColor = Color.FromArgb(0, 112, 222)
                indicator.Location = New Point(-4, 65)
                ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
                ' pnlHome.Location = New Point(51, 99)
                pnlHome.Hide()
                ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
                'pnlSettings.Location = New Point(51, 99)
                pnlSettings.Hide()
                ' pnlSettings2.Location = New Point(51, 99)
                pnlSettings2.Hide()
                ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
                pnlBotLog.Location = New Point(51, 99)
                pnlBotLog.Show()
                ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
                ' pnlAutoitBot.Location = New Point(51, 99)
                pnlAutoitBot.Hide()
                ';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            Else
                'lblDebugger.Text = ("Status: Bot is currently active!") 'TELLS ME WHEN THE TIMER IS ACTIVE
            End If
        End If






    End Sub

    Private Sub tmrReset_Tick(sender As Object, e As EventArgs) Handles tmrReset.Tick
        '''''''tmrReset
        'A) Timer reset main function is to basically start the imagesearch for the game
        '   then open the game once the miniclip logo appears it will stop the timer to
        '   start botting again. 


        ''''''NOTES: -- BOTH FUNCTIONS ARE THE SAME *tmrReset*
        '''''' There is no need to do anything with this weather both vbbot or autoit bot 
        '''''' checkboxes are enabled. They both need to restart bluestacks the same way.






        If cboxEnableVbBot.Checked = True Then ' cboxEnableVbBot will then
            ImgSrchProgress.Value = 0
            ImgSrchProgress2.Value = 0
            'StopBotSub()
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLightBlue
            RichTextBox1.AppendText("VB-BOT: Reseting in progress please wait..." + vbNewLine)
            LoopAllResourceImages("wintest3.png", ListBox1, BlockList3, ImgSrchProgress3)
            Dim MiniclipBmp As Bitmap = My.Resources.miniclip
            If FindOneImage("wintest3.png", ListBox1, ImgSrchProgress3, MiniclipBmp, "miniclip") = True Then
                min = My.Settings.Minutes
                sec = My.Settings.Seconds
                tmrReset.Stop()

                ' StartBotSub()
                'tmrCount.Start()
                ' tmrWatchDog.Start()

                RichTextBox1.SelectionColor = Color.Beige
                RichTextBox1.AppendText(GetDate + ": ")
                RichTextBox1.SelectionColor = ZxLightBlue
                RichTextBox1.AppendText("VB-BOT: BlueStacks Reset" + vbNewLine)
                Thread.Sleep(500)
                RichTextBox1.ResetText()

                UtilizeEverything()

            End If
            Thread.Sleep(1000)

        ElseIf cboxEnableAutoitBot.Checked = True Then
            ImgSrchProgress.Value = 0
            ImgSrchProgress2.Value = 0
            'StopBotSub()
            RichTextBox1.SelectionColor = Color.Beige
            RichTextBox1.AppendText(GetDate + ": ")
            RichTextBox1.SelectionColor = ZxLightBlue
            RichTextBox1.AppendText("AUTOIT-BOT: Reseting in progress please wait..." + vbNewLine)
            LoopAllResourceImages("wintest3.png", ListBox1, BlockList3, ImgSrchProgress3)
            Dim MiniclipBmp As Bitmap = My.Resources.miniclip
            If FindOneImage("wintest3.png", ListBox1, ImgSrchProgress3, MiniclipBmp, "miniclip") = True Then
                min = My.Settings.Minutes
                sec = My.Settings.Seconds
                tmrReset.Stop()

                ' StartBotSub()
                'tmrCount.Start()
                ' tmrWatchDog.Start()

                RichTextBox1.SelectionColor = Color.Beige
                RichTextBox1.AppendText(GetDate + ": ")
                RichTextBox1.SelectionColor = ZxLightBlue
                RichTextBox1.AppendText("AUTOIT-BOT: BlueStacks Reset" + vbNewLine)
                Thread.Sleep(500)
                RichTextBox1.ResetText()

                UtilizeEverything()
            End If
            Thread.Sleep(1000)

        End If



    End Sub

    Private Sub btnRestartBot_Click(sender As Object, e As EventArgs) Handles btnRestartBot.Click
        tmrReset.Start()
    End Sub

    Private Sub btnStartBlutStacks_Click(sender As Object, e As EventArgs) Handles btnStartBlutStacks.Click
        KillBs()
        RestartBluestacks()
    End Sub

    Private Sub tmrProcesses_Tick(sender As Object, e As EventArgs) Handles tmrProcesses.Tick
        Dim proc As Integer = Process.GetProcessesByName("Bot").GetUpperBound(0) + 1
        If Process.GetProcessesByName("Bot").Count > 0 Then
            lblAutoitProcessCheck.Text = ("ACTIVE AUTOIT BOTS:" & " " & proc)
        Else
            lblAutoitProcessCheck.Text = ("ACTIVE AUTOIT BOTS:" & " " & proc)
        End If
    End Sub

    Public Declare Function GetPixel Lib "gdi32" Alias "GetPixel" (ByVal hdc As Integer, ByVal x As Int32, ByVal y As Int32) As Int32
    Private Declare Function GetForegroundWindow Lib "user32" () As Integer
    Private Declare Function GetWindowDC Lib "user32" (ByVal hwnd As Integer) As Integer
    Public Declare Function ReleaseDC Lib "user32" (ByVal hWnd As Integer, ByVal hdc As Integer) As Integer





End Class
