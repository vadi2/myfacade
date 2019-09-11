using System;
using System.Collections.Generic;

namespace myfacade.Models
{
    public partial class Patient
    {
        public Patient()
        {
            BloodPressure = new HashSet<BloodPressure>();
        }

        public int Id { get; set; }
        public string PatientNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<BloodPressure> BloodPressure { get; set; }
    }
}
