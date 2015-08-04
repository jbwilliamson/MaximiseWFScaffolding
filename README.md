ASP.NET Maximise your Web Forms Scaffolding
===================

 -- [View the ASP.NET Maximise Web Forms Scaffolder at the Visual Studio Gallery]() --

_Scaffolding for Web Forms in Visual Studio 2013_. Given a model class, the Maximise Web Forms Scaffolder generates List, detail, Insert, Edit, and Delete pages. The Maximise Web Forms Scaffolder uses the Entity Framework, Bootstrap and Dynamic Data.

![Edit Form](/READMEImages/MainExample2.png "Edit Form")

![List Model](/READMEImages/MainList2.png "List Model")

## Installing the Maximise Web Forms Scaffolder

The Maximise Web Forms Scaffolder requires Visual Studio 2013 _Update 5_. It supports C# (Visual Basic updated soon).

You can install the Maximise Web Forms Scaffolder directly from within Visual Studio. Select the menu option Tools, Extensions and Updates, and install the Maximise Web Forms Scaffolder.

![Install Maximise Web Forms Scaffolder](/READMEImages/Install.png "Install Maximise Web Forms Scaffolder")

The tool required the addition of an assembly reference to your project of 'ScaffoldFilter.dll' and a controls reference updated to the web.conf. 

''''
system.web/pages/controls
<add tagPrefix="scf" assembly="ScaffoldFilter" namespace="ScaffoldFilter" />
''''

They will be automaticly added to your project on the first scaffold process or you can add these manually before scaffolding


## Using the Maximise Web Forms Scaffolder

First, you need to create the Entity Franework model class (code or database first), that you want to scaffold. For example, here is a simple School Course class:

```C#
public class Course
{
	public int CourseId { get; set; }
    public string QAN { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string SubjectCode { get; set; }
    public int RoomId { get; set; }
    public int DepartmentId { get; set; }
    
	public virtual Room Room { get; set; }
    public virtual Department Department { get; set; }
    public virtual ICollection<Teacher> Teachers { get; set; }
    public virtual ICollection<Pupil> Pupils { get; set; }
}
```

Next, you need to annotate the class attributes to configure the scaffold, example of Course model annotation:

```C#
public class Course
{
	[Display(Name = "Class Id", Order = 1, ShortName = "Id")]
    public int CourseId { get; set; }

    [Display(Name = "QAN", Order = 1, Prompt = "[nnn-nnnn-x]", ShortName = "QAN", GroupName = "Title")]
    [Required(ErrorMessage = "Qualification Accreditation Number is a required field")]
    [RegularExpression(@"(\d{3}-\d{4}-[A-Za-z0-9])", ErrorMessage = "QAN should consist of the format [NNN-NNNN-X]")]
    [Description("Specifies the Qualification Accreditation Number of the course, format: [NNN-NNNN-X]")]
    [MaxLength(10)]
    public string QAN { get; set; }

    [Display(Name = "Title", Order = 2, Prompt = "[Class Title]", GroupName = "Title")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "A Course Title should contain between 3 and 50 characters")]
    [Required(ErrorMessage = "Class Title is a required field")]
    [Description("Specifies the title of the Class, should containing alpha-numeric characters only")]
    [RegularExpression("([a-zA-Z0-9 .&%*-]+)", ErrorMessage = "Class Title should consist of alpha-numeric characters only")]
    public string Title { get; set; }

    [Display(Name = "Description", Order = 3, Prompt = "[Class Description]", GroupName = "Description")]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "A Class Description should contain between 3 and 200 characters")]
    [Required(ErrorMessage = "Class Description is a required field")]
    [Description("Specifies a Description of the class")]
    public string Description { get; set; }

    [Display(Name = "Code", Order = 4, Prompt = "[XNNN]", GroupName = "Description")]
    [Required(ErrorMessage = "Subject code is a required field")]
    [Description("Specifies the subject code for this class")]
    [RegularExpression(@"([a-zA-Z]\d{3})", ErrorMessage = "The subject code is a 4 alphanumeric code in the format [XNNN]")]
    [MaxLength(4)]
    public string SubjectCode { get; set; }

    [Display(Name = "Room", Order = 5, ShortName = "Room#", GroupName="Location")]
    [Description("The room designated to this class")]
    [Required(ErrorMessage = "The room number if a required field")]
    [FilterUIHint("ForeignKey", null, "TextField", "Description", "RequireSorting", "No")]
    public int RoomId { get; set; }

    [Display(Name = "Department", Order = 6, ShortName = "Dept#", GroupName="Location")]
    [Description("Course is part of this department")]
    [Required(ErrorMessage = "The course department is a required field")]
    [UIHint("ForeignKey", null, "TextField", "Name", "RequireSorting", "Yes")]
    [FilterUIHint("ForeignKey", null, "TextField", "Name", "RequireSorting", "Yes")]
    public int DepartmentId { get; set; }

    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; }
	
	public virtual Department Department { get; set; }

    public virtual ICollection<Teacher> Teachers { get; set; }

    public virtual ICollection<Pupil> Pupils { get; set; }
}
```

Next, define a location in your project to create the scaffolded view class, this can be the root folder, but would be best in a subfolder such as 'Maintain'. Each scaffolded view will be created in this folder under a view class subfolder (which can now be named different to the model).

Select Add, New Scaffolded Item. From the Add Scaffold dialog, select the Maximise Web Forms Scaffolder and click the Add button.

![Add New Scaffolded Item](/READMEImages/AddNewScaffoldedItem.png "Add, New Scaffolded Item")

Use the Add Web Forms Pages dialog to set several important options for the scaffolder. Select a model class, an Entity Framework data context, the view class name, Async support, master page, client validation.

![Add Web Forms Pages Dialog](/READMEImages/AddWebFormsPages2.png "Add Web Forms Pages Dialog")

After you click the Add button, the Maximise Web Forms Scaffolder adds a new folder, named after the 'view class' which contains Default.aspx, Delete.aspx, Details.aspx, Edit.aspx, and Insert.aspx pages.

## Working with Associated Entities

Associated Entities and Enumerations work as per exist project.  (https://github.com/Superexpert/WebFormsScaffolding)


## Customizing the Generated Content

The Maximise Web Forms Scaffolder uses Dynamic Data templates. If you want to customize the appearance of the pages generated by the Scaffolder then you can modify the Dynamic Data Field templates. Learn more about Dynamic Data at http://msdn.microsoft.com/en-us/library/cc488545.aspx  

The project supports filters for Enumeration and Associated Entities, these can also be customised using the DynamicData/Filters templates.

The Maximise Web Forms Scaffolder takes advantage of Bootstrap for styling the pages. You aren't required to use Bootstrap with the Maximise Web Forms Scaffolder. However, if you don't use Bootstrap then you are responsible for creating your own Cascading Style Sheet. Learn more about Bootstrap at http://getbootstrap.com/   

## Client Validation

The tool supports client validation (in addtional to server side) using the model validation annotated attributes.  You can select either the use of HTML5 validation or 'MS Unobtrusive Validation Library' when scaffolding.  The Unobtrusive validation is best if you wish to support pre-HTML5 web browsers.  You will need to install the Unobtrusive library into your project from Nuget, if required.

![HTML5 Client Validation](/READMEImages/ClientValidation1.png "HTML5 Client Validation")


## Samples

The solution includes a smaple project called 'RandomSchool'.  The project includes 7 scaffolded forms based on an imaginany school system, they are build from the models provided and help demostrate the enhancements added to the project. A duplicate project is also included to show Aysnc db calls.

