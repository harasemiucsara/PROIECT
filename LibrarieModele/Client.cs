using System;

namespace LibrarieModele
{
    public class Client
    {
        public int IdClient { get; set; }
        public string NumeClient { get; set; }

        public Client(int id, string nume)
        {
            IdClient = id;
            NumeClient = nume;
        }

        public Client(string linieFisier)
        {
            string[] date = linieFisier.Split(',');
            IdClient = int.Parse(date[0]);
            NumeClient = date[1];
        }

        public string ConversieLaSir_PentruFisier()
        {
            return $"{IdClient},{NumeClient}";
        }

        public string Info() => $"ID Client: {IdClient} | Nume: {NumeClient}";
    }
}