<%@ Page Title="Manage Claims" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageClaims.aspx.cs" Inherits="PROG6212_POE_P2_ST10355256.ManageClaims" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title">Approve/Reject Claims</h2>
        <section class="row" aria-labelledby="aspnetTitle">
            <div class="details">
                <asp:Label ID="Label1" runat="server" Text="Manager Number:" Width="150px"></asp:Label>
                <asp:TextBox ID="ManagerNumberTextBox" runat="server" Width="200px"></asp:TextBox>
            </div>
        </section>

        <asp:Label ID="ErrorMessageLabel" runat="server" ForeColor="Red" />

        <asp:GridView ID="ClaimsGridView" runat="server" AutoGenerateColumns="False" 
            OnRowDataBound="ClaimsGridView_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:RadioButton ID="SelectClaimRadio" 
                                         runat="server" 
                                         GroupName="ClaimSelection" 
                                         AutoPostBack="false" 
                                         OnClientClick='toggleSelectedClaimId(this, <%# Eval("claim_id") %>)' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="claim_id" HeaderText="Claim ID" />
                <asp:BoundField DataField="lecturer_id" HeaderText="Lecturer ID" />
                <asp:BoundField DataField="lecturer_name" HeaderText="Lecturer Name" />
                <asp:BoundField DataField="lecturer_surname" HeaderText="Lecturer Surname" />
                <asp:BoundField DataField="manager_id" HeaderText="Manager ID" />
                <asp:BoundField DataField="program_id" HeaderText="Program Code" />
                <asp:BoundField DataField="module_id" HeaderText="Module" />
                <asp:BoundField DataField="hours" HeaderText="Hours" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                <asp:BoundField DataField="claim_date" HeaderText="Claim Date" DataFormatString="{0:MM/dd/yyyy}" />
                <asp:BoundField DataField="rate_per_hour" HeaderText="Rate per Hour" DataFormatString="{0:C2}" />
                <asp:BoundField DataField="last_mod_date" HeaderText="Last Modified Date" DataFormatString="{0:MM/dd/yyyy}" />
                <asp:BoundField DataField="notes" HeaderText="Notes" />
                <asp:BoundField DataField="total_amount" HeaderText="Total Amount" DataFormatString="{0:C2}" />
                <asp:BoundField DataField="month" HeaderText="Month" />
                <asp:BoundField DataField="year" HeaderText="Year" />
                <asp:BoundField DataField="support_document" HeaderText="Support Document" />
            </Columns>
        </asp:GridView>

        <asp:HiddenField ID="HiddenFieldSelectedClaimId" runat="server" />

        <asp:Button ID="ApproveButton" runat="server" Text="Approve" OnClick="ApproveButton_Click" CssClass="btn btn-success" />
        <asp:Button ID="RejectButton" runat="server" Text="Reject" OnClick="RejectButton_Click" CssClass="btn btn-danger" />
    </main>

    <script type="text/javascript">
        function toggleSelectedClaimId(radioButton, claimId) {
            var hiddenField = document.getElementById('<%= HiddenFieldSelectedClaimId.ClientID %>');
            // If the radio button is checked, set the claim ID; if unchecked, clear it
            if (radioButton.checked) {
                hiddenField.value = claimId;
                console.log("Claim ID set to: " + claimId); // Debugging
            } else {
                hiddenField.value = ""; // Clear if the same radio button is unselected
            }
        }

    </script>
</asp:Content>
