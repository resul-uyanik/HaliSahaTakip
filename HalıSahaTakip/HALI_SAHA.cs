using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Runtime.InteropServices;


namespace HalıSahaTakip
{
    public partial class HALI_SAHA : Form
    {
        private string connectionString;
        public HALI_SAHA()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["HalıSahaTakipConnectionString"].ConnectionString;
            
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,     // X koordinatı
            int nTopRect,      // Y koordinatı
            int nRightRect,    // Genişlik
            int nBottomRect,   // Yükseklik
            int nWidthEllipse, // Köşe kavis genişliği (Örn: 40)
            int nHeightEllipse // Köşe kavis yüksekliği (Örn: 40)
        );


        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTarihSaat.Text = DateTime.Now.ToString();
        }

        private void HALI_SAHA_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("1-halısaha.jpg");

            // Diğer yuvarlatma kodlarının altına ekle
            pnl_saat.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pnl_saat.Width, pnl_saat.Height, 20, 20));

            // Buton isimlerinin btnRezervasyon ve btnKayitOl olduğunu varsayıyorum
            button2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 25, 25));
            btn_save.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn_save.Width, btn_save.Height, 25, 25));

            // anaPanel: Tasarım ekranındaki panelinizin (Name) özelliğidir.
            // 40, 40 değerleri köşelerin ne kadar yuvarlak olacağını belirler.
            panel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 40, 40));

            // panel1'in arka planını tamamen şeffaf yapıp kontrolü forma bırakır
            panel1.BackColor = Color.Transparent;


        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string c_name = txt_name.Text.ToString();
            string c_surname = txt_surname.Text.ToString();
            string c_tck = txt_tck.Text.ToString();
            string c_tel = masked_tel.Text.ToString();
            c_tel = c_tel.Replace("(", "").Replace(")", "").Replace("_", "");
            c_tel = "0" + c_tel;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_insert_into_Table_Customer", connection)
)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", c_name);
                        cmd.Parameters.AddWithValue("@LastName", c_surname);
                        cmd.Parameters.AddWithValue("@TurkishIdentityNumber", c_tck);
                        cmd.Parameters.AddWithValue("@PhoneNumber", c_tel);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla eklendi!");
                    }
                }
                Liste fr = new Liste();
                fr.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Liste fr = new Liste();
            fr.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txt_surname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txt_tck_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void masked_tel_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            rezervasyon fr = new rezervasyon();
            fr.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
