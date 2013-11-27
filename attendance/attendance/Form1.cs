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
    public partial class frmattendence : Form
    {
        public frmattendence()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("server=.;uid=sa;pwd=niit;database=Eramodel");
        SqlCommand cmd;
        SqlDataAdapter adap;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        string qry;
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Information.Text = "";
                listBox1.Items.Clear();
                listBox3.Items.Clear();
                if (validatedata() == 0)
                {
                    errorProvider1.Clear();
                    qry = "select * from studentadmission where section='" + comboBox3.SelectedItem + "' and classadmission='" + comboBox2.SelectedItem + "'";
                    //MessageBox.Show(qry);
                    adap = new SqlDataAdapter(qry, con);
                    ds = new DataSet();
                    if (adap.Fill(ds) != 0)
                    {
                        dt = ds.Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            listBox1.Items.Add(dr[1].ToString());
                            listBox3.Items.Add(dr[4].ToString() + " " + dr[5].ToString());

                        }
                    }
                    else
                    {
                        MessageBox.Show("NO DATA AVAILABLE", "MESSAGE");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string str;
        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                Information.Text = "";
                string qry3 = "select * from studentadmission where roll='" + listBox1.SelectedItem + "' and classadmission='" + comboBox2.Text + "' and section='" + comboBox3.Text + "'";
                //MessageBox.Show(qry3);
                con.Open();
                cmd = new SqlCommand(qry3, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    str = sdr[4].ToString();
                }
                con.Close();
                if (listBox1.Text != "")
                {
                    if (radioButton1.Checked || radioButton2.Checked)
                    {
                        SqlConnection con1 = new SqlConnection("server=.;uid=sa;pwd=niit;database=Eramodel");
                        string str2 = "select studentroll from studentattendence where studentroll='" + listBox1.SelectedItem + "' and dateofattendence='" + textBox1.Text + "' and class='" + comboBox2.Text + "' and section='" + comboBox3.Text + "'";
                        cmd = new SqlCommand(str2, con1);
                        //MessageBox.Show(str2);
                        con1.Open();
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            SqlConnection con2 = new SqlConnection("server=.;uid=sa;pwd=niit;database=Eramodel");
                            string str3 = "update studentattendence set attendence='" + radioButton1.Checked + "' where studentroll='" + listBox1.SelectedItem + "' and dateofattendence='" + textBox1.Text + "' and class='" + comboBox2.Text + "' and section='" + comboBox3.Text + "'";
                            //MessageBox.Show(str3);
                            con2.Open();
                            cmd = new SqlCommand(str3, con2);
                            int a = cmd.ExecuteNonQuery();
                            if (a > 0)
                            {
                                MessageBox.Show("Update successfully", "Message");
                            }
                            else
                            {
                                MessageBox.Show("Update not done", "Message");
                            }
                            con2.Close();
                        }
                        else
                        {
                            qry = "insert into studentattendence values('" + listBox1.Text + "','" + str + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + radioButton1.Checked + "','" + textBox1.Text + "')";
                            SqlCommand cmd1 = new SqlCommand(qry, con);
                            con.Open();
                            //MessageBox.Show(qry);
                            int row = cmd1.ExecuteNonQuery();
                            if (row > 0)
                            {
                                MessageBox.Show("Insert", "Message");
                            }
                            else
                            {
                                MessageBox.Show("Data not inserted", "Message");
                            }
                            con.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please fill attendence", "Message");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill roll no. and attendence", "Message");
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


        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private int validatedata()
        {
            int i = 0;
            if (comboBox2.Text == "")
            {
                comboBox2.BackColor = Color.IndianRed;
                errorProvider1.SetError(comboBox2, "Please enter student class");
                i = 1;
            }
            else
                comboBox2.BackColor = Color.White;
            if (comboBox3.Text == "")
            {
                comboBox3.BackColor = Color.IndianRed;
                errorProvider1.SetError(comboBox3, "Please enter student section");
                i = 1;
            }
            else
                comboBox3.BackColor = Color.White;
            return i;
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label11_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmshowattendence showattend = new frmshowattendence();
            showattend.Show();
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            Information.Text = "Please enter student class";
        }

        private void comboBox3_Enter(object sender, EventArgs e)
        {
            Information.Text = "Please enter student section";
        }

        private void frmattendence_Load(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToShortDateString();
        }
    }
}
