
Imports System.Drawing.Imaging
Imports System.ComponentModel
Imports System.Collections
Imports System.Threading
'Imports AutoItX3Lib
Imports System.Text
Imports System.Net
Imports System.IO
Imports System
Imports System.Runtime.InteropServices

Public Class MouseFunctions
#Region "Section - DLL Imports &&&& Mouse Events (CLICK)"
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

#Region "Section - Declarations"

    Public Declare Function GetKeyPress Lib "user32" Alias "GetAsyncKeyState" (ByVal key As Integer) As Integer
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    'Private Declare Sub mouse_event Lib "user32" (ByVal dwflags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cbuttons As Integer, ByVal dwextrainfo As Integer)
    Private Const mouseclickup = 4
    Private Const mouseclickdown = 2

    'Declare Function apimouse_event Lib "user32.dll" Alias "mouse_event" (ByVal dwFlags As Int32, ByVal dX As Int32, ByVal dY As Int32, ByVal cButtons As Int32, ByVal dwExtraInfo As Int32) As Boolean
    'Public Const MOUSEEVENTF_LEFTDOWN = &H2
    ' Public Const MOUSEEVENTF_LEFTUP = &H4
    ' Private Const MOUSEEVENTF_RIGHTDOWN = &H8
    'Private Const MOUSEEVENTF_RIGHTUP = &H10

    Public progress As Integer = 0
    Public countdown As Integer = 0

    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Long) As Integer
    Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    Public Const MOUSEEVENTF_ABSOLUTE = &H8000 ' absolute move
    Public Const MOUSEEVENTF_LEFTDOWN = &H2 ' left button down
    Public Const MOUSEEVENTF_LEFTUP = &H4 ' left button up
    Public Const MOUSEEVENTF_MOVE = &H1 ' mouse move
    Public Const MOUSEEVENTF_MIDDLEDOWN = &H20
    Public Const MOUSEEVENTF_MIDDLEUP = &H40
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8
    Public Const MOUSEEVENTF_RIGHTUP = &H10
    Declare Function SetCursorPos& Lib "user32" (ByVal p As Point)



#End Region

    Public Shared Function GetKeyPress(ByVal hotkey0 As Boolean, ByVal ActiveTimer As Object, ByVal lstBox As ListBox)
        If (GetAsyncKeyState(Keys.NumPad0) And 1) > 0 Then
            hotkey0 = Not hotkey0
            If hotkey0 Then
                ActiveTimer.Stop()

                lstBox.Items.Add("HOTKEY: NUMPAD0")

            Else
                ActiveTimer.Stop()
                lstBox.Items.Add("HOTKEY: NUMPAD0")

            End If
        End If

#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths

End Class
