using System;

namespace LibrarieModele
{
    public enum CategorieProdus
    {
        Nespecificat = 0,
        Suplimente = 1,
        Alimente = 2,
        ProduseIngrijire = 3,
        Cosmetica = 4
    }

    [Flags]
    public enum OptiuniProdus
    {
        Niciuna = 0,
        Resigilat = 1,          
        Oferta = 2,    
        ProdusCadou = 4,  
        ProdusNou = 8       
    }

    public class Produs
    {
        public int IdProdus { get; set; }
        public string Nume { get; set; }
        public int Cantitate { get; set; }
        public float Pret { get; set; }

        public CategorieProdus Categorie { get; set; }
        public OptiuniProdus Optiuni { get; set; }

        public Produs()
        {
            Nume = string.Empty;
            Categorie = CategorieProdus.Nespecificat;
            Optiuni = OptiuniProdus.Niciuna;
        }

        public Produs(int idProdus, string nume, int cantitate, float pret, CategorieProdus categorie, OptiuniProdus optiuni)
        {
            IdProdus = idProdus;
            Nume = nume;
            Cantitate = cantitate;
            Pret = pret;
            Categorie = categorie;
            Optiuni = optiuni;
        }

        public string Info() => $"ID: {IdProdus} | {Nume} | Categ: {Categorie} | Optiuni: {Optiuni} | Pret: {Pret}";
        public Produs(string linieFisier)
        {
            string[] dateFisier = linieFisier.Split(',');

            IdProdus = int.Parse(dateFisier[0]);
            Nume = dateFisier[1];
            Cantitate = int.Parse(dateFisier[2]);
            Pret = float.Parse(dateFisier[3]);
            Categorie = (CategorieProdus)Enum.Parse(typeof(CategorieProdus), dateFisier[4]);
            Optiuni = (OptiuniProdus)Enum.Parse(typeof(OptiuniProdus), dateFisier[5]);
        }

        public string ConversieLaSir_PentruFisier()
        {
            return $"{IdProdus},{Nume},{Cantitate},{Pret},{Categorie},{Optiuni}";
        }
    }

}