<%@ Page Title="PersonInsert" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Insert.aspx.cs" Inherits="RandomSchool.Maintain.vPerson.Insert" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>People - Insert</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvPerson" runat="server"
            ItemType="RandomSchool.Models.Person" DefaultMode="Insert"
            InsertItemPosition="FirstItem" 
			InsertMethod="InsertItem"
            RenderOuterTable="false">
            <InsertItemTemplate>
				<asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
				<fieldset id="fsPerson" class="form-horizontal">
					<legend class="small">Person</legend>
					<asp:DynamicControl Mode="Insert" DataField="FirstMidName" SetFocus="True" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="LastName" runat="server" />
				</fieldset>
				<fieldset id="fsSchool" class="form-horizontal">
					<legend class="small">School</legend>
					<asp:DynamicControl Mode="Insert"
						DataField="SchoolId" 
						DataTypeName="RandomSchool.Models.School" 
						DataTextField="SchoolName" 
						DataValueField="SchoolId"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
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
