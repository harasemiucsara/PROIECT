using System.Windows;
using LibrarieModele;

namespace InterfataWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AfiseazaProdus();
        }

        private void AfiseazaProdus()
        {
            Produs p = new Produs(1, "Vitamina C 1000mg", 50, 29.99f,
                CategorieProdus.Suplimente, OptiuniProdus.ProdusNou | OptiuniProdus.Oferta);

            lblId.Content        = $"ID Produs:   {p.IdProdus}";
            lblNume.Content      = $"Nume:        {p.Nume}";
            lblCategorie.Content = $"Categorie:   {p.Categorie}";
            lblCantitate.Content = $"Cantitate:   {p.Cantitate} buc.";
            lblPret.Content      = $"Pret:        {p.Pret:F2} lei";
            lblOptiuni.Content   = $"Optiuni:     {p.Optiuni}";
        }
    }
}
