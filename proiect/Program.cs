using System;
using System.Collections.Generic;
using LibrarieModele;

namespace GestiuneProduse
{
    class Program
    {
        public static void Main()
        {
            List<Produs> produse = new List<Produs>();
            Produs produsNou = null;
            string optiune;

            do
            {
                Console.WriteLine("\n--- MENIU ---");
                Console.WriteLine("C. Citire produs");
                Console.WriteLine("I. Afisare ultim produs");
                Console.WriteLine("A. Afisare produse");
                Console.WriteLine("S. Salvare produs");
                Console.WriteLine("X. Inchidere");

                Console.WriteLine("Alegeti optiunea:");
                // Adăugăm ? pentru a preveni erori în cazul în care ReadLine returnează null
                optiune = Console.ReadLine()?.ToUpper();

                switch (optiune)
                {
                    case "C":
                        produsNou = CitireProdus();
                        break;

                    case "I":
                        AfisareProdus(produsNou);
                        break;

                    case "A":
                        AfisareProduse(produse);
                        break;

                    case "S":
                        if (produsNou != null)
                        {
                            produsNou.IdProdus = produse.Count + 1;
                            produse.Add(produsNou);
                            Console.WriteLine("Produs salvat cu succes.");
                            // Opțional: golim produsNou după salvare ca să nu-l salvăm de 2 ori din greșeală
                            produsNou = null;
                        }
                        else
                        {
                            Console.WriteLine("Eroare: Nu ai citit niciun produs! Alege 'C' mai intai.");
                        }
                        break;

                    case "X":
                        Console.WriteLine("Program inchis.");
                        break;

                    default:
                        Console.WriteLine("Optiune invalida.");
                        break;
                }

            } while (optiune != "X");
        }

        public static Produs CitireProdus()
        {
            Console.WriteLine("Nume produs:");
            string nume = Console.ReadLine();

            Console.WriteLine("Cantitate:");
            int cantitate = int.Parse(Console.ReadLine());

            Console.WriteLine("Pret:");
            float pret = float.Parse(Console.ReadLine());

            Produs produs = new Produs(0, nume, cantitate, pret);
            return produs;
        }

        public static void AfisareProdus(Produs produs)
        {
            if (produs != null)
                Console.WriteLine(produs.Info());
            else
                Console.WriteLine("Nu exista niciun produs in memorie.");
        }

        public static void AfisareProduse(List<Produs> produse)
        {
            if (produse.Count == 0)
            {
                Console.WriteLine("Lista de produse este goala.");
                return;
            }

            foreach (Produs produs in produse)
            {
                AfisareProdus(produs);
            }
        }
    }
}