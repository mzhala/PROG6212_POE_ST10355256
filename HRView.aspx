<%@ Page Title="HR View" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HRView.aspx.cs" Inherits="PROG6212_POE_P2_ST10355256.HRView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2 id="title">HR View</h2>
        <div style="height: 10px;"></div>
        <hr />
        <h4 style="background-color: #212529; color: white; padding: 10px; border-radius: 2px;">Claims Report</h4>

        <hr />
        <div>
            <!-- Filters Section -->
            <div>
                <label for="ClaimIdFilter">Claim ID:</label>
                <asp:DropDownList ID="ClaimIdFilter" runat="server">
                    <asp:ListItem Value="">Select Claim ID</asp:ListItem>
                </asp:DropDownList>

                <label for="ProgramCodeFilter">Program Code:</label>
                <asp:DropDownList ID="ProgramCodeFilter" runat="server">
                    <asp:ListItem Value="">Select Program Code</asp:ListItem>
                </asp:DropDownList>

                <label for="ModuleCodeFilter">Module Code:</label>
                <asp:DropDownList ID="ModuleCodeFilter" runat="server">
                    <asp:ListItem Value="">Select Module Code</asp:ListItem>
                </asp:DropDownList>

                <label for="LecturerIdFilter">Lecturer ID:</label>
                <asp:DropDownList ID="LecturerIdFilter" runat="server">
                    <asp:ListItem Value="">Select Lecturer ID</asp:ListItem>
                </asp:DropDownList>

                <label for="MonthFilter">Month:</label>
                <asp:DropDownList ID="MonthFilter" runat="server">
                    <asp:ListItem Value="">Select Month</asp:ListItem>
                    <asp:ListItem Value="January">January</asp:ListItem>
                    <asp:ListItem Value="February">February</asp:ListItem>
                    <asp:ListItem Value="March">March</asp:ListItem>
                    <asp:ListItem Value="April">April</asp:ListItem>
                    <asp:ListItem Value="May">May</asp:ListItem>
                    <asp:ListItem Value="June">June</asp:ListItem>
                    <asp:ListItem Value="July">July</asp:ListItem>
                    <asp:ListItem Value="August">August</asp:ListItem>
                    <asp:ListItem Value="September">September</asp:ListItem>
                    <asp:ListItem Value="October">October</asp:ListItem>
                    <asp:ListItem Value="November">November</asp:ListItem>
                    <asp:ListItem Value="December">December</asp:ListItem>
                </asp:DropDownList>

                <label for="YearFilter">Year:</label>
                <asp:DropDownList ID="YearFilter" runat="server">
                    <asp:ListItem Value="">Select Year</asp:ListItem>
                </asp:DropDownList>

                <asp:Button ID="FilterButton" runat="server" Text="Generate Report" OnClick="FilterButton_Click" CssClass="btn btn-success" />
            </div>
            <div style="height: 50px;"></div>

            <asp:Label ID="ReportTitleLabel" runat="server" class="report-title"/>

            <!-- GridView to display claims -->
            <asp:GridView ID="ClaimsGridView" runat="server" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:BoundField DataField="claim_id" HeaderText="Claim ID" SortExpression="claim_id" />
                    <asp:BoundField DataField="program_code" HeaderText="Program Code" SortExpression="program_code" />
                    <asp:BoundField DataField="module_code" HeaderText="Module Code" SortExpression="module_code" />
                    <asp:BoundField DataField="lecturer_id" HeaderText="Lecturer ID" SortExpression="lecturer_id" />
                    <asp:BoundField DataField="total_amount" HeaderText="Amount" SortExpression="amount" DataFormatString="R{0:F2}" />
                    <asp:BoundField DataField="month" HeaderText="Month" SortExpression="month" />
                    <asp:BoundField DataField="year" HeaderText="Year" SortExpression="year" />
                </Columns>
            </asp:GridView>

            <div>
              
                <asp:Label ID="TotalAmountLabel" runat="server" Text="Total Amount: R0.00" Font-Bold="True" />
            </div>
            <div>
                <asp:Button ID="DownloadPdfButton" runat="server" Text="Download Report as PDF" CssClass="btn btn-success" OnClick="DownloadPdfButton_Click" />
            </div>
            
            <div style="height: 50px;"></div>
            <hr />
            <h4 style="background-color: #212529; color: white; padding: 10px; border-radius: 2px;">Lecturer Details</h4>
            <hr />

            <asp:GridView ID="LecturerGridView" runat="server" AutoGenerateColumns="False" OnRowEditing="LecturerGridView_RowEditing" OnRowUpdating="LecturerGridView_RowUpdating" OnRowCancelingEdit="LecturerGridView_RowCancelingEdit" OnRowDataBound="LecturerGridView_RowDataBound">
                <Columns>
        
                   <asp:TemplateField HeaderText="Lecturer ID">
                        <ItemTemplate>
                            <%# Eval("lecturer_id") %> 
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lecturer_id" runat="server" Text='<%# Bind("lecturer_id") %>' />

                        </EditItemTemplate>
                        <HeaderStyle Width="300px"/>
                    </asp:TemplateField>
        
        
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <%# Eval("name") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="name" runat="server" Text='<%# Bind("name") %>' />
                        </EditItemTemplate>
                        <HeaderStyle Width="300px"/>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Surname">
                        <ItemTemplate>
                            <%# Eval("surname") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="surname" runat="server" Text='<%# Bind("surname") %>' />
                        </EditItemTemplate>
                        <HeaderStyle Width="300px"/>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Contact Number">
                        <ItemTemplate>
                            <%# Eval("contactNumber") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="contactNumber" runat="server" Text='<%# Bind("contactNumber") %>' />
                        </EditItemTemplate>
                        <HeaderStyle Width="300px"/>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <%# Eval("email") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="email" runat="server" Text='<%# Bind("email") %>' />
                        </EditItemTemplate>
                        <HeaderStyle Width="300px"/>
                    </asp:TemplateField>

        
                    <asp:CommandField ShowEditButton="True" ShowCancelButton="True" />
                </Columns>
            </asp:GridView>

        </div>
    </main>
</asp:Content>
