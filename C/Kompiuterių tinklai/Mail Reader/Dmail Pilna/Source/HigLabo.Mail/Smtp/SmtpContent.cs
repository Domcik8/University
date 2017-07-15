using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HigLabo.Net.Mail;

namespace HigLabo.Net.Smtp
{
    /// Represent smtp content.
    /// <summary>
    /// Represent smtp content.
    /// </summary>
    public class SmtpContent : MimeContent
    {
        private FieldParameterEncoding _FieldParameterEncoding = FieldParameterEncoding.Rfc2047;
        private List<SmtpContent> _Contents;
        private static Dictionary<String, String> FileExtensionContentType = new Dictionary<String, String>();
        private String _BodyText = "";
        /// HeaderのFieldのParameterのEncodingを取得または設定します。
        /// <summary>
        /// HeaderのFieldのParameterのEncodingを取得または設定します。
        /// </summary>
        public FieldParameterEncoding FieldParameterEncoding
        {
            get { return this._FieldParameterEncoding; }
            set { this._FieldParameterEncoding = value; }
        }
        /// Nameの値を取得または設定します。
        /// <summary>
        /// Nameの値を取得または設定します。
        /// </summary>
        public String Name
        {
            get { return this.ContentType.Name; }
            set { this.ContentType.Name = value; }
        }
        /// FileNameの値を取得または設定します。
        /// <summary>
        /// FileNameの値を取得または設定します。
        /// </summary>
        public String FileName
        {
            get { return this.ContentDisposition.FileName; }
            set { this.ContentDisposition.FileName = value; }
        }
        /// ボディ部分のテキスト文字列を取得または設定します。
        /// <summary>
        /// ボディ部分のテキスト文字列を取得または設定します。
        /// </summary>
        public String BodyText
        {
            get { return this._BodyText; }
            private set { this._BodyText = value; }
        }
        /// SmtpContentのコレクションを取得します。
        /// <summary>
        /// SmtpContentのコレクションを取得します。
        /// </summary>
        public new List<SmtpContent> Contents
        {
            get { return this._Contents; }
        }
        static SmtpContent()
        {
            SmtpContent.InitializeFileExtenstionContentType();
        }
		/// <summary>
		/// 
		/// </summary>
		public SmtpContent() :
			base()
		{
            this.ContentTransferEncoding = TransferEncoding.Base64;
            this._Contents = new List<SmtpContent>();
		}
        /// 拡張子とContent-Typeのマッピングのデータを初期化します。
        /// <summary>
        /// 拡張子とContent-Typeのマッピングのデータを初期化します。
        /// </summary>
        private static void InitializeFileExtenstionContentType()
        {
            var l = SmtpContent.FileExtensionContentType;
            l.Add("txt", "text/plain");
            l.Add("css", "text/css");
            l.Add("htm", "text/html");
            l.Add("html", "text/html");
            l.Add("jpg", "Image/jpeg");
            l.Add("gif", "Image/gif");
            l.Add("bmp", "image/x-ms-bmp");
            l.Add("png", "Image/png");
            l.Add("wav", "Audio/wav");
            l.Add("doc", "application/msword");
            l.Add("mdb", "application/msaccess");
            l.Add("xls", "application/vnd.ms-excel");
            l.Add("ppt", "application/vnd.ms-powerpoint");
            l.Add("mpeg", "video/mpeg");
            l.Add("mpg", "video/mpeg");
            l.Add("avi", "video/x-msvideo");
            l.Add("zip", "application/zip");
        }
        /// 拡張子を元にContent-Typeの文字列を取得します。
        /// <summary>
        /// 拡張子を元にContent-Typeの文字列を取得します。
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        private static String GetContentType(String extension)
        {
            String s = extension.Replace(".", "").ToLower();
            if (SmtpContent.FileExtensionContentType.ContainsKey(s.ToLower()) == true)
            {
                return SmtpContent.FileExtensionContentType[s.ToLower()];
            }
            return "application/octet-stream";
        }
        /// 指定したテキストデータをセットします。
        /// <summary>
        /// 指定したテキストデータをセットします。
        /// </summary>
        /// <param name="text"></param>
        public void LoadText(String text)
        {
            this.ContentType.Value = "text/plain";
            this.BodyText = text;
            this.SetData();
        }
        /// HTML形式のテキストをセットします。
        /// <summary>
        /// HTML形式のテキストをセットします。
        /// </summary>
        /// <param name="html"></param>
        public void LoadHtml(String html)
        {
            this.ContentType.Value = "text/html";
            this.BodyText = html;
            this.SetData();
        }
        /// 指定したファイルパスのファイルデータを元にデータをセットします。
        /// <summary>
        /// 指定したファイルパスのファイルデータを元にデータをセットします。
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadFileData(String filePath)
        {
            FileInfo fi = null;
            Byte[] b = null;

            fi = new FileInfo(filePath);

            this.ContentType.Value = SmtpContent.GetContentType(Path.GetExtension(filePath).Replace(".", ""));
            this.ContentType.Name = fi.Name;
            this.ContentDisposition.FileName = fi.Name;
            this.ContentDisposition.Value = "attachment";

            b = new Byte[fi.Length];
            using (var stm = new FileStream(filePath, FileMode.Open))
            {
                stm.Read(b, 0, b.Length);
                this.BodyText = Convert.ToBase64String(b);
                stm.Close();
            }
            this.SetData();
        }
        /// バイトデータを元にデータをセットします。
        /// <summary>
        /// バイトデータを元にデータをセットします。
        /// </summary>
        /// <param name="bytes"></param>
        public void LoadData(Byte[] bytes)
        {
            this.ContentDisposition.Value = "attachment";
            this.ContentTransferEncoding = TransferEncoding.Base64;
            this.BodyText = Convert.ToBase64String(bytes);
            this.SetData();
        }
        private void SetData()
        {
            this.SetHeaderData();
            this.SetBodyData();
            this.Data = this.HeaderData + MailParser.NewLine + this.BodyData;
        }
        private void SetHeaderData()
        {
            StringBuilder sb = new StringBuilder(1024);

            if (this.IsMultiPart == false &&
                this.Contents.Count > 0)
            {
                this.ContentType.Value = "multipart/mixed";
            }
            if (this.IsBody == true)
            {
                sb.AppendFormat("Content-Type: {0}; charset=\"{1}\"", this.ContentType.Value, this.ContentEncoding.WebName);
                sb.Append(MailParser.NewLine);
            }
            else
            {
                sb.AppendFormat("Content-Type: {0};", this.ContentType.Value);
                sb.Append(MailParser.NewLine);
                if (String.IsNullOrEmpty(this.ContentType.Name) == false)
                {
                    if (this._FieldParameterEncoding == Mail.FieldParameterEncoding.Rfc2047)
                    {
                        sb.AppendFormat(" name=\"{0}\"", MailParser.EncodeToMailHeaderLine(this.ContentType.Name
                            , this.ContentTransferEncoding, this.ContentEncoding, MailParser.MaxCharCountPerRow - 8));
                    }
                    else if (this._FieldParameterEncoding == Mail.FieldParameterEncoding.Rfc2231)
                    {
                        sb.AppendFormat(MailParser.EncodeToMailHeaderLineByRfc2231("name", this.ContentType.Name
                            , this.ContentEncoding, MailParser.MaxCharCountPerRow - 8));
                    }
                    sb.Append(MailParser.NewLine);
                }
            }
            sb.AppendFormat("Content-Transfer-Encoding: {0}", MailParser.ToTransferEncoding(this.ContentTransferEncoding));
            sb.Append(MailParser.NewLine);
            if (String.IsNullOrEmpty(this["Content-Disposition"]) == false)
            {
                sb.AppendFormat("Content-Disposition: {0};", this.ContentDisposition.Value);
                sb.Append(MailParser.NewLine);
                if (String.IsNullOrEmpty(this.ContentDisposition.FileName) == false)
                {
                    if (this._FieldParameterEncoding == Mail.FieldParameterEncoding.Rfc2047)
                    {
                        sb.AppendFormat(" filename=\"{0}\"", MailParser.EncodeToMailHeaderLine(this.ContentDisposition.FileName
                            , this.ContentTransferEncoding, this.ContentEncoding, MailParser.MaxCharCountPerRow - 12));
                    }
                    else if (this._FieldParameterEncoding == Mail.FieldParameterEncoding.Rfc2231)
                    {
                        sb.AppendFormat(MailParser.EncodeToMailHeaderLineByRfc2231("filename", this.ContentDisposition.FileName
                            , this.ContentEncoding, MailParser.MaxCharCountPerRow - 12));
                    }
                    sb.Append(MailParser.NewLine);
                }
            }
            if (String.IsNullOrEmpty(this["Content-Description"]) == false)
            {
                sb.AppendFormat("Content-Description: {0}", this["Content-Description"]);
                sb.Append(MailParser.NewLine);
            }
            this.HeaderData = sb.ToString();
        }
        private void SetBodyData()
        {
            StringBuilder sb = new StringBuilder(1024);
            String bodyText = "";

            if (this.IsMultiPart == true)
            {
                for (int i = 0; i < this._Contents.Count; i++)
                {
                    sb.Append("--");
                    sb.Append(this.MultiPartBoundary);
                    sb.Append(MailParser.NewLine);
                    sb.Append(this._Contents[i].Data);
                    sb.Append(MailParser.NewLine);
                }
                sb.AppendFormat("--{0}--", this.MultiPartBoundary);
            }
            else
            {
                if (this.IsAttachment == true)
                {
                    bodyText = this.BodyText;
                }
                else
                {
                    bodyText = MailParser.EncodeToMailBody(this.BodyText, this.ContentTransferEncoding, this.ContentEncoding);
                }
                if (this.ContentTransferEncoding == TransferEncoding.SevenBit)
                {
                    sb.Append(bodyText);
                }
                else
                {
                    for (int i = 0; i < bodyText.Length; i++)
                    {
                        if (i > 0 &&
                            i % 76 == 0)
                        {
                            sb.Append(MailParser.NewLine);
                        }
                        sb.Append(bodyText[i]);
                    }
                }
            }
            this.BodyData = sb.ToString();
        }
    }
}
