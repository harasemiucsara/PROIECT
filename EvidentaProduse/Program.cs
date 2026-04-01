using System.Collections;
using System.Collections.Generic;
using System.Linq; // Pentru cerința LINQ
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareProduseMemorie : IStocareData
    {
        private ArrayList produse = new ArrayList();

        public void AddProdus(Produs p)
        {
            p.IdProdus = produse.Count + 1;
            produse.Add(p);
        }

        public ArrayList GetProduse() => produse;

        // Cerința 3: Metodă cu LINQ
        public List<Produs> FiltrareDupaPret(float pretMaxim)
        {
            return produse.Cast<Produs>()
                          .Where(p => p.Pret <= pretMaxim)
                          .ToList();
        }
    }
}