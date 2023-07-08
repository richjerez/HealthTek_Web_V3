using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class OperatingCounties
    {
        public OperatingCounties()
        {
            EmployeeOperatingCounties = new HashSet<EmployeesOperatingCounties>();
            FacilitiesOperatingCounties = new HashSet<FacilitiesOperatingCounties>();
        }

        [DisplayName("ID")]
        public int OperatingCountiesId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter County.")]
        [DisplayName("County")]
        public string? County { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter State.")]
        [DisplayName("State")]
        public string? State { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Country.")]
        [DisplayName("Country")]
        public string? Country { get; set; }

        public string OPAbbr
        {
            get
            {
                var newAddr = County;
                if (State != null && State.Length > 2)
                {
                    var abbr = stateAbbreviationExpand(State.ToLower());
                    newAddr = County + ", " + abbr + ", " + Country;
                }
                return newAddr;
            }
        }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual ICollection<EmployeesOperatingCounties> EmployeeOperatingCounties { get; set; }
        public virtual ICollection<FacilitiesOperatingCounties> FacilitiesOperatingCounties { get; set; }

        public string stateAbbreviationExpand(string abbr)
        {
            Dictionary<string, string> stateToAbbrev = new Dictionary<string, string>() { { "alabama", "AL" }, { "alaska", "AK" }, { "arizona", "AZ" }, { "arkansas", "AR" }, { "california", "CA" }, { "colorado", "CO" }, { "connecticut", "CT" }, { "delaware", "DE" }, { "district of columbia", "DC" }, { "florida", "FL" }, { "georgia", "GA" }, { "hawaii", "HI" }, { "idaho", "ID" }, { "illinois", "IL" }, { "indiana", "IN" }, { "iowa", "IA" }, { "kansas", "KS" }, { "kentucky", "KY" }, { "louisiana", "LA" }, { "maine", "ME" }, { "maryland", "MD" }, { "massachusetts", "MA" }, { "michigan", "MI" }, { "minnesota", "MN" }, { "mississippi", "MS" }, { "missouri", "MO" }, { "montana", "MT" }, { "nebraska", "NE" }, { "nevada", "NV" }, { "new hampshire", "NH" }, { "new jersey", "NJ" }, { "new mexico", "NM" }, { "new york", "NY" }, { "north carolina", "NC" }, { "north dakota", "ND" }, { "ohio", "OH" }, { "oklahoma", "OK" }, { "oregon", "OR" }, { "pennsylvania", "PA" }, { "rhode island", "RI" }, { "south carolina", "SC" }, { "south dakota", "SD" }, { "tennessee", "TN" }, { "texas", "TX" }, { "utah", "UT" }, { "vermont", "VT" }, { "virginia", "VA" }, { "washington", "WA" }, { "west virginia", "WV" }, { "wisconsin", "WI" }, { "wyoming", "WY" } }; if (abbr != null)
            {
                if (stateToAbbrev.ContainsKey(abbr))
                {
                    return (stateToAbbrev[abbr]);
                }

            }
            /* error handler is to return an empty string rather than throwing an exception */
            return "";
        }

    }
}
