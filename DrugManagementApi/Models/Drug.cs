using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrugManagementApi.Models
{
    public class Drug
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [MaxLength(30)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Label { get; set; }

        public string Description { get; set; }

        [Range(double.Epsilon, double.MaxValue)]
        public double Price { get; set; }

        public override string ToString()
        {
            return "Code: " + Code + " Label" + Label;
        }
    }
}
