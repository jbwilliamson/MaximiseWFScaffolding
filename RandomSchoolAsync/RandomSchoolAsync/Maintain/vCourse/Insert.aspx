<%@ Page Title="CourseInsert" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Insert.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vCourse.Insert" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Courses - Insert</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvCourse" runat="server"
            ItemType="RandomSchoolAsync.Models.Course" DefaultMode="Insert"
            InsertItemPosition="FirstItem" 
			InsertMethod="InsertItem"
            RenderOuterTable="false">
            <InsertItemTemplate>
				<asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
				<fieldset id="fsTitle" class="form-horizontal">
					<legend class="small">Title</legend>
					<asp:DynamicControl Mode="Insert" DataField="QAN" SetFocus="True" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="Title" runat="server" />
				</fieldset>
				<fieldset id="fsDescription" class="form-horizontal">
					<legend class="small">Description</legend>
					<asp:DynamicControl Mode="Insert" DataField="Description" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="SubjectCode" runat="server" />
				</fieldset>
				<fieldset id="fsLocation" class="form-horizontal">
					<legend class="small">Location</legend>
					<asp:DynamicControl Mode="Insert"
						DataField="RoomId" 
						DataTypeName="RandomSchoolAsync.Models.Room" 
						DataTextField="Description" 
						DataValueField="id"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Insert"
						DataField="DepartmentId" 
						DataTypeName="RandomSchoolAsync.Models.Department" 
						DataTextField="Name" 
						DataValueField="DepartmentId"
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
