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
using TestDll;
using System.Text;
using System.Collections.Generic;

namespace mnBase64CSharp
{
    class mnBase64CSharpTest
    {
        static void Main(string[] args)
        {
            string cText = "A Ansistring with special characters ¾½¼™Þ‰Æ€#èüä$!$£{°§1*-+±'?Øø}[»²³$£/&%ç¬›|‹¢@©®÷«]. ";
            string uText = "A Widestring with special characters 日本人, ไทย, Український, עברית, العربية, भारतीय,فارسی, русский, සිංහල, தமிழ். ";

            StringBuilder cSb = new StringBuilder();
            StringBuilder uSb = new StringBuilder();
            for (int i = 0; i <= 100000 - 1; i++)
            {
                cSb.Append(cText);
                uSb.Append(uText);
            }

            DllBase64 b64 = new DllBase64();

            //Encode to Base64
            string sB64EncCppA = b64.EncodeB64(cSb.ToString());
            string sB64EncCppW = b64.EncodeB64(uSb.ToString(), true);
            string sB64EncVB1 = Convert.ToBase64String(Encoding.Default.GetBytes(cSb.ToString()));
            string sB64EncVB2 = Convert.ToBase64String(Encoding.UTF8.GetBytes(uSb.ToString()));

            //Decode Base64
            string sB64DecCppA = b64.DecodeB64(sB64EncVB1);
            string sB64DecCppW = b64.DecodeB64(sB64EncVB2, true);
            string sB64DecVB1 = Encoding.Default.GetString(Convert.FromBase64String(sB64EncCppA));
            string sB64DecVB2 = Encoding.UTF8.GetString(Convert.FromBase64String(sB64EncCppW));
            b64.Close();

            b64.Open();
            string sB64EncCppA2 = b64.EncodeB64(sB64DecVB1);
            string sB64EncCppW2 = b64.EncodeB64(sB64DecVB2, true);
            string sB64EncVB3 = Convert.ToBase64String(Encoding.Default.GetBytes(sB64DecCppA));
            string sB64EncVB4 = Convert.ToBase64String(Encoding.UTF8.GetBytes(sB64DecCppW));

            string sB64DecCppA2 = b64.DecodeB64(sB64EncVB3);
            string sB64DecCppW2 = b64.DecodeB64(sB64EncVB4, true);
            string sB64DecVB3 = Encoding.Default.GetString(Convert.FromBase64String(sB64EncCppA2));
            string sB64DecVB4 = Encoding.UTF8.GetString(Convert.FromBase64String(sB64EncCppW2));
            b64.Close();
            
            Console.ReadKey();
        }
    }
}


/*

OUTPUT:
*******
Start GarbageStart ...
... Exit GarbageStart.

SetNews 08975040
SetNews 04E50C48
SetNews 11310040
SetNews 141H0040
SetNews 0AK60240
SetNews 0K400040
SetNews 18B70840
SetNews 19C40B41

Start GarbageClose ...
19C40B41 delete ...
18B70840 delete ...
0AK60240 delete ...
141H0040 delete ...
04E50C48 delete ...
08975040 delete ...
0K400040 delete ...
11310040 delete ...
... Exit GarbageClose.


Start GarbageStart ...
... Exit GarbageStart.

SetNews 08975040
SetNews 04E50C48
SetNews 0AK60240
SetNews 10E30C40
SetNews 126A0J40
SetNews 201E0L40
SetNews 18B70840
SetNews 19C40B41

Start GarbageClose ...
19C40B41 delete ...
18B70840 delete ...
126A0J40 delete ...
10E30C40 delete ...
04E50C48 delete ...
08975040 delete ...
201E0L40 delete ...
0AK60240 delete ...
... Exit GarbageClose.

*/