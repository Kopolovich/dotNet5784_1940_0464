﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for BindablePasswordBox.xaml
/// </summary>
public partial class BindablePasswordBox : UserControl
{
    public BindablePasswordBox()
    {
        InitializeComponent();
    }

    public string Password
    {
        get { return (string)GetValue(PasswordProperty); }
        set { SetValue(PasswordProperty, value); }
    }
    // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox), new PropertyMetadata("", PasswordPropertyChange));

    private static void PasswordPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if(d is BindablePasswordBox)
        {
            ((BindablePasswordBox)d).UpdatePassword();
        }
    }

    private void UpdatePassword()
    {
        passwordBox.Password = Password;
    }

    private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        Password = ((PasswordBox)sender).Password;
    }
}
