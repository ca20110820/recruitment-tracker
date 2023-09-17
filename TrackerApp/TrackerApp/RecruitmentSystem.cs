using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
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
            Contractor? contractor = GetContractor(firstName, lastName);

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
        }

        public void AddJob(Job job)
        {
            // Check if job exist in jobs
            bool jobExists = jobs.Any(x => x.Title == job.Title);

            if (!jobExists)
            {
                jobs.Add(job);
            }
        }

        public void AssignJob(string jobTitle, string firstName, string lastName)
        {
            Job? job = GetJob(jobTitle);
            Contractor? contractor = GetContractor(firstName, lastName);

            if (contractor != null && job != null)
            {
                // Overwrite if the Job has an existing Contractor Association
                job.AssignContractor(contractor);
            }
        }

        public void CompleteJob(string jobTitle)
        {
            // -- Implementation Strategy --
            // Update IsAvailable status of contractor (associated to job) to true
            // Deassign the contractor of job.
            // Update Completed status of job to true
            
            // Note: We do not delete the job from the list of jobs

            Job? job = GetJob(jobTitle);

            if (job != null) // Check if job exists
            {
                job.Completed = true; // Update Completed status of job to true
                job.DeassignContractor(); // Deassign the contractor of job, also update the IsAvailable in the Job class
            }
        }

        public List<Contractor> GetAvailableContractors()
        {
            /* Return all Available Contractors */
            return contractors.FindAll(contractor => contractor.IsAvailable);
        }

        public List<Job> GetUnassignedJobs()
        {
            /* Return all the jobs that have not Contractor assigned and Not Complete */
            return jobs.FindAll(job => job.ContractorAssigned is null && !job.Completed);
        }

        public List<Job> GetJobByCost(double minCost, double maxCost)
        {
            if (minCost > maxCost || minCost < 0 || maxCost < 0)
                throw new ArgumentOutOfRangeException("minCost or maxCost is out-of-range or invalid!");

            return jobs.FindAll(job => minCost <= job.Cost && job.Cost <= maxCost);
        }

        private Contractor? GetContractor(string firstName, string lastName)
        {
            /* Get Contractor based from First and Last Name, return null if Contractor does not exist */
            return contractors.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }

        private Job? GetJob(string title)
        {
            /* Get Job based from Title, return null if job does not exist */
            return jobs.FirstOrDefault(x => x.Title.Contains(title));
        }

    }
}
