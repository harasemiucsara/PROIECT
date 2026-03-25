using System;
using System.Collections; 
using LibrarieModele;

namespace GestiuneProduse
{
    class Program
    {
        static void Main()
        {
            ArrayList listaProduse = new ArrayList();
            Produs produsCitit = null;
            string optiune;

            do
            {
                Console.WriteLine("\n--- MENIU GESTIUNE STOC ---");
                Console.WriteLine("C. Citire produs de la tastatura");
                Console.WriteLine("S. Salvare produs in colectie (ArrayList)");
                Console.WriteLine("A. Afisare toate produsele");
                Console.WriteLine("F. Cautare produs dupa nume");
                Console.WriteLine("X. Iesire");
                Console.Write("Alegeti optiunea: ");

                optiune = Console.ReadLine()?.ToUpper();

                switch (optiune)
                {
                    case "C":
                        produsCitit = CitireProdusTastatura();
                        break;

                    case "S":
                        if (produsCitit != null)
                        {
                            
                            produsCitit.IdProdus = listaProduse.Count + 1;
                            listaProduse.Add(produsCitit);
                            Console.WriteLine("Produsul a fost salvat.");
                            produsCitit = null; 
                        }
                        else
                        {
                            Console.WriteLine("Eroare");
                        }
                        break;

                    case "A":
                        AfisareColectie(listaProduse);
                        break;

                    case "F":
                        Console.Write("Introduceti numele produsului cautat: ");
                        string numeCautat = Console.ReadLine();
                        CautareDupaNume(listaProduse, numeCautat);
                        break;

                    case "X":
                        Console.WriteLine("Aplicatie inchisa.");
                        break;

                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }

            } while (optiune != "X");
        }

        public static Produs CitireProdusTastatura()
        {
            Console.WriteLine("\n--- Introducere date produs ---");
            Console.Write("Nume: ");
            string nume = Console.ReadLine();

            Console.Write("Cantitate: ");
            int cantitate = int.Parse(Console.ReadLine());

            Console.Write("Pret: ");
            float pret = float.Parse(Console.ReadLine());

            return new Produs(0, nume, cantitate, pret);
        }

        public static void AfisareColectie(ArrayList produse)
        {
            if (produse.Count == 0)
            {
                Console.WriteLine("Colectia este goala.");
                return;
            }

            Console.WriteLine("\nLista produselor din stoc:");
            foreach (Produs p in produse)
            {
                Console.WriteLine(p.Info());
            }
        }

        public static void CautareDupaNume(ArrayList produse, string numeCautat)
        {
            bool gasit = false;
            Console.WriteLine($"\nRezultate cautare pentru '{numeCautat}':");

            foreach (Produs p in produse)
            {
                // Căutare case-insensitive (nu contează literele mari/mici)
                if (p.Nume.Equals(numeCautat, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(p.Info());
                    gasit = true;
                }
            }

            if (!gasit)
            {
                Console.WriteLine("Nu a fost gasit niciun produs cu acest nume.");
            }
        }
    }
}