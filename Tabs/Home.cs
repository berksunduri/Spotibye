using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotibye.Tabs
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'spotibyeDataSet.Customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter.Fill(this.spotibyeDataSet.Customer);
            // TODO: This line of code loads data into the 'spotibyeDataSet.Album' table. You can move, or remove it, as needed.
            this.albumTableAdapter.Fill(this.spotibyeDataSet.Album);


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
