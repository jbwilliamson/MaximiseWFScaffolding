<%@ Page Title="DepartmentEdit" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Edit.aspx.cs" Inherits="RandomSchool.Maintain.vDepartment.Edit" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Departments - Edit</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvDepartment" runat="server"
            ItemType="RandomSchool.Models.Department" DefaultMode="Edit" DataKeyNames="DepartmentId"
            UpdateMethod="UpdateItem" 
			SelectMethod="GetItem"
            RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Department with DepartmentId <%: Request.QueryString["DepartmentId"] %>
            </EmptyDataTemplate>
            <EditItemTemplate>
				<asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
				<fieldset id="fsName" class="form-horizontal">
					<legend class="small">Name</legend>
					<asp:DynamicControl Mode="Edit" DataField="Name" SetFocus="True" runat="server" />
				</fieldset>
				<fieldset id="fsDetails" class="form-horizontal">
					<legend class="small">Details</legend>
					<asp:DynamicControl Mode="Edit" DataField="Budget" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="StartDate" runat="server" />
					<asp:DynamicControl Mode="Edit" 
						DataField="InstructorId" 
						DataTypeName="RandomSchool.Models.Teacher" 
						DataTextField="HomeAddress1" 
						DataValueField="TeacherId"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
				</fieldset>
				<fieldset id="fsFormSubmit" class="form-horizontal">
					<div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
							<asp:Button runat="server" ID="UpdateButton" CommandName="Update" Text="Update" CssClass="btn btn-primary" />
							<asp:Button runat="server" ID="CancelButton" CommandName="Cancel" Text="Cancel" formnovalidate="formnovalidate" CausesValidation="false" CssClass="btn btn-default cancel" />
						</div>
					</div>
                </fieldset>
            </EditItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>

