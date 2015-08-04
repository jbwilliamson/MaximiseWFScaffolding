<%@ Page Title="GradeInsert" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Insert.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vGrade.Insert" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Grades - Insert</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvGrade" runat="server"
            ItemType="RandomSchoolAsync.Models.Grade" DefaultMode="Insert"
            InsertItemPosition="FirstItem" 
			InsertMethod="InsertItem"
            RenderOuterTable="false">
            <InsertItemTemplate>
				<asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
				<fieldset id="fsGrade" class="form-horizontal">
					<legend class="small">Grade</legend>
					<asp:DynamicControl Mode="Insert"
						DataField="YearId" 
						DataTypeName="RandomSchoolAsync.Models.Year" 
						DataTextField="SchoolYear" 
						DataValueField="id"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Insert"
						DataField="GradeId" 
						DataTypeName="RandomSchoolAsync.Models.Grading" 
						DataTextField="GradeLetter" 
						DataValueField="id"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Insert"
						DataField="CourseId" 
						DataTypeName="RandomSchoolAsync.Models.Course" 
						DataTextField="QAN" 
						DataValueField="CourseId"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Insert"
						DataField="PupilId" 
						DataTypeName="RandomSchoolAsync.Models.Pupil" 
						DataTextField="URN" 
						DataValueField="StudentId"
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
