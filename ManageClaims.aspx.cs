using Microsoft.Ajax.Utilities;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG6212_POE_P2_ST10355256
{
    public partial class ManageClaims : System.Web.UI.Page
    {
        // Connection string can also be retrieved from Web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["ClaimsDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTableData(); // Bind data to the grid on initial load
            }
        }

        private void BindTableData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT *, lecturer_details.name as lecturer_name, lecturer_details.surname as lecturer_surname FROM Claims JOIN lecturer_details ON  Claims.lecturer_id = lecturer_details.lecturer_id WHERE status = 'Pending'";

                SqlCommand command = new SqlCommand(query, connection);
                /*
                 string query = "SELECT * FROM Claims WHERE 1=1 AND (";

                // Add filters based on checkbox selection
                if (PendingCheckBox.Checked)
                {
                    query += " status = 'Pending'";
                }
                if (RejectedCheckBox.Checked)
                {
                    query += " OR status = 'Rejected'";
                }
                if (ApprovedCheckBox.Checked)
                {
                    query += " OR status = 'Approved'";
                }
                query += ")";
                SqlCommand command = new SqlCommand(query, connection);
                 */
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        ClaimsGridView.DataSource = dataTable;
                        ClaimsGridView.DataBind();
                        MessageLabel.Visible = false; // Hide message if claims are found
                    }
                    else
                    {
                        ClaimsGridView.DataSource = null; // Clear the GridView if no claims are found
                        ClaimsGridView.DataBind();
                        MessageLabel.Text = "No claims found.";
                        MessageLabel.Visible = true; // Show the message
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (logging, user notification, etc.)
                    Console.WriteLine(ex.Message);
                    MessageLabel.Text = "An error occurred while fetching claims: " + ex.Message;
                    MessageLabel.Visible = true; // Show the error message
                    ClaimsGridView.DataSource = null; // Clear the GridView in case of an error
                    ClaimsGridView.DataBind();
                }
            }
        }


        protected void FilterCheckBoxes_CheckedChanged(object sender, EventArgs e)
        {
            BindTableData(); // Rebind the data when any checkbox is checked/unchecked
        }


        protected void ClaimsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButton rb = (RadioButton)e.Row.FindControl("SelectClaimRadio");
                if (rb != null)
                {
                    // Make sure you are accessing the correct index for claim_id
                    // Assuming claim_id is in the first column (index 0)
                    string claimId = DataBinder.Eval(e.Row.DataItem, "claim_id").ToString();
                    rb.Attributes["data-claim-id"] = claimId;
                }
            }
        }


        protected void ApproveButton_Click(object sender, EventArgs e)
        {
            // Check if manager_id is entered
            if (string.IsNullOrEmpty(ManagerIdTextBox.Text) || ManagerIdTextBox.Text.Length != 5) // Assuming ManagerIdTextBox is the TextBox for manager ID
            {
                ErrorMessageLabel.Text = "Error: ManagerID incorrect, please enter a 5-character ID.";
                return; // Exit the method if the field is empty
            }

            string managerId = ManagerIdTextBox.Text; // Retrieve manager_id as string
            bool anyClaimUpdated = false; // Track if any claim was updated
            foreach (GridViewRow row in ClaimsGridView.Rows)
            {
                RadioButton rb = (RadioButton)row.FindControl("SelectClaimRadio");
                if (rb != null && rb.Checked)
                {
                    int claimId = Convert.ToInt32(rb.Attributes["data-claim-id"]); // Retrieve claim_id from custom attribute
                    UpdateClaimStatus(claimId, "Approved", managerId); // Pass managerId as a string
                    anyClaimUpdated = true; // Mark that an update has occurred
                }
            }

            if (anyClaimUpdated)
            {
                // Optionally, you could display a success message here
                ErrorMessageLabel.Text = "Selected claims have been approved.";
            }
            else
            {
                ErrorMessageLabel.Text = "No claims were selected for approval.";
            }

            // Rebind data to refresh the GridView after approval
            BindTableData();
        }




        protected void RejectButton_Click(object sender, EventArgs e)
        {
            // Check if manager_id is entered
            if (string.IsNullOrEmpty(ManagerIdTextBox.Text) || ManagerIdTextBox.Text.Length != 5) // Assuming ManagerIdTextBox is the TextBox for manager ID
            {
                ErrorMessageLabel.Text = "Error: ManagerID incorrect, please enter a 5-character ID.";
                return; // Exit the method if the field is empty
            }
           
            string managerId = ManagerIdTextBox.Text; // Retrieve manager_id as string
            if (managerId.Length > 20)
            {
                ErrorMessageLabel.Text = "Error: Manager Number must not exceed 5 characters.";
                return; // Exit the method if Module Code is too long
            }
            bool anyClaimUpdated = false; // Track if any claim was updated
            foreach (GridViewRow row in ClaimsGridView.Rows)
            {
                RadioButton rb = (RadioButton)row.FindControl("SelectClaimRadio");
                if (rb != null && rb.Checked)
                {
                    int claimId = Convert.ToInt32(rb.Attributes["data-claim-id"]); // Retrieve claim_id from custom attribute
                    UpdateClaimStatus(claimId, "Rejected", managerId); // Pass managerId as a string
                    anyClaimUpdated = true; // Mark that an update has occurred
                }
            }

            if (anyClaimUpdated)
            {
                // Optionally, you could display a success message here
                ErrorMessageLabel.Text = "Selected claims have been rejected.";
            }
            else
            {
                ErrorMessageLabel.Text = "No claims were selected for rejection.";
            }

            // Rebind data to refresh the GridView after rejection
            BindTableData();
        }

        protected void AutoUpdateStatusButton_Click(object sender, EventArgs e)
        {
            // Check if manager_id is entered
            if (string.IsNullOrEmpty(ManagerIdTextBox.Text) || ManagerIdTextBox.Text.Length != 5) // Assuming ManagerIdTextBox is the TextBox for manager ID
            {
                ErrorMessageLabel.Text = "Error: ManagerID incorrect, please enter a 5-character ID.";
                return; // Exit the method if the field is empty
            }

            string managerId = ManagerIdTextBox.Text; // Retrieve manager_id as string
            bool anyClaimUpdated = false; // Track if any claim was updated
            StringBuilder resultMessages = new StringBuilder(); // To store all messages

            foreach (GridViewRow row in ClaimsGridView.Rows)
            {
                RadioButton rb = (RadioButton)row.FindControl("SelectClaimRadio");
                if (rb != null && rb.Checked)
                {
                    int claimId = Convert.ToInt32(rb.Attributes["data-claim-id"]); // Retrieve claim_id from custom attribute

                    // Call the stored procedure ApproveClaim here and retrieve the message
                    string message = ExecuteApproveClaim(claimId, managerId); // Execute the stored procedure and get the message

                    // Append the message to the resultMessages, ensuring each message appears on a new line
                    resultMessages.AppendLine(message);

                    anyClaimUpdated = true; // Mark that an update has occurred
                }
            }

            // Display the messages in ErrorMessageLabel
            if (anyClaimUpdated)
            {
                // Display all collected messages
                ErrorMessageLabel.Text = resultMessages.ToString();
            }
            else
            {
                ErrorMessageLabel.Text = "No claims were selected for approval.";
            }

            // Rebind data to refresh the GridView after approval
            BindTableData();
        }

        private string ExecuteApproveClaim(int claimId, string managerId)
        {
            // Here, you would call the stored procedure ApproveClaim from your database
            // For the sake of this example, let's assume it returns a string message

            string message = "";

            // Retrieve the necessary details from the claim (program_code, module_code, lecture_id, etc.)
            // You might need to get these values based on claimId. This is an example of retrieving those values.

            string programCode = "";  // Retrieve the actual program_code for the claim
            string moduleCode = "";   // Retrieve the actual module_code for the claim
            string lectureId = "";    // Retrieve the actual lecture_id for the claim
            decimal ratePerHour = 0;  // Retrieve the actual rate_per_hour for the claim

            // Example query to retrieve the claim details based on claimId
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT program_code, module_code, lecturer_id, rate_per_hour FROM Claims WHERE claim_id = @claimId", connection))
                {
                    command.Parameters.AddWithValue("@claimId", claimId);

                    // Execute the query and fetch the claim details
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            programCode = reader["program_code"].ToString();
                            moduleCode = reader["module_code"].ToString();
                            lectureId = reader["lecturer_id"].ToString();
                            ratePerHour = Convert.ToDecimal(reader["rate_per_hour"]);
                        }
                    }
                }
            }

            // Now, execute the stored procedure ApproveClaim with all required parameters
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("ApproveClaim", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the necessary parameters to the stored procedure
                    command.Parameters.AddWithValue("@program_code", programCode);
                    command.Parameters.AddWithValue("@module_code", moduleCode);
                    command.Parameters.AddWithValue("@lecturer_id", lectureId);
                    command.Parameters.AddWithValue("@rate_per_hour", ratePerHour);
                    command.Parameters.AddWithValue("@managerId", managerId);
                    command.Parameters.AddWithValue("@claim_id", claimId);

                    // Execute and read the output message
                    message = Convert.ToString(command.ExecuteScalar());
                }
            }

            return message;
        }




        protected void ClearSelectedButton_Click(object sender, EventArgs e)
        {
            ClearSelectedClaim();
            BindTableData(); // Rebind data to refresh the GridView
        }

        private void ClearSelectedClaim()
        {
            // Clear the hidden field value
            HiddenFieldSelectedClaimId.Value = string.Empty;

            // Deselect all radio buttons in the GridView
            foreach (GridViewRow row in ClaimsGridView.Rows)
            {
                RadioButton rb = (RadioButton)row.FindControl("SelectClaimRadio");
                if (rb != null)
                {
                    rb.Checked = false; // Deselect the radio button
                }
            }
        }

        private void UpdateClaimStatus(int claimId, string status, string managerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Claims SET status = @status, manager_id = @managerId, last_mod_date = GETDATE() WHERE claim_id = @claimId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@managerId", managerId);
                command.Parameters.AddWithValue("@claimId", claimId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle exception (logging, user notification, etc.)
                    Console.WriteLine(ex.Message);
                }
            }
        }


    }
}
