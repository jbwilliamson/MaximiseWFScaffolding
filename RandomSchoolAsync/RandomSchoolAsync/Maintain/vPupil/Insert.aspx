<%@ Page Title="PupilInsert" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Insert.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vPupil.Insert" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Pupils - Insert</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvPupil" runat="server"
            ItemType="RandomSchoolAsync.Models.Pupil" DefaultMode="Insert"
            InsertItemPosition="FirstItem" 
			InsertMethod="InsertItem"
            RenderOuterTable="false">
            <InsertItemTemplate>
				<asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
				<fieldset id="fsName" class="form-horizontal">
					<legend class="small">Name</legend>
					<asp:DynamicControl Mode="Insert"
						DataField="PersonId" 
						DataTypeName="RandomSchoolAsync.Models.Person" 
						DataTextField="FirstMidName" 
						DataValueField="PersonId"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Insert" DataField="URN" runat="server" />
					<asp:DynamicControl Mode="Insert"
						DataField="ParentOne" 
						DataTypeName="RandomSchoolAsync.Models.Parent" 
						DataTextField="FirstName" 
						DataValueField="ParentId"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Insert"
						DataField="ParentTwo" 
						DataTypeName="RandomSchoolAsync.Models.Parent" 
						DataTextField="FirstName" 
						DataValueField="ParentId"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Insert" DataField="AcademicYear" runat="server" />
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
				</fieldset>
				<fieldset id="fsPersonal" class="form-horizontal">
					<legend class="small">Personal</legend>
					<asp:DynamicControl Mode="Insert" DataField="Age" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Gender" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Disability" runat="server" />
					<asp:DynamicControl Mode="Insert"
						DataField="NationId" 
						DataTypeName="RandomSchoolAsync.Models.Nation" 
						DataTextField="Country" 
						DataValueField="Id"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Insert" DataField="DateOfBirth" runat="server" />
				</fieldset>
				<fieldset id="fsContact" class="form-horizontal">
					<legend class="small">Contact</legend>
					<asp:DynamicControl Mode="Insert" DataField="EmailAddress" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Twitter" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Instagram" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="FacebookPage" runat="server" />
				</fieldset>
				<fieldset id="fsAdditional" class="form-horizontal">
					<legend class="small">Additional</legend>
					<asp:DynamicControl Mode="Insert" DataField="OtherInformation" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="EnrollmentDate" runat="server" />
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
