using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HigLabo.Net.Mail;

namespace HigLabo.Net
{
    /// Represent Mime-Content as class.
    /// <summary>
    /// Represent Mime-Content as class.
    /// </summary>
    public class MimeContent : InternetTextMessage
    {
        private List<MimeContent> _Contents = null;
        /// Header部分のデータを取得します。
        /// <summary>
        /// Get header text data of this mail.
        /// Header部分のデータを取得します。
        /// </summary>
        public new String HeaderData
        {
            get { return base.HeaderData; }
            protected set { base.HeaderData = value; }
        }
        /// Body部分のデータを取得します。
        /// <summary>
        /// Get body text data of this mail.
        /// Body部分のデータを取得します。
        /// </summary>
        public new String BodyData
        {
            get { return base.BodyData; }
            protected set { base.BodyData = value; }
        }
        /// MimeContentのコレクションを取得します。
        /// <summary>
        /// Get mime content collection.
        /// MimeContentのコレクションを取得します。
        /// </summary>
        public List<MimeContent> Contents
        {
            get { return this._Contents; }
        }
		/// <summary>
		/// 
		/// </summary>
        public MimeContent():
            base()
        {
            this.Initialize("");
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
        public MimeContent(String text):
            base(text)
        {
            this.Initialize(text);
        }
        private void Initialize(String text)
        {
            this._Contents = new List<MimeContent>();
            if (this.IsMultiPart == true)
            {
                List<String> l = MimeContent.ParseToContentTextList(this.BodyData, this.MultiPartBoundary);
                for (int i = 0; i < l.Count; i++)
                {
                    this._Contents.Add(new MimeContent(l[i]));
                }
            }
        }
        /// Body部のテキストを解析し、それぞれのMIME部分のテキストに分割します。
        /// <summary>
        /// Parse body text and separate as text foe each mime content.
        /// Body部のテキストを解析し、それぞれのMIME部分のテキストに分割します。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="multiPartBoundary"></param>
        public static List<String> ParseToContentTextList(String text, String multiPartBoundary) 
        {
            StringReader sr = null;
            StringBuilder sb = new StringBuilder();
            String CurrentLine = "";
            String StartOfBoundary = "--" + multiPartBoundary;
            String EndOfBoundary = "--" + multiPartBoundary + "--";
            List<String> l = new List<string>();
            Boolean IsBegin = false;

            using (sr = new StringReader(text))
            {
                while (true)
                {
                    CurrentLine = sr.ReadLine();
                    if (CurrentLine == null)
                    { break; }
                    if (IsBegin == false)
                    {
                        if (CurrentLine == StartOfBoundary)
                        {
                            IsBegin = true;
                            sb.Length = 0;
                        }
                        continue;
                    }
                    if (CurrentLine == StartOfBoundary ||
                        CurrentLine == EndOfBoundary)
                    {
                        if (sb.Length > 0)
                        {
                            l.Add(sb.ToString());
                        }
                        sb.Length = 0;
                        if (CurrentLine == EndOfBoundary)
                        { break; }
                    }
                    else
                    {
                        sb.Append(CurrentLine);
                        sb.Append(MailParser.NewLine);
                    }
                    if (sr.Peek() == -1)
                    {
                        if (IsBegin == true)
                        {
                            l.Add(sb.ToString());
                        }
                        break; 
                    }
                }
            }
            return l;
        }
    }
}
