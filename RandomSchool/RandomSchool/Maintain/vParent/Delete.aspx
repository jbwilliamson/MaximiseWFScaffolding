<%@ Page Title="ParentDelete" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Delete.aspx.cs" Inherits="RandomSchool.Maintain.vParent.Delete" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <h3>Are you sure want to delete this Parent?</h3>
        <asp:FormView ID="fvParent" runat="server"
            ItemType="RandomSchool.Models.Parent" DataKeyNames="ParentId"
            DeleteMethod="DeleteItem" 
			SelectMethod="GetItem"
			RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Parent with ParentId <%: Request.QueryString["ParentId"] %>
            </EmptyDataTemplate>
            <ItemTemplate>
				<fieldset id="fsIdentity" class="form-horizontal">
					<legend class="small">Identity</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>First Name :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="FirstName" ID="FirstName" Mode="ReadOnly" />
						</div>
					</div>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Surname :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Surname" ID="Surname" Mode="ReadOnly" />
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
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Work Phone :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="WorkPhone" ID="WorkPhone" Mode="ReadOnly" />
						</div>
					</div>
				</fieldset>
				<fieldset id="fsPersonal" class="form-horizontal">
					<legend class="small">Personal</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Date Of Birth :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="DOB" ID="DOB" Mode="ReadOnly" />
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
							<strong>Marital Status :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="Status" ID="Status" Mode="ReadOnly" />
						</div>
					</div>
				</fieldset>
				<fieldset id="fsJob" class="form-horizontal">
					<legend class="small">Job</legend>
					<div class="row">
						<div class="col-sm-2 text-right">
							<strong>Job Description :</strong>
						</div>
						<div class="col-sm-4">
							<asp:DynamicControl runat="server" DataField="JobDescription" ID="JobDescription" Mode="ReadOnly" />
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

