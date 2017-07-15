using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Muzika
{
    class Program
    {
        static void Main(string[] args)
        {
            int menu = 1;
            int lentele = 1;
            Console.WriteLine("Sveiki atvyke i muzikos duomenu baze.");

            using (var db = new BloggingContext())
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=(localdb)\v11.0;Integrated Security=True";
                cn.Open();
                while (menu != 11)
                {
                    Console.WriteLine("\nIsrinkite ka norite daryti");
                    Console.WriteLine("1. Parodyti lentele Atlikejas");
                    Console.WriteLine("2. Parodyti lentele Albumas");
                    Console.WriteLine("3. Parodyti lentele Autorius");
                    Console.WriteLine("4. Parodyti lentele Daina");
                    Console.WriteLine("5. Parodyti lentele Priklauso");
                    Console.WriteLine("6. CRUD");
                    Console.WriteLine("7. Ideti duomenys");
                    Console.WriteLine("8. Atnaujinti duomenys");
                    Console.WriteLine("9. Istrinti duomenys");
                    Console.WriteLine("10. Parodyti papildomas lenteles");
                    Console.WriteLine("11. Baigti programos darba");
                    Console.Write("Menu : ");
                    Int32.TryParse(System.Console.ReadLine(), out menu);
                    System.Console.WriteLine();

                    switch (menu)
                    {
                        case 1:
                            {
                                SqlDataAdapter da = new SqlDataAdapter("SELECT Slapyvardis, Karjeros_pradzia, Tautybe From Atlikejai", cn);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "Atlikejai");
                                int[] sizes = {-15, -16, -10, 0};
                                ParodytiLentele(ds, sizes);
                                da.Dispose();
                                break;
                            }

                        case 2:
                            {
                                SqlDataAdapter da = new SqlDataAdapter("SELECT Pavadinimas, Turinis_Min, Atlikejas From Albumai", cn);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "Albumai");
                                int[] sizes = {-25, -11, -15, 0};
                                ParodytiLentele(ds, sizes);
                                da.Dispose();
                                break;
                            }

                        case 3:
                            {
                                SqlDataAdapter da = new SqlDataAdapter("SELECT Nr, Slapyvardis, Tautybe, Karjeros_pradzia From Autoriai", cn);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "Autoriai");
                                int[] sizes = {-3, -25, -10, -16};
                                ParodytiLentele(ds, sizes);
                                da.Dispose();
                                break;
                            }

                        case 4:
                            {
                                SqlDataAdapter da = new SqlDataAdapter("SELECT Pavadinimas, Trukme_Min, Zanras, Autorius From Dainos", cn);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "Dainos");
                                int[] sizes = {-25, -10, -15, -11};
                                ParodytiLentele(ds, sizes);
                                da.Dispose();
                                break;
                            }

                        case 5:
                            {
                                SqlDataAdapter da = new SqlDataAdapter("SELECT PriklausoNr, Albumo_Pavadinimas, Dainos_Pavadinimas From Priklausantys", cn);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "Priklausantys");
                                int[] sizes = { -11, -25, -25, 0 };
                                ParodytiLentele(ds, sizes);
                                da.Dispose();
                                break;
                            }

                        case 6:
                            {
                                SqlCommand insert = new SqlCommand();
                                insert.Connection = cn;
                                insert.CommandType = CommandType.Text;
                                insert.CommandText = "INSERT INTO Atlikejai VALUES('Dainininkas', 1972, 'Lietuvis')";
                                SqlDataAdapter da2 = new SqlDataAdapter("SELECT Slapyvardis, Karjeros_pradzia, Tautybe From Atlikejai", cn);
                                da2.InsertCommand = insert;
                                DataSet ds2 = new DataSet();
                                da2.Fill(ds2, "Atlikejai");

                                DataRow newRow = ds2.Tables[0].NewRow();
                                ds2.Tables[0].Rows.Add(newRow);

                                da2.Update(ds2.Tables[0]);

                                Console.WriteLine("Ivedeme");

                                da2.Dispose();

                             /*   SqlCommand update = new SqlCommand();
                                update.Connection = cn;
                                update.CommandType = CommandType.Type*/


                                break;
                            }

                        case 7:
                            {
                                int isrinkta = 1;
                                while (isrinkta != 0)
                                {
                                    Console.WriteLine("Isrinkite lentele");
                                    Console.WriteLine("1. Atlikejas");
                                    Console.WriteLine("2. Albumas");
                                    Console.WriteLine("3. Autorius");
                                    Console.WriteLine("4. Daina");
                                    Console.WriteLine("5. Priklauso");
                                    Console.Write("Lentele : ");
                                    Int32.TryParse(System.Console.ReadLine(), out lentele);
                                    System.Console.WriteLine();

                                    if (lentele == 1 || lentele == 2 || lentele == 3 || lentele == 4 || lentele == 5)
                                    {
                                        isrinkta = 0;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Nera tokios lenteles. Bandykite dar karta.");
                                        System.Console.WriteLine();
                                    }
                                }

                                using (var db2 = new BloggingContext())
                                {
                                    switch (lentele)
                                    {
                                        case 1:
                                            {
                                                try
                                                {
                                                    Console.Write("Iveskite atlikejo varda: ");
                                                    string slapyvardis = Console.ReadLine();
                                                    Console.Write("Iveskite atlikejo karjeros pradzios metus: ");
                                                    int karjeros_pradzia;
                                                    Int32.TryParse(Console.ReadLine(), out karjeros_pradzia);
                                                    Console.Write("Iveskite atlikejo tautybes: ");
                                                    string tautybe = Console.ReadLine();
                                                    var atlikejas = new Atlikejas
                                                    {
                                                        Slapyvardis = slapyvardis,
                                                        Karjeros_pradzia = karjeros_pradzia,
                                                        Tautybe = tautybe
                                                    };
                                                    db2.Atlikejai.Add(atlikejas);
                                                    db2.SaveChanges();
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine("Atlikejas su tokiu slapyvardziu jau buvo itrauktas arba pazeidetete kitus reikalavimus");
                                                }
                                                break;
                                            }

                                        case 2:
                                            {
                                                try
                                                {
                                                    Console.Write("Iveskite albumo pavadinima: ");
                                                    string pavadinimas = Console.ReadLine();
                                                    Console.Write("Iveskite albumo turini minutemis: ");
                                                    double turinis_min;
                                                    double.TryParse(Console.ReadLine(), out turinis_min);
                                                    Console.Write("Iveskite albumo atlikejo slapyvardi: ");
                                                    string atlikejas = Console.ReadLine();
                                                    var albumas = new Albumas
                                                    {
                                                        Pavadinimas = pavadinimas,
                                                        Turinis_Min = turinis_min,
                                                        Atlikejas = atlikejas
                                                    };
                                                    db2.Albumai.Add(albumas);
                                                    db2.SaveChanges();
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine("Albumas su tokiu pavadinimu jau buvo itrauktas arba nera tokio atlikejo  arba pazeidetete kitus reikalavimus");
                                                }
                                                break;
                                            }

                                        case 3:
                                            {
                                                try
                                                {
                                                    Console.Write("Iveskite autoriaus slapyvardi: ");
                                                    string slapyvardis = Console.ReadLine();
                                                    Console.Write("Iveskite autoriaus tautybe: ");
                                                    //Galima paziureti ar buvo toks slapyvardis
                                                    string tautybe = Console.ReadLine();
                                                    Console.Write("Iveskite autoriaus karjeros pradzios metus: ");
                                                    int karjeros_pradzia;
                                                    Int32.TryParse(Console.ReadLine(), out karjeros_pradzia);

                                                    var autorius = new Autorius
                                                    {
                                                        Slapyvardis = slapyvardis,
                                                        Tautybe = tautybe,
                                                        Karjeros_pradzia = karjeros_pradzia
                                                    };
                                                    db2.Autoriai.Add(autorius);
                                                    db2.SaveChanges();
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine("Nepavyko ivesti tokio autoriaus");
                                                }
                                                break;
                                            }

                                        case 4:
                                            {
                                                try
                                                {
                                                    Console.Write("Iveskite dainos pavadinima: ");
                                                    string pavadinimas = Console.ReadLine();
                                                    Console.Write("Iveskite dainos trukme minutemis: ");
                                                    Double trukme_min;
                                                    Double.TryParse(Console.ReadLine(), out trukme_min);
                                                    //Galima paziureti ar buvo toks slapyvardis
                                                    Console.Write("Iveskite Dainos zanra: ");
                                                    string zanras = Console.ReadLine();
                                                    Console.Write("Iveskite Dainos autoriaus numeri: ");
                                                    int autorius;
                                                    Int32.TryParse(Console.ReadLine(), out autorius);

                                                    var daina = new Daina
                                                    {
                                                        Pavadinimas = pavadinimas,
                                                        Trukme_Min = trukme_min,
                                                        Zanras = zanras,
                                                        Autorius = autorius
                                                    };
                                                    db2.Dainos.Add(daina);
                                                    db2.SaveChanges();
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine("Daina su tokiu pavadinimu jau buvo itraukta arba nera tokio autoriaus  arba pazeidetete kitus reikalavimus");
                                                }
                                                break;
                                            }

                                        case 5:
                                            {
                                                try
                                                {
                                                    Console.Write("Iveskite albumo pavadinima: ");
                                                    string albumo_pavadinimas = Console.ReadLine();
                                                    Console.Write("Iveskite dainos pavadinima: ");
                                                    string dainos_pavadinimas = Console.ReadLine();

                                                    var priklauso = new Priklauso
                                                    {
                                                        Albumo_Pavadinimas = albumo_pavadinimas,
                                                        Dainos_Pavadinimas = dainos_pavadinimas
                                                    };
                                                    db2.Priklausantys.Add(priklauso);
                                                    db2.SaveChanges();
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine("Nera tokio albumo arba tokios dainos  arba pazeidetete kitus reikalavimus");
                                                }
                                                break;
                                            }
                                    }
                                }
                                break;
                            }

                        case 8:
                            {
                                int isrinkta = 1;
                                while (isrinkta != 0)
                                {
                                    Console.WriteLine("Isrinkite lentele");
                                    Console.WriteLine("1. Atlikejas");
                                    Console.WriteLine("2. Albumas");
                                    Console.WriteLine("3. Autorius");
                                    Console.WriteLine("4. Daina");
                                    Console.WriteLine("5. Priklauso");
                                    Console.Write("Lentele : ");
                                    Int32.TryParse(System.Console.ReadLine(), out lentele);
                                    System.Console.WriteLine();

                                    if (lentele == 1 || lentele == 2 || lentele == 3 || lentele == 4 || lentele == 5)
                                    {
                                        isrinkta = 0;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Nera tokios lenteles. Bandykite dar karta.");
                                        System.Console.WriteLine();
                                    }
                                }

                                switch (lentele)
                                {
                                    case 1:
                                        {
                                            try
                                            {
                                                Console.Write("Prasome nurodyti koki atlikeja norite pakeisti: ");
                                                string atlikejas = Console.ReadLine();
                                                var eile = db.Atlikejai.Find(atlikejas);
                                                if (eile != null)
                                                {
                                                    Console.Write("Iveskite atlikejo karjeros pradzios metus: ");
                                                    int karjeros_pradzia;
                                                    Int32.TryParse(Console.ReadLine(), out karjeros_pradzia);
                                                    Console.Write("Iveskite atlikejo tautybes: ");
                                                    string tautybe = Console.ReadLine();

                                                    eile.Karjeros_pradzia = karjeros_pradzia;
                                                    eile.Tautybe = tautybe;
                                                    db.SaveChanges();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nera tokio atlikejo");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Klaida bandant atnaujinti lentele atlikejas");
                                            }
                                            break;
                                        }

                                    case 2:
                                        {
                                            try
                                            {
                                                Console.Write("Prasome nurodyti koki albuma norite pakeisti: ");
                                                string albumas = Console.ReadLine();
                                                var eile = db.Albumai.Find(albumas);
                                                if (eile != null)
                                                {
                                                    Console.Write("Iveskite albumo turini minutemis: ");
                                                    double turinis_min;
                                                    double.TryParse(Console.ReadLine(), out turinis_min);
                                                    Console.Write("Iveskite albumo atlikejo slapyvardi: ");
                                                    string atlikejas = Console.ReadLine();

                                                    eile.Turinis_Min = turinis_min;
                                                    eile.Atlikejas = atlikejas;
                                                    db.SaveChanges();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nera tokio albumo");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Klaida bandant atnaujinti lentele albumas, greiciausiai nurodete neegzistuojanti atlikeja");
                                            }
                                            break;
                                        }

                                    case 3:
                                        {
                                            try
                                            {
                                                Console.Write("Prasome nurodyti koki autoriu norite pakeisti: ");
                                                int autorius;
                                                Int32.TryParse(Console.ReadLine(), out autorius);
                                                var eile = db.Autoriai.Find(autorius);
                                                if (eile != null)
                                                {
                                                    Console.Write("Iveskite autoriaus tautybe: ");
                                                    string tautybe = Console.ReadLine();
                                                    Console.Write("Iveskite autoriaus karjeros pradzios metus: ");
                                                    int karjeros_pradzia;
                                                    Int32.TryParse(Console.ReadLine(), out karjeros_pradzia);

                                                    eile.Tautybe = tautybe;
                                                    eile.Karjeros_pradzia = karjeros_pradzia;
                                                    db.SaveChanges();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nera tokio Autoriaus");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Klaida bandant atnaujinti lentele autorius");
                                            }
                                            break;
                                        }

                                    case 4:
                                        {
                                            try
                                            {
                                                Console.Write("Prasome nurodyti kokia daina norite pakeisti: ");
                                                string daina = Console.ReadLine();
                                                var eile = db.Dainos.Find(daina);
                                                if (eile != null)
                                                {
                                                    Console.Write("Iveskite dainos trukme minutemis: ");
                                                    Double trukme_min;
                                                    Double.TryParse(Console.ReadLine(), out trukme_min);
                                                    //Galima paziureti ar buvo toks slapyvardis
                                                    Console.Write("Iveskite Dainos zanra: ");
                                                    string zanras = Console.ReadLine();
                                                    Console.Write("Iveskite Dainos autoriaus numeri: ");
                                                    int autorius;
                                                    Int32.TryParse(Console.ReadLine(), out autorius);

                                                    eile.Trukme_Min = trukme_min;
                                                    eile.Zanras = zanras;
                                                    eile.Autorius = autorius;
                                                    db.SaveChanges();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nera tokios dainos");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Klaida bandant atnaujinti lentele daina, greiciausiai nurodote neegzistuojacio autoriaus numeri");
                                            }
                                            break;
                                        }

                                    case 5:
                                        {
                                            try
                                            {
                                                Console.Write("Prasome nurodyti kokia priklausomybe norite pakeisti: ");
                                                int priklausomybe;
                                                Int32.TryParse(Console.ReadLine(), out priklausomybe);
                                                var eile = db.Priklausantys.Find(priklausomybe);
                                                if (eile != null)
                                                {
                                                    Console.Write("Iveskite albumo pavadinima: ");
                                                    string albumo_pavadinimas = Console.ReadLine();
                                                    Console.Write("Iveskite dainos pavadinima: ");
                                                    string dainos_pavadinimas = Console.ReadLine();

                                                    eile.Albumo_Pavadinimas = albumo_pavadinimas;
                                                    eile.Dainos_Pavadinimas = dainos_pavadinimas;
                                                    db.SaveChanges();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nera tokios priklausomybes");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Klaida bandant atnaujinti lentele priklausomybe, \ngreiciausiai nurodote neegzistuojati albumo pavadinima arba dainos pavadinima");
                                            }
                                            break;
                                        }
                                }
                                break;
                            }

                        case 9:
                            {
                                int isrinkta = 1;
                                while (isrinkta != 0)
                                {
                                    Console.WriteLine("Isrinkite lentele");
                                    Console.WriteLine("1. Atlikejas");
                                    Console.WriteLine("2. Albumas");
                                    Console.WriteLine("3. Autorius");
                                    Console.WriteLine("4. Daina");
                                    Console.WriteLine("5. Priklauso");
                                    Console.Write("Lentele : ");
                                    Int32.TryParse(System.Console.ReadLine(), out lentele);
                                    System.Console.WriteLine();

                                    if (lentele == 1 || lentele == 2 || lentele == 3 || lentele == 4 || lentele == 5)
                                    {
                                        isrinkta = 0;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Nera tokios lenteles. Bandykite dar karta.");
                                    }
                                    System.Console.WriteLine();
                                }

                                switch (lentele)
                                {
                                    case 1:
                                        {
                                            try
                                            {
                                                Console.Write("Prasome nurodyti koki atlikeja norite istrinti: ");
                                                string istrinti = Console.ReadLine();
                                                var eile = db.Atlikejai.Find(istrinti);
                                                if (eile != null)
                                                {
                                                    db.Atlikejai.Remove(eile);
                                                    db.SaveChanges();
                                                    Console.WriteLine("Eilute buvo sekmingai istrinta");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nera tokio atlikejo");
                                                }
                                            }
                                            catch(Exception e)
                                            {
                                                Console.WriteLine("Klaida trinant eilute, greiciausiai eilute yra naudojama kitos lenteles");
                                            }
                                            break;
                                        }

                                    case 2:
                                        {
                                            try
                                            {
                                                Console.Write("Prasome nurodyti koki albuma norite istrinti: ");
                                                string istrinti = Console.ReadLine();
                                                var eile = db.Albumai.Find(istrinti);
                                                if (eile != null)
                                                {
                                                    db.Albumai.Remove(eile);
                                                    db.SaveChanges();
                                                    Console.WriteLine("Eilute buvo sekmingai istrinta");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nera tokio albumo");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Klaida trinant eilute, greiciausiai eilute yra naudojama kitos lenteles");
                                            }
                                            break;
                                        }

                                    case 3:
                                        {
                                            try
                                            {
                                                Console.Write("Prasome nurodyti koki autoriu norite istrinti: ");
                                                int istrinti;
                                                Int32.TryParse(Console.ReadLine(), out istrinti);
                                                var eile = db.Autoriai.Find(istrinti);
                                                if (eile != null)
                                                {
                                                    db.Autoriai.Remove(eile);
                                                    db.SaveChanges();
                                                    Console.WriteLine("Eilute buvo sekmingai istrinta");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nera tokio autoriaus");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Klaida trinant eilute, greiciausiai eilute yra naudojama kitos lenteles");
                                            }
                                            break;
                                        }

                                    case 4:
                                        {
                                            try
                                            {
                                                Console.Write("Prasome nurodyti kokia daina norite istrinti: ");
                                                string istrinti = Console.ReadLine();
                                                var eile = db.Dainos.Find(istrinti);
                                                if (eile != null)
                                                {
                                                    db.Dainos.Remove(eile);
                                                    db.SaveChanges();
                                                    Console.WriteLine("Eilute buvo sekmingai istrinta");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nera tokios dainos");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Klaida trinant eilute, greiciausiai eilute yra naudojama kitos lenteles");
                                            }
                                            break;
                                        }

                                    case 5:
                                        {
                                            try
                                            {
                                                Console.Write("Prasome nurodyti kokia priklausomyve norite istrinti: ");
                                                int istrinti;
                                                Int32.TryParse(Console.ReadLine(), out istrinti);
                                                var eile = db.Priklausantys.Find(istrinti);
                                                if (eile != null)
                                                {
                                                    db.Priklausantys.Remove(eile);
                                                    db.SaveChanges();
                                                    Console.WriteLine("Eilute buvo sekmingai istrinta");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nera tokios priklausomybes");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Klaida trinant eilute, greiciausiai eilute yra naudojama kitos lenteles");
                                            }
                                            break;
                                        }
                                }
                                break;
                            }

                        case 10:
                            {
                                int isrinkta = 1;
                                while (isrinkta != 0)
                                {
                                    Console.WriteLine("Pasirinkite kokia papildoma lentele norite pamatyti");
                                    Console.WriteLine("1. Lentele dainu pagal nurodyta zanra");
                                    Console.WriteLine("2. Lentele albumu ir ju atlikeju tautybe");
                                    Console.WriteLine("3. Autoriu albumu sarasas");
                                    Console.Write("Lentele : ");
                                    Int32.TryParse(System.Console.ReadLine(), out lentele);
                                    System.Console.WriteLine();

                                    if (lentele == 1 || lentele == 2 || lentele == 3)
                                    {
                                        isrinkta = 0;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Nera tokios lenteles. Bandykite dar karta.");
                                        System.Console.WriteLine();
                                    }
                                }

                                switch(lentele)
                                {
                                    case 1:
                                        {
                                            SqlDataAdapter da = new SqlDataAdapter();

                                            SqlCommand command = new SqlCommand("SELECT Pavadinimas, Trukme_Min, Zanras, Autorius From Dainos" + " WHERE Zanras = @Z", cn);
                                            Console.Write("Zanras: ");
                                            string zanras = Console.ReadLine();
                                            command.Parameters.AddWithValue("@Z", zanras);
                                            da.SelectCommand = command;
                                            DataSet ds = new DataSet();
                                            da.Fill(ds, "Dainos");

                                            int[] sizes = {-25, -10, -15, -11};
                                            ParodytiLentele(ds, sizes);
                                            da.Dispose();
                                            break;
                                        }

                                    case 2:
                                        {
                                            var albumaiPagalTautybe = from at in db.Atlikejai
                                                                      join al in db.Albumai
                                                                      on at.Slapyvardis equals al.Atlikejas into albumuGroup
                                                                      from albumas in albumuGroup
                                                                      select new { albumas.Pavadinimas, at.Slapyvardis, at.Tautybe };

                                            Console.Write("|{0,-25}|", "Albumo pavadinimas");
                                            Console.Write("|{0,-15}|", "Atlikejas");
                                            Console.Write("|{0,-10}|", "Tautybe");
                                            Console.WriteLine("\n--------------------------------------------------------");
                                            foreach(var albumas in albumaiPagalTautybe)
                                            {
                                                Console.Write("|{0,-25}|", albumas.Pavadinimas);
                                                Console.Write("|{0,-15}|", albumas.Slapyvardis);
                                                Console.Write("|{0,-10}|\n", albumas.Tautybe);
                                            }
                                            break;
                                        }

                                   case 3:
                                        {
                                            Console.Write("Kiek autoriu norite matyti: ");
                                            int paimti;
                                            Int32.TryParse(Console.ReadLine(), out paimti);
                                            Console.WriteLine();
                                            var albumaiPagalTautybe = db.Atlikejai.GroupJoin(db.Albumai,
                                                                                        at => at.Slapyvardis,
                                                                                        al => al.Atlikejas,
                                                                                        (at, albumuGroup) => albumuGroup.Select(al => new
                                                                                        {
                                                                                            Pavadinimas = al.Pavadinimas, 
                                                                                            Slapyvardis = at.Slapyvardis,
                                                                                            Tautybe = at.Tautybe 
                                                                                        })).SelectMany(alb => alb);
                                            var AutoriaiPagalAlbumuSkaicius = albumaiPagalTautybe.GroupBy((a) => (a.Slapyvardis)).Take(paimti);
                                            
                                            foreach (var autoriusGroup in AutoriaiPagalAlbumuSkaicius)
                                            {
                                                Console.WriteLine(autoriusGroup.Key + ": " + autoriusGroup.Count());
                                                foreach(var autorius in autoriusGroup)
                                                {
                                                    Console.WriteLine(" " + autorius.Pavadinimas);
                                                }
                                            }

                                                
                                          /*  Console.Write("|{0,-25}|", "Albumo pavadinimas");
                                            Console.Write("|{0,-15}|", "Atlikejas");
                                            Console.Write("|{0,-10}|", "Tautybe");
                                            Console.WriteLine("\n--------------------------------------------------------");
                                            foreach (var albumas in albumaiPagalTautybe)
                                            {
                                                Console.Write("|{0,-25}|", albumas.Pavadinimas);
                                                Console.Write("|{0,-15}|", albumas.Slapyvardis);
                                                Console.Write("|{0,-10}|\n", albumas.Tautybe);
                                            }*/
                                            break;
                                        }
                                }
                                break;
                            }
                            
                        case 11:
                            {
                                Console.WriteLine("Viso gero!");
                                Console.ReadKey();
                                break;
                            }

                        default:
                            {
                                System.Console.WriteLine("Buvo ivesta bloga opcija, prasome bandyti dar karta");
                                break;
                            }
                    }
                }
                cn.Close();
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