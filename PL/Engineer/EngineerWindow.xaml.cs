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
using System.Windows.Shapes;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public EngineerWindow(int Id = 0)
        {
            InitializeComponent();
            if (Id == 0)
                CurrentEngineer = new();
            else
                try
                {
                    CurrentEngineer = s_bl.Engineer.Read(Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"error", MessageBoxButton.OK);
                }
                
        }



        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentEngineer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(0));

        private void Button_Click_AddOrUpdateEngineer(object sender, RoutedEventArgs e)
        {
            Button? btn = sender! as Button;
            if (btn != null)
            {
                try
                {
                    if (btn.Content.ToString() == "Add")
                    {
                        s_bl.Engineer.Create(CurrentEngineer);
                    }

                    else
                    {
                        s_bl.Engineer.Update(CurrentEngineer);
                    }
                }
                catch(Exception ex)
                { 
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK );
                }

        
            }
        }
    }
}
