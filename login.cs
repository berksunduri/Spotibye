using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotibye
{
    public partial class login : Form
    {
        string connectionString = @"Data Source=.; Initial Catalog= Spotibye;Integrated Security=True";
        public login()
        {
            InitializeComponent();
           
        }
        public static class Global
        {
            public static int customerID;

        }
        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            createacc ca = new createacc();
            ca.ShowDialog();

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query="SELECT * FROM Customer WHERE fullName='"+txtUsername.Text.Trim()+"'AND pass='"+txtPassword.Text.Trim()+"'";
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlCon);
                string query1 = "SELECT customerID FROM Customer WHERE fullName = '" + txtUsername.Text.Trim() + "'AND pass = '" + txtPassword.Text.Trim() + "'";
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText="SELECT customerID FROM Customer WHERE fullName = '" + txtUsername.Text.Trim() + "'AND pass = '" + txtPassword.Text.Trim() + "'";
                SqlDataReader dr = sqlCmd.ExecuteReader();
                while(dr.Read())
                {
                    Global.customerID = dr.GetInt32(0);
                }
                dr.Close();
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                if(dtbl.Rows.Count==1)
                {
                    this.Hide();
                    userapp ua = new userapp();
                    ua.Show();
                }
                else
                {
                    MessageBox.Show("Wrong username or password");
                }
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminlogin al = new adminlogin();
            al.Show();
        }
    }
}
