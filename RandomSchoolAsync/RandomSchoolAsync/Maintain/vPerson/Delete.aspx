<%@ Page Title="PersonDelete" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Delete.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vPerson.Delete" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <h3>Are you sure want to delete this Person?</h3>
        <asp:FormView ID="fvPerson" runat="server"
            ItemType="RandomSchoolAsync.Models.Person" DataKeyNames="PersonId"
            DeleteMethod="DeleteItem" 
			SelectMethod="GetItem"
			RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Person with PersonId <%: Request.QueryString["PersonId"] %>
            </EmptyDataTemplate>
            <ItemTemplate>
				<fieldset id="fsPerson" class="form-horizontal">
					<legend class="small">Person</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>First Name :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="FirstMidName" ID="FirstMidName" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Last Name :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="LastName" ID="LastName" Mode="ReadOnly" />
						</div>
					</div>
				</fieldset>
				<fieldset id="fsSchool" class="form-horizontal">
					<legend class="small">School</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>School :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.School != null ? Item.School.SchoolName : "" %>
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

