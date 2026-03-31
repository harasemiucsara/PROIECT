using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareDate
    {
        void AdaugaProdus(Produs produs);
        Produs[] GetProduse();
        void ModificaProdus(int idCautat, float pretNou);
    }
}