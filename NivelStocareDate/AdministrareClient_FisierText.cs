using System;
using System.IO;
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareClienti_FisierText
    {
        private string numeFisier;

        public AdministrareClienti_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AdaugaClient(Client client)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(client.ConversieLaSir_PentruFisier());
            }
        }

        public Client[] GetClienti()
        {
            Client[] clienti = new Client[100]; 
            int nr = 0;

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    clienti[nr++] = new Client(linie);
                }
            }

            Array.Resize(ref clienti, nr);
            return clienti;
        }
    }
}