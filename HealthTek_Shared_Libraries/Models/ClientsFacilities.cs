using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class ClientsFacilities
    {
        public int ClientsFacilitiesId { get; set; }

        [DisplayName("Client")]
        public int FkClientsId { get; set; }

        [DisplayName("Facility")]
        public int FkFacilitiesId { get; set; }

        [DisplayName("Client Label")]
        [DataType(DataType.Text)]
        public string? ClientLabel { get; set; }

        [DisplayName("Chart Number")]
        [DataType(DataType.Text)]
        public string? ChartNumber { get; set; }

        [DisplayName("Created")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        public DateTime LastUpdateDate { get; set; }

        [DisplayName("Client")]
        public string? ClientChartLabel
        {
            get
            {
                if (FkClients == null)
                {
                    return ChartNumber;
                }
                return (ChartNumber + ": " + FkClients.LastName.Substring(0, 1) + ", " + FkClients.FirstName);
            }
        }

        public virtual Facilities? FkFacilities { get; set; }
        public virtual Clients? FkClients { get; set; }
    }
}
