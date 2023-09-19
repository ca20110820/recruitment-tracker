using System;
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
            recruitmentSystem.AddContractor(new Contractor("Cedric", "Anover", 45, "23/09/2023"));
            recruitmentSystem.AddContractor(new Contractor("John", "Cena", 12, "05/01/2023"));
            recruitmentSystem.AddContractor(new Contractor("Jack", "Ma", 200d));
            recruitmentSystem.AddContractor(new Contractor("John", "Wick", 50d, "29/06/2023", false));

            recruitmentSystem.AddJob(new Job("Data Scientist", "29/12/2023", 300000));
            recruitmentSystem.AddJob(new Job("Data Engineer", "5/11/2023", 100000));
            recruitmentSystem.AddJob(new Job("Programmer", "6/01/2024", 100000));
            recruitmentSystem.AddJob(new Job("Software Architect", "25/02/2024", 100000, completed:true));
            recruitmentSystem.AssignJob(recruitmentSystem.jobs[2], recruitmentSystem.contractors[0]);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            datagridContractor.ItemsSource = recruitmentSystem.contractors;
            datagridJob.ItemsSource = recruitmentSystem.jobs;
        }

        private void datagridContractor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contractor selectedContractor = (Contractor)datagridContractor.SelectedItem;
            UpdateContractorGroupBox(selectedContractor);
        }

        private void datagridJob_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Job selectedJob = (Job)datagridJob.SelectedItem;
            UpdateJobGroupBox(selectedJob);
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

            if (job.ContractorAssigned != null)
            {
                comboboxContractorAssigned.SelectedItem = job;
            }
            else
            {
                if (!job.Completed)
                {
                    comboboxContractorAssigned.ItemsSource = null;
                    comboboxContractorAssigned.ItemsSource = recruitmentSystem.GetAvailableContractors();
                }
            }
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
        }

        private void btnViewUnassignedJobs_Click(object sender, RoutedEventArgs e)
        {
            datagridJob.ItemsSource = recruitmentSystem.GetUnassignedJobs();
        }
    }
}
