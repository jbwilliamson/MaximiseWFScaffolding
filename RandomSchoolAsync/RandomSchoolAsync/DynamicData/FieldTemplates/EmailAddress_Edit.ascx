﻿
<%@ Control Language="C#" CodeBehind="EmailAddress_Edit.ascx.cs" Inherits="RandomSchoolAsync.FieldTemplates.EmailAddress_EditField" %>
<div id="Div1" runat="server" class="form-group">
    <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="TextBox1" />
    <div class="col-sm-3">
		<asp:TextBox ID="TextBox1" type="email" runat="server" Text='<%# FieldValueEditString %>' CssClass="form-control DDTextBox"></asp:TextBox>
    </div>
</div>
