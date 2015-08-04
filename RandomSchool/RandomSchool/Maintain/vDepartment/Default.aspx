<%@ Page Title="DepartmentList" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="RandomSchool.Maintain.vDepartment.Default" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Register TagName="Foreign" TagPrefix="LC" Src="~/DynamicData/UserTemplates/LabelControl.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Departments List</h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="Insert" Text="Create new" />
    </p>

    <scf:ScaffoldingFilterRepeater runat="server" ID="FilterRepeater" ModelName="RandomSchool.Models.Department, RandomSchool" ScaffoldFilterContainerId="">
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="ScaffoldLabel_PreRender" />
        <scf:ScaffoldFilterControl runat="server" ID="ScaffoldFilter" OnFilterChanged="ScaffoldFilter_FilterChanged" OnFilterLoad="ScaffoldFilter_FilterLoad" OnLoad="sfForeignKey_Load"/>
    </ItemTemplate>
    </scf:ScaffoldingFilterRepeater>

    <div>
        <asp:ListView id="lvDepartment" runat="server"
            DataKeyNames="DepartmentId"
			ItemType="RandomSchool.Models.Department"
            SelectMethod="GetData" OnSorting="lvDepartment_Sorting"
			OnPagePropertiesChanging="lvDepartment_PagePropertiesChanging" OnLayoutCreated="lvDepartment_LayoutCreated">
            <EmptyDataTemplate>
                There are no entries found for Departments
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="table table-striped" title="Departments list view">
					<caption></caption>
                    <thead>
                        <tr runat="server" id="headerRow">
							<th><asp:LinkButton Text="Id" CommandName="Sort" CommandArgument="DepartmentId" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Name" CommandName="Sort" CommandArgument="Name" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Budget" CommandName="Sort" CommandArgument="Budget" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Start Date" CommandName="Sort" CommandArgument="StartDate" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Administrator" CommandName="Sort" CommandArgument="Administrator.HomeAddress1" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder" />
                    </tbody>
                </table>
				<asp:DataPager ID="dpDepartment" PageSize="10" runat="server">
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
					<td><asp:DynamicControl runat="server" DataField="DepartmentId" ID="DepartmentId" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="Name" ID="Name" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="Budget" ID="Budget" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="StartDate" ID="StartDate" Mode="ReadOnly" /></td>
					<td><LC:Foreign runat="server" ForeignKeyText='<%# Item.Administrator == null ? "Not Set" : Item.Administrator.HomeAddress1 == null ? "Not Set" : Item.Administrator.HomeAddress1 %>' /></td>
					<td>
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vDepartment/Details", Item.DepartmentId) %>' Text="Details" /> | 
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vDepartment/Edit", Item.DepartmentId) %>' Text="Edit" /> | 
                        <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vDepartment/Delete", Item.DepartmentId) %>' Text="Delete" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

