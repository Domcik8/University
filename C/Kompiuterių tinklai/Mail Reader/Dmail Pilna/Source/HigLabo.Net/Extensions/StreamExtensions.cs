﻿using System;
using System.IO;

namespace HigLabo.Net
{
    /// <summary>
    /// 
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Byte[] ToByteArray(this Stream stream) //Is stream nuskaito duomenys i MemoryStream ir i Byte[] ideda
        {
            var mm = new MemoryStream(); //+
            stream.CopyTo(mm); //+
            return mm.ToArray(); //+
        }
    }
}
