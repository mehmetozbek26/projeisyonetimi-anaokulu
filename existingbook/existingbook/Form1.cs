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
    public partial class frmaddexistingbooks : Form
    {
        public frmaddexistingbooks()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("server=.;uid=sa;pwd=niit;database=Eramodel");
        //DataSet ds;
        SqlCommand cmd;
        string qry;
        int row;
        SqlDataReader sdr;
        private void btnnewbook_Click(object sender, EventArgs e)
        {
            try
            {
                Information1.Text = "";
                if (validatedata() == 0)
                {
                    errorProvider1.Clear();
                    qry = "update newbooks set date='" + dateofentry.Text + "',price='" + textBox1.Text + "',totalstock ='" + txttotalstock.Text + "' where bookcode='" + textBox2.Text + "'";
                    //MessageBox.Show(qry);
                    cmd = new SqlCommand(qry, con);
                    con.Open();
                    row = cmd.ExecuteNonQuery();
                    if (row > 0)
                    {
                        MessageBox.Show("Books details are inserted");
                    }
                    else
                    {
                        MessageBox.Show("Fill the form correctly ");
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
        private void btnreset_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox2.Clear();
            txtnewstock.Clear();
            txtbookname.Clear();
            txtprice.Clear();
            dateofentry.Text = "";
            txtpreviousstock.Clear();
            txttotalstock.Clear();
            textBox1.Clear();
        }

        private void txttotalstock_Enter(object sender, EventArgs e)
        {
            try
            {
                Information1.Text = "Please enter total stock";
                if ((txtpreviousstock.Text != "") && (txtnewstock.Text != ""))
                {
                    int i, j, k;
                    i = Convert.ToInt32(txtpreviousstock.Text);
                    j = Convert.ToInt32(txtnewstock.Text);
                    k = i + j;
                    txttotalstock.Text = k.ToString();
                }
                else
                {
                    MessageBox.Show("You can not fill this box the value of this box is depended on the value of previous stock and new stock");
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int validatedata()
        {
            int i = 0;
            if (textBox2.Text == "")
            {
                textBox2.BackColor = Color.LightCoral;
                errorProvider1.SetError(textBox2, "Please enter book code");
                i = 1;
            }
            else
            {
                textBox2.BackColor = Color.White;
            }
            if (textBox1.Text == "")
            {
                textBox1.BackColor = Color.LightCoral;
                errorProvider1.SetError(textBox1, "Please enter book code");
                i = 1;
            }
            else
            {
                textBox1.BackColor = Color.White;
            }

            if (dateofentry.Value > DateTime.Now)
            {
                MessageBox.Show("The date that you are entered is greater than Computer's date", "Error");
                errorProvider1.SetError(dateofentry, "Please enter date of entry books");
                i = 1;
            }

            if (txtnewstock.Text == "")
            {
                txtnewstock.BackColor = Color.LightCoral;
                errorProvider1.SetError(txtnewstock, "Please enter new stock");
                i = 1;
            }
            else
            {
                txtnewstock.BackColor = Color.White;
            }
            if (txttotalstock.Text == "")
            {
                txttotalstock.BackColor = Color.LightCoral;
                errorProvider1.SetError(txttotalstock, "Please enter total stock");
                i = 1;
            }
            else
            {
                txttotalstock.BackColor = Color.White;
            }
            return i;
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmbookcode bk = new frmbookcode();
                DialogResult res = bk.ShowDialog();
                if (res == DialogResult.OK)
                {
                    textBox2.Text = bk.str;
                    qry = "select * from newbooks where bookcode='" + textBox2.Text + "'";
                    con.Open();
                    cmd = new SqlCommand(qry, con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        txtbookname.Text = sdr[1].ToString();
                        txtpreviousstock.Text = sdr[6].ToString();
                        txtprice.Text = sdr[5].ToString();
                        textBox4.Text = sdr[3].ToString();
                    }
                    con.Close();
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            Information1.Text = "Please enter book code";
        }

        private void txtbookname_Enter(object sender, EventArgs e)
        {
            Information1.Text = "Please enter book name";
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            Information1.Text = "Please enter previous bill no.";
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            Information1.Text = "Please enter current bill no.";
        }

        private void dateofentry_Enter(object sender, EventArgs e)
        {
            Information1.Text = "Please enter date of entry";
        }

        private void txtprice_Enter(object sender, EventArgs e)
        {
            Information1.Text = "Please enter previous price";
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Information1.Text = "Please enter new price";
        }

        private void txtpreviousstock_Enter(object sender, EventArgs e)
        {
            Information1.Text = "Please enter previous stock";
        }

        private void txtnewstock_Enter(object sender, EventArgs e)
        {
            Information1.Text = "Please enter new stock";
            txttotalstock.Clear();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
            {
                e.Handled = true;
                MessageBox.Show("You can not enter characters.", "Message");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
            {
                e.Handled = true;
                inform.Text = "Please enter correct format";
                inform.Visible = true;
            }
            else
            {
                inform.Visible = false;
            }
        }

        private void txtnewstock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
            {
                e.Handled = true;
                inform.Text = "Please enter correct format";
                inform.Visible = true;
            }
            else
            {
                inform.Visible = false;
            }
        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox2, "Double click this box");
        }
    }
}