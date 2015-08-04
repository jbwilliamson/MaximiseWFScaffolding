<%@ Page Title="TeacherDelete" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Delete.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vTeacher.Delete" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <h3>Are you sure want to delete this Teacher?</h3>
        <asp:FormView ID="fvTeacher" runat="server"
            ItemType="RandomSchoolAsync.Models.Teacher" DataKeyNames="TeacherId"
            DeleteMethod="DeleteItem" 
			SelectMethod="GetItem"
			RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Teacher with TeacherId <%: Request.QueryString["TeacherId"] %>
            </EmptyDataTemplate>
            <ItemTemplate>
				<fieldset id="fsName" class="form-horizontal">
					<legend class="small">Name</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Person Id :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Person != null ? Item.Person.FirstMidName : "" %>
						</div>
					</div>
				</fieldset>
				<fieldset id="fsAddress" class="form-horizontal">
					<legend class="small">Address</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Home Address Line 1 :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="HomeAddress1" ID="HomeAddress1" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Home Address Line 2 :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="HomeAddress2" ID="HomeAddress2" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Town or City :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Town" ID="Town" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>County :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="County" ID="County" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Country :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Country" ID="Country" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Postcode :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Postcode" ID="Postcode" Mode="ReadOnly" />
						</div>
					</div>
				</fieldset>
				<fieldset id="fsPhone" class="form-horizontal">
					<legend class="small">Phone</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Home Phone :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="HomePhone" ID="HomePhone" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Mobile Phone :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="MobilePhone" ID="MobilePhone" Mode="ReadOnly" />
						</div>
					</div>
				</fieldset>
				<fieldset id="fsPersonal" class="form-horizontal">
					<legend class="small">Personal</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Age :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Age" ID="Age" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Gender :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Gender" ID="Gender" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Nationality :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Nation != null ? Item.Nation.Country : "" %>
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Date Of Birth :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="DateOfBirth" ID="DateOfBirth" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Salary :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Salary" ID="Salary" Mode="ReadOnly" />
						</div>
					</div>
				</fieldset>
				<fieldset id="fsContact" class="form-horizontal">
					<legend class="small">Contact</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Email Address :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="EmailAddress" ID="EmailAddress" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Your Website :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="WebSite" ID="WebSite" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Twitter :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Twitter" ID="Twitter" Mode="ReadOnly" />
						</div>
					</div>
				</fieldset>
				<fieldset id="fsAdditional" class="form-horizontal">
					<legend class="small">Additional</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Academic Information :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="AcademicInformation" ID="AcademicInformation" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Hire Date :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="DateOfHire" ID="DateOfHire" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Office Location :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Location" ID="Location" Mode="ReadOnly" />
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

