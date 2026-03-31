using System;
using System.IO;
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareProduse_FisierText : IStocareDate
    {
        private const int NR_MAX_PRODUSE = 100;
        private string numeFisier;

        public AdministrareProduse_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AdaugaProdus(Produs produs)
        {
            using (StreamWriter swFisierText = new StreamWriter(numeFisier, true))
            {
                swFisierText.WriteLine(produs.ConversieLaSir_PentruFisier());
            }
        }

        public Produs[] GetProduse()
        {
            Produs[] produse = new Produs[NR_MAX_PRODUSE];
            int nrProduse = 0;

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = sr.ReadLine()) != null)
                {
                    produse[nrProduse++] = new Produs(linieFisier);
                }
            }
            Array.Resize(ref produse, nrProduse);
            return produse;
        }

        public void ModificaProdus(int idCautat, float pretNou)
        {
            Produs[] produse = GetProduse();
            bool gasit = false;

            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (Produs p in produse)
                {
                    if (p.IdProdus == idCautat)
                    {
                        p.Pret = pretNou;
                        gasit = true;
                    }
                    sw.WriteLine(p.ConversieLaSir_PentruFisier());
                }
            }

            if (!gasit) Console.WriteLine("Produsul cu acest ID nu a fost gasit in fisier.");
            else Console.WriteLine("Pretul a fost modificat si salvat cu succes in fisier!");
        }
    }
}