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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            
        }
        SqlConnection baglan = new SqlConnection("Server=CAN\\CAN; Initial Catalog = UzakMasaustuu; Integrated Security = True");
        Form1 f1 = new Form1();
        
        private void button1_Click(object sender, EventArgs e)
        {
          

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand komut = new SqlCommand();
            string Komut = "DELETE FROM person WHERE ID = '" + textBox1.Text + "'";
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Tüm Alanları Doldurunuz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (MessageBox.Show(textBox1.Text + "'nolu Silmek istediğinizden emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    baglan.Open();
                    SqlCommand komutsatiri = new SqlCommand(Komut, baglan);
                    komutsatiri.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show(textBox1.Text + "'Nolu Kayıt Silindi!\nLütfen yenileme tuşuna basın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt Silme Başarısız!" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
