using System;
using LibrarieModele;
using NivelStocareDate;

namespace EvidentaProduse
{
    class Program
    {
        static void Main(string[] args)
        {
            IStocareDate adminProduse = new AdministrareProduse_FisierText("Produse.txt");
            AdministrareClienti_FisierText adminClienti = new AdministrareClienti_FisierText("Clienti.txt"); 

            string optiune;

            do
            {
                Console.WriteLine("\n--- MENIU GESTIUNE FISIER TEXT ---");
                Console.WriteLine("1. Adauga Produs Nou");
                Console.WriteLine("2. Afiseaza Toate Produsele");
                Console.WriteLine("3. Modifica Pret Produs");
                Console.WriteLine("4. Cauta Produs");
                Console.WriteLine("5. Adauga Client Nou");      
                Console.WriteLine("6. Afiseaza Toti Clientii"); 
                Console.WriteLine("X. Iesire");
                Console.Write("Alegeti optiunea: ");
                optiune = Console.ReadLine()?.ToUpper();

                try
                {
                    switch (optiune)
                    {
                        case "1":
                            Console.WriteLine("\n--- Introducere Date Produs ---");
                            Console.Write("ID Produs: "); int id = int.Parse(Console.ReadLine());
                            Console.Write("Nume Produs: "); string nume = Console.ReadLine();
                            Console.Write("Cantitate: "); int cant = int.Parse(Console.ReadLine());
                            Console.Write("Pret: "); float pret = float.Parse(Console.ReadLine());

                            Produs pNou = new Produs(id, nume, cant, pret, CategorieProdus.Suplimente, OptiuniProdus.ProdusNou);
                            adminProduse.AdaugaProdus(pNou);
                            Console.WriteLine("Produs salvat!");
                            break;

                        case "2":
                            Produs[] listaProduse = adminProduse.GetProduse();
                            Console.WriteLine("\n--- Lista Produse ---");
                            if (listaProduse.Length == 0) Console.WriteLine("Fisierul de produse este gol.");
                            else foreach (var p in listaProduse) Console.WriteLine(p.Info());
                            break;

                        case "3":
                            Console.WriteLine("\n--- Modificare Produs ---");
                            Console.Write("ID-ul produsului de modificat: ");
                            int idCautat = int.Parse(Console.ReadLine());
                            Console.Write("Pret nou: ");
                            float pretNou = float.Parse(Console.ReadLine());
                            adminProduse.ModificaProdus(idCautat, pretNou);
                            break;

                        case "4":
                            Console.WriteLine("\n--- Cautare Produs ---");
                            Console.Write("Numele produsului cautat: ");
                            string numeCautat = Console.ReadLine();

                            Produs[] produseExistente = adminProduse.GetProduse();
                            bool gasit = false;

                            foreach (var p in produseExistente)
                            {
                                if (p.Nume.ToLower() == numeCautat.ToLower())
                                {
                                    Console.WriteLine("Gasit: " + p.Info());
                                    gasit = true;
                                }
                            }
                            if (!gasit) Console.WriteLine("Produsul nu a fost gasit.");
                            break;

                        case "5":
                            Console.WriteLine("\n--- Introducere Date Client ---");
                            Console.Write("ID Client: "); int idC = int.Parse(Console.ReadLine());
                            Console.Write("Nume Client: "); string numeC = Console.ReadLine();

                            Client cNou = new Client(idC, numeC);
                            adminClienti.AdaugaClient(cNou);
                            Console.WriteLine("Client salvat in fisier!");
                            break;

                        case "6": 
                            Client[] listaClienti = adminClienti.GetClienti();
                            Console.WriteLine("\n--- Lista Clienti ---");
                            if (listaClienti.Length == 0) Console.WriteLine("Fisierul de clienti este gol.");
                            else foreach (var c in listaClienti) Console.WriteLine(c.Info());
                            break;

                        case "X":
                            Console.WriteLine("Se inchide programul...");
                            break;

                        default:
                            Console.WriteLine("Optiune invalida!");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Eroare: Ai introdus text in loc de numar!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare generala: {ex.Message}");
                }

            } while (optiune != "X");
        }
    }
}