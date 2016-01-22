' Copyright © exc-jdbi 2016
'
' © mnBase64 2016
'
' mnBase64 - www.github.com/exc-jdbi/base64
'
' mnBase64 is Free and Opensource Software

Option Strict On
Option Explicit On

Imports System.Runtime.InteropServices

Public Class DllBase64
    Public Function Open() As Boolean
        Return dll.startGarbage()
    End Function
    Public Function Close() As Boolean
        Return dll.closeGarbage()
    End Function
    Public Function EncodeB64(ByVal sStr As String, Optional ByVal _utf8 As Boolean = False) As String
        EncodeB64 = String.Empty
        Dim ptr1 As IntPtr = IntPtr.Zero, ptr2 As IntPtr = IntPtr.Zero

        Try
            Dim iBase64 As Integer = 0
            Dim sTxt As String = String.Empty
            ptr2 = Marshal.AllocHGlobal(Marshal.SizeOf(IntPtr.Zero))

            If _utf8 Then
                ptr1 = Marshal.StringToHGlobalUni(sStr)
                iBase64 = dll.b64EncryptW(ptr1, sStr.Length, ptr2, _utf8)
            Else : ptr1 = Marshal.StringToHGlobalAnsi(sStr)
                iBase64 = dll.b64EncryptA(ptr1, sStr.Length, ptr2)
            End If

            If iBase64 > 0 Then
                Dim p0 As IntPtr = IntPtr.Zero
                p0 = Marshal.ReadIntPtr(ptr2)
                If _utf8 Then
                    sTxt = Marshal.PtrToStringUni(p0)
                Else : sTxt = Marshal.PtrToStringAnsi(p0)
                End If
                If Not sTxt = String.Empty Then
                    Return sTxt
                End If
            End If
        Finally
            Marshal.FreeHGlobal(ptr1)
            Marshal.FreeHGlobal(ptr2)
        End Try
    End Function
    Public Function DecodeB64(ByVal sStr As String, Optional ByVal _utf8 As Boolean = False) As String
        DecodeB64 = String.Empty
        Dim ptr1 As IntPtr = IntPtr.Zero, ptr2 As IntPtr = IntPtr.Zero

        Try
            Dim iBase64 As Integer = 0
            Dim strTxt As String = String.Empty
            ptr2 = Marshal.AllocHGlobal(Marshal.SizeOf(IntPtr.Zero))

            If _utf8 Then
                ptr1 = Marshal.StringToHGlobalUni(sStr)
                iBase64 = dll.b64DecryptW(ptr1, sStr.Length, ptr2, _utf8)
            Else : ptr1 = Marshal.StringToHGlobalAnsi(sStr)
                iBase64 = dll.b64DecryptA(ptr1, sStr.Length, ptr2)
            End If

            If iBase64 > 0 Then
                Dim p0 As IntPtr = IntPtr.Zero
                p0 = Marshal.ReadIntPtr(ptr2)
                If _utf8 Then
                    strTxt = Marshal.PtrToStringUni(p0)
                Else : strTxt = Marshal.PtrToStringAnsi(p0)
                End If
                If Not strTxt = String.Empty Then
                    Return strTxt
                End If
            End If
        Finally
            Marshal.FreeHGlobal(ptr1)
            Marshal.FreeHGlobal(ptr2)
        End Try
    End Function
    Private MustInherit Class dll
        Const dllPathConfDebug As String = "../../../../mnBase64Cpp/mnBase64Test/Debug/mnBase64.dll"
        Const dllPathConfRelease As String = "../../../../mnBase64Cpp/mnBase64Test/Debug/mnBase64.dll"

        Const dllPath = dllPathConfDebug
        <DllImport(dllPath)> _
        Public Shared Function startGarbage() As Boolean
        End Function
        <DllImport(dllPath)> _
        Public Shared Function closeGarbage() As Boolean
        End Function
        <DllImport(dllPath, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function b64EncryptA(ByVal strString As IntPtr, ByVal iLen As Integer, ByVal ppOut As IntPtr) As Integer
        End Function
        <DllImport(dllPath, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function b64DecryptA(ByVal strString As IntPtr, ByVal iLen As Integer, ByVal ppOut As IntPtr) As Integer
        End Function
        <DllImport(dllPath, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function b64EncryptW(ByVal strString As IntPtr, ByVal iLen As Integer, ByVal ppOut As IntPtr, Optional ByVal _utf8 As Boolean = False) As Integer
        End Function
        <DllImport(dllPath, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function b64DecryptW(ByVal strString As IntPtr, ByVal iLen As Integer, ByVal ppOut As IntPtr, Optional ByVal _utf8 As Boolean = False) As Integer
        End Function
    End Class
End Class

