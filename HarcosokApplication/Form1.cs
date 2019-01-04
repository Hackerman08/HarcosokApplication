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

        MySqlConnection con = new MySqlConnection("Server = localhost; Database = regisztracio; Uid = root; Pwd = ;");
        public HarcosokApplication()
        {
            InitializeComponent();
            displayData();

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

            string nev = textBox2.Text;
            string leiras = textBox3.Text;
            string harcos_id = comboBox1.Text;

            using (var conn = new MySqlConnection("Server = localhost; Database = cs_harcosok; Uid = root; Pwd = ;"))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "UPDATE felhasznalo SET`id`=@databesa,`nev`=@nev,`leiras`=@leiras,`harcos_id`=@harcos_id  WHERE id=@databesa";
                command.Parameters.AddWithValue("@nev", nev);
                command.Parameters.AddWithValue("@jelszo", leiras);
                command.Parameters.AddWithValue("@regdatum", harcos_id);
                command.Parameters.AddWithValue("@databesa", databesa);
                int erintettSorok = command.ExecuteNonQuery();
                MessageBox.Show("Módósítva!!!!");
                displayData();
                conn.Close();
                
            }





        }
    }
}
