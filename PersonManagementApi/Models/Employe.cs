using System;

namespace PersonManagementApi.Models
{
    public class Employe
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Departement { get; set; }
        public DateTime DateofJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}
