using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AlterationRecords.Common
{
    public class Encryption
    {
        private static byte[] key = null;
        private static byte[] IV = null;
        private static Hashtable m_hashTable = null;

        static Encryption()
        {
            try
            {
                InitClass();
            }
            catch (Exception)
            {
                //ExceptionManager.Publish(ex);
            }
        }

        private static void InitClass()
        {
            key = Encoding.UTF8.GetBytes(ABA + AA + CADE + EJOI + EJI + KJIE + KJE + AAKJE + KJEI + KEIMP + EJI343 + KKpEP + KJEere + eip3934 + ke34 + erjpe);
            IV = Encoding.UTF8.GetBytes(KEEIRJ + ERAEA + aer45q + asdflk67 + kaeprr84 + akmpe3432 + piamef + afe34p4a + sakdjf438 + gmirpo5086
                + fig059as + a60vlsm + jpln47 + jkgaaierii9 + djzmd958 + dymazuj + fkr895);
            m_hashTable = new Hashtable();
        }

        private static string ABA
        {
            get { return "T"; }
        }

        private static string AA
        {
            get { return "r"; }
        }

        private static string CADE
        {
            get { return "a"; }
        }

        private static string KEEIRJ
        {
            get { return "D"; }
        }

        private static string ERAEA
        {
            get { return "a"; }
        }

        private static string aer45q
        {
            get { return "v"; }
        }

        private static string asdflk67
        {
            get { return "e"; }
        }

        private static string kaeprr84
        {
            get { return "M"; }
        }

        private static string akmpe3432
        {
            get { return "a"; }
        }

        private static string piamef
        {
            get { return "l"; }
        }

        private static string afe34p4a
        {
            get { return "c"; }
        }

        private static string sakdjf438
        {
            get { return "o"; }
        }

        private static string gmirpo5086
        {
            get { return "l"; }
        }

        private static string fig059as
        {
            get { return "m"; }
        }

        private static string a60vlsm
        {
            get { return "E"; }
        }

        private static string jpln47
        {
            get { return "h"; }
        }

        private static string jkgaaierii9
        {
            get { return "l"; }
        }

        private static string djzmd958
        {
            get { return "e"; }
        }

        private static string dymazuj
        {
            get { return "1"; }
        }

        private static string fkr895
        {
            get { return "2"; }
        }

        private static string EJOI
        {
            get { return "v"; }
        }

        private static string EJI
        {
            get { return "i"; }
        }

        private static string KJIE
        {
            get { return "s"; }
        }

        private static string KJE
        {
            get { return "A"; }
        }

        private static string AAKJE
        {
            get { return "L"; }
        }

        private static string KJEI
        {
            get { return "u"; }
        }

        private static string KEIMP
        {
            get { return "c"; }
        }

        private static string EJI343
        {
            get { return "e"; }
        }

        private static string KKpEP
        {
            get { return "r"; }
        }

        private static string KJEere
        {
            get { return "o"; }
        }

        private static string eip3934
        {
            get { return "1"; }
        }

        private static string ke34
        {
            get { return "2"; }
        }

        private static string erjpe
        {
            get { return "3"; }
        }

        public static string Encrypt(string input)
        {
            string returnString = "";

            RijndaelManaged rm = null;
            ICryptoTransform trans = null;
            MemoryStream plainStream = null;
            MemoryStream encryptedStream = null;
            CryptoStream enc = null;
            try
            {
                if ((input != null) && (input.Length > 0))
                {
                    //Instantiate the crypto provider
                    rm = new RijndaelManaged();

                    //Create the encryption transformer
                    trans = rm.CreateEncryptor(key, IV);

                    //Put the input into a memorystream
                    plainStream = new MemoryStream(Encoding.UTF8.GetBytes(input));

                    //Prepare the crypto stream for writing
                    encryptedStream = new MemoryStream();

                    //put it into a byte array
                    byte[] byteArray = plainStream.ToArray();

                    enc = new CryptoStream(encryptedStream, trans, CryptoStreamMode.Write);

                    //Write input bytes into crypto stream and close up.
                    enc.Write(byteArray, 0, (int)plainStream.Length);
                    enc.FlushFinalBlock();

                    //Write the encryptor back out to the text.
                    returnString = Convert.ToBase64String(encryptedStream.ToArray());
                }
            }
            catch (Exception)
            {
                //ExceptionManager.Publish(ex);
            }
            finally
            {
                if (rm != null)
                {
                    rm.Clear();
                }

                if (trans != null)
                {
                    trans.Dispose();
                }

                if (plainStream != null)
                {
                    plainStream.Close();
                }

                if (encryptedStream != null)
                {
                    encryptedStream.Close();
                }

                if (enc != null)
                {
                    enc.Close();
                }
            }

            return returnString;
        }

        public static string Decrypt(string input)
        {
            string returnString = "";

            RijndaelManaged rm = null;
            ICryptoTransform trans = null;
            MemoryStream encryptedStream = null;
            MemoryStream plainStream = null;
            CryptoStream cs = null;
            try
            {
                if ((input != null) && (input.Length > 0))
                {
                    //Instantiate the crypto provider
                    if (m_hashTable.ContainsKey(input))
                    {
                        returnString = Util.CString(m_hashTable[input], "");
                    }
                    else
                    {
                        rm = new RijndaelManaged();

                        // Create the decryption transformer
                        trans = rm.CreateDecryptor(key, IV);

                        // Put the string into a bytearray
                        byte[] encryptedByteArray = Convert.FromBase64String(input);

                        // Put the byte array into a memorystream.
                        encryptedStream = new MemoryStream(encryptedByteArray);

                        // Put the encrypted string in the cryptostream class
                        CryptoStreamMode mode = CryptoStreamMode.Read;
                        cs = new CryptoStream(encryptedStream, trans, mode);

                        byte[] plainByteArray = new byte[encryptedByteArray.Length];
                        int bytesIn = cs.Read(plainByteArray, 0, (int)encryptedStream.Length);

                        cs.Close();

                        plainStream = new MemoryStream();
                        plainStream.Write(plainByteArray, 0, bytesIn);

                        returnString = Encoding.UTF8.GetString(plainByteArray, 0, bytesIn);

                        m_hashTable.Add(input, returnString);
                    }
                }
            }
            catch (Exception)
            {
                //string sUserMsg = "Unable to decrypt input = '" + input + "'."; 
                //ExceptionManager.Publish(ex);
            }
            finally
            {
                if (rm != null)
                {
                    rm.Clear();
                }

                if (trans != null)
                {
                    trans.Dispose();
                }

                if (encryptedStream != null)
                {
                    encryptedStream.Close();
                }

                if (plainStream != null)
                {
                    plainStream.Close();
                }

                if (cs != null)
                {
                    cs.Close();
                }
            }

            return returnString;
        }

        public static int GetEncryptedTextLength(int textLength)
        {
            int size = int.MinValue;
            if (textLength >= 1 && textLength <= 15)
            {
                size = 24;
            }
            else if (textLength >= 16 && textLength <= 31)
            {
                size = 44;
            }
            else if (textLength >= 32 && textLength <= 47)
            {
                size = 64;
            }
            else if (textLength >= 48 && textLength <= 63)
            {
                size = 88;
            }
            else if (textLength >= 64 && textLength <= 79)
            {
                size = 108;
            }
            else if (textLength >= 80 && textLength <= 95)
            {
                size = 128;
            }
            else if (textLength >= 96 && textLength <= 111)
            {
                size = 152;
            }
            else if (textLength >= 112 && textLength <= 127)
            {
                size = 172;
            }
            else if (textLength >= 128 && textLength <= 143)
            {
                size = 192;
            }
            else if (textLength >= 144 && textLength <= 159)
            {
                size = 216;
            }
            else if (textLength >= 160 && textLength <= 175)
            {
                size = 236;
            }
            else if (textLength >= 176 && textLength <= 191)
            {
                size = 256;
            }
            else if (textLength >= 192 && textLength <= 207)
            {
                size = 280;
            }
            else if (textLength >= 208 && textLength <= 223)
            {
                size = 300;
            }
            else if (textLength >= 224 && textLength <= 239)
            {
                size = 320;
            }
            else if (textLength >= 240 && textLength <= 255)
            {
                size = 344;
            }
            else if (textLength >= 256 && textLength <= 271)
            {
                size = 364;
            }
            else if (textLength >= 272 && textLength <= 287)
            {
                size = 384;
            }
            else if (textLength >= 288 && textLength <= 303)
            {
                size = 408;
            }
            else if (textLength >= 304 && textLength <= 319)
            {
                size = 428;
            }
            else if (textLength >= 320 && textLength <= 335)
            {
                size = 448;
            }
            else if (textLength >= 336 && textLength <= 351)
            {
                size = 472;
            }
            else if (textLength >= 352 && textLength <= 367)
            {
                size = 492;
            }
            else if (textLength >= 368 && textLength <= 383)
            {
                size = 512;
            }
            else if (textLength >= 384 && textLength <= 399)
            {
                size = 536;
            }
            else if (textLength >= 400 && textLength <= 415)
            {
                size = 556;
            }
            else if (textLength >= 416 && textLength <= 431)
            {
                size = 576;
            }
            else if (textLength >= 432 && textLength <= 447)
            {
                size = 600;
            }
            else if (textLength >= 448 && textLength <= 463)
            {
                size = 620;
            }
            else if (textLength >= 464 && textLength <= 479)
            {
                size = 640;
            }
            else if (textLength >= 480 && textLength <= 495)
            {
                size = 664;
            }
            else if (textLength >= 496 && textLength <= 511)
            {
                size = 684;
            }
            else if (textLength >= 512 && textLength <= 527)
            {
                size = 704;
            }
            else if (textLength >= 528 && textLength <= 543)
            {
                size = 728;
            }
            else if (textLength >= 544 && textLength <= 559)
            {
                size = 748;
            }
            else if (textLength >= 560 && textLength <= 575)
            {
                size = 768;
            }
            else if (textLength >= 576 && textLength <= 591)
            {
                size = 792;
            }
            else if (textLength >= 592 && textLength <= 607)
            {
                size = 812;
            }
            else if (textLength >= 608 && textLength <= 623)
            {
                size = 832;
            }
            else if (textLength >= 624 && textLength <= 639)
            {
                size = 856;
            }
            else if (textLength >= 640 && textLength <= 655)
            {
                size = 876;
            }
            else if (textLength >= 656 && textLength <= 671)
            {
                size = 896;
            }
            else if (textLength >= 672 && textLength <= 687)
            {
                size = 920;
            }
            else if (textLength >= 688 && textLength <= 703)
            {
                size = 940;
            }
            else if (textLength >= 704 && textLength <= 719)
            {
                size = 960;
            }
            else if (textLength >= 720 && textLength <= 735)
            {
                size = 984;
            }
            else if (textLength >= 736 && textLength <= 751)
            {
                size = 1004;
            }
            else if (textLength >= 752 && textLength <= 767)
            {
                size = 1024;
            }
            else if (textLength >= 768 && textLength <= 783)
            {
                size = 1048;
            }
            else if (textLength >= 784 && textLength <= 799)
            {
                size = 1068;
            }
            else if (textLength >= 800 && textLength <= 815)
            {
                size = 1088;
            }
            else if (textLength >= 816 && textLength <= 831)
            {
                size = 1112;
            }
            else if (textLength >= 832 && textLength <= 847)
            {
                size = 1132;
            }
            else if (textLength >= 848 && textLength <= 863)
            {
                size = 1152;
            }
            else if (textLength >= 864 && textLength <= 879)
            {
                size = 1176;
            }
            else if (textLength >= 880 && textLength <= 895)
            {
                size = 1196;
            }
            else if (textLength >= 896 && textLength <= 911)
            {
                size = 1216;
            }
            else if (textLength >= 912 && textLength <= 927)
            {
                size = 1240;
            }
            else if (textLength >= 928 && textLength <= 943)
            {
                size = 1260;
            }
            else if (textLength >= 944 && textLength <= 959)
            {
                size = 1280;
            }
            else if (textLength >= 960 && textLength <= 975)
            {
                size = 1304;
            }
            else if (textLength >= 976 && textLength <= 991)
            {
                size = 1324;
            }
            else if (textLength >= 992 && textLength <= 1007)
            {
                size = 1344;
            }
            else if (textLength >= 1008 && textLength <= 1023)
            {
                size = 1368;
            }
            else if (textLength >= 1024 && textLength <= 1039)
            {
                size = 1388;
            }
            else if (textLength >= 1040 && textLength <= 1055)
            {
                size = 1408;
            }
            else if (textLength >= 1056 && textLength <= 1071)
            {
                size = 1432;
            }
            else if (textLength >= 1072 && textLength <= 1087)
            {
                size = 1452;
            }
            else if (textLength >= 1088 && textLength <= 1103)
            {
                size = 1472;
            }
            else if (textLength >= 1104 && textLength <= 1119)
            {
                size = 1496;
            }
            else if (textLength >= 1120 && textLength <= 1135)
            {
                size = 1516;
            }
            else if (textLength >= 1136 && textLength <= 1151)
            {
                size = 1536;
            }
            else if (textLength >= 1152 && textLength <= 1167)
            {
                size = 1560;
            }
            else if (textLength >= 1168 && textLength <= 1183)
            {
                size = 1580;
            }
            else if (textLength >= 1184 && textLength <= 1199)
            {
                size = 1600;
            }
            else if (textLength >= 1200 && textLength <= 1215)
            {
                size = 1624;
            }
            else if (textLength >= 1216 && textLength <= 1231)
            {
                size = 1644;
            }
            else if (textLength >= 1232 && textLength <= 1247)
            {
                size = 1664;
            }
            else if (textLength >= 1248 && textLength <= 1263)
            {
                size = 1688;
            }
            else if (textLength >= 1264 && textLength <= 1279)
            {
                size = 1708;
            }
            else if (textLength >= 1280 && textLength <= 1295)
            {
                size = 1728;
            }
            else if (textLength >= 1296 && textLength <= 1311)
            {
                size = 1752;
            }
            else if (textLength >= 1312 && textLength <= 1327)
            {
                size = 1772;
            }
            else if (textLength >= 1328 && textLength <= 1343)
            {
                size = 1792;
            }
            else if (textLength >= 1344 && textLength <= 1359)
            {
                size = 1816;
            }
            else if (textLength >= 1360 && textLength <= 1375)
            {
                size = 1836;
            }
            else if (textLength >= 1376 && textLength <= 1391)
            {
                size = 1856;
            }
            else if (textLength >= 1392 && textLength <= 1407)
            {
                size = 1880;
            }
            else if (textLength >= 1408 && textLength <= 1423)
            {
                size = 1900;
            }
            else if (textLength >= 1424 && textLength <= 1439)
            {
                size = 1920;
            }
            else if (textLength >= 1440 && textLength <= 1455)
            {
                size = 1944;
            }
            else if (textLength >= 1456 && textLength <= 1471)
            {
                size = 1964;
            }
            else if (textLength >= 1472 && textLength <= 1487)
            {
                size = 1984;
            }
            else if (textLength >= 1488 && textLength <= 1503)
            {
                size = 2008;
            }
            else if (textLength >= 1504 && textLength <= 1519)
            {
                size = 2028;
            }
            else if (textLength >= 1520 && textLength <= 1535)
            {
                size = 2048;
            }
            else if (textLength >= 1536 && textLength <= 1551)
            {
                size = 2072;
            }
            else if (textLength >= 1552 && textLength <= 1567)
            {
                size = 2092;
            }
            else if (textLength >= 1568 && textLength <= 1583)
            {
                size = 2112;
            }
            else if (textLength >= 1584 && textLength <= 1599)
            {
                size = 2136;
            }
            else if (textLength >= 1600 && textLength <= 1615)
            {
                size = 2156;
            }
            else if (textLength >= 1616 && textLength <= 1631)
            {
                size = 2176;
            }
            else if (textLength >= 1632 && textLength <= 1647)
            {
                size = 2200;
            }
            else if (textLength >= 1648 && textLength <= 1663)
            {
                size = 2220;
            }
            else if (textLength >= 1664 && textLength <= 1679)
            {
                size = 2240;
            }
            else if (textLength >= 1680 && textLength <= 1695)
            {
                size = 2264;
            }
            else if (textLength >= 1696 && textLength <= 1711)
            {
                size = 2284;
            }
            else if (textLength >= 1712 && textLength <= 1727)
            {
                size = 2304;
            }
            else if (textLength >= 1728 && textLength <= 1743)
            {
                size = 2328;
            }
            else if (textLength >= 1744 && textLength <= 1759)
            {
                size = 2348;
            }
            else if (textLength >= 1760 && textLength <= 1775)
            {
                size = 2368;
            }
            else if (textLength >= 1776 && textLength <= 1791)
            {
                size = 2392;
            }
            else if (textLength >= 1792 && textLength <= 1807)
            {
                size = 2412;
            }
            else if (textLength >= 1808 && textLength <= 1823)
            {
                size = 2432;
            }
            else if (textLength >= 1824 && textLength <= 1839)
            {
                size = 2456;
            }
            else if (textLength >= 1840 && textLength <= 1855)
            {
                size = 2476;
            }
            else if (textLength >= 1856 && textLength <= 1871)
            {
                size = 2496;
            }
            else if (textLength >= 1872 && textLength <= 1887)
            {
                size = 2520;
            }
            else if (textLength >= 1888 && textLength <= 1903)
            {
                size = 2540;
            }
            else if (textLength >= 1904 && textLength <= 1919)
            {
                size = 2560;
            }
            else if (textLength >= 1920 && textLength <= 1935)
            {
                size = 2584;
            }
            else if (textLength >= 1936 && textLength <= 1951)
            {
                size = 2604;
            }
            else if (textLength >= 1952 && textLength <= 1967)
            {
                size = 2624;
            }
            else if (textLength >= 1968 && textLength <= 1983)
            {
                size = 2648;
            }
            else if (textLength >= 1984 && textLength <= 1999)
            {
                size = 2668;
            }
            else if (textLength >= 2000 && textLength <= 2015)
            {
                size = 2688;
            }
            else if (textLength >= 2016 && textLength <= 2031)
            {
                size = 2712;
            }
            else if (textLength >= 2032 && textLength <= 2047)
            {
                size = 2732;
            }
            else if (textLength >= 2048 && textLength <= 2063)
            {
                size = 2752;
            }
            else if (textLength >= 2064 && textLength <= 2079)
            {
                size = 2776;
            }
            else if (textLength >= 2080 && textLength <= 2095)
            {
                size = 2796;
            }
            else if (textLength >= 2096 && textLength <= 2111)
            {
                size = 2816;
            }
            else if (textLength >= 2112 && textLength <= 2127)
            {
                size = 2840;
            }
            else if (textLength >= 2128 && textLength <= 2143)
            {
                size = 2860;
            }
            else if (textLength >= 2144 && textLength <= 2159)
            {
                size = 2880;
            }
            else if (textLength >= 2160 && textLength <= 2175)
            {
                size = 2904;
            }
            else if (textLength >= 2176 && textLength <= 2191)
            {
                size = 2924;
            }
            else if (textLength >= 2192 && textLength <= 2207)
            {
                size = 2944;
            }
            else if (textLength >= 2208 && textLength <= 2223)
            {
                size = 2968;
            }
            else if (textLength >= 2224 && textLength <= 2239)
            {
                size = 2988;
            }
            else if (textLength >= 2240 && textLength <= 2255)
            {
                size = 3008;
            }
            else if (textLength >= 2256 && textLength <= 2271)
            {
                size = 3032;
            }
            else if (textLength >= 2272 && textLength <= 2287)
            {
                size = 3052;
            }
            else if (textLength >= 2288 && textLength <= 2303)
            {
                size = 3072;
            }
            else if (textLength >= 2304 && textLength <= 2319)
            {
                size = 3096;
            }
            else if (textLength >= 2320 && textLength <= 2335)
            {
                size = 3116;
            }
            else if (textLength >= 2336 && textLength <= 2351)
            {
                size = 3136;
            }
            else if (textLength >= 2352 && textLength <= 2367)
            {
                size = 3160;
            }
            else if (textLength >= 2368 && textLength <= 2383)
            {
                size = 3180;
            }
            else if (textLength >= 2384 && textLength <= 2399)
            {
                size = 3200;
            }
            else if (textLength >= 2400 && textLength <= 2415)
            {
                size = 3224;
            }
            else if (textLength >= 2416 && textLength <= 2431)
            {
                size = 3244;
            }
            else if (textLength >= 2432 && textLength <= 2447)
            {
                size = 3264;
            }
            else if (textLength >= 2448 && textLength <= 2463)
            {
                size = 3288;
            }
            else if (textLength >= 2464 && textLength <= 2479)
            {
                size = 3308;
            }
            else if (textLength >= 2480 && textLength <= 2495)
            {
                size = 3328;
            }
            else if (textLength >= 2496 && textLength <= 2511)
            {
                size = 3352;
            }
            else if (textLength >= 2512 && textLength <= 2527)
            {
                size = 3372;
            }
            else if (textLength >= 2528 && textLength <= 2543)
            {
                size = 3392;
            }
            else if (textLength >= 2544 && textLength <= 2559)
            {
                size = 3416;
            }
            else if (textLength >= 2560 && textLength <= 2575)
            {
                size = 3436;
            }
            else if (textLength >= 2576 && textLength <= 2591)
            {
                size = 3456;
            }
            else if (textLength >= 2592 && textLength <= 2607)
            {
                size = 3480;
            }
            else if (textLength >= 2608 && textLength <= 2623)
            {
                size = 3500;
            }
            else if (textLength >= 2624 && textLength <= 2639)
            {
                size = 3520;
            }
            else if (textLength >= 2640 && textLength <= 2655)
            {
                size = 3544;
            }
            else if (textLength >= 2656 && textLength <= 2671)
            {
                size = 3564;
            }
            else if (textLength >= 2672 && textLength <= 2687)
            {
                size = 3584;
            }
            else if (textLength >= 2688 && textLength <= 2703)
            {
                size = 3608;
            }
            else if (textLength >= 2704 && textLength <= 2719)
            {
                size = 3628;
            }
            else if (textLength >= 2720 && textLength <= 2735)
            {
                size = 3648;
            }
            else if (textLength >= 2736 && textLength <= 2751)
            {
                size = 3672;
            }
            else if (textLength >= 2752 && textLength <= 2767)
            {
                size = 3692;
            }
            else if (textLength >= 2768 && textLength <= 2783)
            {
                size = 3712;
            }
            else if (textLength >= 2784 && textLength <= 2799)
            {
                size = 3736;
            }
            else if (textLength >= 2800 && textLength <= 2815)
            {
                size = 3756;
            }
            else if (textLength >= 2816 && textLength <= 2831)
            {
                size = 3776;
            }
            else if (textLength >= 2832 && textLength <= 2847)
            {
                size = 3800;
            }
            else if (textLength >= 2848 && textLength <= 2863)
            {
                size = 3820;
            }
            else if (textLength >= 2864 && textLength <= 2879)
            {
                size = 3840;
            }
            else if (textLength >= 2880 && textLength <= 2895)
            {
                size = 3864;
            }
            else if (textLength >= 2896 && textLength <= 2911)
            {
                size = 3884;
            }
            else if (textLength >= 2912 && textLength <= 2927)
            {
                size = 3904;
            }
            else if (textLength >= 2928 && textLength <= 2943)
            {
                size = 3928;
            }
            else if (textLength >= 2944 && textLength <= 2959)
            {
                size = 3948;
            }
            else if (textLength >= 2960 && textLength <= 2975)
            {
                size = 3968;
            }
            else if (textLength >= 2976 && textLength <= 2991)
            {
                size = 3992;
            }
            else if (textLength >= 2992 && textLength <= 3007)
            {
                size = 4012;
            }
            else if (textLength >= 3008 && textLength <= 3023)
            {
                size = 4032;
            }
            else if (textLength >= 3024 && textLength <= 3039)
            {
                size = 4056;
            }
            else if (textLength >= 3040 && textLength <= 3055)
            {
                size = 4076;
            }
            else if (textLength >= 3056 && textLength <= 3071)
            {
                size = 4096;
            }
            else if (textLength >= 3072 && textLength <= 3087)
            {
                size = 4120;
            }
            else if (textLength >= 3088 && textLength <= 3103)
            {
                size = 4140;
            }
            else if (textLength >= 3104 && textLength <= 3119)
            {
                size = 4160;
            }
            else if (textLength >= 3120 && textLength <= 3135)
            {
                size = 4184;
            }
            else if (textLength >= 3136 && textLength <= 3151)
            {
                size = 4204;
            }
            else if (textLength >= 3152 && textLength <= 3167)
            {
                size = 4224;
            }
            else if (textLength >= 3168 && textLength <= 3183)
            {
                size = 4248;
            }
            else if (textLength >= 3184 && textLength <= 3199)
            {
                size = 4268;
            }
            else if (textLength >= 3200 && textLength <= 3215)
            {
                size = 4288;
            }
            else if (textLength >= 3216 && textLength <= 3231)
            {
                size = 4312;
            }
            else if (textLength >= 3232 && textLength <= 3247)
            {
                size = 4332;
            }
            else if (textLength >= 3248 && textLength <= 3263)
            {
                size = 4352;
            }
            else if (textLength >= 3264 && textLength <= 3279)
            {
                size = 4376;
            }
            else if (textLength >= 3280 && textLength <= 3295)
            {
                size = 4396;
            }
            else if (textLength >= 3296 && textLength <= 3311)
            {
                size = 4416;
            }
            else if (textLength >= 3312 && textLength <= 3327)
            {
                size = 4440;
            }
            else if (textLength >= 3328 && textLength <= 3343)
            {
                size = 4460;
            }
            else if (textLength >= 3344 && textLength <= 3359)
            {
                size = 4480;
            }
            else if (textLength >= 3360 && textLength <= 3375)
            {
                size = 4504;
            }
            else if (textLength >= 3376 && textLength <= 3391)
            {
                size = 4524;
            }
            else if (textLength >= 3392 && textLength <= 3407)
            {
                size = 4544;
            }
            else if (textLength >= 3408 && textLength <= 3423)
            {
                size = 4568;
            }
            else if (textLength >= 3424 && textLength <= 3439)
            {
                size = 4588;
            }
            else if (textLength >= 3440 && textLength <= 3455)
            {
                size = 4608;
            }
            else if (textLength >= 3456 && textLength <= 3471)
            {
                size = 4632;
            }
            else if (textLength >= 3472 && textLength <= 3487)
            {
                size = 4652;
            }
            else if (textLength >= 3488 && textLength <= 3503)
            {
                size = 4672;
            }
            else if (textLength >= 3504 && textLength <= 3519)
            {
                size = 4696;
            }
            else if (textLength >= 3520 && textLength <= 3535)
            {
                size = 4716;
            }
            else if (textLength >= 3536 && textLength <= 3551)
            {
                size = 4736;
            }
            else if (textLength >= 3552 && textLength <= 3567)
            {
                size = 4760;
            }
            else if (textLength >= 3568 && textLength <= 3583)
            {
                size = 4780;
            }
            else if (textLength >= 3584 && textLength <= 3599)
            {
                size = 4800;
            }
            else if (textLength >= 3600 && textLength <= 3615)
            {
                size = 4824;
            }
            else if (textLength >= 3616 && textLength <= 3631)
            {
                size = 4844;
            }
            else if (textLength >= 3632 && textLength <= 3647)
            {
                size = 4864;
            }
            else if (textLength >= 3648 && textLength <= 3663)
            {
                size = 4888;
            }
            else if (textLength >= 3664 && textLength <= 3679)
            {
                size = 4908;
            }
            else if (textLength >= 3680 && textLength <= 3695)
            {
                size = 4928;
            }
            else if (textLength >= 3696 && textLength <= 3711)
            {
                size = 4952;
            }
            else if (textLength >= 3712 && textLength <= 3727)
            {
                size = 4972;
            }
            else if (textLength >= 3728 && textLength <= 3743)
            {
                size = 4992;
            }
            else if (textLength >= 3744 && textLength <= 3759)
            {
                size = 5016;
            }
            else if (textLength >= 3760 && textLength <= 3775)
            {
                size = 5036;
            }
            else if (textLength >= 3776 && textLength <= 3791)
            {
                size = 5056;
            }
            else if (textLength >= 3792 && textLength <= 3807)
            {
                size = 5080;
            }
            else if (textLength >= 3808 && textLength <= 3823)
            {
                size = 5100;
            }
            else if (textLength >= 3824 && textLength <= 3839)
            {
                size = 5120;
            }
            else if (textLength >= 3840 && textLength <= 3855)
            {
                size = 5144;
            }
            else if (textLength >= 3856 && textLength <= 3871)
            {
                size = 5164;
            }
            else if (textLength >= 3872 && textLength <= 3887)
            {
                size = 5184;
            }
            else if (textLength >= 3888 && textLength <= 3903)
            {
                size = 5208;
            }
            else if (textLength >= 3904 && textLength <= 3919)
            {
                size = 5228;
            }
            else if (textLength >= 3920 && textLength <= 3935)
            {
                size = 5248;
            }
            else if (textLength >= 3936 && textLength <= 3951)
            {
                size = 5272;
            }
            else if (textLength >= 3952 && textLength <= 3967)
            {
                size = 5292;
            }
            else if (textLength >= 3968 && textLength <= 3983)
            {
                size = 5312;
            }
            else if (textLength >= 3984 && textLength <= 3999)
            {
                size = 5336;
            }
            else if (textLength >= 4000 && textLength <= 0)
            {
                size = 5356;
            }
            return size + 5;
        }
    }
}