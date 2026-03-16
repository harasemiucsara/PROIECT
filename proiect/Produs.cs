namespace LibrarieModele
{
    public class Produs
    {
        public int IdProdus { get; set; }
        public string Nume { get; set; }
        public int Cantitate { get; set; }
        public float Pret { get; set; }

        public Produs(int idProdus, string nume, int cantitate, float pret)
        {
            IdProdus = idProdus;
            Nume = nume;
            Cantitate = cantitate;
            Pret = pret;
        }

        public string Info() => $"ID: {IdProdus} | Nume: {Nume} | Cantitate: {Cantitate} | Preț: {Pret}";
    }
}