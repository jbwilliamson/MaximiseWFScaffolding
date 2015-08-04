<%@ Page Title="GradeDelete" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Delete.aspx.cs" Inherits="RandomSchool.Maintain.vGrade.Delete" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <h3>Are you sure want to delete this Grade?</h3>
        <asp:FormView ID="fvGrade" runat="server"
            ItemType="RandomSchool.Models.Grade" DataKeyNames="id"
            DeleteMethod="DeleteItem" 
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
							<%#: Item.Year != null ? Item.Year.SchoolYear : "" %>
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Grade :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Grading != null ? Item.Grading.GradeLetter : "" %>
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Course :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Course != null ? Item.Course.QAN : "" %>
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Pupil :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Pupil != null ? Item.Pupil.URN : "" %>
						</div>
					</div>
					<div class="row">
						&nbsp;
					</div>
				</fieldset>
				<fieldset id="fsFormSubmit" class="form-horizontal">
					<div class="form-group">
						<div class="col-sm-offset-2 col-sm-10">
							<asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger" />
							<asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-default" />
						</div>
					</div>
                </fieldset>
            </ItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>

