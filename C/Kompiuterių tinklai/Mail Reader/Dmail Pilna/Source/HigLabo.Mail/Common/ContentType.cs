using System;
using System.Collections.Generic;
using System.Text;
using HigLabo.Net.Mail;

namespace HigLabo.Net
{
    /// Represent Content-Type as class.
    /// <summary>
    /// Represent Content-Type as class.
    /// </summary>
    public class ContentType : InternetTextMessage.Field
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
        /// Get or set name.
        /// </summary>
        public String Name
        {
            get
            {
                InternetTextMessage.Field f = InternetTextMessage.Field.FindField(this._Fields, "Name");
                if (f == null)
                {
                    return "";
                }
				return MailParser.DecodeFromMailHeaderLine(f.Value);
			}
            set
            {
                InternetTextMessage.Field f = InternetTextMessage.Field.FindField(this._Fields, "Name");
                if (f == null)
                {
                    f = new InternetTextMessage.Field("Name", value);
                    this._Fields.Add(f);
                }
                else
                {
                    f.Value = value;
                }
            }
        }
        /// <summary>
        /// Get or set boundary.
        /// </summary>
        public String Boundary
        {
            get
            {
                InternetTextMessage.Field f = InternetTextMessage.Field.FindField(this._Fields, "Boundary");
                if (f == null)
                {
                    return "";
                }
                return f.Value;
            }
            set
            {
                InternetTextMessage.Field f = InternetTextMessage.Field.FindField(this._Fields, "Boundary");
                if (f == null)
                {
                    f = new InternetTextMessage.Field("Boundary", value);
                    this._Fields.Add(f);
                }
                else
                {
                    f.Value = value;
                }
            }
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
        public ContentType(String value) : 
            base("Content-Type", value)
        {
            this.Value = value;
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="fields"></param>
        public ContentType(String value, InternetTextMessage.Field[] fields) :
            base("Content-Type", value)
        {
            this.Value = value;
            for (int i = 0; i < fields.Length; i++)
            {
                this._Fields.Add(fields[i]);
            }
        }
    }
}
