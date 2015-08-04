<%@ Page Title="PersonList" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="RandomSchool.Maintain.vPerson.Default" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Register TagName="Foreign" TagPrefix="LC" Src="~/DynamicData/UserTemplates/LabelControl.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>People List</h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="Insert" Text="Create new" />
    </p>

    <scf:ScaffoldingFilterRepeater runat="server" ID="FilterRepeater" ModelName="RandomSchool.Models.Person, RandomSchool" ScaffoldFilterContainerId="">
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="ScaffoldLabel_PreRender" />
        <scf:ScaffoldFilterControl runat="server" ID="ScaffoldFilter" OnFilterChanged="ScaffoldFilter_FilterChanged" OnFilterLoad="ScaffoldFilter_FilterLoad" OnLoad="sfForeignKey_Load"/>
    </ItemTemplate>
    </scf:ScaffoldingFilterRepeater>

    <div>
        <asp:ListView id="lvPerson" runat="server"
            DataKeyNames="PersonId"
			ItemType="RandomSchool.Models.Person"
            SelectMethod="GetData" OnSorting="lvPerson_Sorting"
			OnPagePropertiesChanging="lvPerson_PagePropertiesChanging" OnLayoutCreated="lvPerson_LayoutCreated">
            <EmptyDataTemplate>
                There are no entries found for People
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="table table-striped" title="People list view">
					<caption></caption>
                    <thead>
                        <tr runat="server" id="headerRow">
							<th><asp:LinkButton Text="Person Id" CommandName="Sort" CommandArgument="PersonId" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="First Name" CommandName="Sort" CommandArgument="FirstMidName" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Last Name" CommandName="Sort" CommandArgument="LastName" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="School" CommandName="Sort" CommandArgument="School.SchoolName" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder" />
                    </tbody>
                </table>
				<asp:DataPager ID="dpPerson" PageSize="10" runat="server">
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
					<td><asp:DynamicControl runat="server" DataField="PersonId" ID="PersonId" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="FirstMidName" ID="FirstMidName" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="LastName" ID="LastName" Mode="ReadOnly" /></td>
					<td><LC:Foreign runat="server" ForeignKeyText='<%# Item.School == null ? "Not Set" : Item.School.SchoolName == null ? "Not Set" : Item.School.SchoolName %>' /></td>
					<td>
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vPerson/Details", Item.PersonId) %>' Text="Details" /> | 
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vPerson/Edit", Item.PersonId) %>' Text="Edit" /> | 
                        <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vPerson/Delete", Item.PersonId) %>' Text="Delete" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

