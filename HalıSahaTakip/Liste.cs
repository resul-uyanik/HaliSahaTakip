using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace HalıSahaTakip
{
    public partial class Liste : Form
    {
        private string connectionString;
        public Liste()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["HalıSahaTakipConnectionString"].ConnectionString;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HALI_SAHA fr = new HALI_SAHA();
            fr.Show();
            this.Hide();
        }

        private void Liste_Load(object sender, EventArgs e)
        {

        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_select_Table_Customer", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        connection.Open();
                        da.Fill(dt);
                        dataGridView2.DataSource = dt; 
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }

        }

        private void btn_starting_update_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                int c_id = int.Parse(selectedRow.Cells[0].Value.ToString());
                string c_name = selectedRow.Cells[1].Value.ToString();
                string c_surname = selectedRow.Cells[2].Value.ToString();
                string c_tck = selectedRow.Cells[3].Value.ToString();
                string c_tel = selectedRow.Cells[4].Value.ToString();

                if (c_tel.StartsWith("0"))
                {
                    c_tel = c_tel.Remove(0, 1);
                    c_tel = c_tel.Replace("(", "").Replace(")", "").Replace("_", "").Replace(" ", "");
                }
                try
                {
                    txt_name.Text = c_name;
                    txt_surname.Text = c_surname;
                    txt_tck.Text = c_tck;
                    masked_tel.Text = c_tel;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Aktarma işlemi esnasında hata oluştu: " + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Lütfen, güncellemek için bir kayıt seçiniz");
            }

        }

        private void btn_update_Click(object sender, EventArgs e)
        {

            int selected_ID = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);

            string c_name = txt_name.Text;
            string c_surname = txt_surname.Text;
            string c_tck = txt_tck.Text;
            string c_tel = masked_tel.Text.ToString();
            
            c_tel = c_tel.Replace("(", "").Replace(")", "").Replace("_", "").Replace(" ", "");
            if (!c_tel.StartsWith("0"))
            {
                c_tel = "0" + c_tel;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_update_Table_Customer", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", selected_ID);
                        cmd.Parameters.AddWithValue("@FirstName", c_name);
                        cmd.Parameters.AddWithValue("@LastName", c_surname);
                        cmd.Parameters.AddWithValue("@TurkishIdentityNumber", c_tck);
                        cmd.Parameters.AddWithValue("@PhoneNumber", c_tel);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txt_name.Clear();
                        txt_surname.Clear();
                        txt_tck.Clear();
                        masked_tel.Clear();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme işlemi esnasında hata oluştu: " + ex.Message);
            }

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            if (dataGridView2.SelectedRows.Count > 0)
            {

                int selected_id = int.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());

                DialogResult result = MessageBox.Show("Bu kaydı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connnection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_delete_Table_Customer", connnection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@UserId", selected_id);

                                connnection.Open();
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Silme işlemi gerçekleştirildi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Silme işlemi başarısız. Hata = " + ex.Message);
                    }
                }

            }

            else
            {
                MessageBox.Show("Hata, Değer Seçili Değil!");
            }

        }
    }
}
