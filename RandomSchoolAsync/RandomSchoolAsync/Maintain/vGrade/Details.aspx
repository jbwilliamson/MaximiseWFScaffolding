<%@ Page Title="Grade Details" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Details.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vGrade.Details" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Grades - Details</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvGrade" runat="server"
            ItemType="RandomSchoolAsync.Models.Grade" DataKeyNames="id"
            SelectMethod="GetItem"
            RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Grade with id <%: Request.QueryString["id"] %>
            </EmptyDataTemplate>
            <ItemTemplate>
				<fieldset id="fsGrade" class="form-horizontal">
					<legend class="small">Grade</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Year :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Year != null ? Item.Year.SchoolYear : "Not Set" %>
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Grade :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Grading != null ? Item.Grading.GradeLetter : "Not Set" %>
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Course :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Course != null ? Item.Course.QAN : "Not Set" %>
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Pupil :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Pupil != null ? Item.Pupil.URN : "Not Set" %>
						</div>
					</div>
					<div class="row">
						&nbsp;
					</div>
				</fieldset>
				<fieldset id="fsFormSubmit" class="form-horizontal">
					<div class="form-group">
						<div class="col-sm-offset-2 col-sm-10">
							<asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Back" CssClass="btn btn-default" />
						</div>
					</div>
                </fieldset>
            </ItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>

