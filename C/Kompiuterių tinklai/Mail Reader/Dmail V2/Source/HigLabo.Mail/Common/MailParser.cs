using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace HigLabo.Net.Mail
{
    /// Class for parse mail text.
    /// <summary>
    /// Class for parse mail text.
    /// </summary>
    public class MailParser
    {
        private class RegexList
        {
            public static readonly Regex HexDecoder = new Regex("((\\=([0-9A-F][0-9A-F]))*)", RegexOptions.IgnoreCase);
            public static readonly Regex HexDecoder1 = new Regex("((%([0-9A-F][0-9A-F]))*)", RegexOptions.IgnoreCase);
            public static readonly Regex IsResponseOk = new Regex(@"^.*\+OK.*$", RegexOptions.IgnoreCase);
            public static readonly Regex DecodeByRfc2047 = new Regex(@"[\s]{0,1}[=][\?](?<Encoding>[^?]+)[\?](?<BorQ>[B|b|Q|q])[\?](?<Value>[^?]+)[\?][=][\s]{0,1}");
            public static readonly Regex DecodeByRfc2231 = new Regex(@"(?<Encoding>[^']+)[\'](?<Language>[a-zA-z\-]*)[\'](?<Value>[^\s]+)");
            public static readonly Regex IsReceiveCompleted = new Regex(String.Format(@"{0}\.{0}", MailParser.NewLine));
			public static readonly Regex AsciiCharOnly = new Regex("[^\x00-\x7F]");
            public static readonly Regex ThreeLetterTimeZone = new Regex("(\\([^(].*\\))");
            public static readonly Regex TimeZone = new Regex("[+\\-][0-9][0-9][0-9][0-9]");
            public static readonly ICollection<Regex> ContentTypeBoundary = new List<Regex>();
            public static readonly ICollection<Regex> ContentTypeName = new List<Regex>();
            public static readonly ICollection<Regex> ContentDispositionFileName = new List<Regex>();
            public static readonly String Rfc2231FormatText = @"[;\t\s]+{0}\*{1}=(?<Value>[^\n\r;]+)(;|$)";
            public static readonly String Rfc2231FormatText1 = @"[;\t\s]+{0}\*{1}\*=(?<Value>[^\n\r;]+)(;|$)";
            static RegexList()
            {
                InitializeRegexList();
            }
            private static void InitializeRegexList()
            {
                RegexList.ContentTypeBoundary.Add(new Regex(".*boundary=[\"]*(?<Value>[^\"]*).*", RegexOptions.IgnoreCase));
                RegexList.ContentTypeName.Add(new Regex(".*name=[\"]*(?<Value>[^\"]*)[;\n\r]", RegexOptions.IgnoreCase));
                RegexList.ContentTypeName.Add(new Regex(".*name=[\"]*(?<Value>[^\"]*).*", RegexOptions.IgnoreCase));
                RegexList.ContentTypeName.Add(new Regex(@"[;\t\s]+name\*=(?<Value>[^\n\r]+).*", RegexOptions.IgnoreCase));
                RegexList.ContentDispositionFileName.Add(new Regex("[;\t\\s]+filename=[\"]*(?<Value>[^\"]*)[;\n\r]", RegexOptions.IgnoreCase));
                RegexList.ContentDispositionFileName.Add(new Regex("[;\t\\s]+filename=[\"]*(?<Value>[^\"]*).*", RegexOptions.IgnoreCase));
                RegexList.ContentDispositionFileName.Add(new Regex("[;\t\\s]+filename\\*=[\"]*(?<Value>[^\"\n\r]+).*", RegexOptions.IgnoreCase));
            }
        }
        private static Dictionary<string, Encoding> _EncodingList = new Dictionary<string, Encoding>();
        private static TimeSpan _TimeZoneOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
        private static String _DateTimeFormatString = "ddd, dd MMM yyyy HH:mm:ss +0000";
        /// This is multi-part message in MIME formatという値を取得します。
        /// <summary>
        /// This is multi-part message in MIME formatという値を取得します。
        /// </summary>
        public const String ThisIsMultiPartMessageInMimeFormat = "This is multi-part message in MIME format.";
        /// 改行文字列の値です。
        /// <summary>
        /// 改行文字列の値です。
        /// </summary>
        public const String NewLine = "\r\n";
        /// 日付文字列の書式を設定する文字列です。
        /// <summary>
        /// 日付文字列の書式を設定する文字列です。
        /// </summary>
        public static String DateTimeFormatString
        {
            get { return MailParser._DateTimeFormatString; }
        }
        /// 日付文字列のUTCからのオフセットをセットするための値を取得または設定します。
        /// <summary>
        /// 日付文字列のUTCからのオフセットをセットするための値を取得または設定します。
        /// この値を変更することによりDateTimeFormatStringのオフセットの値を変更可能です。
        /// </summary>
        public static TimeSpan TimeZoneOffset
        {
            get { return MailParser._TimeZoneOffset; }
            set
            {
                MailParser._TimeZoneOffset = value;
                MailParser.SetDateTimeFormatString();
            }
        }
        /// 1行の最大文字数を取得します。
        /// <summary>
        /// 1行の最大文字数を取得します。
        /// </summary>
        public const Int32 MaxCharCountPerRow = 76;
        static MailParser()
        {
            MailParser.SetDateTimeFormatString();
            MailParser.InitializeEncodingList();
        }
        private static void SetDateTimeFormatString()
        {
            MailParser._DateTimeFormatString = String.Format("ddd, dd MMM yyyy HH:mm:ss +{0:00}{1:00}"
                , MailParser._TimeZoneOffset.Hours, MailParser._TimeZoneOffset.Minutes);
        }
        private static void InitializeEncodingList()
        {
            var d = _EncodingList;
            d["UTF7"] = Encoding.UTF7;
            d["UTF8"] = Encoding.UTF8;
            d["UTF32"] = Encoding.UTF32;
            d["CP1252"] = Encoding.GetEncoding(1252);
        }
        /// レスポンスが+OKを含むかどうかを取得します。
        /// <summary>
        /// レスポンスが+OKを含むかどうかを取得します。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Boolean IsResponseOk(String text)
        {
			return RegexList.IsResponseOk.IsMatch(text);
        }
        /// Fromの文字列から送信先メールアドレスとして使用可能な文字列を取得します。
        /// <summary>
        /// Fromの文字列から送信先メールアドレスとして使用可能な文字列を取得します。
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public static String MailAddress(String from)
        {
            Regex rg = new Regex("[<]{1}(?<MailAddress>[^>]+)[>]{1}");
            Match m = null;

            m = rg.Match(from);
            if (String.IsNullOrEmpty(m.Value) == true)
            {
                return from;
            }
            return m.Groups["MailAddress"].Value;
        }
        /// 日付データからメールのヘッダーで使用する日付文字列を生成して取得します。
        /// <summary>
        /// 日付データからメールのヘッダーで使用する日付文字列を生成して取得します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static String DateTimeOffsetString(DateTimeOffset dateTime)
        {
            return dateTime.ToString(MailParser.DateTimeFormatString, new CultureInfo("en-US"));
        }
        /// このメソッドは以下の形式をサポートします。
        /// <summary>
        /// このメソッドは以下の形式をサポートします。
        /// Tue, 25 Oct 2011 20:44:24
        /// Tue, 25 Oct 2011 20:44:24 +0900
        /// Tue, 25 Oct 2011 20:44:24 +0900 (JST)
        /// Tue, 25 Oct 2011 20:44:24 F
        /// Tue, 25 Oct 2011 20:44:24 EDT
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(String dateTime)
        {
            DateTimeOffset dtime = DateTimeOffset.Now;
            String dateTime_TimeZone = dateTime;
            TimeSpan ts = TimeSpan.Zero;

            if (DateTimeOffset.TryParse(dateTime, out dtime) == true) { return dtime; }

            var m = RegexList.ThreeLetterTimeZone.Match(dateTime);//(CST)
            if (m.Success == true)
            {
                dateTime_TimeZone = dateTime.Remove(m.Index, m.Length).TrimEnd();//Remove last (CST) string
            }
            // extract date and time
            Int32 index = dateTime_TimeZone.LastIndexOf(" ");
            if (index < 1) throw new FormatException("probably not a date");
            var dateTimePart = dateTime_TimeZone.Substring(0, index - 1);//Tue, 25 Oct 2011 20:44:24
            var timeZonePart = dateTime_TimeZone.Substring(index + 1);//+0600 or GMT (Three letter military timezone)

            if (DateTimeOffset.TryParse(dateTimePart, out dtime) == false) { throw new FormatException(); }

            if (RegexList.TimeZone.IsMatch(timeZonePart))
            {
                var hour = Convert.ToInt32(timeZonePart.Substring(1, 2));
                var minute = Convert.ToInt32(timeZonePart.Substring(3, 2));
                if (timeZonePart.Substring(0, 1) == "-")
                {
                    hour = -hour;
                    minute = -minute;
                }
                ts = new TimeSpan(hour, minute, 0);
                dtime = new DateTimeOffset(dtime.DateTime, ts);
            }
            else
            {
                switch (timeZonePart)
                {
                    case "A": ts = new TimeSpan(1, 0, 0); break;
                    case "B": ts = new TimeSpan(2, 0, 0); break;
                    case "C": ts = new TimeSpan(3, 0, 0); break;
                    case "D": ts = new TimeSpan(4, 0, 0); break;
                    case "E": ts = new TimeSpan(5, 0, 0); break;
                    case "F": ts = new TimeSpan(6, 0, 0); break;
                    case "G": ts = new TimeSpan(7, 0, 0); break;
                    case "H": ts = new TimeSpan(8, 0, 0); break;
                    case "I": ts = new TimeSpan(9, 0, 0); break;
                    case "K": ts = new TimeSpan(10, 0, 0); break;
                    case "L": ts = new TimeSpan(11, 0, 0); break;
                    case "M": ts = new TimeSpan(12, 0, 0); break;
                    case "N": ts = new TimeSpan(-1, 0, 0); break;
                    case "O": ts = new TimeSpan(-2, 0, 0); break;
                    case "P": ts = new TimeSpan(-3, 0, 0); break;
                    case "Q": ts = new TimeSpan(-4, 0, 0); break;
                    case "R": ts = new TimeSpan(-5, 0, 0); break;
                    case "S": ts = new TimeSpan(-6, 0, 0); break;
                    case "T": ts = new TimeSpan(-7, 0, 0); break;
                    case "U": ts = new TimeSpan(-8, 0, 0); break;
                    case "V": ts = new TimeSpan(-9, 0, 0); break;
                    case "W": ts = new TimeSpan(-10, 0, 0); break;
                    case "X": ts = new TimeSpan(-11, 0, 0); break;
                    case "Y": ts = new TimeSpan(-12, 0, 0); break;
                    case "Z":
                    case "UT":
                    case "GMT": break;    // It's UTC
                    case "EST": ts = new TimeSpan(5, 0, 0); break;
                    case "EDT": ts = new TimeSpan(4, 0, 0); break;
                    case "CST": ts = new TimeSpan(6, 0, 0); break;
                    case "CDT": ts = new TimeSpan(5, 0, 0); break;
                    case "MST": ts = new TimeSpan(7, 0, 0); break;
                    case "MDT": ts = new TimeSpan(6, 0, 0); break;
                    case "PST": ts = new TimeSpan(8, 0, 0); break;
                    case "PDT": ts = new TimeSpan(7, 0, 0); break;
                    case "JST": ts = new TimeSpan(9, 0, 0); break;
                    default: throw new FormatException("invalid time zone");
                }
                dtime = new DateTimeOffset(dtime.DateTime, ts);
            }
            return dtime;
        }
        /// 文字列からTransferEncodingの値を取得します。
        /// <summary>
        /// 文字列からTransferEncodingの値を取得します。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TransferEncoding ToTransferEncoding(String text)
        {
            switch (text.ToLower())
            {
                case "7bit": return TransferEncoding.SevenBit;
                case "base64": return TransferEncoding.Base64;
                case "quoted-printable": return TransferEncoding.QuotedPrintable;
            }
            return TransferEncoding.SevenBit;
        }
        /// TransferEncodingから文字列を取得します。
        /// <summary>
        /// TransferEncodingから文字列を取得します。
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static String ToTransferEncoding(TransferEncoding encoding)
        {
            switch (encoding)
            {
                case TransferEncoding.SevenBit: return "7bit";
                case TransferEncoding.Base64: return "Base64";
                case TransferEncoding.QuotedPrintable: return "Quoted-Printable";
            }
            return "7bit";
        }
        /// メールヘッダーの文字列をエンコードします。
        /// <summary>
        /// メールヘッダーの文字列をエンコードします。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encodeType"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static String EncodeToMailHeaderLine(String text, TransferEncoding encodeType, Encoding encoding)
        {
            return MailParser.EncodeToMailHeaderLine(text, encodeType, encoding, MailParser.MaxCharCountPerRow);
        }
        /// メールヘッダーの文字列をエンコードします。
        /// <summary>
        /// メールヘッダーの文字列をエンコードします。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encodeType"></param>
        /// <param name="encoding"></param>
        /// <param name="maxCharCount"></param>
        /// <returns></returns>
        public static String EncodeToMailHeaderLine(String text, TransferEncoding encodeType, Encoding encoding, Int32 maxCharCount)
        {
            Byte[] bb = null;
            StringBuilder sb = new StringBuilder();
            Int32 StartIndex = 0;
            Int32 CharCountPerRow = 0;
            Int32 ByteCount = 0;

            if (maxCharCount > MailParser.MaxCharCountPerRow)
            { throw new ArgumentException("maxCharCount must less than MailParser.MaxCharCountPerRow."); }

            if (String.IsNullOrEmpty(text) == true)
            { return ""; }

            if (MailParser.AsciiCharOnly(text) == true)
            {
                StartIndex = 0;
                CharCountPerRow = maxCharCount;
                for (int i = 0; i < text.Length; i++)
                {
                    sb.Append(text[i]);
                    if (StartIndex == CharCountPerRow)
                    {
                        sb.Append(MailParser.NewLine);
                        StartIndex = 0;
                        CharCountPerRow = MailParser.MaxCharCountPerRow;
                        if (i < text.Length - 1)
                        {
                            sb.Append("\t");
                        }
                    }
                    else
                    {
                        StartIndex += 1;
                    }
                }
                return sb.ToString();
            }
            if (encodeType == TransferEncoding.Base64)
            {
                CharCountPerRow = (Int32)Math.Floor((maxCharCount - (encoding.WebName.Length + 10)) * 0.75);
                for (int i = 0; i < text.Length; i++)
                {
                    ByteCount = encoding.GetByteCount(text.Substring(StartIndex, (i + 1) - StartIndex));
                    if (ByteCount > CharCountPerRow)
                    {
                        bb = encoding.GetBytes(text.Substring(StartIndex, i - StartIndex));
                        sb.AppendFormat("=?{0}?B?{1}?={2}\t", encoding.WebName, Convert.ToBase64String(bb), MailParser.NewLine);
                        StartIndex = i;
                        CharCountPerRow = (Int32)Math.Floor((MailParser.MaxCharCountPerRow - (encoding.WebName.Length + 10)) * 0.75);
                    }
                }
                bb = encoding.GetBytes(text.Substring(StartIndex));
                sb.AppendFormat("=?{0}?B?{1}?=", encoding.WebName, Convert.ToBase64String(bb));

                return sb.ToString();
            }
            else if (encodeType == TransferEncoding.QuotedPrintable)
            {
                CharCountPerRow = (Int32)Math.Floor((maxCharCount - (Double)(encoding.WebName.Length + 10)) / 3);
                for (int i = 0; i < text.Length; i++)
                {
                    ByteCount = encoding.GetByteCount(text.Substring(StartIndex, (i + 1) - StartIndex));
                    if (ByteCount > CharCountPerRow)
                    {
                        bb = encoding.GetBytes(text.Substring(StartIndex, i - StartIndex));
                        sb.AppendFormat("=?{0}?Q?{1}?={2}\t", encoding.WebName, MailParser.ToQuotedPrintableOnHeader(encoding.GetString(bb)), MailParser.NewLine);
                        StartIndex = i;
                        CharCountPerRow = (Int32)Math.Floor((MailParser.MaxCharCountPerRow - (encoding.WebName.Length + 10)) * 0.75);
                    }
                }
                bb = encoding.GetBytes(text.Substring(StartIndex));
                sb.AppendFormat("=?{0}?Q?{1}?=", encoding.WebName, MailParser.ToQuotedPrintable(encoding.GetString(bb)));

                return sb.ToString();
            }
            else
            {
                return text;
            }
        }
        /// メールヘッダーの文字列をRFC2231の仕様に従ってエンコードします。
        /// <summary>
        /// メールヘッダーの文字列をRFC2231の仕様に従ってエンコードします。
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="text"></param>
        /// <param name="encoding"></param>
        /// <param name="maxCharCount"></param>
        /// <returns></returns>
        public static String EncodeToMailHeaderLineByRfc2231(String parameterName, String text, Encoding encoding, Int32 maxCharCount)
        {
            Byte[] bb = null;
            StringBuilder sb = new StringBuilder();
            Int32 StartIndex = 0;
            Int32 CharCountPerRow = 0;
            Int32 RowNo = 0;

            CharCountPerRow = MailParser.MaxCharCountPerRow - parameterName.Length - 3;
            bb = encoding.GetBytes(text);
            for (int i = 0; i < bb.Length; i++)
            {
                //0-9
                if (0x30 <= bb[i] && bb[i] <= 0x39)
                {
                    sb.Append((Char)bb[i]);
                }
                else if (0x41 <= bb[i] && bb[i] <= 0x5a)//A-Z
                {
                    sb.Append((Char)bb[i]);
                }
                else if (0x61 <= bb[i] && bb[i] <= 0x7a)//a-z
                {
                    sb.Append((Char)bb[i]);
                }
                else
                {
                    sb.Append("%");
                    sb.Append(bb[i].ToString("X2"));
                }
            }

            if (sb.Length > CharCountPerRow)
            {
                String s = sb.ToString();
                sb.Length = 0;
                while (true)
                {
                    if (RowNo > 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(parameterName);
                    sb.Append("*");
                    sb.Append(RowNo);
                    sb.Append("*=");
                    if (RowNo == 0)
                    {
                        sb.Append(encoding.WebName);
                        sb.Append("''");
                    }
                    if (StartIndex + CharCountPerRow < s.Length)
                    {
                        sb.Append(s.Substring(StartIndex, CharCountPerRow));
                        sb.Append(MailParser.NewLine);
                    }
                    else
                    {
                        sb.Append(s.Substring(StartIndex, s.Length - StartIndex));
                        sb.Append(";");
                        break;
                    }
                    RowNo += 1;
                    StartIndex += CharCountPerRow;
                }
                return sb.ToString();
            }
            else
            {
                return String.Format("{0}*={1}''{2}", parameterName, encoding.WebName, sb.ToString());
            }
        }
        /// メールヘッダーの文字列をデコードします。
        /// <summary>
        /// メールヘッダーの文字列をデコードします。
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static String DecodeFromMailHeaderLine(String line)
        {
			Regex rg = RegexList.DecodeByRfc2047;
            MatchCollection mc = null;
            Match m = null;
            Byte[] bb = null;
			Encoding en = null;
			Int32 StartIndex = 0;
			StringBuilder sb = new StringBuilder();

            if (String.IsNullOrEmpty(line) == true) { return ""; }

            m = RegexList.DecodeByRfc2231.Match(line);
            mc = rg.Matches(line);
            if (m.Success == true && mc.Count == 0)
            {
                en = MailParser.GetEncoding(m.Groups["Encoding"].Value);
                sb.Append(MailParser.DecodeFromMailHeaderLineByRfc2231(m.Groups["Value"].Value, en));
            }
            else
            {
                for (int i = 0; i < mc.Count; i++)
                {
                    m = mc[i];
                    sb.Append(line.Substring(StartIndex, m.Index - StartIndex));
                    StartIndex = m.Index + m.Length;

                    if (m.Groups.Count < 3)
                    {
                        throw new InvalidDataException();
                    }
                    if (m.Groups["BorQ"].Value.ToUpper() == "B")
                    {
                        bb = Convert.FromBase64String(m.Groups["Value"].Value);
                    }
                    else if (m.Groups["BorQ"].Value.ToUpper() == "Q")
                    {
                        bb = MailParser.FromQuotedPrintableTextOnHeader(m.Groups["Value"].Value);
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }
                    en = MailParser.GetEncoding(m.Groups["Encoding"].Value);
                    sb.Append(en.GetString(bb));
                }
                sb.Append(line.Substring(StartIndex, line.Length - StartIndex));
            }
			return sb.ToString();
        }
        /// メールヘッダーの文字列をRFC2231の仕様に従ってデコードします。
        /// <summary>
        /// メールヘッダーの文字列をRFC2231の仕様に従ってデコードします。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static String DecodeFromMailHeaderLineByRfc2231(String text, Encoding encoding)
        {
            Int32 CurrentIndex = 0;
            Byte[] bb = new Byte[text.Length];
            Int32 ByteArrayIndex = 0;
            Boolean IsDigitChar = false;
            String HexChar = "";

            while (true)
            {
                //%FF形式かどうかチェック
                if (CurrentIndex <= text.Length - 3 &&
                    text[CurrentIndex] == '%')
                {
                    HexChar = text.Substring(CurrentIndex + 1, 2);
                    IsDigitChar = RegexList.HexDecoder1.IsMatch(HexChar);
                }
                else
                {
                    IsDigitChar = false;
                }

                if (IsDigitChar == true)
                {
                    bb[ByteArrayIndex] = Convert.ToByte(HexChar, 16);
                    CurrentIndex += 3;
                }
                else
                {
                    bb[ByteArrayIndex] = (Byte)Char.Parse(text.Substring(CurrentIndex, 1));
                    CurrentIndex += 1;
                }
                ByteArrayIndex += 1;
                if (CurrentIndex >= text.Length) { break; }
            }
            //バイト配列を文字列に変換
            Byte[] bb2 = new Byte[ByteArrayIndex];
            Array.Copy(bb, 0, bb2, 0, ByteArrayIndex);
            return encoding.GetString(bb2);
        }
        /// Content-Typeの解析を行います。
        /// <summary>
        /// Parse content-type.
        /// Content-Typeの解析を行います。
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="line"></param>
        public static void ParseContentType(ContentType contentType, String line)
        {
            Match m = null;

            //name=???;
            foreach (Regex rx in MailParser.RegexList.ContentTypeName)
            {
                m = rx.Match(line);
                if (String.IsNullOrEmpty(m.Groups["Value"].Value) == false)
                {
                    contentType.Name = m.Groups["Value"].Value;
                    break;
                }
            }
            if (String.IsNullOrEmpty(contentType.Name) == true)
            {
                contentType.Name = MailParser.ParseHeaderParameterValue("name", line);
            }

            //boundary
            foreach (Regex rx in MailParser.RegexList.ContentTypeBoundary)
            {
                m = rx.Match(line);
                if (String.IsNullOrEmpty(m.Groups["Value"].Value) == false)
                {
                    contentType.Boundary = m.Groups["Value"].Value;
                    break;
                }
            }
            if (String.IsNullOrEmpty(contentType.Boundary) == true)
            {
                contentType.Boundary = MailParser.ParseHeaderParameterValue("boundary", line);
            }
        }
        /// Content-Dispositionの解析を行います。
        /// <summary>
        /// Parse content-disposision.
        /// Content-Dispositionの解析を行います。
        /// </summary>
        /// <param name="contentDisposition"></param>
        /// <param name="line"></param>
        public static void ParseContentDisposition(ContentDisposition contentDisposition, String line)
        {
            Match m = null;

            //filename=???;
            foreach (Regex rx in MailParser.RegexList.ContentDispositionFileName)
            {
                m = rx.Match(line);
                if (String.IsNullOrEmpty(m.Groups["Value"].Value) == false)
                {
                    contentDisposition.FileName = m.Groups["Value"].Value;
                    return;
                }
            }
            contentDisposition.FileName = MailParser.ParseHeaderParameterValue("filename", line);
        }
        private static String ParseHeaderParameterValue(String parameterName, String line)
        {
            Match m = null;
            Int32 RowNo = 0;
            StringBuilder sb = new StringBuilder();

            List<String> l = new List<String>();
            l.Add(MailParser.RegexList.Rfc2231FormatText);
            l.Add(MailParser.RegexList.Rfc2231FormatText1);

            for (int i = 0; i < l.Count; i++)
            {
                while (true)
                {
                    var rx = new Regex(String.Format(l[i], parameterName, RowNo), RegexOptions.IgnoreCase);
                    m = rx.Match(line);
                    if (String.IsNullOrEmpty(m.Groups["Value"].Value) == true)
                    {
                        break;
                    }
                    else
                    {
                        sb.Append(m.Groups["Value"].Value);
                    }
                    RowNo += 1;
                }
            }
            return sb.ToString();
        }
        /// メール本文の文字列をメールの仕様に従ってエンコードします。
        /// <summary>
        /// メール本文の文字列をメールの仕様に従ってエンコードします。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encodeType"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static String EncodeToMailBody(String text, TransferEncoding encodeType, Encoding encoding)
        {
            Byte[] bb = encoding.GetBytes(text);
			if (encodeType == TransferEncoding.Base64)
            {
				return Convert.ToBase64String(bb);
            }
			else if (encodeType == TransferEncoding.QuotedPrintable)
			{
				return MailParser.ToQuotedPrintable(encoding.GetString(bb));
			}
			return encoding.GetString(bb);
        }
        /// メール本文の文字列を解析し、デコードされたメール本文の文字列を取得します。
        /// <summary>
        /// メール本文の文字列を解析し、デコードされたメール本文の文字列を取得します。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encodeType"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static String DecodeFromMailBody(String text, TransferEncoding encodeType, Encoding encoding)
        {
            Byte[] b = null;

            if (encodeType == TransferEncoding.Base64)
            {
                b = Convert.FromBase64String(text);
			}
			else if (encodeType == TransferEncoding.QuotedPrintable)
			{
				b = MailParser.FromQuotedPrintableText(text);
			}
			else
			{
				b = encoding.GetBytes(text);
			}
			return encoding.GetString(b);
		}
        /// Boundary文字列を生成します。
        /// <summary>
        /// Boundary文字列を生成します。
        /// </summary>
        /// <returns></returns>
        public static string GenerateBoundary()
        {
            String s = String.Format("NextPart_{0}", Guid.NewGuid().ToString("D"));
            return s;
        }
		/// Q-encodeでデコードされた文字列をエンコードして文字列を取得します。
		/// <summary>
		/// Q-encodeでデコードされた文字列をエンコードして文字列を取得します。
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static String ToQuotedPrintableOnHeader(String text)
		{
			StringReader sr = new StringReader(text);
			StringBuilder sb = new StringBuilder();
			Int32 i;

			while ((i = sr.Read()) > 0)
			{
				//ASCII文字の場合
				if (32 < i && i < 127)
				{
					//半角スペース、＝、？、＿
					if (i == 32 ||
						i == 61 ||
						i == 63 ||
						i == 95)
					{
						sb.Append("=");
                        sb.Append(Convert.ToString(i, 16).ToUpper());
					}
					else
					{
						sb.Append(Convert.ToChar(i));
					}
				}
				else
				{
                    sb.Append("=");
                    sb.Append(Convert.ToString(i, 16).ToUpper());
                }
			}
			return sb.ToString();
		}
		/// QuotedPrintableでデコードされた文字列をエンコードして文字列を取得します。
        /// <summary>
        /// QuotedPrintableでデコードされた文字列をエンコードして文字列を取得します。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String ToQuotedPrintable(String text)
        {
            StringReader sr = new StringReader(text);
            StringBuilder sb = new StringBuilder();
            Int32 i;

            while ((i = sr.Read()) > 0)
            {
				//＝の場合
                if (i == 61)
				{
                    sb.Append("=");
                    sb.Append(Convert.ToString(i, 16).ToUpper());
                }
				//ASCII文字、キャリッジリターン、ラインフィード、水平タブ、スペースの場合
				else if ((32 < i && i < 127) ||
                    i == AsciiCharCode.CarriageReturn.GetNumber() ||
                    i == AsciiCharCode.LineFeed.GetNumber() ||
                    i == AsciiCharCode.HorizontalTabulation.GetNumber() ||
                    i == AsciiCharCode.Space.GetNumber())
                {
                    sb.Append(Convert.ToChar(i));
                }
                else
                {
                    sb.Append("=");
                    sb.Append(Convert.ToString(i, 16).ToUpper());
                }
            }
            return sb.ToString();
        }
		/// QuotedPrintableでエンコードされた文字列をデコードして文字列を取得します。
		/// <summary>
		/// QuotedPrintableでエンコードされた文字列をデコードして文字列を取得します。
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static Byte[] FromQuotedPrintableTextOnHeader(String text)
		{
			if (text == null)
			{ throw new ArgumentNullException(); }

			MemoryStream ms = new MemoryStream();
			String line;
			Int32 i = 0;

			using (StringReader sr = new StringReader(text))
			{
				while ((line = sr.ReadLine()) != null)
				{
					// 行の最後の文字が=の場合、行が継続していることを示す。
					if (line.EndsWith("="))
					{
						// =を取り除く
						line = line.Substring(0, line.Length - 1);
					}
					i = 0;
					while (i < line.Length)
					{
						// 現在位置の文字が"="である場合
						if (line.Substring(i, 1) == "=")
						{
							// 16進文字列を取得
							Int32 charLen = i == (line.Length - 2) ? 1 : 2;
							String target = line.Substring(i + 1, charLen);
							ms.WriteByte(Convert.ToByte(target, 16));
							i += 3;
						}
						// Space represented by "_"
						else if (line.Substring(i, 1) == "_")
						{

							ms.WriteByte(Convert.ToByte(' '));
							i = i + 1;
						}
						// 現在位置の文字が"="ではない場合
						else
						{
							String target = line.Substring(i, 1);
							ms.WriteByte(Convert.ToByte(Char.Parse(target)));
							i = i + 1;
						}
					}
				}

			}
			return ms.ToArray();
		}
		/// QuotedPrintableでエンコードされた文字列をデコードして文字列を取得します。
        /// <summary>
        /// QuotedPrintableでエンコードされた文字列をデコードして文字列を取得します。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Byte[] FromQuotedPrintableText(String text)
        {
            if (text == null)
            { throw new ArgumentNullException(); }

            MemoryStream ms = new MemoryStream();
            String line;
            Boolean AddNewLine = false;
            Int32 i = 0;

            using (StringReader sr = new StringReader(text))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    // 行の最後の文字が=の場合、行が継続していることを示す。
                    if (line.EndsWith("="))
                    {
                        // =を取り除く
                        line = line.Substring(0, line.Length - 1);
                        AddNewLine = false;
                    }
                    else
                    {
                        AddNewLine = true;
                    }
                    i = 0;
                    while (i < line.Length)
                    {
                        // 現在位置の文字が"="である場合
                        if (line.Substring(i, 1) == "=")
                        {
                            // 16進文字列を取得
                            Int32 charLen = i == (line.Length - 2) ? 1 : 2; 
                            String target = line.Substring(i + 1, charLen);
                            ms.WriteByte(Convert.ToByte(target, 16));
                            i += 3;
                        }
                        // 現在位置の文字が"="ではない場合
                        else
                        {
                            String target = line.Substring(i, 1);
                            ms.WriteByte(Convert.ToByte(Char.Parse(target)));
                            i = i + 1;
                        }
                    }
                    //改行の追加
                    if (AddNewLine == true)
                    {
                        ms.WriteByte(AsciiCharCode.CarriageReturn.GetNumber());
                        ms.WriteByte(AsciiCharCode.LineFeed.GetNumber());
                    }
                }

            }
            return ms.ToArray();
        }
        /// 文字列をBase64文字列に変更します。
        /// <summary>
        /// 文字列をBase64文字列に変更します。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String ToBase64String(String text)
        {
            Byte[] b = null;
            b = Encoding.ASCII.GetBytes(text);
            return Convert.ToBase64String(b, 0, b.Length);
        }
		/// 文字列をBase64文字列に変更します。
		/// <summary>
		/// 文字列をBase64文字列に変更します。
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static String FromBase64String(String text)
		{
			Byte[] b = null;
			b = Convert.FromBase64String(text);
			return Encoding.ASCII.GetString(b);
		}
		/// MD5ダイジェストに従って文字列を変換します。
        /// <summary>
        /// MD5ダイジェストに従って文字列を変換します。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String ToMd5DigestString(String text)
        {
            Byte[] bb = null;
            StringBuilder sb = new StringBuilder();

            bb = Encoding.Default.GetBytes(text);
            MD5 md5 = new MD5CryptoServiceProvider();
            bb = md5.ComputeHash(bb);
            for (int i = 0; i < bb.Length; i++)
            {
                sb.Append(bb[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }
		/// Cram-MD5に従って文字列を変換します。
        /// <summary>
        /// Cram-MD5に従って文字列を変換します。
        /// </summary>
        /// <param name="challenge"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static String ToCramMd5String(String challenge, String userName, String password)
        {
            StringBuilder sb = new StringBuilder();
            Byte[] bb = null;
            HMACMD5 md5 = new HMACMD5(Encoding.ASCII.GetBytes(password));
            // Base64デコードしたチャレンジコードに対してパスワードをキーとしたHMAC-MD5ハッシュ値を計算する
            bb = md5.ComputeHash(Convert.FromBase64String(challenge));
            // 計算したHMAC-MD5ハッシュ値のbyte[]を16進表記の文字列に変換する
            for (int i = 0; i < bb.Length; i++)
            {
                sb.Append(bb[i].ToString("x02"));
            }
            // ユーザ名と計算したHMAC-MD5ハッシュ値をBase64エンコードしてレスポンスとして返す
            bb = Encoding.ASCII.GetBytes(String.Format("{0} {1}", userName, sb.ToString()));
            return Convert.ToBase64String(bb);
        }
        /// 指定した文字がASCII文字列のみで構成されているかどうかを示す値を取得します。
        /// <summary>
        /// 指定した文字がASCII文字列のみで構成されているかどうかを示す値を取得します。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Boolean AsciiCharOnly(String text)
        {
			return !RegexList.AsciiCharOnly.IsMatch(text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Encoding GetEncoding(String encoding)
        {
            return GetEncoding(encoding, Encoding.ASCII);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="defaultEncoding"></param>
        /// <returns></returns>
        public static Encoding GetEncoding(String encoding, Encoding defaultEncoding)
		{
            var d = _EncodingList;
            if (d.ContainsKey(encoding.ToUpper()) == true)
            {
                return d[encoding.ToUpper()];
            }
            try
            {
                return Encoding.GetEncoding(encoding);
            }
            catch { return defaultEncoding; }
		}
    }
}
