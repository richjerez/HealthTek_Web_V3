using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class Locations
    {
        public Locations()
        {
            ApptsStartLocation = new HashSet<Appointments>();
            ApptsEndLocation = new HashSet<Appointments>();
        }

        [DisplayName("ID")]
        public int LocationsId { get; set; }

        [DisplayName("POS")]
        public string? PlaceOfService { get; set; }

        [DisplayName("Employee")]
        public string? FkEmployeesId { get; set; }

        [DisplayName("Facility")]
        public int? FkFacilitiesId { get; set; }

        [DisplayName("Contact")]
        public int? FkClientContactsId { get; set; }

        [DisplayName("Caregiver")]
        public int? FkCaregiversId { get; set; }

        [DisplayName("Client")]
        public int? FkClientsId { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Please enter Address.")]
        [DataType(DataType.Text)]
        public string? Address { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "Please enter City.")]
        [DataType(DataType.Text)]
        public string? City { get; set; }

        [DisplayName("State")]
        [Required(ErrorMessage = "Please enter State.")]
        [DataType(DataType.Text)]
        public string? State { get; set; }

        [DisplayName("Zip Code")]
        [Required(ErrorMessage = "Please enter Zip.")]
        [DataType(DataType.Text)]
        public string? Zipcode { get; set; }

#nullable enable
        [DisplayName("County")]
        [DataType(DataType.Text)]
        public string? County { get; set; }

        [DisplayName("Region")]
        [DataType(DataType.Text)]
        public string? Region { get; set; }

        [DisplayName("Country")]
        [DataType(DataType.Text)]
        public string? Country { get; set; }

        [DisplayName("GPS Latitude")]
        [DisplayFormat(DataFormatString = "{0:#.#####}")]
        [Column(TypeName = "decimal(10, 8)")]
        public decimal? GpsLatitude { get; set; }

        [DisplayName("GPS Longitude")]
        [DisplayFormat(DataFormatString = "{0:#.#####}")]
        [Column(TypeName = "decimal(10, 8)")]
        public decimal? GpsLongitude { get; set; }

        [DisplayName("Location Name")]
        [DataType(DataType.Text)]
        public string? LocationName { get; set; }

        [DisplayName("Location Description")]
        [DataType(DataType.Text)]
        public string? LocationDescription { get; set; }
#nullable disable

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Clients? FkClients { get; set; }
        public ClientContacts? ClientContacts { get; set; }
        public Employees? Employees { get; set; }
        public Facilities? Facilities { get; set; }
        public Caregivers? Caregivers { get; set; }

        public virtual ICollection<Appointments>? ApptsStartLocation { get; set; }
        public virtual ICollection<Appointments>? ApptsEndLocation { get; set; }

        [DisplayName("Primary Address")]
        public string FullPrimaryAddress
        {
            get
            {
                return (Address + ", " + City + ", " + State + " " + Zipcode);
            }
        }

        [DisplayName("Full Location")]
        public string? FullLocation
        {
            get
            {
                return (LocationName + ": " + Address + ", " + City + ", " + State + " " + Zipcode);
            }
        }
        public string? CityState
        {
            get
            {
                var newAddr = City + ", " + State;
                if (State != null && State.Length > 2)
                {
                    var abbr = stateAbbreviationExpand(State.ToLower());
                    newAddr = City + ", " + abbr;
                }
                return newAddr;
            }
        }
        public string? Coordinates
        {
            get
            {
                var coord = GpsLatitude + ", " + GpsLongitude;

                return coord;
            }
        }

        public string stateAbbreviationExpand(string abbr)
        {
            Dictionary<string, string> stateToAbbrev = new Dictionary<string, string>() { { "alabama", "AL" },
                { "alaska", "AK" }, { "arizona", "AZ" }, { "arkansas", "AR" }, { "california", "CA" }, { "colorado", "CO" },
                { "connecticut", "CT" }, { "delaware", "DE" }, { "district of columbia", "DC" }, { "florida", "FL" },
                { "georgia", "GA" }, { "hawaii", "HI" }, { "idaho", "ID" }, { "illinois", "IL" }, { "indiana", "IN" },
                { "iowa", "IA" }, { "kansas", "KS" }, { "kentucky", "KY" }, { "louisiana", "LA" }, { "maine", "ME" },
                { "maryland", "MD" }, { "massachusetts", "MA" }, { "michigan", "MI" }, { "minnesota", "MN" },
                { "mississippi", "MS" }, { "missouri", "MO" }, { "montana", "MT" }, { "nebraska", "NE" }, { "nevada", "NV" },
                { "new hampshire", "NH" }, { "new jersey", "NJ" }, { "new mexico", "NM" }, { "new york", "NY" },
                { "north carolina", "NC" }, { "north dakota", "ND" }, { "ohio", "OH" }, { "oklahoma", "OK" },
                { "oregon", "OR" }, { "pennsylvania", "PA" }, { "rhode island", "RI" }, { "south carolina", "SC" },
                { "south dakota", "SD" }, { "tennessee", "TN" }, { "texas", "TX" }, { "utah", "UT" }, { "vermont", "VT" },
                { "virginia", "VA" }, { "washington", "WA" }, { "west virginia", "WV" }, { "wisconsin", "WI" }, { "wyoming", "WY" } }; if (abbr != null)
            {
                if (stateToAbbrev.ContainsKey(abbr))
                {
                    return (stateToAbbrev[abbr]);
                }
            }
            /* error handler is to return an empty string rather than throwing an exception */
            return "";
        }
        public Dictionary<string, string> stateDictionary()
        {
            Dictionary<string, string> stateDict = new Dictionary<string, string>() { { "AL", "Alabama" },
                { "AK", "Alaska" }, { "AZ", "Arizona" }, { "AR", "Arkansas" }, { "CA", "California" }, { "CO", "Colorado" },
                { "CT", "Connecticut" }, { "DE", "Delaware" }, { "DC", "District of Columbia" }, { "FL", "Florida" },
                { "GA", "Georgia" }, { "HI", "Hawaii" }, { "ID", "Idaho" }, { "IL", "Illinois" }, { "IN", "Indiana" },
                { "IA", "Iowa" }, { "KS", "Kansas" }, { "KY", "Kentucky" }, { "LA", "Louisiana" }, { "ME", "Maine" },
                { "MD", "Maryland" }, { "MA", "Massachusetts" }, { "MI", "Michigan" }, { "MN", "Minnesota" },
                { "MS", "Mississippi" }, { "MO", "Missouri" }, { "MT", "Montana" }, { "NE", "Nebraska" }, { "NV", "Nevada" },
                { "NH", "New Hampshire" }, { "NJ", "New Jersey" }, { "NM", "New Mexico" }, { "NY", "New York" },
                { "NC", "North Carolina" }, { "ND", "North Dakota" }, { "OH", "Ohio" }, { "OK", "Oklahoma" },
                { "OR", "Oregon" }, { "PA", "Pennsylvania" }, { "RI", "Rhode Island" }, { "SC", "South Carolina" },
                { "SD", "South Dakota" }, { "TN", "Tennessee" }, { "TX", "Texas" }, { "UT", "Utah" }, { "VT", "Vermont" },
                { "VA", "Virginia" }, { "WA", "Washington" }, { "WV", "West Virginia" }, { "WI", "Wisconsin" }, { "WY", "Wyoming" } };

            return stateDict;
        }
    }
}