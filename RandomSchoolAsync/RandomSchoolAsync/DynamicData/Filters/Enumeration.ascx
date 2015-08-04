<%@ Control Language="C#" CodeBehind="Enumeration.ascx.cs" Inherits="RandomSchoolAsync.Filters.EnumerationFilter" %>
<div runat="server" class="form-group">
	<asp:DropDownList runat="server" ID="SFFilter_DropDownList1" 
		AutoPostBack="True" 
		CssClass="SFFilterDDFilter"
		OnSelectedIndexChanged="SFFilter_DropDownList1_SelectedIndexChanged">
	  <asp:ListItem Text="All" Value="" />
	</asp:DropDownList>
</div>
