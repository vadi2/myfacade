using System;
using System.Collections.Generic;

namespace myfacade.Models
{
    public partial class ViSiBloodPressure
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime MeasuredAt { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }

        public virtual ViSiPatient ViSiPatient { get; set; }
    }
}
