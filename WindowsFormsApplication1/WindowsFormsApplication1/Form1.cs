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
    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
            txtpwd.Text = "niit";
            comboBox1.SelectedIndex = 1;
            txtuserid.Text = "mahmut";



        }
        SqlConnection cnn = new SqlConnection("Data Source=.; database=AnaOkuluDB;integrated security=true");

        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Screen.PrimaryScreen.Bounds.Width.ToString() + " x " +
            //    Screen.PrimaryScreen.Bounds.Height.ToString());
        }

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            if (txtuserid.Text == "")
            {
                MessageBox.Show("Kullanıcı Adi giriniz.");
            }
            else if (txtpwd.Text == "")
            {
                MessageBox.Show("Şifre giriniz.");
            }
            else
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select userid,confirmpassword,comdepart from pwd where userid='" + txtuserid.Text + "' and confirmpassword='" + txtpwd.Text + "' and comdepart='" + comboBox1.SelectedItem + "'", cnn);

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (txtuserid.Text.ToString() == rdr["userid"].ToString() && txtpwd.Text.ToString() == rdr["confirmpassword"].ToString())
                    {
                        //MessageBox.Show("Giriş başarılı.");
                    }
                    GlobalClass.GlobalVar = rdr["comdepart"].ToString();
                    Form yeniform = new ListeForm();



                    yeniform.Size = Screen.PrimaryScreen.WorkingArea.Size;
                    yeniform.Show();
                    this.Hide();
                    rdr.Close();
                }
                else
                {
                    MessageBox.Show("Hatalı kullanıcı adı veya şifre.");
                }
                cnn.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }




    }
}
