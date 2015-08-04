using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using EnvDTE;
using Microsoft.AspNet.Scaffolding.EntityFramework;
using Microsoft.AspNet.Scaffolding.NuGet;
using Microsoft.AspNet.Scaffolding.MaxWebForms.UI;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using Microsoft.AspNet.Scaffolding.MaxWebForms.Utils;
using System.IO;
using EnvDTE80;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.ComponentModel;
using ScaffoldFilter;

namespace Microsoft.AspNet.Scaffolding.MaxWebForms.Scaffolders
{
    // This class performs all of the work of scaffolding. The methods are executed in the
    // following order:
    // 1) ShowUIAndValidate() - displays the Visual Studio dialog for setting scaffolding options
    // 2) Validate() - validates the model collected from the dialog
    // 3) GenerateCode() - if all goes well, generates the scaffolding output from the templates
    public class MaximiseWFScaffolding : CodeGenerator
    {
        private WebFormsCodeGeneratorViewModel _codeGeneratorViewModel;

        internal MaximiseWFScaffolding(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {

        }

        // Shows the Visual Studio dialog that collects scaffolding options
        // from the user.
        // Passing the dialog to this method so that all scaffolder UIs
        // are modal is still an open question and tracked by bug 578173.
        public override bool ShowUIAndValidate()
        {
            _codeGeneratorViewModel = new WebFormsCodeGeneratorViewModel(Context);

            WebFormsScaffolderDialog window = new WebFormsScaffolderDialog(_codeGeneratorViewModel);
            bool? isOk = window.ShowModal();

            if (isOk == true)
            {
                Validate();
            }

            return (isOk == true);
        }

        // Validates the model returned by the Visual Studio dialog.
        // We always force a Visual Studio build so we have a model
        // that we can use with the Entity Framework.
        private void Validate()
        {
            CodeType modelType = _codeGeneratorViewModel.ModelType.CodeType;
            ModelType dbContextType = _codeGeneratorViewModel.DbContextModelType;
            string dbContextTypeName = (dbContextType != null)
                ? dbContextType.TypeName
                : null;

            if (modelType == null)
            {
                throw new InvalidOperationException(Resources.WebFormsScaffolder_SelectModelType);
            }

            if (dbContextType == null || String.IsNullOrEmpty(dbContextTypeName))
            {
                throw new InvalidOperationException(Resources.WebFormsScaffolder_SelectDbContextType);
            }

            // always force the project to build so we have a compiled
            // model that we can use with the Entity Framework
            var visualStudioUtils = new VisualStudioUtils();

            visualStudioUtils.InstallReference(Context.ActiveProject);

            visualStudioUtils.BuildProject(Context.ActiveProject);


            Type reflectedModelType = GetReflectionType(modelType.FullName);
            if (reflectedModelType == null)
            {
                throw new InvalidOperationException(Resources.WebFormsScaffolder_ProjectNotBuilt);
            }
        }

        // Top-level method that generates all of the scaffolding output from the templates.
        // Shows a busy wait mouse cursor while working.
        public override void GenerateCode()
        {
            var project = Context.ActiveProject;
            var selectionRelativePath = GetSelectionRelativePath();

            if (_codeGeneratorViewModel == null)
            {
                throw new InvalidOperationException(Resources.WebFormsScaffolder_ShowUIAndValidateNotCalled);
            }

            Cursor currentCursor = Mouse.OverrideCursor;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                GenerateCode(project, selectionRelativePath, this._codeGeneratorViewModel);
            }
            finally
            {
                Mouse.OverrideCursor = currentCursor;
            }
        }

        // Collects the common data needed by all of the scaffolded output and generates:
        // 1) Dynamic Data Field Templates
        // 2) Web Forms Pages
        private void GenerateCode(Project project, string selectionRelativePath, WebFormsCodeGeneratorViewModel codeGeneratorViewModel)
        {
            // Get Model Type
            var modelType = codeGeneratorViewModel.ModelType.CodeType;
            string firstfocusField = "";
            IDictionary<string, string> fieldGroups;
            IDictionary<string, string> shortNames;
            IDictionary<string, string> gridView;
 
            // Ensure the Data Context
            string dbContextTypeName = codeGeneratorViewModel.DbContextModelType.TypeName;
            IEntityFrameworkService efService = Context.ServiceProvider.GetService<IEntityFrameworkService>();
            ModelMetadata efMetadataUnOrdered = efService.AddRequiredEntity(Context, dbContextTypeName, modelType.FullName);

            ModelMetadata efMetadata = OrderMetaData(efMetadataUnOrdered, modelType);

            fieldGroups = FindFieldGroups(efMetadata, modelType);
            gridView = GridViewDisplay(efMetadata, modelType);
            shortNames = FindAnyShortNames(efMetadata, modelType);

            foreach (PropertyMetadata pma in efMetadataUnOrdered.Properties)
            {
                if (pma.Scaffold && !pma.IsAssociation && !pma.IsAutoGenerated && !pma.IsReadOnly && !pma.IsAssociation && !pma.IsPrimaryKey)
                {
                    firstfocusField = pma.PropertyName;
                    break;
                }
            }

            // Get the dbContext
            ICodeTypeService codeTypeService = GetService<ICodeTypeService>();
            CodeType dbContext = codeTypeService.GetCodeType(project, dbContextTypeName);

            // Get the dbContext namespace
            string dbContextNamespace = dbContext.Namespace != null ? dbContext.Namespace.FullName : String.Empty;

            // Ensure the Dynamic Data Field templates
            EnsureDynamicDataFieldTemplates(project, dbContextNamespace, dbContextTypeName, efMetadata, 
                codeGeneratorViewModel.UseAsyncRepository, 
                codeGeneratorViewModel.ViewClassName, 
                codeGeneratorViewModel.UseClientSideValidation, 
                codeGeneratorViewModel.UseHTML5, 
                codeGeneratorViewModel.UseUnobtrusiveJSLibrary);

            // Ensure the Dynamic Data User Control templates
            EnsureDynamicDataUserControlTemplates(project, dbContextNamespace, dbContextTypeName);

            EnsureDynamicFilterTemplates(project, dbContextNamespace, dbContextTypeName, efMetadata,
                codeGeneratorViewModel.UseAsyncRepository,
                codeGeneratorViewModel.ViewClassName);

            // Add Repository classes
            AddRepositoryClass(
                project,
                selectionRelativePath,
                dbContextNamespace,
                dbContextTypeName,
                modelType,
                codeGeneratorViewModel.UseAsyncRepository,
                codeGeneratorViewModel.ViewClassName,
                efMetadata,
                codeGeneratorViewModel.OverwriteViews
            );

            // Add Extension class
            AddExtensionClass(
                project,
                selectionRelativePath,
                dbContextNamespace,
                dbContextTypeName,
                modelType,
                codeGeneratorViewModel.ViewClassName,
                efMetadata,
                codeGeneratorViewModel.OverwriteViews
            );


            // Add Web Forms Pages from Templates
            AddWebFormsPages(
                project, 
                selectionRelativePath,
                dbContextNamespace,
                dbContextTypeName,
                modelType,
                codeGeneratorViewModel.ViewClassName,
                efMetadata, 
                fieldGroups,
                shortNames,
                gridView,
                codeGeneratorViewModel.UseMasterPage,
                codeGeneratorViewModel.UseAsyncRepository,
                codeGeneratorViewModel.UseClientSideValidation, 
                codeGeneratorViewModel.UseHTML5, 
                codeGeneratorViewModel.UseUnobtrusiveJSLibrary,
                codeGeneratorViewModel.DesktopMasterPage, 
                codeGeneratorViewModel.DesktopPlaceholderId, 
                codeGeneratorViewModel.OverwriteViews,
                firstfocusField
           );
        }

        // A set of Dynamic User Control templates is created that support Bootstrap
        private void EnsureDynamicDataUserControlTemplates(Project project, string dbContextNamespace, string dbContextTypeName)
        {
            var fieldTemplates = new[] { 
                "LabelControl", "LabelControl.ascx.designer", "LabelControl.ascx",
            };
            var fieldTemplatesPath = "DynamicData\\UserTemplates";
            bool fileCreated;

            // Add the folder
            AddFolder(project, fieldTemplatesPath);

            foreach (var fieldTemplate in fieldTemplates)
            {
                var templatePath = Path.Combine(fieldTemplatesPath, fieldTemplate);
                var outputPath = Path.Combine(fieldTemplatesPath, fieldTemplate);

                fileCreated = AddFileFromTemplate(
                    project: project,
                    outputPath: outputPath,
                    templateName: templatePath,
                    templateParameters: new Dictionary<string, object>() 
                    {
                        {"DefaultNamespace", project.GetDefaultNamespace()},
                        {"DbContextNamespace", dbContextNamespace},
                        {"DbContextTypeName", dbContextTypeName}
                    },
                    skipIfExists: true);
            }
        }

        // A set of Repository classes for database access
        private void AddRepositoryClass(Project project, 
            string selectionRelativePath, 
            string dbContextNamespace, 
            string dbContextTypeName, 
            CodeType modelType,
            bool UseAsyncRepository,
            string viewClassName,
            ModelMetadata efMetadata,
            bool overwriteViews = true)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }

            // Get pluralized name used for web forms folder name
            string pluralizedModelName = efMetadata.EntitySetName;
           
            // Generate dictionary for related entities
            var relatedModels = GetRelatedModelDictionary(efMetadata);

            var fieldTemplatesPath = "DynamicData\\Repositories";
            AddFolder(project, fieldTemplatesPath);

            Dictionary<string, string> repositoryClasses;

            if (UseAsyncRepository)
            {
                repositoryClasses = new Dictionary<string, string>()
                    {
                        {"IRepositoryAsync", "IRepositoryAsync"},
                        {"RepositoryAsyncBase", "RepositoryAsyncBase"},
                        {"ClassRepositoryAsync", modelType.Name + "RepositoryAsync"}
                    };
            }
            else
            {
                repositoryClasses = new Dictionary<string, string>()
                    {
                        {"IRepository", "IRepository"},
                        {"RepositoryBase", "RepositoryBase"},
                        {"ClassRepository", modelType.Name + "Repository"}
                    };
            }

            // Now add each view
            foreach (KeyValuePair<string, string> repClass in repositoryClasses)
            {
                AddRepositoryTemplates(
                    templateName : repClass.Key, 
                    outputClassName: repClass.Value,
                    outputFolderPath: fieldTemplatesPath,
                    pluralizedModelName: pluralizedModelName,
                    modelType: modelType,
                    efMetadata: efMetadata,
                    relatedModels: relatedModels,
                    dbContextNamespace: dbContextNamespace,
                    dbContextTypeName: dbContextTypeName,
                    viewClassName : viewClassName,
                    overwrite: overwriteViews);
            }
        }

        // A set of Extension classes 
        private void AddExtensionClass(Project project,
            string selectionRelativePath,
            string dbContextNamespace,
            string dbContextTypeName,
            CodeType modelType,
            string viewClassName,
            ModelMetadata efMetadata,
            bool overwriteViews = true)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }

            // Get pluralized name used for web forms folder name
            string pluralizedModelName = efMetadata.EntitySetName;
            bool fileCreated;
            // Generate dictionary for related entities
            string modelNameSpace = modelType.Namespace != null ? modelType.Namespace.FullName : String.Empty;
            
            var fieldTemplatesPath = "DynamicData\\Extenders";
            AddFolder(project, fieldTemplatesPath);

            var extensionClasses = new Dictionary<string, string>() 
                    {
                        {"ControlExtensions", "ControlExtensions"},
                        {"ValidationConstants", "ValidationConstants"}
                    };

            var defaultNamespace = Context.ActiveProject.GetDefaultNamespace();

            foreach (KeyValuePair<string, string> extClass in extensionClasses)
            {
                var templatePath = Path.Combine(fieldTemplatesPath, extClass.Key);
                string outputPath = Path.Combine(fieldTemplatesPath, extClass.Value);

                fileCreated = AddFileFromTemplate(project,
                      outputPath,
                      templateName: templatePath,
                      templateParameters: new Dictionary<string, object>() 
                {
                    {"ModelName", modelType.Name}, // singular model name (e.g., Movie)
                    {"FullModelName", modelType.FullName}, // singular model name with namespace (e.g., Samples.Movie)
                    {"PluralizedModelName", pluralizedModelName}, // the plural model name (e.g. Movies)
                    {"ModelMetadata", efMetadata}, // the EF meta date for the model
                    {"DefaultNamespace", defaultNamespace}, // the default namespace of the project (used by VB)
                    {"ModelNamespace", modelNameSpace}, // the namespace of the model (e.g., Samples.Models)                        
                    {"DbContextNamespace", dbContextNamespace},
                    {"DbContextTypeName", dbContextTypeName}
                },
                skipIfExists: !overwriteViews);
            }
        }

        // A set of Dynamic Data filter templates is created that support Bootstrap
        private void EnsureDynamicFilterTemplates(Project project, string dbContextNamespace, string dbContextTypeName, ModelMetadata efMetadata,
            bool UseAsyncRepository, string viewClassName)
        {
            var filterTemplates = new[] { 
                "Enumeration", "Enumeration.ascx.designer", "Enumeration.ascx", "ForeignKey", "ForeignKey.ascx.designer", "ForeignKey.ascx"
            };

            //,"ForeignKey", "ForeignKey.ascx.designer", "ForeignKey.ascx"

            var filterTemplatesPath = "DynamicData\\Filters";
            string folderNamespace = "";
            bool fileCreated;
            // Get pluralized name used for web forms folder name
            string pluralizedModelName = efMetadata.EntitySetName;

            // Add the folder
            AddFolder(project, filterTemplatesPath);

            folderNamespace = GetDefaultNamespace() + "." + (viewClassName.Length == 0 ? pluralizedModelName : viewClassName);

            foreach (var filterTemplate in filterTemplates)
            {
                var templatePath = Path.Combine(filterTemplatesPath, filterTemplate);
                var outputPath = Path.Combine(filterTemplatesPath, filterTemplate);

                fileCreated = AddFileFromTemplate(
                    project: project,
                    outputPath: outputPath,
                    templateName: templatePath,
                    templateParameters: new Dictionary<string, object>() 
                    {
                        {"DefaultNamespace", project.GetDefaultNamespace()},
                        {"DbContextNamespace", dbContextNamespace},
                        {"DbContextTypeName", dbContextTypeName},
                        {"FolderNamespace", folderNamespace},
                        {"IsAsyncRepository", UseAsyncRepository}
                    },
                    skipIfExists: true);
            }
        }

        // A set of Dynamic Data field templates is created that support Bootstrap
        private void EnsureDynamicDataFieldTemplates(Project project, string dbContextNamespace, string dbContextTypeName, ModelMetadata efMetadata, 
            bool UseAsyncRepository, 
            string viewClassName, 
            bool useClientSideValidation, 
            bool useHTML5, 
            bool useUnobtrusiveJSLibrary)
        {
            var fieldTemplates = new[] { 
                "Boolean", "Boolean.ascx.designer", "Boolean.ascx",
                "Boolean_Edit", "Boolean_Edit.ascx.designer", "Boolean_Edit.ascx",
                "Children", "Children.ascx.designer", "Children.ascx",
                "Children_Insert", "Children_Insert.ascx.designer", "Children_Insert.ascx",
                "Date", "Date.ascx.designer", "Date.ascx",
                "Date_Edit", "Date_Edit.ascx.designer", "Date_Edit.ascx",
                "DateTime", "DateTime.ascx.designer", "DateTime.ascx",
                "DateTime_Edit", "DateTime_Edit.ascx.designer", "DateTime_Edit.ascx",
                "Decimal_Edit", "Decimal_Edit.ascx.designer", "Decimal_Edit.ascx",
                "EmailAddress", "EmailAddress.ascx.designer", "EmailAddress.ascx",
                "EmailAddress_Edit", "EmailAddress_Edit.ascx.designer", "EmailAddress_Edit.ascx",
                "Enumeration", "Enumeration.ascx.designer", "Enumeration.ascx",
                "Enumeration_Edit", "Enumeration_Edit.ascx.designer", "Enumeration_Edit.ascx",
                "ForeignKey", "ForeignKey.ascx.designer", "ForeignKey.ascx",
                "ForeignKey_Edit", "ForeignKey_Edit.ascx.designer", "ForeignKey_Edit.ascx",
                "Integer_Edit", "Integer_Edit.ascx.designer", "Integer_Edit.ascx",
                "MultilineText_Edit", "MultilineText_Edit.ascx.designer", "MultilineText_Edit.ascx",
                "PhoneNumber", "PhoneNumber.ascx.designer", "PhoneNumber.ascx",
                "PhoneNumber_Edit", "PhoneNumber_Edit.ascx.designer", "PhoneNumber_Edit.ascx",
                "Text", "Text.ascx.designer", "Text.ascx",
                "Text_Edit", "Text_Edit.ascx.designer", "Text_Edit.ascx",
                "Url", "Url.ascx.designer", "Url.ascx",
                "Url_Edit", "Url_Edit.ascx.designer", "Url_Edit.ascx"
            };

            bool fileCreated;
            var fieldTemplatesPath = "DynamicData\\FieldTemplates";
            string folderNamespace = "";

            // Get pluralized name used for web forms folder name
            string pluralizedModelName = efMetadata.EntitySetName;

            string clientSideValidation = "None";
            if (useClientSideValidation) {
                clientSideValidation = (useHTML5 == true ? "HTML5" : "Unobtrusive");
            }

            // Add the folder
            AddFolder(project, fieldTemplatesPath);

            folderNamespace = GetDefaultNamespace() + "." + (viewClassName.Length == 0 ? pluralizedModelName : viewClassName);

            foreach (var fieldTemplate in fieldTemplates)
            {
                var templatePath = Path.Combine(fieldTemplatesPath, fieldTemplate);
                var outputPath = Path.Combine(fieldTemplatesPath, fieldTemplate);

                fileCreated = AddFileFromTemplate(
                    project: project,
                    outputPath: outputPath,
                    templateName: templatePath,
                    templateParameters: new Dictionary<string, object>() 
                    {
                        {"DefaultNamespace", project.GetDefaultNamespace()},
                        {"DbContextNamespace", dbContextNamespace},
                        {"DbContextTypeName", dbContextTypeName},
                        {"FolderNamespace", folderNamespace},
                        {"IsAsyncRepository", UseAsyncRepository},
                        {"ClientSideValidation", clientSideValidation}
                    },
                    skipIfExists: true);
            }
        }

        // Generates all of the Web Forms Pages (Default Insert, Edit, Delete), 
        private void AddWebFormsPages(
            Project project, 
            string selectionRelativePath,
            string dbContextNamespace,
            string dbContextTypeName,
            CodeType modelType,
            string viewClassName,
            ModelMetadata efMetadata,
            IDictionary<string, string> fieldGroups,
            IDictionary<string, string> shortNames,
            IDictionary<string, string> gridView,
            bool useMasterPage,
            bool useAsyncRepository,
            bool useClientSideValidation, 
            bool useHTML5, 
            bool useUnobtrusiveJSLibrary,
            string masterPage = null,
            string desktopPlaceholderId = null,
            bool overwriteViews = true,
            string firstfocusField = ""
        )
        {
            string outputFolderPath = "";

            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }

            // Get pluralized name used for web forms folder name

            string pluralizedModelName = efMetadata.EntitySetName;

            // Generate dictionary for related entities
            var relatedModels = GetRelatedModelDictionary(efMetadata);

            var webForms = new[] { "Default", "Insert", "Edit", "Delete", "Details" };

            // Extract these from the selected master page : Tracked by 721707
            var sectionNames = new[] { "HeadContent", "MainContent" };

            // Add folder for views. This is necessary to display an error when the folder already exists but 
            // the folder is excluded in Visual Studio: see https://github.com/Superexpert/WebFormsScaffolding/issues/18
 
            outputFolderPath = Path.Combine(selectionRelativePath, (viewClassName.Length == 0 ? pluralizedModelName : viewClassName));
            AddFolder(Context.ActiveProject, outputFolderPath);

            // Now add each view
            foreach (string webForm in webForms)
            {
                AddWebFormsViewTemplates(
                    outputFolderPath: outputFolderPath,
                    pluralizedModelName: pluralizedModelName,
                    modelType: modelType,
                    efMetadata: efMetadata,
                    relatedModels: relatedModels,
                    fieldGroups : fieldGroups,
                    shortNames : shortNames,
                    gridView : gridView,
                    dbContextNamespace: dbContextNamespace,
                    dbContextTypeName: dbContextTypeName,
                    viewClassName : viewClassName,
                    webFormsName: webForm,
                    useMasterPage: useMasterPage,
                    useAsyncRepository : useAsyncRepository,
                    useClientSideValidation: useClientSideValidation,
                    useHTML5 : useHTML5,
                    useUnobtrusiveJSLibrary : useUnobtrusiveJSLibrary,
                    masterPage: masterPage,
                    sectionNames: sectionNames,
                    primarySectionName: desktopPlaceholderId,
                    overwrite: overwriteViews,
                    focusField: firstfocusField);
            }
        }

        private void AddWebFormsViewTemplates(
                                string outputFolderPath,
                                string pluralizedModelName,
                                CodeType modelType,
                                ModelMetadata efMetadata,
                                IDictionary<string, RelatedModelMetadata> relatedModels,
                                IDictionary<string, string> fieldGroups,
                                IDictionary<string, string> shortNames,
                                IDictionary<string, string> gridView,
                                string dbContextNamespace,
                                string dbContextTypeName,
                                string viewClassName,
                                string webFormsName,
                                bool useMasterPage,
                                bool useAsyncRepository,
                                bool useClientSideValidation, 
                                bool useHTML5,
                                bool useUnobtrusiveJSLibrary,
                                string masterPage = "",
                                string[] sectionNames = null,
                                string primarySectionName = "",
                                bool overwrite = false,
                                string focusField = ""
        )
        {
            var folderNamespace = "";
            
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }
            
            if (String.IsNullOrEmpty(webFormsName))
            {
                throw new ArgumentException(Resources.WebFormsViewScaffolder_EmptyActionName, "webFormsName");
            }

            // Generate unique code beside name (needed because VB does not namespace)
            var codeBesideName = GetUniqueCodeBesideName(Context.ActiveProject, webFormsName);

            PropertyMetadata primaryKey = efMetadata.PrimaryKeys.FirstOrDefault();

            string modelNameSpace = modelType.Namespace != null ? modelType.Namespace.FullName : String.Empty;
            string relativePath = outputFolderPath.Replace(@"\", @"/");

            var modelDisplayNames = GetDisplayNames(modelType);

            List<string> webFormsTemplates = new List<string>();
            webFormsTemplates.AddRange(new string[] { webFormsName, webFormsName + ".aspx", webFormsName + ".aspx.designer" });

            string clientSideValidation = "None";
            if (useClientSideValidation)
            {
                clientSideValidation = (useHTML5 == true ? "HTML5" : "Unobtrusive");
            }

            // Scaffold aspx page and code behind
            foreach (string webForm in webFormsTemplates)
            {
                Project project = Context.ActiveProject;
                var templatePath = Path.Combine("WebForms", webForm);
                string outputPath = Path.Combine(outputFolderPath, webForm);

                var defaultNamespace = Context.ActiveProject.GetDefaultNamespace();

                folderNamespace = GetDefaultNamespace() + "." + (viewClassName.Length == 0 ? pluralizedModelName : viewClassName);
      
                AddFileFromTemplate(project,
                    outputPath,
                    templateName: templatePath,
                    templateParameters: new Dictionary<string, object>() 
                    {
                        {"IsContentPage", useMasterPage}, // does this page have a master page?
                        {"MasterPageFile", masterPage ?? String.Empty}, // master page associated with this page
                        {"PrimarySectionName", primarySectionName}, // the main content section of a master page
                        
                        {"ModelName", modelType.Name}, // singular model name (e.g., Movie)
                        {"FullModelName", modelType.FullName}, // singular model name with namespace (e.g., Samples.Movie)
                        {"PluralizedModelName", pluralizedModelName}, // the plural model name (e.g. Movies)
                        {"ModelMetadata", efMetadata}, // the EF meta date for the model
                        {"RelatedModels", relatedModels}, // models related by association to the model
                        {"FieldGroups", fieldGroups}, // groups used to setup fieldsets
                        {"ShortNames", shortNames}, // use the short name from the display attribute for list view
                        {"GridView", gridView}, // use the custom defined attribute to include/exclude fields on the default record list

                        {"DefaultNamespace", defaultNamespace}, // the default namespace of the project (used by VB)
                        {"FolderNamespace", folderNamespace}, // the namespace of the current folder (used by C#)
                        {"ModelNamespace", modelNameSpace}, // the namespace of the model (e.g., Samples.Models)                        
                        {"CodeBesideName", codeBesideName}, // the Web Forms code beside class name (e.g., _Default)
                        {"PrimaryKeyName", primaryKey.PropertyName}, // primary key of model (e.g., Id)
                        {"PrimaryKeyType", primaryKey.ShortTypeName}, // short primary key type name (e.g., string)

                        {"RelativePath", relativePath}, // relative path of current page (e.g., /samples/movie)

                        {"DbContextNamespace", dbContextNamespace},
                        {"DbContextTypeName", dbContextTypeName},
                        {"ModelDisplayNames", modelDisplayNames},
                        {"FocusField", focusField},
                        {"IsAsyncRepository", useAsyncRepository},
                        {"ClientSideValidation", clientSideValidation}
                    },
                    skipIfExists: !overwrite);
            }
        }

        private void AddRepositoryTemplates(
                        string templateName,
                        string outputClassName,
                        string outputFolderPath,
                        string pluralizedModelName,
                        CodeType modelType,
                        ModelMetadata efMetadata,
                        IDictionary<string, RelatedModelMetadata> relatedModels,
                        string dbContextNamespace,
                        string dbContextTypeName,
                        string viewClassName,
                        bool overwrite = false)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }

            if (String.IsNullOrEmpty(templateName))
            {
                throw new ArgumentException(Resources.WebFormsViewScaffolder_EmptyActionName, "templateName");
            }

            if (String.IsNullOrEmpty(outputClassName))
            {
                throw new ArgumentException(Resources.WebFormsViewScaffolder_EmptyActionName, "outputClassName");
            }

            // Generate unique code beside name (needed because VB does not namespace)
            var codeBesideName = GetUniqueCodeBesideName(Context.ActiveProject, outputClassName);
            bool fileCreated;

            PropertyMetadata primaryKey = efMetadata.PrimaryKeys.FirstOrDefault();

            string modelNameSpace = modelType.Namespace != null ? modelType.Namespace.FullName : String.Empty;
            //var modelDisplayNames = GetDisplayNames(modelType);

            Project project = Context.ActiveProject;
            var templatePath = Path.Combine("DynamicData\\Repositories", templateName);
            string outputPath = Path.Combine(outputFolderPath, outputClassName);

            var defaultNamespace = Context.ActiveProject.GetDefaultNamespace();

            fileCreated = AddFileFromTemplate(project,
                outputPath,
                templateName: templatePath,
                templateParameters: new Dictionary<string, object>() 
                {
                    {"ModelName", modelType.Name}, // singular model name (e.g., Movie)
                    {"FullModelName", modelType.FullName}, // singular model name with namespace (e.g., Samples.Movie)
                    {"PluralizedModelName", pluralizedModelName}, // the plural model name (e.g. Movies)
                    {"ModelMetadata", efMetadata}, // the EF meta date for the model
                    {"RelatedModels", relatedModels}, // models related by association to the model

                    {"DefaultNamespace", defaultNamespace}, // the default namespace of the project (used by VB)
                    {"ModelNamespace", modelNameSpace}, // the namespace of the model (e.g., Samples.Models)
                    {"CodeBesideName", codeBesideName}, // the Web Forms code beside class name (e.g., _Default)
                    {"PrimaryKeyName", primaryKey.PropertyName}, // primary key of model (e.g., Id)
                    {"PrimaryKeyType", primaryKey.ShortTypeName}, // short primary key type name (e.g., string)

                    {"DbContextNamespace", dbContextNamespace},
                    {"DbContextTypeName", dbContextTypeName}
                    //{"ModelDisplayNames", modelDisplayNames}
                },
                skipIfExists: !overwrite);
        }

        // VB, unlike C#, does not namespace by folder. For this reason, we must generate a unique
        // code-beside name for each VB Web Form class. For example, there cannot be more than one
        // Insert class in the same VB project. Instead, we need Insert, Insert1, Insert2, ...
        private string GetUniqueCodeBesideName(Project project, string originalName)
        {
            // In VB, rename Default to _Default (because Default is a keyword)
            if (originalName == "Default" && ProjectLanguage.VisualBasic.Equals(Context.ActiveProject.GetCodeLanguage()))
            {
                originalName = "_Default";
            }

            var counter = 0;
            var currentName = originalName;

            ICodeTypeService codeTypeService = GetService<ICodeTypeService>();
            while (codeTypeService.GetAllCodeTypes(project).Any(c => c.Name == currentName))
            {
                counter++;
                currentName = originalName + counter.ToString();
            }
            return currentName;
        }

        // Called to ensure that the project was compiled successfully
        private Type GetReflectionType(string typeName)
        {
            return GetService<IReflectedTypesService>().GetType(Context.ActiveProject, typeName);
        }

        // We are just pulling in some dependent nuget packages
        // to meet "Web Application Project" experience in this change.
        // There are some open questions regarding the experience for
        // webforms scaffolder in the case of an empty project.
        // Those details need to be worked out and
        // depending on that, we would modify the list of packages below
        // or conditions which determine when they are installed etc.
        public override IEnumerable<NuGetPackage> Dependencies
        {
            get
            {
                return GetService<IEntityFrameworkService>().Dependencies;
            }
        }

        private TService GetService<TService>() where TService : class
        {
            return (TService)ServiceProvider.GetService(typeof(TService));
        }

        // Returns the relative path of the folder selected in Visual Studio or an empty 
        // string if no folder is selected.
        protected string GetSelectionRelativePath()
        {
            return Context.ActiveProjectItem == null ? String.Empty : ProjectItemUtils.GetProjectRelativePath(Context.ActiveProjectItem);
        }

        // If a Visual Studio folder is selected then returns the folder's namespace, otherwise
        // returns the project namespace.
        protected string GetDefaultNamespace()
        {
            return Context.ActiveProjectItem == null 
                ? Context.ActiveProject.GetDefaultNamespace() 
                : Context.ActiveProjectItem.GetDefaultNamespace();
        }

        // Create a dictionary that maps foreign keys to related models. We only care about associations
        // with a single key (so we can display in a DropDownList)
        protected IDictionary<string, RelatedModelMetadata> GetRelatedModelDictionary(ModelMetadata efMetadata)
        {
            var dict = new Dictionary<string, RelatedModelMetadata>();

            foreach (var relatedEntity in efMetadata.RelatedEntities)
            {
                if (relatedEntity.ForeignKeyPropertyNames.Count() == 1)
                {
                    dict[relatedEntity.ForeignKeyPropertyNames[0]] = relatedEntity;
                }
            }
            return dict;
        }

        // Create a mapping between property names and display names in case
        // the property is decorated with a DisplayAttribute
        protected IDictionary<string, string> GetDisplayNames(CodeType modelType)
        {
            var type = GetReflectionType(modelType.FullName);
            var lookup = new Dictionary<string, string>();

            foreach (PropertyInfo prop in type.GetProperties()) {
                var attr = (DisplayAttribute)prop.GetCustomAttribute(typeof(DisplayAttribute), true);
                var value = "";

                if (attr == null) {
                    // Added check on 'DisplayNameAttribute' for a full name
                    var dnaAttr = (DisplayNameAttribute)prop.GetCustomAttribute(typeof(DisplayNameAttribute), true);
                    value = dnaAttr != null && !String.IsNullOrWhiteSpace(dnaAttr.DisplayName) ? dnaAttr.DisplayName : prop.Name;
                }
                else {
                    value = attr != null && !String.IsNullOrWhiteSpace(attr.Name) ? attr.Name : prop.Name;
                }

                lookup.Add(prop.Name, value);
            }
            return lookup;
        }

        protected IDictionary<string, string> FindAnyShortNames(ModelMetadata efMetadata, CodeType modelType)
        {
            Dictionary<string, string> shortNames = new Dictionary<string, string>();
            var type = GetReflectionType(modelType.FullName);

            foreach (PropertyMetadata pma in efMetadata.Properties)
            {
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    if (pma.PropertyName == prop.Name)
                    {
                        var attr = (DisplayAttribute)prop.GetCustomAttribute(typeof(DisplayAttribute), true);
                        if (attr != null && attr.GetShortName() != null && attr.GetShortName().Length > 0)
                        {
                            shortNames.Add(pma.PropertyName, attr.GetShortName());
                        }
                        //else
                        //{
                        //    shortNames.Add(pma.PropertyName, pma.PropertyName);
                        //}
                    }
                }
            }

            return shortNames;
        }

        // Extract any group names for fields, these will be used to create fieldsets in the generated form
        protected IDictionary<string, string> FindFieldGroups(ModelMetadata efMetadata, CodeType modelType)
        {
            Dictionary<string, string> fieldGroups = new Dictionary<string, string>();

            var type = GetReflectionType(modelType.FullName);

            foreach (PropertyMetadata pma in efMetadata.Properties)
            {
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    if (pma.PropertyName == prop.Name)
                    {
                        var attr = (DisplayAttribute)prop.GetCustomAttribute(typeof(DisplayAttribute), true);
                        if (attr != null && attr.GetGroupName() != null && attr.GetGroupName().Length > 0)
                        {
                            fieldGroups.Add(pma.PropertyName, attr.GetGroupName());
                        }
                        else 
                        {
                            fieldGroups.Add(pma.PropertyName, "Form Group");
                        }
                    }
                }
            }

            return fieldGroups;
        }

        //// Extract any GridViewAttributes from model, will use this to determine what fields to displays on default page
        protected IDictionary<string, string> GridViewDisplay(ModelMetadata efMetadata, CodeType modelType)
        {
            Dictionary<string, string> gridDisplay = new Dictionary<string, string>();

            gridDisplay = CheckOnlyTrueGridView(efMetadata, modelType);
            if (gridDisplay.Count > 0)
            {
                gridDisplay = FindTrueGridView(efMetadata, modelType);
                return gridDisplay;
            }

            gridDisplay = FindAnyGridView(efMetadata, modelType);
            return gridDisplay;
        }

        private Dictionary<string, string> CheckOnlyTrueGridView(ModelMetadata efMetadata, CodeType modelType)
        {
            string gridType = "ScaffoldFilter.GridViewAttribute, ScaffoldFilter";

            Dictionary<string, string> gridDisplay = new Dictionary<string, string>();
            var type = GetReflectionType(modelType.FullName);
            Type gridView = GetReflectionType(gridType);

            foreach (PropertyMetadata pma in efMetadata.Properties)
            {
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    if (pma.PropertyName == prop.Name)
                    {
                        var attr = prop.GetCustomAttribute(gridView, true);
                        if (attr != null )
                        {
                            var gridViewAttr = Convert.ChangeType(attr, gridView);
                            if (gridViewAttr != null)
                            {
                                var result = gridView.InvokeMember("GetDisplay", BindingFlags.InvokeMethod, null, gridViewAttr, null);

                                if ((bool)result == true)
                                {
                                    gridDisplay.Add(pma.PropertyName, "true");
                                }
                            }
                        }
                    }
                }
            }

            return gridDisplay;
        }

        private Dictionary<string, string> FindAnyGridView(ModelMetadata efMetadata, CodeType modelType)
        {
            string gridType = "ScaffoldFilter.GridViewAttribute, ScaffoldFilter";

            Dictionary<string, string> gridDisplay = new Dictionary<string, string>();
            var type = GetReflectionType(modelType.FullName);
            Type gridView = GetReflectionType(gridType);

            foreach (PropertyMetadata pma in efMetadata.Properties)
            {
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    if (pma.PropertyName == prop.Name)
                    {
                        var attr = prop.GetCustomAttribute(gridView, true);

                        if (attr == null)
                        {
                            gridDisplay.Add(pma.PropertyName, "true");
                        }
                        else
                        {
                            var gridViewAttr = Convert.ChangeType(attr, gridView);
                            if (gridViewAttr == null)
                            {
                                gridDisplay.Add(pma.PropertyName, "true");
                            }
                            else
                            {
                                var result = gridView.InvokeMember("GetDisplay", BindingFlags.InvokeMethod, null, gridViewAttr, null);
                                if (result == null)
                                {
                                    gridDisplay.Add(pma.PropertyName, (bool)result == true ? "true" : "false");
                                }
                                else
                                {
                                    gridDisplay.Add(pma.PropertyName, "false");
                                }
                            }
                        }
                    }
                }
            }

            return gridDisplay;
        }

        private Dictionary<string, string> FindTrueGridView(ModelMetadata efMetadata, CodeType modelType)
        {
            string gridType = "ScaffoldFilter.GridViewAttribute, ScaffoldFilter";

            Dictionary<string, string> gridDisplay = new Dictionary<string, string>();
            var type = GetReflectionType(modelType.FullName);
            Type gridView = GetReflectionType(gridType);

            foreach (PropertyMetadata pma in efMetadata.Properties)
            {
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    if (pma.PropertyName == prop.Name)
                    {
                        var attr = prop.GetCustomAttribute(gridView, true);

                        if (attr == null)
                        {
                            gridDisplay.Add(pma.PropertyName, "false");
                        }
                        else
                        {
                            var gridViewAttr = Convert.ChangeType(attr, gridView);
                            if (gridViewAttr == null)
                            {
                                gridDisplay.Add(pma.PropertyName, "false");
                            }
                            else
                            {
                                var result = gridView.InvokeMember("GetDisplay", BindingFlags.InvokeMethod, null, gridViewAttr, null);
                                if (result != null)
                                {
                                    gridDisplay.Add(pma.PropertyName, (bool)result == true ? "true" : "false");
                                }
                                else
                                {
                                    gridDisplay.Add(pma.PropertyName, "false");
                                }
                            }
                        }
                    }
                }
            }

            return gridDisplay;
        }

        // Check for the use of 'Order' in 'DisplayAttribute' and use the value to sort the 
        // 'Properties' array in ModelMetadata
        protected ModelMetadata OrderMetaData(ModelMetadata efMetadataUnOrdered, CodeType modelType)
        {
            bool orderSet = false;
            var type = GetReflectionType(modelType.FullName);

            // Loop through proteries, find the related record in 'type' and then find the DisplayAttribute

            foreach (PropertyMetadata pma in efMetadataUnOrdered.Properties)
            {
                foreach(PropertyInfo prop in type.GetProperties())
                {
                    if (pma.PropertyName == prop.Name)
                    {
                        var attr = (DisplayAttribute)prop.GetCustomAttribute(typeof(DisplayAttribute), true);
                        if (attr != null && attr.GetOrder() != null && attr.GetOrder() > 0)
                        {
                            orderSet = true;
                            break;
                        }
                    }
                }
            }

            if (orderSet == false)
            {
                return efMetadataUnOrdered;
            }

            Array.Sort(efMetadataUnOrdered.Properties, delegate(PropertyMetadata prop1, PropertyMetadata prop2)
            {
                int orderx = 10000;
                int ordery = 10000;

                foreach (PropertyInfo propx in type.GetProperties())
                {
                    if (propx.Name == prop1.PropertyName)
                    {
                        var attrx = (DisplayAttribute)propx.GetCustomAttribute(typeof(DisplayAttribute), true);
                        if (attrx != null && attrx.GetOrder() != null && attrx.GetOrder() > 0)
                        {
                            orderx = (int)attrx.GetOrder();
                            break;
                        }
                    }
                }

                foreach (PropertyInfo propy in type.GetProperties())
                {
                    if (propy.Name == prop2.PropertyName)
                    {
                        var attry = (DisplayAttribute)propy.GetCustomAttribute(typeof(DisplayAttribute), true);
                        if (attry != null && attry.GetOrder() != null && attry.GetOrder() > 0)
                        {
                            ordery = (int)attry.GetOrder();
                            break;
                        }
                    }
                }

                return orderx.CompareTo(ordery);
            });
  
            return efMetadataUnOrdered;
        }
    }
}
