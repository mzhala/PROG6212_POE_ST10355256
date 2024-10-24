using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG6212_POE_P2_ST10355256
{
    public partial class ClaimStatusTracker : Page
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
    }
}