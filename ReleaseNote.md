Release note - Maximise Web Forms Scaffolding - v0.1.0
===================

This projects forks the existing 'WebFormsScaffolding' project and adds a number of useful enhancements to provide a better forms scaffolding experience.

- Client side validation has been added, select either HTML5 or MS Unobtrusive Validation Library when performaing a scaffold.
- Filters can be setup for Enumeration and Associated Entities using the [FilterUIHint("ForeignKey")] or [FilterUIHint("Enumeration")] attribute.
- A repository pattern has been added for database access. A repository interface is used to build a generic Repository base, a class repository is then built for each model scaffolded.
- Sync and Aysnc database access is supported, you specify this on scaffolding a class.  Need to use one or the other in a project, they will not mix as yet.  (Note that model binding and listview SelectMethod do not work with Async, this has been fixed in ASP.NET 4.6, will update support soon)
- The view class name can now be specified when scaffolding.  This allows the forms class to be different than the model class.

- List view - now has bootstrap sort icons for asc/desc.
- List view - page size can be configured.
- List view - sorting, position and page size are saved in the session and will be restored when returning to list page (e.g. after edit etc)
- Edit, Insert - FieldSet HTML element added for each unquiue 'GroupName' in the 'Display' attribute.  If no 'GroupName' specified then one generic fieldset created.
- Edit, Insert - labels are now assiocated with their input control.
- Edit, Insert - Date and Datetime FieldTemplates now match the types DataType.Date and DataType.DateTime, allowing validation of Dates or validation of Date&Time
- Edit, Insert - 'Placeholder' attribute of the HTML input field is supported from the 'Display' atribute 'Prompt' property.
- Edit, Insert, Details - the order of the displayed fields can be specifed using the 'Order' property of the 'Display' attribute.  The order property should work within the 'Groupname'.
- Edit, Insert, Details - either the 'Display' or the 'DisplayName' attribute can be used to specific the field display name.

- Edit, Insert - the foreign key table or Associated Entities, can be sorted before displayed in a dropdownlist using the 'UIHint' attribute: (useful if table not sorted by EF)
	[UIHint("ForeignKey", null, "RequireSorting", "Yes", "SortDesending", false)]
- List view - by default a filter will use the first string it finds to display text in the filter dropdownlist and provides sorting based on useage in the list.  This can be overridden using the 'FilterUIhint' attribute:
	[FilterUIHint("ForeignKey", null, "TextField", "Name", "RequireSorting", "Yes", "SortDesending", false)]
- Edit, Insert - templates support the 'SetFocus' attribute to set the focus to the first field in the form, automatically sets the first field in form as focus.

- Annotations - the included 'ScaffoldFilter' assembly includes two useful annotation attributes
[ScaffoldFilter.GridView(true)] - Include this field in the listview, if model does not include a 'GridView' attribute then all fields are include in the list (you manually remove ones not needed), this can be reduced to only the required fields using this attribute from the model
[ScaffoldFilter.SQLDateAttribute(ErrorMessage = "{0} date specified is outside acceptable range")] - Validates a date against an SQL valid date, SQL Server does not allow all dates and will throw a nasty looking error if date not valid.  This is a server side check to make sure your date is valid in SQL Server.
