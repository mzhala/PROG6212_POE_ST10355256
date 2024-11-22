using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace PROG6212_POE_P2_ST10355256
{
    public partial class HRView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateFilters(); // Populate dropdown filters on initial load
                UpdateReportTitle();
            }
        }

        private void PopulateFilters()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ClaimsDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Populate Claim ID Dropdown
                PopulateDropdown(connection, "SELECT DISTINCT claim_id FROM Claims WHERE status = 'approved'", ClaimIdFilter, "claim_id");

                // Populate Program Code Dropdown
                PopulateDropdown(connection, "SELECT DISTINCT program_code FROM Claims WHERE status = 'approved'", ProgramCodeFilter, "program_code");

                // Populate Module Code Dropdown
                PopulateDropdown(connection, "SELECT DISTINCT module_code FROM Claims WHERE status = 'approved'", ModuleCodeFilter, "module_code");

                // Populate Lecturer ID Dropdown
                PopulateDropdown(connection, "SELECT DISTINCT lecturer_id FROM Claims WHERE status = 'approved'", LecturerIdFilter, "lecturer_id");

                // Populate Month Dropdown
                MonthFilter.Items.Clear();
                MonthFilter.Items.Add(new ListItem("All", "")); // Add default option

                // Loop through months and add them with full names
                for (int i = 1; i <= 12; i++)
                {
                    string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i); // Get month name
                    MonthFilter.Items.Add(new ListItem(monthName, monthName)); // Add to dropdown
                }


                // Populate Year Dropdown
                PopulateDropdown(connection, "SELECT DISTINCT year FROM Claims WHERE status = 'approved'", YearFilter, "year");
            }
        }

        private void PopulateDropdown(SqlConnection connection, string query, DropDownList dropdown, string columnName)
        {
            dropdown.Items.Clear();
            dropdown.Items.Add(new ListItem("All", ""));

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dropdown.Items.Add(new ListItem(reader[columnName].ToString(), reader[columnName].ToString()));
                    }
                }
            }
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ClaimsDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetClaimReport", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters for filters
                    command.Parameters.AddWithValue("@ClaimId", string.IsNullOrEmpty(ClaimIdFilter.SelectedValue) ? (object)DBNull.Value : ClaimIdFilter.SelectedValue);
                    command.Parameters.AddWithValue("@ProgramCode", string.IsNullOrEmpty(ProgramCodeFilter.SelectedValue) ? (object)DBNull.Value : ProgramCodeFilter.SelectedValue);
                    command.Parameters.AddWithValue("@ModuleCode", string.IsNullOrEmpty(ModuleCodeFilter.SelectedValue) ? (object)DBNull.Value : ModuleCodeFilter.SelectedValue);
                    command.Parameters.AddWithValue("@LecturerId", string.IsNullOrEmpty(LecturerIdFilter.SelectedValue) ? (object)DBNull.Value : LecturerIdFilter.SelectedValue);
                    command.Parameters.AddWithValue("@Month", string.IsNullOrEmpty(MonthFilter.SelectedValue) ? (object)DBNull.Value : MonthFilter.SelectedValue);
                    command.Parameters.AddWithValue("@Year", string.IsNullOrEmpty(YearFilter.SelectedValue) ? (object)DBNull.Value : YearFilter.SelectedValue);

                    // Execute and fill the GridView
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        // Populate GridView with the first result set
                        ClaimsGridView.DataSource = ds.Tables[0];
                        ClaimsGridView.DataBind();

                        // Update total amount label with the second result set
                        if (ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0]["TotalAmount"] != DBNull.Value)
                        {
                            TotalAmountLabel.Text = "Total Amount: R" + Convert.ToDecimal(ds.Tables[1].Rows[0]["TotalAmount"]).ToString("N2");
                        }
                        else
                        {
                            TotalAmountLabel.Text = "Total Amount: R0.00";
                        }
                    }
                }
            }
            UpdateReportTitle();
        }

        private void UpdateReportTitle()
        {
            string lecturerId = LecturerIdFilter.SelectedValue;
            string programCode = ProgramCodeFilter.SelectedValue;
            string moduleCode = ModuleCodeFilter.SelectedValue;
            string month = MonthFilter.SelectedValue;
            string year = YearFilter.SelectedValue;
            string claimid  = ClaimIdFilter.SelectedValue;

            string title = "Contract Monthly Claim Report for ";

            if (!string.IsNullOrEmpty(lecturerId) || !string.IsNullOrEmpty(programCode) ||
                !string.IsNullOrEmpty(moduleCode) || !string.IsNullOrEmpty(month) || !string.IsNullOrEmpty(year))
            {
                title += "Approved Claims";

                if (!string.IsNullOrEmpty(claimid))
                {
                    title += $" for Claim {claimid}";
                }

                if (!string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
                {
                    title += $" for {month} {year}";
                }
                else if(!string.IsNullOrEmpty(month))
                {
                    title += $" for {month}";
                }
                else if (!string.IsNullOrEmpty(year))
                {
                    title += $" for {year}";
                }

                if (!string.IsNullOrEmpty(lecturerId))
                {
                    title += $" for Lecturer {lecturerId}";
                }

                if (!string.IsNullOrEmpty(programCode))
                {
                    title += $", Program {programCode}";
                }

                if (!string.IsNullOrEmpty(moduleCode))
                {
                    title += $", Module {moduleCode}";
                }
            }
            else
            {
                title += "all Approved Claims";
            }

            // Set the generated title to the label
            ReportTitleLabel.Text = title;
        }

    }
}
