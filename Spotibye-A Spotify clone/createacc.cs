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
    public partial class createacc : Form
    {
        string connectionString = @"Data Source=.; Initial Catalog= Spotibye;Integrated Security=True";
        public createacc()
        {
            InitializeComponent();
            
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

            using(SqlConnection sqlCon= new SqlConnection(connectionString))
            {
                if(txtfullName.Text==""||txtPassword.Text==""||txtEmail.Text==""||txtCountry.Text=="")
                {
                    MessageBox.Show("You left a field empty. Can't Create User");
                }
                else
                {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("UserAdd",sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@fullName",txtfullName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@pass", txtPassword.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@country", txtCountry.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@isPremium",checkPremium.Checked);
                
                sqlCmd.ExecuteNonQuery();
                
                
                ClearFields();
                MessageBox.Show("User Created Successfuly");
                    this.Hide();
                    login log = new login();
                    log.ShowDialog();
                }
                

                

            }
            
        }

        private void createacc_Load(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel1_Click(object sender, EventArgs e)
        {

        }
        void ClearFields()
        {
            txtfullName.Text = txtPassword.Text = txtEmail.Text = txtCountry.Text = "";
        }
    }
}
