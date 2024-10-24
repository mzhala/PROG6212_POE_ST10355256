<%@ Page Title="Manage Claims" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageClaims.aspx.cs" Inherits="PROG6212_POE_P2_ST10355256.ManageClaims" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title">Approve/Reject Claims</h2>
        <section class="row" aria-labelledby="aspnetTitle">
            <div class="details">
                <asp:Label ID="Label1" runat="server" Text="Manager Number:" Width="150px"></asp:Label>
                <asp:TextBox ID="ManagerIdTextBox" runat="server" Width="200px"></asp:TextBox>
            </div>
        </section>

        <asp:Label ID="ErrorMessageLabel" runat="server" ForeColor="Red" />
        <section class="row-container">
            <asp:Label ID="text" runat="server"> Show claims that are: </asp:Label>
            <asp:CheckBox ID="PendingCheckBox" runat="server" Text="Pending" AutoPostBack="true" OnCheckedChanged="FilterCheckBoxes_CheckedChanged" Checked="true" />
            <asp:CheckBox ID="RejectedCheckBox" runat="server" Text="Rejected" AutoPostBack="true" OnCheckedChanged="FilterCheckBoxes_CheckedChanged" />
            <asp:CheckBox ID="ApprovedCheckBox" runat="server" Text="Approved" AutoPostBack="true" OnCheckedChanged="FilterCheckBoxes_CheckedChanged" />

        </section>
        <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" Visible="false"></asp:Label>

        <asp:GridView ID="ClaimsGridView" runat="server" AutoGenerateColumns="False" 
            OnRowDataBound="ClaimsGridView_RowDataBound" CssClass="gridview-table">

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
                <asp:BoundField DataField="claim_id" HeaderText="Claim ID"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="lecturer_id" HeaderText="Lecturer ID"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="lecturer_name" HeaderText="Lecturer Name"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="lecturer_surname" HeaderText="Lecturer Surname"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="month" HeaderText="Month"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="year" HeaderText="Year"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="program_id" HeaderText="Program Code"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="module_id" HeaderText="Module"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="rate_per_hour" HeaderText="Rate per Hour" DataFormatString="R {0:N2}"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="hours" HeaderText="Hours"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="status" HeaderText="Status"  ItemStyle-CssClass="gridview-col-sm"/>               
                <asp:BoundField DataField="notes" HeaderText="Notes"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="total_amount" HeaderText="Total Amount" DataFormatString="{0:C2}"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="manager_id" HeaderText="Manager ID"  ItemStyle-CssClass="gridview-col-sm"/>
                <asp:BoundField DataField="support_document" HeaderText="Support Document"  ItemStyle-CssClass="gridview-col-sm"/>
                
            </Columns>
        </asp:GridView>

        <asp:HiddenField ID="HiddenFieldSelectedClaimId" runat="server" />
        <section class="row-container">
            <asp:Button ID="ClearSelectedButton" runat="server" Text="Clear Selected" OnClick="ClearSelectedButton_Click" CssClass="btn btn-secondary"/>
            <asp:Button ID="ApproveButton" runat="server" Text="Approve" OnClick="ApproveButton_Click" CssClass="btn btn-success" />
            <asp:Button ID="RejectButton" runat="server" Text="Reject" OnClick="RejectButton_Click" CssClass="btn btn-danger" />
        </section>
        
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
