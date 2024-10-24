using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG6212_POE_P2_ST10355256
{
    public partial class ManageClaims : System.Web.UI.Page
    {
        // Connection string can also be retrieved from Web.config
        //private string connectionString = "Server=localhost;Database=Claims;Integrated Security=True;";
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
                string query = "SELECT claim_id, lecturer_id, lecturer_name, lecturer_surname, program_id, module_id, hours, status, claim_date, rate_per_hour, last_mod_date, notes, month, year FROM Claims";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    ClaimsGridView.DataSource = dataTable;
                    ClaimsGridView.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle exception (logging, user notification, etc.)
                    Console.WriteLine(ex.Message);
                }
            }
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
            foreach (GridViewRow row in ClaimsGridView.Rows)
            {
                RadioButton rb = (RadioButton)row.FindControl("SelectClaimRadio");
                if (rb != null && rb.Checked)
                {
                    int claimId = Convert.ToInt32(rb.Attributes["data-claim-id"]); // Retrieve claim_id from custom attribute
                    UpdateClaimStatus(claimId, "Approved");
                    break; // Exit the loop after finding the selected row
                }
            }
            // Rebind data to refresh the GridView after approval
            BindTableData();
        }

        protected void RejectButton_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in ClaimsGridView.Rows)
            {
                RadioButton rb = (RadioButton)row.FindControl("SelectClaimRadio");
                if (rb != null && rb.Checked)
                {
                    int claimId = Convert.ToInt32(rb.Attributes["data-claim-id"]); // Retrieve claim_id from custom attribute
                    UpdateClaimStatus(claimId, "Rejected");
                    break; // Exit the loop after finding the selected row
                }
            }
            // Rebind data to refresh the GridView after rejection
            BindTableData();
        }

        private void UpdateClaimStatus(int claimId, string status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Claims SET status = @status, last_mod_date = GETDATE() WHERE claim_id = @claimId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@status", status);
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
