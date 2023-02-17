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
using System.Data.Sql;

namespace UzakMasaustu
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            
        }

        SqlConnection baglan = new SqlConnection("Server=CAN\\CAN; Initial Catalog = UzakMasaustuu; Integrated Security = True");
        Form1 f1 = new Form1();
        

        private void button2_Click(object sender, EventArgs e)
        {
            
            baglan.Open();
            string ekle = "insert into Admin(yetkili,Sifre) values (@yetkili,@sifre);";
            SqlCommand komut = new SqlCommand(ekle, baglan);
            komut.Parameters.AddWithValue("@yetkili", textBox1.Text);
            komut.Parameters.AddWithValue("@sifre", textBox2.Text);
            komut.ExecuteNonQuery();
            komut.Dispose();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            string ekle = "insert into Admin(Yetkili,Sifre) values (@yetkili,@sifre);";
            SqlCommand komut = new SqlCommand(ekle, baglan);
            komut.Parameters.AddWithValue("@yetkili", textBox4.Text);
            komut.Parameters.AddWithValue("@sifre", textBox5.Text);
            komut.ExecuteNonQuery();
            komut.Dispose();
            
            string sec = "SELECT * from Person";
            SqlCommand komut2 = new SqlCommand(sec, baglan);
            SqlDataAdapter da = new SqlDataAdapter(komut2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            f1.dataGridView1.DataSource = dt;
            baglan.Close();
            MessageBox.Show("Kayıt Başarılı.");
        }
    }
}
