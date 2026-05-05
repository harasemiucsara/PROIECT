using System.Windows;
using LibrarieModele;

namespace InterfataWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAfiseaza_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtNumeProdus.Text;
            string pretText = txtPretProdus.Text;

            // Verificam daca campurile sunt completate
            if (nume == "" || pretText == "")
            {
                txtRezultat.Text = "Completati toate campurile!";
                return;
            }

            // Convertim pretul
            float pret = float.Parse(pretText);

            // Cream un obiect Produs
            Produs p = new Produs(1, nume, 10, pret, CategorieProdus.Suplimente, OptiuniProdus.ProdusNou);

            // Afisam informatiile
            txtRezultat.Text = $"ID: {p.IdProdus}\n" +
                               $"Nume: {p.Nume}\n" +
                               $"Pret: {p.Pret} lei\n" +
                               $"Categorie: {p.Categorie}\n" +
                               $"Optiuni: {p.Optiuni}";
        }
    }
}
