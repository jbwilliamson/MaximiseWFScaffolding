<%@ Page Title="PersonEdit" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Edit.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vPerson.Edit" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>People - Edit</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvPerson" runat="server"
            ItemType="RandomSchoolAsync.Models.Person" DefaultMode="Edit" DataKeyNames="PersonId"
            UpdateMethod="UpdateItem" 
			SelectMethod="GetItem"
            RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Person with PersonId <%: Request.QueryString["PersonId"] %>
            </EmptyDataTemplate>
            <EditItemTemplate>
				<asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
				<fieldset id="fsPerson" class="form-horizontal">
					<legend class="small">Person</legend>
					<asp:DynamicControl Mode="Edit" DataField="FirstMidName" SetFocus="True" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="LastName" runat="server" />
				</fieldset>
				<fieldset id="fsSchool" class="form-horizontal">
					<legend class="small">School</legend>
					<asp:DynamicControl Mode="Edit" 
						DataField="SchoolId" 
						DataTypeName="RandomSchoolAsync.Models.School" 
						DataTextField="SchoolName" 
						DataValueField="SchoolId"
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

