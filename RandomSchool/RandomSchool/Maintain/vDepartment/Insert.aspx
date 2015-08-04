<%@ Page Title="DepartmentInsert" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Insert.aspx.cs" Inherits="RandomSchool.Maintain.vDepartment.Insert" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Departments - Insert</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvDepartment" runat="server"
            ItemType="RandomSchool.Models.Department" DefaultMode="Insert"
            InsertItemPosition="FirstItem" 
			InsertMethod="InsertItem"
            RenderOuterTable="false">
            <InsertItemTemplate>
				<asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
				<fieldset id="fsName" class="form-horizontal">
					<legend class="small">Name</legend>
					<asp:DynamicControl Mode="Insert" DataField="Name" SetFocus="True" runat="server" />
				</fieldset>
				<fieldset id="fsDetails" class="form-horizontal">
					<legend class="small">Details</legend>
					<asp:DynamicControl Mode="Insert" DataField="Budget" runat="server" />
					<asp:DynamicControl Mode="Insert" DataField="StartDate" runat="server" />
					<asp:DynamicControl Mode="Insert"
						DataField="InstructorId" 
						DataTypeName="RandomSchool.Models.Teacher" 
						DataTextField="HomeAddress1" 
						DataValueField="TeacherId"
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
