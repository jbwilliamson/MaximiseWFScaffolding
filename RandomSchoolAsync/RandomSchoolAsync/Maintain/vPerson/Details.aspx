<%@ Page Title="Person Details" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Details.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vPerson.Details" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>People - Details</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvPerson" runat="server"
            ItemType="RandomSchoolAsync.Models.Person" DataKeyNames="PersonId"
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
							<%#: Item.School != null ? Item.School.SchoolName : "Not Set" %>
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

