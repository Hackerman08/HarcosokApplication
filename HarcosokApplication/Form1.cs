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
            displayData3();
            displayData4();
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
                displayData3();
                conn.Close();
                
            }





        }
       
        public void displayData2()
        {
            con.Open();
            adpt = new MySqlDataAdapter("select nev,letrehozas from harcosok", con);
            dt = new DataTable();
            adpt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        
        public void displayData3()
        {
            con.Open();
            adpt = new MySqlDataAdapter("select nev from kepessegek", con);
            dt = new DataTable();
            adpt.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }
        public void displayData4()
        {
            con.Open();
            adpt = new MySqlDataAdapter("select leiras from kepessegek", con);
            dt = new DataTable();
            adpt.Fill(dt);
            dataGridView3.DataSource = dt;
            con.Close();
        }
       
        private void Törlés_Click(object sender, EventArgs e)
        {
            using (var conn = new MySqlConnection("Server = localhost; Database = cs_harcosok; Uid = root; Pwd = ;"))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "DELETE FROM `kepessegek` WHERE id=@databesa";

                command.Parameters.AddWithValue("@databesa", databesa);
                int erintettSorok = command.ExecuteNonQuery();
                MessageBox.Show("Törölve!!!!");
                displayData3();
                conn.Close();
                

            }
        }
      
        
    }
}
