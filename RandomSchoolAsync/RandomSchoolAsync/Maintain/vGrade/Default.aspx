<%@ Page Title="GradeList" Async="true" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="RandomSchoolAsync.Maintain.vGrade.Default" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Register TagName="Foreign" TagPrefix="LC" Src="~/DynamicData/UserTemplates/LabelControl.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Grades List</h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="Insert" Text="Create new" />
    </p>

    <scf:ScaffoldingFilterRepeater runat="server" ID="FilterRepeater" ModelName="RandomSchoolAsync.Models.Grade, RandomSchoolAsync" ScaffoldFilterContainerId="">
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="ScaffoldLabel_PreRender" />
        <scf:ScaffoldFilterControl runat="server" ID="ScaffoldFilter" OnFilterChanged="ScaffoldFilter_FilterChanged" OnFilterLoad="ScaffoldFilter_FilterLoad" OnLoad="sfForeignKey_Load"/>
    </ItemTemplate>
    </scf:ScaffoldingFilterRepeater>

    <div>
        <asp:ListView id="lvGrade" runat="server"
            DataKeyNames="id"
			ItemType="RandomSchoolAsync.Models.Grade"
            SelectMethod="GetData" OnSorting="lvGrade_Sorting"
			OnPagePropertiesChanging="lvGrade_PagePropertiesChanging" OnLayoutCreated="lvGrade_LayoutCreated">
            <EmptyDataTemplate>
                There are no entries found for Grades
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="table table-striped" title="Grades list view">
					<caption></caption>
                    <thead>
                        <tr runat="server" id="headerRow">
							<th><asp:LinkButton Text="Id" CommandName="Sort" CommandArgument="id" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Year" CommandName="Sort" CommandArgument="Year.SchoolYear" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Grading" CommandName="Sort" CommandArgument="Grading.GradeLetter" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Course" CommandName="Sort" CommandArgument="Course.QAN" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th><asp:LinkButton Text="Pupil" CommandName="Sort" CommandArgument="Pupil.URN" runat="Server" />&nbsp;<span runat="server" class=""></span></th>
							<th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder" />
                    </tbody>
                </table>
				<asp:DataPager ID="dpGrade" PageSize="10" runat="server">
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
					<td><asp:DynamicControl runat="server" DataField="id" ID="id" Mode="ReadOnly" /></td>
					<td><LC:Foreign runat="server" ForeignKeyText='<%# Item.Year == null ? "Not Set" : Item.Year.SchoolYear == null ? "Not Set" : Item.Year.SchoolYear %>' /></td>
					<td><LC:Foreign runat="server" ForeignKeyText='<%# Item.Grading == null ? "Not Set" : Item.Grading.GradeLetter == null ? "Not Set" : Item.Grading.GradeLetter %>' /></td>
					<td><LC:Foreign runat="server" ForeignKeyText='<%# Item.Course == null ? "Not Set" : Item.Course.QAN == null ? "Not Set" : Item.Course.QAN %>' /></td>
					<td><LC:Foreign runat="server" ForeignKeyText='<%# Item.Pupil == null ? "Not Set" : Item.Pupil.URN == null ? "Not Set" : Item.Pupil.URN %>' /></td>
					<td>
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vGrade/Details", Item.id) %>' Text="Details" /> | 
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vGrade/Edit", Item.id) %>' Text="Edit" /> | 
                        <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Maintain/vGrade/Delete", Item.id) %>' Text="Delete" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

