﻿#pragma checksum "..\..\DisplayFileAndFolder.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "15FB30841363E1FA6DD7AFF237BE34A8823AC19ADEF66E64DA9F754AC887B179"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ImageGallery_WPF_Exam {
    
    
    /// <summary>
    /// DisplayFileAndFolder
    /// </summary>
    public partial class DisplayFileAndFolder : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearch;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbSearchType;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearch;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbSearchHistory;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClearHistory;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDisplay;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbTypeFile;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewItems;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miOpen;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miDelete;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miConvertFormat;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miCopy;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miCut;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miPaste;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgSelectedIcon;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView treeViewFileStructure;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\DisplayFileAndFolder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgFileContent;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ImageGallery-WPF-Exam;component/displayfileandfolder.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DisplayFileAndFolder.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtSearch = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.cmbSearchType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.btnSearch = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\DisplayFileAndFolder.xaml"
            this.btnSearch.Click += new System.Windows.RoutedEventHandler(this.btnSearch_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cmbSearchHistory = ((System.Windows.Controls.ComboBox)(target));
            
            #line 31 "..\..\DisplayFileAndFolder.xaml"
            this.cmbSearchHistory.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbSearchHistory_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnClearHistory = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\DisplayFileAndFolder.xaml"
            this.btnClearHistory.Click += new System.Windows.RoutedEventHandler(this.btnClearHistory_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtDisplay = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.cmbTypeFile = ((System.Windows.Controls.ComboBox)(target));
            
            #line 47 "..\..\DisplayFileAndFolder.xaml"
            this.cmbTypeFile.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbTypeFile_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.listViewItems = ((System.Windows.Controls.ListView)(target));
            
            #line 53 "..\..\DisplayFileAndFolder.xaml"
            this.listViewItems.DragOver += new System.Windows.DragEventHandler(this.listViewItems_DragOver);
            
            #line default
            #line hidden
            
            #line 54 "..\..\DisplayFileAndFolder.xaml"
            this.listViewItems.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.listViewItems_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 56 "..\..\DisplayFileAndFolder.xaml"
            this.listViewItems.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listViewItems_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 57 "..\..\DisplayFileAndFolder.xaml"
            this.listViewItems.DragEnter += new System.Windows.DragEventHandler(this.listViewItems_DragEnter);
            
            #line default
            #line hidden
            
            #line 58 "..\..\DisplayFileAndFolder.xaml"
            this.listViewItems.Drop += new System.Windows.DragEventHandler(this.listViewItems_Drop);
            
            #line default
            #line hidden
            
            #line 59 "..\..\DisplayFileAndFolder.xaml"
            this.listViewItems.MouseMove += new System.Windows.Input.MouseEventHandler(this.listViewItems_MouseMove);
            
            #line default
            #line hidden
            
            #line 60 "..\..\DisplayFileAndFolder.xaml"
            this.listViewItems.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListViewFiles_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.miOpen = ((System.Windows.Controls.MenuItem)(target));
            
            #line 67 "..\..\DisplayFileAndFolder.xaml"
            this.miOpen.Click += new System.Windows.RoutedEventHandler(this.miOpen_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 68 "..\..\DisplayFileAndFolder.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.miInsert_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.miDelete = ((System.Windows.Controls.MenuItem)(target));
            
            #line 69 "..\..\DisplayFileAndFolder.xaml"
            this.miDelete.Click += new System.Windows.RoutedEventHandler(this.miDelete_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.miConvertFormat = ((System.Windows.Controls.MenuItem)(target));
            
            #line 70 "..\..\DisplayFileAndFolder.xaml"
            this.miConvertFormat.Click += new System.Windows.RoutedEventHandler(this.miConvertFormat_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.miCopy = ((System.Windows.Controls.MenuItem)(target));
            
            #line 71 "..\..\DisplayFileAndFolder.xaml"
            this.miCopy.Click += new System.Windows.RoutedEventHandler(this.miCopy_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.miCut = ((System.Windows.Controls.MenuItem)(target));
            
            #line 72 "..\..\DisplayFileAndFolder.xaml"
            this.miCut.Click += new System.Windows.RoutedEventHandler(this.miCut_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.miPaste = ((System.Windows.Controls.MenuItem)(target));
            
            #line 73 "..\..\DisplayFileAndFolder.xaml"
            this.miPaste.Click += new System.Windows.RoutedEventHandler(this.miPaste_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.imgSelectedIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 17:
            this.treeViewFileStructure = ((System.Windows.Controls.TreeView)(target));
            return;
            case 18:
            this.imgFileContent = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

