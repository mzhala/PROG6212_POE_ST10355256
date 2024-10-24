<%@ Page Title="Claim Status Tracker" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClaimStatusTracker.aspx.cs" Inherits="PROG6212_POE_P2_ST10355256.ClaimStatusTracker" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2 id="title">Claim Status Tracker</h2>
        
        <section class="claim_filters" style="margin-bottom:10px">
            
            <label for="LectureFilter">Lecture ID:</label>
            <asp:DropDownList ID="LectureFilter" runat="server">
                <asp:ListItem Text="Select Lecture" Value="" />
            </asp:DropDownList>

            <label for="ProgramFilter" style="margin-left:25px">Program ID:</label>
            <asp:DropDownList ID="ProgramFilter" runat="server">
                <asp:ListItem Text="Select Program" Value="" />
            </asp:DropDownList>

            <label for="ModuleFilter" style="margin-left:25px">Module ID:</label>
            <asp:DropDownList ID="ModuleFilter" runat="server">
                <asp:ListItem Text="Select Module" Value="" />
            </asp:DropDownList>

            <label for="ManagerFilter" style="margin-left:25px">Manager ID:</label>
            <asp:DropDownList ID="ManagerFilter" runat="server">
                <asp:ListItem Text="Select Manager" Value="" />
            </asp:DropDownList>

            <label style="margin-left:25px; margin-right:10px">Status:</label>
            <asp:CheckBox ID="StatusApproved" runat="server" Text="Approved" />
            <asp:CheckBox ID="StatusPending" runat="server" Text="Pending" />
            <asp:CheckBox ID="StatusRejected" runat="server" Text="Rejected" />

            <asp:Button ID="FilterButton" runat="server" Text="Filter" OnClick="FilterButton_Click" CssClass="btn btn-secondary" style="margin-left:25px" />
        </section>

        

        <section class="row">
            <asp:GridView ID="ClaimsGridView" runat="server" AutoGenerateColumns="true" 
                CssClass="table table-bordered" EmptyDataText="No claims found.">
            </asp:GridView>
        </section>

        <asp:Label ID="TotalAmountLabel" runat="server" CssClass="total-amount" />
    </main>
</asp:Content>
