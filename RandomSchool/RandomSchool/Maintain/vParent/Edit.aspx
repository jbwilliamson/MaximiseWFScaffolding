<%@ Page Title="ParentEdit" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Edit.aspx.cs" Inherits="RandomSchool.Maintain.vParent.Edit" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Parents - Edit</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvParent" runat="server"
            ItemType="RandomSchool.Models.Parent" DefaultMode="Edit" DataKeyNames="ParentId"
            UpdateMethod="UpdateItem" 
			SelectMethod="GetItem"
            RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Parent with ParentId <%: Request.QueryString["ParentId"] %>
            </EmptyDataTemplate>
            <EditItemTemplate>
				<asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
				<fieldset id="fsIdentity" class="form-horizontal">
					<legend class="small">Identity</legend>
					<asp:DynamicControl Mode="Edit" DataField="FirstName" SetFocus="True" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="Surname" runat="server" />
				</fieldset>
				<fieldset id="fsContact" class="form-horizontal">
					<legend class="small">Contact</legend>
					<asp:DynamicControl Mode="Edit" DataField="EmailAddress" runat="server" />
				</fieldset>
				<fieldset id="fsAddress" class="form-horizontal">
					<legend class="small">Address</legend>
					<asp:DynamicControl Mode="Edit" DataField="HomeAddress1" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="HomeAddress2" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="Town" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="County" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="Country" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="Postcode" runat="server" />
				</fieldset>
				<fieldset id="fsPhone" class="form-horizontal">
					<legend class="small">Phone</legend>
					<asp:DynamicControl Mode="Edit" DataField="HomePhone" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="MobilePhone" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="WorkPhone" runat="server" />
				</fieldset>
				<fieldset id="fsPersonal" class="form-horizontal">
					<legend class="small">Personal</legend>
					<asp:DynamicControl Mode="Edit" DataField="DOB" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="Gender" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="Status" runat="server" />
				</fieldset>
				<fieldset id="fsJob" class="form-horizontal">
					<legend class="small">Job</legend>
					<asp:DynamicControl Mode="Edit" DataField="JobDescription" runat="server" />
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

