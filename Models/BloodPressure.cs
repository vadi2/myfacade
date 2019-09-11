using System;
using System.Collections.Generic;

namespace myfacade.Models
{
    public partial class BloodPressure
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime MeasuredAt { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
