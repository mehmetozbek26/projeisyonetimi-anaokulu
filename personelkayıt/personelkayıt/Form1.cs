using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using AnaOkuluBilisim.Models;

namespace AnaOkuluBilisim
{
    public partial class PersonelKayit : Form
    {
        public PersonelGuncelle prsguncelle;
        public PersonelKayit()
        {
            InitializeComponent();

            prsguncelle = new PersonelGuncelle();
            prsguncelle.prskayit = this;
        }
        SqlConnection cnn = new SqlConnection("Data Source=.; database=AnaOkuluDB;integrated security=true");
        private void PersonelKayit_Load(object sender, EventArgs e)
        {


            PersonelGetir();

            cmbDepartman.Items.Add("Yönetim");
            cmbDepartman.Items.Add("Muhasebe");
            cmbDepartman.Items.Add("Banko");
            cmbCinsiyet.Items.Add("ERKEK");
            cmbCinsiyet.Items.Add("KIZ");
            cmbKanGrubu.Items.Add("A rh(+)");
            cmbKanGrubu.Items.Add("A rh(-)");
            cmbKanGrubu.Items.Add("0 rh(+)");
            cmbKanGrubu.Items.Add("A rh(-)");
            cmbKanGrubu.Items.Add("B rh(+)");
            cmbKanGrubu.Items.Add("B rh(-)");
            cmbKanGrubu.Items.Add("AB rh(+)");
            cmbKanGrubu.Items.Add("AB rh(-)");
            cmbAskerlikDurumu.Items.Add("Yapıldı");
            cmbAskerlikDurumu.Items.Add("Yapılmadı");
            cmbAskerlikDurumu.Items.Add("Tecilli");
            cmbOgrenimDurumu.Items.Add("Lise");
            cmbOgrenimDurumu.Items.Add("ÖnLisans");
            cmbOgrenimDurumu.Items.Add("YüksekLisans");
            cmbKayitDurumu.Items.Add("Çalışıyor");
            cmbKayitDurumu.Items.Add("Çalışmıyor");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("sp_PersonelEkle", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Adi", txtAd.Text);
            cmd.Parameters.Add("@SoyAdi", txtSoyad.Text);
            cmd.Parameters.Add("@SicilNo", txtSicilNo.Text);
            cmd.Parameters.Add("@BasvuruTarihi", txtBasvuruTarihi.Text);
            cmd.Parameters.Add("@BaslamaTarihi", txtBaslamaTarihi.Text);
            cmd.Parameters.Add("@AskerlikDurumu", cmbAskerlikDurumu.Text);
            cmd.Parameters.Add("@TecilBitisYili", txtTecilBitisYili.Text);
            cmd.Parameters.Add("@Kangrubu", cmbKanGrubu.Text);
            cmd.Parameters.Add("@Cinsiyet", cmbCinsiyet.Text);
            cmd.Parameters.Add("@Adres", txtAdres.Text);
            cmd.Parameters.Add("@Semt", txtSemt.Text);
            cmd.Parameters.Add("@ilce", txtilce.Text);
            cmd.Parameters.Add("@il", txtil.Text);
            cmd.Parameters.Add("@Mail", txtMail.Text);
            cmd.Parameters.Add("@EvTel", txtEvTel.Text);
            cmd.Parameters.Add("@CepTel", txtCepTel.Text);
            cmd.Parameters.Add("@EgitimDurumu", cmbOgrenimDurumu.Text);
            cmd.Parameters.Add("@AyrilisTarihi", txtAyrilisTarihi.Text);
            cmd.Parameters.Add("@TcNo", txtTcNo.Text);
            cmd.Parameters.Add("@Departman", cmbDepartman.Text);

            cmd.Parameters.Add("@KTcNo", textBox1.Text);
            cmd.Parameters.Add("@KUyruk", txtUyruk.Text);
            cmd.Parameters.Add("@KCinsiyet", comboBox1.Text);
            cmd.Parameters.Add("@KKimlikSeriNo", txtKimlikSeriNo.Text);
            cmd.Parameters.Add("@KDogumYeri", txtDogumYeri.Text);
            cmd.Parameters.Add("@KDogumTarihi", txtDogumTarihi.Text);
            cmd.Parameters.Add("@KDogumili", txtKimlikil.Text);
            cmd.Parameters.Add("@KDogumilce", txtKimlikilce.Text);
            cmd.Parameters.Add("@KMahalle", txtMahalle.Text);
            cmd.Parameters.Add("@KKoy", txtKoy.Text);
            cmd.Parameters.Add("@KCilt", txtCilt.Text);
            cmd.Parameters.Add("@KAile", txtAile.Text);
            cmd.Parameters.Add("@KSiraNo", txtSiraNo.Text);
            cmd.Parameters.Add("@KVerilisYeri", txtVerYeri.Text);
            cmd.Parameters.Add("@KKayitNo", txtKayitNo.Text);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Eklendi.");

            }
            catch
            { MessageBox.Show("Hata Oluştu"); }
            finally
            {
                cnn.Close();
                DataTable tablo = new DataTable();
                tablo.Clear();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Personeller", cnn);
                da.Fill(tablo);
                dataGridView1.DataSource = tablo;
            }
        }
        public void ClearTextBoxes(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child is TextBox)
                {
                    (child as TextBox).Text = "";
                }
                else
                {
                    ClearTextBoxes(child);
                }
            }
        }
        public void PersonelGetir()
        {
            DataTable tablo = new DataTable();
            tablo.Clear();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Personeller", cnn);
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Refresh();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(panel1);
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {

            prsguncelle = new PersonelGuncelle();
            prsguncelle.prskayit = this;
            prsguncelle.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandText = "Delete from Personeller where PersonelId='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
            cmd.ExecuteNonQuery();
            cnn.Close();
            PersonelGetir();
            MessageBox.Show("Kayıt Silindi.");
        }


    }
}