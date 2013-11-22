using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace AnaOkuluBilisim
{
    public partial class YemekList : Form
    {
        public YemekList()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection("Data Source=.; database=AnaOkuluDB;integrated security=true");

        public void YemekListele()
        {
            DataTable dt = new DataTable();
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Yemekler", cnn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cnn.Close();
        }
        public void MailleriGetir()
        {
            DataTable dt = new DataTable();
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select EMail from Veliler", cnn);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            DataGridViewColumn column = dataGridView2.Columns[0];
            column.Width = dataGridView2.Width;

            cnn.Close();
        }
        private void YemekList_Load(object sender, EventArgs e)
        {
            MailleriGetir();
            YemekListele();
        }
        private void btnYukle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("sp_YemekYukle", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Corba", txtCorba.Text);
            cmd.Parameters.Add("@AnaYemek", txtAnaYemek.Text);
            cmd.Parameters.Add("@Tatli", txtTatli.Text);
            cmd.Parameters.Add("@Tarih", dateTimePicker1.Text);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Liste Eklendi.");
            }
            catch
            {
                MessageBox.Show("Hata Oluştu.");
            }
            finally
            {
                cnn.Close();
                YemekListele();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Veri Yok");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Delete from Yemekler where YemekId='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                YemekListele();
                MessageBox.Show("Liste Silindi.");
            }
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rw in dataGridView2.Rows)
            {
                string a = rw.Cells[0].Value.ToString();



                MailMessage mail = new MailMessage(); //
                mail.From = new MailAddress("ilkerarslan49@gmail.com", "Ana Okulu"); //Mailin kimden gittiğini belirtiyoruz
                mail.To.Add(a); //Mailin kime gideceğini belirtiyoruz
                mail.Subject = "Yemek Listesi"; //Mail konusu          
                mail.Body = dataGridView1.CurrentRow.Cells[1].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + "-" + dateTimePicker1.Text; //Mailin içeriği                  
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential("ilkerarslan49@gmail.com", "tezteztez");
                sc.Send(mail);
                MessageBox.Show("Mail Gönderildi.");
            }
        }
    }
}
