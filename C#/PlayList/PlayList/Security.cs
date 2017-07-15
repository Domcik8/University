using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace PlayList
{
    class Protection
    {
        string input;
        private string name{ get; set; }
        private string defaultPath = @"C:\Users\Dominik\Desktop\VU\3 semestras\Taikomasis objektinis programavimas\PlayList\PlayList\";
        private string output = @"C:\Users\Dominik\Desktop\VU\3 semestras\Taikomasis objektinis programavimas\PlayList\PlayList\Rez.txt";
        public string Output
        {
            get
            {
                return output;
            }
            set
            {
                output = defaultPath + value + ".txt";
            }
        }

        public void setInput(string input)
        {
            Boolean setName = false;
            this.input = input;
            while (!setName)
            {
                System.Console.WriteLine("Enter the name of txt file where new playlist should be saved");
                name = System.Console.ReadLine();

                if (Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
                {
                    setName = true;
                    System.Console.WriteLine("Name has been accepted\n");
                }
                else
                {
                    System.Console.WriteLine("Name can have only letters and numbers");
                    continue;
                }
            }
            Output = name;
        }

        public string CreateKey()
        {
            string key = "1111";
            Boolean setKey = false;
            while (!setKey)
            {
                System.Console.WriteLine("Enter 4 letter/digit key for encryption/decryption : ");
                key = System.Console.ReadLine();
                if (key.Length != 4)
                {
                    System.Console.WriteLine("Key's lenght has to be 4");
                    continue;
                }
                if (Regex.IsMatch(key, @"^[a-zA-Z0-9]+$"))
                {
                    setKey = true;
                    System.Console.WriteLine("Key has been accepted\n");
                    key = key + key + key + key;
                }
                else
                {
                    System.Console.WriteLine("Key can have only letters and numbers");
                    continue;
                }
            }
            return key;
        }

        public void EncryptFile(string skey, MyList playList, string[] selectedGenres, string selectedMood)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(output, FileMode.Create))
                    {
                        /*The using statement calls the Dispose method on the object in the correct way, 
                        * and (when you use it as shown earlier) it also causes the object itself to go out of scope as soon as Dispose is called. 
                        * Within the using block, the object is read-only and cannot be modified or reassigned.*/
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                foreach (string genre in selectedGenres)
                                {
                                    var songQuery =
                                        from song in playList
                                        where ((song.genre == genre) && ((song.mood == selectedMood) || (selectedMood == "Any")))
                                        select song;
                                    foreach (Song song in songQuery)
                                    {
                                        //Rodo ka uzkoduoju
                                        //Console.WriteLine(song.Nr.ToString("D4") + " " + song.name + song.genre + song.mood);
                                        foreach (byte part in System.Text.Encoding.ASCII.GetBytes(song.Nr.ToString("D4")))
                                            cs.WriteByte(part);
                                        foreach (byte part in System.Text.Encoding.ASCII.GetBytes(" "))
                                            cs.WriteByte(part);
                                        foreach (byte part in System.Text.Encoding.ASCII.GetBytes(song.name))
                                        {
                                            cs.WriteByte(part);
                                        }
                                        foreach (byte part in System.Text.Encoding.ASCII.GetBytes(song.genre))
                                            cs.WriteByte(part);
                                        foreach (byte part in System.Text.Encoding.ASCII.GetBytes(song.mood))
                                            cs.WriteByte(part);
                                        cs.WriteByte(10);
                                    }
                                }
                            }
                        }
                    }
                }
                System.Console.WriteLine("Play list was created successfully");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error while encrypting");
            }
        }

        public void DecryptFile(string skey)
        {
            Boolean fileOpened = false;
            string input = @"C:\Users\Dominik\Desktop\VU\3 semestras\Taikomasis objektinis programavimas\PlayList\PlayList\";
            System.Console.WriteLine("Enter the name of txt file where encrypted play list is");

            while (!fileOpened)
            {
                name = System.Console.ReadLine();
                input = defaultPath + name + ".txt";

                byte[] buffer = new byte[60];

                try
                {
                    using (RijndaelManaged aes = new RijndaelManaged())
                    {
                        byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);
                        byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);
                        using (FileStream fsCrypt = new FileStream(input, FileMode.Open))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        System.Console.Write((char)data);
                                    }
                                }
                            }
                        }
                    }
                    fileOpened = true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("File could be found, please try again");
                }
            }
        }
    }
}