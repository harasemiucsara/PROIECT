using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            txtNume.Text = "";
            txtPret.Text = "";
            txtCantitate.Text = "";
            dpDataExpirarii.SelectedDate = null;
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

            // Luam data expirarii din DatePicker
            string dataExpirarii = "Nespecificata";
            if (dpDataExpirarii.SelectedDate != null)
                dataExpirarii = dpDataExpirarii.SelectedDate.Value.ToString("dd.MM.yyyy");

            // Cream produsul si il adaugam in lista
            Produs p = new Produs(urmatorul_id, txtNume.Text, cantitate, pret, categorie, optiuni);
            listaProduse.Add(p);
            urmatorul_id++;

            // Adaugam produsul in ListBox
            listBoxProduse.Items.Add($"[{p.IdProdus}] {p.Nume} | {p.Pret} lei | {p.Categorie} | Exp: {dataExpirarii}");

            txtMesajAdaugare.Text = $"Produs adaugat! Total: {listaProduse.Count}";
            txtMesajAdaugare.Foreground = System.Windows.Media.Brushes.Green;
            txtStatus.Text = $"Produs '{p.Nume}' adaugat cu succes.";
        }

        // ==================== FILTRARE CU COMBOBOX ====================

        private void CmbFiltru_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxProduse == null) return;

            // Luam textul selectat din ComboBox
            string filtru = (cmbFiltruCategorie.SelectedItem as ComboBoxItem)?.Content.ToString();

            listBoxProduse.Items.Clear();

            foreach (Produs p in listaProduse)
            {
                // Daca e "Toate", afisam tot; altfel filtram dupa categorie
                if (filtru == "Toate" || p.Categorie.ToString() == filtru)
                {
                    listBoxProduse.Items.Add($"[{p.IdProdus}] {p.Nume} | {p.Pret} lei | {p.Categorie}");
                }
            }

            txtStatus.Text = $"Filtru aplicat: {filtru}";
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
                txtStatus.Text = "Cautare finalizata: produs gasit.";
            }
            else
            {
                txtRezultatCautare.Text = "Produsul nu a fost gasit.";
                txtStatus.Text = "Cautare finalizata: niciun rezultat.";
            }
        }
    }
}
