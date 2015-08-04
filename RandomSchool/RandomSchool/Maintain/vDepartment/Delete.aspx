<%@ Page Title="DepartmentDelete" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Delete.aspx.cs" Inherits="RandomSchool.Maintain.vDepartment.Delete" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <h3>Are you sure want to delete this Department?</h3>
        <asp:FormView ID="fvDepartment" runat="server"
            ItemType="RandomSchool.Models.Department" DataKeyNames="DepartmentId"
            DeleteMethod="DeleteItem" 
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
							<%#: Item.Administrator != null ? Item.Administrator.HomeAddress1 : "" %>
						</div>
					</div>
					<div class="row">
						&nbsp;
					</div>
				</fieldset>
				<fieldset id="fsFormSubmit" class="form-horizontal">
					<div class="form-group">
						<div class="col-sm-offset-2 col-sm-10">
							<asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger" />
							<asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-default" />
						</div>
					</div>
                </fieldset>
            </ItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>

