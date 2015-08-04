<%@ Page Title="ParentInsert" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Insert.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vParent.Insert" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Parents - Insert</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvParent" runat="server"
            ItemType="RandomSchoolAsync.Models.Parent" DefaultMode="Insert"
            InsertItemPosition="FirstItem" 
			InsertMethod="InsertItem"
            RenderOuterTable="false">
            <InsertItemTemplate>
				<asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
				<fieldset id="fsIdentity" class="form-horizontal">
					<legend class="small">Identity</legend>
					<asp:DynamicControl Mode="Insert" DataField="FirstName" SetFocus="True" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Surname" runat="server" />
				</fieldset>
				<fieldset id="fsContact" class="form-horizontal">
					<legend class="small">Contact</legend>
					<asp:DynamicControl Mode="Insert" DataField="EmailAddress" runat="server" />
				</fieldset>
				<fieldset id="fsAddress" class="form-horizontal">
					<legend class="small">Address</legend>
					<asp:DynamicControl Mode="Insert" DataField="HomeAddress1" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="HomeAddress2" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Town" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="County" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Country" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Postcode" runat="server" />
				</fieldset>
				<fieldset id="fsPhone" class="form-horizontal">
					<legend class="small">Phone</legend>
					<asp:DynamicControl Mode="Insert" DataField="HomePhone" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="MobilePhone" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="WorkPhone" runat="server" />
				</fieldset>
				<fieldset id="fsPersonal" class="form-horizontal">
					<legend class="small">Personal</legend>
					<asp:DynamicControl Mode="Insert" DataField="DOB" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Gender" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Status" runat="server" />
				</fieldset>
				<fieldset id="fsJob" class="form-horizontal">
					<legend class="small">Job</legend>
					<asp:DynamicControl Mode="Insert" DataField="JobDescription" runat="server" />
				</fieldset>
				<fieldset id="fsFormSubmit" class="form-horizontal">
					<div class="form-group">
						<div class="col-sm-offset-2 col-sm-10">
							<asp:Button runat="server" ID="InsertButton" CommandName="Insert" Text="Insert" CssClass="btn btn-primary" />
							<asp:Button runat="server" ID="CancelButton" CommandName="Cancel" Text="Cancel" formnovalidate="formnovalidate" CausesValidation="false" CssClass="btn btn-default cancel" />
						</div>
					</div>
                </fieldset>
            </InsertItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>
