﻿#pragma checksum "..\..\AddNewUser.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7236481F078CD91FE0DC7C3364ECB1A8"
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


namespace Pairs {
    
    
    /// <summary>
    /// AddNewUser
    /// </summary>
    public partial class AddNewUser : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\AddNewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NumeUser;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\AddNewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock labelNumeUser;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\AddNewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNewUserOK;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\AddNewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNewUserCancel;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\AddNewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SelectieImagine;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\AddNewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelectPoza;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\AddNewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PrevizualizarePoza;
        
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
            System.Uri resourceLocater = new System.Uri("/Pairs;component/addnewuser.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddNewUser.xaml"
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
            this.NumeUser = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.labelNumeUser = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.btnNewUserOK = ((System.Windows.Controls.Button)(target));
            
            #line 8 "..\..\AddNewUser.xaml"
            this.btnNewUserOK.Click += new System.Windows.RoutedEventHandler(this.btnNewUserOK_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnNewUserCancel = ((System.Windows.Controls.Button)(target));
            
            #line 9 "..\..\AddNewUser.xaml"
            this.btnNewUserCancel.Click += new System.Windows.RoutedEventHandler(this.btnNewUserCancel_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SelectieImagine = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btnSelectPoza = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\AddNewUser.xaml"
            this.btnSelectPoza.Click += new System.Windows.RoutedEventHandler(this.btnSelectPoza_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PrevizualizarePoza = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

