using System;
using System.IO;
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareClienti_FisierText : IStocareClienti
    {
        private string numeFisier;

        public AdministrareClienti_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        // CREATE - adauga un client nou in fisier
        public void AdaugaClient(Client client)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(client.ConversieLaSir_PentruFisier());
            }
        }

        // READ - returneaza toti clientii din fisier
        public Client[] GetClienti()
        {
            Client[] clienti = new Client[100];
            int nr = 0;

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(linie))
                        clienti[nr++] = new Client(linie);
                }
            }

            Array.Resize(ref clienti, nr);
            return clienti;
        }

        // UPDATE - modifica numele unui client dupa ID
        public void ModificaClient(int idCautat, string numeNou)
        {
            Client[] clienti = GetClienti();
            bool gasit = false;

            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (Client c in clienti)
                {
                    if (c.IdClient == idCautat)
                    {
                        c.NumeClient = numeNou;
                        gasit = true;
                    }
                    sw.WriteLine(c.ConversieLaSir_PentruFisier());
                }
            }

            if (!gasit)
                Console.WriteLine("Clientul cu acest ID nu a fost gasit.");
            else
                Console.WriteLine("Clientul a fost modificat cu succes!");
        }

        // DELETE - sterge un client dupa ID
        public void StergeClient(int idDeSters)
        {
            Client[] clienti = GetClienti();
            bool gasit = false;

            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (Client c in clienti)
                {
                    if (c.IdClient == idDeSters)
                    {
                        gasit = true;
                        continue; // sarim peste clientul de sters
                    }
                    sw.WriteLine(c.ConversieLaSir_PentruFisier());
                }
            }

            if (!gasit)
                Console.WriteLine("Clientul cu acest ID nu a fost gasit.");
            else
                Console.WriteLine("Clientul a fost sters cu succes!");
        }
    }
}
