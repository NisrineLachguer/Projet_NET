﻿#pragma checksum "..\..\..\..\Views\ClientsControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A6936E7EB3877E047CE62C0555EC34C76EFD81B5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace WpfApp1 {
    
    
    /// <summary>
    /// ClientsControl
    /// </summary>
    public partial class ClientsControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 85 "..\..\..\..\Views\ClientsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameFilterTextBox;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\Views\ClientsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailFilterTextBox;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\Views\ClientsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SortByComboBox;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\Views\ClientsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton AscendingRadioButton;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\Views\ClientsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton DescendingRadioButton;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\Views\ClientsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AjouterBtn;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\Views\ClientsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ModifierBtn;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\Views\ClientsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SupprimerBtn;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\Views\ClientsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ClientsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\..\Views\ClientsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TtlClientsLbl;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Gestion_hotel;component/views/clientscontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ClientsControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NameFilterTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.EmailFilterTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.SortByComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.AscendingRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.DescendingRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            
            #line 93 "..\..\..\..\Views\ClientsControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FilterButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 94 "..\..\..\..\Views\ClientsControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ResetFiltersButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.AjouterBtn = ((System.Windows.Controls.Button)(target));
            
            #line 99 "..\..\..\..\Views\ClientsControl.xaml"
            this.AjouterBtn.Click += new System.Windows.RoutedEventHandler(this.AjouterBtn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ModifierBtn = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\..\..\Views\ClientsControl.xaml"
            this.ModifierBtn.Click += new System.Windows.RoutedEventHandler(this.ModifierBtn_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.SupprimerBtn = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\..\..\Views\ClientsControl.xaml"
            this.SupprimerBtn.Click += new System.Windows.RoutedEventHandler(this.SupprimerBtn_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ClientsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 12:
            this.TtlClientsLbl = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

