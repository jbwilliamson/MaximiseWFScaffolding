<%@ Page Title="TeacherInsert" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Insert.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vTeacher.Insert" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Teachers - Insert</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvTeacher" runat="server"
            ItemType="RandomSchoolAsync.Models.Teacher" DefaultMode="Insert"
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
					<asp:DynamicControl Mode="Insert"
						DataField="NationId" 
						DataTypeName="RandomSchoolAsync.Models.Nation" 
						DataTextField="Country" 
						DataValueField="Id"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Insert" DataField="DateOfBirth" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Salary" runat="server" />
				</fieldset>
				<fieldset id="fsContact" class="form-horizontal">
					<legend class="small">Contact</legend>
					<asp:DynamicControl Mode="Insert" DataField="EmailAddress" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="WebSite" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Twitter" runat="server" />
				</fieldset>
				<fieldset id="fsAdditional" class="form-horizontal">
					<legend class="small">Additional</legend>
					<asp:DynamicControl Mode="Insert" DataField="AcademicInformation" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="DateOfHire" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Location" runat="server" />
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
