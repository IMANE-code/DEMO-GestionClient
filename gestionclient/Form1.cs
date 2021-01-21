using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gestionetudiant
{

    public partial class Form1 : Form
    {
        //CurrencyManager cm;
        SqlConnection conx = new SqlConnection(@"Data Source=DESKTOP-N8LUDQ5;Initial Catalog=clients;Integrated Security=True");
        DataSet ds = new DataSet();
        SqlDataAdapter Dap = new SqlDataAdapter();
       
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conx.Open();

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == ""|| textBox4.Text == "")
            {
                MessageBox.Show("un champ vide !!!!!");
            }
            else
            {
                DataRow ligne = ds.Tables["client"].NewRow();
                ligne[0]= textBox1.Text;
                ligne[1]= textBox2.Text;
                ligne[2]= textBox3.Text;
                ligne[3]= textBox4.Text;
               
                ds.Tables["client"].Rows.Add(ligne);
                MessageBox.Show("client est bien ajouté :)");

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox1.Focus();
            }
            
            conx.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) 
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            conx.Open();

            Dap = new SqlDataAdapter("select * from client", conx);

            //remplir gridview
            Dap.Fill(ds, "client");
            dataGridView1.DataSource = ds.Tables["client"];

            //remplir combobox
            for (int i = 0; i < ds.Tables["client"].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables["client"].Rows[i][1]);
            }

            conx.Close();
        }

    
        private void button2_Click_1(object sender, EventArgs e)
        {
            conx.Open();
            int pos = -1;
            if (textBox1.Text == "")
            {
                MessageBox.Show("id vide !!!");
                return;
            }
            for ( int i = 0; i < ds.Tables["client"].Rows.Count; i++)
            {
                if (ds.Tables["client"].Rows[i][0].ToString() == textBox1.Text)
                {
                    pos = i;break;
                }
            }
            if (pos != -1)
            {
                ds.Tables["client"].Rows[pos].Delete();
                MessageBox.Show("ce client est supprimé ");
            }
            else
            {
                MessageBox.Show("ce client n'est existe pas !!!");
            }

            conx.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conx.Open();
            int pos = -1;
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("champ vide !!!");
                return;
            }
            for (int i = 0; i < ds.Tables["client"].Rows.Count; i++)
            {
                if (ds.Tables["client"].Rows[i][0].ToString() == textBox1.Text)
                {
                    pos = i; break;

                }
            }

            if (pos != -1)
            {
                ds.Tables["client"].Rows[pos][1] = textBox2.Text;
                ds.Tables["client"].Rows[pos][2] = textBox3.Text;
                ds.Tables["client"].Rows[pos][3] = textBox4.Text;

                MessageBox.Show("ce client bien modifier !!!");
            }
            else
            {
                MessageBox.Show("ce client n'est existe pas !!!");
            }
            conx.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conx.Open();
            for (int i = 0; i < ds.Tables["client"].Rows.Count; i++) { 
                if (comboBox1.Text == (""+ds.Tables["client"].Rows[i][1]))
                {
                    textBox1.Text = ""+ ds.Tables["client"].Rows[i][0];
                    textBox2.Text = ""+ ds.Tables["client"].Rows[i][1];
                    textBox3.Text = ""+ ds.Tables["client"].Rows[i][2];
                    textBox4.Text = ""+ ds.Tables["client"].Rows[i][3];
                }
            }
            conx.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            //cm.EndCurrentEdit();
            
            conx.Open();
            SqlCommandBuilder sauvgard = new SqlCommandBuilder(Dap);
            Dap.Update(ds,"client");
            conx.Close();
            MessageBox.Show("tous les modifications bien enregistrer","ADD",MessageBoxButtons.OK,MessageBoxIcon.Information);
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Focus();
        }

       
    }
}
