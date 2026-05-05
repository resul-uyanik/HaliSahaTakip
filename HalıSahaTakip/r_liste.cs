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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace HalıSahaTakip
{
    public partial class r_liste : Form
    {
        private string connectionString;
        public r_liste()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["HalıSahaTakipConnectionString"].ConnectionString;

        }

        private int GetAnimalIdByName(string c_name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT UserId FROM Users WHERE FirstName = @FirstName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = c_name;

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            throw new Exception("Kayıt bulunamadı!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                return -1;
            }
        }

        private void ComboLoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString)
)
                {
                    string query1 = "SELECT city_id, city_name FROM Table_Cities";
                    SqlDataAdapter da1 = new SqlDataAdapter(query1, connection);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    combo_city.DataSource = dt1;
                    combo_city.DisplayMember = "city_name";
                    combo_city.ValueMember = "city_id";

                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query2 = "SELECT FieldName FROM Table_FootballFields";
                    SqlDataAdapter da2 = new SqlDataAdapter(query2, connection);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    combo_field.DataSource = dt2;
                    combo_field.DisplayMember = "FieldName";
                    combo_field.ValueMember = "FieldName";
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query3 = "SELECT times FROM Table_Time";
                    SqlDataAdapter da3 = new SqlDataAdapter(query3, connection);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    combo_hour.DataSource = dt3;
                    combo_hour.DisplayMember = "times";
                    combo_hour.ValueMember = "times";
                }


            }

            catch (Exception ex)
            {

                MessageBox.Show("Hata = " + ex);

            }
        }

        private void FillFieldsByCity(int cityId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Sadece seçilen city_id'ye ait sahaları getiriyoruz
                string query = "SELECT FieldName FROM Table_FootballFields WHERE city_id = @p1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@p1", cityId);

                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                combo_field.DataSource = dt;
                combo_field.DisplayMember = "FieldName";
                combo_field.ValueMember = "FieldName";
            }
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_select_Table_Reservations", connection))
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

        private void r_liste_Load(object sender, EventArgs e)
        {
            ComboLoadData();
            combo_city.SelectedIndex = -1;
            combo_field.SelectedIndex = -1;
            combo_hour.SelectedIndex = -1;


        }

        private void btn_starting_update_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                int c_id = int.Parse(selectedRow.Cells[0].Value.ToString());
                string c_name = selectedRow.Cells[1].Value.ToString();
                string c_surname = selectedRow.Cells[2].Value.ToString();
                string c_tel = selectedRow.Cells[3].Value.ToString();
                string c_city = selectedRow.Cells[4].Value.ToString();
                string c_field = selectedRow.Cells[5].Value.ToString();
                string c_date = selectedRow.Cells[6].Value.ToString();
                string c_hour = selectedRow.Cells[7].Value.ToString();


                if (c_tel.StartsWith("0"))
                {
                    c_tel = c_tel.Remove(0, 1);
                    c_tel = c_tel.Replace("(", "").Replace(")", "").Replace("_", "").Replace(" ", "");
                }
                try
                {
                    txt_name.Text = c_name;
                    
                    combo_city.SelectedIndex = combo_city.FindStringExact(c_city);
                    combo_field.SelectedIndex = combo_field.FindStringExact(c_field);
                    date_history.Text = c_date;
                    combo_hour.SelectedIndex = combo_hour.FindStringExact(c_hour);


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
                                using (SqlCommand cmd = new SqlCommand("sp_delete_Table_Reservations", connnection))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ReservationId", selected_id);

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

        private void btn_update_Click(object sender, EventArgs e)
        {

            int selected_ID = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
            string c_name = txt_name.Text;
            int UserId = GetAnimalIdByName(c_name);

            string selectedValue_city = combo_city.SelectedItem.ToString();
            int selectedIndex_city = combo_city.SelectedIndex + 1;
            string selectedValue_field = combo_field.SelectedItem.ToString();
            int selectedIndex_field = combo_field.SelectedIndex + 1;
            string c_history = date_history.Value.ToString("yyyy-MM-dd");
            string selectedValue_time = combo_hour.SelectedItem.ToString();
            int selectedIndex_time = combo_hour.SelectedIndex + 1;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_update_Table_Reservations", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ReservationId", selected_ID);
                        //cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@FieldId", selectedIndex_field);
                        cmd.Parameters.AddWithValue("@ReservationDate", c_history);
                        cmd.Parameters.AddWithValue("@time_id", selectedIndex_time);
                        cmd.Parameters.AddWithValue("@city_id", selectedIndex_city);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txt_name.Clear();
                        //textBox1.Clear();
                        //masked_tel.Clear();
                        combo_city.SelectedIndex = -1;
                        combo_field.SelectedIndex = -1;
                        date_history.Checked = false;
                        combo_hour.SelectedIndex = -1;



                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme işlemi esnasında hata oluştu: " + ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            rezervasyon fr = new rezervasyon();
            fr.Show();
            this.Hide();
        }

        

        private void combo_city_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox tam olarak yüklendiğinde ve bir değer seçildiğinde çalışır
            if (combo_city.SelectedValue != null && combo_city.SelectedValue is int)
            {
                int selectedCityId = (int)combo_city.SelectedValue;
                FillFieldsByCity(selectedCityId);
            }
        }
        
    }
}
