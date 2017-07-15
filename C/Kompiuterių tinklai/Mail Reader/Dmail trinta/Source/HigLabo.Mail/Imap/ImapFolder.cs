using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HigLabo.Net.Imap
{
    public class ImapFolder //+
    {
        public String Name { get; set; } //+
        public Boolean HasChildren { get; set; } //+
        public Boolean NoSelect { get; set; } //+
        public Int32 MailCount { get; set; } //+
        public Int32 RecentMailCount { get; set; } //+
        public List<ImapFolder> Children { get; set; } //+
        public ReadOnlyCollection<String> Flags { get; private set; } //+
        public ImapFolder(SelectResult result) //Sudeda informacija apie rezultata i ImapFolder
        {
            this.Name = result.FolderName; //+
            this.MailCount = result.Exists; //+
            this.RecentMailCount = result.Recent;  //+
            this.Flags = new ReadOnlyCollection<string>(new List<string>(result.Flags)); //+
        }
    }
}
