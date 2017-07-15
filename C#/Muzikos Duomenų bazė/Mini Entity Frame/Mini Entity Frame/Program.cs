using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Mini_Entity_Frame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Iveskite dainos pavadinima: ");
            string pav = Console.ReadLine();
            Console.Write("Iveskite dainos trukme: ");
            int tr = 0;
            

            Int32.TryParse(Console.ReadLine(), out tr);
            Console.Write("Iveskite kuri updatinsime ir po to trinsime: ");
            int n = 0;
            Int32.TryParse(Console.ReadLine(), out n);
            using (var db = new miniEntities())
            {
                /*SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dominik\Documents\mini.mdf;Integrated Security=True;Connect Timeout=30";
                cn.Open();*/
                //db.Database.Connection.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dominik\Documents\mini.mdf;Integrated Security=True;Connect Timeout=30";
                
                try
                {
                    foreach (Daina daina in db.Dainas)
                    {
                        Console.WriteLine("id: {0}, pavadinimas {1}, trukme {2}, autorius {3}", daina.Id, daina.Pavadinimas, daina.Trukme, daina.Autoriu.Autorius);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Klaida printinant");
                }



                try
                {
                    Console.WriteLine("Bandome ideti");
                    var daina = new Daina
                    {
                        Pavadinimas = pav,
                        Trukme = tr,                        
                    };
                    var autorius = db.Autorius.FirstOrDefault();
                    autorius.Dainas.Add(daina);
                    //db.Dainas.Add(daina);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error idedant");
                }

                try
                {
                    foreach (Daina daina in db.Dainas)
                    {
                        Console.WriteLine("id: {0}, pavadinimas {1}, trukme {2}, autorius {3}", daina.Id, daina.Pavadinimas, daina.Trukme, daina.Autoriu.Autorius);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Klaida printinant");
                }
                try
                {
                    Console.WriteLine("Bandome updatinti ideta");
                    var daina = db.Dainas.Find(n);
                    Console.Write("Iveskite nauja pavadinima: ");
                    pav = Console.ReadLine();
                    Console.Write("Iveskite nauja trukme: ");
                    Int32.TryParse(Console.ReadLine(), out tr);
                    daina.Pavadinimas = pav;
                    daina.Trukme = tr;

                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Klaida bandant atnaujinti lentele");
                }

                try
                {
                    foreach (Daina daina in db.Dainas)
                    {
                        Console.WriteLine("id: {0}, pavadinimas {1}, trukme {2}, autorius {3}", daina.Id, daina.Pavadinimas, daina.Trukme, daina.Autoriu.Autorius);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Klaida printinant");
                }

                try
                {
                    Console.WriteLine("Bandome istrinti ideta");
                    var daina = db.Dainas.Find(n);
                    db.Dainas.Remove(daina);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Klaida bandant istrinti");
                }

                try
                {
                    foreach (Daina daina in db.Dainas)
                    {
                        Console.WriteLine("id: {0}, pavadinimas {1}, trukme {2}, autorius {3}", daina.Id, daina.Pavadinimas, daina.Trukme, daina.Autoriu.Autorius);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Klaida printinant");
                }
                Console.ReadLine();



                {

                    SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dominik\Documents\mini.mdf;Integrated Security=True;Connect Timeout=30";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT Pavadinimas, Trukme From Dainas", cn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Dainas");
                    int[] sizes = { -15, -16, -10, 0 };
                    ParodytiLentele(ds, sizes);
                    da.Dispose();
                }
            }
        }
        private static void ParodytiLentele(DataSet ds, int[] size)
        {
            int j = 0;
            int sum = size.Sum() * -1;
            foreach (DataColumn column in ds.Tables[0].Columns)
            {
                string format = string.Format("|{{0,{0}}}|", size[j]);
                Console.Write(String.Format(format, column));
                j++;
            }

            Console.WriteLine();
            for (int i = 0; i < (sum + ds.Tables[0].Columns.Count * 2); i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                j = 0;
                foreach (DataColumn column in ds.Tables[0].Columns)
                {
                    string format = string.Format("|{{0,{0}}}|", size[j]);
                    Console.Write(String.Format(format, row[column]));
                    j++;
                }
                Console.WriteLine();
            }
        }
      
    }
}
