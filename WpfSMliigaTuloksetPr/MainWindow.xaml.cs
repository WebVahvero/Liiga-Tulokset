using System.Windows;
using System.Xml;
using System;


namespace WpfSMliigaTuloksetPr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int kotimaalit = 0;
        int vierasmaalit = 0;

        int kotiPelIndex = 0;
        string[] kotiPelaajienNimet = new string[50];
        string[] kotiPelaajienNumerot = new string[50];

        int vierasPelIndex = 0;
        string[] vierasPelaajienNimet = new string[50];
        string[] vierasPelaajienNumerot = new string[50];

        public MainWindow()
        {
            InitializeComponent();

            XmlReader lukija = XmlReader.Create("SMliigaJoukkueetPelaajat.xml");

            string Joukkue = "";
                        
            lukija.MoveToContent();
            while (lukija.Read())
            {
                if (lukija.NodeType == XmlNodeType.Element
                    && lukija.Name == "Joukkue")
                {
                    if (lukija.HasAttributes)
                    {                       
                        Joukkue = lukija.GetAttribute("nimi");
                        lstKotijoukkue.Items.Add(Joukkue);
                        lstVierasjoukkue.Items.Add(Joukkue);                        
                    }
                }
            }

            lstVierasjoukkue.SelectedIndex = 0;
            lstKotijoukkue.SelectedIndex = 1;
        }
           
        private void lstKotijoukkue_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstKotijoukkue.SelectedItem == lstVierasjoukkue.SelectedItem)
            {
                MessageBox.Show("Valinta virheellinen");
                lstKotijoukkue.SelectedIndex++;
            }

            string joukkue = "";
            
            kotimaalit = 0;
            lblKotimaalit.Content = 0;

            for (int i =0; i < 50; i++)
            {
                kotiPelaajienNumerot[i] = "";
                kotiPelaajienNimet[i] = string.Empty;
            }
            lstKotimaalit.Items.Clear();
            lblKotimaalit.Content = 0;

            joukkue = lstKotijoukkue.SelectedItem.ToString();
            XmlReader lukija = XmlReader.Create("SMliigaJoukkueetPelaajat.xml");
            lblKotijoukkue.Content = joukkue;

            lukija.MoveToContent();
            lstKotipelaajat.Items.Clear();           
            while (lukija.Read())
            {
                if (lukija.NodeType == XmlNodeType.Element
                    && lukija.Name == "Joukkue")
                {
                    if (lukija.HasAttributes)
                    {
                        string haettujoukkue = lukija.GetAttribute("nimi");
                        if (joukkue == haettujoukkue)
                        {
                            while (lukija.Read())
                            {
                                if (lukija.NodeType == XmlNodeType.EndElement && lukija.Name == "Pelaajat")
                                {
                                    return;
                                }
                                if (lukija.NodeType == XmlNodeType.Element && lukija.Name == "Nimi")
                                {
                                    lukija.Read();
                                    lstKotipelaajat.Items.Add(lukija.Value);
                                    kotiPelaajienNimet[kotiPelIndex] = lukija.Value;
                                    while (lukija.Read())
                                    {
                                        if (lukija.NodeType == XmlNodeType.Element &&
                                        lukija.Name == "Pelaajanro")
                                        {
                                            lukija.Read();
                                            kotiPelaajienNumerot[kotiPelIndex] = lukija.Value;
                                            kotiPelIndex++;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    

        private void lstVierasjoukkue_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (lstKotijoukkue.SelectedItem == lstVierasjoukkue.SelectedItem)
            {
                MessageBox.Show("Valinta virheellinen");
                lstKotijoukkue.SelectedIndex++;
            }

            string joukkue = "";
            
            vierasmaalit = 0;
            lblVierasmaalit.Content = 0;

            for (int i = 0; i < 50; i++)
            {
                vierasPelaajienNumerot[i] = "";
                vierasPelaajienNimet[i] = string.Empty;
            }
            lstVierasmaalit.Items.Clear();
            lblVierasmaalit.Content = 0;

            joukkue = lstVierasjoukkue.SelectedItem.ToString();
            XmlReader lukija = XmlReader.Create("SMliigaJoukkueetPelaajat.xml");
            lblVierasjoukkue.Content = joukkue;

            lukija.MoveToContent();
            lstVieraspelaajat.Items.Clear();
            while (lukija.Read())
            {
                if (lukija.NodeType == XmlNodeType.Element
                    && lukija.Name == "Joukkue")
                {
                    if (lukija.HasAttributes)
                    {
                        string haettujoukkue = lukija.GetAttribute("nimi");
                        if (joukkue == haettujoukkue)
                        {
                            while (lukija.Read())
                            {
                                if (lukija.NodeType == XmlNodeType.EndElement && lukija.Name == "Pelaajat")
                                {
                                    return;
                                }
                                if (lukija.NodeType == XmlNodeType.Element && lukija.Name == "Nimi")
                                {
                                    lukija.Read();
                                    lstVieraspelaajat.Items.Add(lukija.Value);
                                    vierasPelaajienNimet[vierasPelIndex] = lukija.Value;
                                    while (lukija.Read())
                                    {
                                        if (lukija.NodeType == XmlNodeType.Element &&
                                        lukija.Name == "Pelaajanro")
                                        {
                                            lukija.Read();
                                            vierasPelaajienNumerot[vierasPelIndex] = lukija.Value;
                                            vierasPelIndex++;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnKirjaaKotiMaali_Click(object sender, RoutedEventArgs e)
        {
            DateTime maalinTekoHetki = DateTime.Now;

            int valittuPelaajaindeksi = lstKotipelaajat.SelectedIndex;

            if (lstKotipelaajat.SelectedIndex >=0)
            {
                lstKotimaalit.Items.Add(maalinTekoHetki.ToLongTimeString() + ": " + kotiPelaajienNumerot[valittuPelaajaindeksi] + ". " + lstKotipelaajat.SelectedItem.ToString());
                kotimaalit++;
                lblKotimaalit.Content = kotimaalit;
            }
            else
            {
                MessageBox.Show("Valitse maalintehnyt kotijoukkueen pelaaja");
            }

        }

        private void btnKirjaaVierasMaali_Click(object sender, RoutedEventArgs e)
        {
            DateTime maalinTekoHetki = DateTime.Now;

            int valittuPelaajaindeksi = lstVieraspelaajat.SelectedIndex;

            if (lstVieraspelaajat.SelectedIndex >= 0)
            {
                lstVierasmaalit.Items.Add(maalinTekoHetki.ToLongTimeString() + ": " + vierasPelaajienNumerot[valittuPelaajaindeksi] + ". " + lstVieraspelaajat.SelectedItem.ToString());
                vierasmaalit++;
                lblVierasmaalit.Content = vierasmaalit;
            }
            else
            {
                MessageBox.Show("Valitse maalintehnyt vierasjoukkueen pelaaja");
            }
        }
    }   
}

