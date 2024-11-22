using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG6212_POE_P2_ST10355256
{
    public partial class _Default : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ClaimsDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindClaimsData(); // Populate the GridView on initial load
            BindDropdowns(); // Populate dropdown lists
        }

        private void BindClaimsData()
        {
            // Get the entered lecturer_id from the TextBox
            string lecturerId = TextBox1.Text.Trim(); // Update this to your TextBox ID

            string query = "SELECT * FROM Claims WHERE lecturer_id = @lecturerId"; // Filter by lecturer_id
            string sumQuery = "SELECT SUM(total_amount) FROM Claims WHERE lecturer_id = @lecturerId"; // Sum query for total_amount

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for lecturer_id
                    command.Parameters.AddWithValue("@lecturerId", lecturerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    ClaimsGridView.DataSource = dataTable;
                    ClaimsGridView.DataBind();
                }

                // Calculate the sum of total_amount for the entered lecturer_id
                using (SqlCommand sumCommand = new SqlCommand(sumQuery, connection))
                {
                    sumCommand.Parameters.AddWithValue("@lecturerId", lecturerId);
                    connection.Open();

                    object result = sumCommand.ExecuteScalar(); // Fetch the sum value

                    if (result != DBNull.Value && result != null)
                    {
                        decimal totalAmount = Convert.ToDecimal(result);
                        TotalAmountLabel.Text = $"Total Amount: R{totalAmount.ToString("N2", CultureInfo.CreateSpecificCulture("en-ZA"))}"; // Display total amount in currency format
                    }
                    else
                    {
                        TotalAmountLabel.Text = "Total Amount: N/A"; // Handle cases where there is no data
                    }

                    connection.Close();
                }
            }
        }

        // TextChanged event handler for TextBox
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            BindClaimsData(); // Call BindClaimsData when the TextBox value changes
        }
        private void BindDropdowns()
        {
            // Populate Month dropdown
            DropDownListMonth.Items.Clear();
            DropDownListMonth.Items.Add(new ListItem("January", "January"));
            DropDownListMonth.Items.Add(new ListItem("February", "February"));
            DropDownListMonth.Items.Add(new ListItem("March", "March"));
            DropDownListMonth.Items.Add(new ListItem("April", "April"));
            DropDownListMonth.Items.Add(new ListItem("May", "May"));
            DropDownListMonth.Items.Add(new ListItem("June", "June"));
            DropDownListMonth.Items.Add(new ListItem("July", "July"));
            DropDownListMonth.Items.Add(new ListItem("August", "August"));
            DropDownListMonth.Items.Add(new ListItem("September", "September"));
            DropDownListMonth.Items.Add(new ListItem("October", "October"));
            DropDownListMonth.Items.Add(new ListItem("November", "November"));
            DropDownListMonth.Items.Add(new ListItem("December", "December"));

            // Populate Year dropdown (example: from 2000 to the current year)
            DropDownListYear.Items.Clear();
            for (int year = 2000; year <= DateTime.Now.Year; year++)
            {
                DropDownListYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
        }
            protected void Button1_Click(object sender, EventArgs e)
        {
            // Collect data from form fields
            string lecturerNumber = TextBox1.Text;
            //string month = TextBox4.Text;
            string month = DropDownListMonth.SelectedValue; // Use selected value from dropdown
            string year = DropDownListYear.SelectedValue;   // Use selected value from dropdown
            string programCode = TextBox6.Text;
            string module = TextBox7.Text;
            //string ratePerHour = TextBox8.Text;

            // Check required fields (excluding support document and notes)
            if (string.IsNullOrWhiteSpace(lecturerNumber) || lecturerNumber.Length != 5)
            {
                SuccessMessageLabel.Text = "Error: Lecture ID incorrect, please enter a valid 5-character ID";
                SuccessMessageLabel.ForeColor = System.Drawing.Color.Red;
                SuccessMessageLabel.Visible = true;
                return; // Exit the method if required fields are empty
            }

            if (
                //string.IsNullOrWhiteSpace(month) ||
                //string.IsNullOrWhiteSpace(TextBox5.Text) || // Year
                
                string.IsNullOrWhiteSpace(programCode) ||
                string.IsNullOrWhiteSpace(module) ||
                string.IsNullOrWhiteSpace(TextBox8.Text) ||
                string.IsNullOrWhiteSpace(TextBox9.Text)) // Hours
            {
                SuccessMessageLabel.Text = "Error: All fields except Notes are required to be filled out.";
                SuccessMessageLabel.ForeColor = System.Drawing.Color.Red;
                SuccessMessageLabel.Visible = true;
                return; // Exit the method if required fields are empty
            }

            // Parse year and hours as integers
            //int year, hours;
            int hours;
            double ratePerHour;
            
            // Validate the Year input
            if (!double.TryParse(TextBox8.Text, out ratePerHour))
            {
                SuccessMessageLabel.Text = "Error: ratePerHour must be an a valid number.";
                SuccessMessageLabel.ForeColor = System.Drawing.Color.Red;
                SuccessMessageLabel.Visible = true;
                return; // Exit the method if the year is not valid
            }

            // Validate the Hours input
            if (!int.TryParse(TextBox9.Text, out hours))
            {
                SuccessMessageLabel.Text = "Error: Hours must be an integer.";
                SuccessMessageLabel.ForeColor = System.Drawing.Color.Red;
                SuccessMessageLabel.Visible = true;
                return; // Exit the method if hours is not valid
            }

            

            // Validate Program Code and Module Code length
            if (programCode.Length > 5)
            {
                SuccessMessageLabel.Text = "Error: Program Code must not exceed 5 characters.";
                SuccessMessageLabel.ForeColor = System.Drawing.Color.Red;
                SuccessMessageLabel.Visible = true;
                return; // Exit the method if Program Code is too long
            }

            if (module.Length > 5)
            {
                SuccessMessageLabel.Text = "Error: Module Code must not exceed 5 characters.";
                SuccessMessageLabel.ForeColor = System.Drawing.Color.Red;
                SuccessMessageLabel.Visible = true;
                return; // Exit the method if Module Code is too long
            }

            if (month.Length > 9)
            {
                SuccessMessageLabel.Text = "Error: Please enter a valid month. Expected 'January' - ' December'.";
                SuccessMessageLabel.ForeColor = System.Drawing.Color.Red;
                SuccessMessageLabel.Visible = true;
                return; // Exit the method if Module Code is too long
            }

            if (FileUpload1.HasFile)
            {
                // Validate the file type
                string fileExtension = Path.GetExtension(FileUpload1.FileName).ToLower();

                // Corrected logic for validating allowed file types
                if (fileExtension != ".pdf" && fileExtension != ".docx" && fileExtension != ".xlsx")
                {
                    SuccessMessageLabel.Text = "Error: Only PDF, DOCX, and XLSX files are allowed.";
                    SuccessMessageLabel.ForeColor = System.Drawing.Color.Red;
                    SuccessMessageLabel.Visible = true;
                    return;
                }

                // Specify the upload path
                string uploadPath = Server.MapPath("~/Uploads/");
                string fileName = Path.GetFileName(FileUpload1.FileName);
                string fullPath = Path.Combine(uploadPath, fileName);

                // Save the file
                try
                {
                    FileUpload1.SaveAs(fullPath);
                    // Store the file name in the database
                    string supportDocument = fileName; // Store the file name or full path in the database

                    // Insert the claim into the database
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string insertQuery = "INSERT INTO Claims (lecturer_id, month, year, program_code, module_code, rate_per_hour, hours, support_document, notes) VALUES (@lecturerNumber, @lecturerName, @lecturerSurname, @month, @year, @programCode, @module, @ratePerHour, @hours, @supportDocument, @notes)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@lecturerNumber", lecturerNumber);
                            command.Parameters.AddWithValue("@month", month);
                            command.Parameters.AddWithValue("@year", year);
                            command.Parameters.AddWithValue("@programCode", programCode);
                            command.Parameters.AddWithValue("@module", module);
                            command.Parameters.AddWithValue("@ratePerHour", ratePerHour);
                            command.Parameters.AddWithValue("@hours", hours);
                            command.Parameters.AddWithValue("@supportDocument", supportDocument);
                            command.Parameters.AddWithValue("@notes", TextBox11.Text);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    SuccessMessageLabel.Text = "Claim submitted successfully!";
                    SuccessMessageLabel.ForeColor = System.Drawing.Color.Green;
                    SuccessMessageLabel.Visible = true;

                    // Optionally, rebind data to refresh the GridView
                    BindClaimsData();
                }
                catch (Exception ex)
                {
                    SuccessMessageLabel.Text = "Error: " + ex.Message;
                    SuccessMessageLabel.ForeColor = System.Drawing.Color.Red;
                    SuccessMessageLabel.Visible = true;
                }
            }
            else
            {
                SuccessMessageLabel.Text = "Error: Please select a file to upload.";
                SuccessMessageLabel.ForeColor = System.Drawing.Color.Red;
                SuccessMessageLabel.Visible = true;
            }

        }

        // Method to clear the form fields
        private void ClearForm()
        {
            //TextBox1.Text = ""; //Lecture id
            //TextBox2.Text = ""; //Lecture name
            //TextBox3.Text = ""; //Lecture surname
            //TextBox4.Text = "";
            //TextBox5.Text = ""; // Year
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = ""; // Hours
            //TextBox10.Text = "";
            TextBox11.Text = "";
        }

        public bool ValidateInsertData(string lecturerId, string month, int year, string programCode, string moduleCode, decimal ratePerHour, int hours, string supportDocument, string notes)
        {
            // Add validation logic here.
            // Example validation: Check if required fields are empty or invalid.
            if (string.IsNullOrEmpty(lecturerId))
            {
                return false; // Validation failed.
            }

            if (year < 2000 || year > DateTime.Now.Year)
            {
                return false; // Invalid year.
            }

            // Additional validations as required...
            return true; // Validation passed.
        }

        public bool InsertClaim(string lecturerId, string month, int year, string programCode, string moduleCode, decimal ratePerHour, int hours, string supportDocument, string notes)
        {
            // First, validate the data before attempting to insert.
            if (!ValidateInsertData(lecturerId, month, year, programCode, moduleCode, ratePerHour, hours, supportDocument, notes))
            {
                return false; // Data is invalid; don't insert.
            }

            // Data is valid; proceed with the insert.
            string insertQuery = "INSERT INTO Claims (lecturer_id, month, year, program_code, module_code, rate_per_hour, hours, support_document, notes) " +
                                 "VALUES (@lecturerId, @month, @year, @programCode, @moduleCode, @ratePerHour, @hours, @supportDocument, @notes)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@lecturerId", lecturerId);
                    command.Parameters.AddWithValue("@month", month);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@programCode", programCode);
                    command.Parameters.AddWithValue("@moduleCode", moduleCode);
                    command.Parameters.AddWithValue("@ratePerHour", ratePerHour);
                    command.Parameters.AddWithValue("@hours", hours);
                    command.Parameters.AddWithValue("@supportDocument", supportDocument);
                    command.Parameters.AddWithValue("@notes", notes);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0; // Return true if the insert was successful.
                }
            }

        }
        public bool UpdateClaimStatus(int claimId, string status)
        {
            string updateQuery = "UPDATE Claims SET status = @status WHERE claim_id = @claimId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@claimId", claimId);
                    command.Parameters.AddWithValue("@status", status);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if the update was successful.
                }
            }
        }

    }
}