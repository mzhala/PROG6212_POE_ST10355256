using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG6212_POE_P2_ST10355256
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindClaimsData(); // Populate the GridView on initial load
        }

        private void BindClaimsData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ClaimsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM claims";  // Adjust query if necessary

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    ClaimsGridView.DataSource = dataTable;
                    ClaimsGridView.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Collect data from form fields
            string lecturerNumber = TextBox1.Text;
            string lecturerName = TextBox2.Text;
            string lecturerSurname = TextBox3.Text;
            string month = TextBox4.Text;
            string year = TextBox5.Text;
            string programCode = TextBox6.Text;
            string module = TextBox7.Text;
            string ratePerHour = TextBox8.Text;
            string hours = TextBox9.Text;
            string supportDocument = TextBox10.Text; // Assuming you want to use this
            string notes = TextBox11.Text;

            // Use connection string from web.config
            string connectionString = ConfigurationManager.ConnectionStrings["ClaimsDBConnectionString"].ConnectionString;

            // SQL Insert query
            string query = "INSERT INTO claims (Lecturer_id, Lecturer_Name, Lecturer_Surname, Month, Year, Program_id, Module_id, rate_per_hour, Hours, Support_Document, Notes) " +
                           "VALUES (@LecturerNumber, @LecturerName, @LecturerSurname, @Month, @Year, @ProgramCode, @Module, @RatePerHour, @Hours, @SupportDocument, @Notes)";

            // Use a SqlConnection to connect to the database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@LecturerNumber", lecturerNumber);
                    cmd.Parameters.AddWithValue("@LecturerName", lecturerName);
                    cmd.Parameters.AddWithValue("@LecturerSurname", lecturerSurname);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@ProgramCode", programCode);
                    cmd.Parameters.AddWithValue("@Module", module);
                    cmd.Parameters.AddWithValue("@RatePerHour", ratePerHour);
                    cmd.Parameters.AddWithValue("@Hours", hours);
                    cmd.Parameters.AddWithValue("@SupportDocument", supportDocument); // Added back the support document
                    cmd.Parameters.AddWithValue("@Notes", notes);

                    try
                    {
                        // Open connection and execute insert command
                        con.Open();
                        cmd.ExecuteNonQuery();

                        // Show success message
                        SuccessMessageLabel.Text = "Claim submitted successfully.";
                        SuccessMessageLabel.Visible = true;

                        // Clear the form fields
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        // Handle the error (e.g., log it or display a message)
                        SuccessMessageLabel.Text = "Error: " + ex.Message;
                        SuccessMessageLabel.ForeColor = System.Drawing.Color.Red; // Change text color to red for error
                        SuccessMessageLabel.Visible = true;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        // Method to clear the form fields
        private void ClearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
        }


    }
}