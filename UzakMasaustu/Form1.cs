using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
using MSTSCLib;

namespace UzakMasaustu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Initial Catalog=UzakMasaustuu;Data Source=CAN;Integrated Security=SSPI;");
        public void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                    baglan.Open();
                SqlParameter pr = new SqlParameter("@yetkili", textBox1.Text);
                SqlParameter pr2 = new SqlParameter("@sifre", textBox2.Text);
                SqlCommand sorgula = new SqlCommand("select * from Admin where Yetkili = @yetkili and sifre = @sifre", baglan);
                sorgula.Parameters.Add(pr);
                sorgula.Parameters.Add(pr2);
                SqlDataReader oku = sorgula.ExecuteReader();

                if (oku.Read())
                    sekmesizTabControl1.SelectedIndex = 1;//TabPage 2 ye geç.
                else
                    label3.Text = "Kullanıcı adı yada şifre hatalı!";
                oku.Close();
            }
            catch (Exception ex)
            {
                label3.Text = "Hata:\t" + ex.Message.ToString();
            }
            finally
            {
                kayitGetir();
            }
            //Beni Hatırla
            if (checkBox1.Checked)
            {
                Properties.Settings.Default["Yetkili"] = textBox1.Text;
            }
            Properties.Settings.Default.Save();
        }

        public void kayitGetir()
        {
            //Sql'deki tabloyu dataGridView de listeleme.
            string kayit = "SELECT * from Person";
            SqlCommand komut = new SqlCommand(kayit, baglan);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Listedeki kişiye çift tıklandığı zaman textboxlara verileri aktarma.
                int index = e.RowIndex;
                DataGridViewRow rw = dataGridView1.Rows[index];
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                sekmesizTabControl1.SelectedIndex = 2;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hatalı seçim!");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //Enter ile giriş.
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                axMsTscAxNotSafeForScripting1.Server = textBox3.Text;
                axMsTscAxNotSafeForScripting1.UserName = textBox4.Text;

                IMsTscNonScriptable secured = (IMsTscNonScriptable)axMsTscAxNotSafeForScripting1.GetOcx();
                secured.ClearTextPassword = textBox5.Text;
                axMsTscAxNotSafeForScripting1.Connect();
                if (axMsTscAxNotSafeForScripting1.Connected.ToString() == "1")
                    axMsTscAxNotSafeForScripting1.Disconnect();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error Connecting", "Error connecting to remote desktop " + textBox3.Text + " Error:  " + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (axMsTscAxNotSafeForScripting1.Connected.ToString() == "1")
                    axMsTscAxNotSafeForScripting1.Disconnect();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error Disconnecting", "Error disconnecting from remote desktop " + textBox3.Text + " Error:  " + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            sekmesizTabControl1.SelectedIndex = 1;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            sekmesizTabControl1.SelectedIndex = 2;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            sekmesizTabControl1.SelectedIndex = 0;
            textBox1.Clear();
            textBox2.Clear();
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            sekmesizTabControl1.SelectedIndex = 0;
            textBox1.Clear();
            textBox2.Clear();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox9_Click_1(object sender, EventArgs e)
        {
            //Yeni kayıt ekledikten sonra kaydın görünmesi için listeyi yenileme tuşu.
            baglan.Open();
            string sec = "SELECT * from Person";
            SqlCommand komut = new SqlCommand(sec, baglan);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
            MessageBox.Show("Güncellendi!!");
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            baglan.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("select * from person", baglan);
            da.Fill(ds, "person");
            baglan.Close();

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Sütun başlıkları ortalama ve sütün aralıkları düzenleme
            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //BeniHatırla
            textBox1.Text = Properties.Settings.Default["Yetkili"].ToString();
            if (textBox1.Text.Count() > 1)
                checkBox1.Checked = true;


        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default["Yetkili"] = textBox1.Text;
        }
    }
}


