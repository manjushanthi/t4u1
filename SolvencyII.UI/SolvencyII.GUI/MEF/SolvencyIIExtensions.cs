using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows.Forms;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Domain;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Loggers;
using SolvencyII.UI.Shared.UserControls;

namespace SolvencyII.GUI.MEF
{
    /// <summary>
    /// MEF implementation - currently not used for main template lookup.
    /// It was found to be slow with the large dll's containing templates.
    /// For the sake of speed reflections is now being used on the referenced dlls.
    /// Note that MEF will still superseed the reflection lookup so with a correctly 
    ///  configured  dll in the correct place with the correctly named template, with 
    ///  the right version number - it will get used instead.
    /// (The correct path is the exe path \Extensions.)
    /// </summary>
    internal class SolvencyIIExtensions
    {
        // Declaration of control collections used in MEF
        [ImportMany(AllowRecomposition = true, RequiredCreationPolicy = CreationPolicy.NonShared)]
        private List<ISolvencyUserControl> _userControls;
        [ImportMany(AllowRecomposition = true, RequiredCreationPolicy = CreationPolicy.NonShared)]
        private List<ISolvencyOpenUserControl> _openUserControls;

        public event GenericDelegates.StringResponse Finished;
        private void OnFinished(string msg) { if (Finished != null) Finished(msg); }

        #region Constrstuction

        public SolvencyIIExtensions(GenericDelegates.StringResponse finishedMefComposed)
        {
            Finished += finishedMefComposed;
            Compose();
            if (_userControls == null) _userControls = new List<ISolvencyUserControl>();
            if(_openUserControls == null) _openUserControls = new List<ISolvencyOpenUserControl>();

            PopulateTheReferencedAssemblies();
        }

        /// <summary>
        /// Starting of MEF interrogation
        /// </summary>
        private void Compose()
        {
            //Asynchronous call to the method
            delComposeAsync compos = new delComposeAsync(ComposeAsync);
            compos.BeginInvoke(null, null);

        }
        public delegate void delComposeAsync();
        private bool IsComposingParts = true;

        private void PopulateTheReferencedAssemblies()
        {
            // The only reason the lines below are here is to ensure the jit compiler loads them.
            // This is needed in the SolvencyIIExtensions.GetControlByTableVID where reflection is used.
            //#if (!FOR_UT)
            //    Extensibility.EBA.HeartBeat.Pulse();
            //Extensibility.SOL2.HeartBeat.Pulse();
            //#endif

            //Extensibility.SOL2_Preparatory.HeartBeat.Pulse();

#if (FOR_NCA)
            Extensibility_SOL2.HeartBeat.Pulse();
            Extensibility.SOL2_Preparatory.HeartBeat.Pulse();
#elif  (FOR_UT)
            Extensibility.SOL2_Preparatory.HeartBeat.Pulse();
#else
#error "Compilation variable not set for FOR_NCA nor FOR_UT";
#endif

        }

        /// <summary>
        /// Async Interrogation of MEF libraries.
        /// The composition is from the folder the exe is in with the sub directory .\Extensions.
        /// </summary>
        private void ComposeAsync()
        {
            // Here we populate the _userControls from the DLLs found in the ExecutingAssembly and those in 
            // the executing assembly folder.
            var catalog = new AggregateCatalog();

            string location = Path.GetDirectoryName(Application.ExecutablePath);
            location = Path.Combine(location, "Extensions");
            if (!Directory.Exists(location)) Directory.CreateDirectory(location);

            catalog.Catalogs.Add(new DirectoryCatalog(location));
            var container = new CompositionContainer(catalog);

            string msg = "";
            try
            {
                container.ComposeParts(this);
            }
            catch (Exception e)
            {
                msg = string.Format("{0}\n{1}\n\n{2}", LanguageLabels.GetLabel(76, "Unfortunately there was a problem loading the extensions.\nPlease check the files in the folder:"), location, e.Message);
            }
            IsComposingParts = false;

            OnFinished(msg);
        }

        #endregion

        /// <summary>
        /// Locates a UserControlBase (template) using first MEF and in it fails Reflections
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        public object GetControlByTableVID(TreeItem selectedItem)
        {
            WaitIfNeeded();
            
            // Try MEF firstly so all user updated templates will take precidence.
            ISolvencyUserControl res = _userControls.Where(c => c.TableVID == selectedItem.TableID && c.FrameworkCode == selectedItem.FrameworkCode).OrderByDescending(c => c.Version).FirstOrDefault();
            if (res != null)
            {
                return res.Create;
            }

            // We have not found the control in MEF so use Reflection on referenced dlls.
            try
            {
                var result = ReferencedLookup(selectedItem, false);
                if (result != null)
                {
                    return Activator.CreateInstance(result);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(eSeverity.Error, "MEF", e.ToString());
                // Needed to drop out of the thread;
                return null;
            }

            // Its not referenced directly
            return null;
        }

        /// <summary>
        /// Locates a UserControlBase (template) using only Reflections
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        public UserControlBase GetOpenSubControlByTableVID(TreeItem selectedItem)
        {
            WaitIfNeeded();

            var result = ReferencedLookup(selectedItem, true);
            if (result != null)
            {
                try
                {
                    return (UserControlBase)Activator.CreateInstance(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            // Its not referenced directly
            return null;
        }

        /// <summary>
        /// Locates a OpenUserControlBase2 (open template) using first MEF and in it fails Reflections
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        public OpenUserControlBase2 GetOpenControlByTableVID(TreeItem selectedItem)
        {
            WaitIfNeeded();

            // Try MEF firstly so all user updated templates will take precidence.
            ISolvencyOpenUserControl res = _openUserControls.Where(c => c.TableVID == selectedItem.TableID && c.FrameworkCode == selectedItem.FrameworkCode).OrderByDescending(c => c.Version).FirstOrDefault();
            if (res != null)
            {
                return (OpenUserControlBase2)res.Create;
            }

            // We have not found the control in MEF so use Reflection on referenced dlls.
            var result = ReferencedLookup(selectedItem, false);
            if (result != null)
            {
                try
                {
                    return (OpenUserControlBase2)Activator.CreateInstance(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            // Its not referenced directly
            return null;
        }

        /// <summary>
        /// Gather the required template from the associated extensibility libraries
        /// </summary>
        /// <param name="treeItem"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        private Type ReferencedLookup(TreeItem treeItem, bool controlName)
        {
            // See if the class is referenced directly
            string className = treeItem.GetClassName(controlName);


            Type result;
            
            switch (StaticSettings.DbType)
            {
                case DbType.SolvencyII:
                    result = ReferencedDLLLookup("SolvencyII.Extensibility_SOL2", className);
                    break;
                case DbType.CRDIV:
                    result = ReferencedDLLLookup("SolvencyII.Extensibility_EBA", className);
                    break;
                case DbType.SolvencyII_Preparatory:
                    result = ReferencedDLLLookup("SolvencyII.Extensibility_SOL2_Prep", className);  
                    break;
                default:
                    result = ReferencedDLLLookup("SolvencyII.Extensibility_SOL2", className);
                    if (result == null) result = ReferencedDLLLookup("SolvencyII.Extensibility_EBA", className);
                    if (result == null) result = ReferencedDLLLookup("SolvencyII.Extensibility_SOL2_Prep", className);
                    break;
            }
            return result;
        }

        /// <summary>
        /// The MEF can take sometime to setup a waiting state is required
        /// </summary>
        private void WaitIfNeeded()
        {
            while (IsComposingParts)
            {
                System.Threading.Thread.Sleep(50);
                //It gives some respovines to the screen when loading.
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Relection lookup
        /// </summary>
        /// <param name="assembleName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private Type ReferencedDLLLookup(string assembleName, string className)
        {
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetReferencedAssemblies().FirstOrDefault(a => a.Name == assembleName);
            if (assemblyName != null)
            {
                Assembly assemblyTemp = Assembly.Load(assemblyName);
                if (assemblyTemp != null)
                {
                    try
                    {
                        var myClass = assemblyTemp.GetTypes().FirstOrDefault(t => t.Name == className);
                        if (myClass != null)
                        {
                            return myClass;
                        }

                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        Console.WriteLine(ex);
                        // If you experiencing Reflection exceptions here 
                        // Ensure the references are correct to the Solvency.Contracts.dll
                        // Differing Interface references can cause this problem.

                        //foreach (var item in ex.LoaderExceptions)
                        //{
                        //    MessageBox.Show(item.Message.ToString());
                        //}
                        return null;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return null;
                    }
                }
            }
            return null;
        }

    }
}
