using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCJPortal.ViewModels.ViewModels.Authorization
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.UserProjectIds = new List<int>();
        }

        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsAllProjectsAllowed { get; set; }
        public int? CountryId { get; set; }
        public CountryViewModel Country { get; set; }

        // пока не понятно будет много ролей или одна
        [Required]
        public string RoleId { get; set; }
        public List<int> UserProjectIds { get; set; }
    }
}
