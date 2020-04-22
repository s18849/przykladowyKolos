using przykladowyKolos.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace przykladowyKolos.Services
{
    public interface IAnimalDbService
    {
        public List<GetAnimalResponse> GetAnimal(string orderBy);
    }
}
