<%@ Page Title="CourseList" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="RandomSchool.Maintain.vCourse.Default" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Register TagName="Foreign" TagPrefix="LC" Src="~/DynamicData/UserTemplates/LabelControl.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Courses List</h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="Insert" Text="Create new" />
    </p>

    <scf:ScaffoldingFilterRepeater runat="server" ID="FilterRepeater" ModelName="RandomSchool.Models.Course, RandomSchool" ScaffoldFilterContainerId="">
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="ScaffoldLabel_PreRender" />
        <scf:ScaffoldFilterControl runat="server" ID="ScaffoldFilter" OnFilterChanged="ScaffoldFilter_FilterChanged" OnFilterLoad="ScaffoldFilter_FilterLoad" OnLoad="sfForeignKey_Load"/>
    </ItemTemplate>
    </scf:ScaffoldingFilterRepeater>

    <div>
        <asp:ListView id="lvCourse" runat="server"
            DataKeyNames="CourseId"
			ItemType="RandomSchool.Models.Course"
            SelectMethod="GetData" OnSorting="lvCourse_Sorting"
			OnPagePropertiesChanging="lvCourse_PagePropertiesChanging" OnLayoutCreated="lvCourse_LayoutCreated">
            <EmptyDataTemplate>
                There are no entries found for Courses
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="table table-striped" title="Courses list view">
					<caption></caption>
                    <thead>
                        <tr runat="server" id="headerRow">
							<th><asp:LinkButton Text="Class#" CommandName="Sort" CommandArgument="CourseId" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="QAN" CommandName="Sort" CommandArgument="QAN" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Title" CommandName="Sort" CommandArgument="Title" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Description" CommandName="Sort" CommandArgument="Description" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Code" CommandName="Sort" CommandArgument="SubjectCode" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Room" CommandName="Sort" CommandArgument="Room.Description" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Department" CommandName="Sort" CommandArgument="Department.Name" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder" />
                    </tbody>
                </table>
				<asp:DataPager ID="dpCourse" PageSize="10" runat="server">
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
					<td><asp:DynamicControl runat="server" DataField="CourseId" ID="CourseId" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="QAN" ID="QAN" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="Title" ID="Title" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="Description" ID="Description" Mode="ReadOnly" /></td>
					<td><asp:DynamicControl runat="server" DataField="SubjectCode" ID="SubjectCode" Mode="ReadOnly" /></td>
					<td><LC:Foreign runat="server" ForeignKeyText='<%# Item.Room == null ? "Not Set" : Item.Room.Description == null ? "Not Set" : Item.Room.Description %>' /></td>
					<td><LC:Foreign runat="server" ForeignKeyText='<%# Item.Department == null ? "Not Set" : Item.Department.Name == null ? "Not Set" : Item.Department.Name %>' /></td>
					<td>
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vCourse/Details", Item.CourseId) %>' Text="Details" /> | 
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vCourse/Edit", Item.CourseId) %>' Text="Edit" /> | 
                        <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vCourse/Delete", Item.CourseId) %>' Text="Delete" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

