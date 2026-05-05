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
    public partial class rezervasyon : Form
    {
        private string connectionString;
        public rezervasyon()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["HalıSahaTakipConnectionString"].ConnectionString;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }

        private void ComboLoadData()
        {
            try
            {
//                using (SqlConnection connection = new SqlConnection(connectionString)
//)
//                {
//                    string query1 = "SELECT FirstName FROM Users";
//                    SqlDataAdapter da1 = new SqlDataAdapter(query1, connection);
//                    DataTable dt1 = new DataTable();
//                    da1.Fill(dt1);

//                    combo_name.DataSource = dt1;
//                    combo_name.DisplayMember = "FirstName";
//                    combo_name.ValueMember = "FirstName";

//                }
                using (SqlConnection connection = new SqlConnection(connectionString)
)
                {
                    string query2 = "SELECT city_id, city_name FROM Table_Cities";
                    SqlDataAdapter da2 = new SqlDataAdapter(query2, connection);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);

                    combo_city.DataSource = dt2;
                    combo_city.DisplayMember = "city_name";
                    combo_city.ValueMember = "city_id";

                }

                

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query3 = "SELECT FieldName FROM Table_FootballFields";
                    SqlDataAdapter da3 = new SqlDataAdapter(query3, connection);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    combo_field.DataSource = dt3;
                    combo_field.DisplayMember = "FieldName";
                    combo_field.ValueMember = "FieldName";
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query4 = "SELECT times FROM Table_Time";
                    SqlDataAdapter da4 = new SqlDataAdapter(query4, connection);
                    DataTable dt4 = new DataTable();
                    da4.Fill(dt4);
                    combo_hour.DataSource = dt4;
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

        private void btn_save_Click(object sender, EventArgs e)
        {
            
            string c_name = txt_name.Text.ToString();
            //string c_surname = txt_surname.Text.ToString();
            //string c_tel = masked_tel.Text.ToString();
            //c_tel = c_tel.Replace("(", "").Replace(")", "").Replace("_", "");
            //c_tel = "0" + c_tel;
            string c_city = combo_city.SelectedValue.ToString();
            int c_city_id = combo_city.SelectedIndex + 1;
            string c_field = combo_field.SelectedValue.ToString();
            int c_field_id = combo_field.SelectedIndex + 1;
            string c_date_history = date_history.Value.ToString("yyyy-MM-dd");
            string c_hour = combo_hour.SelectedValue.ToString();
            int c_hour_id = combo_hour.SelectedIndex + 1;
            //string c_price = label_price.Text.ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_insert_into_Table_Reservations", connection)
)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", c_name);
                        //cmd.Parameters.AddWithValue("@LastName", c_surname);
                        //cmd.Parameters.AddWithValue("@PhoneNumber", c_tel);
                        cmd.Parameters.AddWithValue("@city_id", c_city_id);
                        cmd.Parameters.AddWithValue("@FieldId", c_field_id);
                        cmd.Parameters.AddWithValue("@ReservationDate", c_date_history);
                        cmd.Parameters.AddWithValue("@time_id", c_hour_id);
                        //cmd.Parameters.AddWithValue("@PaymentAmount", c_price);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla eklendi!");

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata = " + ex.Message);
            }

        }

        private void rezervasyon_Load(object sender, EventArgs e)
        {
            ComboLoadData();
        }

        private void label7_Click(object sender, EventArgs e)
        {

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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void combo_field_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void date_history_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void combo_hour_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            r_liste fr = new r_liste();
            fr.Show();
            this.Hide();
        }
    }
}
