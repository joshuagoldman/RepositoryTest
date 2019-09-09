using System;
using System.ComponentModel.DataAnnotations;

namespace Lajtz.Models
{
    public class Item
    {
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
