using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerApp
{
    public class Job
    {
        public string Title { get; private set; }
        public DateOnly Date { get; set; }
        public double Cost { get; set; }
        public bool Completed { get; set; }
        public Contractor? ContractorAssigned { get; set; }

        public Job(string title, DateOnly date, double cost, Contractor? contractorAssigned = null, bool completed=false)
        {
            Title = title;
            Date = date;
            Cost = cost;
            ContractorAssigned = contractorAssigned; // Default to null
            Completed = completed; // Default to false
        }
        public Job(string title, string date, double cost, Contractor? contractorAssigned = null, bool completed = false)
        {
            Title = title;
            Date = DateOnly.Parse(date); // Parse date if given as string
            Cost = cost;
            ContractorAssigned = contractorAssigned; // Default to null
            Completed = completed; // Default to false
        }

        public void AssignContractor(Contractor contractor)
        {
            /* Assign a Contractor to the Job */
            contractor.IsAvailable = false; // Update the State of IsAvailable to false
            contractor.StartDate = Date; // Assign Job Date to Contractor's StartDate
            ContractorAssigned = contractor;
        }

        public void DeassignContractor()
        {
            /* Deassign a Contractor */
            if (ContractorAssigned != null)
            {
                ContractorAssigned.IsAvailable = true; // Update the State of IsAvailable to true
                ContractorAssigned.StartDate = null; // Update the State of StartDate to null
            }
            ContractorAssigned = null;
        }

        public void JobFinished()
        {
            /* Change the States of Job when Completed */
            Completed = true; // Update Completed status to true
            DeassignContractor(); // Deassign the Contractor
        }
    }
}
