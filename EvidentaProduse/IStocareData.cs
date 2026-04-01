using System.Collections;
using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareData
    {
        void AddProdus(Produs p);
        ArrayList GetProduse();
        // Aici poți adăuga și metoda de căutare
    }
}