using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HigLabo.Net.Mail;

namespace HigLabo.Net.Smtp
{
    /// Represent an line of smtp command result responsed from server.
    /// <summary>
    /// Represent an line of smtp command result responsed from server.
    /// </summary>
    public class SmtpCommandResultLine
    {
		private class RegexList
		{
			public static readonly Regex SmtpResultLine = new Regex(@"(?<StatusCode>[0-9]{3})(?<HasNextLine>[\s-]{0,1})(?<Message>.*)", RegexOptions.IgnoreCase);
		}
        private Boolean _InvalidFormat = false;
        private Int32 _StatusCodeNumber = 0;
        private SmtpCommandResultCode _StatusCode = SmtpCommandResultCode.None;
        private Boolean _HasNextLine = false;
        private String _Message = "";
		/// <summary>
		/// 
		/// </summary>
        public Boolean InvalidFormat
        {
            get { return this._InvalidFormat; }
        }
		/// <summary>
		/// 
		/// </summary>
        public Int32 CodeNumber
        {
            get { return this._StatusCodeNumber; }
        }
		/// <summary>
		/// 
		/// </summary>
        public SmtpCommandResultCode StatusCode
        {
            get { return this._StatusCode; }
        }
		/// <summary>
		/// 
		/// </summary>
        public Boolean HasNextLine
        {
            get { return this._HasNextLine; }
        }
		/// <summary>
		/// 
		/// </summary>
        public String Message
        {
            get { return this._Message; }
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="line"></param>
        public SmtpCommandResultLine(String line)
        {
            Match m = RegexList.SmtpResultLine.Match(line);
            this._InvalidFormat = !Int32.TryParse(m.Groups["StatusCode"].Value, out this._StatusCodeNumber);
            this._StatusCode = (SmtpCommandResultCode)this._StatusCodeNumber;
            this._HasNextLine = m.Groups["HasNextLine"].Value == "-";
            this._Message = m.Groups["Message"].Value;
        }
    }
}
