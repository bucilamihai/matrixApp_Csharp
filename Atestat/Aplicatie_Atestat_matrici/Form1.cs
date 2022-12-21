using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat_matrici
{
    public partial class Form1 : Form
    {
        int incercari_parola;

        int[,] matrice = new int[101, 101];
        int nr_linii, nr_coloane;

        int nr_intrebare, punctaj;
        string nume_utilizator;

        PictureBox[,] matrice_pic = new PictureBox[5, 5];
        int pos_l, pos_c;

        Label ajutor = new Label();
        Label despre = new Label();
        private bool ExistaCont(string nume) // verifica daca exista un cont cu un anumit nume
        {
            int nr_inregistrari = contDataSet.Utilizator.Count, i;
            for (i = 0; i < nr_inregistrari; i++)
                if (nume == contDataSet.Utilizator[i].Nume)
                    return true;
            return false;
        }

        private string ParolaUtilizator(string nume) // returneaza parola unui utilizator
        {
            int nr_inregistrari = contDataSet.Utilizator.Count, i;
            for (i = 0; i < nr_inregistrari; i++)
            {
                if (nume == contDataSet.Utilizator[i].Nume)
                    return contDataSet.Utilizator[i].Parola;
            }
            return "";
        }

        private int SumaLinie(int[,] matrice, int i, int nr_coloane)
        {
            int j, sum = 0;
            for (j = 1; j <= nr_coloane; j++)
            {
                sum += matrice[i, j];
            }
            return sum;
        }

        private int ProdusLinie(int[,] matrice, int i, int nr_coloane)
        {
            int j, produs = 1;
            for (j = 1; j <= nr_coloane; j++)
            {
                produs *= matrice[i, j];
            }
            return produs;
        }

        private double AvgLinie(int[,] matrice, int i, int nr_coloane)
        {
            return Math.Round((double)SumaLinie(matrice, i, nr_coloane) / nr_coloane, 2);
        }

        private int MinLinie(int[,] matrice, int i, int nr_coloane)
        {
            int j, min = matrice[i, 1];
            for (j = 2; j <= nr_coloane; j++)
                if (matrice[i, j] < min)
                    min = matrice[i, j];
            return min;
        }

        private int MaxLinie(int[,] matrice, int i, int nr_coloane)
        {
            int j, max = matrice[i, 1];
            for (j = 2; j <= nr_coloane; j++)
                if (matrice[i, j] > max)
                    max = matrice[i, j];
            return max;
        }

        private int SumaColoana(int[,] matrice, int j, int nr_linii)
        {
            int i, sum = 0;
            for (i = 1; i <= nr_linii; i++)
                sum += matrice[i, j];
            return sum;
        }

        private int ProdusColoana(int[,] matrice, int j, int nr_linii)
        {
            int i, produs = 1;
            for (i = 1; i <= nr_linii; i++)
            {
                produs *= matrice[i, j];
            }
            return produs;
        }

        private double AvgColoana(int[,] matrice, int j, int nr_linii)
        {
            return Math.Round((double)SumaColoana(matrice, j, nr_linii) / nr_linii, 2);
        }

        private int MinColoana(int[,] matrice, int j, int nr_linii)
        {
            int i, min = matrice[1, j];
            for (i = 2; i <= nr_linii; i++)
                if (matrice[i, j] < min)
                    min = matrice[i, j];
            return min;
        }

        private int MaxColoana(int[,] matrice, int j, int nr_linii)
        {
            int i, max = matrice[1, j];
            for (i = 2; i <= nr_linii; i++)
                if (matrice[i, j] > max)
                    max = matrice[i, j];
            return max;
        }

        private void GenerareIntrebare()
        {
            enunt_label.Text = contDataSet.Intrebari[nr_intrebare].Enunt;
            declarare_label.Text = contDataSet.Intrebari[nr_intrebare].Declarare;
            radioButton8.Text = contDataSet.Intrebari[nr_intrebare].Varianta1;
            radioButton9.Text = contDataSet.Intrebari[nr_intrebare].Varianta2;
            radioButton10.Text = contDataSet.Intrebari[nr_intrebare].Varianta3;
            radioButton11.Text = contDataSet.Intrebari[nr_intrebare].Varianta4;
        }

        private int IdUtilizator(string nume)
        {
            int nr_utilizatori = contDataSet.Utilizator.Count(), i;
            for(i=0;i<nr_utilizatori;i++)
            {
                if (nume == contDataSet.Utilizator[i].Nume)
                    return i;
            }
            return -1;
        }

        private void GenerarePictureBox()
        {
            int i, j;
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    matrice_pic[i, j] = new PictureBox();
                    matrice_pic[i, j].Location = new Point(25 + 75 * i, 25 + 75 * j);
                    matrice_pic[i, j].Width = matrice_pic[i, j].Height = 50;
                    matrice_pic[i, j].BackColor = Color.Gray;
                    parcurgere_panel.Controls.Add(matrice_pic[i, j]);
                }
            } 
        }

        private void MatricePic_Gri()
        {
            int i, j;
            for (i = 0; i < 5; i++)
                for (j = 0; j < 5; j++)
                    matrice_pic[i, j].BackColor = Color.Gray;
        }
        
        private void PanelVisibleFalse() // ascund toate panel-urile
        {
            inreg_panel.Visible = false;
            conec_panel.Visible = false;
            test_panel.Visible = false;
            calculator_panel.Visible = false;
            generare_panel.Visible = false;
            parcurgere_panel.Visible = false;
            statistici_panel.Visible = false;
            schimbareparola_panel.Visible = false;
            stergecont_panel.Visible = false;
            despre.Visible = false;
            ajutor.Visible = false;
        }

        private void PanelBackColorTransparent() // decolorez toate panel-urile
        {
            inreToolStripMenuItem.BackColor = Color.Transparent;
            conectareToolStripMenuItem.BackColor = Color.Transparent;
            matriciToolStripMenuItem.BackColor = Color.Transparent;
            statisticiToolStripMenuItem.BackColor = Color.Transparent;
            gestionareContToolStripMenuItem.BackColor = Color.Transparent; 
            iesireToolStripMenuItem.BackColor = Color.Transparent;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'contDataSet.Intrebari' table. You can move, or remove it, as needed.
            this.intrebariTableAdapter.Fill(this.contDataSet.Intrebari);
            // TODO: This line of code loads data into the 'contDataSet.Utilizator' table. You can move, or remove it, as needed.
            this.utilizatorTableAdapter.Fill(this.contDataSet.Utilizator);
            utilizatorDataGridView.Visible = false; // ascund tabelul si navigatorul
            utilizatorBindingNavigator.Visible = false;
            intrebariDataGridView.Visible = false;
            matriciToolStripMenuItem.Visible = false; // ascund menustrip-uri
            statisticiToolStripMenuItem.Visible = false;
            gestionareContToolStripMenuItem.Visible = false;
            PanelVisibleFalse();
            conectat_label.Visible = false;
            parola_textBox.UseSystemPasswordChar = true;
            parolaok_textBox.UseSystemPasswordChar = true;
            parola_textBox2.UseSystemPasswordChar = true;
        }

        private void utilizatorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.utilizatorBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.contDataSet);
        }

        ///~/// INREGISTRARE ///~///
        private void inreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conectareToolStripMenuItem.Visible = false;
            PanelBackColorTransparent();
            inreToolStripMenuItem.BackColor = Color.LightBlue;
            PanelVisibleFalse();
            inreg_panel.Visible = true;
            nume_textBox.Text = parola_textBox.Text = parolaok_textBox.Text = "";
            inreg_panel.Location = new Point(1100 / 2 - inreg_panel.Width / 2, 600 / 2 - inreg_panel.Height / 2); // centrez panel-ul
        }

        private void inreg_button_Click(object sender, EventArgs e)
        {
            string nume = nume_textBox.Text, parola = parola_textBox.Text, parola_conf = parolaok_textBox.Text;
            if (nume != "" && parola != "" && parola_conf != "")
            {
                if (!ExistaCont(nume))
                {
                    if (parola == parola_conf)
                    {
                        utilizatorTableAdapter.InregistrareQuery(nume, parola, 0, 0);
                        utilizatorTableAdapter.Fill(contDataSet.Utilizator);
                        MessageBox.Show("V-ati inregistrat cu succes");
                        conectareToolStripMenuItem.Visible = true;
                        inreToolStripMenuItem.BackColor = Color.Transparent;
                        inreg_panel.Visible = false;
                    }
                    else
                        MessageBox.Show("Parolele nu sunt identice", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Exista deja un cont cu acest nume", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Nu ati introdus toate datele necesare", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void afis_parola_checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (afis_parola_checkBox1.Checked == true)
            {
                parola_textBox.UseSystemPasswordChar = false;
                parolaok_textBox.UseSystemPasswordChar = false;
            }
            else
            {
                parola_textBox.UseSystemPasswordChar = true;
                parolaok_textBox.UseSystemPasswordChar = true;
            }
        }

        ///~/// CONECTARE ///~///
        private void conectareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inreToolStripMenuItem.Visible = false;
            PanelBackColorTransparent();
            conectareToolStripMenuItem.BackColor = Color.LightBlue;
            PanelVisibleFalse();
            conec_panel.Visible = true;
            nume_textBox2.Text = parola_textBox2.Text = "";
            conec_panel.Location = new Point(1100 / 2 - conec_panel.Width / 2, 600 / 2 - conec_panel.Height / 2); // centrez panel-ul
            incercari_parola = 0;
        }

        private void conectare_button_Click(object sender, EventArgs e)
        {
            nume_utilizator = nume_textBox2.Text;
            string nume = nume_textBox2.Text, parola = parola_textBox2.Text;
            if (nume != "" && parola != "")
            {
                if (ExistaCont(nume))
                {
                    if (ParolaUtilizator(nume) == parola)
                    {
                        MessageBox.Show("V-ati conectat cu succes");
                        inreToolStripMenuItem.Visible = false;
                        conectareToolStripMenuItem.Visible = false;
                        inreToolStripMenuItem.BackColor = Color.Transparent;
                        conec_panel.Visible = false;
                        matriciToolStripMenuItem.Visible = true;
                        statisticiToolStripMenuItem.Visible = true;
                        gestionareContToolStripMenuItem.Visible = true;
                        conectat_label.Text = "Nume: " + nume + "\n";
                        conectat_label.Text += "Status: conectat";
                        conectat_label.Visible = true;
                    }
                    else
                    {
                        if (incercari_parola < 2)
                        {
                            incercari_parola++;
                            MessageBox.Show("Ai gresit parola" + "\n" + "Mai ai " + (3 - incercari_parola) + " incercari", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Ai gresit parola de prea multe ori", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                }
                else
                    MessageBox.Show("Nu exista un cont cu acest nume", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Nu ati introdus toate datele necesare", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void afis_parola_checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (afis_parola_checkBox2.Checked == true)
                parola_textBox2.UseSystemPasswordChar = false;
            else
                parola_textBox2.UseSystemPasswordChar = true;
        }

        ///~/// TEST ///~///
        private void testMatriciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelVisibleFalse();
            test_panel.Visible = true;
            PanelBackColorTransparent();
            matriciToolStripMenuItem.BackColor = Color.LightBlue;
            test_panel.Location = new Point(1100 / 2 - test_panel.Width / 2, 600 / 2 - test_panel.Height / 2); // centrez panel-ul
            radioButton8.Checked = radioButton9.Checked = radioButton10.Checked = radioButton11.Checked = false;
            nr_intrebare = 0;
            punctaj = 0;
            label5.Text = "Intrebarea #" + (nr_intrebare + 1);
            GenerareIntrebare();
        }

        private void next_button_Click(object sender, EventArgs e)
        {
            if (radioButton8.Checked == true || radioButton9.Checked == true || radioButton10.Checked == true || radioButton11.Checked == true)
            {
                if (radioButton8.Checked == true)
                {
                    if (radioButton8.Text == contDataSet.Intrebari[nr_intrebare].Raspuns_corect)
                        punctaj += 20;
                }
                if (radioButton9.Checked == true)
                {
                    if (radioButton9.Text == contDataSet.Intrebari[nr_intrebare].Raspuns_corect)
                        punctaj += 20;
                }
                if (radioButton10.Checked == true)
                {
                    if (radioButton10.Text == contDataSet.Intrebari[nr_intrebare].Raspuns_corect)
                        punctaj += 20;
                }
                if (radioButton11.Checked == true)
                {
                    if (radioButton11.Text == contDataSet.Intrebari[nr_intrebare].Raspuns_corect)
                        punctaj += 20;
                }
                nr_intrebare++;
                if (nr_intrebare < 5)
                {
                    GenerareIntrebare();
                    label5.Text = "Intrebarea #" + (nr_intrebare + 1);
                    radioButton8.Checked = radioButton9.Checked = radioButton10.Checked = radioButton11.Checked = false;
                }
                else
                {
                    MessageBox.Show("Testul s-a incheiat. Ai acumulat " + punctaj + " puncte");
                    test_panel.Visible = false;
                    matriciToolStripMenuItem.BackColor = Color.Transparent;
                    int k = IdUtilizator(nume_utilizator);
                    int nr_teste = contDataSet.Utilizator[k].Nr_teste_efectuate;
                    int punctaj_max = contDataSet.Utilizator[k].Punctaj_maxim;
                    if (punctaj > punctaj_max)
                        utilizatorTableAdapter.ActualizareTestQuery(nr_teste + 1, punctaj, nume_utilizator);
                    else
                        utilizatorTableAdapter.ActualizareTestQuery(nr_teste + 1, punctaj_max, nume_utilizator);
                    utilizatorTableAdapter.Fill(contDataSet.Utilizator);
                }
            }
            else
                MessageBox.Show("Nu ati ales niciun raspuns");
        }

        ///~/// CALCULATOR ///~///
        private void calculatorMatriciToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PanelVisibleFalse();
            calculator_panel.Visible = true;
            PanelBackColorTransparent();
            matriciToolStripMenuItem.BackColor = Color.LightBlue;
            calculator_panel.Location = new Point(1100 / 2 - calculator_panel.Width / 2, 600 / 2 - calculator_panel.Height / 2);
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            calculeaza_button.Enabled = false;
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            calculare_richTextBox.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            radioButton7.Checked = false;
        }

        private void memoreaza_button_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            string[] matrice_text = calculare_richTextBox.Text.Split('\n');
            int i, j;
            nr_linii = matrice_text.Length;
            nr_coloane = 0;
            bool este_matrice = true;
            for (i = 0; i < nr_linii; i++)
            {
                if (matrice_text[i] == string.Empty) // sterg liniile in plus
                {
                    i--;
                    nr_linii--;
                }
            }
            for (i = 0; i < nr_linii; i++)
            {
                string[] linie = matrice_text[i].Split(' ');
                if (i == 0)
                    nr_coloane = linie.Length;
                else
                {
                    if (nr_coloane != linie.Length)
                        este_matrice = false;
                    else
                        nr_coloane = linie.Length;
                }
                for (j = 0; j < nr_coloane; j++)
                    matrice[i + 1, j + 1] = Convert.ToInt32(linie[j]);
            }
            if (este_matrice && nr_linii > 0 && nr_coloane > 0)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                calculeaza_button.Enabled = true;
                for (i = 1; i <= nr_linii; i++)
                    comboBox1.Items.Add(i);
                for (i = 1; i <= nr_coloane; i++)
                    comboBox2.Items.Add(i);
            }
            else
                MessageBox.Show("Textul nu reprezinta o matrice");
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e) // cand utilizatorul bifeaza, poate selecta linia
        {
            if (radioButton6.Checked)
                comboBox1.Visible = true;
            else
                comboBox1.Visible = false;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e) // cand utilizatorul bifeaza, poate selecta coloana
        {
            if (radioButton7.Checked)
                comboBox2.Visible = true;
            else
                comboBox2.Visible = false;
        }

        private void calculeaza_button_Click(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true) // linia
            {
                int indice = Convert.ToInt32(comboBox1.SelectedItem);
                if (indice != 0)
                {
                    if (radioButton1.Checked == true) // suma
                        MessageBox.Show("Suma pe linia " + indice + " este " + SumaLinie(matrice, indice, nr_coloane));
                    if (radioButton2.Checked == true) // produsul
                        MessageBox.Show("Produsul pe linia " + indice + " este " + ProdusLinie(matrice, indice, nr_coloane));
                    if (radioButton3.Checked == true) // media aritmetica
                        MessageBox.Show("Media aritmetica pe linia " + indice + " este " + AvgLinie(matrice, indice, nr_coloane));
                    if (radioButton4.Checked == true) // minimul
                        MessageBox.Show("Minimul pe linia " + indice + " este " + MinLinie(matrice, indice, nr_coloane));
                    if (radioButton5.Checked == true) // maximul
                        MessageBox.Show("Maximul pe linia " + indice + " este " + MaxLinie(matrice, indice, nr_coloane));
                }
                else
                    MessageBox.Show("Nu ati ales o linie");
            }
            if (radioButton7.Checked == true) // coloana
            {
                int indice = Convert.ToInt32(comboBox2.SelectedItem);
                if (indice != 0)
                {
                    if (radioButton1.Checked == true) // suma
                        MessageBox.Show("Suma pe coloana " + indice + " este " + SumaColoana(matrice, indice, nr_linii));
                    if (radioButton2.Checked == true) // produsul
                        MessageBox.Show("Produsul pe coloana " + indice + " este " + ProdusColoana(matrice, indice, nr_linii));
                    if (radioButton3.Checked == true) // media aritmetica
                        MessageBox.Show("Media aritmetica pe coloana " + indice + " este " + AvgColoana(matrice, indice, nr_linii));
                    if (radioButton4.Checked == true) // minimul
                        MessageBox.Show("Minimul pe coloana " + indice + " este " + MinColoana(matrice, indice, nr_linii));
                    if (radioButton5.Checked == true) // maximul
                        MessageBox.Show("Maximul pe coloana " + indice + " este " + MaxColoana(matrice, indice, nr_linii));
                }
                else
                    MessageBox.Show("Nu ati ales o coloana");
            }
        }

        ///~/// GENERARE ///~///
        private void generareMatriciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelVisibleFalse();
            generare_panel.Visible = true;
            PanelBackColorTransparent();
            matriciToolStripMenuItem.BackColor = Color.LightBlue;
            generare_panel.Location = new Point(1100 / 2 - generare_panel.Width / 2, 600 / 2 - generare_panel.Height / 2);
            generare_richTextBox.Clear();
            nrlinii_textBox.Clear();
            nrcoloane_textBox.Clear();
            min_textBox.Clear();
            max_textBox.Clear();
        }

        private void generare_button_Click(object sender, EventArgs e)
        {
            generare_richTextBox.Clear();
            if (nrlinii_textBox.Text != "" && nrcoloane_textBox.Text != "" &&
                min_textBox.Text != "" && max_textBox.Text != "")
            {
                int n = Convert.ToInt32(nrlinii_textBox.Text), m = Convert.ToInt32(nrcoloane_textBox.Text);
                int min = Convert.ToInt32(min_textBox.Text), max = Convert.ToInt32(max_textBox.Text);
                int i, j;
                Random rnd = new Random();
                if (n < 1 || n > 10 || m < 1 || m > 10 || min < -999 || max > 999)
                {
                    MessageBox.Show("Nu pot genera matricea. Dimensiuni incorecte sau valori prea mari", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    for (i = 1; i <= n; i++)
                    {
                        for (j = 1; j <= m; j++)
                        {
                            generare_richTextBox.Text += Convert.ToString(rnd.Next(min, max + 1));
                            generare_richTextBox.Text += " ";
                        }
                        generare_richTextBox.Text += "\n";
                    }
                }
            }
            else
                MessageBox.Show("Nu ati introdus toate datele necesare", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        ///~/// TESTE EFECTUATE ///~///
        private void testeRezolvateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelVisibleFalse();
            statistici_panel.Visible = true;
            testeDataGridView.Visible = true;
            punctajDataGridView.Visible = false;
            PanelBackColorTransparent();
            statisticiToolStripMenuItem.BackColor = Color.LightBlue;
            testeDataGridView.Width = 400;
            testeDataGridView.Height = 150;
            statistici_panel.Location = new Point(100, 100);
            utilizatorTableAdapter.FillByTesteEfectuate(contDataSet.Utilizator);
        }

        ///~/// PUNCTAJ OBTINUT ///~///
        private void punctajObtinutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelVisibleFalse();
            statistici_panel.Visible = true;
            punctajDataGridView.Visible = true;
            testeDataGridView.Visible = false;
            PanelBackColorTransparent();
            statisticiToolStripMenuItem.BackColor = Color.LightBlue;
            punctajDataGridView.Width = 400;
            punctajDataGridView.Height = 150;
            statistici_panel.Location = new Point(100, 100);
            utilizatorTableAdapter.FillByPunctajObtinut(contDataSet.Utilizator);
        }

        ///~/// PARCURGERI IN MATRICE ///~///
        private void parcurgeriInMatriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelVisibleFalse();
            parcurgere_panel.Visible = true;
            PanelBackColorTransparent();
            matriciToolStripMenuItem.BackColor = Color.LightBlue;
            parcurgere_panel.Location = new Point(1100 / 2 - parcurgere_panel.Width / 2, 600 / 2 - parcurgere_panel.Height / 2);
            GenerarePictureBox();
        }

        private void parc_linie_Click(object sender, EventArgs e)
        {
            MatricePic_Gri();
            timer_linie.Start();
            timer_coloana.Stop();
            timer_dp.Stop();
            timer_ds.Stop();
            pos_l = pos_c = 0;
        }

        private void timer_linie_Tick(object sender, EventArgs e)
        {
            matrice_pic[pos_l, pos_c].BackColor = Color.Red;
            pos_l++;
            if (pos_l == 5)
            {
                if (pos_c == 4)
                    timer_linie.Stop();
                pos_c++;
                pos_l = 0;
            }
        }

        private void parc_coloana_Click(object sender, EventArgs e)
        {
            MatricePic_Gri();
            timer_coloana.Start();
            timer_linie.Stop();
            timer_dp.Stop();
            timer_ds.Stop();
            pos_l = pos_c = 0;
        }

        private void timer_coloana_Tick(object sender, EventArgs e)
        {
            matrice_pic[pos_l, pos_c].BackColor = Color.Red;
            pos_c++;
            if (pos_c == 5)
            {
                if (pos_l == 4)
                    timer_coloana.Stop();
                pos_l++;
                pos_c = 0;
            }
        }

        private void parc_dp_Click(object sender, EventArgs e)
        {
            MatricePic_Gri();
            timer_dp.Start();
            timer_coloana.Stop();
            timer_linie.Stop();
            timer_ds.Stop();
            pos_l = pos_c = 0;
        }

        private void timer_dp_Tick(object sender, EventArgs e)
        {
            matrice_pic[pos_l, pos_c].BackColor = Color.Red;
            pos_c++;
            pos_l++;
            if (pos_c == 5)
                timer_dp.Stop();
        }

        private void parc_ds_Click(object sender, EventArgs e)
        {
            MatricePic_Gri();
            timer_ds.Start();
            timer_linie.Stop();
            timer_coloana.Stop();
            timer_dp.Stop();
            pos_l = 4;
            pos_c = 0;
        }

        private void timer_ds_Tick(object sender, EventArgs e)
        {
            matrice_pic[pos_l, pos_c].BackColor = Color.Red;
            pos_c++;
            pos_l--;
            if (pos_c == 5)
                timer_ds.Stop();
        }

        ///~/// GESTIONARE CONTURI ///~///

        private void schimbareParolaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelBackColorTransparent();
            gestionareContToolStripMenuItem.BackColor = Color.LightBlue;
            PanelVisibleFalse();
            schimbareparola_panel.Visible = true;
            schimbareparola_panel.Location = new Point(1100 / 2 - schimbareparola_panel.Width / 2, 600 / 2 - schimbareparola_panel.Height / 2);
        }

        private void schimbaparola_button_Click(object sender, EventArgs e)
        {
            int i, nr_inregistrari = contDataSet.Utilizator.Count;
            if(textBox1.Text != "" && textBox2.Text != "")
            {
                string parola = textBox1.Text, parola_noua = textBox2.Text;
                for (i = 0; i < nr_inregistrari; i++)
                {
                    if (contDataSet.Utilizator[i].Nume == nume_utilizator)
                    {
                        if (contDataSet.Utilizator[i].Parola == parola)
                        {
                            MessageBox.Show("Parola schimbata cu succes");
                            utilizatorTableAdapter.SchimbareParolaQuery(parola_noua, nume_utilizator);
                            utilizatorTableAdapter.Fill(contDataSet.Utilizator);
                            gestionareContToolStripMenuItem.BackColor = Color.Transparent;
                            schimbareparola_panel.Visible = false;
                            textBox1.Clear();
                            textBox2.Clear();
                            break;
                        }
                        else
                            MessageBox.Show("Parola actuala nu este corecta", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Nu ati introdus toate datele necesare", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void stergereContToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nume_utilizator == "admin")
            {
                PanelBackColorTransparent();
                gestionareContToolStripMenuItem.BackColor = Color.LightBlue;
                PanelVisibleFalse();
                stergecont_panel.Visible = true;
                stergecont_panel.Location = new Point(1100 / 2 - stergecont_panel.Width / 2, 600 / 2 - stergecont_panel.Height / 2);
            }
            else
                MessageBox.Show("Acces interzis. Nu sunteti administrator.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void stergecont_button_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                int i, ok = 0, nr_inregistrari = contDataSet.Utilizator.Count;
                string nume = textBox4.Text;
                for (i = 0; i < nr_inregistrari; i++)
                {
                    if (contDataSet.Utilizator[i].Nume == nume)
                    {
                        ok = 1;
                        utilizatorTableAdapter.StergeContQuery(nume);
                        utilizatorTableAdapter.Fill(contDataSet.Utilizator);
                        textBox4.Clear();
                    }
                }
                if (ok == 0)
                    MessageBox.Show("Nu exista un cont cu acest nume", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Contul a fost sters cu succes");
            }
            else
                MessageBox.Show("Nu ati introdus toate datele necesare", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        ///~/// IESIRE ///~///
        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}