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


namespace HalıSahaTakip
{
    public partial class baglan : Form
    {
        private string connectionString;

        public baglan()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["HalıSahaTakipConnectionString"].ConnectionString;
        }

        private void btn_connection_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        MessageBox.Show("1 saniye sonra yönlendiriliceksiniz", "Bağlantı AÇIK",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                Thread.Sleep(1000);
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
               // this.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata = " + ex);

            }
        }
    }
}
