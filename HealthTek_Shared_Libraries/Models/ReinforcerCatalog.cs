using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class ReinforcerCatalog
    {
        public ReinforcerCatalog()
        {
            Preferences = new HashSet<Preferences>();
        }

        [DisplayName("ID")]
        public int ReinforcerCatalogId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Reinforcer Name.")]
        [DisplayName("Name")]
        public string? ReinforcerName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string? ReinforcerDescription { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual ICollection<Preferences> Preferences { get; set; }
    }
}
