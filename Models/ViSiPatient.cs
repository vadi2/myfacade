using System;
using System.Collections.Generic;

namespace myfacade.Models
{
    public partial class VisiPatient
    {
        public VisiPatient()
        {
            ViSiBloodPressure = new HashSet<ViSiBloodPressure>();
        }

        public int Id { get; set; }
        public string PatientNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<ViSiBloodPressure> ViSiBloodPressure { get; set; }
    }
}
