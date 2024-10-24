using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic; // Add this for List<string>
using System.Globalization;
using System.Linq;

namespace PROG6212_POE_P2_ST10355256
{
    public partial class ClaimStatusTracker : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindClaimsData(); // Populate the GridView on initial load
                PopulateFilters(); // Populate all filters
            }
            // Subscribe to RowDataBound event
            ClaimsGridView.RowDataBound += ClaimsGridView_RowDataBound;
        }
        private void ClaimsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Make sure it's a data row, not a header or footer
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Format the rate_per_hour column (assuming it's in the correct index)
                if (decimal.TryParse(e.Row.Cells[5].Text, out decimal ratePerHour))
                {
                    e.Row.Cells[5].Text = "R" + ratePerHour.ToString("N2", CultureInfo.CreateSpecificCulture("en-ZA"));
                }

                // Format the total_amount column (assuming it's in the correct index)
                if (decimal.TryParse(e.Row.Cells[6].Text, out decimal totalAmount))
                {
                    e.Row.Cells[6].Text = "R" + totalAmount.ToString("N2", CultureInfo.CreateSpecificCulture("en-ZA"));
                }
            }
        }
        private void PopulateFilters()
        {
            // Populate Lecture IDs
            PopulateDropDown(LectureFilter, "SELECT DISTINCT(lecturer_id) FROM Claims");

            // Populate Program IDs
            PopulateDropDown(ProgramFilter, "SELECT DISTINCT(program_id) FROM Claims");

            // Populate Module IDs
            PopulateDropDown(ModuleFilter, "SELECT DISTINCT(module_id) FROM Claims");

            // Populate Manager IDs
            PopulateDropDown(ManagerFilter, "SELECT DISTINCT(manager_id) FROM Claims");
        }

        private void PopulateDropDown(DropDownList dropDownList, string query)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ClaimsDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dropDownList.DataSource = dataTable;
                    dropDownList.DataTextField = dataTable.Columns[0].ColumnName; // Name column
                    dropDownList.DataValueField = dataTable.Columns[0].ColumnName; // ID column
                    dropDownList.DataBind();
                }
            }

            // Add a default item at the top
            dropDownList.Items.Insert(0, new ListItem("Select", ""));
        }

        private void BindClaimsData(string lectureId = null, string programId = null, string moduleId = null, string managerId = null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ClaimsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Claims WHERE 1=1"; // Base query for displaying claims

            // Apply filters based on user selection
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(lectureId))
            {
                query += " AND lecturer_id = @lectureId";
                parameters.Add(new SqlParameter("@lectureId", lectureId));
            }

            if (!string.IsNullOrEmpty(programId))
            {
                query += " AND program_id = @programId";
                parameters.Add(new SqlParameter("@programId", programId));
            }

            if (!string.IsNullOrEmpty(moduleId))
            {
                query += " AND module_id = @moduleId";
                parameters.Add(new SqlParameter("@moduleId", moduleId));
            }

            if (!string.IsNullOrEmpty(managerId))
            {
                query += " AND manager_id = @managerId";
                parameters.Add(new SqlParameter("@managerId", managerId));
            }

            // Collect status values based on checkboxes
            var statusList = new List<string>();
            if (StatusApproved.Checked) statusList.Add("Approved");
            if (StatusPending.Checked) statusList.Add("Pending");
            if (StatusRejected.Checked) statusList.Add("Rejected");

            if (statusList.Count > 0)
            {
                // Use parameters for status filtering
                query += " AND status IN (" + string.Join(",", statusList.Select((s, index) => $"@status{index}")) + ")";
                for (int i = 0; i < statusList.Count; i++)
                {
                    parameters.Add(new SqlParameter($"@status{i}", statusList[i]));
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray()); // Add all parameters at once

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the GridView to the filtered claims data
                    ClaimsGridView.DataSource = dataTable;
                    ClaimsGridView.DataBind();

                    // Calculate and display the sum of the total_amount for the filtered rows
                    decimal totalAmount = 0;

                    // Sum the total_amount from the filtered data
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["total_amount"] != DBNull.Value) // Ensure the value is not null
                        {
                            totalAmount += Convert.ToDecimal(row["total_amount"]);
                        }
                    }

                    // Display the total amount in the label
                    TotalAmountLabel.Text = $"Total Amount: R{totalAmount.ToString("N2", CultureInfo.CreateSpecificCulture("en-ZA"))}";
                }
            }
        }




        protected void FilterButton_Click(object sender, EventArgs e)
        {
            string selectedLectureId = LectureFilter.SelectedValue;
            string selectedProgramId = ProgramFilter.SelectedValue;
            string selectedModuleId = ModuleFilter.SelectedValue;
            string selectedManagerId = ManagerFilter.SelectedValue;

            BindClaimsData(selectedLectureId, selectedProgramId, selectedModuleId, selectedManagerId); // Rebind data with filters
        }
    }
}