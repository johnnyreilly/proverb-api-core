﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Proverb.Data.EntityFramework.Models;

namespace Proverb.Web.Models
{
    public class SageVM
    {
        public SageVM() {

        }

        public SageVM(User user) {
            Id = user.Id;
            Name = user.Name;
            UserName = user.UserName;
            Email = user.Email;
            DateOfBirth = user.DateOfBirth;
            Sagacity = user.Sagacity;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Sagacity { get; set; }

        public void UpdateUser(User user) 
        {
            user.Id = Id;
            user.Name = Name;
            user.UserName = UserName;
            user.Email = Email;
            user.DateOfBirth = DateOfBirth;
            user.Sagacity = Sagacity;
        }
    }
}
