using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using HigLabo.Net.Smtp;

namespace HigLabo.Net.Mail
{
    /// Represent mailaddress when sending by smtp.
    /// <summary>
    /// Represent mailaddress when sending by smtp.
    /// </summary>
    public class MailAddress
    {
		private class RegexList
		{
			public static Regex DisplayName_MailAddress = new Regex("(?<DisplayName>.*)<(?<MailAddress>[^>]*)>");
			public static Regex MailAddressWithBracket = new Regex("<(?<MailAddress>[^>]*)>");
		}
        private String _Value = "";
        private String _DisplayName = "";
		private String _UserName = "";
		private String _DomainName = "";
        private Boolean _IsDoubleQuote = false;
        private Encoding _Encoding = Encoding.ASCII;
        private TransferEncoding _TransferEncoding = TransferEncoding.Base64;
        /// メールアドレスの値を取得または設定します。
        /// <summary>
        /// Get or set mailaddress value.
        /// メールアドレスの値を取得または設定します。
        /// </summary>
        public String Value
        {
            get { return this._Value; }
            set { this._Value = value; }
        }
        /// 表示名を取得または設定します。
        /// <summary>
        /// 表示名を取得または設定します。
        /// </summary>
        public String DisplayName
        {
            get { return this._DisplayName; }
            set { this._DisplayName = value; }
        }
		/// ユーザー名を取得または設定します。
		/// <summary>
		/// ユーザー名を取得または設定します。
		/// </summary>
		public String UserName
		{
			get { return this._UserName; }
			set { this._UserName = value; }
		}
		/// ドメイン名を取得または設定します。
		/// <summary>
		/// ドメイン名を取得または設定します。
		/// </summary>
		public String DomainName
		{
			get { return this._DomainName; }
			set { this._DomainName = value; }
		}
		/// 表示名をダブルコーテーションで囲うどうかを示す値を取得または設定します。
        /// <summary>
        /// 表示名をダブルコーテーションで囲うどうかを示す値を取得または設定します。
        /// </summary>
        public Boolean IsDoubleQuote
        {
            get { return this._IsDoubleQuote; }
            set { this._IsDoubleQuote = value; }
        }
        /// 表示名のエンコードに使われるEncodingを取得または設定します。
        /// <summary>
        /// 表示名のエンコードに使われるEncodingを取得または設定します。
        /// </summary>
        public Encoding Encoding
        {
            get { return this._Encoding; }
            set { this._Encoding = value; }
        }
        /// 表示名のエンコードに使われるTransferEncodingを取得または設定します。
        /// <summary>
        /// 表示名のエンコードに使われるTransferEncodingを取得または設定します。
        /// </summary>
        public TransferEncoding TransferEncoding
        {
            get { return this._TransferEncoding; }
            set { this._TransferEncoding = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mailAddress"></param>
        public MailAddress(String mailAddress)
        {
            if (String.IsNullOrEmpty(mailAddress) == true)
            { throw new FormatException(); }
			if (mailAddress.Contains("@") == false)
			{ throw new FormatException("Mail address must be contain @ char."); }

			Match m = RegexList.MailAddressWithBracket.Match(mailAddress);
			if (m.Success == true)
			{
				this._Value = m.Groups["MailAddress"].Value;
			}
			else
			{
				this._Value = mailAddress;
			}
			String[] ss = _Value.Split('@');
			this._UserName = ss[0];
			this._DomainName = ss[1];
            this.InitializeProperty();
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mailAddress"></param>
		/// <param name="displayName"></param>
        public MailAddress(String mailAddress, String displayName) : 
			this(mailAddress)
        {
            this._DisplayName = displayName;
        }
        private void InitializeProperty()
        {
            if (CultureInfo.CurrentCulture.Name.StartsWith("ja") == true)
            {
                this.Encoding = Encoding.GetEncoding("iso-2022-jp");
                this.TransferEncoding = TransferEncoding.Base64;
            }
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public override string ToString()
        {
            if (String.IsNullOrEmpty(this._DisplayName) == true)
            {
                return String.Format("<{0}>", this._Value);
            }
            if (this._IsDoubleQuote == true)
            {
                return String.Format("\"{0}\" <{1}>", this._DisplayName, this._Value);
            }
            else
            {
                return String.Format("{0} <{1}>", this._DisplayName, this._Value);
            }
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public String ToEncodeString()
        {
            return MailAddress.ToMailAddressText(this._Encoding, this._TransferEncoding
                , this._Value, this._DisplayName, this._IsDoubleQuote);
        }
        /// 指定したエンコード方式でエンコードされた表示名＜メールアドレス＞の文字列を取得します。
        /// <summary>
        /// Get mail address text encoded by specify encoding.
        /// 指定したエンコード方式でエンコードされた表示名＜メールアドレス＞の文字列を取得します。
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <param name="displayName"></param>
        /// <param name="doubleQuote"></param>
        /// <returns></returns>
        public static String ToMailAddressText(String mailAddress, String displayName, Boolean doubleQuote)
        {
            if (CultureInfo.CurrentCulture.Name.StartsWith("ja") == true)
            {
                return MailAddress.ToMailAddressText(Encoding.GetEncoding("iso-2022-jp"), TransferEncoding.Base64
                    , mailAddress, displayName, doubleQuote);
            }
            return MailAddress.ToMailAddressText(Encoding.ASCII, TransferEncoding.Base64, mailAddress, displayName, doubleQuote);
        }
        /// 指定したエンコード方式でエンコードされた表示名＜メールアドレス＞の文字列を取得します。
        /// <summary>
        /// Get mail address text encoded by specify encoding.
        /// 指定したエンコード方式でエンコードされた表示名＜メールアドレス＞の文字列を取得します。
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="transferEncoding"></param>
        /// <param name="mailAddress"></param>
        /// <param name="displayName"></param>
        /// <param name="doubleQuote"></param>
        /// <returns></returns>
        public static String ToMailAddressText(Encoding encoding, TransferEncoding transferEncoding
            , String mailAddress, String displayName, Boolean doubleQuote)
        {
            if (String.IsNullOrEmpty(displayName) == true)
            {
                return mailAddress;
            }
            else
            {
                if (doubleQuote == true)
                {
                    return String.Format("\"{0}\" <{1}>", displayName, mailAddress);
                }
                else
                {
                    return String.Format("{0} <{1}>"
                        , MailParser.EncodeToMailHeaderLine(displayName, transferEncoding, encoding, MailParser.MaxCharCountPerRow - mailAddress.Length - 3)
                        , mailAddress);
                }
            }
        }
        /// 文字列を元にMailAddressのインスタンスを生成します。
        /// <summary>
        /// Create MailAddress object by mail address text.
        /// 文字列を元にMailAddressのインスタンスを生成します。
        /// 表示名＜メールアドレス＞の書式の場合、DisplayNameとMailAddressに値をセットしてインスタンスを生成します。
        /// ＜メールアドレス＞の場合、MailAddressにメールアドレスの値をセットしてインスタンスを生成します。
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <returns></returns>
        public static MailAddress Create(String mailAddress)
        {
            Regex rx = RegexList.DisplayName_MailAddress;
            Match m = null;

            m = rx.Match(mailAddress);
            if (String.IsNullOrEmpty(m.Value) == true)
            {
                rx = RegexList.MailAddressWithBracket;
                m = rx.Match(mailAddress);
                if (String.IsNullOrEmpty(m.Value) == true)
                {
                    return new MailAddress(mailAddress);
                }
                else
                {
                    return new MailAddress(m.Groups["MailAddress"].Value);
                }
            }
            else
            {
				if (String.IsNullOrEmpty(m.Groups["DisplayName"].Value) == true)
				{
					return new MailAddress(mailAddress);
				}
				else
				{
					return new MailAddress(m.Groups["MailAddress"].Value, m.Groups["DisplayName"].Value.TrimEnd(' '));
				}
            }
        }
		/// 文字列を元にMailAddressのインスタンスの生成を試みます。
		/// <summary>
		/// Try to create MailAddress object by mail address text.
		/// 文字列を元にMailAddressのインスタンスの生成を試みます。
		/// メールアドレスに変換できない文字列の場合、戻り値はnullになります。
		/// </summary>
		/// <param name="mailAddress"></param>
		/// <returns></returns>
		public static MailAddress TryCreate(String mailAddress)
		{
			try
			{
				if (String.IsNullOrEmpty(mailAddress) == true)
				{ return null; }
				if (mailAddress.Contains("@") == false)
				{ return null; }
				return MailAddress.Create(mailAddress);
			}
			catch { }
			return null;
		}
		/// メールアドレス一覧の文字列からMailAddressの一覧を取得します。
        /// <summary>
        /// Get mailaddress list from mail address list text.
        /// メールアドレス一覧の文字列からMailAddressの一覧を取得します。
        /// </summary>
        /// <param name="mailAddressListText"></param>
        /// <returns></returns>
        public static List<MailAddress> CreateMailAddressList(String mailAddressListText)
        {
            List<MailAddress> l = new List<MailAddress>();
			MailAddress m = null;
            String[] ss = null;

            ss = mailAddressListText.Split(',');
            for (int i = 0; i < ss.Length; i++)
            {
				m = MailAddress.TryCreate(ss[i].Trim());
				if (m == null)
				{ continue; }
                l.Add(m);
            }
            return l;
        }
    }
}
