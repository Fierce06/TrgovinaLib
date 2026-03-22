using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrgovinaLib
{
    //dodaj se eno abstraktno metodo
    //uporabi indekser
    public abstract class Izdelek
    {
        protected static int globalID = 0;
        protected int id;
        protected string naziv;
        protected double cena;

        public int ID
        {
            get { return id; }
        }

        public string Naziv
        {
            get { return naziv; }
            set
            {
                if (value != "")
                {
                    naziv = value;
                }
                else
                {
                    naziv = "neimenovan izdelek";
                }
            }
        }
        public double Cena
        {
            get { return cena; }
            set
            {
                if (value > 0 && value < 100000)
                {
                    cena = value;
                }
            }
        }
        public Izdelek(string naziv, double cena)
        {
            Naziv = naziv;
            Cena = cena;
            id = ++globalID;
        }

        public abstract string VrstaIzdelka();

        public virtual string Izpis()
        {
            return "ID:" + id + ", Naziv:" + naziv + ", Cena: " + cena + " ";
        }
        public static double IzracunajDDV(double cena)
        {
            return cena * Trgovina.DDV;
        }
        public static double operator +(Izdelek a, Izdelek b)
        {
            return a.Cena + b.Cena;
        }

    }
    public interface IGarancija
    {
        int GarancijaMeseci { get; set; }

        string PrikaziGarancijo();
    }
    
    public class Telefon : Izdelek, IGarancija
    {
        private int kameraMP;
        private bool podpora5G;
        private int baterijaMAH;

        public int GarancijaMeseci { get; set; }

        public string PrikaziGarancijo()
        {
            return "Garancija: " + GarancijaMeseci + " mesecev";
        }
        public override string VrstaIzdelka()
        {
            return "Telefon";
        }

        public int KameraMP
        {
            get { return kameraMP; }
            set
            {
                if (value >= 8 && value <= 200)
                {
                    kameraMP = value;
                }
            }
        }
        public bool Podpora5G
        {
            get { return podpora5G; }
            set
            {
                podpora5G = value;
                if (podpora5G)
                {
                    Cena = Cena + 50;
                }
            }
        }
        public int BaterijaMAH
        {
            get { return baterijaMAH; }
            set
            {
                if (value >= 2000 && value <= 8000)
                {
                    baterijaMAH = value;
                }
            }
        }
        public Telefon(string naziv, double cena, int kameraMP, bool podpora5G, int baterijaMAH) : base(naziv, cena)
        {
            KameraMP = kameraMP;
            Podpora5G = podpora5G;
            BaterijaMAH = baterijaMAH;
        }
         
        public override string Izpis()
        {
            return base.Izpis()
            + ", Kamera: " + KameraMP + " MP"
            + ", 5G: " + Podpora5G
            + ", Baterija: " + BaterijaMAH + " mAh";
        }
    }


    public class Laptop : Izdelek, IGarancija
    {
        private int ram;
        private string procesor;
        private double teza;

        public int GarancijaMeseci { get; set; }

        public string PrikaziGarancijo()
        {
            return "Garancija: " + GarancijaMeseci + " mesecev";
        }

        public override string VrstaIzdelka()
        {
            return "Laptop";
        }


        public int Ram
        {
            get { return ram; }
            set
            {
                if (value == 8 || value == 16 || value == 32)
                {
                    ram = value;
                }
            }
        }
        public string Procesor
        {
            get { return procesor; }
            set
            {
                if (value != "")
                {
                    procesor = value;
                }
                else
                {
                    procesor = "neimenovan procesor";
                }
            }
        }
        public double Teza
        {
            get { return teza; }
            set
            {
                if (value >= 0.1 && value <= 4.0)
                {
                    teza = value;
                }
            }
        }

        public Laptop(string naziv, double cena, int ram, string procesor, double teza)
            : base(naziv, cena)
        {
            Ram = ram;
            Procesor = procesor;
            Teza = teza;
        }

        public override string Izpis()
        {
            return base.Izpis()
                + ", RAM: " + Ram + " GB"
                + ", CPU: " + Procesor
                + ", Teža: " + Teza + " kg";
        }
    }


    public class Stranka
    {
        public static int SteviloStrank = 0;
        public readonly int id;
        private string ime;
        private string email;

        public string Ime
        {
            get { return ime; }
            set
            {
                if (value != "")
                { ime = value; }
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                if (value.Contains("@"))
                {
                    email = value;
                }

            }
        }
         
        public Stranka(string ime, string email)
        {
            Ime = ime;
            Email = email;
            SteviloStrank++;
            id = SteviloStrank;
        }
        public string Izpis()
        {
            return "ID" + id + ", ime" + Ime + ", email" + Email;
        }
    }


    public class Trgovina
    {
        public const double DDV = 0.22;
        public static string ImeTrgovine = "TechShop";
        private double popust;

        private List<Izdelek> izdelki = new List<Izdelek>();

        public void DodajIzdelek(Izdelek izdelek)
        {
            izdelki.Add(izdelek);
        }
        public Izdelek this[int index]
        {
            get { return izdelki[index]; }
            set { izdelki[index] = value; }
        }

        public double Popust
        {
            get { return popust; }
            set
            {
                if (value >= 0 && value <= 0.5)
                {
                    popust = value;
                }
            }
        }
        public Trgovina(double popust)
        {
            Popust = popust;
        }

        public double KoncnaCena(Izdelek izdelek)
        {
            double cenaZDDV = izdelek.Cena + Izdelek.IzracunajDDV(izdelek.Cena);
            return cenaZDDV * (1 - Popust);
        }
    }
}