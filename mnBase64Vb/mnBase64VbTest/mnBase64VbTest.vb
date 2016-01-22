' Copyright © exc-jdbi 2016
'
' © mnBase64 2016
'
' mnBase64 - www.github.com/exc-jdbi/base64
'
' mnBase64 is Free and Opensource Software

Option Strict On
Option Explicit On

Imports TestDll
Imports System.Text

Module mnBase64VbTest
    Sub Main()
        Dim cText As String = "A Ansistring with special characters ¾½¼™Þ‰Æ€#èüä$!$£{°§1*-+±'?Øø}[»²³$£/&%ç¬›|‹¢@©®÷«]. "
        Dim uText As String = "A Widestring with special characters 日本人, ไทย, Український, עברית, العربية, भारतीय,فارسی, русский, සිංහල, தமிழ். "

        Dim cSb As New StringBuilder
        Dim uSb As New StringBuilder
        For i As Integer = 0 To 100000 - 1
            cSb.Append(cText)
            uSb.Append(uText)
        Next

        Dim b64 As New DllBase64

        'Encode to Base64
        Dim sB64EncCppA As String = b64.EncodeB64(cSb.ToString)
        Dim sB64EncCppW As String = b64.EncodeB64(uSb.ToString, True)
        Dim sB64EncVB1 As String = Convert.ToBase64String(Encoding.Default.GetBytes(cSb.ToString))
        Dim sB64EncVB2 As String = Convert.ToBase64String(Encoding.UTF8.GetBytes(uSb.ToString))

        'Decode Base64
        Dim sB64DecCppA As String = b64.DecodeB64(sB64EncVB1)
        Dim sB64DecCppW As String = b64.DecodeB64(sB64EncVB2, True)
        Dim sB64DecVB1 As String = Encoding.Default.GetString(Convert.FromBase64String(sB64EncCppA))
        Dim sB64DecVB2 As String = Encoding.UTF8.GetString(Convert.FromBase64String(sB64EncCppW))
        b64.Close()

        b64.Open()
        Dim sB64EncCppA2 As String = b64.EncodeB64(sB64DecVB1)
        Dim sB64EncCppW2 As String = b64.EncodeB64(sB64DecVB2, True)
        Dim sB64EncVB3 As String = Convert.ToBase64String(Encoding.Default.GetBytes(sB64DecCppA))
        Dim sB64EncVB4 As String = Convert.ToBase64String(Encoding.UTF8.GetBytes(sB64DecCppW))

        Dim sB64DecCppA2 As String = b64.DecodeB64(sB64EncVB3)
        Dim sB64DecCppW2 As String = b64.DecodeB64(sB64EncVB4, True)
        Dim sB64DecVB3 As String = Encoding.Default.GetString(Convert.FromBase64String(sB64EncCppA2))
        Dim sB64DecVB4 As String = Encoding.UTF8.GetString(Convert.FromBase64String(sB64EncCppW2))
        b64.Close()

        Console.ReadKey()
    End Sub
End Module

'OUTPUT:
'*******
'Start GarbageStart ...
'... Exit GarbageStart.

'SetNews 08975040
'SetNews 04E50C48
'SetNews 11310040
'SetNews 141H0040
'SetNews 0AK60240
'SetNews 0K400040
'SetNews 18B70840
'SetNews 19C40B41

'Start GarbageClose ...
'19C40B41 delete ...
'18B70840 delete ...
'0AK60240 delete ...
'141H0040 delete ...
'04E50C48 delete ...
'08975040 delete ...
'0K400040 delete ...
'11310040 delete ...
'... Exit GarbageClose.


'Start GarbageStart ...
'... Exit GarbageStart.

'SetNews 08975040
'SetNews 04E50C48
'SetNews 0AK60240
'SetNews 10E30C40
'SetNews 126A0J40
'SetNews 201E0L40
'SetNews 18B70840
'SetNews 19C40B41

'Start GarbageClose ...
'19C40B41 delete ...
'18B70840 delete ...
'126A0J40 delete ...
'10E30C40 delete ...
'04E50C48 delete ...
'08975040 delete ...
'201E0L40 delete ...
'0AK60240 delete ...
'... Exit GarbageClose.
