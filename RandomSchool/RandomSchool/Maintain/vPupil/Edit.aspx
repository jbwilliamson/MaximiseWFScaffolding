<%@ Page Title="PupilEdit" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Edit.aspx.cs" Inherits="RandomSchool.Maintain.vPupil.Edit" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Pupils - Edit</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvPupil" runat="server"
            ItemType="RandomSchool.Models.Pupil" DefaultMode="Edit" DataKeyNames="StudentId"
            UpdateMethod="UpdateItem" 
			SelectMethod="GetItem"
            RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Pupil with StudentId <%: Request.QueryString["StudentId"] %>
            </EmptyDataTemplate>
            <EditItemTemplate>
				<asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
				<fieldset id="fsName" class="form-horizontal">
					<legend class="small">Name</legend>
					<asp:DynamicControl Mode="Edit" 
						DataField="PersonId" 
						DataTypeName="RandomSchool.Models.Person" 
						DataTextField="FirstMidName" 
						DataValueField="PersonId"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Edit" DataField="URN" runat="server" />
					<asp:DynamicControl Mode="Edit" 
						DataField="ParentOne" 
						DataTypeName="RandomSchool.Models.Parent" 
						DataTextField="FirstName" 
						DataValueField="ParentId"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Edit" 
						DataField="ParentTwo" 
						DataTypeName="RandomSchool.Models.Parent" 
						DataTextField="FirstName" 
						DataValueField="ParentId"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Edit" DataField="AcademicYear" runat="server" />
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
				</fieldset>
				<fieldset id="fsPersonal" class="form-horizontal">
					<legend class="small">Personal</legend>
					<asp:DynamicControl Mode="Edit" DataField="Age" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="Gender" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="Disability" runat="server" />
					<asp:DynamicControl Mode="Edit" 
						DataField="NationId" 
						DataTypeName="RandomSchool.Models.Nation" 
						DataTextField="Country" 
						DataValueField="Id"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Edit" DataField="DateOfBirth" runat="server" />
				</fieldset>
				<fieldset id="fsContact" class="form-horizontal">
					<legend class="small">Contact</legend>
					<asp:DynamicControl Mode="Edit" DataField="EmailAddress" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="Twitter" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="Instagram" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="FacebookPage" runat="server" />
				</fieldset>
				<fieldset id="fsAdditional" class="form-horizontal">
					<legend class="small">Additional</legend>
					<asp:DynamicControl Mode="Edit" DataField="OtherInformation" runat="server" />
					<asp:DynamicControl Mode="Edit" DataField="EnrollmentDate" runat="server" />
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

