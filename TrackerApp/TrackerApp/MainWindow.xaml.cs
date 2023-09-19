using System;
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
            // Add Initial Data to RecruitmentSystem
            recruitmentSystem.AddContractor(new Contractor("Cedric", "Anover", 45));
            recruitmentSystem.AddContractor(new Contractor("John", "Cena", 12));
            recruitmentSystem.AddContractor(new Contractor("Jack", "Ma", 500d));

            recruitmentSystem.AddJob(new Job("Data Scientist", "29/12/2023", 300000));
            recruitmentSystem.AddJob(new Job("Data Engineer", "5/11/2023", 100000));
            recruitmentSystem.AddJob(new Job("Programmer", "6/01/2024", 100000));


            InitializeComponent();




        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
