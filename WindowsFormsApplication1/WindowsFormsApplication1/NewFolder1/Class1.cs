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

namespace AnaOkuluBilisim
{
    public partial class OgrDuzenle : Form
    {
        public Ogrenciler ogrenciler;
        public OgrDuzenle()
        {

            InitializeComponent();


        }
        DataTable tablo = new DataTable();
        DataGridView dataGridView1 = new DataGridView();
        SqlConnection cnn = new SqlConnection("Data Source=.; database=AnaOkuluDB;integrated security=true");
        private void OgrDuzenle_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'anaOkuluDBDataSet11.Ogrenciler' table. You can move, or remove it, as needed.

            // TODO: This line of code loads data into the 'anaOkuluDBDataSet11.Ogrenciler' table. You can move, or remove it, as needed.

            // TODO: This line of code loads data into the 'anaOkuluDBDataSet1.Ogrenciler' table. You can move, or remove it, as needed.


            string ogrid = (GlobalClass.Ogrid);
            dataGridView1.RowTemplate.Height = 100;
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Select Adi,Soyadi,Adres,Semt,ilce,il,PostaKodu,EvTel,KanGrubu,TcNo,Uyruk,Cinsiyet,KimlikSeriNo,DogumYeri,DogumTarihi,Dogumili,Dogumilce,Mahalle,Koy,Cilt,Aile,SiraNo,VerilisYeri,KayitNo,Resim,KayitTarihi,CikisTarihi,SinifId,Sinifi,Servisi,DavranisSorunu,Yapilanlar,YasitlariylaIliskisi,Fobileri,SevdigiYiyecekler,Asilar,GecirdigiHastaliklar,Alerjiler,Ameliyatlar,SaglikSorunlari,ilac,Protez,Diyet,RuhsalDurum from Ogrenciler Where OgrenciId='" + ogrid + "'", cnn);

            SqlCommand cmd2 = new SqlCommand("Select Adi,Soyadi,Ceptel,EvTel,TcNo,YakinlikDerecesi,Meslek,Email from Veliler Where OgrenciId='" + ogrid + "'", cnn);
            SqlCommand cmd3 = new SqlCommand("Select Adi,Soyadi,CepTel,EvTel,TcNo,YakinlikDerecesi,Meslek,Email from UcuncuSahislar Where OgrenciId='" + ogrid + "'", cnn);
            //SqlDataAdapter da = new SqlDataAdapter("Select o.OgrenciId,o.Adi,o.Soyadi,o.Adres,o.Semt,o.ilce,o.il,o.PostaKodu,o.EvTel,o.KanGrubu,o.TcNo,o.Uyruk,o.Cinsiyet,o.KimlikSeriNo,o.DogumYeri,o.DogumTarihi,o.Dogumili,o.Dogumilce,o.Mahalle,o.Koy,o.Cilt,o.Aile,o.SiraNo,o.VerilisYeri,o.KayitNo,o.Resim,o.KayitTarihi,o.CikisTarihi,o.SinifId,o.Sinifi,o.Servisi,o.DavranisSorunu,o.Yapilanlar,o.YasitlariylaIliskisi,o.Fobileri,o.SevdigiYiyecekler,o.Asilar,o.GecirdigiHastaliklar,o.Alerjiler,o.Ameliyatlar,o.SaglikSorunlari,o.ilac,o.Protez,o.Diyet,o.RuhsalDurum,v.Adi,v.Soyadi,v.Ceptel,v.EvTel,v.TcNo,v.YakinlikDerecesi,v.Meslek,v.Email,u.Adi,u.Soyadi,u.CepTel,u.EvTel,u.TcNo,u.YakinlikDerecesi,u.Meslek,u.Email from Ogrenciler as o,Veliler as v,UcuncuSahislar as u Where o.OgrenciId='" + ogrid + "'", cnn);

            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            txtPostaKodu.Text = dr["PostaKodu"].ToString();
            txtAile.Text = dr["Aile"].ToString();
            txtOgrenciAd.Text = dr["Adi"].ToString();
            textBox3.Text = dr["Alerjiler"].ToString();
            txtOgrenciSoyad.Text = dr["Soyadi"].ToString();
            txtOgrenciAdres.Text = dr["Adres"].ToString();
            txtSemt.Text = dr["Semt"].ToString();
            txtIlce.Text = dr["ilce"].ToString();
            txtil.Text = dr["il"].ToString();
            txtDogumYeri.Text = dr["DogumYeri"].ToString();
            txtDogumTarihi.Text = dr["DogumTarihi"].ToString();
            txtKimlikil.Text = dr["Dogumili"].ToString();
            txtKimlikilce.Text = dr["Dogumilce"].ToString();
            txtMahalle.Text = dr["Mahalle"].ToString();
            txtKoy.Text = dr["Koy"].ToString();
            txtCilt.Text = dr["Cilt"].ToString();
            txtSiraNo.Text = dr["SiraNo"].ToString();
            txtVerYeri.Text = dr["VerilisYeri"].ToString();
            txtKayitNo.Text = dr["KayitNo"].ToString();
            byte[] resim = dr["Resim"] as byte[];
            MemoryStream ms = new MemoryStream();

            ms.Write(resim, 0, resim.Length);
            Image img = Image.FromStream(ms);
            pictureBox1.Image = img;

            txtKayitTarihi.Text = dr["KayitTarihi"].ToString();
            txtCikisTarihi.Text = dr["CikisTarihi"].ToString();
            cmbSinif.Items.Add(dr["Sinifi"].ToString());
            cmbSinif.SelectedIndex = 0;
            cmbServis.Items.Add(dr["Servisi"].ToString());
            cmbServis.SelectedIndex = 0;
            txtDavranisSorunlari.Text = dr["DavranisSorunu"].ToString();
            txtYapilanlar.Text = dr["Yapilanlar"].ToString();
            txtYasitlariylailiskisi.Text = dr["YasitlariylaIliskisi"].ToString();
            txtFobileri.Text = dr["Fobileri"].ToString();
            txtYiyecekler.Text = dr["SevdigiYiyecekler"].ToString();
            textBox9.Text = dr["Asilar"].ToString();
            textBox1.Text = dr["GecirdigiHastaliklar"].ToString();
            textBox4.Text = dr["Ameliyatlar"].ToString();
            textBox5.Text = dr["SaglikSorunlari"].ToString();
            textBox6.Text = dr["ilac"].ToString();
            textBox10.Text = dr["Protez"].ToString();
            txtDiyet.Text = dr["Diyet"].ToString();
            textBox7.Text = dr["RuhsalDurum"].ToString();
            txtEvTel.Text = dr["EvTel"].ToString();
            txtUyruk.Text = dr["Uyruk"].ToString();
            txtKimlikSeriNo.Text = dr["KimlikSeriNo"].ToString();
            txtTcNo.Text = dr["TcNo"].ToString();
            cmbKanGrubu.Items.Add(dr["KanGrubu"].ToString());
            cmbKanGrubu.SelectedIndex = 0;
            cmbCinsiyet.Items.Add(dr["Cinsiyet"].ToString());
            cmbCinsiyet.SelectedIndex = 0;

            dr.Close();







            SqlDataReader dr2 = cmd2.ExecuteReader();
            dr2.Read();
            txtVeliCep.Text = dr2["CepTel"].ToString();
            textBox2.Text = dr2["Adi"].ToString();
            txtVeliSoyAdi.Text = dr2["Soyadi"].ToString();
            txtVeliEvTel.Text = dr2["EvTel"].ToString();
            txtVeliTc.Text = dr2["TcNo"].ToString();
            txtVeliYakinlikDerecesi.Text = dr2["YakinlikDerecesi"].ToString();
            txtVeliMeslek.Text = dr2["Meslek"].ToString();
            txtVeliEmail.Text = dr2["Email"].ToString();
            dr2.Close();
            SqlDataReader dr3 = cmd3.ExecuteReader();
            dr3.Read();
            txtUcuncuSahisCep.Text = dr3["CepTel"].ToString();
            txtUcuncuSahisAd.Text = dr3["Adi"].ToString();
            txtUcuncuSahisSoyad.Text = dr3["Soyadi"].ToString();
            txtUcuncuSahisEvTel.Text = dr3["EvTel"].ToString();
            txtUcuncuSahisTc.Text = dr3["TcNo"].ToString();
            txtUcuncuSahisYakinlikDerecesi.Text = dr3["YakinlikDerecesi"].ToString();
            txtUcuncuSahisMeslek.Text = dr3["Meslek"].ToString();
            txtUcuncuSahisEmail.Text = dr3["Email"].ToString();
            dr3.Close();
            cnn.Close();
        }








    }
}