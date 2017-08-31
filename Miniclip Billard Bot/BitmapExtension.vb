'*******************************************************************************
'*
'*      Epistemex
'*
'*      Bitmap extension: .Contains(bmp)
'*      KF
'*
'*      2012-09-26      Initial version
'*      2012-09-26      Minor optimization, exit for's impl.
'*
'*******************************************************************************

Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Module BitmapExtension

    <Extension()>
    Public Function ImageContains(src As Bitmap, ByRef bmp As Bitmap) As Point
        '
        '-- Some logic pre-checks
        '
        If src Is Nothing OrElse bmp Is Nothing Then Return Nothing

        If src.Width = bmp.Width AndAlso src.Height = bmp.Height Then
            If src.GetPixel(0, 0) = bmp.GetPixel(0, 0) Then
                Return New Point(0, 0)
            Else
                Return Nothing
            End If

        ElseIf src.Width < bmp.Width OrElse src.Height < bmp.Height Then
            Return Nothing

        End If
        '
        '-- Prepare optimizations
        '
        Dim sr As New Rectangle(0, 0, src.Width, src.Height)
        Dim br As New Rectangle(0, 0, bmp.Width, bmp.Height)

        Dim srcLock As BitmapData = src.LockBits(sr, Imaging.ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb)
        Dim bmpLock As BitmapData = bmp.LockBits(br, Imaging.ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb)

        Dim sStride As Integer = srcLock.Stride
        Dim bStride As Integer = bmpLock.Stride

        Dim srcSz As Integer = sStride * src.Height
        Dim bmpSz As Integer = bStride * bmp.Height

        Dim srcBuff(srcSz) As Byte
        Dim bmpBuff(bmpSz) As Byte

        Marshal.Copy(srcLock.Scan0, srcBuff, 0, srcSz)
        Marshal.Copy(bmpLock.Scan0, bmpBuff, 0, bmpSz)

        ' we don't need to lock the image anymore as we have a local copy
        bmp.UnlockBits(bmpLock)
        src.UnlockBits(srcLock)

        Dim x, y, x2, y2, sx, sy, bx, by, sw, sh, bw, bh As Integer
        Dim r, g, b As Byte

        Dim p As Point = Nothing

        bw = bmp.Width
        bh = bmp.Height

        sw = src.Width - bw      ' limit scan to only what we need. the extra corner
        sh = src.Height - bh     ' point we need is taken care of in the loop itself.

        bx = 0 : by = 0
        '
        '-- Scan source for bitmap
        '
        For y = 0 To sh
            sy = y * sStride
            For x = 0 To sw

                sx = sy + x * 3
                '
                '-- Find start point/pixel
                '
                r = srcBuff(sx + 2)
                g = srcBuff(sx + 1)
                b = srcBuff(sx)

                If r = bmpBuff(2) AndAlso g = bmpBuff(1) AndAlso b = bmpBuff(0) Then
                    p = New Point(x, y)
                    '
                    '-- We have a pixel match, check the region
                    '
                    For y2 = 0 To bh - 1
                        by = y2 * bStride
                        For x2 = 0 To bw - 1
                            bx = by + x2 * 3

                            sy = (y + y2) * sStride
                            sx = sy + (x + x2) * 3

                            r = srcBuff(sx + 2)
                            g = srcBuff(sx + 1)
                            b = srcBuff(sx)

                            If Not (r = bmpBuff(bx + 2) AndAlso
                                    g = bmpBuff(bx + 1) AndAlso
                                    b = bmpBuff(bx)) Then
                                '
                                '-- Not matching, continue checking
                                '
                                p = Nothing
                                sy = y * sStride
                                Exit For
                            End If

                        Next
                        If p = Nothing Then Exit For
                    Next
                End If 'end of region check

                If p <> Nothing Then Exit For
            Next
            If p <> Nothing Then Exit For
        Next

        bmpBuff = Nothing
        srcBuff = Nothing

        Return p

    End Function


    Public Function PositionWindow(ByVal ChosenForm As Form, ByVal w2 As Integer, ByVal h2 As Integer, ByVal Position As String)
        If Position Is "TopLeft" Then
            Dim x As Integer
            Dim y As Integer
            x = Screen.PrimaryScreen.WorkingArea.Width - w2
            y = Screen.PrimaryScreen.WorkingArea.Height - h2
            ChosenForm.Location = New Point(x, y)
        End If
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths
    Public Function SearchDesktop(ByVal DesktopImg As String, ByVal SearchImage As Bitmap, ByVal lst As ListBox, ByVal str As String)




        'Dim bmpSrc As Bitmap = Image.FromFile(DesktopImg)
        'Dim p As Point = BitmapExtension.ImageContains(bmpSrc, SearchImage)
        'If p <> Nothing Then
        '    System.Windows.Forms.Cursor.Position = New System.Drawing.Point(p.X, p.Y)
        '    MouseFunctions.mouse_event(MouseFunctions.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
        '    MouseFunctions.mouse_event(MouseFunctions.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
        '    'lst.Items.Add(str)
        'Else
        'End If
        'bmpSrc.Dispose()




        Dim bmpSrc As Bitmap = Image.FromFile(DesktopImg)
        Dim p As Point = BitmapExtension.ImageContains(bmpSrc, SearchImage)
        If p <> Nothing Then
            System.Windows.Forms.Cursor.Position = New System.Drawing.Point(p.X, p.Y)
            MouseFunctions.mouse_event(MouseFunctions.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
            MouseFunctions.mouse_event(MouseFunctions.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
            'lst.Items.Add(str)
            Return True
        Else
        End If
        bmpSrc.Dispose()




#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function 'This function hrere is fucked -<---
#Enable Warning BC42105 ' Function doesn't return a value on all code paths






    Public Function GetMyResourcesDictionary() As Dictionary(Of String, Object)
        Dim ItemDictionary As New Dictionary(Of String, Object)
        Dim ItemEnumerator As System.Collections.IDictionaryEnumerator
        Dim ItemResourceSet As Resources.ResourceSet
        Dim ResourceNameList As New List(Of String)
        ItemResourceSet = My.Resources.ResourceManager.GetResourceSet(New System.Globalization.CultureInfo("en"), True, True)
        ItemEnumerator = ItemResourceSet.GetEnumerator
        Do While ItemEnumerator.MoveNext
            ResourceNameList.Add(ItemEnumerator.Key.ToString)
        Loop
        For Each resourceName As String In ResourceNameList
            ItemDictionary.Add(resourceName, GetItem(resourceName))
        Next
        ResourceNameList = Nothing
        Return ItemDictionary
    End Function




    Public Function GetItem(ByVal resourceName As String) As Object
        Return My.Resources.ResourceManager.GetObject(resourceName)
    End Function






    Dim counter = 0
    Public Function LoopAllResourceImages(ByVal DesktopImg As String, ByVal LstBox As ListBox, ByVal BlackList As String(), ByVal ImageProgress As BunifuCircleProgressbar)
        Dim counter1 = 0
        ImageProgress.Invoke(Sub() ImageProgress.Value = 0)
        BitmapExtension.SaveDesktopToImage(DesktopImg)
        Dim myDictionary As Dictionary(Of String, Object) = GetMyResourcesDictionary()
        For Each kvp As KeyValuePair(Of String, Object) In myDictionary
            Dim name As String = kvp.Key
            If Not BlackList.Contains(name) Then
                If TypeOf kvp.Value Is Bitmap Then
                    BitmapExtension.SearchDesktop(DesktopImg, kvp.Value, LstBox, name + ": Found!")
                    'AND IF FOUND HERE STOP AND START TIMER
                End If
                If TypeOf kvp.Value Is Image Then
                    'do whatever
                End If
            Else
                'LstBox.AppendText(name + " :Blacklisted!")
                'LstBox.Items.Add(name + " :Blacklisted!")
            End If
            counter1 += 1
            ImageProgress.Invoke(Sub() ImageProgress.Value = counter1)
            ImageProgress.Invoke(Sub() ImageProgress.Update())
            ImageProgress.Invoke(Sub() ImageProgress.Refresh())

        Next
        'progress bar works finekk

#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths

    Public Function LoopAllResourceImages2(ByVal DesktopImg As String, ByVal LstBox As ListBox, ByVal BlackList As String(), ByVal ImageProgress As BunifuCircleProgressbar)

        Dim counter2 = 0
        ImageProgress.Value = 0
        BitmapExtension.SaveDesktopToImage(DesktopImg)
        Dim myDictionary As Dictionary(Of String, Object) = GetMyResourcesDictionary()
        For Each kvp As KeyValuePair(Of String, Object) In myDictionary
            Dim name As String = kvp.Key
            If Not BlackList.Contains(name) Then
                If TypeOf kvp.Value Is Bitmap Then
                    BitmapExtension.SearchDesktop(DesktopImg, kvp.Value, LstBox, name + ": Found!")
                    'AND IF FOUND HERE STOP AND START TIMER
                End If
                If TypeOf kvp.Value Is Image Then
                    'do whatever
                End If
            Else
                'LstBox.AppendText(name + " :Blacklisted!")
                ' LstBox.Items.Add(name + " :Blacklisted!")
            End If
            counter2 += 1
            ImageProgress.Value = counter2
            ImageProgress.Update()
            ImageProgress.Refresh()
        Next
        'progress bar works finekk

#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths


    Public Function FindOneImage(ByVal DesktopImg As String, ByVal LstBox As ListBox, ByVal ImageProgress As BunifuCircleProgressbar, ByVal FindImage As Bitmap, ByVal name As String) As Boolean

        If BitmapExtension.SearchDesktop(DesktopImg, FindImage, LstBox, name + ": Found!") = True Then
            Return True
        End If
#Disable Warning BC42353 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42353 ' Function doesn't return a value on all code paths



    Public Function GetResourceImageCount()
        Dim myDictionary As Dictionary(Of String, Object) = GetMyResourcesDictionary()
        For Each kvp As KeyValuePair(Of String, Object) In myDictionary
            counter += 1
        Next
        Return counter
    End Function

    '    Dim Rect As Rectangle
    'Rect.Width = Screen.PrimaryScreen.Bounds.Width
    'Rect.Height = Screen.PrimaryScreen.Bounds.Height
    'Rect.X = 0
    'Rect.Y = 0
    'CaptureScreen.Snapshot("My Documents\Test.bmp", Rect)
    Public Sub SaveDesktopToImage(filename As String)
        ' On Error Resume Next
        'Dim bounds As Rectangle
        'Dim screenshot As System.Drawing.Bitmap
        'Dim graph As Graphics
        'bounds = Screen.PrimaryScreen.Bounds
        'screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb)
        'graph = Graphics.FromImage(screenshot)
        'graph.CopyFromScreen(0, 0, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)

        'screenshot.Save(My.Application.Info.DirectoryPath + "\" + filename, Imaging.ImageFormat.Png)
        'screenshot.Dispose()

        'graph.Dispose()

        On Error Resume Next
        Dim bounds As Rectangle
        Dim screenshot As System.Drawing.Bitmap
        Dim graph As Graphics
        bounds = Screen.PrimaryScreen.Bounds
        screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb)
        graph = Graphics.FromImage(screenshot)
        graph.CopyFromScreen(0, 0, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)

        screenshot.Save(My.Application.Info.DirectoryPath + "\" + filename, Imaging.ImageFormat.Png)
        screenshot.Dispose()

        graph.Dispose()
    End Sub
End Module