using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Proverb.Data.EntityFramework.Models;

namespace Proverb.Web.Models
{
    public class SayingVM
    {
        public SayingVM() {

        }

        public SayingVM(Saying saying) {
            Id = saying.Id;
            Text = saying.Text;
            SageId = saying.SageId;
            SageName = saying.Sage.Name;
        }

        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int SageId { get; set; }

        public string SageName { get; set; }

        public void UpdateSaying(Saying saying) 
        {
            saying.Id = Id;
            saying.Text = Text;
            saying.SageId = SageId;
        }
    }
}
