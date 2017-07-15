using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using HigLabo.Net.Mail;
using HigLabo.Net.Pop3;

namespace HigLabo.Net.Mail
{
    /// <summary>
    /// Represent mail message with attachment as MailContent.
    /// </summary>
    public class MailMessage : InternetTextMessage
    {
        private Boolean _InvalidFormat = false;
        /// デコード済みの本文の文字列データ
        /// <summary>
        /// Field for decoded body data.
        /// デコード済みの本文の文字列データ
        /// </summary>
        private String _BodyText;
        private Boolean _BodyTextCreated = false;
        private MailContent _BodyContent = null;
        private List<MailContent> _Contents = new List<MailContent>();
        private Int64? _Index = 0;
        private Int32 _Size = 0;
        /// メールボックスにおけるメールのIndexの値を取得します。
        /// <summary>
        /// Get mail index of this mailbox.
        /// メールボックスにおけるメールのIndexの値を取得します。
        /// </summary>
        public Int64? Index
        {
            get { return this._Index; }
        }
        /// Toの値を取得します。
        /// <summary>
        /// Get TO value of this mail.
        /// Toの値を取得します。
        /// </summary>
        public String To
        {
            get { return this["To"]; }
        }
        /// Ccの値を取得します。
        /// <summary>
        /// Get CC value of this mail.
        /// Ccの値を取得します。
        /// </summary>
        public String Cc
        {
            get { return this["Cc"]; }
        }
        /// Bccの値を取得します。
        /// <summary>
        /// Get BCC value of this mail.
        /// Bccの値を取得します。
        /// </summary>
        public String Bcc
        {
            get { return this["Bcc"]; }
        }
        /// Body部のメッセージのテキストを取得します。
        /// <summary>
        /// Get body text message of this mail.
        /// Body部のメッセージのテキストを取得します。
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
        /// Header部分のデータを取得します。
        /// <summary>
        /// Get header text data of this mail.
        /// Header部分のデータを取得します。
        /// </summary>
        public new String HeaderData
        {
            get { return base.HeaderData; }
        }
        /// Body部分のデータを取得します。
        /// <summary>
        /// Get body text data of this mail.
        /// Body部分のデータを取得します。
        /// </summary>
        public new String BodyData
        {
            get { return base.BodyData; }
        }
        /// メールのサイズを取得します。
        /// <summary>
        /// Get mail size of this mail.
        /// メールのサイズを取得します。
        /// </summary>
        public Int32 Size
        {
            get { return this._Size; }
            set { this._Size = value; }
        }
        /// Body部分のMailContentを取得します。
        /// <summary>
        /// Get content of this mail message.
        /// Body部分のMailContentを取得します。
        /// </summary>
        public MailContent BodyContent
        {
            get
            {
                this.EnsureBodyContent(this._Contents);
                return this._BodyContent;
            }
        }
        /// MailContentのコレクションを取得します。
        /// <summary>
        /// Get mail content collection of this mail.
        /// MailContentのコレクションを取得します。
        /// </summary>
        public List<MailContent> Contents
        {
            get { return this._Contents; }
        }
        /// メッセージのフォーマットが正しいかどうかを示す値を取得します。
        /// <summary>
        /// Get a value that specify this mail format is valid or invalid.
        /// メッセージのフォーマットが正しいかどうかを示す値を取得します。
        /// </summary>
        public Boolean InvalidFormat
        {
            get { return this._InvalidFormat; }
        }
        /// Body部分のテキストが生成済みか同かを示す値を取得します。
        /// <summary>
        /// Get value that indicate body text is created or not.
        /// Body部分のテキストが生成済みか同かを示す値を取得します。
        /// </summary>
        protected Boolean BodyTextCreated
        {
            get { return this._BodyTextCreated; }
            set { this._BodyTextCreated = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public MailMessage(String text) :
            base(text)
        {
            this.Initialize(text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="index"></param>
        public MailMessage(String text, Int64 index) :
            base(text)
        {
            this._Index = index;
            this.Initialize(text);
        }
        private void Initialize(String text)
        {
            this.Data = text;
            this._Size = text.Length;
            if (this.IsMultiPart == true)
            {
                List<String> l = MimeContent.ParseToContentTextList(this.BodyData, this.MultiPartBoundary);
                for (int i = 0; i < l.Count; i++)
                {
                    this._Contents.Add(new MailContent(this, l[i]));
                }
            }
        }
        /// Body部のデータがセットされているか確認し、セットされてない場合はデータをセットします。
        /// <summary>
        /// Ensure that body data is set or not,and set body data if body data is not set.
        /// Body部のデータがセットされているか確認し、セットされてない場合はデータをセットします。
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        private Boolean EnsureBodyContent(List<MailContent> contents)
        {
            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i].IsBody == true)
                {
                    this._BodyContent = contents[i];
                    return true;
                }
                if (this.EnsureBodyContent(contents[i].Contents) == true)
                { return true; }
            }
            return false;
        }
        /// 全てのMailContentのコレクションを取得します。
        /// <summary>
        /// Get all mail content collection.
        /// 全てのMailContentのコレクションを取得します。
        /// </summary>
        /// <returns></returns>
        public static List<MailContent> GetAllContents(MailMessage pop3Message)
        {
            if (pop3Message == null)
            { throw new ArgumentNullException("pop3Message"); }
            List<MailContent> l = new List<MailContent>();
            l = MailMessage.GetAttachedContents(pop3Message.Contents, delegate(MailContent c) { return true; });
            return l;
        }
        /// IsAttachmentがtrueのMailContentのコレクションを取得します。
        /// <summary>
        /// Get mail content collection that IsAttachment property is true.
        /// IsAttachmentがtrueのMailContentのコレクションを取得します。
        /// </summary>
        /// <returns></returns>
        public static List<MailContent> GetAttachedContents(MailMessage pop3Message)
        {
            if (pop3Message == null)
            { throw new ArgumentNullException("pop3Message"); }
            List<MailContent> l = new List<MailContent>();
            l = MailMessage.GetAttachedContents(pop3Message.Contents, delegate(MailContent c) { return c.IsAttachment; });
            return l;
        }
        /// predicateで与えた条件を満たすMailContentのコレクションを取得します。
        /// <summary>
        /// Get mail content collection that specify predicate is true.
        /// predicateで与えた条件を満たすMailContentのコレクションを取得します。
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static List<MailContent> GetAttachedContents(List<MailContent> contents, Predicate<MailContent> predicate)
        {
            List<MailContent> l = new List<MailContent>();
            for (int i = 0; i < contents.Count; i++)
            {
                if (predicate(contents[i]) == true)
                {
                    l.Add(contents[i]);
                }
                l.AddRange(MailMessage.GetAttachedContents(contents[i].Contents, predicate).ToArray());
            }
            return l;
        }
        /// Body部のテキストがセットされているか確認し、セットされてない場合はBody部の文字列をセットします。
        /// <summary>
        /// Ensure that body text is set or not,and set body text if body text is not set.
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
                else if (this.IsMultiPart == true)
                {
                    if (this.BodyContent == null)
                    {
                        this.BodyText = "";
                    }
                    else
                    {
                        this.BodyText = this.BodyContent.BodyText;
                    }
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
        /// このインスタンスの値を元に、SmtpMessageクラスのインスタンスを生成します。
        /// <summary>
        /// Create SmtpMessage instance with this instance value.
        /// このインスタンスの値を元に、SmtpMessageクラスのインスタンスを生成します。
        /// </summary>
        /// <returns></returns>
        public Smtp.SmtpMessage CreateSmtpMessage()
        {
            Smtp.SmtpMessage mg = new HigLabo.Net.Smtp.SmtpMessage();
            Field f = null;

            mg.To.AddRange(MailAddress.CreateMailAddressList(this.To));
            mg.Cc.AddRange(MailAddress.CreateMailAddressList(this.Cc));
            for (int i = 0; i < this.Header.Count; i++)
            {
                f = this.Header[i];
                if (String.IsNullOrEmpty(f.Value) == true)
                { continue; }
                if (f.Key.ToLower() == "to" ||
                    f.Key.ToLower() == "cc")
                { continue; }
                mg[f.Key] = MailParser.DecodeFromMailHeaderLine(f.Value);
            }
            for (int i = 0; i < this.ContentType.Fields.Count; i++)
            {
                f = this.ContentType.Fields[i];
                mg.ContentType.Fields.Add(new Field(f.Key, MailParser.DecodeFromMailHeaderLine(f.Value)));
            }
            for (int i = 0; i < this.ContentDisposition.Fields.Count; i++)
            {
                f = this.ContentDisposition.Fields[i];
                mg.ContentDisposition.Fields.Add(new Field(f.Key, MailParser.DecodeFromMailHeaderLine(f.Value)));
            }
            mg.BodyText = this.BodyText;
            for (int i = 0; i < this.Contents.Count; i++)
            {
                mg.Contents.Add(this.Contents[i].CreateSmtpContent());
            }
            return mg;
        }
    }
}
