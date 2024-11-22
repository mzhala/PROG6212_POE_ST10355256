using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web; // Add this line


namespace PROG6212_POE_P2_ST10355256
{
    public partial class HRView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateFilters(); // Populate dropdown filters on initial load
                //UpdateReportTitle();
                // Hide the Download button initially when the page loads
                DownloadPdfButton.Visible = false;
                LoadLecturers();
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
                MonthFilter.Items.Add(new System.Web.UI.WebControls.ListItem("All", "")); // Add default option

                // Loop through months and add them with full names
                for (int i = 1; i <= 12; i++)
                {
                    string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i); // Get month name
                    MonthFilter.Items.Add(new System.Web.UI.WebControls.ListItem(monthName, monthName)); // Add to dropdown
                }

                // Populate Year Dropdown
                PopulateDropdown(connection, "SELECT DISTINCT year FROM Claims WHERE status = 'approved'", YearFilter, "year");
            }
        }

        private void PopulateDropdown(SqlConnection connection, string query, DropDownList dropdown, string columnName)
        {
            dropdown.Items.Clear();
            dropdown.Items.Add(new System.Web.UI.WebControls.ListItem("All", ""));

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dropdown.Items.Add(new System.Web.UI.WebControls.ListItem(reader[columnName].ToString(), reader[columnName].ToString()));
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

                       // Check if there are any rows in the GridView and show or hide the button
                        if (ClaimsGridView.Rows.Count > 0)
                        {
                            DownloadPdfButton.Visible = true;  // Show the button if data is present
                        }
                        else
                        {
                            DownloadPdfButton.Visible = false; // Hide the button if no data
                        }

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
            string claimid = ClaimIdFilter.SelectedValue;

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
                else if (!string.IsNullOrEmpty(month))
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

        protected void DownloadPdfButton_Click(object sender, EventArgs e)
        {
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            MemoryStream stream = new MemoryStream();
            PdfWriter.GetInstance(pdfDoc, stream);
            pdfDoc.Open();

            // Add the report title
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Paragraph title = new Paragraph(ReportTitleLabel.Text, titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(title);
            pdfDoc.Add(new Paragraph("\n"));

            // Add the GridView data
            PdfPTable table = new PdfPTable(ClaimsGridView.HeaderRow.Cells.Count);
            table.WidthPercentage = 100;

            // Add table headers
            foreach (TableCell headerCell in ClaimsGridView.HeaderRow.Cells)
            {
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, headerFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.LIGHT_GRAY
                };
                table.AddCell(pdfCell);
            }

            // Add table rows
            foreach (GridViewRow gridViewRow in ClaimsGridView.Rows)
            {
                foreach (TableCell tableCell in gridViewRow.Cells)
                {
                    Font cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text, cellFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(pdfCell);
                }
            }

            pdfDoc.Add(table);
            pdfDoc.Add(new Paragraph("\n"));

            // Add the total amount
            Font totalFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Paragraph totalAmount = new Paragraph(TotalAmountLabel.Text, totalFont);
            totalAmount.Alignment = Element.ALIGN_RIGHT;
            pdfDoc.Add(totalAmount);

            pdfDoc.Close();

            // Download the PDF
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=HRReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }

        private void BindGridView()
        {
            // fetch data from a database or other data source
            string connectionString = ConfigurationManager.ConnectionStrings["ClaimsDBConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Lecturer_Details", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Bind data to GridView
                LecturerGridView.DataSource = dt;
                LecturerGridView.DataBind();
            }
        }

        private void LoadLecturers()
        {
            // SQL query to fetch lecturer details
            string query = "SELECT lecturer_id, name, surname, contactNumber, email FROM Lecturer_Details";
            string connectionString = ConfigurationManager.ConnectionStrings["ClaimsDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                LecturerGridView.DataSource = dt;
                LecturerGridView.DataBind();
            }
        }

        protected void LecturerGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LecturerGridView.EditIndex = e.NewEditIndex;
            LoadLecturers(); // Rebind to show editing controls
        }

        protected void LecturerGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get the row being edited
            GridViewRow row = LecturerGridView.Rows[e.RowIndex];

            // Extract the updated values from the TextBoxes
            string lecturerId = ((Label)row.FindControl("lecturer_id")).Text;
            string name = ((TextBox)row.FindControl("name")).Text;
            string surname = ((TextBox)row.FindControl("surname")).Text;
            string contactNumber = ((TextBox)row.FindControl("contactNumber")).Text;
            string email = ((TextBox)row.FindControl("email")).Text;

            // Update the database
            string updateQuery = "UPDATE Lecturer_Details SET name = @name, surname = @surname, contactNumber = @contactNumber, email = @email WHERE lecturer_id = @lecturerId";
            string connectionString = ConfigurationManager.ConnectionStrings["ClaimsDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@lecturerId", lecturerId);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.Parameters.AddWithValue("@contactNumber", contactNumber);
                command.Parameters.AddWithValue("@email", email);

                connection.Open();
                command.ExecuteNonQuery();
            }

            // Exit edit mode and rebind data
            LecturerGridView.EditIndex = -1;
            LoadLecturers(); // Rebind data to refresh the grid
        }


        protected void LecturerGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LecturerGridView.EditIndex = -1; // Exit editing mode
            LoadLecturers(); // Rebind data
        }

        protected void LecturerGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Check if the row is a data row (not header/footer)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Retrieve the lecturer's name from the second cell (assuming index 1)
                string lecturerName = e.Row.Cells[1].Text;

                // If the lecturer's name is "John Doe", change the background color of the row
                if (lecturerName == "John Doe")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }

    }
}
