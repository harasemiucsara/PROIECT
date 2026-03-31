using LibrarieModele;
using System;
using System.Linq; 
using LibrarieModele;

namespace NivelStocareDate
{
    public class Administrator
    {
        private const int NR_MAX_PRODUSE = 100;
        private Produs[] produse;
        private int nrProduse;

        public Administrator()
        {
            produse = new Produs[NR_MAX_PRODUSE];
            nrProduse = 0;
        }

        public void AdaugaProdus(Produs produs)
        {
            if (nrProduse < NR_MAX_PRODUSE)
            {
                produse[nrProduse++] = produs;
            }
        }

        public Produs[] GetProduse()
        {
            return produse.Where(p => p != null).ToArray();
        }
        public Produs CautaProdusDupaNume(string nume)
        {
            return produse.FirstOrDefault(p => p != null && p.Nume.ToLower() == nume.ToLower());
        }
    }
}