using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using HigLabo.Net.Mail;

namespace HigLabo.Net.Smtp
{
    /// Represent smtp message.
    /// <summary>
    /// Represent smtp message.
    /// </summary>
    public class SmtpMessage : InternetTextMessage
    {
        private List<SmtpContent> _Contents;
        private List<String> _EncodeHeaderKeys = new List<String>();
        private List<MailAddress> _To = new List<MailAddress>();
        private List<MailAddress> _Cc = new List<MailAddress>();
        private List<MailAddress> _Bcc = new List<MailAddress>();
        private MailPriority _MailPriority = MailPriority.Normal;
        private String _BodyText = "";
        private Encoding _HeaderEncoding = Encoding.ASCII;
        private TransferEncoding _HeaderTransferEncoding = TransferEncoding.SevenBit;
        /// 宛先のメールアドレスのリストを取得します。
        /// <summary>
        /// 宛先のメールアドレスのリストを取得します。
        /// </summary>
        public List<MailAddress> To
        {
            get { return this._To; }
        }
        /// CCのメールアドレスのリストを取得します。
        /// <summary>
        /// CCのメールアドレスのリストを取得します。
        /// </summary>
        public List<MailAddress> Cc
        {
            get { return this._Cc; }
        }
        /// BCCのメールアドレスのリストを取得します。
        /// <summary>
        /// BCCのメールアドレスのリストを取得します。
        /// </summary>
        public List<MailAddress> Bcc
        {
            get { return this._Bcc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public MailPriority Priority
        {
            get { return _MailPriority; }
            set { _MailPriority = value; }
        }
        /// HeaderのEncodingを取得または設定します。
        /// <summary>
        /// HeaderのEncodingを取得または設定します。
        /// </summary>
        public Encoding HeaderEncoding
        {
            get { return this._HeaderEncoding; }
            set { this._HeaderEncoding = value; }
        }
        /// HeaderのTransferEncodingを取得または設定します。
        /// <summary>
        /// HeaderのTransferEncodingを取得または設定します。
        /// </summary>
        public TransferEncoding HeaderTransferEncoding
        {
            get { return this._HeaderTransferEncoding; }
            set { this._HeaderTransferEncoding = value; }
        }
        /// ボディ部分のテキスト文字列を取得または設定します。
        /// <summary>
        /// ボディ部分のテキスト文字列を取得または設定します。
        /// </summary>
        public String BodyText
        {
            get { return this._BodyText; }
            set { this._BodyText = value; }
        }
        /// HTML形式のメールかどうかを示す値を取得または設定します。
        /// <summary>
        /// HTML形式のメールかどうかを示す値を取得または設定します。
        /// </summary>
        public new Boolean IsHtml
        {
            get { return base.IsHtml; }
            set { this.ContentType.Value = "text/html"; }
        }
        /// SmtpContentのコレクションを取得します。
        /// <summary>
        /// SmtpContentのコレクションを取得します。
        /// </summary>
        public List<SmtpContent> Contents
        {
            get { return this._Contents; }
        }
		/// <summary>
		/// 
		/// </summary>
        public SmtpMessage()
        {
            this.Initialize();
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mailFrom"></param>
		/// <param name="to"></param>
		/// <param name="cc"></param>
		/// <param name="subject"></param>
		/// <param name="bodyText"></param>
        public SmtpMessage(String mailFrom, String to, String cc, String subject, String bodyText)
        {
            this.Initialize();
            this.From = mailFrom;
            if (String.IsNullOrEmpty(to) == false)
            {
                this.To.Add(new MailAddress(to));
            }
            if (String.IsNullOrEmpty(cc) == false)
            {
                this.Cc.Add(new MailAddress(cc));
            }
            this.Subject = subject;
            this.BodyText = bodyText;
        }
        /// 初期化処理を行います。
        /// <summary>
        /// 初期化処理を行います。
        /// </summary>
        private void Initialize()
        {
            this._Contents = new List<SmtpContent>();

            this["MIME-Version"] = "1.0";
            
            if (CultureInfo.CurrentCulture.Name.StartsWith("ja") == true)
            {
                this.HeaderEncoding = Encoding.GetEncoding("iso-2022-jp");
                this.HeaderTransferEncoding = TransferEncoding.Base64;
                this.ContentEncoding = Encoding.GetEncoding("iso-2022-jp");
                this.ContentTransferEncoding = TransferEncoding.Base64;
            }
            this._EncodeHeaderKeys.Add("subject");
        }
        /// 実際に送信される文字列のデータを取得します。
        /// <summary>
        /// 実際に送信される文字列のデータを取得します。
        /// </summary>
        /// <returns></returns>
        public String GetDataText()
        {
            StringBuilder sb = new StringBuilder(1024);
            CultureInfo ci = CultureInfo.CurrentCulture;
            Field f = null;
            SmtpContent ct = null;
            String line = "";
            String bodyText = "";

            //ContentTransferEncoding
            f = InternetTextMessage.Field.FindField(this.Header, "Content-Transfer-Encoding");
            if (f == null)
            {
                f = new Field("Content-Transfer-Encoding", MailParser.ToTransferEncoding(this.ContentTransferEncoding));
                this.Header.Add(f);
            }
            else
            {
                f.Value = MailParser.ToTransferEncoding(this.ContentTransferEncoding);
            }

            for (int i = 0; i < this.Header.Count; i++)
            {
                f = this.Header[i];
                if (this._EncodeHeaderKeys.Contains(f.Key.ToLower()) == true)
                {
                    sb.AppendFormat("{0}: {1}{2}", f.Key
                        , MailParser.EncodeToMailHeaderLine(f.Value, this.HeaderTransferEncoding, this.HeaderEncoding
                        , MailParser.MaxCharCountPerRow - f.Key.Length - 2), MailParser.NewLine);
                }
                else if(f.Key.ToLower() != "content-type")
                {
                    sb.AppendFormat("{0}: {1}{2}", f.Key, f.Value, MailParser.NewLine);
                }
            }
            //Headerに設定されていない場合のみセットする
            //Priority
            f = Field.FindField(this.Header, "X-Priority");
            if (f == null)
            {
                sb.AppendFormat("X-Priority: {0}{1}", ((byte)this.Priority).ToString(), MailParser.NewLine);
            }
            //TO
            f = Field.FindField(this.Header, "To");
            if (f == null)
            {
                line = this.CreateMailAddressListText(this._To);
                if (String.IsNullOrEmpty(line) == false)
                {
                    sb.Append("To: ");
                    sb.Append(line);
                }
            }
            //CC
            f = Field.FindField(this.Header, "Cc");
            if (f == null)
            {
                line = this.CreateMailAddressListText(this._Cc);
                if (String.IsNullOrEmpty(line) == false)
                {
                    sb.Append("Cc: ");
                    sb.Append(line);
                }
            }

            if (this.Contents.Count > 0)
            {
                if (String.IsNullOrEmpty(this.MultiPartBoundary) == true)
                {
                    this.MultiPartBoundary = MailParser.GenerateBoundary();
                }
                //Multipartboundary
                sb.AppendFormat("Content-Type: multipart/mixed; boundary=\"{0}\"", this.MultiPartBoundary);
                sb.Append(MailParser.NewLine);
                sb.Append(MailParser.NewLine);

                //This is multi-part message in MIME format.
                sb.Append(MailParser.ThisIsMultiPartMessageInMimeFormat);
                sb.Append(MailParser.NewLine);
                //Add BodyText Content
                if (String.IsNullOrEmpty(this.BodyText) == false)
                {
                    ct = new SmtpContent();
                    if (this.IsHtml == true)
                    {
                        ct.LoadHtml(this.BodyText);
                    }
                    else
                    {
                        ct.LoadText(this.BodyText);
                    }
                    ct.ContentEncoding = this.ContentEncoding;
                    ct.ContentTransferEncoding = this.ContentTransferEncoding;
                    if (this.Contents.Exists(delegate(SmtpContent c) { return c.IsBody; }) == false)
                    {
                        sb.Append("--");
                        sb.Append(this.MultiPartBoundary);
                        sb.Append(MailParser.NewLine);
                        sb.Append(ct.Data);
                        sb.Append(MailParser.NewLine);
                    }
                }
                for (int i = 0; i < this._Contents.Count; i++)
                {
                    //Skip empty SmtpContent instance
                    if (String.IsNullOrEmpty(this._Contents[i].Data) == true) { continue; }
                    sb.Append("--");
                    sb.Append(this.MultiPartBoundary);
                    sb.Append(MailParser.NewLine);
                    sb.Append(this.Contents[i].Data);
                    sb.Append(MailParser.NewLine);
                }
                sb.Append(MailParser.NewLine);
                sb.AppendFormat("--{0}--", this.MultiPartBoundary);
            }
            else
            {
                sb.AppendFormat("Content-Type: {0}; charset=\"{1}\"", this.ContentType.Value, this.ContentEncoding.WebName);
                sb.Append(MailParser.NewLine);
                sb.Append(MailParser.NewLine);
                bodyText = MailParser.EncodeToMailBody(this.BodyText, this.ContentTransferEncoding, this.ContentEncoding);
				if (this.ContentTransferEncoding == TransferEncoding.SevenBit)
				{
                    sb.Append(bodyText);
				}
				else
				{
                    for (int i = 0; i < bodyText.Length; i++)
					{
						if (i > 0 && i % 76 == 0)
						{
							sb.Append(MailParser.NewLine);
						}
                        //Is current index is first char of line
                        if (i == 0 || (i > 2 && bodyText[i - 2] == '\r' && bodyText[i - 1] == '\n'))
                        {
                            if (bodyText[i] == '.')
                            {
                                sb.Append(".");
                            }
                        }
                        sb.Append(bodyText[i]);
					}
				}
            }
            sb.Append(MailParser.NewLine);
            sb.Append(MailParser.NewLine);
            sb.Append(".");
            sb.Append(MailParser.NewLine);

            return sb.ToString();
        }
        /// ユーザー名とメールアドレスをFromにセットします。
        /// <summary>
        /// ユーザー名とメールアドレスをFromにセットします。
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="mailAddress"></param>
        public void SetFromMailAddress(String userName, String mailAddress)
        {
            this.From = SmtpMessage.CreateFromMailAddress(userName, mailAddress); ;
        }
        /// ユーザー名とメールアドレスを示す文字列を生成します。
        /// <summary>
        /// ユーザー名とメールアドレスを示す文字列を生成します。
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="mailAddress"></param>
        public static String CreateFromMailAddress(String userName, String mailAddress)
        {
            return String.Format("\"{0}\" <{1}>", userName, mailAddress);
        }
        /// メールアドレスの一覧データからメールアドレスの文字列を生成します。
        /// <summary>
        /// メールアドレスの一覧データからメールアドレスの文字列を生成します。
        /// </summary>
        /// <param name="mailAddressList"></param>
        /// <returns></returns>
        private String CreateMailAddressListText(List<MailAddress> mailAddressList)
        {
            StringBuilder sb = new StringBuilder();
            List<MailAddress> l = mailAddressList;
            String s = "";

            for (int i = 0; i < l.Count; i++)
            {
                sb.AppendFormat("{0}{1}", s, l[i].ToEncodeString().Trim());
                sb.Append(MailParser.NewLine);

                s = "\t, ";
            }
            return sb.ToString();
        }
    }
}
