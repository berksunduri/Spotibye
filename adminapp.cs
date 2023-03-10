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
    public partial class adminapp : Form
    {
        string connectionString = @"Data Source=.; Initial Catalog= Spotibye;Integrated Security=True";
        public adminapp()
        {
            InitializeComponent();
        }

        private void adminapp_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT * FROM dbo.Artist";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    dropSongArtist.Items.Add(dr["artistName"]);
                }
                sqlCon.Close();


            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT * FROM Album";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    dropSongAlbum.Items.Add(dr["albumName"]);
                }
                sqlCon.Close();


            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT * FROM Genre";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                   dropSongGenre.Items.Add(dr["genreName"]);
                }
                sqlCon.Close();


            }
        }
        private void buttonSongAdd_Click(object sender, EventArgs e)
        {
            using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("SongAdd",sqlCon);
                string x = txtSongName.Text.Trim();
                var y = Convert.ToDateTime(txtSongDate.Text.Trim());
         
                string z = dropSongArtist.SelectedItem.ToString();
                string c = dropSongAlbum.SelectedItem.ToString();
                string v = dropSongGenre.SelectedItem.ToString();
                string b = txtSongDuration.Text.Trim();
                var n = Convert.ToInt32(txtSongListened.Text.Trim());
                
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@songName",x);
                sqlCmd.Parameters.AddWithValue("@songDate",y);
                sqlCmd.Parameters.AddWithValue("@artistName",z);
                sqlCmd.Parameters.AddWithValue("@albumName",c );
                sqlCmd.Parameters.AddWithValue("@genreName",v);
                sqlCmd.Parameters.AddWithValue("@duration", b);
                sqlCmd.Parameters.AddWithValue("@timesListened",n);
                sqlCmd.ExecuteNonQuery();
                ClearFields();
                MessageBox.Show("Song Added Successfuly");

            }
        }

        private void buttonArtistAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("ArtistAdd", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@artistName",txtArtistName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@country",txtArtistCountry.Text.Trim());
                sqlCmd.ExecuteNonQuery();
                ClearFields();
                MessageBox.Show("Artist Added Successfuly");
            }
        }
        

        private void buttonAlbumAdd_Click(object sender, EventArgs e)
        {
            using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("AlbumAdd", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@albumName",txtAlbumName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@albumDate",Convert.ToDateTime(txtAlbumDate.Text.Trim()));
                sqlCmd.ExecuteNonQuery();
                ClearFields();
                MessageBox.Show("Album Added Successfuly");
            }
        }
        void ClearFields()
        {
            txtArtistName.Text = txtArtistCountry.Text = txtAlbumName.Text = txtAlbumDate.Text=txtSongName.Text=txtSongListened.Text=txtSongDuration.Text=txtSongDate.Text = "";
        }

        private void buttondeleteSong_Click(object sender, EventArgs e)
        {
            var songID = 0;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();


                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT songID FROM dbo.Song WHERE songName='" + txtSongName.Text + "'";
                SqlDataReader dr = sqlCmd.ExecuteReader();
                while (dr.Read())
                {
                    songID = dr.GetInt32(0);
                }
                dr.Close();



            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "delete from dbo.SongPlaylist where SongId=" + songID;
                sqlCmd.ExecuteNonQuery();
            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "DELETE  FROM dbo.Song WHERE songName='"+txtSongName.Text+"'";
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfuly");
            }
        }

        private void deleteAlbum_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "DELETE  FROM dbo.Album WHERE albumName='" + txtAlbumName.Text + "'";
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfuly");
            }
        }

        private void deleteArtist_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "DELETE  FROM dbo.Artist WHERE artistName='" + txtArtistName.Text + "'";
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfuly");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
