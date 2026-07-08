using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.entities;
using JobBoardPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Companies
{
    public class Company:BaseEntity
    {
        public Company(string name, string? description, string? website, string? location, Guid? logoId, Guid userId)
        {
            Name = name;
            Description = description;
            Website = website;
            Location = location;
            LogoId = logoId;
            UserId = userId;
            Validate();
        }
        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentNullException("name can not be null");
            }
            if (string.IsNullOrEmpty(Location))
            {
                throw new ArgumentNullException("Location can not be null");
            }
            if(Name.Length < 3)
            {
                throw new ArgumentException("name must be longer than 2 characters");
            }
            if(Location.Length < 10)
            {
                throw new ArgumentException("location must be longer than 10 characters");
            }
         
        }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string Location { get; set; }

        public Guid? LogoId { get; set; }
        public bool IsActive { get; set; } = true;
        public List<JobAd>? JobAds{ get; set; }
        public Guid UserId { get; set; }
        public User Owner{ get; set; }
    }

}
