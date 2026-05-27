using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using LibrarieModele;
using NivelStocareDate;

namespace InterfataWPF
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<string> ProduseLista { get; set; } = new ObservableCollection<string>();

        private string _statusMesaj = "Gata. Introduceti datele unui produs.";
        public string StatusMesaj
        {
            get { return _statusMesaj; }
            set
            {
                _statusMesaj = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusMesaj"));
            }
        }

        private List<Produs> listaProduse = new List<Produs>();
        private int urmatorul_id = 1;
        private AdministrareProduse_FisierText? stocareProduse;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            string caleFisier = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "produse.txt");
            stocareProduse = new AdministrareProduse_FisierText(caleFisier);
            IncarcaProduseDinFisier();
        }

        private void IncarcaProduseDinFisier()
        {
            try
            {
                Produs[] produseIncarcate = stocareProduse!.GetProduse();
                foreach (Produs p in produseIncarcate)
                {
                    listaProduse.Add(p);
                    ProduseLista.Add($"[{p.IdProdus}] {p.Nume} | {p.Pret} lei | {p.Categorie}");
                    if (p.IdProdus >= urmatorul_id)
                        urmatorul_id = p.IdProdus + 1;
                }
                if (listaProduse.Count > 0)
                    StatusMesaj = $"Date incarcate din fisier: {listaProduse.Count} produse.";
            }
            catch
            {
                StatusMesaj = "Fisierul de date existent nu a putut fi citit. Se porneste cu lista goala.";
            }
        }


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
            StatusMesaj = "Campuri resetate. Puteti adauga un produs nou.";
        }

        private void MenuIesire_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuDespre_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Evidenta Produse\nPIU - Laborator 8\nHarasemiuc Sara", "Despre aplicatie");
        }


        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (txtNume.Text == "" || txtPret.Text == "" || txtCantitate.Text == "")
            {
                txtMesajAdaugare.Text = "Completati toate campurile!";
                txtMesajAdaugare.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            float pret = float.Parse(txtPret.Text);
            int cantitate = int.Parse(txtCantitate.Text);

            CategorieProdus categorie = CategorieProdus.Nespecificat;
            if (rbSuplimente.IsChecked == true) categorie = CategorieProdus.Suplimente;
            if (rbAlimente.IsChecked == true)   categorie = CategorieProdus.Alimente;
            if (rbCosmetica.IsChecked == true)  categorie = CategorieProdus.Cosmetica;
            if (rbIngrijire.IsChecked == true)  categorie = CategorieProdus.ProduseIngrijire;

            OptiuniProdus optiuni = OptiuniProdus.Niciuna;
            if (chkProdusNou.IsChecked == true) optiuni = optiuni | OptiuniProdus.ProdusNou;
            if (chkOferta.IsChecked == true)    optiuni = optiuni | OptiuniProdus.Oferta;
            if (chkResigilat.IsChecked == true) optiuni = optiuni | OptiuniProdus.Resigilat;
            if (chkCadou.IsChecked == true)     optiuni = optiuni | OptiuniProdus.ProdusCadou;

            string dataExpirarii = "Nespecificata";
            if (dpDataExpirarii.SelectedDate != null)
                dataExpirarii = dpDataExpirarii.SelectedDate.Value.ToString("dd.MM.yyyy");

            Produs p = new Produs(urmatorul_id, txtNume.Text, cantitate, pret, categorie, optiuni);
            listaProduse.Add(p);
            stocareProduse.AdaugaProdus(p);
            urmatorul_id++;

            ProduseLista.Add($"[{p.IdProdus}] {p.Nume} | {p.Pret} lei | {p.Categorie} | Exp: {dataExpirarii}");

            txtMesajAdaugare.Text = $"Produs adaugat! Total: {listaProduse.Count}";
            txtMesajAdaugare.Foreground = System.Windows.Media.Brushes.Green;
            StatusMesaj = $"Produs '{p.Nume}' adaugat cu succes.";
        }


        private void CmbFiltru_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFiltruCategorie == null || cmbFiltruCategorie.SelectedItem == null) return;

            string filtru = (cmbFiltruCategorie.SelectedItem as ComboBoxItem)?.Content.ToString();

            ProduseLista.Clear();

            foreach (Produs p in listaProduse)
            {
                if (filtru == "Toate" || p.Categorie.ToString() == filtru)
                {
                    ProduseLista.Add($"[{p.IdProdus}] {p.Nume} | {p.Pret} lei | {p.Categorie}");
                }
            }

            StatusMesaj = $"Filtru aplicat: {filtru}";
        }


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
                StatusMesaj = "Cautare finalizata: produs gasit.";
            }
            else
            {
                txtRezultatCautare.Text = "Produsul nu a fost gasit.";
                StatusMesaj = "Cautare finalizata: niciun rezultat.";
            }
        }
    }
}
