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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            
        }

        SqlConnection baglan = new SqlConnection("Server=CAN\\CAN; Initial Catalog = UzakMasaustuu; Integrated Security = True");
        Form1 f1 = new Form1();

        private void button3_Click(object sender, EventArgs e)
        {
            if (baglan.State == ConnectionState.Closed)
                baglan.Open();
            SqlParameter pr = new SqlParameter("@yetkili", textBox4.Text);
            SqlParameter pr2 = new SqlParameter("@sifre", textBox5.Text);
            SqlCommand sorgula = new SqlCommand("select * from Admin where Yetkili = @yetkili and sifre = @sifre", baglan);
            sorgula.Parameters.Add(pr);
            sorgula.Parameters.Add(pr2);
            SqlDataReader oku = sorgula.ExecuteReader();

            if (oku.Read())
            {
                Form8 f8 = new Form8();
                f8.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Kullanıcı adı yada şifre hatalı!");
            oku.Close();
        }
    }
}
