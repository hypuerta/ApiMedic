namespace ApiMedic.Entities.Responses
{
    public class ResponseAppointmentsByDoctor
    {
        public string DoctorIdentification { get; set; }

        public string DoctorName { get; set; }

        public string PatientIdentification { get; set; }

        public string PatientName { get; set; }

        public string DateAppointment { get; set; }
    }
}