﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Startup.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Startup.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press 1 for Part1.
        /// </summary>
        public static string ExecutePart1 {
            get {
                return ResourceManager.GetString("ExecutePart1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press 2 for Part2.
        /// </summary>
        public static string ExecutePart2 {
            get {
                return ResourceManager.GetString("ExecutePart2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Input: .
        /// </summary>
        public static string Input {
            get {
                return ResourceManager.GetString("Input", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press Q to Quit.
        /// </summary>
        public static string Quit {
            get {
                return ResourceManager.GetString("Quit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press R to re-run.
        /// </summary>
        public static string ReRun {
            get {
                return ResourceManager.GetString("ReRun", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press D to return to the day selection.
        /// </summary>
        public static string ReturnToDaySelection {
            get {
                return ResourceManager.GetString("ReturnToDaySelection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press P to return to the part selection.
        /// </summary>
        public static string ReturnToPartSelection {
            get {
                return ResourceManager.GetString("ReturnToPartSelection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press Y to return to the year selection.
        /// </summary>
        public static string ReturnToYearSelection {
            get {
                return ResourceManager.GetString("ReturnToYearSelection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press {0} for {1}.
        /// </summary>
        public static string YearSelectionFmt {
            get {
                return ResourceManager.GetString("YearSelectionFmt", resourceCulture);
            }
        }
    }
}