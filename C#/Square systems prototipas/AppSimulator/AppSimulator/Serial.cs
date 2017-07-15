using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AppSimulator
{
	public static class Serial
	{
		public static byte[] ObjectToByteArray(Object obj)
		{
			if (obj == null) return null;

			BinaryFormatter bf = new BinaryFormatter();
			using (MemoryStream ms = new MemoryStream())
			{
				bf.Serialize(ms, obj);
				return ms.ToArray();
			}
		}

		public static Common.Message ByteArrayToObject(byte[] byteArr)
		{
			BinaryFormatter bf = new BinaryFormatter();
			using (var ms = new MemoryStream(byteArr))
			{
				return (Common.Message)bf.Deserialize(ms);
			}
		}
	}
}
