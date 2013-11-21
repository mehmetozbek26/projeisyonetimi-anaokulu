using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace AnaOkuluBilisim
{
    public partial class SinifEkle : Form
    {

        public SinifEkle()
        {
            InitializeComponent();


        }
        SqlConnection cnn = new SqlConnection("Data Source=.; database=AnaOkuluDB;integrated security=true");
        public void sinifbilgileri()
        {
            DataTable dtt = new DataTable();
            cnn.Open();
            SqlDataAdapter dat = new SqlDataAdapter("Select * from Siniflar", cnn);
            dat.Fill(dtt);
            dataGridView2.DataSource = dtt;
            cnn.Close();
        }
        private void SinifEkle_Load(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Ogretmenler", cnn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cnn.Close();

            sinifbilgileri();



            txtOgretmenAdi.Enabled = false;
            txtOgretmenSoyAdi.Enabled = false;

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtOgretmenAdi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtOgretmenSoyAdi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            int ogrId = 0;

            SqlCommand cmd = new SqlCommand("sp_SinifEkle", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SinifAdi", txtSinifAdi.Text);
            cmd.Parameters.Add("@KapaSitesi", txtKapasite.Text);
            cmd.Parameters.Add("@OgretmenId", ogrId);

            cnn.Open();
            ogrId = Convert.ToInt32(cmd.ExecuteScalar());

            MessageBox.Show("Sinif Eklendi.");
            cnn.Close();
            sinifbilgileri();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandText = "Delete from Siniflar where sinifId='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'";
            cmd.ExecuteNonQuery();
            cnn.Close();
            sinifbilgileri();
            MessageBox.Show("Sinif Silindi");


        }








    }
}
