<%@ Page Title="ParentList" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vParent.Default" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Parents List</h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="Insert" Text="Create new" />
    </p>

    <scf:ScaffoldingFilterRepeater runat="server" ID="FilterRepeater" ModelName="RandomSchoolAsync.Models.Parent, RandomSchoolAsync" ScaffoldFilterContainerId="">
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="ScaffoldLabel_PreRender" />
        <scf:ScaffoldFilterControl runat="server" ID="ScaffoldFilter" OnFilterChanged="ScaffoldFilter_FilterChanged" OnFilterLoad="ScaffoldFilter_FilterLoad" OnLoad="sfForeignKey_Load"/>
    </ItemTemplate>
    </scf:ScaffoldingFilterRepeater>

    <div>
        <asp:ListView id="lvParent" runat="server"
            DataKeyNames="ParentId"
			ItemType="RandomSchoolAsync.Models.Parent"
            SelectMethod="GetData" OnSorting="lvParent_Sorting"
			OnPagePropertiesChanging="lvParent_PagePropertiesChanging" OnLayoutCreated="lvParent_LayoutCreated">
            <EmptyDataTemplate>
                There are no entries found for Parents
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="table table-striped" title="Parents list view">
					<caption></caption>
                    <thead>
                        <tr runat="server" id="headerRow">
							<th><asp:LinkButton Text="Parent Id" CommandName="Sort" CommandArgument="ParentId" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="First Name" CommandName="Sort" CommandArgument="FirstName" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Surname" CommandName="Sort" CommandArgument="Surname" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Email Address" CommandName="Sort" CommandArgument="EmailAddress" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Town or City" CommandName="Sort" CommandArgument="Town" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Date Of Birth" CommandName="Sort" CommandArgument="DOB" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Gender" CommandName="Sort" CommandArgument="Gender" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Marital Status" CommandName="Sort" CommandArgument="Status" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder" />
                    </tbody>
                </table>
				<asp:DataPager ID="dpParent" PageSize="10" runat="server">
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
					<td><asp:DynamicControl runat="server" DataField="ParentId" ID="ParentId" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="FirstName" ID="FirstName" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="Surname" ID="Surname" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="EmailAddress" ID="EmailAddress" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="Town" ID="Town" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="DOB" ID="DOB" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="Gender" ID="Gender" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="Status" ID="Status" Mode="ReadOnly" /></td>
					<td>
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vParent/Details", Item.ParentId) %>' Text="Details" /> | 
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vParent/Edit", Item.ParentId) %>' Text="Edit" /> | 
                        <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vParent/Delete", Item.ParentId) %>' Text="Delete" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

