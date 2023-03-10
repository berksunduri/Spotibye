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
using static Spotibye.login;

namespace Spotibye
{
    public partial class userapp : Form
    {
        string connectionString = @"Data Source=.; Initial Catalog= Spotibye;Integrated Security=True";
        public enum Genre 
            {

            Pop=1,
            Jazz,
            Classic
        }
        public userapp()
        {
            InitializeComponent();
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedIndex = 0;
        }
        private void playlistButton_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedIndex = 1;
        }

        private void usersButton_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedIndex = 2;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void allsongBox_MouseDown(object sender, MouseEventArgs e)
        {
            userpopList.DoDragDrop(allsongBox.SelectedItem.ToString(), DragDropEffects.Copy);
          
        }

        private void userpopList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void userpopList_DragDrop(object sender, DragEventArgs e)
        {
            int genreId = 0;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                var songToAdd = e.Data.GetData(DataFormats.Text);

                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT genreID FROM dbo.Song WHERE songName = '" + songToAdd.ToString()+"'";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    genreId = dr.GetInt32(0);
                }
                sqlCon.Close();

                if (genreId == (int)Genre.Pop)
                {
                    userpopList.Items.Add(songToAdd);
                    allsongBox.Items.Remove(e.Data.GetData(DataFormats.Text));
                }
                else
                {
                    MessageBox.Show("Song genre isn't compatible.");
                }
            }

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("dbo.PlaylistSongAdd", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@GenreID",(int)Genre.Pop);
                sqlCmd.Parameters.AddWithValue("@songName", e.Data.GetData(DataFormats.Text).ToString());
                sqlCmd.Parameters.AddWithValue("@customerID", Global.customerID);
                sqlCmd.ExecuteNonQuery();

            }
        }

        private void userjazzList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void userjazzList_DragDrop(object sender, DragEventArgs e)
        {
            int genreId = 0;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                var songToAdd = e.Data.GetData(DataFormats.Text);
                
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT genreID FROM dbo.Song WHERE songName = '" + songToAdd.ToString() + "'";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    genreId = dr.GetInt32(0);
                }
                sqlCon.Close();

                if (genreId == (int)Genre.Jazz)
                {
                    userjazzList.Items.Add(e.Data.GetData(DataFormats.Text));
                    allsongBox.Items.Remove(e.Data.GetData(DataFormats.Text));
                }
                else
                {
                    MessageBox.Show("Song genre isn't compatible.");
                }
            }
            
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("dbo.PlaylistSongAdd", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@GenreID", (int)Genre.Jazz);
                sqlCmd.Parameters.AddWithValue("@songName", e.Data.GetData(DataFormats.Text).ToString());
                sqlCmd.Parameters.AddWithValue("@customerID", Global.customerID);
                sqlCmd.ExecuteNonQuery();

            }
        }

        private void userapp_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT a.artistName,s.songName,s.timesListened,s.duration FROM dbo.Song AS s INNER JOIN SongArtists AS sa ON s.songID = sa.songID INNER JOIN Artist AS a ON sa.artistID = a.artistID WHERE s.songID NOT IN (SELECT SongID FROM dbo.SongPlaylist sp INNER JOIN dbo.Playlists p ON p.pID = sp.pID WHERE p.CustomerId = "+Global.customerID+")";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    allsongBox.Items.Add(dr["songName"]);
                }
                sqlCon.Close();
            }
            //Playlists
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT * FROM dbo.Song s  INNER JOIN dbo.SongPlaylist sp ON sp.songID = s.songID INNER JOIN dbo.Playlists p ON p.pID = sp.pID WHERE p.customerID="+Global.customerID+ " AND p.genreID = 1 ";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    userpopList.Items.Add(dr["songName"]);
                }
                sqlCon.Close();
            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT * FROM dbo.Song s  INNER JOIN dbo.SongPlaylist sp ON sp.songID = s.songID INNER JOIN dbo.Playlists p ON p.pID = sp.pID WHERE p.customerID=" + Global.customerID + " AND p.genreID = 2 ";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    userjazzList.Items.Add(dr["songName"]);
                }
                sqlCon.Close();
            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT * FROM dbo.Song s  INNER JOIN dbo.SongPlaylist sp ON sp.songID = s.songID INNER JOIN dbo.Playlists p ON p.pID = sp.pID WHERE p.customerID=" + Global.customerID + " AND p.genreID = 3 ";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    userclassicList.Items.Add(dr["songName"]);
                }
                sqlCon.Close();
            }


            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT * FROM dbo.Customer WHERE isPremium=1 AND customerID NOT IN(SELECT followerID FROM dbo.Followers WHERE customerID="+Global.customerID+") AND customerID!="+Global.customerID;
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    notFollowedList.Items.Add(dr["fullName"]);
                }
                sqlCon.Close();
            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT fullName  FROM dbo.Customer c INNER JOIN dbo.Followers f ON c.customerID = f.followerID WHERE f.customerID="+Global.customerID;
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                   followedList.Items.Add(dr["fullName"]);
                }
                sqlCon.Close();
            }
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT TOP 10 a.artistName,s.songName,s.timesListened FROM dbo.Song AS s INNER JOIN SongArtists AS sa ON s.songID = sa.songID INNER JOIN Artist AS a ON sa.artistID = a.artistID WHERE genreID=1 ORDER BY timesListened DESC ";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    listPop10.Items.Add(dr["artistName"] + "-" + dr["songName"]+"-"+dr["timesListened"]);
                }
                sqlCon.Close();
            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT TOP 10 a.artistName,s.songName,s.timesListened FROM dbo.Song AS s INNER JOIN SongArtists AS sa ON s.songID = sa.songID INNER JOIN Artist AS a ON sa.artistID = a.artistID WHERE genreID=2 ORDER BY timesListened DESC";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    listJazz10.Items.Add(dr["artistName"] + "-" + dr["songName"] + "-" + dr["timesListened"]);
                }
                sqlCon.Close();
            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT TOP 10 a.artistName,s.songName,s.timesListened FROM dbo.Song AS s INNER JOIN SongArtists AS sa ON s.songID = sa.songID INNER JOIN Artist AS a ON sa.artistID = a.artistID WHERE genreID=3 ORDER BY timesListened DESC";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    listClassic10.Items.Add(dr["artistName"] + "-" + dr["songName"] + "-" + dr["timesListened"]);
                }
                sqlCon.Close();
            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT TOP 10 a.artistName,s.songName,s.timesListened FROM dbo.Song AS s INNER JOIN SongArtists AS sa ON s.songID = sa.songID INNER JOIN Artist AS a ON sa.artistID = a.artistID ORDER BY timesListened DESC";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    listGeneral10.Items.Add(dr["artistName"]+"-"+dr["songName"] + "-" + dr["timesListened"]);
                }
                sqlCon.Close();
            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT TOP 10 a.country,a.artistName,s.songName FROM dbo.Song  AS s  INNER JOIN SongArtists AS sa  ON s.songID = sa.songID  INNER JOIN Artist AS a  ON sa.artistID = a.artistID ORDER BY s.timesListened DESC";
                SqlDataReader dr = sqlCmd.ExecuteReader();
             
                while (dr.Read())
                {
                    listCountry10.Items.Add(dr["artistName"] + "-" + dr["songName"] + "-" + dr["country"]);
                }
                sqlCon.Close();
            }
        }

        private void notFollowedList_MouseDown(object sender, MouseEventArgs e)
        {
            followedList.DoDragDrop(notFollowedList.SelectedItem.ToString(), DragDropEffects.Copy);
        }

        private void followedList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void followedList_DragDrop(object sender, DragEventArgs e)
        {
            int userID = 0;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                var userToFollow = e.Data.GetData(DataFormats.Text);

                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT customerID FROM dbo.Customer WHERE fullName='"+userToFollow.ToString()+"'";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    userID = dr.GetInt32(0);
                }
                sqlCon.Close();

               
            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "INSERT INTO dbo.Followers (customerID,followerID) VALUES("+Global.customerID+","+userID+")";
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Followed Successfuly");
            }
            followedList.Items.Add(e.Data.GetData(DataFormats.Text));
            notFollowedList.Items.Remove(e.Data.GetData(DataFormats.Text));
        }

        private void userclassicList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void userclassicList_DragDrop(object sender, DragEventArgs e)
        {
            int genreId = 0;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                var songToAdd = e.Data.GetData(DataFormats.Text);

                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "SELECT genreID FROM dbo.Song WHERE songName = '" + songToAdd.ToString() + "'";
                SqlDataReader dr = sqlCmd.ExecuteReader();

                while (dr.Read())
                {
                    genreId = dr.GetInt32(0);
                }
                sqlCon.Close();

                if (genreId == (int)Genre.Classic)
                {
                    userclassicList.Items.Add(e.Data.GetData(DataFormats.Text));
                    allsongBox.Items.Remove(e.Data.GetData(DataFormats.Text));
                }
                else
                {
                    MessageBox.Show("Song genre isn't compatible.");
                }
            }

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("dbo.PlaylistSongAdd", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@GenreID", (int)Genre.Classic);
                sqlCmd.Parameters.AddWithValue("@songName", e.Data.GetData(DataFormats.Text).ToString());
                sqlCmd.Parameters.AddWithValue("@customerID", Global.customerID);
                sqlCmd.ExecuteNonQuery();

            }
        }

       /* private void userpopList_MouseDown(object sender, MouseEventArgs e)
        {
            allsongBox.DoDragDrop(userpopList.SelectedItem.ToString(), DragDropEffects.Copy);
        }

        private void userjazzList_MouseDown(object sender, MouseEventArgs e)
        {
            allsongBox.DoDragDrop(userjazzList.SelectedItem.ToString(), DragDropEffects.Copy);
        }

        private void userclassicList_MouseDown(object sender, MouseEventArgs e)
        {
            allsongBox.DoDragDrop(userclassicList.SelectedItem.ToString(), DragDropEffects.Copy);
        }

        private void allsongBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void allsongBox_DragDrop(object sender, DragEventArgs e)
        {
            allsongBox.Items.Add(e.Data.GetData(DataFormats.Text));
            userpopList.Items.Remove(e.Data.GetData(DataFormats.Text));
            userjazzList.Items.Remove(e.Data.GetData(DataFormats.Text));
            userclassicList.Items.Remove(e.Data.GetData(DataFormats.Text));
        }*/

    }
}
