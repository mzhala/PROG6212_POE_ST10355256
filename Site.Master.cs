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
      
        }

        protected string GetActiveClass(string pageName)
        {
            // Check the current page's file name
            string currentPage = System.IO.Path.GetFileNameWithoutExtension(Request.Url.AbsolutePath);

            // Compare it with the page name you want
            if (currentPage.Equals(pageName, StringComparison.OrdinalIgnoreCase))
            {
                return "active"; // Add the active class to the link
            }

            return ""; // No active class if the page name doesn't match
        }

    }
}