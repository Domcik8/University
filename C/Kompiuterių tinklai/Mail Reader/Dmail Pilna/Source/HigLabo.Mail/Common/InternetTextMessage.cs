using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using HigLabo.Net.Mail;

namespace HigLabo.Net
{
    /// Represent message defined RFC822,RFC2045-2049 as class.
    /// <summary>
    /// Represent message defined RFC822,RFC2045-2049 as class.
    /// </summary>
	public class InternetTextMessage 
	{
        private class RegexList
        {
            public static readonly ICollection<Regex> ContentEncodingCharset = new List<Regex>();
            public static readonly Regex HeaderParse = new Regex("^(?<key>[^:]*):[\\s]*(?<value>.*)");
            public static readonly Regex HeaderParse1 = new Regex("(?<value>[^;]*)[;]*");
            public static readonly Regex Attachment = new Regex("^attachment.*$");
            public static readonly Regex Image = new Regex("^image/.*$");
        }
        private List<Field> _Header;
		/// ヘッダーの文字列データ（US-ASCII）
		/// <summary>
		/// Field for header data (encoded by US-ASCII)
		/// ヘッダーの文字列データ（US-ASCII）
		/// </summary>
		private String _HeaderData = "";
		/// 本文の文字列データ（US-ASCII）
        /// <summary>
        /// Field for body data (encoded by US-ASCII)
        /// 本文の文字列データ（US-ASCII）
        /// </summary>
        private String _BodyData = "";
        private String _Data;
        private Boolean _DecodeHeaderText = true;
        private Encoding _ContentEncoding = Encoding.Default;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public String this[String key]
		{
			get 
			{
				Field f = InternetTextMessage.Field.FindField(this._Header, key);
				if (f == null)
				{
					return "";
				}
				else
				{
                    if (this._DecodeHeaderText == true)
                    {
                        return MailParser.DecodeFromMailHeaderLine(f.Value);
                    }
                    else
                    {
                        return f.Value;
                    }
				}
			}
			set 
			{
				Field f = InternetTextMessage.Field.FindField(this._Header, key);
				if (f == null)
				{
					f = new Field(key, value);
					this._Header.Add(f);
				}
				else
				{
					f.Value = value;
				}
			}
		}
        /// Fromを取得または設定します。
        /// <summary>
        /// Get from value.
        /// Fromを取得または設定します。
        /// </summary>
        public String From
        {
            get { return this["From"]; }
            set { this["From"] = value; }
        }
        /// Reply-Toを取得または設定します。
        /// <summary>
        /// Reply-Toを取得または設定します。
        /// </summary>
        public String ReplyTo
        {
            get { return this["Reply-To"]; }
            set { this["Reply-To"] = value; }
        }
        /// In-Reply-Toを取得または設定します。
        /// <summary>
        /// In-Reply-Toを取得または設定します。
        /// </summary>
        public String InReplyTo
        {
            get { return this["In-Reply-To"]; }
            set { this["In-Reply-To"] = value; }
        }
        /// 件名を取得または設定します。
        /// <summary>
        /// 件名を取得または設定します。
        /// </summary>
        public String Subject
        {
            get { return this["Subject"]; }
            set { this["Subject"] = value; }
        }
        /// Dateを取得または設定します。
        /// <summary>
        /// Dateを取得または設定します。
        /// </summary>
        public DateTimeOffset Date
        {
            get { return MailParser.ToDateTimeOffset(this["Date"]); }
            set { this["Date"] = MailParser.DateTimeOffsetString(value); }
        }
        /// MessageIDを取得または設定します。
        /// <summary>
        /// MessageIDを取得または設定します。
        /// </summary>
        public String MessageID
        {
            get { return this["Message-ID"]; }
            set { this["Message-ID"] = value; }
        }
        /// Referencesを取得または設定します。
        /// <summary>
        /// Referencesを取得または設定します。
        /// </summary>
        public String References
        {
            get { return this["References"]; }
            set { this["References"] = value; }
        }
        /// ContentTypeを取得します。
        /// <summary>
        /// ContentTypeを取得します。
        /// </summary>
        public ContentType ContentType
        {
            get 
            {
                ContentType ff = null;
                ff = InternetTextMessage.Field.FindField(this._Header, "content-type") as ContentType;
                if (ff == null)
                {
                    ff = new ContentType("text/plain");
                    this._Header.Add(ff);
                }
                return ff;
            }
        }
        /// Encodingを取得します。
        /// <summary>
        /// Encodingを取得します。
        /// </summary>
        public Encoding ContentEncoding
        {
            get { return this._ContentEncoding; }
            set { this._ContentEncoding = value; }
        }
        /// ContentDispositionを取得します。
        /// <summary>
        /// ContentDispositionを取得します。
        /// </summary>
        public ContentDisposition ContentDisposition
        {
            get
            {
                ContentDisposition ff = null;
                ff = InternetTextMessage.Field.FindField(this._Header, "content-disposition") as ContentDisposition;
                if (ff == null)
                {
                    ff = new ContentDisposition("");
                    this._Header.Add(ff);
                }
                return ff;
            }
        }
        /// MultiPartBoundaryの文字列を取得または設定します。
        /// <summary>
        /// MultiPartBoundaryの文字列を取得または設定します。
        /// </summary>
        public String MultiPartBoundary
        {
            get { return this.ContentType.Boundary; }
            set { this.ContentType.Boundary = value; }
        }
        /// Body部がMIMEで構成されているかどうかを示す値を取得します。
        /// <summary>
        /// Body部がMIMEで構成されているかどうかを示す値を取得します。
        /// </summary>
        public Boolean IsMultiPart
        {
            get { return Regex.IsMatch(this.ContentType.Value, ".*multipart/.*", RegexOptions.IgnoreCase); }
        }
        /// このインスタンスが本文部分のデータを表す場合、Trueを返します。
        /// <summary>
        /// このインスタンスが本文部分のデータを表す場合、Trueを返します。
        /// </summary>
        public Boolean IsBody
        {
            get
            {
                return (this.ContentType.Value.StartsWith("text/", StringComparison.OrdinalIgnoreCase) == true);
            }
        }
        /// このインスタンスがテキスト形式のデータを表す場合、Trueを返します。
        /// <summary>
        /// このインスタンスがテキスト形式のデータを表す場合、Trueを返します。
        /// </summary>
        public Boolean IsText
        {
            get
            {
                return (this.ContentType.Value.StartsWith("text/", StringComparison.OrdinalIgnoreCase) == true) ||
                    (this.ContentType.Value.Equals("application/xml", StringComparison.OrdinalIgnoreCase) == true) ||
                    (this.ContentType.Value.Equals("application/json", StringComparison.OrdinalIgnoreCase) == true);
            }
        }
        /// このインスタンスがHTML形式のテキストデータを表す場合、Trueを返します。
        /// <summary>
        /// このインスタンスがHTML形式のテキストデータを表す場合、Trueを返します。
        /// </summary>
        public Boolean IsHtml
        {
            get
            {
                return (this.ContentType.Value.StartsWith("text/html", StringComparison.OrdinalIgnoreCase) == true);
            }
        }
        /// このインスタンスが添付ファイルデータの場合、Trueを返します。
        /// <summary>
        /// このインスタンスが添付ファイルデータの場合、Trueを返します。
        /// </summary>
        public Boolean IsAttachment
        {
            get
            {
                if (String.IsNullOrEmpty(this.ContentDisposition.Value) == false)
                {
                    return RegexList.Attachment.Match(this.ContentDisposition.Value).Success ||
                        RegexList.Image.Match(this.ContentType.Value).Success;
                }
                return false;
            }
        }
        /// ContentDispositionを取得または設定します。
        /// <summary>
        /// ContentDispositionを取得または設定します。
        /// </summary>
        public String ContentDescription
        {
            get { return this["Content-Description"]; }
            set { this["Content-Description"] = value; }
        }
        /// ContentTransferEncodingの値を取得または設定します。
        /// <summary>
        /// ContentTransferEncodingの値を取得または設定します。
        /// </summary>
        public TransferEncoding ContentTransferEncoding
        {
            get { return MailParser.ToTransferEncoding(this["Content-Transfer-Encoding"]); }
            set { this["Content-Transfer-Encoding"] = MailParser.ToTransferEncoding(value); }
        }
        /// CharSetの値を取得します。
        /// <summary>
        /// CharSetの値を取得します。
        /// </summary>
        public String CharSet
        {
            get { return this.ContentEncoding.HeaderName; }
        }
        /// ヘッダーのコレクションを取得します。
        /// <summary>
        /// ヘッダーのコレクションを取得します。
        /// </summary>
        public List<Field> Header
        {
            get { return this._Header; }
        }
        /// ヘッダー部分のフィールドの値をデコードするかどうかを示す値を取得します。
        /// <summary>
        /// ヘッダー部分のフィールドの値をデコードするかどうかを示す値を取得します。
        /// </summary>
        public Boolean DecodeHeaderText
        {
            get { return this._DecodeHeaderText; }
            set { this._DecodeHeaderText = value; }
        }
		/// Header部分のデータを取得します。
		/// <summary>
		/// Header部分のデータを取得します。
		/// </summary>
		protected String HeaderData
		{
			get { return this._HeaderData; }
			set { this._HeaderData = value; }
		}
		/// Body部分のデータを取得します。
        /// <summary>
        /// Body部分のデータを取得します。
        /// </summary>
        protected String BodyData
        {
            get { return this._BodyData; }
            set { this._BodyData = value; }
        }
        /// このインスタンスを生成するときに使用した文字列データを取得します。
        /// <summary>
        /// Get text data used to create this instance.
        /// このインスタンスを生成するときに使用した文字列データを取得します。
        /// </summary>
        public String Data
        {
            get { return this._Data; }
            protected set { this._Data = value; }
        }
        static InternetTextMessage()
		{
			InternetTextMessage.Initialize();
		}
		private static void Initialize()
		{
			RegexList.ContentEncodingCharset.Add(new Regex(".*charset ?= ?[\"]*(?<Value>[^\";]*)[;\n\r]", RegexOptions.IgnoreCase));
			RegexList.ContentEncodingCharset.Add(new Regex(".*charset ?= ?[\"]*(?<Value>[^\"]*).*", RegexOptions.IgnoreCase));
		}
		/// <summary>
		/// 
		/// </summary>
        public InternetTextMessage()
        {
            this.Initialize("");
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
        public InternetTextMessage(String text)
        {
            this.Initialize(text);
        }
        /// 初期化処理を行います。
        /// <summary>
        /// 初期化処理を行います。
        /// </summary>
        /// <param name="text"></param>
        private void Initialize(String text)
		{
			this._Header = new List<Field>();
            
            this.Date = DateTime.Now;
            this._Header.Add(new Field("From", ""));
            this._Header.Add(new Field("Subject", ""));
            this.ContentType.Value = "text/plain";
            this.ContentTransferEncoding = TransferEncoding.SevenBit;
            this.ContentDisposition.Value = "inline";
            this.SetDefaultContentEncoding();

            this.Parse(text);
        }
        /// 既定のContent-Encodingの値をセットします。
        /// <summary>
        /// 既定のContent-Encodingの値をセットします。
        /// </summary>
        private void SetDefaultContentEncoding()
        {
            if (System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ja")
            {
                this.ContentEncoding = Encoding.GetEncoding("iso-2022-jp");
            }
        }
        /// テキストを解析します。
        /// <summary>
        /// テキストを解析します。
        /// </summary>
        /// <param name="text"></param>
        protected void Parse(String text)
        {
            StringReader sr = null;
            //複数行フィールドの場合の2行目以降の行のリスト
            List<String> l = new List<String>();
            String CurrentLine = "";
            String FirstLine = "";
            Boolean IsConcating = false;
            Int32 c = 0;
			StringBuilder sb = new StringBuilder(512);

            using (sr = new StringReader(text))
            {
                while (true)
                {
                    CurrentLine = sr.ReadLine();
					sb.Append(CurrentLine);
					sb.Append(MailParser.NewLine);
					if (IsConcating == true)
                    {
                        l.Add(CurrentLine);
                    }
                    else
                    {
                        l.Clear();
                        FirstLine = CurrentLine;
                        //ヘッダーとボディ部の区切り行かどうかチェック
                        if (FirstLine == "")
                        {
							_HeaderData = sb.ToString();
							//以降のデータはBody部のデータ
							sb = new StringBuilder(text.Length - _HeaderData.Length);
                            while (true)
                            {
                                CurrentLine = sr.ReadLine();
                                if (CurrentLine == null) { break; }
                                if (CurrentLine == ".") { break; }
                                if (CurrentLine.StartsWith("..") == true)
                                { CurrentLine = CurrentLine.Substring(1, CurrentLine.Length - 1); }
                                sb.Append(CurrentLine);
                                if (sr.Peek() == -1) { break; }
                                //Not append new line char after last line
                                //最終行には改行コードを追加しない
                                sb.Append(MailParser.NewLine);
                            }
                            this.BodyData = sb.ToString();
                            return;
                        }
                    }
                    //次の行の先頭の文字を取得
                    c = sr.Peek();
                    //次の行がなかったら終了
					if (c == -1)
					{
						_HeaderData = sb.ToString();
						break;
					}
                    //次の行の先頭の文字がタブ文字または半角スペースの場合、複数行のフィールドとして連結する
                    if (c == 9 || c == 32)
                    {
                        IsConcating = true;
                        continue;
                    }
                    else
                    {
                        IsConcating = false;
                        this.ParseHeaderField(FirstLine, l);
                        l.Clear();
                        IsConcating = false;
                    }
                }
            }
        }
        /// 行の文字列を解析し、フィールドのインスタンスを生成します。
        /// <summary>
        /// 行の文字列を解析し、フィールドのインスタンスを生成します。
        /// </summary>
        /// <param name="line"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        private void ParseHeaderField(String line, List<String> lines)
        {
            Match m = RegexList.HeaderParse.Match(line);
            Match m1 = null;
            Regex rx = RegexList.HeaderParse1;
            Field f = null;
            List<String> l = lines;
			Int32 size = 0;
			for (int i = 0; i < lines.Count; i++)
			{
				size += line.Length;
			}
            StringBuilder sb = new StringBuilder(size);

            if (String.IsNullOrEmpty(m.Groups["key"].Value) == false)
            {
                m1 = rx.Match(m.Groups["value"].Value);
                if (m.Groups["key"].Value.ToLower() == "content-type" ||
                    m.Groups["key"].Value.ToLower() == "content-disposition")
                {
                    sb.Append(line);
                    for (int i = 0; i < l.Count; i++)
                    {
                        sb.Append(l[i].TrimStart('\t'));
                    }
                    this.ParseContentEncoding(sb.ToString());

                    if (m.Groups["key"].Value.ToLower() == "content-type")
                    {
                        MailParser.ParseContentType(this.ContentType, sb.ToString());
                        this.ContentType.Value = m1.Groups["value"].Value;
                    }
                    else if (m.Groups["key"].Value.ToLower() == "content-disposition")
                    {
                        MailParser.ParseContentDisposition(this.ContentDisposition, sb.ToString());
                        this.ContentDisposition.Value = m1.Groups["value"].Value;
                    }
                }
                else
                {
                    f = Field.FindField(this._Header, m.Groups["key"].Value);
                    if (f == null)
                    {
                        f = new Field(m.Groups["key"].Value, m.Groups["value"].Value);
                        this.Header.Add(f);
                    }
                    else
                    {
                        f.Value = m.Groups["value"].Value;
                    }
                    for (int i = 0; i < l.Count; i++)
                    {
                        f.Value += l[i].TrimStart('\t');
                    }
                }
            }
        }
        /// Content-Encodingの解析を行います。
        /// <summary>
        /// Content-Encodingの解析を行います。
        /// </summary>
        /// <param name="line"></param>
        private void ParseContentEncoding(String line)
        {
            Match m = null;

            //charset=???;
            foreach (Regex rx in RegexList.ContentEncodingCharset)
            {
                m = rx.Match(line);
                if (String.IsNullOrEmpty(m.Groups["Value"].Value) == false)
                {
                    this._ContentEncoding = MailParser.GetEncoding(m.Groups["Value"].Value, this.ContentEncoding);
                    break;
                }
            }
        }
        /// バイナリデータをデコードして指定した物理パスに出力します。
        /// <summary>
        /// Decode binary data and output as file to specify file path.
        /// バイナリデータをデコードして指定した物理パスに出力します。
        /// </summary>
        /// <param name="filePath"></param>
        public void DecodeData(String filePath)
        {
            using (var stm = new FileStream(filePath, FileMode.Create))
            {
                this.DecodeData(stm, true);
            }
        }
        /// バイナリデータをデコードして指定したストリームに出力します。
        /// <summary>
        /// Decode binary data and output to specify stream.
        /// バイナリデータをデコードして指定したストリームに出力します。
        /// </summary>
        /// <param name="stream">書込み先のストリームオブジェクトです。</param>
        /// <param name="isClose">ストリームに書き込んだあとにストリームを閉じるかどうかを示す値を設定します。</param>
        public void DecodeData(Stream stream, Boolean isClose)
        {
            Byte[] bb = null;
            BinaryWriter sw = null;

            if (String.IsNullOrEmpty(this.ContentDisposition.Value) == true)
            { return; }

            if (this.ContentTransferEncoding == TransferEncoding.Base64)
            {
                bb = Convert.FromBase64String(this.BodyData.Replace("\n", "").Replace("\r", ""));
            }
            else if (this.ContentTransferEncoding == TransferEncoding.QuotedPrintable)
            {
                bb = MailParser.FromQuotedPrintableText(this.BodyData);
            }
            else if (this.ContentTransferEncoding == TransferEncoding.SevenBit)
            {
                bb = Encoding.ASCII.GetBytes(this.BodyData);
            }
            try
            {
                sw = new BinaryWriter(stream);
                sw.Write(bb);
                sw.Flush();
            }
            finally
            {
                if (isClose == true)
                {
                    sw.Close();
                }
            }
        }
        /// キーと値のセットで構成されるフィールドを表すクラスです。
        /// <summary>
        /// キーと値のセットで構成されるフィールドを表すクラスです。
        /// RFC822で定義されます。
        /// </summary>
        public class Field
        {
            private String _Key = "";
            private String _Value = "";
			/// <summary>
			/// 
			/// </summary>
            public String Key
            {
                get { return this._Key ?? ""; }
                set { this._Key = value; }
            }
			/// <summary>
			/// 
			/// </summary>
            public String Value
            {
                get { return this._Value ?? ""; }
                set { this._Value = value; }
            }
			/// <summary>
			/// 
			/// </summary>
			/// <param name="key"></param>
			/// <param name="value"></param>
            public Field(String key, String value)
            {
                this._Key = key;
                this._Value = value;
            }
			/// <summary>
			/// 
			/// </summary>
			/// <param name="fields"></param>
			/// <param name="key"></param>
			/// <returns></returns>
            public static Field FindField(List<Field> fields, String key)
            {
                List<Field> l = fields.FindAll(delegate(Field f) { return String.Equals(f.Key, key, StringComparison.InvariantCultureIgnoreCase); });
                if (l.Count > 0)
                {
                    return l[l.Count - 1];
                }
                return null;
            }
			/// <summary>
			/// 
			/// </summary>
			/// <returns></returns>
            public override string ToString()
            {
                return String.Format("{0}: {1}", this.Key, this.Value);
            }
        }
	}
}
