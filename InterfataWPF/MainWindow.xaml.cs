using System.Collections.Generic;
using System.Windows;
using LibrarieModele;

namespace InterfataWPF
{
    public partial class MainWindow : Window
    {
        // Lista in care pastram produsele adaugate
        private List<Produs> listaProduse = new List<Produs>();
        private int urmatorul_id = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        // ==================== MENIU ====================

        private void MenuNou_Click(object sender, RoutedEventArgs e)
        {
            // Golim campurile pentru a introduce un produs nou
            txtNume.Text = "";
            txtPret.Text = "";
            txtCantitate.Text = "";
            chkProdusNou.IsChecked = false;
            chkOferta.IsChecked = false;
            chkResigilat.IsChecked = false;
            chkCadou.IsChecked = false;
            rbSuplimente.IsChecked = true;
            txtMesajAdaugare.Text = "";
            txtStatus.Text = "Campuri resetate. Puteti adauga un produs nou.";
        }

        private void MenuIesire_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuDespre_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Evidenta Produse\nPIU - Laborator 8\nHarasemiuc Sara", "Despre aplicatie");
        }

        // ==================== ADAUGARE PRODUS ====================

        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            // Verificam ca toate campurile sunt completate
            if (txtNume.Text == "" || txtPret.Text == "" || txtCantitate.Text == "")
            {
                txtMesajAdaugare.Text = "Completati toate campurile!";
                txtMesajAdaugare.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            float pret = float.Parse(txtPret.Text);
            int cantitate = int.Parse(txtCantitate.Text);

            // Determinam categoria selectata cu RadioButton
            CategorieProdus categorie = CategorieProdus.Nespecificat;
            if (rbSuplimente.IsChecked == true) categorie = CategorieProdus.Suplimente;
            if (rbAlimente.IsChecked == true)   categorie = CategorieProdus.Alimente;
            if (rbCosmetica.IsChecked == true)  categorie = CategorieProdus.Cosmetica;
            if (rbIngrijire.IsChecked == true)  categorie = CategorieProdus.ProduseIngrijire;

            // Determinam optiunile selectate cu CheckBox
            OptiuniProdus optiuni = OptiuniProdus.Niciuna;
            if (chkProdusNou.IsChecked == true) optiuni = optiuni | OptiuniProdus.ProdusNou;
            if (chkOferta.IsChecked == true)    optiuni = optiuni | OptiuniProdus.Oferta;
            if (chkResigilat.IsChecked == true) optiuni = optiuni | OptiuniProdus.Resigilat;
            if (chkCadou.IsChecked == true)     optiuni = optiuni | OptiuniProdus.ProdusCadou;

            // Cream produsul si il adaugam in lista
            Produs p = new Produs(urmatorul_id, txtNume.Text, cantitate, pret, categorie, optiuni);
            listaProduse.Add(p);
            urmatorul_id++;

            txtMesajAdaugare.Text = $"Produs adaugat! Total produse: {listaProduse.Count}";
            txtMesajAdaugare.Foreground = System.Windows.Media.Brushes.Green;
            txtStatus.Text = $"Produs '{p.Nume}' adaugat cu succes.";
        }

        // ==================== CAUTARE PRODUS ====================

        private void BtnCauta_Click(object sender, RoutedEventArgs e)
        {
            string numeCautat = txtCautare.Text.ToLower();

            if (numeCautat == "")
            {
                txtRezultatCautare.Text = "Introduceti un nume pentru cautare.";
                return;
            }

            if (listaProduse.Count == 0)
            {
                txtRezultatCautare.Text = "Nu exista produse adaugate inca.";
                return;
            }

            // Cautam produsul dupa nume
            Produs gasit = null;
            foreach (Produs p in listaProduse)
            {
                if (p.Nume.ToLower().Contains(numeCautat))
                {
                    gasit = p;
                    break;
                }
            }

            if (gasit != null)
            {
                txtRezultatCautare.Text = $"Produs gasit:\n" +
                                          $"ID: {gasit.IdProdus}\n" +
                                          $"Nume: {gasit.Nume}\n" +
                                          $"Pret: {gasit.Pret} lei\n" +
                                          $"Cantitate: {gasit.Cantitate} buc.\n" +
                                          $"Categorie: {gasit.Categorie}\n" +
                                          $"Optiuni: {gasit.Optiuni}";
                txtStatus.Text = $"Cautare finalizata: produs gasit.";
            }
            else
            {
                txtRezultatCautare.Text = "Produsul nu a fost gasit.";
                txtStatus.Text = "Cautare finalizata: niciun rezultat.";
            }
        }
    }
}
