using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace AnaOkuluBilisim
{
    public partial class YoklamaDurum : Form
    {
        public YoklamaDurum()
        {
            InitializeComponent();
        }


        SqlConnection cnn = new SqlConnection("Data Source=.; database=AnaOkuluDB;integrated security=true");
        private void YoklamaDurum_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from siniflar", cnn);
            cnn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            cmbDurum.Items.Insert(0, "Seçiniz");
            while (rdr.Read())
            {
                //comboBox1.Items.Add(rdr["SinifAdi"].ToString());
                cmbDurum.Items.Insert(Convert.ToInt32(rdr["sinifId"].ToString()), rdr["SinifAdi"].ToString());
            }
            cnn.Close();
            rdr.Close();
        }
        private void cmbDurum_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter("sp_YoklamaGoster", cnn))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(cmbDurum.SelectedIndex.ToString());
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnYoklamaDurum_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                string a = rw.Cells[11].Value.ToString();
                MailMessage mail = new MailMessage(); //
                mail.From = new MailAddress("cengmahmutay@gmail.com", "Ana Okulu"); //Mailin kimden gittiğini belirtiyoruz
                mail.To.Add(a); //Mailin kime gideceğini belirtiyoruz
                mail.Subject = "Yemek Listesi"; //Mail konusu          
                mail.Body = dataGridView1.CurrentRow.Cells[1].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[4].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[5].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[6].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[7].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[8].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[9].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[10].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[11].Value.ToString() + "-" + dataGridView1.CurrentRow.Cells[12].Value.ToString(); //Mailin içeriği                  
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential("cengmahmutay@gmail.com", "eeeeeeeeeee");
                sc.Send(mail);
                MessageBox.Show("Mail Gönderildi.");
            }
        }
    }
}
