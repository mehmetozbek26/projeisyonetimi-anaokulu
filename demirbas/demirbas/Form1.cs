using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AnaOkuluBilisim
{
    public partial class DemirbasKayit : Form
    {
        DemirBasGuncelle DmrBasGuncelle;

        public DemirbasKayit()
        {
            InitializeComponent();
            DmrBasGuncelle = new DemirBasGuncelle();
            DmrBasGuncelle.DmrBasKayit = this;
        }
        SqlConnection cnn = new SqlConnection("Data Source=.; database=AnaOkuluDB;integrated security=true");
        private void DemirbasKayit_Load(object sender, EventArgs e)
        {
            DemirbasGetir();
            DemirbasMekanGetir();
        }
        int DemirId = 0;
        private void button1_Click(object sender, EventArgs e)
        {

            
            try
            {
                cnn.Open();
                DemirId = Convert.ToInt32(cmd.ExecuteScalar());
                MessageBox.Show("Kayıt Başarılı.");
            }
            catch
            {
                MessageBox.Show("Hata Oluştu.");
            }
            finally
            {
                cnn.Close();
                DemirbasGetir();
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
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(panel1);
            ClearTextBoxes(panel2);
        }
        public void DemirbasGetir()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("select * from demirbaslar", cnn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public void DemirbasMekanGetir()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("select * from demirbasmekanlari", cnn);
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandText = "delete from Demirbaslar where DemirbasId ='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' ";
            cmd.ExecuteNonQuery();
            cnn.Close();
            DemirbasGetir();
            MessageBox.Show("Kayıt Silindi.");

        }
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            DmrBasGuncelle.ShowDialog();
        }

        private void btnMekanKaydet_Click(object sender, EventArgs e)
        {


            SqlCommand cmd = new SqlCommand("sp_DemirbasMekaniEkle", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DemirbasId", DemirId);
            cmd.Parameters.Add("@BulunduguYer", txtBulunduguYer.Text);
            cmd.Parameters.Add("@Adet", txtAdeti.Text);
            cmd.Parameters.Add("@Sorumlusu", txtSorumlusu.Text);
            cmd.Parameters.Add("@TeslimTarihi", txtTeslimTarihi.Text);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Mekanlar Eklendi");
            }
            catch
            {
                MessageBox.Show("Hata Oluştu");
            }
            finally
            {
                cnn.Close();

            }
        }
        private void btnMekanSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from DemirbasMekanlari where MekanId='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'  ", cnn);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
            MessageBox.Show("Veri Silindi.");
            DemirbasMekanGetir();
        }






















    }


}
