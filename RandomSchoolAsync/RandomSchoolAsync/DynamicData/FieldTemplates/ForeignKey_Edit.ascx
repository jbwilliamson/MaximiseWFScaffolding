<%@ Control Language="C#" CodeBehind="ForeignKey_Edit.ascx.cs" Inherits="RandomSchoolAsync.FieldTemplates.ForeignKey_EditField" %>
<div id="Div1" runat="server" class="form-group">
    <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="DropDownList1" />
    <div class="col-sm-3">
        <asp:DropDownList 
			ID="DropDownList1"
			CssClass="form-control"
			Runat="server" OnLoad="DropDownList1_Load">
        </asp:DropDownList>
    </div>
</div>
