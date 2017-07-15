using System;
using System.Collections.Generic;
using System.Text;
using HigLabo.Net.Mail;

namespace HigLabo.Net
{
	/// Represent Content-Disposition as class.
	/// <summary>
	/// Represent Content-Disposition as class.
	/// </summary>
	public class ContentDisposition : InternetTextMessage.Field
	{
		private List<InternetTextMessage.Field> _Fields = new List<InternetTextMessage.Field>();
		/// <summary>
		/// Get field collection.
		/// </summary>
		public List<InternetTextMessage.Field> Fields
		{
			get { return this._Fields; }
		}
		/// <summary>
		/// Get or set filename.
		/// </summary>
		public String FileName
		{
			get
			{
				InternetTextMessage.Field f = InternetTextMessage.Field.FindField(this._Fields, "FileName");
				if (f == null)
				{
					return "";
				}
				return MailParser.DecodeFromMailHeaderLine(f.Value);
			}
			set
			{
				InternetTextMessage.Field f = InternetTextMessage.Field.FindField(this._Fields, "FileName");
				if (f == null)
				{
					f = new InternetTextMessage.Field("FileName", value);
					this._Fields.Add(f);
				}
				else
				{
					f.Value = value;
				}
				this.Value = "attachment";
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		public ContentDisposition(String value) :
			base("Content-Disposition", value)
		{
			this.Value = value;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="fields"></param>
		public ContentDisposition(String value, InternetTextMessage.Field[] fields) :
			base("Content-Disposition", value)
		{
			this.Value = value;
			for (int i = 0; i < fields.Length; i++)
			{
				this._Fields.Add(fields[i]);
			}
		}
	}
}
