<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PROG6212_POE_P2_ST10355256._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Lecture Claims Submissions</h1>
            <div class="details">
                <asp:Label ID="Label1" runat="server" Text="Lecturer Number:" Width="150px"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width="200px"></asp:TextBox>
            </div>
            <div class="details">
                <asp:Label ID="Label4" runat="server" Text="Lecturer Name:" Width="150px"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Width="200px"></asp:TextBox>
            </div>
            <div class="details">
                <asp:Label ID="Label5" runat="server" Text="Lecturer Surname:" Width="150px"></asp:Label>
                <asp:TextBox ID="TextBox3" runat="server" Width="200px"></asp:TextBox>
            </div>
        </section>

        <div class="row">
            <section class="col-md-2">
                 <p>Month</p>
                 <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </section>
            <section class="col-md-2">
                <p>Year</p>
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            </section>
            <section class="col-md-2">
                <p>Program Code</p>
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            </section>
            <section class="col-md-2">
                 <p>Module</p>
                 <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
            </section>
            <section class="col-md-2">
                <p>Rate/hr</p>
                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
            </section>
            <section class="col-md-2">
                <p>Hours</p>
                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
            </section>
            <section class="col-md-2">
                 <p>Support Document</p>
                 <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
            </section>
            <section class="col-md-2">
                <p>Notes</p>
                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
            </section>
            <section class="col-md-2">
                <p>Click to Submit</p>
                <asp:Button ID="Button1" runat="server" Text="Submit Claim" OnClick="Button1_Click" />
            </section>
            <section>
                <asp:Label ID="SuccessMessageLabel" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
            </section>
        </div>

        <!-- GridView to display the claims data -->
        <section class="row">
            <h2>Submitted Claims</h2>
            <asp:GridView ID="ClaimsGridView" runat="server" AutoGenerateColumns="true" 
                CssClass="table table-bordered" EmptyDataText="No claims found.">
            </asp:GridView>
        </section>

    </main>
    
</asp:Content>
