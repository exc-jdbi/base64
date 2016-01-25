/*
 * Copyright © exc-jdbi 2016
 *
 * © mnBase64 2016
 *
 * mnBase64 - www.github.com/exc-jdbi/base64
 *
 * mnBase64 is Free and Opensource Software
*/

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TestDll
{
    public class DllBase64
    {
        public bool Open()
        {
            return dll.startGarbage();
        }
        public bool Close()
        {
            return dll.closeGarbage();
        }
        public string EncodeB64(string sStr, bool _utf8 = false)
        {
            string ret = string.Empty;
            IntPtr ptr1 = IntPtr.Zero;
            IntPtr ptr2 = IntPtr.Zero;

            try
            {
                int iBase64 = 0;
                string strTxt = string.Empty;
                ptr2 = Marshal.AllocHGlobal(Marshal.SizeOf(IntPtr.Zero));

                if (_utf8)
                {
                    ptr1 = Marshal.StringToHGlobalUni(sStr);
                    iBase64 = dll.b64EncryptW(ptr1, sStr.Length, ptr2, _utf8);
                }
                else
                {
                    ptr1 = Marshal.StringToHGlobalAnsi(sStr);
                    iBase64 = dll.b64EncryptA(ptr1, sStr.Length, ptr2);
                }

                if (iBase64 > 0)
                {
                    IntPtr p0 = IntPtr.Zero;
                    p0 = Marshal.ReadIntPtr(ptr2);
                    if (_utf8)
                    {
                        strTxt = Marshal.PtrToStringUni(p0);
                    }
                    else
                    {
                        strTxt = Marshal.PtrToStringAnsi(p0);
                    }
                    if (!(strTxt == string.Empty))
                    {
                        return strTxt;
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ptr1);
                Marshal.FreeHGlobal(ptr2);
            }
            return ret;
        }
        public string DecodeB64(string sStr, bool _utf8 = false)
        {
            string ret = string.Empty;
            IntPtr ptr1 = IntPtr.Zero;
            IntPtr ptr2 = IntPtr.Zero;

            try
            {
                int iBase64 = 0;
                string strTxt = string.Empty;
                ptr2 = Marshal.AllocHGlobal(Marshal.SizeOf(IntPtr.Zero));

                if (_utf8)
                {
                    ptr1 = Marshal.StringToHGlobalUni(sStr);
                    iBase64 = dll.b64DecryptW(ptr1, sStr.Length, ptr2, _utf8);
                }
                else
                {
                    ptr1 = Marshal.StringToHGlobalAnsi(sStr);
                    iBase64 = dll.b64DecryptA(ptr1, sStr.Length, ptr2);
                }

                if (iBase64 > 0)
                {
                    IntPtr p0 = IntPtr.Zero;
                    p0 = Marshal.ReadIntPtr(ptr2);
                    if (_utf8)
                    {
                        strTxt = Marshal.PtrToStringUni(p0);
                    }
                    else
                    {
                        strTxt = Marshal.PtrToStringAnsi(p0);
                    }
                    if (!(strTxt == string.Empty))
                    {
                        return strTxt;
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ptr1);
                Marshal.FreeHGlobal(ptr2);
            }
            return ret;
        }
        private abstract class dll
        {
            const string dllPathConfDebug = "../../../../mnBase64Cpp/mnBase64Test/Debug/mnBase64.dll";
            const string dllPathConfRelease = "../../../../mnBase64Cpp/mnBase64Test/Debug/mnBase64.dll";

            const string dllPath = dllPathConfDebug;
            [DllImport(dllPath)]
            public static extern bool startGarbage();
            [DllImport(dllPath)]
            public static extern bool closeGarbage();
            [DllImport(dllPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int b64EncryptA(IntPtr strString, int iLen, IntPtr ppOut);
            [DllImport(dllPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern int b64DecryptA(IntPtr strString, int iLen, IntPtr ppOut);
            [DllImport(dllPath, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
            public static extern int b64EncryptW(IntPtr strString, int iLen, IntPtr ppOut, bool _utf8 = false);
            [DllImport(dllPath, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
            public static extern int b64DecryptW(IntPtr strString, int iLen, IntPtr ppOut, bool _utf8 = false);
        }
    }
}
