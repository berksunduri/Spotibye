using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotibye
{
    public partial class adminlogin : Form
    {
        public adminlogin()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text=="admin"&&txtPassword.Text=="admin")
            {
                this.Hide();
                adminapp aa = new adminapp();
                aa.Show();
            }
            else
            {
                MessageBox.Show("Incorrect credentials for ADMIN.");
            }
        }
    }
}
