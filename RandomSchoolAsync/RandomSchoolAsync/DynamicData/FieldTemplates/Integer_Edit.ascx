<%@ Control Language="C#" CodeBehind="Integer_Edit.ascx.cs" Inherits="RandomSchoolAsync.FieldTemplates.Integer_EditField" %>
<div id="Div1" runat="server" class="form-group">
    <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="TextBox1" />
    <div class="col-sm-3">
		<asp:TextBox ID="TextBox1" type="Number" step="1" runat="server" Text='<%# FieldValueEditString %>' CssClass="form-control DDTextBox"></asp:TextBox>
    </div>
</div>
