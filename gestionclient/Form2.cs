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
    public partial class Form2 : Form
    {
        SqlConnection conx = new SqlConnection(@"Data Source=DESKTOP-N8LUDQ5;Initial Catalog=clients;Integrated Security=True");
        DataSet ds = new DataSet();
        SqlDataAdapter Dap = new SqlDataAdapter();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
              
            string req = "select * from client where ville = '"+comboBox1.Text+"'";
            SqlCommand cmd = new SqlCommand(req,conx);

            try { 
                conx.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conx.Close();
                dataGridView1.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
            conx.Open();
            Dap = new SqlDataAdapter("select * from client", conx);
            Dap.Fill(ds, "client");

            for (int i = 0; i < ds.Tables["client"].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables["client"].Rows[i][3]);
            }
            conx.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
