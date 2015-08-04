<%@ Page Title="CourseDelete" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Delete.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vCourse.Delete" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <h3>Are you sure want to delete this Course?</h3>
        <asp:FormView ID="fvCourse" runat="server"
            ItemType="RandomSchoolAsync.Models.Course" DataKeyNames="CourseId"
            DeleteMethod="DeleteItem" 
			SelectMethod="GetItem"
			RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Course with CourseId <%: Request.QueryString["CourseId"] %>
            </EmptyDataTemplate>
            <ItemTemplate>
				<fieldset id="fsTitle" class="form-horizontal">
					<legend class="small">Title</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>QAN :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="QAN" ID="QAN" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Title :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Title" ID="Title" Mode="ReadOnly" />
						</div>
					</div>
				</fieldset>
				<fieldset id="fsDescription" class="form-horizontal">
					<legend class="small">Description</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Description :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Description" ID="Description" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Code :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="SubjectCode" ID="SubjectCode" Mode="ReadOnly" />
						</div>
					</div>
				</fieldset>
				<fieldset id="fsLocation" class="form-horizontal">
					<legend class="small">Location</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Room :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Room != null ? Item.Room.Description : "" %>
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Department :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Department != null ? Item.Department.Name : "" %>
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

