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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        Form1 f1 = new Form1();
        SqlConnection baglan = new SqlConnection("Server=CAN\\CAN; Initial Catalog = UzakMasaustuu; Integrated Security = True");
        SqlCommand komut = new SqlCommand();
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet ds = new DataSet();
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from person", baglan);
            da.UpdateCommand = new SqlCommand("update person set adsoyad=@adsoyad,pcadi=@pcadi,kullaniciadi=@kullaniciadi,sifre=@sifre where id=@id", baglan);
            da.UpdateCommand.Parameters.Add("@adsoyad", SqlDbType.NVarChar, 50, "adsoyad");
            da.UpdateCommand.Parameters.Add("@pcadi", SqlDbType.NVarChar, 50, "pcadi");
            da.UpdateCommand.Parameters.Add("@kullaniciadi", SqlDbType.NVarChar, 50, "kullaniciadi");
            da.UpdateCommand.Parameters.Add("@sifre", SqlDbType.NVarChar, 50, "sifre");
            baglan.Close();
            DataRow rw = ds.Tables["person"].Rows[0];
            rw["adsoyad"] = "adsoyad";
            rw["pcadi"] = "pcadi";
            rw["kullaniciadi"] = "kullaniciadi";
            rw["sifre"] = "sifre0";
            da.Update(ds, "person");
            baglan.Close();
        }

        public void Form3_Load(object sender, EventArgs e)
        {
            string kayit = "SELECT * from Person";
            SqlCommand komut = new SqlCommand(kayit, baglan);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow rw = dataGridView1.Rows[index];
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            sekmesizTabCtrl1.SelectedIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();
            komut.Connection = baglan;
            komut.CommandText = "UPDATE person SET adsoyad='" + textBox1.Text + "',pcadi='" + textBox2.Text + "',kullaniciadi='" + textBox3.Text + "',sifre='" + textBox4.Text + "' WHERE ID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
            komut.ExecuteNonQuery();
            komut.Dispose();
            baglan.Close();
            this.Close();
            MessageBox.Show("Kayıt Düzenlendi.\nLütfen yenileme tuşuna basınız.");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string ara, cumle;
            ara = textBox6.Text;
            cumle = "Select * from person where adsoyad like '%" + textBox6.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(cumle, baglan);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
