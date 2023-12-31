﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

namespace TrackerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        RecruitmentSystem recruitmentSystem = new RecruitmentSystem();

        public MainWindow()
        {

            InitializeComponent();
            // Add Initial Data to RecruitmentSystem
            recruitmentSystem.AddContractor(new Contractor("Cedric", "Anover", 45));
            recruitmentSystem.AddContractor(new Contractor("John", "Cena", 12));
            recruitmentSystem.AddContractor(new Contractor("Jack", "Ma", 200d));
            recruitmentSystem.AddContractor(new Contractor("John", "Wick", 50d));

            recruitmentSystem.AddJob(new Job("Data Scientist", "29/12/2023", 300000));
            recruitmentSystem.AddJob(new Job("Data Engineer", "5/11/2023", 70000));
            recruitmentSystem.AddJob(new Job("Programmer", "6/01/2024", 100000));
            recruitmentSystem.AddJob(new Job("Software Architect", "25/02/2024", 40000, completed:true));
            recruitmentSystem.AssignJob(recruitmentSystem.jobs[2], recruitmentSystem.contractors[0]);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            datagridContractor.ItemsSource = null;
            datagridJob.ItemsSource = null;

            datagridContractor.ItemsSource = recruitmentSystem.contractors;
            datagridJob.ItemsSource = recruitmentSystem.jobs;
        }

        private void datagridContractor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contractor selectedContractor = (Contractor)datagridContractor.SelectedItem;
            if (selectedContractor != null) // Sometimes Job is resulting in null
            {
                UpdateContractorGroupBox(selectedContractor);
            }
        }

        private void datagridJob_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Job selectedJob = (Job)datagridJob.SelectedItem;

            if (selectedJob != null) // Sometimes Job is resulting in null
            {
                UpdateJobGroupBox(selectedJob);
            }
        }


        private void UpdateContractorGroupBox(Contractor contractor)
        {
            txtbxFirstName.Text = contractor.FirstName;
            txtbxLastName.Text = contractor.LastName;

            if (contractor.StartDate != null)
            {
                //datepickerStartDate.DisplayDate = DateTime.Parse(contractor.StartDate.ToString());
                datepickerStartDate.Text = contractor.StartDate.ToString();
            }
            else
            {
                datepickerStartDate.Text = "";
            }

            sliderHourlyWage.Value = contractor.HourlyWage;
            chkbxIsAvailable.IsChecked = contractor.IsAvailable;
        }

        private void UpdateJobGroupBox(Job job)
        {
            txtbxTitle.Text = job.Title;
            datepickerJobDate.Text = job.Date.ToString();
            sliderCost.Value = job.Cost;
            chkbxCompleted.IsChecked = job.Completed;

            comboboxContractorAssigned.ItemsSource = recruitmentSystem.contractors;
            comboboxContractorAssigned.SelectedItem = job.ContractorAssigned;
        }

        private void sliderHourlyWage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelHourlyWage.Content = $"${sliderHourlyWage.Value:0.##}";
        }

        private void sliderCost_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelCost.Content = $"${sliderCost.Value:0.##}";
        }

        private void btnViewAvailableContractors_Click(object sender, RoutedEventArgs e)
        {
            datagridContractor.ItemsSource = recruitmentSystem.GetAvailableContractors();
            tabctrlTables.SelectedItem = tabitemContractor;
        }

        private void btnViewUnassignedJobs_Click(object sender, RoutedEventArgs e)
        {
            datagridJob.ItemsSource = recruitmentSystem.GetUnassignedJobs();
            tabctrlTables.SelectedItem = tabitemJob;
        }

        private void btnShowJobsByCostRange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                datagridJob.ItemsSource = recruitmentSystem.GetJobs(double.Parse(txtbxMinCost.Text), double.Parse(txtbxMaxCost.Text));
                tabctrlTables.SelectedItem = tabitemJob;
            }
            catch (Exception err) 
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void btnAddContractor_Click(object sender, RoutedEventArgs e)
        {

            Contractor newContractor;

            if (txtbxFirstName.Text.Trim().Length==0 || txtbxLastName.Text.Trim().Length == 0 || Math.Round(sliderHourlyWage.Value, 2)==0)
            {
                MessageBox.Show("Invalid Input", "Error", MessageBoxButton.OK);
                return;
            }

            try
            {

                newContractor = new Contractor(
                            txtbxFirstName.Text, 
                            txtbxLastName.Text, 
                            Math.Round(sliderHourlyWage.Value, 2),
                            datepickerStartDate.SelectedDate != null? DateOnly.Parse(datepickerStartDate.Text) : null,
                            chkbxIsAvailable.IsChecked != null? chkbxIsAvailable.IsChecked.Value : true
                    );
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK);
                return;
            }

            try
            {
                recruitmentSystem.AddContractor(newContractor); // Add the New Contractor
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK);
            }

        }

        private void btnRemoveContractor_Click(object sender, RoutedEventArgs e)
        {
            if (datagridContractor.SelectedItem == null)
            {
                MessageBox.Show("Please select a Contractor", "Warning", MessageBoxButton.OK);
                tabctrlTables.SelectedItem = tabitemContractor;
                return;
            }

            Contractor contractor = (Contractor)datagridContractor.SelectedItem;

            try
            {
                recruitmentSystem.RemoveContractor(contractor);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK);
            }
            finally
            {
                datagridContractor.ItemsSource = null;
                datagridContractor.ItemsSource = recruitmentSystem.contractors;
            }
            
        }

        private void btnUpdateContractor_Click(object sender, RoutedEventArgs e)
        {
            if (datagridContractor.SelectedItem == null) // Make sure user selected a contractor to be modified
            {
                MessageBox.Show("Please select a Contractor", "Warning", MessageBoxButton.OK);
                tabctrlTables.SelectedItem = tabitemContractor; // Focus on tabitemContractor widget
                return;
            }

            Contractor contractor = (Contractor)datagridContractor.SelectedItem; // Get Selected Contractor

            // Show MessageBox if Invalid Inputs
            if (txtbxFirstName.Text.Trim().Length == 0 || txtbxLastName.Text.Trim().Length == 0 || Math.Round(sliderHourlyWage.Value, 2) == 0)
            {
                MessageBox.Show("Invalid Input", "Error", MessageBoxButton.OK);
                return;
            }

            contractor.FirstName = txtbxFirstName.Text;
            contractor.LastName = txtbxLastName.Text;
            contractor.HourlyWage = Math.Round(sliderHourlyWage.Value, 2);

        }

        private void btnAddJob_Click(object sender, RoutedEventArgs e)
        {
            // Show MessageBox if Invalid Inputs
            if (txtbxTitle.Text.Trim().Length == 0 || datepickerJobDate.SelectedDate == null || sliderCost.Value == 0 || (bool)chkbxCompleted.IsChecked)
            {
                MessageBox.Show("Invalid Input", "Error", MessageBoxButton.OK);
                return;
            }

            // Create New Job object
            var dateOnly = new DateOnly(datepickerJobDate.SelectedDate.Value.Year, datepickerJobDate.SelectedDate.Value.Month, datepickerJobDate.SelectedDate.Value.Day);
            Job newJob = new Job(txtbxTitle.Text, dateOnly, Math.Round(sliderCost.Value,2));

            try
            {
                recruitmentSystem.AddJob(newJob);
            }
            catch
            {
                MessageBox.Show("Job already exist", "Error", MessageBoxButton.OK);
                return;
            }
        }

        private void btnUpdateJob_Click(object sender, RoutedEventArgs e)
        {
            // Note: UpdateJob button can be used to change the state of the Job, i.e. Assign a Contractor or Change to Finish Status.

            comboboxContractorAssigned.ItemsSource = recruitmentSystem.GetAvailableContractors();

            if (datagridJob.SelectedItem == null) // Make sure user selected a job to be modified
            {
                MessageBox.Show("Please select a Job", "Warn", MessageBoxButton.OK);
                tabctrlTables.SelectedItem = tabitemJob; // Focus on tabitemContractor widget
                return;
            }

            Job selectedJob = (Job)datagridJob.SelectedItem; // Get Selected Job

            // Check invalid modified properties
            if (txtbxTitle.Text.Trim().Length==0|| datepickerJobDate.SelectedDate==null||sliderCost.Value==0)
            {
                MessageBox.Show("Invalid New Values", "Error", MessageBoxButton.OK);
                return;
            }

            if (chkbxCompleted.IsChecked.Value) // If Job is Finished
            {
                recruitmentSystem.CompleteJob(selectedJob);
                return;
            }

            selectedJob.Title = txtbxTitle.Text;
            selectedJob.Date = new DateOnly(datepickerJobDate.SelectedDate.Value.Year, datepickerJobDate.SelectedDate.Value.Month, datepickerJobDate.SelectedDate.Value.Day);
            selectedJob.Cost = Math.Round(sliderCost.Value, 2);

            Contractor? selectedContractor = (Contractor?)comboboxContractorAssigned.SelectedItem;
            if (!selectedJob.Completed && selectedJob.ContractorAssigned == null && selectedContractor != null)
            {
                //selectedJob.ContractorAssigned = selectedContractor;
                recruitmentSystem.AssignJob(selectedJob, selectedContractor); // Assign New Contractor
            }
            // We want to deassign Old Contractor and Assign New Contractor to the Job
            else if (!selectedJob.Completed && selectedJob.ContractorAssigned != null && selectedContractor != null)
            {
                if (selectedJob.ContractorAssigned.FullName != selectedContractor.FullName)
                {
                    selectedJob.DeassignContractor(); // Deassign Old Contractor
                    recruitmentSystem.AssignJob(selectedJob, selectedContractor); // Assign New Contractor
                }
            }
        }

        private void comboboxContractorAssigned_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
