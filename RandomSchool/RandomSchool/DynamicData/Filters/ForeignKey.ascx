<%@ Control Language="C#" CodeBehind="ForeignKey.ascx.cs" Inherits="RandomSchool.Filters.ForeignKeyFilter" %>
<div runat="server" class="form-group">
	<asp:DropDownList runat="server" ID="SFFilter_DropDownList1" 
		AppendDataBoundItems="true"
		AutoPostBack="True" 
		CssClass="SFFilterDDFilter"
		OnSelectedIndexChanged="SFFilter_DropDownList1_SelectedIndexChanged" OnLoad="SFFilter_DropDownList1_Load">
	</asp:DropDownList>
</div>
