<%@ Page Title="Claim Status Tracker" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClaimStatusTracker.aspx.cs" Inherits="PROG6212_POE_P2_ST10355256.ClaimStatusTracker" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2 id="title">Claim Status Tracker</h2>

        <!-- GridView to display the claims data -->
        <section class="row">
            <asp:GridView ID="ClaimsGridView" runat="server" AutoGenerateColumns="true" 
                CssClass="table table-bordered" EmptyDataText="No claims found.">
            </asp:GridView>
        </section>
    </main>
</asp:Content>
