using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HigLabo.Net.Imap //+
{
    public class SelectResult //+
    {
        public String FolderName { get; private set; } //+
        public Int32 Exists { get; private set; } //+
        public Int32 Recent { get; private set; } //+
        public ReadOnlyCollection<String> Flags { get; private set; } //+
        public SelectResult(String folderName, Int32 exists, Int32 recent, params String[] flags) //Sudeda visa informacija apie rezultata i objekta
        {
            this.FolderName = folderName; //+
            this.Exists = exists; //+
            this.Recent = recent; //+
            this.Flags = new ReadOnlyCollection<string>(new List<string>(flags));
        }
    }
}
