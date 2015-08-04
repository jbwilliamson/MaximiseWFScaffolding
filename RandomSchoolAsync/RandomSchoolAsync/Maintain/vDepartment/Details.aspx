<%@ Page Title="Department Details" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Details.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vDepartment.Details" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Departments - Details</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvDepartment" runat="server"
            ItemType="RandomSchoolAsync.Models.Department" DataKeyNames="DepartmentId"
            SelectMethod="GetItem"
            RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Department with DepartmentId <%: Request.QueryString["DepartmentId"] %>
            </EmptyDataTemplate>
            <ItemTemplate>
				<fieldset id="fsName" class="form-horizontal">
					<legend class="small">Name</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Department Name :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Name" ID="Name" Mode="ReadOnly" />
						</div>
					</div>
				</fieldset>
				<fieldset id="fsDetails" class="form-horizontal">
					<legend class="small">Details</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Department Budget :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Budget" ID="Budget" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Start Date :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="StartDate" ID="StartDate" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Administrator :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Administrator != null ? Item.Administrator.HomeAddress1 : "Not Set" %>
						</div>
					</div>
					<div class="row">
						&nbsp;
					</div>
				</fieldset>
				<fieldset id="fsFormSubmit" class="form-horizontal">
					<div class="form-group">
						<div class="col-sm-offset-2 col-sm-10">
							<asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Back" CssClass="btn btn-default" />
						</div>
					</div>
                </fieldset>
            </ItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>

