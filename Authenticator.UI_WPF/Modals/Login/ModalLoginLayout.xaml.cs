﻿using System.Windows;
using System.Windows.Controls;
using WPFLocalizeExtension.Providers;

namespace Authenticator.Components
{
    /// <summary>
    /// Interaction logic for PageLayout.xaml
    /// </summary>
    public partial class ModalLoginLayout : ContentControl
    {

        public int ModalWidth
        {
            get { return (int)GetValue(ModalWidthProperty); }
            set { SetValue(ModalWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModalWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModalWidthProperty =
            DependencyProperty.Register("ModalWidth", typeof(int), typeof(ModalLoginLayout), new PropertyMetadata(300));

        public string ModalTitle
        {
            get { return (string)GetValue(ModalTitleProperty); }
            set { SetValue(ModalTitleProperty, value); }
        }

        public static readonly DependencyProperty ModalTitleProperty =
            DependencyProperty.Register("ModalTitle", typeof(string), typeof(ModalLoginLayout), new PropertyMetadata(string.Empty));

        public string ModalSubTitle
        {
            get { return (string)GetValue(ModalSubTitleProperty); }
            set { SetValue(ModalSubTitleProperty, value); }
        }
        public static readonly DependencyProperty ModalSubTitleProperty =
            DependencyProperty.Register("ModalSubTitle", typeof(string), typeof(ModalLoginLayout), new PropertyMetadata(string.Empty));

        public string ModalNumber
        {
            get { return (string)GetValue(ModalNumberProperty); }
            set { SetValue(ModalNumberProperty, value); }
        }
        public static readonly DependencyProperty ModalNumberProperty =
            DependencyProperty.Register("ModalNumber", typeof(string), typeof(ModalLoginLayout), new PropertyMetadata(string.Empty));

        public string IconPath
        {
            get { 
                return (string)GetValue(IconPathProperty); 
            }
            set { 
                SetValue(IconPathProperty, value); 
            }
        }

        // Using a DependencyProperty as the backing store for IconPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconPathProperty =
            DependencyProperty.Register("IconPath", typeof(string), typeof(ModalLoginLayout), new PropertyMetadata(string.Empty));

        public ModalLoginLayout()
        {
            InitializeComponent();
            
            
        }
    }
}
