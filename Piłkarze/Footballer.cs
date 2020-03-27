using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piłkarze
{
    internal class Footballer
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }
        public int Waga { get; set; }


        public Footballer(string imie, string nazwisko, int wiek, int waga)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
            Waga = waga;
            
        }

        public Footballer(string imie, string nazwisko) : this(imie, nazwisko, 50, 75) { }

        public bool czyIstnieje(Footballer footballer)
        {
            if (footballer.Imie != Imie)
                return false;
            if (footballer.Nazwisko != Nazwisko)
                return false;
            if (footballer.Wiek != Wiek)
                return false;
            if (footballer.Waga != Waga)
                return false;
            return true;
        }
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} , {Wiek} lat, {Waga} kg";
        }

        public string ToFileFormat()
        {
            return $"{Imie}|{Nazwisko}|{Wiek}|{Waga}";
        }

        public static Footballer CreateFromString(string sPilkarz)
        {
            string imie, nazwisko;
            int wiek, waga;
            var pola = sPilkarz.Split('|');
            if (pola.Length == 4)
            {
                nazwisko = pola[1];
                imie = pola[0];
                wiek = int.Parse(pola[2]);
                waga = int.Parse(pola[3]);
                return new Footballer(imie, nazwisko, wiek, waga);
            }
            throw new Exception("Błędny format danych z pliku");
        }

        public Footballer Edit(Footballer fb)
        {
            this.Imie = fb.Imie;
            this.Nazwisko = fb.Nazwisko;
            this.Wiek = fb.Wiek;
            this.Waga = fb.Waga;
            return this;
        }
    }
}
