using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace LoginFormNew
{
    public partial class frmaccountants : Form
    {
        public frmaccountants()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("server=.;uid=sa;pwd=niit;database=shivammodel");
        SqlDataAdapter adap;
        SqlCommand cmd;
        string qry;
        DataTable dt;
        DataRow dr;
        private void frmaccountants_Load(object sender, EventArgs e)
        {
            qry = "select *From months";
            adap = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            adap.Fill(ds);
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                comselmonth.Items.Add(dr[0].ToString());
            }

        }

        private void btnfeesubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (validatedata() == 0)
                {
                    errorProvider1.Clear();
                    Information.Text = "";
                    qry = "insert into feedetails values('" + txtreg.Text + "','" + comselmonth.Text + "','" + txteducationfee.Text + "','" + txtcomputerfee.Text + "','" + txtlibfee.Text + "','" + txttransfee.Text + "','" + txttotalfee.Text + "')";
                    cmd = new SqlCommand(qry, con);
                    //MessageBox.Show(qry);
                    con.Open();
                    int row = cmd.ExecuteNonQuery();
                    if (row > 0)
                    {
                        MessageBox.Show("Fee Updated");
                    }
                    else
                    {
                        MessageBox.Show("Please Fill the form Correctly");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void txttotalfee_Enter(object sender, EventArgs e)
        {
            Information.Text = "Please Enter Total Fee";
            if ((txteducationfee.Text != "") && (txtcomputerfee.Text != "") && (txtlibfee.Text != "") && (txttransfee.Text != ""))
            {
                int i, j, k, l, m;
                i = Convert.ToInt32(txteducationfee.Text);
                j = Convert.ToInt32(txtcomputerfee.Text);
                k = Convert.ToInt32(txtlibfee.Text);
                l = Convert.ToInt32(txttransfee.Text);
                m = i + j + k + l;
                txttotalfee.Text = m.ToString();
            }
            else
            {
                MessageBox.Show("You Can't Fill This Box The Value Of This Box Is Depend On The Value Of Previous Stock And New Stock");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (val() == 0)
                {
                    errorProvider2.Clear();
                    Information1.Text = "";
                    qry = "select *from studentadmission where regno='" + textBox1.Text + "'";
                    adap = new SqlDataAdapter(qry, con);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        textBox2.Text = dr[1].ToString();
                        textBox3.Text = dr[2].ToString();
                        textBox13.Text = dr[3].ToString();
                        textBox4.Text = dr[4].ToString();
                        textBox5.Text = dr[5].ToString();
                        textBox6.Text = dr[6].ToString();
                        textBox12.Text = dr[8].ToString();
                        textBox7.Text = dr[9].ToString();
                        textBox14.Text = dr[10].ToString();
                        textBox8.Text = dr[11].ToString();
                        textBox9.Text = dr[12].ToString();
                        textBox10.Text = dr[13].ToString();
                        textBox11.Text = dr[16].ToString();
                        maskedTextBox1.Text = dr[17].ToString();
                        maskedTextBox2.Text = dr[18].ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox12.Text = "";
            textBox7.Clear();
            textBox14.Text = "";
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtreg.Clear();
            comselmonth.Text = "";
            txtcomputerfee.Clear();
            txteducationfee.Clear();
            txtlibfee.Clear();
            txttotalfee.Clear();
            txttransfee.Clear();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtreg_DoubleClick(object sender, EventArgs e)
        {
            frmregnoandname reg = new frmregnoandname();
            DialogResult res = reg.ShowDialog();
            if (res == DialogResult.OK)
            {
                txtreg.Text = reg.str1;
            }
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {

            frmregistation reg = new frmregistation();
            DialogResult res = reg.ShowDialog();
            if (res == DialogResult.OK)
            {
                textBox1.Text = reg.str1;
            }
        }

        private void txtreg_Enter(object sender, EventArgs e)
        {
            Information.Text = "Please Select Registration No.";
        }

        private void comselmonth_Enter(object sender, EventArgs e)
        {
            Information.Text = "Please Select Month";
        }

        private void txteducationfee_Enter(object sender, EventArgs e)
        {
            Information.Text = "Please Enter Education Fee";
        }

        private void txtcomputerfee_Enter(object sender, EventArgs e)
        {
            Information.Text = "Please Enter Computer Fee";
        }

        private void txtlibfee_Enter(object sender, EventArgs e)
        {
            Information.Text = "Please Enter Library Fee";
        }

        private void txttransfee_Enter(object sender, EventArgs e)
        {
            Information.Text = "Please Enter Transportion Fee";
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Information1.Text = "Please Select Registration No.";
        }
        private int validatedata()
        {
            int z = 0;
            if (txtreg.Text == "")
            {
                errorProvider1.SetError(txtreg, "Please Enter Registration No.");
                z = 1;
            }
            if (comselmonth.Text == "")
            {
                errorProvider1.SetError(comselmonth, "Please Select Month");
                z = 1;
            }
            if (txteducationfee.Text == "")
            {
                errorProvider1.SetError(txteducationfee, "Please Enter Education Fee");
                z = 1;
            }
            if (txtcomputerfee.Text == "")
            {
                errorProvider1.SetError(txtcomputerfee, "Please Enter Computer Fee");
                z = 1;
            }
            if (txtlibfee.Text == "")
            {
                errorProvider1.SetError(txtlibfee, "Please Enter Library Fee");
                z = 1;
            }
            if (txttransfee.Text == "")
            {
                errorProvider1.SetError(txttransfee, "Please Enter Transportion Fee");
                z = 1;
            }
            if (txttotalfee.Text == "")
            {
                errorProvider1.SetError(txttotalfee, "Please Enter Total Fee");
                z = 1;
            }
            return z;
        }
        private int val()
        {
            int y = 0;
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "Please Select Registration NO.");
                y = 1;
            }
            return y;
        }
    }
}