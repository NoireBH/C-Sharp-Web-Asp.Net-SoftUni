using Microsoft.AspNetCore.Identity;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.ValidationConstants.Ad;
using System.ComponentModel;

namespace SoftUniBazar.Models.Ad
{
    public class AdViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string Owner { get; set; } = null!;

		public string ImageUrl { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

        public string Category { get; set; } = null!;
    }
}
