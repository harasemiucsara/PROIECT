using System;
namespace LibrarieModele
{
    public class Produs
    {
        public int IdProdus { get; set; }
        public string Nume { get; set; }
        public int Cantitate { get; set; }
        public float Pret { get; set; }

        // Constructor implicit (ca în modelul de laborator)
        public Produs()
        {
            Nume = string.Empty;
            Cantitate = 0;
            Pret = 0;
        }

        // Constructor cu parametri
        public Produs(int idProdus, string nume, int cantitate, float pret)
        {
            IdProdus = idProdus;
            Nume = nume;
            Cantitate = cantitate;
            Pret = pret;
        }

        public string Info() => $"ID: {IdProdus} | Nume: {Nume} | Cantitate: {Cantitate} | Pret: {Pret}";
    }
}