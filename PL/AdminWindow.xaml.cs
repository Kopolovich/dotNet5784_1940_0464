﻿using PL.Engineer;
using PL.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace PL;

/// <summary>
/// Interaction logic for AdminWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public AdminWindow()
    {
        InitializeComponent();
    }

    private void Button_Click_ShowEngineerListWindow(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }

    private void Button_Click_ShowTaskListWindow(object sender, RoutedEventArgs e)
    {
        new TaskListWindow().Show();
    }

    private void Button_Click_CreateSchedule(object sender, RoutedEventArgs e)
    {
        
        try
        {
            DateTime finishDate =  s_bl.CreateProjectSchedule(DateTime.Now);
            MessageBox.Show($"Scheduled start dates have been assigned to all tasks!\nplanned date to complete project: {finishDate.ToShortDateString()}  \nGoodLuck!", "", MessageBoxButton.OK);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "", MessageBoxButton.OK);
        }
    }

    private void Button_Click_ShowGanttWindow(object sender, RoutedEventArgs e)
    {
        if(s_bl.GetProjectStatus() == BO.ProjectStatus.InPlanning)
            MessageBox.Show("Can not show Gantt chart before project schedule is created", "", MessageBoxButton.OK);
        else   
            new GanttChartWindow().Show();
    }
}
