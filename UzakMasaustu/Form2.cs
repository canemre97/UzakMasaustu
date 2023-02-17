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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            
        }
        Form1 f1 = new Form1();
        SqlConnection baglan = new SqlConnection("Server=CAN\\CAN; Initial Catalog = UzakMasaustuu; Integrated Security = True");

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Tüm alanları doldurunuz!");
                }
                else
                {
                    baglan.Open();
                    string ekle = "insert into Person(AdSoyad,PcAdi,KullaniciAdi,Sifre) values (@adsoyad, @pcadi, @kullaniciadi,@sifre);";
                    SqlCommand komut = new SqlCommand(ekle, baglan);
                    komut.Parameters.AddWithValue("@adsoyad", textBox1.Text);
                    komut.Parameters.AddWithValue("@pcadi", textBox2.Text);
                    komut.Parameters.AddWithValue("@kullaniciadi", textBox3.Text);
                    komut.Parameters.AddWithValue("@sifre", textBox4.Text);
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    //güncelle
                    string sec = "SELECT * from Person";
                    SqlCommand komut2 = new SqlCommand(sec, baglan);
                    SqlDataAdapter da = new SqlDataAdapter(komut2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    f1.dataGridView1.DataSource = dt;
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı.");
                    this.Hide();
                }
           
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA\nKayıt oluşturulamadı."+ex);
            }
            

        }
    }
}
