<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageClaims.aspx.cs" Inherits="PROG6212_POE_P2_ST10355256.ManageClaims" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title">Approve/Reject Claims</h2>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Approve/Reject Claims</h1>
            <div class="details">
                <asp:Label ID="Label1" runat="server" Text="Manager Number:" Width="150px"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width="200px"></asp:TextBox>
            </div>
        </section>

        <!-- Table with selectable rows using GridView -->
        <asp:GridView ID="ClaimsGridView" runat="server" AutoGenerateColumns="False" 
            OnRowDataBound="ClaimsGridView_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:RadioButton ID="SelectClaimRadio" runat="server" GroupName="ClaimSelection"
                            CommandArgument='<%# Eval("claim_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="lecturer_id" HeaderText="Lecturer ID" />
                <asp:BoundField DataField="lecturer_name" HeaderText="Lecturer Name" />
                <asp:BoundField DataField="month" HeaderText="Month" />
                <asp:BoundField DataField="year" HeaderText="Year" />
                <asp:BoundField DataField="program_id" HeaderText="Program Code" />
                <asp:BoundField DataField="module_id" HeaderText="Module" />
                <asp:BoundField DataField="status" HeaderText="Status" />
            </Columns>
        </asp:GridView>

        <!-- Approve and Reject Buttons -->
        <asp:Button ID="ApproveButton" runat="server" Text="Approve" OnClick="ApproveButton_Click" CssClass="btn btn-success" />
        <asp:Button ID="RejectButton" runat="server" Text="Reject" OnClick="RejectButton_Click" CssClass="btn btn-danger" />
    </main>

    <script type="text/javascript">
        function toggleRadioButton(radioButton) {
            // Check if the radio button is already selected
            if (radioButton.checked) {
                // Deselect the radio button
                radioButton.checked = false;
            } else {
                // Select the radio button
                radioButton.checked = true;
            }

            // Prevent the default behavior of the radio button
            return false; // Prevent the click event from doing anything else
        }
    </script>
</asp:Content>
