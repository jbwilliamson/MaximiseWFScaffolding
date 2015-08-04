using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSLangProj;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Microsoft.AspNet.Scaffolding.MaxWebForms.Utils
{
    internal class VisualStudioUtils
    {
        private DTE2 _dte;

        internal VisualStudioUtils()
        {
            // initialize DTE object -- the top level object for working with Visual Studio
            this._dte = Package.GetGlobalService(typeof(DTE)) as DTE2;
        }

        internal void InstallReference(Project project)
        {
            bool dllFound = false;
            bool xmlFound = false;

            if (project == null)
            {
                throw new NullReferenceException("project");
            }

            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string dllFileName = Path.Combine(assemblyFolder, "ScaffoldFilter.dll");
            dllFileName = dllFileName.Substring(6, (dllFileName.Length - 6));

            VSProject vsProject = project.Object as VSProject;
            if (vsProject != null)
            {
                foreach (Reference r in vsProject.References)
                {
                    if (r.Name == "ScaffoldFilter")
                    {
                        dllFound = true;
                        break;
                    }
                }

                if (!dllFound)
                {
                    try
                    {
                        vsProject.References.Add(dllFileName);
                    }
                    catch
                    {
                        throw new InvalidOperationException(Resources.WebFormsScaffolder_AssemblyRequired);
                    }
                }
            }

            ProjectItem cfg  = project.ProjectItems.Item("web.config");
            System.Xml.XmlDocument dom;
            dom = new System.Xml.XmlDocument();

            dom.Load(@cfg.Properties.Item("FullPath").Value.ToString());

            XmlNode root = dom.SelectSingleNode("//system.web//pages//controls");
            if (root == null || root.ChildNodes.Count == 0)
            {
                return;
            }

            xmlFound = false;
            foreach (XmlNode xn in root.ChildNodes)
            {
                if (xn.Name == "add")
                {
                    foreach(XmlAttribute xmla in xn.Attributes)
                    {
                        if (xmla.Name == "tagPrefix" && xmla.InnerText == "scf")
                        {
                            xmlFound = true;
                            break;
                        }
                    }
                }
            }

            if (!xmlFound)
            {
                XmlNode nodeAdd = dom.CreateElement("add");

                XmlAttribute nodetagPrefix = dom.CreateAttribute("tagPrefix");
                nodetagPrefix.InnerText = "scf";
                XmlAttribute nodeassembly = dom.CreateAttribute("assembly");
                nodeassembly.InnerText = "ScaffoldFilter";
                XmlAttribute nodenamespace = dom.CreateAttribute("namespace");
                nodenamespace.InnerText = "ScaffoldFilter";

                nodeAdd.Attributes.Append(nodetagPrefix);
                nodeAdd.Attributes.Append(nodeassembly);
                nodeAdd.Attributes.Append(nodenamespace);
                root.AppendChild(nodeAdd);

                try
                {
                    dom.Save(@cfg.Properties.Item("FullPath").Value.ToString());
                }
                catch
                {
                    throw new InvalidOperationException(Resources.WebFormsScaffolder_ConfigRefRequired);
                }
            }
        }

        internal void BuildProject(Project project)
        {
            var solutionConfiguration = _dte.Solution.SolutionBuild.ActiveConfiguration.Name;
            if (project == null)
            {
                throw new NullReferenceException("project");
            }

            _dte.Solution.SolutionBuild.BuildProject(solutionConfiguration, project.FullName, true);
        }
    }
}
