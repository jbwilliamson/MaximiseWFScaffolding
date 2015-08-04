<%@ Control Language="C#" CodeBehind="Date_Edit.ascx.cs" Inherits="RandomSchool.FieldTemplates.Date_EditField" %>
<div id="Div1" runat="server" class="form-group">
    <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="TextBox1" />
    <div class="col-sm-3">
		<asp:TextBox ID="TextBox1" type="Date" runat="server" Text='<%# FieldValueEditString %>' CssClass="form-control DDTextBox"></asp:TextBox>
    </div>
</div>