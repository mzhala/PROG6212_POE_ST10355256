<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PROG6212_POE_P2_ST10355256._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Lecture Claims Submissions</h1>
            <div class="details">
                <asp:Label ID="Label1" runat="server" Text="Lecturer Number:" Width="150px"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" Width="200px" />
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
                 <asp:DropDownList ID="DropDownListMonth" runat="server" style="width:150px"></asp:DropDownList>
            </section>
            <section class="col-md-2">
                <p>Year</p>
                <asp:DropDownList ID="DropDownListYear" runat="server" style="width:150px"></asp:DropDownList>
            </section>
            <section class="col-md-2">
                <p>Program Code</p>
                <asp:TextBox ID="TextBox6" runat="server" style="width:150px"></asp:TextBox>
            </section>
            <section class="col-md-2">
                 <p>Module</p>
                 <asp:TextBox ID="TextBox7" runat="server" style="width:150px"></asp:TextBox>
            </section>
            <section class="col-md-2">
                <p>Rate/hr</p>
                <asp:TextBox ID="TextBox8" runat="server" style="width:150px"></asp:TextBox>
            </section>
            <section class="col-md-2">
                <p>Hours</p>
                <asp:TextBox ID="TextBox9" runat="server" style="width:150px"></asp:TextBox>
            </section>
            <section class="col-md-2">
                 <p>Support Document</p>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </section>
            <section class="col-md-2">
                <p>Notes</p>
                <asp:TextBox ID="TextBox11" runat="server" style="width:150px"></asp:TextBox>
            </section>
            <section class="col-md-2">
                <p Style="margin-left:10px">Click to Submit</p>
                <asp:Button ID="Button1" runat="server" Text="Submit Claim" OnClick="Button1_Click"  CssClass="btn btn-secondary" Style="margin-left:10px"/>
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
        <asp:Label ID="TotalAmountLabel" runat="server" Font-Bold="true" ForeColor="Green" />

    </main>
    
</asp:Content>
