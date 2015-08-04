<%@ Page Title="Pupil Details" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Details.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vPupil.Details" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<h2>Pupils - Details</h2>
		<p>&nbsp;</p>
        <asp:FormView ID="fvPupil" runat="server"
            ItemType="RandomSchoolAsync.Models.Pupil" DataKeyNames="StudentId"
            SelectMethod="GetItem"
            RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Pupil with StudentId <%: Request.QueryString["StudentId"] %>
            </EmptyDataTemplate>
            <ItemTemplate>
				<fieldset id="fsName" class="form-horizontal">
					<legend class="small">Name</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Person Id :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Person != null ? Item.Person.FirstMidName : "Not Set" %>
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>URN :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="URN" ID="URN" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>First Parent or Guardian :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.FirstParent != null ? Item.FirstParent.FirstName : "Not Set" %>
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Second Parent or Guardian :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.SecondParent != null ? Item.SecondParent.FirstName : "Not Set" %>
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Academic Year :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="AcademicYear" ID="AcademicYear" Mode="ReadOnly" />
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
							<strong>Student disability :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Disability" ID="Disability" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Nationality :</strong>
						</div>
						<div class="col-sm-4">
							<%#: Item.Nation != null ? Item.Nation.Country : "Not Set" %>
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
							<strong>Twitter :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Twitter" ID="Twitter" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Instagram :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Instagram" ID="Instagram" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Facebook Page :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="FacebookPage" ID="FacebookPage" Mode="ReadOnly" />
						</div>
					</div>
				</fieldset>
				<fieldset id="fsAdditional" class="form-horizontal">
					<legend class="small">Additional</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Other Information :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="OtherInformation" ID="OtherInformation" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Enrollment Date :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="EnrollmentDate" ID="EnrollmentDate" Mode="ReadOnly" />
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

