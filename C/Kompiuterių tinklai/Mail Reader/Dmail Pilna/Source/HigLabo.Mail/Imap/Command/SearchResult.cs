using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using HigLabo.Net.Mail;

namespace HigLabo.Net.Imap
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchResult : ImapCommandResult
    {
        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<Int64> MailIndexList { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="text"></param>
        public SearchResult(String tag, String text)
            : base(tag, text)
        {
            if (this.Status != ImapCommandResultStatus.Ok)
            {
                this.MailIndexList = new ReadOnlyCollection<Int64>(new List<Int64>());
                return;
            }
            String s = "* Search ";
            if (text.StartsWith(s, StringComparison.OrdinalIgnoreCase) == false)
            { throw new MailClientException(); }

            String line = "";
            using (StringReader sr = new StringReader(text))
            {
                line = sr.ReadLine();
            }
            Int32 startIndex = s.Length;
            String ss = line.Substring(startIndex, line.Length - startIndex);
            List<Int64> l = new List<Int64>();
            Int64 mailIndex = 0;
            foreach (var el in ss.Split(' '))
            {
                if (Int64.TryParse(el, out mailIndex) == false) { continue; }
                l.Add(mailIndex);
            }
            this.MailIndexList = new ReadOnlyCollection<Int64>(l);
        }
    }
}
