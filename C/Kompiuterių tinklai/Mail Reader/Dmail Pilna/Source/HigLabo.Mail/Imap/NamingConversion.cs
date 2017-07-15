using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HigLabo.Net.Imap
{
    /// <summary>
    /// UTF-7 Mailbox International Naming Convention
    /// </summary>
    public class NamingConversion
    {
        /// <summary>
        /// Encodes specified data with IMAP modified UTF7 encoding. Defined in RFC 3501 5.1.3.  Mailbox International Naming Convention.
        /// Example: �� is encoded to &amp;APYA9g-.
        /// </summary>
        /// <param name="text">Text to encode.</param>
        /// <returns></returns>
        public static string EncodeString(string text)
        {
            /* RFC 3501 5.1.3.  Mailbox International Naming Convention
                In modified UTF-7, printable US-ASCII characters, except for "&",
                represent themselves; that is, characters with octet values 0x20-0x25
                and 0x27-0x7e.  The character "&" (0x26) is represented by the
                two-octet sequence "&-".

                All other characters (octet values 0x00-0x1f and 0x7f-0xff) are
                represented in modified BASE64, with a further modification from
                [UTF-7] that "," is used instead of "/".  Modified BASE64 MUST NOT be
                used to represent any printing US-ASCII character which can represent
                itself.
				
                "&" is used to shift to modified BASE64 and "-" to shift back to
                US-ASCII.  There is no implicit shift from BASE64 to US-ASCII, and
                null shifts ("-&" while in BASE64; note that "&-" while in US-ASCII
                means "&") are not permitted.  However, all names start in US-ASCII,
                and MUST end in US-ASCII; that is, a name that ends with a non-ASCII
                ISO-10646 character MUST end with a "-").
            */

            // Base64 chars, except '/' is replaced with ','
            char[] base64Chars = new char[]{
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                '0','1','2','3','4','5','6','7','8','9','+',','
            }; //+

            MemoryStream retVal = new MemoryStream();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                // The character "&" (0x26) is represented by the two-octet sequence "&-".
                if (c == '&') //+
                { 
                    retVal.Write(new byte[] { (byte)'&', (byte)'-' }, 0, 2); //+
                }
                // It is allowed char, don't need to encode
                else if (c >= 0x20 && c <= 0x25 || c >= 0x27 && c <= 0x7E)
                {
                    retVal.WriteByte((byte)c);
                }
                // Not allowed char, encode it
                else
                {
                    // Superfluous shifts are not allowed. 
                    // For example: �� may not encoded as &APY-&APY-, but must be &APYA9g-.

                    // Get all continuous chars that need encoding and encode them as one block
                    MemoryStream encodeBlock = new MemoryStream();
                    for (int ic = i; ic < text.Length; ic++)
                    {
                        char cC = text[ic];

                        // Allowed char
                        if (cC >= 0x20 && cC <= 0x25 || cC >= 0x27 && cC <= 0x7E)
                        {
                            break;
                        }
                        else
                        {
                            encodeBlock.WriteByte((byte)((cC & 0xFF00) >> 8));
                            encodeBlock.WriteByte((byte)(cC & 0xFF));
                            i = ic;
                        }
                    }

                    // Ecode block
                    byte[] encodedData = Base64EncodeEx(encodeBlock.ToArray(), base64Chars, false);
                    retVal.WriteByte((byte)'&');
                    retVal.Write(encodedData, 0, encodedData.Length);
                    retVal.WriteByte((byte)'-');
                }
            }

            return System.Text.Encoding.Default.GetString(retVal.ToArray());
        }
        /// <summary>
        /// Decodes IMAP modified UTF7 encoded data. Defined in RFC 3501 5.1.3.  Mailbox International Naming Convention.
        /// Example: &amp;APYA9g- is decoded to ��.
        /// </summary>
        /// <param name="text">Text to encode.</param>
        /// <returns></returns>
        public static string DecodeString(string text)
        {
            /* RFC 3501 5.1.3.  Mailbox International Naming Convention
                In modified UTF-7, printable US-ASCII characters, except for "&",
                represent themselves; that is, characters with octet values 0x20-0x25
                and 0x27-0x7e.  The character "&" (0x26) is represented by the
                two-octet sequence "&-".

                All other characters (octet values 0x00-0x1f and 0x7f-0xff) are
                represented in modified BASE64, with a further modification from
                [UTF-7] that "," is used instead of "/".  Modified BASE64 MUST NOT be
                used to represent any printing US-ASCII character which can represent
                itself.
				
                "&" is used to shift to modified BASE64 and "-" to shift back to
                US-ASCII.  There is no implicit shift from BASE64 to US-ASCII, and
                null shifts ("-&" while in BASE64; note that "&-" while in US-ASCII
                means "&") are not permitted.  However, all names start in US-ASCII,
                and MUST end in US-ASCII; that is, a name that ends with a non-ASCII
                ISO-10646 character MUST end with a "-").
            */

            // Base64 chars, except '/' is replaced with ','
            char[] base64Chars = new char[]{
				'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
				'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
				'0','1','2','3','4','5','6','7','8','9','+',','
			};

            StringBuilder retVal = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                // Encoded block or escaped &
                if (c == '&')
                {
                    int endingPos = -1;
                    // Read encoded block
                    for (int b = i + 1; b < text.Length; b++)
                    {
                        // - marks block end
                        if (text[b] == '-')
                        {
                            endingPos = b;
                            break;
                        }
                        // Invalid & sequence, just treat it as '&' char and not like shift.
                        // &....&, but must be &....-
                        else if (text[b] == '&')
                        {
                            break;
                        }
                    }

                    // If no ending -, invalid encoded block. Treat it like it is
                    if (endingPos == -1)
                    {
                        // Just let main for to handle other chars after &
                        retVal.Append(c);
                    }
                    // If empty block, then escaped &
                    else if (endingPos - i == 1)
                    {
                        retVal.Append(c);
                        // Move i over '-'
                        i++;
                    }
                    // Decode block
                    else
                    {
                        // Get encoded block
                        byte[] encodedBlock = System.Text.Encoding.Default.GetBytes(text.Substring(i + 1, endingPos - i - 1));
                        // Convert to UTF-16 char						
                        byte[] decodedData = Base64DecodeEx(encodedBlock, base64Chars);
                        //String decodeString = text.Substring(i + 1, endingPos - i - 1);
                        //byte[] decodedData = Convert.FromBase64String(text.Substring(i + 1, endingPos - i - 1));
                        char[] decodedChars = new char[decodedData.Length / 2];
                        for (int iC = 0; iC < decodedChars.Length; iC++)
                        {
                            decodedChars[iC] = (char)(decodedData[iC * 2] << 8 | decodedData[(iC * 2) + 1]);
                        }

                        // Decode data
                        retVal.Append(decodedChars);

                        // Move i over '-'
                        i += encodedBlock.Length + 1;
                    }
                }
                // Normal byte
                else
                {
                    retVal.Append(c);
                }
            }

            return retVal.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Base64Encode(byte[] data)
		{
			return Base64EncodeEx(data,null,true);
		}
		/// <summary>
		/// Encodes specified data with bas64 encoding.
		/// </summary>
		/// <param name="data">Data to to encode.</param>
		/// <param name="base64Chars">Custom base64 chars (64 chars) or null if default chars used.</param>
		/// <param name="padd">Padd missing block chars. Normal base64 must be 4 bytes blocks, if not 4 bytes in block, 
		/// missing bytes must be padded with '='. Modified base64 just skips missing bytes.</param>
		/// <returns></returns>
		public static byte[] Base64EncodeEx(byte[] data,char[] base64Chars,bool padd)
		{
			/* RFC 2045 6.8.  Base64 Content-Transfer-Encoding
			
				Base64 is processed from left to right by 4 6-bit byte block, 4 6-bit byte block 
				are converted to 3 8-bit bytes.
				If base64 4 byte block doesn't have 3 8-bit bytes, missing bytes are marked with =. 
				
			
				Value Encoding  Value Encoding  Value Encoding  Value Encoding
					0 A            17 R            34 i            51 z
					1 B            18 S            35 j            52 0
					2 C            19 T            36 k            53 1
					3 D            20 U            37 l            54 2
					4 E            21 V            38 m            55 3
					5 F            22 W            39 n            56 4
					6 G            23 X            40 o            57 5
					7 H            24 Y            41 p            58 6
					8 I            25 Z            42 q            59 7
					9 J            26 a            43 r            60 8
					10 K           27 b            44 s            61 9
					11 L           28 c            45 t            62 +
					12 M           29 d            46 u            63 /
					13 N           30 e            47 v
					14 O           31 f            48 w         (pad) =
					15 P           32 g            49 x
					16 Q           33 h            50 y
					
				NOTE: 4 base64 6-bit bytes = 3 8-bit bytes				
					// |    6-bit    |    6-bit    |    6-bit    |    6-bit    |
					// | 1 2 3 4 5 6 | 1 2 3 4 5 6 | 1 2 3 4 5 6 | 1 2 3 4 5 6 |
					// |    8-bit         |    8-bit        |    8-bit         |
			*/

			if(base64Chars != null && base64Chars.Length != 64){
				throw new Exception("There must be 64 chars in base64Chars char array !");
			}

			if(base64Chars == null){
				base64Chars = new char[]{
					'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
					'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
					'0','1','2','3','4','5','6','7','8','9','+','/'
				};
			}

			// Convert chars to bytes
			byte[] base64LoockUpTable = new byte[64];
			for(int i=0;i<64;i++){
				base64LoockUpTable[i] = (byte)base64Chars[i];
			}
						
			int encodedDataLength = (int)Math.Ceiling((data.Length * 8) / (double)6);
			// Retrun value won't be interegral 4 block, but has less. Padding requested, padd missing with '='
			if(padd && (encodedDataLength / (double)4 != Math.Ceiling(encodedDataLength / (double)4))){
				encodedDataLength += (int)(Math.Ceiling(encodedDataLength / (double)4) * 4) - encodedDataLength;
			}

			// See how many line brakes we need
			int numberOfLineBreaks = 0;
			if(encodedDataLength > 76){
				numberOfLineBreaks = (int)Math.Ceiling(encodedDataLength / (double)76) - 1;
			}

			// Construc return valu buffer
			byte[] retVal = new byte[encodedDataLength + (numberOfLineBreaks * 2)];  // * 2 - CRLF

			int lineBytes = 0;
			// Loop all 3 bye blocks
			int position = 0; 
			for(int i=0;i<data.Length;i+=3){
				// Do line splitting
				if(lineBytes >= 76){
					retVal[position + 0] = (byte)'\r';
					retVal[position + 1] = (byte)'\n';					
					position += 2;
					lineBytes = 0;
				}

				// Full 3 bytes data block
				if((data.Length - i) >= 3){
					retVal[position + 0] = base64LoockUpTable[data[i + 0] >> 2];
					retVal[position + 1] = base64LoockUpTable[(data[i + 0] & 0x3) << 4 | data[i + 1] >> 4];
					retVal[position + 2] = base64LoockUpTable[(data[i + 1] & 0xF) << 2 | data[i + 2] >> 6];
					retVal[position + 3] = base64LoockUpTable[data[i + 2] & 0x3F];
					position += 4;
					lineBytes += 4;
				}
				// 2 bytes data block, left (last block)
				else if((data.Length - i) == 2){
					retVal[position + 0] = base64LoockUpTable[data[i + 0] >> 2];
					retVal[position + 1] = base64LoockUpTable[(data[i + 0] & 0x3) << 4 | data[i + 1] >> 4];
					retVal[position + 2] = base64LoockUpTable[(data[i + 1] & 0xF) << 2];					
					if(padd){
						retVal[position + 3] = (byte)'=';
					}
				}
				// 1 bytes data block, left (last block)
				else if((data.Length - i) == 1){
					retVal[position + 0] = base64LoockUpTable[data[i + 0] >> 2];
					retVal[position + 1] = base64LoockUpTable[(data[i + 0] & 0x3) << 4];					
					if(padd){
						retVal[position + 2] = (byte)'=';
						retVal[position + 3] = (byte)'=';
					}
				}
			}

			return retVal;
		}
		/// <summary>
		/// Decodes base64 data. Defined in RFC 2045 6.8.  Base64 Content-Transfer-Encoding.
		/// </summary>
		/// <param name="base64Data">Base64 decoded data.</param>
		/// <returns></returns>
		public static byte[] Base64Decode(byte[] base64Data)
		{
			return Base64DecodeEx(base64Data,null);
		}
		/// <summary>
		/// Decodes base64 data. Defined in RFC 2045 6.8.  Base64 Content-Transfer-Encoding.
		/// </summary>
		/// <param name="base64Data">Base64 decoded data.</param>
		/// <param name="base64Chars">Custom base64 chars (64 chars) or null if default chars used.</param>
		/// <returns></returns>
		public static byte[] Base64DecodeEx(byte[] base64Data,char[] base64Chars)
		{
			/* RFC 2045 6.8.  Base64 Content-Transfer-Encoding
			
				Base64 is processed from left to right by 4 6-bit byte block, 4 6-bit byte block 
				are converted to 3 8-bit bytes.
				If base64 4 byte block doesn't have 3 8-bit bytes, missing bytes are marked with =. 
				
			
				Value Encoding  Value Encoding  Value Encoding  Value Encoding
					0 A            17 R            34 i            51 z
					1 B            18 S            35 j            52 0
					2 C            19 T            36 k            53 1
					3 D            20 U            37 l            54 2
					4 E            21 V            38 m            55 3
					5 F            22 W            39 n            56 4
					6 G            23 X            40 o            57 5
					7 H            24 Y            41 p            58 6
					8 I            25 Z            42 q            59 7
					9 J            26 a            43 r            60 8
					10 K           27 b            44 s            61 9
					11 L           28 c            45 t            62 +
					12 M           29 d            46 u            63 /
					13 N           30 e            47 v
					14 O           31 f            48 w         (pad) =
					15 P           32 g            49 x
					16 Q           33 h            50 y
					
				NOTE: 4 base64 6-bit bytes = 3 8-bit bytes				
					// |    6-bit    |    6-bit    |    6-bit    |    6-bit    |
					// | 1 2 3 4 5 6 | 1 2 3 4 5 6 | 1 2 3 4 5 6 | 1 2 3 4 5 6 |
					// |    8-bit         |    8-bit        |    8-bit         |
			*/
			
			if(base64Chars != null && base64Chars.Length != 64){
				throw new Exception("There must be 64 chars in base64Chars char array !");
			}

			if(base64Chars == null){
				base64Chars = new char[]{
					'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
					'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
					'0','1','2','3','4','5','6','7','8','9','+','/'
				};
			}

			//--- Create decode table ---------------------//
			byte[] decodeTable = new byte[128];
			for(int i=0;i<128;i++){
				int mappingIndex = -1;
				for(int bc=0;bc<base64Chars.Length;bc++){
					if(i == base64Chars[bc]){
						mappingIndex = bc;
						break;
					}
				}

				if(mappingIndex > -1){
					decodeTable[i] = (byte)mappingIndex;
				}
				else{
					decodeTable[i] = 0xFF;
				}
			}
			//---------------------------------------------//

			byte[] decodedDataBuffer  = new byte[((base64Data.Length * 6) / 8) + 4];
			int    decodedBytesCount  = 0;
			int    nByteInBase64Block = 0;
			byte[] decodedBlock       = new byte[3];
			byte[] base64Block        = new byte[4];

			for(int i=0;i<base64Data.Length;i++){
				byte b = base64Data[i];

				// Read 4 byte base64 block and process it 			
				// Any characters outside of the base64 alphabet are to be ignored in base64-encoded data.

				// Padding char
				if(b == '='){
					base64Block[nByteInBase64Block] = 0xFF;
				}
				else{
					byte decodeByte = decodeTable[b & 0x7F];
					if(decodeByte != 0xFF){
						base64Block[nByteInBase64Block] = decodeByte;
						nByteInBase64Block++;
					}
				}

                /* Check if we can decode some bytes. 
                 * We must have full 4 byte base64 block or reached at the end of data.
                 */
                int encodedBytesCount = -1;
                // We have full 4 byte base64 block
                if(nByteInBase64Block == 4){
                    encodedBytesCount = 3;
                }
                // We have reached at the end of base64 data, there may be some bytes left
                else if(i == base64Data.Length - 1){
                    // Invalid value, we can't have only 6 bit, just skip 
                    if(nByteInBase64Block == 1){
                        encodedBytesCount = 0;
                    }
                    // There is 1 byte in two base64 bytes (6 + 2 bit)
                    else if(nByteInBase64Block == 2){
                        encodedBytesCount = 1;
                    }
                    // There are 2 bytes in two base64 bytes ([6 + 2],[4 + 4] bit)
                    else if(nByteInBase64Block == 3){
                        encodedBytesCount = 2;
                    }
                }

                // We have some bytes available to decode, decode them
                if(encodedBytesCount > -1){
                    decodedDataBuffer[decodedBytesCount + 0] = (byte)((int)base64Block[0] << 2         | (int)base64Block[1] >> 4);
					decodedDataBuffer[decodedBytesCount + 1] = (byte)(((int)base64Block[1] & 0xF) << 4 | (int)base64Block[2] >> 2);
					decodedDataBuffer[decodedBytesCount + 2] = (byte)(((int)base64Block[2] & 0x3) << 6 | (int)base64Block[3] >> 0);

                    // Increase decoded bytes count
					decodedBytesCount += encodedBytesCount;

                    // Reset this block, reade next if there is any
					nByteInBase64Block = 0;
                }
			}

			// There is some decoded bytes, construct return value
			if(decodedBytesCount > -1){
				byte[] retVal = new byte[decodedBytesCount];
				Array.Copy(decodedDataBuffer,0,retVal,0,decodedBytesCount);
				return retVal;
			}
			// There is no decoded bytes
			else{
				return new byte[0];
			}
		}
    }

}
