using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace TrackerApp
{
    public class RecruitmentSystem
    {
        private List<Contractor> contractors = new List<Contractor>();
        private List<Job> jobs = new List<Job>();

        public List<Contractor> Contractors { get {  return contractors; } }
        public List<Job> Jobs { get {  return jobs; } }

        public void AddContractor(Contractor newContractor)
        {
            // Check if contractor exist in contractors
            bool contractorExists = contractors.Any(contractor => contractor.FirstName == newContractor.FirstName && contractor.LastName == newContractor.LastName);

            // Add the New Contractor if they do not exists in our list of contractors
            if (!contractorExists)
            {
                contractors.Add(newContractor);
            }
        }

        public void RemoveContractor(string firstName, string lastName)
        {
            /* Remove a Contractor based from First and Last Name */

            // -- Implementation Strategy --
            // Find and Remove the Contractor based from First & Last Names, if applicable
            // Remove the Contractor Association with a Job, if applicable

            // Assert that its only one person for this project
            Contractor? contractor = contractors.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);

            if (contractor != null) // Contractor Exist
            {
                // Remove Association with a Job, if applicable
                foreach(Job job in jobs)
                {
                    Contractor? tempContractor = job.ContractorAssigned;
                    if (tempContractor != null && tempContractor.FirstName==firstName && tempContractor.LastName == lastName)
                    {
                        // Set null to job.ContractorAssigned
                        job.DeassignContractor();
                        break;
                    }
                }

                contractors.Remove(contractor);
            }
            else // Contractor Does Not Exist
            {

            }

        }

    }
}
