using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HigLabo.Net.Mail;

namespace HigLabo.Net.Pop3
{
    /// Represent mail content.
    /// <summary>
    /// Represent mail content.
    /// </summary>
    public class MailContent : MimeContent
    {
        private MailMessage _Message;
        private MailContent _ParentContent = null;
        /// デコード済みの本文の文字列データ
        /// <summary>
        /// Field for decoded body data.
        /// デコード済みの本文の文字列データ
        /// </summary>
        private String _BodyText;
        private Boolean _BodyTextCreated = false;
        private List<MailContent> _Contents = new List<MailContent>();
        /// 親のContentを取得または設定します。
        /// <summary>
        /// Get or set parent content object.
        /// 親のContentを取得または設定します。
        /// </summary>
        public MailContent ParentContent
        {
            get { return this._ParentContent; }
            private set { this._ParentContent = value; }
        }
        /// Nameの値を取得します。
        /// <summary>
        /// Get name value.
        /// Nameの値を取得します。
        /// </summary>
        public String Name
        {
            get { return this.ContentType.Name; }
        }
        /// FileNameの値を取得します。
        /// <summary>
        /// Get filename value.
        /// FileNameの値を取得します。
        /// </summary>
        public String FileName
        {
            get { return this.ContentDisposition.FileName; }
        }
        /// Body部の文字列を取得します。
        /// <summary>
        /// Get body text of this mail.
        /// Body部の文字列を取得します。
        /// </summary>
        public String BodyText
        {
            get
            {
                this.EnsureBodyText();
                return this._BodyText;
            }
            set { this._BodyText = value; }
        }
        /// MailContentのコレクションを取得します。
        /// <summary>
        /// Get mail content collection of this mail.
        /// MailContentのコレクションを取得します。
        /// </summary>
        public new List<MailContent> Contents
        {
            get { return this._Contents; }
        }
        /// Body部分のテキストが生成済みかどうかを示す値を取得します。
        /// <summary>
        /// Get value that indicate body text is created or not.
        /// Body部分のテキストが生成済みかどうかを示す値を取得します。
        /// </summary>
        protected Boolean BodyTextCreated
        {
            get { return this._BodyTextCreated; }
            set { this._BodyTextCreated = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="text"></param>
        public MailContent(MailMessage message, String text) :
            base(text)
        {
            this.Initialize(message, text);
        }
        /// 初期化処理を行います。
        /// <summary>
        /// 初期化処理を行います。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="text"></param>
        private void Initialize(MailMessage message, String text)
        {
            MailContent ct = null;

            this._Message = message;
            this._Contents = new List<MailContent>();
            this.Data = text;
            this._BodyText = "";
            if (this.IsMultiPart == true)
            {
                List<String> l = MimeContent.ParseToContentTextList(this.BodyData, this.MultiPartBoundary);
                for (int i = 0; i < l.Count; i++)
                {
                    ct = new MailContent(this._Message, l[i]);
                    ct.ParentContent = this;
                    this._Contents.Add(ct);
                }
            }
        }
        /// Body部のテキストがセットされているか確認し、セットされてない場合はBody部の文字列をセットします。
        /// <summary>
        /// Body部のテキストがセットされているか確認し、セットされてない場合はBody部の文字列をセットします。
        /// </summary>
        /// <returns></returns>
        protected virtual void EnsureBodyText()
        {
            if (this.BodyTextCreated == false)
            {
                if (this.ContentType.Value.IndexOf("message/rfc822") > -1)
                {
                    this.BodyText = this.BodyData;
                }
                else if (this.IsText == true)
                {
                    this.BodyText = MailParser.DecodeFromMailBody(this.BodyData, this.ContentTransferEncoding, this.ContentEncoding);
                }
                else
                {
                    this.BodyText = this.BodyData;
                }
            }
            this.BodyTextCreated = true;
        }
        /// このインスタンスの値を元に、SmtpContentクラスのインスタンスを生成します。
        /// <summary>
        /// Create SmtpContent instance with this instance value.
        /// このインスタンスの値を元に、SmtpContentクラスのインスタンスを生成します。
        /// </summary>
        /// <returns></returns>
        public Smtp.SmtpContent CreateSmtpContent()
        {
            Smtp.SmtpContent ct = new HigLabo.Net.Smtp.SmtpContent();
            Field f = null;

            for (int i = 0; i < this.Header.Count; i++)
            {
                f = this.Header[i];
                if (String.IsNullOrEmpty(f.Value) == true)
                { continue; }
                ct[f.Key] = MailParser.DecodeFromMailHeaderLine(f.Value);
            }
            for (int i = 0; i < this.ContentType.Fields.Count; i++)
            {
                f = this.ContentType.Fields[i];
                ct.ContentType.Fields.Add(new Field(f.Key, MailParser.DecodeFromMailHeaderLine(f.Value)));
            }
            for (int i = 0; i < this.ContentDisposition.Fields.Count; i++)
            {
                f = this.ContentDisposition.Fields[i];
                ct.ContentDisposition.Fields.Add(new Field(f.Key, MailParser.DecodeFromMailHeaderLine(f.Value)));
            }
            ct.LoadText(this.BodyText);
            for (int i = 0; i < this.Contents.Count; i++)
            {
                ct.Contents.Add(this.Contents[i].CreateSmtpContent());
            }
            return ct;
        }
    }
}
