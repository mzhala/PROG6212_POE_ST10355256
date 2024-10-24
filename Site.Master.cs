using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace PROG6212_POE_P2_ST10355256
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Ensure this runs only on initial load
            {
                SetActiveNavItem();
            }
        }

        private void SetActiveNavItem()
        {
            string currentPage = Request.Url.AbsolutePath; // Get the current URL path

            // Use the IDs of your nav items
            HtmlGenericControl lecturerClaimsNav = (HtmlGenericControl)FindControl("LecturerClaimsNav");
            HtmlGenericControl manageClaimsNav = (HtmlGenericControl)FindControl("ManageClaimsNav");
            HtmlGenericControl claimStatusTrackerNav = (HtmlGenericControl)FindControl("ClaimStatusTrackerNav");

            // Remove active class from all
            lecturerClaimsNav.Attributes["class"] = "nav-item";
            manageClaimsNav.Attributes["class"] = "nav-item";
            claimStatusTrackerNav.Attributes["class"] = "nav-item";

            // Check the current page and set the active class
            if (currentPage.EndsWith("/")) // Home page
            {
                lecturerClaimsNav.Attributes["class"] += " active";
            }
            else if (currentPage.Contains("ManageClaims"))
            {
                manageClaimsNav.Attributes["class"] += " active";
            }
            else if (currentPage.Contains("ClaimStatusTracker"))
            {
                claimStatusTrackerNav.Attributes["class"] += " active";
            }
        }
    }
}