Release note - Maximise Web Forms Scaffolding - v0.1.0
===================

This projects forks the existing 'WebFormsScaffolding' and adds a number of useful enhancements to provide a better scaffolding experience

- Client side validation has been added, select either HTML5 or MS Unobtrusive Validation Library.
- List filters can be setup for Enumeration and Associated Entities whiching the [FilterUIHint("ForeignKey")] or [FilterUIHint("Enumeration")] attribute.
- A repository pattern has been added for database access. A repository interface is used to build a generic Repository base, a class repository is then built for each model scaffolded
- Sync and Aysnc database access is supported, you specify this on scaffold.  Need to use one or the other in a project, they will not mix as yet.  (Note that model binding and listview SelectMethod do not work with Async, this has been fixed in ASP.NET 4.6, will update support soon)
- The view class name can now be specified when scaffolded (was set to same as model)

- List view now has bootstrap sort icons
- List view page size can be configured
- List view, sorting, position and page size are saved in the session and return to edited position
- Edit, Insert, FieldSet HTML element added for each unquiue 'GroupName' in the 'Display' attribute.  If no 'GroupName' specified then ne generic fieldset created.
- Edit, Insert, labels are now assiocated with their input control
- Edit, Insert, Date and Datetime FieldTemplates now match the types DataType.Date and DataType.DateTime, allowing validation of Dates and Date&Time
- Edit, Insert, 'Placeholder' attribute of the HTML input field is supported from the 'Display' atribute 'Prompt' property.
- Edit, Insert, Details, the order of the displayed fields can be specifed using the 'Order' property of the 'Display' attribute.  The order property should work within the 'Groupname'.
- Edit, Insert, Details, the 'Display' or the 'DisplayName' attribute can be used to specific the field display name.

- Edit, Insert, the foreign key table or Associated Entities, can be sorted before displayed in a dropdownlist using the 'UIHint' attribute: (useful if table not sorted by EF)
	[UIHint("ForeignKey", null, "RequireSorting", "Yes", "SortDesending", false)]
- List view, by default a filter will use the first string it finds to display text in the filter dropdownlist and provides sorting based on useage in the list.  This can be overridden using the 'FilterUIhint' attribute:
	[FilterUIHint("ForeignKey", null, "TextField", "Name", "RequireSorting", "Yes", "SortDesending", false)]
- Edit, Insert, templates support the 'SetFocus' attribute to set the focus to the first field in the form.

- Annotations, the included 'ScaffoldFilter' assembly includes two useful annotation attributes
[ScaffoldFilter.GridView(true)] - Include this field in the listview, if model does not include a 'GridView' then all fields are include in the list, this can be reduced to only the required fields using this attribute
[ScaffoldFilter.SQLDateAttribute(ErrorMessage = "{0} date specified is outside acceptable range")] - Validates a date against a SQL valid date, SQL Server does not allow all dates and will throw a nasty looking error id not valid.  This is a server side check to make sure your date is valid in SQL Server.