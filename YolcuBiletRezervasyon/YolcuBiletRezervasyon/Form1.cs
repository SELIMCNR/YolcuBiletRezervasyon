using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace YolcuBiletRezervasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Data Source=DESKTOP-BC3LOP2\SQLEXPRESS01;Initial Catalog=DbBankaTest;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-BC3LOP2\SQLEXPRESS01;Initial Catalog=testYolcuBilet;Integrated Security=True;");

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("insert into TBLYOLCUBILGI(AD,SOYAD,TELEFON,TC,CINSIYET,MAIL) values (@p1,@p2,@p3,@p4,@p5,@p6)",conn);
            komut.Parameters.AddWithValue("@p1", Txt_ad.Text);
            komut.Parameters.AddWithValue("@p2", Txt_soyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTxt_telefon.Text);
            komut.Parameters.AddWithValue("@p4", MskTxt_tc.Text);
            komut.Parameters.AddWithValue("@p5", cmb_cinsiyet.Text);
            komut.Parameters.AddWithValue("@p6", Txt_Mail.Text);
            komut.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Yolcu Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnKAPTAN_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("insert into TBL_KAPTAN (KAPTANNO,ADSOYAD,TELEFON) values (@p1,@p2,@p3)",conn);
            komut.Parameters.AddWithValue("@p1", Txt_Kaptono.Text);
            komut.Parameters.AddWithValue("@p2", Txt_AdSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTxt_telefonn.Text);
            komut.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kaptan Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btn_seferoluştur_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("insert into TBLSEFERBİLGİ(KALKIS,VARIS,TARIH,SAAT,KAPTAN,FİYAT) values(@p1,@p2,@p3,@p4,@p5,@p6)",conn);
            komut.Parameters.AddWithValue("@p1", Txt_kalkış.Text);
            komut.Parameters.AddWithValue("@p2", Txt_Varış.Text);
            komut.Parameters.AddWithValue("@p3", MskTxt_Tarih.Text);
            komut.Parameters.AddWithValue("@p4", MskTxt_Saat.Text);
           
            komut.Parameters.AddWithValue("@p5",MskTxt_kaptan.Text);
            komut.Parameters.AddWithValue("@p6",Txt_fiyat.Text);
            komut.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Sefer Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            seferListesi();
        }

        void seferListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLSEFERBİLGİ", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            seferListesi();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txt_seferno.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();  

        }

        private void btn0_Click(object sender, EventArgs e)
        {
            Txt_Kotuk.Text = "0";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Txt_Kotuk.Text = "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Txt_Kotuk.Text = "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Txt_Kotuk.Text ="3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            Txt_Kotuk.Text = "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            Txt_Kotuk.Text = "5";
          
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            Txt_Kotuk.Text = "6";

        }

        private void btn7_Click(object sender, EventArgs e)
        {
            Txt_Kotuk.Text = "7";

        }

        private void btn8_Click(object sender, EventArgs e)
        {
            Txt_Kotuk.Text = "8";

        }

        private void btn9_Click(object sender, EventArgs e)
        {
            Txt_Kotuk.Text = "9";

        }

        private void Btn_rezervasyon_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut2 = new SqlCommand("select TC from TBLYOLCUBILGI WHERE TC=" + msktc.Text, conn);
         
            SqlDataReader dr = komut2.ExecuteReader();

            if (dr.Read())
            {
                if(msktc.Text.Length ==11 && msktc.Text != "")
                {
                    dr.Close();
                    SqlCommand komut = new SqlCommand("insert into TBLSEFERDETAY (SEFERNO,YOLCUTC,KOLTUK) values (@p1,@p2,@p3) ",conn);
                    komut.Parameters.AddWithValue("@p1", Txt_seferNo1.Text);
                    komut.Parameters.AddWithValue("@p2", msktc.Text);
                    komut.Parameters.AddWithValue("@p3",Txt_Kotuk.Text);    
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Koltuk rezervasyonu yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    seferListesi();
                    if (Txt_Kotuk.Text == btn0.Text)
                    {
                        btn0.Enabled = false;
                        btn0.BackColor = Color.Blue;
                    }

                    if (Txt_Kotuk.Text == btn1.Text)
                    {
                        btn1.Enabled = false;
                        btn1.BackColor = Color.Blue;
                    }
                    if (Txt_Kotuk.Text == btn2.Text)
                    {
                        btn2.Enabled = false;
                        btn2.BackColor = Color.Blue;
                    }
                    if (Txt_Kotuk.Text == btn3.Text)
                    {
                        btn3.Enabled = false;
                        btn3.BackColor = Color.Blue;
                    }
                    if (Txt_Kotuk.Text == btn4.Text)
                    {
                        btn4.Enabled = false;
                        btn4.BackColor = Color.Blue;
                    }
                    if (Txt_Kotuk.Text == btn5.Text)
                    {
                        btn5.Enabled = false;
                        btn5.BackColor = Color.Blue;
                    }
                    if (Txt_Kotuk.Text == btn6.Text)
                    {
                        btn6.Enabled = false;
                        btn6.BackColor = Color.Blue;
                    }
                    if (Txt_Kotuk.Text == btn7.Text)
                    {
                        btn7.Enabled = false;
                        btn7.BackColor = Color.Blue;
                    }

                    if (Txt_Kotuk.Text == btn8.Text)
                    {
                        btn8.Enabled = false;
                        btn8.BackColor = Color.Blue;
                    }
                    if (Txt_Kotuk.Text == btn9.Text)
                    {
                        btn9.Enabled = false;
                        btn9.BackColor = Color.Blue;
                    }
                }
                else
                {
                    MessageBox.Show("T.C. No Eksik Lütfen 11 Hane Giriniz !", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else
            {
                MessageBox.Show("Yolcu Listesinde T.C. No Yoktur.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            conn.Close();
        }
    }
}
