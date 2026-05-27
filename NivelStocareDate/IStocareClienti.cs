using LibrarieModele;

namespace NivelStocareDate
{
    // Interfata CRUD pentru entitatea Client
    public interface IStocareClienti
    {
        void AdaugaClient(Client client);       // Create
        Client[] GetClienti();                  // Read
        void ModificaClient(int idCautat, string numeNou);  // Update
        void StergeClient(int idDeSters);       // Delete
    }
}
