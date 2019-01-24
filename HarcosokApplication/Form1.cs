using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HarcosokApplication
{
    public partial class HarcosokApplication : Form
    
        {
        int databesa;
        MySqlCommand cmd;
        MySqlDataAdapter adpt;
        DataTable dt;

        MySqlConnection con = new MySqlConnection("Server = localhost; Database = cs_harcosok; Uid = root; Pwd = ;");
        public HarcosokApplication()
        {
            InitializeComponent();
            displayData();
            displayData2();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nev = textBox1.Text;
            string d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            using (var conn = new MySqlConnection("Server = localhost; Database = cs_harcosok; Uid = root; Pwd = ;"))
            {
                conn.Open();
                var ellenÖrzes = conn.CreateCommand();
                ellenÖrzes.CommandText = "SELECT COUNT(*) FROM `harcosok` WHERE nev=@nev ";
                ellenÖrzes.Parameters.AddWithValue("@nev", nev);
                var darab = (long)ellenÖrzes.ExecuteScalar();
                if (darab != 0)
                {
                    MessageBox.Show("Van már!!!");
                    return;
                }




                var command = conn.CreateCommand();
                command.CommandText = "INSERT INTO `harcosok`( `nev`, `letrehozas`) VALUES (@nev,@letrehozas)";
                command.Parameters.AddWithValue("@nev", nev);
                
                command.Parameters.AddWithValue("@letrehozas", d);
                int erintettSorok = command.ExecuteNonQuery();
                MessageBox.Show("Felvéve!!!!");
                displayData();
                displayData2();
                textBox1.Clear();
                conn.Close();
                

            }
        }

        public void displayData()
        {
            using (var conn = new MySqlConnection("Server = localhost; Database = cs_harcosok; Uid = root; Pwd = ;"))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "SELECT nev FROM harcosok";

                try
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    comboBox1.Items.Clear();
                    while (reader.Read())
                    {
                        
                        comboBox1.Items.Add(reader["nev"]);
                    }
                    con.Close();
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                conn.Close();
            }
        }
        

       
        private void button2_Click(object sender, EventArgs e)
        {

            int selectedIndex = comboBox1.SelectedIndex;
            Object selectedItem = comboBox1.SelectedItem;
            string nev = textBox2.Text;
            string leiras = textBox3.Text;
            int harcos_id= selectedIndex+1;

           
            using (var conn = new MySqlConnection("Server = localhost; Database = cs_harcosok; Uid = root; Pwd = ;"))
            {
                conn.Open();
                
                var command = conn.CreateCommand();
                command.CommandText = "INSERT INTO `kepessegek`( `nev`, `leiras`, `harcos_id`) VALUES (@nev,@leiras,@harcos_id)";
                command.Parameters.AddWithValue("@nev", nev);
                
                command.Parameters.AddWithValue("@leiras", leiras);
                command.Parameters.AddWithValue("@harcos_id", harcos_id);

               
               

                int erintettSorok = command.ExecuteNonQuery();
                MessageBox.Show("Felvéve!!!!!!!!");
                displayData2();
                textBox2.Clear();
                textBox3.Clear();
                conn.Close();
                
            }





        }
       
        public void displayData2()
        {
            using (var conn = new MySqlConnection("Server = localhost; Database = cs_harcosok; Uid = root; Pwd = ;"))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "select nev from harcosok";

                try
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    listBox1.Items.Clear();
                    while (reader.Read())
                    {

                        listBox1.Items.Add(reader["nev"]);
                    }
                    con.Close();

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                conn.Close();
            }
        }

        

        private void displayData3(object sender, EventArgs e)
        {
            using (var conn = new MySqlConnection("Server = localhost; Database = cs_harcosok; Uid = root; Pwd = ;"))
            {
                conn.Open();
                int selectedIndex = listBox1.SelectedIndex;
                Object selectedItem = listBox1.SelectedItem;
                int id = selectedIndex + 1;
                var command = conn.CreateCommand();
                command.CommandText = "SELECT `nev` from kepessegek WHERE harcos_id=@id";
                command.Parameters.AddWithValue("@id", id);
                try
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    listBox2.Items.Clear();
                    
                    while (reader.Read())
                    {
                        
                        listBox2.Items.Add(reader["nev"]);
                    }
                    con.Close();

                }
                catch (Exception b)
                {
                    MessageBox.Show(b.Message);
                }
                conn.Close();
               
            }
        }

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            using (var conn = new MySqlConnection("Server = localhost; Database = cs_harcosok; Uid = root; Pwd = ;"))
            {
                conn.Open();
                
                int selectedIndex = listBox2.SelectedIndex;
                Object selectedItem = listBox2.SelectedItem;
                int id2 = selectedIndex + 1;
                var command = conn.CreateCommand();
                command.CommandText = "SELECT `leiras` from kepessegek WHERE id=@id2";
                command.Parameters.AddWithValue("@id2", id2);
                try
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    
                    listBox3.Items.Clear();
                    while (reader.Read())
                    {
                        listBox3.Items.Add(reader["leiras"]);

                    }
                    con.Close();

                }
                catch (Exception c)
                {
                    MessageBox.Show(c.Message);
                }
                conn.Close();

            }
        }

        private void Törlés_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {


                var kepesseg_torles = con.CreateCommand();
                kepesseg_torles.CommandText = @"DELETE FROM kepessegek WHERE id=@id;";
               
                kepesseg_torles.ExecuteNonQuery();

                listBox1.Text = "Nincs kiválasztott képesség";
                MessageBox.Show("Sikeres törlés!");
            }
            else { MessageBox.Show("Nincs kiválasztva képesség"); }
        }
    }
}
