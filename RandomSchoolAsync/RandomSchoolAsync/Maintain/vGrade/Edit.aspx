<%@ Page Title="GradeEdit" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Edit.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vGrade.Edit" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Grades - Edit</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvGrade" runat="server"
            ItemType="RandomSchoolAsync.Models.Grade" DefaultMode="Edit" DataKeyNames="id"
            UpdateMethod="UpdateItem" 
			SelectMethod="GetItem"
            RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Grade with id <%: Request.QueryString["id"] %>
            </EmptyDataTemplate>
            <EditItemTemplate>
				<asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
				<fieldset id="fsGrade" class="form-horizontal">
					<legend class="small">Grade</legend>
					<asp:DynamicControl Mode="Edit" 
						DataField="YearId" 
						DataTypeName="RandomSchoolAsync.Models.Year" 
						DataTextField="SchoolYear" 
						DataValueField="id"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Edit" 
						DataField="GradeId" 
						DataTypeName="RandomSchoolAsync.Models.Grading" 
						DataTextField="GradeLetter" 
						DataValueField="id"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Edit" 
						DataField="CourseId" 
						DataTypeName="RandomSchoolAsync.Models.Course" 
						DataTextField="QAN" 
						DataValueField="CourseId"
						UIHint="ForeignKey" runat="server" OnLoad="dcForeignKey_Load" />
					<asp:DynamicControl Mode="Edit" 
						DataField="PupilId" 
						DataTypeName="RandomSchoolAsync.Models.Pupil" 
						DataTextField="URN" 
						DataValueField="StudentId"
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

