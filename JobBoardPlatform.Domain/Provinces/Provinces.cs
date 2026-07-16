using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Cities;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Provinces
{
    public class Province : BaseEntity
    {
        public Province(string name)
        {
            Name = name;
        }
        public Province()
        {
            
        }
        public string Name { get; set; }
        public List<City>? Cities { get; set; }

       
    }
}
