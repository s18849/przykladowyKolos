using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace przykladowyKolos.DTOs.Responses
{
    public class GetAnimalResponse
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime AdmissionDate { get; set; }

        public string LastNameOfOwner { get; set; }

    }
}
