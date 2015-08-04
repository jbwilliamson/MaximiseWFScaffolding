<%@ Page Title="PupilList" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vPupil.Default" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Register TagName="Foreign" TagPrefix="LC" Src="~/DynamicData/UserTemplates/LabelControl.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Pupils List</h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="Insert" Text="Create new" />
    </p>

    <scf:ScaffoldingFilterRepeater runat="server" ID="FilterRepeater" ModelName="RandomSchoolAsync.Models.Pupil, RandomSchoolAsync" ScaffoldFilterContainerId="">
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="ScaffoldLabel_PreRender" />
        <scf:ScaffoldFilterControl runat="server" ID="ScaffoldFilter" OnFilterChanged="ScaffoldFilter_FilterChanged" OnFilterLoad="ScaffoldFilter_FilterLoad" OnLoad="sfForeignKey_Load"/>
    </ItemTemplate>
    </scf:ScaffoldingFilterRepeater>

    <div>
        <asp:ListView id="lvPupil" runat="server"
            DataKeyNames="StudentId"
			ItemType="RandomSchoolAsync.Models.Pupil"
            SelectMethod="GetData" OnSorting="lvPupil_Sorting"
			OnPagePropertiesChanging="lvPupil_PagePropertiesChanging" OnLayoutCreated="lvPupil_LayoutCreated">
            <EmptyDataTemplate>
                There are no entries found for Pupils
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="table table-striped" title="Pupils list view">
					<caption></caption>
                    <thead>
                        <tr runat="server" id="headerRow">
							<th><asp:LinkButton Text="Student Id" CommandName="Sort" CommandArgument="StudentId" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Person" CommandName="Sort" CommandArgument="Person.FirstMidName" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Town or City" CommandName="Sort" CommandArgument="Town" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Gender" CommandName="Sort" CommandArgument="Gender" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Nation" CommandName="Sort" CommandArgument="Nation.Country" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Date Of Birth" CommandName="Sort" CommandArgument="DateOfBirth" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Enrollment Date" CommandName="Sort" CommandArgument="EnrollmentDate" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder" />
                    </tbody>
                </table>
				<asp:DataPager ID="dpPupil" PageSize="10" runat="server">
					<Fields>
						<asp:TemplatePagerField>
							<PagerTemplate>
								<asp:Label runat="server" AssociatedControlID="ddlPageSize">Page Size</asp:Label>
								<asp:DropDownList runat="server" id="ddlPageSize" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" OnPreRender="ddlPageSize_PreRender" AutoPostBack="true">
									<asp:ListItem Value="10" />
									<asp:ListItem Value="15" />
									<asp:ListItem Value="20" />
									<asp:ListItem Value="25" />
									<asp:ListItem Value="30" />
								</asp:DropDownList>
							</PagerTemplate>
						</asp:TemplatePagerField>
						
						<asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn" />
                        <asp:NumericPagerField ButtonType="Button"  NumericButtonCssClass="btn" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                        <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn" />
                    </Fields>
				</asp:DataPager>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
					<td><asp:DynamicControl runat="server" DataField="StudentId" ID="StudentId" Mode="ReadOnly" /></td>
					<td><LC:Foreign runat="server" ForeignKeyText='<%# Item.Person == null ? "Not Set" : Item.Person.FirstMidName == null ? "Not Set" : Item.Person.FirstMidName %>' /></td>
					<td><asp:DynamicControl runat="server" DataField="Town" ID="Town" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="Gender" ID="Gender" Mode="ReadOnly" /></td>
					<td><LC:Foreign runat="server" ForeignKeyText='<%# Item.Nation == null ? "Not Set" : Item.Nation.Country == null ? "Not Set" : Item.Nation.Country %>' /></td>
					<td><asp:DynamicControl runat="server" DataField="DateOfBirth" ID="DateOfBirth" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="EnrollmentDate" ID="EnrollmentDate" Mode="ReadOnly" /></td>
					<td>
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vPupil/Details", Item.StudentId) %>' Text="Details" /> | 
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vPupil/Edit", Item.StudentId) %>' Text="Edit" /> | 
                        <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vPupil/Delete", Item.StudentId) %>' Text="Delete" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

