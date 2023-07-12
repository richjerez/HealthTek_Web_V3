using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HealthTek_Shared_Libraries
{
    public class ExternalLists
    {
        public ExternalLists() { }

        #region Demographics
        public List<string> SchoolLevel = new List<string>() { "KinderGarden", "Elementary", "Middle", "High", "Associates", "Bachelor", "Masters", "Doctorate" };

        public List<string> Religions = new List<string>()
        {
            "None","Atheism/Agnosticism","Bahá’í","Baptist","Buddhism","Catholic","Christianity","Confucianism","Druze","Gnosticism","Hinduism","Islam","Jainism","Judaism","Non-denominational","Pentecostal",
            "Rastafarianism","Roman Catholic","Shinto","Sikhism","Zoroastrianism","Traditional African Religions","African Diaspora Religions","Indigenous American Religions"
        };

        public List<string> Ethnicities = new List<string>() {
            "Afghan", "Albanian", "Algerian"  , "American"  , "Andorran"  , "Angolan"  , "Antiguans"  , "Argentinean"  , "Armenian"  , "Australian"  , "Austrian"  , "Azerbaijani"  , "Bahamian"  , "Bahraini"  , "Bangladeshi"  , "Barbadian"  , "Barbudans"  , "Batswana"  , "Belarusian"  , "Belgian"  , "Belizean"  , "Beninese"  , "Bhutanese"  , "Bolivian"
            , "Bosnian"  , "Brazilian"  , "British"  , "Bruneian"  , "Bulgarian"  , "Burkinabe"  , "Burmese"  , "Burundian"  , "Cambodian"  , "Cameroonian"  , "Canadian"  , "Cape Verdean"  , "Central African"  , "Chadian"  , "Chilean"  , "Chinese"  , "Colombian"  , "Comoran"  , "Congolese"  , "Costa Rican"  , "Croatian"  , "Cuban"  , "Cypriot"  , "Czech"  , "Danish"
            , "Djibouti"  , "Dominican"  , "Dutch"  , "East Timorese"  , "Ecuadorean"  , "Egyptian"  , "Emirian"  , "Equatorial Guinean"  , "Eritrean"  , "Estonian"  , "Ethiopian"  , "Fijian"  , "Filipino"  , "Finnish"  , "French"  , "Gabonese"  , "Gambian"  , "Georgian"  , "German"  , "Ghanaian"  , "Greek"  , "Grenadian"  , "Guatemalan"  , "Guinea,Bissauan"  , "Guinean"
            , "Guyanese"  , "Haitian"  , "Herzegovinian"  , "Honduran"  , "Hungarian"  , "I,Kiribati"  , "Icelander"  , "Indian"  , "Indonesian" , "Iranian" , "Iraqi"
          , "Irish" , "Israeli" , "Italian" , "Ivorian" , "Jamaican" , "Japanese" , "Jordanian" , "Kazakhstani" , "Kenyan" , "Kittian and Nevisian" , "Kuwaiti" , "Kyrgyz" , "Laotian" , "Latvian" , "Lebanese" , "Liberian" , "Libyan" , "Liechtensteiner" , "Lithuanian" , "Luxembourger" , "Macedonian" , "Malagasy" , "Malawian" , "Malaysian" , "Maldivian" , "Malian"
          , "Maltese" , "Marshallese" , "Mauritanian" , "Mauritian" , "Mexican" , "Micronesian" , "Moldovan" , "Monacan" , "Mongolian" , "Moroccan" , "Mosotho" , "Motswana"
          , "Mozambican" , "Namibian" , "Nauruan" , "Nepalese" , "New Zealander" , "Ni,Vanuatu" , "Nicaraguan" , "Nigerian" , "Nigerien" , "North Korean" , "Northern Irish" , "Norwegian" , "Omani" , "Pakistani" , "Palauan" , "Panamanian" , "Papua New Guinean" , "Paraguayan" , "Peruvian" , "Polish" , "Portuguese", "Puerto Rican"
          , "Qatari" , "Romanian" , "Russian" , "Rwandan" , "Saint Lucian" , "Salvadoran" , "Samoan" , "San Marinese" , "Sao Tomean" , "Saudi" , "Scottish" , "Senegalese" , "Serbian" , "Seychellois" , "Sierra Leonean"
          , "Singaporean" , "Slovakian" , "Slovenian" , "Solomon Islander" , "Somali" , "South African" , "South Korean" , "Spanish" , "Sri Lankan" , "Sudanese"
          , "Surinamer" , "Swazi" , "Swedish" , "Swiss" , "Syrian" , "Taiwanese" , "Tajik" , "Tanzanian" , "Thai" , "Togolese" , "Tongan" , "Trinidadian or Tobagonian" , "Tunisian" , "Turkish" , "Tuvaluan" , "Ugandan" , "Ukrainian" , "Uruguayan" , "Uzbekistani" , "Venezuelan" , "Vietnamese" , "Welsh" , "Yemenite" , "Zambian", "Zimbabwean"
        };

        public List<CultureInfo> Languages = CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToList();
        #endregion

        #region Locations
        public List<string> States = new List<string>() { { "Alabama" },
                { "Alaska" }, { "Arizona" }, { "Arkansas" }, { "California" }, { "Colorado" },
                { "Connecticut" }, { "Delaware" }, { "District of Columbia" }, { "Florida" },
                { "Georgia" }, { "Hawaii" }, { "Idaho" }, { "Illinois" }, { "Indiana" },
                { "Iowa" }, { "Kansas" }, { "Kentucky" }, { "Louisiana" }, { "Maine" },
                { "Maryland" }, { "Massachusetts" }, { "Michigan" }, { "Minnesota" },
                { "Mississippi" }, { "Missouri" }, { "Montana" }, { "Nebraska" }, { "Nevada" },
                { "New Hampshire" }, { "New Jersey" }, { "New Mexico" }, { "New York" },
                { "North Carolina" }, { "North Dakota" }, { "Ohio" }, { "Oklahoma" },
                { "Oregon" }, {"Pennsylvania" }, { "Rhode Island" }, { "South Carolina" },
                { "South Dakota" }, { "Tennessee" }, {"Texas" }, { "Utah" }, { "Vermont" },
                { "Virginia" }, { "Washington" }, {"West Virginia" }, { "Wisconsin" }, { "Wyoming" } };

        public List<string> Countries = new List<string>()
        {
            "Afghanistan","Albania","Algeria","American Samoa","Andorra","Angola","Anguilla","Antarctica","Antigua and Barbuda","Argentina","Armenia","Aruba",
            "Australia","Austria","Azerbaijan", "Bahamas","Bahrain","Bangladesh","Barbados","Belarus","Belgium","Belize","Benin","Bermuda","Bhutan","Bolivia",
            "Bosnia and Herzegovina","Botswana","Bouvet Island","Brazil","British Indian Ocean Territory","Brunei Darussalam","Bulgaria","Burkina Faso",
            "Burundi","Cambodia","Cameroon","Canada","Cape Verde","Cayman Islands","Central African Republic","Chad","Chile","China","Christmas Island",
            "Cocos (Keeling) Islands","Colombia","Comoros","Congo","Congo, the Democratic Republic of the","Cook Islands","Costa Rica","Cote D'Ivoire","Croatia",
            "Cuba","Cyprus","Czech Republic","Denmark","Djibouti","Dominica","Dominican Republic","Ecuador","Egypt","El Salvador","Equatorial Guinea","Eritrea",
            "Estonia","Ethiopia","Falkland Islands (Malvinas)","Faroe Islands","Fiji","Finland","France","French Guiana","French Polynesia","French Southern Territories",
            "Gabon","Gambia","Georgia","Germany","Ghana","Gibraltar","Greece","Greenland","Grenada","Guadeloupe","Guam","Guatemala","Guinea","Guinea-Bissau",
            "Guyana","Haiti","Heard Island and Mcdonald Islands","Holy See (Vatican City State)","Honduras","Hong Kong","Hungary","Iceland","India","Indonesia",
            "Iran, Islamic Republic of","Iraq","Ireland","Israel","Italy","Jamaica","Japan","Jordan","Kazakhstan","Kenya","Kiribati","Korea, Democratic People's Republic of",
            "Korea, Republic of","Kuwait","Kyrgyzstan","Lao People's Democratic Republic","Latvia","Lebanon","Lesotho","Liberia","Libyan Arab Jamahiriya",
            "Liechtenstein","Lithuania","Luxembourg","Macao","Macedonia, the Former Yugoslav Republic of","Madagascar","Malawi","Malaysia","Maldives","Mali",
            "Malta","Marshall Islands","Martinique","Mauritania","Mauritius","Mayotte","Mexico","Micronesia, Federated States of","Moldova, Republic of","Monaco",
            "Mongolia","Montserrat","Morocco","Mozambique","Myanmar","Namibia","Nauru","Nepal","Netherlands","Netherlands Antilles","New Caledonia","New Zealand",
            "Nicaragua","Niger","Nigeria","Niue","Norfolk Island","Northern Mariana Islands","Norway","Oman","Pakistan","Palau","Palestinian Territory, Occupied",
            "Panama","Papua New Guinea","Paraguay","Peru","Philippines","Pitcairn","Poland","Portugal","Puerto Rico","Qatar","Reunion","Romania","Russian Federation",
            "Rwanda","Saint Helena","Saint Kitts and Nevis","Saint Lucia","Saint Pierre and Miquelon","Saint Vincent and the Grenadines","Samoa","San Marino",
            "Sao Tome and Principe","Saudi Arabia","Senegal","Serbia and Montenegro","Seychelles","Sierra Leone","Singapore","Slovakia","Slovenia","Solomon Islands",
            "Somalia","South Africa","South Georgia and the South Sandwich Islands","Spain","Sri Lanka","Sudan","Suriname","Svalbard and Jan Mayen","Swaziland",
            "Sweden","Switzerland","Syrian Arab Republic","Taiwan, Province of China","Tajikistan","Tanzania, United Republic of","Thailand","Timor-Leste",
            "Togo","Tokelau","Tonga","Trinidad and Tobago","Tunisia","Turkey","Turkmenistan","Turks and Caicos Islands","Tuvalu","Uganda","Ukraine","United Arab Emirates",
            "United Kingdom","United States","United States Minor Outlying Islands","Uruguay","Uzbekistan","Vanuatu","Venezuela","Viet Nam","Virgin Islands, British",
            "Virgin Islands, US","Wallis and Futuna","Western Sahara","Yemen","Zambia","Zimbabwe"
        };

        public List<string> FloridaCities = new List<string>() {"Alachua ",
"Alford ",
"Altamonte Springs ",
"Altha ",
"Alva ",
"Anna Maria ",
"Apalachicola ",
"Apollo Beach ",
"Apopka ",
"Arcadia ",
"Archer ",
"Astatula ",
"Astor ",
"Atlantic Beach ",
"Auburndale ",
"Avon Park ",
"Babson Park ",
"Bagdad ",
"Bartow ",
"Bay Pines ",
"Bell ",
"Belle Glade ",
"Belleair Beach ",
"Belleview ",
"Beverly Hills ",
"Big Pine Key ",
"Blountstown ",
"Boca Raton ",
"Bokeelia ",
"Bonifay ",
"Bonita Springs ",
"Bowling Green ",
"Boynton Beach ",
"Bradenton Beach ",
"Bradenton ",
"Brandon ",
"Branford ",
"Bristol ",
"Bronson ",
"Brooker ",
"Brooksville ",
"Bunnell ",
"Bushnell ",
"Callahan ",
"Campbellton ",
"Canal Point ",
"Cape Canaveral ",
"Cape Coral ",
"Captiva ",
"Carrabelle ",
"Caryville ",
"Casselberry ",
"Cedar Key ",
"Center Hill ",
"Century ",
"Chattahoochee ",
"Chiefland ",
"Chipley ",
"Chokoloskee ",
"Christmas ",
"Clearwater ",
"Clermont ",
"Clewiston ",
"Cocoa Beach ",
"Cocoa ",
"Coleman ",
"Cortez ",
"Cottondale ",
"Crescent City ",
"Crestview ",
"Cross City ",
"Crystal River ",
"Crystal Springs ",
"Cypress ",
"Dade City ",
"Dania ",
"Davenport ",
"Daytona Beach ",
"De Leon Springs ",
"Debary ",
"Deerfield Beach ",
"Deland ",
"Delray Beach ",
"Deltona ",
"Destin ",
"Dover ",
"Dundee ",
"Dunedin ",
"Dunnellon ",
"Eagle Lake ",
"East Palatka ",
"Eastpoint ",
"Ebro ",
"Edgewater ",
"Eglin Afb ",
"Elfers ",
"Ellenton ",
"Englewood ",
"Estero ",
"Eustis ",
"Fellsmere ",
"Fernandina Beach ",
"Ferndale ",
"Flagler Beach ",
"Floral City ",
"Fort Lauderdale ",
"Fort Meade ",
"Fort Myers Beach ",
"Fort Myers ",
"Fort Pierce ",
"Fort Walton Beach ",
"Fort White ",
"Freeport ",
"Frostproof ",
"Fruitland Park ",
"Gainesville ",
"Geneva ",
"Gibsonton ",
"Glen Saint Mary ",
"Goldenrod ",
"Gonzalez ",
"Goodland ",
"Gotha ",
"Graceville ",
"Grand Ridge ",
"Green Cove Springs ",
"Greensboro ",
"Greenville ",
"Greenwood ",
"Gretna ",
"Groveland ",
"Gulf Breeze ",
"Haines City ",
"Hallandale ",
"Hampton ",
"Hastings ",
"Havana ",
"Hawthorne ",
"Hernando ",
"Hialeah ",
"High Springs ",
"Highland City ",
"Hilliard ",
"Hobe Sound ",
"Holiday ",
"Hollywood ",
"Holmes Beach ",
"Homestead ",
"Homosassa Springs ",
"Homosassa ",
"Horseshoe Beach ",
"Howey In The Hills ",
"Hudson ",
"Immokalee ",
"Indialantic ",
"Indian Rocks Beach ",
"Indiantown ",
"Inglis ",
"Interlachen ",
"Inverness ",
"Islamorada ",
"Jacksonville Beach ",
"Jacksonville ",
"Jasper ",
"Jay ",
"Jennings ",
"Jensen Beach ",
"Jupiter ",
"Kathleen ",
"Key Biscayne ",
"Key Colony Beach ",
"Key Largo ",
"Key West ",
"Keystone Heights ",
"Kissimmee ",
"Labelle ",
"Lacoochee ",
"Lady Lake ",
"Lake Alfred ",
"Lake Butler ",
"Lake City ",
"Lake Hamilton ",
"Lake Helen ",
"Lake Mary ",
"Lake Panasoffkee ",
"Lake Placid ",
"Lake Wales ",
"Lake Worth ",
"Lakeland ",
"Land O Lakes ",
"Largo ",
"Laurel Hill ",
"Laurel ",
"Lawtey ",
"Lecanto ",
"Lee ",
"Leesburg ",
"Lehigh Acres ",
"Live Oak ",
"Longboat Key ",
"Longwood ",
"Loughman ",
"Lutz ",
"Lynn Haven ",
"Macclenny ",
"Madison ",
"Maitland ",
"Malabar ",
"Malone ",
"Mango ",
"Marathon ",
"Marco Island ",
"Marianna ",
"Mary Esther ",
"Mascotte ",
"Mayo ",
"Mc Intosh ",
"Melbourne Beach ",
"Melbourne ",
"Merritt Island ",
"Mexico Beach ",
"Miami Beach ",
"Miami ",
"Micanopy ",
"Middleburg ",
"Midway ",
"Milton ",
"Mims ",
"Minneola ",
"Miramar Beach ",
"Molino ",
"Monticello ",
"Montverde ",
"Moore Haven ",
"Mount Dora ",
"Mulberry ",
"Naples ",
"Neptune Beach ",
"New Port Richey ",
"New Smyrna Beach ",
"Newberry ",
"Niceville ",
"Nokomis ",
"Noma ",
"North Fort Myers ",
"North Miami Beach ",
"North Palm Beach ",
"North Port ",
"Oak Hill ",
"Oakland ",
"Ocala ",
"Ocoee ",
"Odessa ",
"Okahumpka ",
"Okeechobee ",
"Oldsmar ",
"Opa Locka ",
"Orange City ",
"Orange Park ",
"Orlando ",
"Ormond Beach ",
"Osprey ",
"Oviedo ",
"Pahokee ",
"Paisley ",
"Palatka ",
"Palm Bay ",
"Palm Beach Gardens ",
"Palm Beach ",
"Palm City ",
"Palm Coast ",
"Palm Harbor ",
"Palmetto ",
"Panama City Beach ",
"Panama City ",
"Paxton ",
"Pembroke Pines ",
"Penney Farms ",
"Pensacola ",
"Perry ",
"Pierson ",
"Pineland ",
"Pinellas Park ",
"Placida ",
"Plant City ",
"Polk City ",
"Pomona Park ",
"Pompano Beach ",
"Ponce De Leon ",
"Port Charlotte ",
"Port Orange ",
"Port Richey ",
"Port Saint Joe ",
"Port Saint Lucie ",
"Port Salerno ",
"Punta Gorda ",
"Quincy ",
"Reddick ",
"Riverview ",
"Rockledge ",
"Roseland ",
"Rotonda West ",
"Ruskin ",
"Safety Harbor ",
"San Antonio ",
"Sanford ",
"Sanibel ",
"Sarasota ",
"Satellite Beach ",
"Sebastian ",
"Sebring ",
"Seffner ",
"Seminole ",
"Shalimar ",
"Sharpes ",
"Silver Springs ",
"Sneads ",
"Sopchoppy ",
"Sorrento ",
"South Bay ",
"Spring Hill ",
"Starke ",
"Stuart ",
"Tallahassee ",
"Tampa ",
"Tangerine ",
"Tarpon Springs ",
"Tavares ",
"Tavernier ",
"Thonotosassa ",
"Titusville ",
"Trenton ",
"Umatilla ",
"Valparaiso ",
"Valrico ",
"Venice ",
"Vernon ",
"Vero Beach ",
"Wabasso ",
"Waldo ",
"Wauchula ",
"Wausau ",
"Waverly ",
"Webster ",
"Welaka ",
"West Palm Beach ",
"Westville ",
"Wewahitchka ",
"White Springs ",
"Wildwood ",
"Williston ",
"Wimauma ",
"Windermere ",
"Winter Beach ",
"Winter Garden ",
"Winter Haven ",
"Winter Park ",
"Winter Springs ",
"Woodville ",
"Yalaha ",
"Yankeetown ",
"Yulee ",
"Zellwood ",
"Zephyrhills ",
"Zolfo Springs ",};
        #endregion

        #region ABA
        public List<string> CollectionMethod = new List<string>()
        {
            "Frequency", "Duration", "Trials"
        };

        public List<string> Functions = new List<string>()
        {
            "Attention", "Escape", "Access to Tangibles", "Sensory Stimulation"
        };
        public List<string> Communication = new List<string>()
        {
            "Yes","No","Recipient Declined","N/A, Provider is the Prescriber","N/A, Recipient is not on medication"
        };
        public List<string> TimeFrame = new List<string>()
        {
            "Second(s)","Minute(s)","Hour(s)","Week(s)","Month(s)","Year(s)"
        };
        public List<string> ClientParticipations = new List<string>() { "Poor", "Fair", "Good" };
        public string[] SupervisionCharacteristics = {
                "Observation of supervisee working with the individual",
                "Observation of supervisee working with caregiver / other provider",
                "Specific recipient discussed",
                "Recipient privacy discussed",
                "Supervisory discussion reviewed",
                "Required documentation reviewed",
                "BACB Task List skills covered (task list numbers)"};
        public string[] SupervisionModes = { "Face-to-Face", "Individual", "Observational", "Group" };
        public decimal[] SupervisionRatings = { 10.00m, 9.50m, 9.00m, 8.50m, 8.00m, 7.50m, 7.00m, 6.50m, 6.00m, 5.50m, 5.00m, 4.50m, 4.00m, 3.50m, 3.00m, 2.50m, 2.00m, 1.50m, 1.00m, 0.50m, 0.00m };

        #endregion

        #region Statuses
        public List<string> AssignmentStatuses = new List<string>()
        {
            "Open", "Awaiting", "Archived"
        };

        public List<string> AuthorizationStatuses = new List<string>()
        {
            "Initiated", "Pending", "Incomplete", "Archived", "Active", "Expired", "Denied"
        };

        public List<string> BehaviorStatuses = new List<string>()
        {
            "In Progress", "Met", "Not Started", "Mastered"
        };

        public List<string> BillingStatuses = new List<string>()
        {
            "Unbilled", "Billable", "Billed", "Funded", "Archived", "Paid", "Rejected"
        };

        public List<string> ClientStatuses = new List<string>()
        {
            "Active", "Inactive", "Archived"
        };

        public List<string> PolicyStatuses = new List<string>()
        {
            "Active", "Processing", "COIN", "Expired", "Archived"
        };

        public List<string> DocumentStatuses = new List<string>()
        {
            "Uploaded", "Edited", "Archived", "Sorted"
        };

        public List<string> EmployeeStatuses = new List<string>()
        {
            "Active", "Inactive", "Applicant", "Pending Interview", "Interviewed", "Not Eligible"
        };

        public List<string> IntakeStatuses = new List<string>()
        {
            "Discovery", "Processing", "Troubleshooting", "Archived", "Completed", "Not Eligible"
        };

        public List<string> InvoiceStatuses = new List<string>()
        {
            "Draft", "Submitted", "Partial", "Cancelled", "Paid", "Overdue"
        };

        public List<string> QaStatuses = new List<string>()
        {
            "Draft", "Received", "Unsigned", "Archived", "Approved", "Rejected"
        };

        public List<string> SupervisionStatuses = new List<string>()
        {
            "Draft", "Submitted", "Unsigned", "Archived", "Approved", "Rejected"
        };

        public List<string> TaskStatuses = new List<string>()
        {
            "New", "Awaiting", "Processing", "Archived", "Completed"
        };
        #endregion

        #region Types
        public List<string> AppointmentTypes = new List<string>()
        {
            "Call", "Services", "Meeting", "Call", "Reminder", "Interview"
        };

        public List<string> AssessmentTypes = new List<string>()
        {
            "Initial Assessment", "ReAssessment"
        };

        public List<string> ServiceCodeTypes = new List<string>()
        {
            "ABA", "Clinical"
        };

        public List<string> DocumentTypes = new List<string>()
        {
            "HR", "Client", "Intake"
        };

        public List<string> FacilityTypes = new List<string>()
        {
            "ABA", "Clinical"
        };

        public List<string> SupervisionTypes = new List<string>()
        {
            "Individual", "Group"
        };

        public List<string> TaskTypes = new List<string>()
        {
            "Assignment", "Authorization", "Intake", "Schedule", "Finance", "Utilization", "Reply", "Misc"
        };

        public List<string> ObjectiveTypes = new List<string>()
        {
            "CTG-M", "CTG-R", "CTG-I", "Maladaptive", "Replacement"
        };

        #endregion

        #region Dictionaries
        public Dictionary<string, string> TypeofBill = new Dictionary<string, string>() {
{"111","Hospital Inpatient admit through discharge(Including Medicare Part A)"},
{"112","Hospital Inpatient  interim (Including Medicare Part A)"},
{"113","Hospital Inpatient  interim (Including Medicare Part A)- continuing claims"},
{"114","Hospital Inpatient  interim (Including Medicare Part A) – last claim"},
{"115","Hospital Inpatient  late charge only(Including Medicare Part A)"},
{"117","Hospital Inpatient replacement of prior claim  (Including Medicare Part A)"},
{"118","Hospital Inpatient void/cancel of a prior claim (Including Medicare Part A)"},
{"119","Hospital Inpatient  final claim for a home (Including Medicare Part A)"},
{"121","Hospital Inpatient  admit through discharge (Medicare Part B Only)"},
{"122","Hospital Inpatient  interim (Medicare Part B Only)"},
{"123","Hospital Inpatient  interim – continuing claims (Medicare Part B Only)"},
{"124","Hospital Inpatient  interim – last claim (Medicare Part B Only)"},
{"125","Hospital Inpatient  late charge only (Medicare Part B Only)"},
{"127","Hospital Inpatient  replacement of prior claim (Medicare Part B Only)"},
{"128","Hospital Inpatient void/cancel of a prior claim (Medicare Part B Only)"},
{"129","Hospital Inpatient  final claim for a home (Medicare Part B Only)"},
{"131","Hospital Outpatient admit through discharge"},
{"132","Hospital Outpatient interim –"},
{"133","Hospital Outpatient interim – continuing claims"},
{"134","Hospital Outpatient interim – last claim"},
{"135","Hospital Outpatient late charge only"},
{"137","Hospital Outpatient replacement of prior claim"},
{"138","Hospital Outpatient void/cancel of a prior claim"},
{"139","Hospital Outpatient final claim for a home"},
{"141","Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) admit through discharge"},
{"142","Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim –"},
{"143","Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim – continuing claims"},
{"144","Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim – last claim"},
{"145","Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) late charge only"},
{"147","Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) replacement of prior claim"},
{"148","Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) void/cancel of a prior claim"},
{"149","Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) final claim for a home"},
{"151","Hospital Nursing Facility Level I admit through discharge"},
{"152","Hospital Nursing Facility Level I interim –"},
{"153","Hospital Nursing Facility Level I interim – continuing claims"},
{"154","Hospital Nursing Facility Level I interim – last claim"},
{"155","Hospital Nursing Facility Level I late charge only"},
{"157","Hospital Nursing Facility Level I replacement of prior claim"},
{"158","Hospital Nursing Facility Level I void/cancel of a prior claim"},
{"159","Hospital Nursing Facility Level I final claim for a home"},
{"161","Hospital Nursing Facility Level II admit through discharge"},
{"162","Hospital Nursing Facility Level II interim –"},
{"163","Hospital Nursing Facility Level II interim – continuing claims"},
{"164","Hospital Nursing Facility Level II interim – last claim"},
{"165","Hospital Nursing Facility Level II late charge only"},
{"167","Hospital Nursing Facility Level II replacement of prior claim"},
{"168","Hospital Nursing Facility Level II void/cancel of a prior claim"},
{"169","Hospital Nursing Facility Level II final claim for a home"},
{"171","Hospital Intermediate Care – Level III Nursing Facility admit through discharge"},
{"172","Hospital Intermediate Care – Level III Nursing Facility interim –"},
{"173","Hospital Intermediate Care – Level III Nursing Facility interim – continuing claims"},
{"174","Hospital Intermediate Care – Level III Nursing Facility interim – last claim"},
{"175","Hospital Intermediate Care – Level III Nursing Facility late charge only"},
{"177","Hospital Intermediate Care – Level III Nursing Facility replacement of prior claim"},
{"178","Hospital Intermediate Care – Level III Nursing Facility void/cancel of a prior claim"},
{"179","Hospital Intermediate Care – Level III Nursing Facility final claim for a home"},
{"181","Hospital Swing Beds admit through discharge"},
{"182","Hospital Swing Beds interim –"},
{"183","Hospital Swing Beds interim – continuing claims"},
{"184","Hospital Swing Beds interim – last claim"},
{"185","Hospital Swing Beds late charge only"},
{"187","Hospital Swing Beds replacement of prior claim"},
{"188","Hospital Swing Beds void/cancel of a prior claim"},
{"189","Hospital Swing Beds final claim for a home"},
{"211","Skilled Nursing Inpatient admit through discharge  (Including Medicare Part A)"},
{"212","Skilled Nursing Inpatient  interim – (Including Medicare Part A)"},
{"213","Skilled Nursing Inpatient  interim – continuing claims (Including Medicare Part A)"},
{"214","Skilled Nursing Inpatient interim – last claim  (Including Medicare Part A)"},
{"215","Skilled Nursing Inpatient  late charge only (Including Medicare Part A)"},
{"217","Skilled Nursing Inpatient  replacement of prior claim (Including Medicare Part A)"},
{"218","Skilled Nursing Inpatient  void/cancel of a prior claim (Including Medicare Part A)"},
{"219","Skilled Nursing Inpatient  final claim for a home (Including Medicare Part A)"},
{"221","Skilled Nursing Inpatient  admit through discharge (Medicare Part B Only)"},
{"222","Skilled Nursing Inpatient  interim –  (Medicare Part B Only)"},
{"223","Skilled Nursing Inpatient  interim – continuing claims (Medicare Part B Only)"},
{"224","Skilled Nursing Inpatient  interim – last claim (Medicare Part B Only)"},
{"225","Skilled Nursing Inpatient  late charge only (Medicare Part B Only)"},
{"227","Skilled Nursing Inpatient (Medicare Part B Only) replacement of prior claim"},
{"228","Skilled Nursing Inpatient (Medicare Part B Only) void/cancel of a prior claim"},
{"229","Skilled Nursing Inpatient (Medicare Part B Only) final claim for a home"},
{"231","Skilled Nursing Outpatient admit through discharge"},
{"232","Skilled Nursing Outpatient interim –"},
{"233","Skilled Nursing Outpatient interim – continuing claims"},
{"234","Skilled Nursing Outpatient interim – last claim"},
{"235","Skilled Nursing Outpatient late charge only"},
{"237","Skilled Nursing Outpatient replacement of prior claim"},
{"238","Skilled Nursing Outpatient void/cancel of a prior claim"},
{"239","Skilled Nursing Outpatient final claim for a home"},
{"241","Skilled Nursing Other (for hospital referenced diagnostic services or home health not under a plan of treatment) admit through discharge"},
{"242","Skilled Nursing Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim –"},
{"243","Skilled Nursing Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim – continuing claims"},
{"244","Skilled Nursing Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim – last claim"},
{"245","Skilled Nursing Other (for hospital referenced diagnostic services or home health not under a plan of treatment) late charge only"},
{"247","Skilled Nursing Other (for hospital referenced diagnostic services or home health not under a plan of treatment) replacement of prior claim"},
{"248","Skilled Nursing Other (for hospital referenced diagnostic services or home health not under a plan of treatment) void/cancel of a prior claim"},
{"249","Skilled Nursing Other (for hospital referenced diagnostic services or home health not under a plan of treatment) final claim for a home"},
{"251","Skilled Nursing Nursing Facility Level I admit through discharge"},
{"252","Skilled Nursing Nursing Facility Level I interim –"},
{"253","Skilled Nursing Nursing Facility Level I interim – continuing claims"},
{"254","Skilled Nursing Nursing Facility Level I interim – last claim"},
{"255","Skilled Nursing Nursing Facility Level I late charge only"},
{"257","Skilled Nursing Nursing Facility Level I replacement of prior claim"},
{"258","Skilled Nursing Nursing Facility Level I void/cancel of a prior claim"},
{"259","Skilled Nursing Nursing Facility Level I final claim for a home"},
{"261","Skilled Nursing Nursing Facility Level II admit through discharge"},
{"262","Skilled Nursing Nursing Facility Level II interim –"},
{"263","Skilled Nursing Nursing Facility Level II interim – continuing claims"},
{"264","Skilled Nursing Nursing Facility Level II interim – last claim"},
{"265","Skilled Nursing Nursing Facility Level II late charge only"},
{"267","Skilled Nursing Nursing Facility Level II replacement of prior claim"},
{"268","Skilled Nursing Nursing Facility Level II void/cancel of a prior claim"},
{"269","Skilled Nursing Nursing Facility Level II final claim for a home"},
{"271","Skilled Nursing Intermediate Care – Level III Nursing Facility admit through discharge"},
{"272","Skilled Nursing Intermediate Care – Level III Nursing Facility interim –"},
{"273","Skilled Nursing Intermediate Care – Level III Nursing Facility interim – continuing claims"},
{"274","Skilled Nursing Intermediate Care – Level III Nursing Facility interim – last claim"},
{"275","Skilled Nursing Intermediate Care – Level III Nursing Facility late charge only"},
{"277","Skilled Nursing Intermediate Care – Level III Nursing Facility replacement of prior claim"},
{"278","Skilled Nursing Intermediate Care – Level III Nursing Facility void/cancel of a prior claim"},
{"279","Skilled Nursing Intermediate Care – Level III Nursing Facility final claim for a home"},
{"281","Skilled Nursing Swing Beds admit through discharge"},
{"282","Skilled Nursing Swing Beds interim –"},
{"283","Skilled Nursing Swing Beds interim – continuing claims"},
{"284","Skilled Nursing Swing Beds interim – last claim"},
{"285","Skilled Nursing Swing Beds late charge only"},
{"287","Skilled Nursing Swing Beds replacement of prior claim"},
{"288","Skilled Nursing Swing Beds void/cancel of a prior claim"},
{"289","Skilled Nursing Swing Beds final claim for a home"},
{"311","Home Health Inpatient (Including Medicare Part A) admit through discharge"},
{"312","Home Health Inpatient (Including Medicare Part A) interim –"},
{"313","Home Health Inpatient (Including Medicare Part A) interim – continuing claims"},
{"314","Home Health Inpatient (Including Medicare Part A) interim – last claim"},
{"315","Home Health Inpatient (Including Medicare Part A) late charge only"},
{"317","Home Health Inpatient (Including Medicare Part A) replacement of prior claim"},
{"318","Home Health Inpatient (Including Medicare Part A) void/cancel of a prior claim"},
{"319","Home Health Inpatient (Including Medicare Part A) final claim for a home"},
{"321","Home Health Inpatient (Medicare Part B Only) admit through discharge"},
{"322","Home Health Inpatient (Medicare Part B Only) interim –"},
{"323","Home Health Inpatient (Medicare Part B Only) interim – continuing claims"},
{"324","Home Health Inpatient (Medicare Part B Only) interim – last claim"},
{"325","Home Health Inpatient (Medicare Part B Only) late charge only"},
{"327","Home Health Inpatient (Medicare Part B Only) replacement of prior claim"},
{"328","Home Health Inpatient (Medicare Part B Only) void/cancel of a prior claim"},
{"329","Home Health Inpatient (Medicare Part B Only) final claim for a home"},
{"331","Home Health Outpatient admit through discharge"},
{"332","Home Health Outpatient interim –"},
{"333","Home Health Outpatient interim – continuing claims"},
{"334","Home Health Outpatient interim – last claim"},
{"335","Home Health Outpatient late charge only"},
{"337","Home Health Outpatient replacement of prior claim"},
{"338","Home Health Outpatient void/cancel of a prior claim"},
{"339","Home Health Outpatient final claim for a home"},
{"341","Home Health Other (for hospital referenced diagnostic services or home health not under a plan of treatment) admit through discharge"},
{"342","Home Health Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim –"},
{"343","Home Health Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim – continuing claims"},
{"344","Home Health Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim – last claim"},
{"345","Home Health Other (for hospital referenced diagnostic services or home health not under a plan of treatment) late charge only"},
{"347","Home Health Other (for hospital referenced diagnostic services or home health not under a plan of treatment) replacement of prior claim"},
{"348","Home Health Other (for hospital referenced diagnostic services or home health not under a plan of treatment) void/cancel of a prior claim"},
{"349","Home Health Other (for hospital referenced diagnostic services or home health not under a plan of treatment) final claim for a home"},
{"351","Home Health Nursing Facility Level I admit through discharge"},
{"352","Home Health Nursing Facility Level I interim –"},
{"353","Home Health Nursing Facility Level I interim – continuing claims"},
{"354","Home Health Nursing Facility Level I interim – last claim"},
{"355","Home Health Nursing Facility Level I late charge only"},
{"357","Home Health Nursing Facility Level I replacement of prior claim"},
{"358","Home Health Nursing Facility Level I void/cancel of a prior claim"},
{"359","Home Health Nursing Facility Level I final claim for a home"},
{"361","Home Health Nursing Facility Level II admit through discharge"},
{"362","Home Health Nursing Facility Level II interim –"},
{"363","Home Health Nursing Facility Level II interim – continuing claims"},
{"364","Home Health Nursing Facility Level II interim – last claim"},
{"365","Home Health Nursing Facility Level II late charge only"},
{"367","Home Health Nursing Facility Level II replacement of prior claim"},
{"368","Home Health Nursing Facility Level II void/cancel of a prior claim"},
{"369","Home Health Nursing Facility Level II final claim for a home"},
{"371","Home Health Intermediate Care – Level III Nursing Facility admit through discharge"},
{"372","Home Health Intermediate Care – Level III Nursing Facility interim –"},
{"373","Home Health Intermediate Care – Level III Nursing Facility interim – continuing claims"},
{"374","Home Health Intermediate Care – Level III Nursing Facility interim – last claim"},
{"375","Home Health Intermediate Care – Level III Nursing Facility late charge only"},
{"377","Home Health Intermediate Care – Level III Nursing Facility replacement of prior claim"},
{"378","Home Health Intermediate Care – Level III Nursing Facility void/cancel of a prior claim"},
{"379","Home Health Intermediate Care – Level III Nursing Facility final claim for a home"},
{"381","Home Health Swing Beds admit through discharge"},
{"382","Home Health Swing Beds interim –"},
{"383","Home Health Swing Beds interim – continuing claims"},
{"384","Home Health Swing Beds interim – last claim"},
{"385","Home Health Swing Beds late charge only"},
{"387","Home Health Swing Beds replacement of prior claim"},
{"388","Home Health Swing Beds void/cancel of a prior claim"},
{"389","Home Health Swing Beds final claim for a home"},
{"411","Christian Science Hospital Inpatient (Including Medicare Part A) admit through discharge"},
{"412","Christian Science Hospital Inpatient (Including Medicare Part A) interim –"},
{"413","Christian Science Hospital Inpatient (Including Medicare Part A) interim – continuing claims"},
{"414","Christian Science Hospital Inpatient (Including Medicare Part A) interim – last claim"},
{"415","Christian Science Hospital Inpatient (Including Medicare Part A) late charge only"},
{"417","Christian Science Hospital Inpatient (Including Medicare Part A) replacement of prior claim"},
{"418","Christian Science Hospital Inpatient (Including Medicare Part A) void/cancel of a prior claim"},
{"419","Christian Science Hospital Inpatient (Including Medicare Part A) final claim for a home"},
{"421","Christian Science Hospital Inpatient (Medicare Part B Only) admit through discharge"},
{"422","Christian Science Hospital Inpatient (Medicare Part B Only) interim –"},
{"423","Christian Science Hospital Inpatient (Medicare Part B Only) interim – continuing claims"},
{"424","Christian Science Hospital Inpatient (Medicare Part B Only) interim – last claim"},
{"425","Christian Science Hospital Inpatient (Medicare Part B Only) late charge only"},
{"427","Christian Science Hospital Inpatient (Medicare Part B Only) replacement of prior claim"},
{"428","Christian Science Hospital Inpatient (Medicare Part B Only) void/cancel of a prior claim"},
{"429","Christian Science Hospital Inpatient (Medicare Part B Only) final claim for a home"},
{"431","Christian Science Hospital Outpatient admit through discharge"},
{"432","Christian Science Hospital Outpatient interim –"},
{"433","Christian Science Hospital Outpatient interim – continuing claims"},
{"434","Christian Science Hospital Outpatient interim – last claim"},
{"435","Christian Science Hospital Outpatient late charge only"},
{"437","Christian Science Hospital Outpatient replacement of prior claim"},
{"438","Christian Science Hospital Outpatient void/cancel of a prior claim"},
{"439","Christian Science Hospital Outpatient final claim for a home"},
{"441","Christian Science Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) admit through discharge"},
{"442","Christian Science Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim –"},
{"443","Christian Science Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim – continuing claims"},
{"444","Christian Science Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim – last claim"},
{"445","Christian Science Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) late charge only"},
{"447","Christian Science Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) replacement of prior claim"},
{"448","Christian Science Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) void/cancel of a prior claim"},
{"449","Christian Science Hospital Other (for hospital referenced diagnostic services or home health not under a plan of treatment) final claim for a home"},
{"451","Christian Science Hospital Nursing Facility Level I admit through discharge"},
{"452","Christian Science Hospital Nursing Facility Level I interim –"},
{"453","Christian Science Hospital Nursing Facility Level I interim – continuing claims"},
{"454","Christian Science Hospital Nursing Facility Level I interim – last claim"},
{"455","Christian Science Hospital Nursing Facility Level I late charge only"},
{"457","Christian Science Hospital Nursing Facility Level I replacement of prior claim"},
{"458","Christian Science Hospital Nursing Facility Level I void/cancel of a prior claim"},
{"459","Christian Science Hospital Nursing Facility Level I final claim for a home"},
{"461","Christian Science Hospital Nursing Facility Level II admit through discharge"},
{"462","Christian Science Hospital Nursing Facility Level II interim –"},
{"463","Christian Science Hospital Nursing Facility Level II interim – continuing claims"},
{"464","Christian Science Hospital Nursing Facility Level II interim – last claim"},
{"465","Christian Science Hospital Nursing Facility Level II late charge only"},
{"467","Christian Science Hospital Nursing Facility Level II replacement of prior claim"},
{"468","Christian Science Hospital Nursing Facility Level II void/cancel of a prior claim"},
{"469","Christian Science Hospital Nursing Facility Level II final claim for a home"},
{"471","Christian Science Hospital Intermediate Care – Level III Nursing Facility admit through discharge"},
{"472","Christian Science Hospital Intermediate Care – Level III Nursing Facility interim –"},
{"473","Christian Science Hospital Intermediate Care – Level III Nursing Facility interim – continuing claims"},
{"474","Christian Science Hospital Intermediate Care – Level III Nursing Facility interim – last claim"},
{"475","Christian Science Hospital Intermediate Care – Level III Nursing Facility late charge only"},
{"477","Christian Science Hospital Intermediate Care – Level III Nursing Facility replacement of prior claim"},
{"478","Christian Science Hospital Intermediate Care – Level III Nursing Facility void/cancel of a prior claim"},
{"479","Christian Science Hospital Intermediate Care – Level III Nursing Facility final claim for a home"},
{"481","Christian Science Hospital Swing Beds admit through discharge"},
{"482","Christian Science Hospital Swing Beds interim –"},
{"483","Christian Science Hospital Swing Beds interim – continuing claims"},
{"484","Christian Science Hospital Swing Beds interim – last claim"},
{"485","Christian Science Hospital Swing Beds late charge only"},
{"487","Christian Science Hospital Swing Beds replacement of prior claim"},
{"488","Christian Science Hospital Swing Beds void/cancel of a prior claim"},
{"489","Christian Science Hospital Swing Beds final claim for a home"},
{"511","Christian Science Extended Care Inpatient (Including Medicare Part A) admit through discharge"},
{"512","Christian Science Extended Care Inpatient (Including Medicare Part A) interim –"},
{"513","Christian Science Extended Care Inpatient (Including Medicare Part A) interim – continuing claims"},
{"514","Christian Science Extended Care Inpatient (Including Medicare Part A) interim – last claim"},
{"515","Christian Science Extended Care Inpatient (Including Medicare Part A) late charge only"},
{"517","Christian Science Extended Care Inpatient (Including Medicare Part A) replacement of prior claim"},
{"518","Christian Science Extended Care Inpatient (Including Medicare Part A) void/cancel of a prior claim"},
{"519","Christian Science Extended Care Inpatient (Including Medicare Part A) final claim for a home"},
{"521","Christian Science Extended Care Inpatient (Medicare Part B Only) admit through discharge"},
{"522","Christian Science Extended Care Inpatient (Medicare Part B Only) interim –"},
{"523","Christian Science Extended Care Inpatient (Medicare Part B Only) interim – continuing claims"},
{"524","Christian Science Extended Care Inpatient (Medicare Part B Only) interim – last claim"},
{"525","Christian Science Extended Care Inpatient (Medicare Part B Only) late charge only"},
{"527","Christian Science Extended Care Inpatient (Medicare Part B Only) replacement of prior claim"},
{"528","Christian Science Extended Care Inpatient (Medicare Part B Only) void/cancel of a prior claim"},
{"529","Christian Science Extended Care Inpatient (Medicare Part B Only) final claim for a home"},
{"531","Christian Science Extended Care Outpatient admit through discharge"},
{"532","Christian Science Extended Care Outpatient interim –"},
{"533","Christian Science Extended Care Outpatient interim – continuing claims"},
{"534","Christian Science Extended Care Outpatient interim – last claim"},
{"535","Christian Science Extended Care Outpatient late charge only"},
{"537","Christian Science Extended Care Outpatient replacement of prior claim"},
{"538","Christian Science Extended Care Outpatient void/cancel of a prior claim"},
{"539","Christian Science Extended Care Outpatient final claim for a home"},
{"541","Christian Science Extended Care Other (for hospital referenced diagnostic services or home health not under a plan of treatment) admit through discharge"},
{"542","Christian Science Extended Care Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim –"},
{"543","Christian Science Extended Care Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim – continuing claims"},
{"544","Christian Science Extended Care Other (for hospital referenced diagnostic services or home health not under a plan of treatment) interim – last claim"},
{"545","Christian Science Extended Care Other (for hospital referenced diagnostic services or home health not under a plan of treatment) late charge only"},
{"547","Christian Science Extended Care Other (for hospital referenced diagnostic services or home health not under a plan of treatment) replacement of prior claim"},
{"548","Christian Science Extended Care Other (for hospital referenced diagnostic services or home health not under a plan of treatment) void/cancel of a prior claim"},
{"549","Christian Science Extended Care Other (for hospital referenced diagnostic services or home health not under a plan of treatment) final claim for a home"},
{"551","Christian Science Extended Care Nursing Facility Level I admit through discharge"},
{"552","Christian Science Extended Care Nursing Facility Level I interim –"},
{"553","Christian Science Extended Care Nursing Facility Level I interim – continuing claims"},
{"554","Christian Science Extended Care Nursing Facility Level I interim – last claim"},
{"555","Christian Science Extended Care Nursing Facility Level I late charge only"},
{"557","Christian Science Extended Care Nursing Facility Level I replacement of prior claim"},
{"558","Christian Science Extended Care Nursing Facility Level I void/cancel of a prior claim"},
{"559","Christian Science Extended Care Nursing Facility Level I final claim for a home"},
{"561","Christian Science Extended Care Nursing Facility Level II admit through discharge"},
{"562","Christian Science Extended Care Nursing Facility Level II interim –"},
{"563","Christian Science Extended Care Nursing Facility Level II interim – continuing claims"},
{"564","Christian Science Extended Care Nursing Facility Level II interim – last claim"},
{"565","Christian Science Extended Care Nursing Facility Level II late charge only"},
{"567","Christian Science Extended Care Nursing Facility Level II replacement of prior claim"},
{"568","Christian Science Extended Care Nursing Facility Level II void/cancel of a prior claim"},
{"569","Christian Science Extended Care Nursing Facility Level II final claim for a home"},
{"571","Christian Science Extended Care Intermediate Care – Level III Nursing Facility admit through discharge"},
{"572","Christian Science Extended Care Intermediate Care – Level III Nursing Facility interim –"},
{"573","Christian Science Extended Care Intermediate Care – Level III Nursing Facility interim – continuing claims"},
{"574","Christian Science Extended Care Intermediate Care – Level III Nursing Facility interim – last claim"},
{"575","Christian Science Extended Care Intermediate Care – Level III Nursing Facility late charge only"},
{"577","Christian Science Extended Care Intermediate Care – Level III Nursing Facility replacement of a prior claim"},
{"578","Christian Science Extended Care Intermediate Care – Level III Nursing Facility void/cancel of a prior claim"},
{"579","Christian Science Extended Care Intermediate Care – Level III Nursing Facility final claim for a home"},
{"581","Christian Science Extended Care Swing Beds admit through discharge"},
{"582","Christian Science Extended Care Swing Beds interim –"},
{"583","Christian Science Extended Care Swing Beds interim – continuing claims"},
{"584","Christian Science Extended Care Swing Beds interim – last claim"},
{"585","Christian Science Extended Care Swing Beds late charge only"},
{"587","Christian Science Extended Care Swing Beds replacement of a prior claim"},
{"588","Christian Science Extended Care Swing Beds void/cancel of a prior claim"},
{"589","Christian Science Extended Care Swing Beds final claim for a home"}    };

        public Dictionary<string, string> BillingCodes = new Dictionary<string, string>() {
{"240"," All inclusive ancillary, general"},
{"260"," Intravenous(IV) therapy"},
{"261"," Intravenous(IV) therapy, infusion pump"},
{"262"," Intravenous(IV) therapy, pharmacy services"},
{"263"," Intravenous(IV) therapeutic drug, supply and delivery"},
{"264"," Intravenous(IV) therapy, supplies"},
{"269"," Intravenous(IV) therapy,"},
{"274"," Medical / surgical supplies and devices, prosthetic and orthotic"},
{"280"," Oncology"},
{"290"," Durable medical equipment, general"},
{"291"," Durable medical equipment, rental"},
{"292"," Durable medical equipment, purchase of new"},
{"294"," Durable medical equipment, supplies and drugs"},
{"299"," Durable medical equipment,"},
{"300"," Laboratory, general classification"},
{"301"," Laboratory, chemistry"},
{"302"," Laboratory, immunology"},
{"303"," Laboratory, renal patient"},
{"304"," Laboratory, dialysis"},
{"305"," Laboratory, hematology"},
{"306"," Laboratory, bacteriology and microbiology"},
{"307"," Laboratory, urology"},
{"308"," Reserved laboratory"},
{"309"," Laboratory,"},
{"310"," Laboratory, pathology general classification"},
{"311"," Laboratory pathology cytology"},
{"312"," Laboratory pathology histology"},
{"313"," Reserved laboratory pathology"},
{"314"," Laboratory, pathology biopsy"},
{"315"," Reserved laboratory, pathology"},
{"316"," Reserved laboratory, pathology"},
{"317"," Reserved laboratory, pathology"},
{"318"," Reserved laboratory, pathology"},
{"319"," Laboratory, pathology"},
{"320"," Radiology, diagnostic general classification"},
{"321"," Radiology, diagnostic angiocardiology"},
{"322"," Radiology, diagnostic arthrography"},
{"323"," Radiology, diagnostic arteriography"},
{"324"," Radiology, diagnostic chest X-ray"},
{"325"," Reserved radiology, diagnostic"},
{"326"," Reserved radiology, diagnostic"},
{"327"," Reserved radiology, diagnostic"},
{"328"," Reserved radiology, diagnostic"},
{"329"," Radiology, diagnostic"},
{"330"," Radiology, therapeutic chemapy general classification"},
{"331"," Radiology, therapeutic chemapy, chemapy administration – injected"},
{"332"," Radiology, therapeutic chemapy, chemapy and administration – oral"},
{"333"," Radiology, therapeutic chemapy, radiation therapy"},
{"334"," Reserved radiology, therapeutic chemapy"},
{"335"," Radiology, therapeutic chemapy, administration (intravenous(IV))"},
{"336"," Reserved radiology, therapeutic chemapy"},
{"337"," Reserved radiology, therapeutic chemapy"},
{"338"," Reserved radiology, therapeutic chemapy"},
{"339"," Radiology, therapeutic chemapy"},
{"340"," Nuclear medicine, general classification"},
{"341"," Nuclear medicine, diagnostic"},
{"342"," Nuclear medicine, therapeutic"},
{"343"," Nuclear medicine, diagnostic radiopharmaceuticals"},
{"344"," Nuclear medicine, therapeutic radiopharmaceuticals"},
{"349"," Nuclear medicine,"},
{"350"," Computerized axial tomography (CAT) scan, general"},
{"351"," Computerized axial tomography (CAT) scan, head scan"},
{"352"," Computerized axial tomography (CAT) scan, body scan"},
{"353"," Reserved computerized axial tomography scan (CAT)"},
{"354"," Reserved computerized axial tomography scan (CAT)"},
{"355"," Reserved computerized axial tomography scan (CAT)"},
{"356"," Reserved computerized axial tomography scan (CAT)"},
{"357"," Reserved computerized axial tomography scan (CAT)"},
{"358"," Reserved computerized axial tomography scan (CAT)"},
{"359"," Computerized axial tomography scan (CAT)"},
{"360"," Operating room services, general"},
{"361"," Operating room services, minor surgery"},
{"362"," Operating services, organ transplant  than kidney"},
{"363"," Reserved operating room services"},
{"364"," Reserved operating room services"},
{"365"," Reserved operating room services"},
{"366"," Reserved operating room services"},
{"367"," Operating room services, kidney transplant"},
{"368"," Reserved operating room services"},
{"369"," Operating room services"},
{"380"," Blood and blood products, general"},
{"381"," Blood and blood products, packed red cells"},
{"382"," Blood and blood products, whole blood products"},
{"383"," Blood and blood products, plasma"},
{"384"," Blood and blood products, platelets"},
{"385"," Blood and blood products, leukocytes"},
{"386"," Blood and blood products, components"},
{"387"," Blood and blood products, derivatives (cryoprecipitate)"},
{"388"," Blood and blood products, reserved components"},
{"389"," Blood and blood products,"},
{"390"," Blood administration process, storage general"},
{"391"," Blood administration process, storage administration"},
{"399"," Blood,  storage"},
{"400"," Imaging services, general"},
{"401"," Imaging services, diagnostic mammography"},
{"402"," Imaging services, ultrasound"},
{"403"," Imaging services, screening mammography"},
{"404"," Imaging services, positron emission tomography"},
{"405"," Reserved imaging services"},
{"406"," Reserved imaging services"},
{"407"," Reserved imaging services"},
{"408"," Reserved imaging services"},
{"409"," Imaging services,"},
{"410"," Respiratory services, general"},
{"411"," Reserved respiratory services"},
{"412"," Respiratory services, inhalation"},
{"413"," Respiratory services, hyperbaric oxygen therapy"},
{"414"," Reserved respiratory services"},
{"415"," Reserved respiratory services"},
{"416"," Reserved respiratory services"},
{"417"," Reserved respiratory services"},
{"418"," Reserved respiratory services"},
{"419"," Respiratory services,"},
{"420"," Physical therapy, general"},
{"421"," Physical therapy, visit"},
{"422"," Physical therapy, hourly"},
{"423"," Physical therapy, group"},
{"424"," Physical therapy, evaluation or reevaluation"},
{"425"," Reserved physical therapy"},
{"426"," Reserved physical therapy"},
{"427"," Reserved physical therapy"},
{"428"," Reserved physical therapy"},
{"429"," Physical therapy,"},
{"430"," Occupational therapy, general"},
{"431"," Occupational therapy, visit"},
{"432"," Occupational therapy, hourly"},
{"433"," Occupational therapy, group"},
{"434"," Occupational therapy, evaluation or reevaluation"},
{"435"," Reserved occupational therapy"},
{"436"," Reserved occupational therapy"},
{"437"," Reserved occupational therapy"},
{"438"," Reserved occupational therapy"},
{"439"," Occupational therapy,"},
{"440"," Speech therapy, general"},
{"441"," Speech therapy, visit"},
{"442"," Speech therapy, hourly"},
{"443"," Speech therapy, group"},
{"444"," Speech therapy, evaluation or reevaluation"},
{"445"," Reserved speech therapy"},
{"446"," Reserved speech therapy"},
{"447"," Reserved speech therapy"},
{"448"," Reserved speech therapy"},
{"449"," Speech therapy,"},
{"450"," Emergency room, general"},
{"451"," Emergency room, EMTALA emergency and medical"},
{"452"," Emergency room, beyond EMTALA screening"},
{"453"," Reserved emergency room"},
{"454"," Reserved emergency room"},
{"455"," Reserved emergency room"},
{"456"," Emergency room, urgent care"},
{"457"," Reserved emergency room"},
{"458"," Reserved emergency room"},
{"459"," Emergency room,"},
{"460"," Pulmonary function, general"},
{"461"," Reserved pulmonary function"},
{"462"," Reserved pulmonary function"},
{"463"," Reserved pulmonary function"},
{"464"," Reserved pulmonary function"},
{"465"," Reserved pulmonary function"},
{"466"," Reserved pulmonary function"},
{"467"," Reserved pulmonary function"},
{"468"," Reserved pulmonary function"},
{"469"," Pulmonary function,"},
{"470"," Audiology, general"},
{"471"," Audiology, diagnostic"},
{"472"," Audiology, treatment"},
{"473"," Reserved audiology"},
{"474"," Reserved audiology"},
{"475"," Reserved audiology"},
{"476"," Reserved audiology"},
{"477"," Reserved audiology"},
{"478"," Reserved audiology"},
{"479"," Audiology,"},
{"480"," Cardiology, general"},
{"481"," Cardiology, cardiac catheterization laboratory"},
{"482"," Cardiology, stress test"},
{"483"," Cardiology, echocardiology"},
{"484"," Reserved cardiology"},
{"485"," Reserved cardiology"},
{"486"," Reserved cardiology"},
{"487"," Reserved cardiology"},
{"488"," Reserved cardiology"},
{"489"," Cardiology,"},
{"490"," Ambulatory surgery"},
{"499",", ambulatory surgery"},
{"500"," Outpatient services"},
{"510"," Clinic,"},
{"511"," Clinic, chronic pain center"},
{"512"," Clinic, dental"},
{"513"," Clinic, psychiatric"},
{"514"," Clinic, obstetrics / gynecology(OB / GYN)"},
{"515"," Clinic, pediatric"},
{"516"," Clinic, urgent care"},
{"517"," Clinic, family practice"},
{"518"," Reserved clinic"},
{"519"," Clinic,"},
{"521"," Rural clinic"},
{"526"," Urgent care clinic"},
{"529"," Other"},
{"530"," Osteopathic services, general"},
{"531"," Osteopathic services, therapy"},
{"532"," Reserved osteopathic services"},
{"533"," Reserved osteopathic services"},
{"534"," Reserved osteopathic services"},
{"535"," Reserved osteopathic services"},
{"536"," Reserved osteopathic services"},
{"537"," Reserved osteopathic services"},
{"538"," Reserved osteopathic services"},
{"539"," Osteopathic services,"},
{"540"," Ambulance, general"},
{"541"," Ambulance, supplies"},
{"542"," Ambulance, medical transport"},
{"543"," Ambulance, heart mobile"},
{"544"," Ambulance, oxygen"},
{"545"," Ambulance, air"},
{"546"," Ambulance, neonatal services"},
{"547"," Ambulance, pharmacy"},
{"548"," Ambulance, electrocardiogram(EKG) transmission"},
{"549"," Ambulance,"},
{"550"," Skilled nursing"},
{"551"," Skilled nursing, visit"},
{"561"," Home health medical, social services, general"},
{"571"," Aide / home health visit"},
{"601"," Home health, oxygen, general"},
{"610"," Magnetic resonance technology, general"},
{"611"," Magnetic resonance technology, brain/brain stem"},
{"612"," Magnetic resonance technology, spinal cord/spine"},
{"613"," Magnetic resonance technology reserved"},
{"614"," Magnetic resonance technology, magnetic resonance imaging (MRI)"},
{"615"," Magnetic resonance technology, head and neck"},
{"616"," Magnetic resonance technology, lower extremities"},
{"617"," Magnetic resonance technology reserved"},
{"618"," Magnetic resonance technology,"},
{"619"," Magnetic resonance technology,"},
{"621"," Medical surgical supplies incidentals, radiology"},
{"622"," Medical surgical supplies incidental to  diagnostic services"},
{"624"," U.S.Food and Drug Administration (FDA) investigational devices"},
{"631"," Drug, single"},
{"632"," Drug, multi"},
{"634"," Pharmacy, extension of 025X erythropoietin (EPO) less than 10,000 units"},
{"635"," Pharmacy extension of 025X erythropoietin (EPO) 10,000 or more units"},
{"636"," Pharmacy 025x extension drugs requiring detailed coding"},
{"650"," Hospice"},
{"651"," Hospice, routine home care"},
{"652"," Hospice, continuous home care"},
{"656"," Hospice, general inpatient care (non-respite)"},
{"722"," Labor room delivery, delivery room"},
{"723"," Labor room delivery, circumcision"},
{"724"," Labor room delivery birthing center"},
{"729"," Labor room delivery,"},
{"730"," Electrocardiogram(EKG), electroencephalogram(EEG)"},
{"731"," Electrocardiogram(EKG), electroencephalogram(EEG), Holter monitor"},
{"732"," Telemetry"},
{"739"," Electrocardiogram(EKG), electroencephalogram(EEG),"},
{"740"," Electroencephalogram(EEG), general"},
{"741"," Reserved electroencephalogram(EEG)"},
{"742"," Reserved electroencephalogram(EEG)"},
{"743"," Reserved electroencephalogram(EEG)"},
{"744"," Reserved electroencephalogram(EEG)"},
{"745"," Reserved electroencephalogram(EEG)"},
{"746"," Reserved electroencephalogram(EEG)"},
{"747"," Reserved electroencephalogram(EEG)"},
{"748"," Reserved electroencephalogram(EEG)"},
{"749"," Reserved electroencephalogram(EEG)"},
{"750"," Gastrointestinal(GI) services general"},
{"751"," Reserved gastrointestinal(GI) services"},
{"752"," Reserved gastrointestinal(GI) services"},
{"753"," Reserved gastrointestinal(GI) services"},
{"754"," Reserved gastrointestinal(GI) services"},
{"755"," Reserved gastrointestinal(GI) services"},
{"756"," Reserved gastrointestinal(GI) services"},
{"757"," Reserved gastrointestinal(GI) services"},
{"758"," Reserved gastrointestinal(GI) services"},
{"759"," Reserved gastrointestinal(GI) services"},
{"760"," Specialty room, general"},
{"761"," Specialty room, treatment room"},
{"769"," Specialty room, rooms"},
{"770"," Preventive care services, general"},
{"771"," Preventive care services, vaccine administration"},
{"772"," Reserved preventive care services"},
{"773"," Reserved preventive care services"},
{"774"," Reserved preventive care services"},
{"775"," Reserved preventive care services"},
{"776"," Reserved preventive care services"},
{"777"," Reserved preventive care services"},
{"778"," Reserved preventive care services"},
{"779"," Reserved preventive care services"},
{"780"," Telemedicine, general"},
{"781"," Reserved telemedicine"},
{"782"," Reserved telemedicine"},
{"783"," Reserved telemedicine"},
{"784"," Reserved telemedicine"},
{"785"," Reserved telemedicine"},
{"786"," Reserved telemedicine"},
{"787"," Reserved telemedicine"},
{"788"," Reserved telemedicine"},
{"789"," Reserved telemedicine"},
{"790"," Extra - corporeal shock wave therapy, general"},
{"791"," Reserved extra-corporeal shock wave therapy"},
{"792"," Reserved extra-corporeal shock wave therapy"},
{"793"," Reserved extra-corporeal shock wave therapy"},
{"794"," Reserved extra-corporeal shock wave therapy"},
{"795"," Reserved extra-corporeal shock wave therapy"},
{"796"," Reserved extra-corporeal shock wave therapy"},
{"797"," Reserved extra-corporeal shock wave therapy"},
{"798"," Reserved extra-corporeal shock wave therapy"},
{"799"," Reserved extra-corporeal shock wave therapy"},
{"811"," Acquisition of body components, living donor"},
{"812"," Acquisition of body components, cadaver donor"},
{"813"," Acquisition of body components, unknown donor"},
{"814"," Acquisition of body components unsuccessful organ search, donor bank charges"},
{"819",", donor"},
{"820"," Hemodialysis, outpatient or home general"},
{"821"," Hemodialysis, composite or  rate"},
{"829"," Hemodialysis,"},
{"831"," Peritoneal dialysis, outpatient or home composite or  rate"},
{"851"," Continuous cycling peritoneal dialysis, outpatient or home composite or  rate"},
{"880"," Dialysis, miscellaneous"},
{"900"," Behavioral health treatment services, general"},
{"901"," Behavioral health treatment services, electroshock"},
{"902"," Behavioral health treatment services, milieu therapy"},
{"903"," Behavioral health treatment services, play therapy"},
{"904"," Behavioral health treatment services, activity Therapy"},
{"905"," Behavioral health treatment services, intensive outpatient psychiatric"},
{"906"," Behavioral health treatment services, intensive outpatient chemical dependency"},
{"907"," Behavioral health treatment services, community behavioral health program"},
{"908"," Reserved behavioral health treatment services"},
{"909"," Reserved behavioral health treatment services"},
{"910"," Reserved behavioral health treatment services, 090X extension"},
{"911"," Behavioral health treatment services, 090X extension: rehabilitation"},
{"912"," Behavioral health treatment services, 090X extension: partial hospitalization, less intensive"},
{"913"," Behavioral health treatment services, 090X extension: partial hospitalization, intensive"},
{"914"," Behavioral health treatment services, 090X extension: individual therapy"},
{"915"," Behavioral health treatment, services 090X extension: group therapy"},
{"916"," Behavioral health treatment, services 090X extension: family therapy"},
{"917"," Behavioral health treatment, services 090X extension: bio feedback"},
{"918"," Behavioral health treatment, services 090X extension: testing"},
{"919"," Behavioral health treatment, services 090X extension:"},
{"920"," diagnostic services, general"},
{"921"," diagnostic services, peripheral vascular laboratory"},
{"922"," diagnostic services, electromyelgram"},
{"923"," diagnostic services, pap smear"},
{"924"," diagnostic services, allergy test"},
{"925"," diagnostic services, pregnancy test"},
{"926"," Reserved , diagnostic services"},
{"927"," Reserved , diagnostic services"},
{"928"," Reserved , diagnostic services"},
{"929"," diagnostic services,"},
{"940"," therapeutic services, general"},
{"941"," therapeutic services, recreational therapy"},
{"942"," Education, training"},
{"943"," Cardiac rehabilitation"},
{"944"," therapeutic services, drug rehabilitation"},
{"945"," therapeutic services, alcohol rehabilitation"},
{"946"," therapeutic services, complex medical equipment – routine"},
{"947"," therapeutic services, complex medical equipment, ancillary"},
{"948"," Pulmonary rehabilitation"},
{"949"," therapeutic services,"},
{"950"," Reserved therapeutic services, 094X extension:"},
{"951"," therapeutic services, 094X extension: athletic training"},
{"952"," therapeutic services, 094X extension: kinesiapy"},
{"953"," Reserved therapeutic services, extension of 094X"},
{"954"," Reserved therapeutic services, extension of 094X"},
{"955"," Reserved therapeutic services, extension of 094X"},
{"956"," Reserved therapeutic services, extension of 094X"},
{"957"," Reserved , therapeutic services, extension of 094X"},
{"958"," Reserved , therapeutic services, extension of 094X"},
{"959"," Reserved , therapeutic services, extension of 094X"},
{"960"," Professional fee"},
{"961"," Professional fee, psychology"},
{"962"," Professional fee, eye"},
{"963"," Professional fee, anesthesiologist Medical Doctor"},
{"964"," Professional fee, anesthesiologist, certified registered nurse anesthetist (CRNA)"},
{"969",", professional fee"},
{"971"," Professional fee, laboratory"},
{"972"," Professional fee, radiology, diagnostic"},
{"975"," Professional fee, operating room"},
{"981"," Professional fee, emergency room"},
{"982"," Professional fee, outpatient"},
{"983"," Professional fee, clinic"},
{"984"," Professional fee, social services"},
{"985"," Professional fee, electrocardiogram (EKG)"},
{"986"," Professional fee, electroencephalography (EEG)"},
{"987"," Professional fee, house visit"},
{"988"," Professional fee, consultation"}        };

        public Dictionary<string, string> PlaceOfServices = new Dictionary<string, string>() {
            {
                "01", "Pharmacy"
            },
            {
                "02", "Telehealth Provided Other than in the Patient's Home"
            },
            {
                "03", "School"
            },
            {
                "04", "Homeless Shelter"
            },
            {
                "05", "Indian Health Service Free-standing Facility"
            },
            {
                "06", "Indian Health Service Provider-based Facility"
            },
            {
                "07", "Tribal 638 Free-standing Facility"
            },
            {
                "08", "Tribal 638 Provider-based Facility"
            },
            {
                "09", "Prison / Correctional Facility"
            },
            {
                "10", "Telehealth Provided in Patient's Home"
            },
            {
                "11", "Office"
            },
            {
                "12", "School"
            },
            {
                "13", "Assisted Living Facility"
            },
            {
                "14", "Group Home"
            },
            {
                "15", "Mobile Unit"
            },
            {
                "16", "Temporary Lodging"
            },
            {
                "17", "Walk-in Retail Health Clinic"
            },
            {
                "18", "Place of Employment-Worksite"
            },
            {
                "19", "Off-Campus Outpatient Hospital"
            },
            {
                "20", "Urget Care Facility"
            },
            {
                "21", "Inaptient Hospital"
            },
            {
                "22", "On-Campus Outpatient Hospital"
            },
            {
                "23", "Emergency Room - Hospital"
            },
            {
                "24", "Ambulatory Surgical Center"
            },
            {
                "25", "Birthing Center"
            },
            {
                "26", "Military Treatment Facility"
            },
            {
                "31", "Skilled Nursing Facility"
            },
            {
                "32", "Nursing Facility"
            },
            {
                "33", "Custodial Care Facility"
            },
            {
                "34", "Hospice"
            },
            {
                "41", "Ambulance - Land"
            },
            {
                "42", "Ambulance - Air or Water"
            },
            {
                "49", "Independent Clinic"
            },
            {
                "50", "Federally Qualified Health Center"
            },
            {
                "51", "Inaptient Psychiatric Facility"
            },
            {
                "52", "Psychiatric Facility-Partial Hospitalization"
            },
            {
                "53", "Community Mental Health Center"
            },
            {
                "54", "Intermediate Care Facility / Individuals with Intellectual Disabilities"
            },
            {
                "55", "Residential Substance Abuse Treatment Facility"
            },
            {
                "56", "Psychiatric Residential Treatment Center"
            },
            {
                "57", "Non-residential Substance Abuse Treatment Facility"
            },
            {
                "58", "Non-residential Opioid Treatment Facility"
            },
            {
                "60", "Mass Immunization Center"
            },
            {
                "61", "Comprehensive Inpatient Rehabilitation Facility"
            },
            {
                "62", "Comprehensive Outpatient Rehabilitation Facility"
            },
            {
                "65", "End-Stage Renal Disease Treatment Facility"
            },
            {
                "71", "Public Health Clinic"
            },
            {
                "72", "Rural Health Clinic"
            },
            {
                "81", "Independent Laboratory"
            },
            {
                "99", "Other Place of Service"
            }
        };

        public Dictionary<string, string> Diagnosis = new Dictionary<string, string>() {
      {
         "F84.0", "Autistic disorder"
      },
      {
         "F39", "Unspecified mood affective disorder"
      },
      {
         "F42", "Obsessive-compulsive disorder"
      },
      {
         "F53", "Puerperal psychosis"
      },
      {
         "F54", "Psychological and behavioral factors associated with disorders or diseases classified elsewhere"
      },
      {
         "F59", "Unspecified behavioral syndromes associated with physiological disturbances and physical factors"
      },
      {
         "F66", "Other sexual disorders"
      },
      {
         "F69", "Unspecified disorder of adult personality and behavior"
      },
      {
         "F70", "Mild intellectual disabilities"
      },
      {
         "F71", "Moderate intellectual disabilities"
      },
      {
         "F72", "Severe intellectual disabilities"
      },
      {
         "F73", "Profound intellectual disabilities"
      },
      {
         "F78", "Other intellectual disabilities"
      },
      {
         "F79", "Unspecified intellectual disabilities"
      },
      {
         "F82", "Specific developmental disorder of motor function"
      },
      {
         "F88", "Other disorders of psychological development"
      },
      {
         "F89", "Unspecified disorder of psychological development"
      },
      {
         "F99", "Mental disorder, not otherwise specified"
      },
      {
         "F30.2", "Manic episode, severe with psychotic symptoms"
      },
      {
         "F30.3", "Manic episode in partial remission"
      },
      {
         "F30.4", "Manic episode in full remission"
      },
      {
         "F30.8", "Other manic episodes"
      },
      {
         "F30.9", "Manic episode, unspecified"
      },
      {
         "F31.0", "Bipolar disorder, current episode hypomanic"
      },
      {
         "F31.2", "Bipolar disorder, current episode manic severe with psychotic features"
      },
      {
         "F31.4", "Bipolar disorder, current episode depressed, severe, without psychotic features"
      },
      {
         "F31.5", "Bipolar disorder, current episode depressed, severe, with psychotic features"
      },
      {
         "F31.9", "Bipolar disorder, unspecified"
      },
      {
         "F32.0", "Major depressive disorder, single episode, mild"
      },
      {
         "F32.1", "Major depressive disorder, single episode, moderate"
      },
      {
         "F32.2", "Major depressive disorder, single episode, severe without psychotic features"
      },
      {
         "F32.3", "Major depressive disorder, single episode, severe with psychotic features"
      },
      {
         "F32.4", "Major depressive disorder, single episode, in partial remission"
      },
      {
         "F32.5", "Major depressive disorder, single episode, in full remission"
      },
      {
         "F32.8", "Other depressive episodes"
      },
      {
         "F32.9", "Major depressive disorder, single episode, unspecified"
      },
      {
         "F33.0", "Major depressive disorder, recurrent, mild"
      },
      {
         "F33.1", "Major depressive disorder, recurrent, moderate"
      },
      {
         "F33.2", "Major depressive disorder, recurrent severe without psychotic features"
      },
      {
         "F33.3", "Major depressive disorder, recurrent, severe with psychotic symptoms"
      },
      {
         "F33.8", "Other recurrent depressive disorders"
      },
      {
         "F33.9", "Major depressive disorder, recurrent, unspecified"
      },
      {
         "F34.0", "Cyclothymic disorder"
      },
      {
         "F34.1", "Dysthymic disorder Y"
      },
      {
         "F34.8", "Other persistent mood affective disorders"
      },
      {
         "F34.9", "Persistent mood affective disorder, unspecified"
      },
      {
         "F40.8", "Other phobic anxiety disorders"
      },
      {
         "F40.9", "Phobic anxiety disorder, unspecified"
      },
      {
         "F41.0", "Panic disorder episodic paroxysmal anxiety"
      },
      {
         "F41.1", "Generalized anxiety disorder"
      },
      {
         "F41.3", "Other mixed anxiety disorders"
      },
      {
         "F41.8", "Other specified anxiety disorders"
      },
      {
         "F41.9", "Anxiety disorder, unspecified"
      },
      {
         "F42.2", "Mixed obsessional thoughts and acts"
      },
      {
         "F42.3", "Hoarding disorder"
      },
      {
         "F42.4", "Excoriation (skin-picking) disorder"
      },
      {
         "F42.8", "Other obsessive-compulsive disorder"
      },
      {
         "F42.9", "Obsessive-compulsive disorder, unspecified"
      },
      {
         "F43.0", "Acute stress reaction"
      },
      {
         "F43.8", "Other reactions to severe stress"
      },
      {
         "F43.9", "Reaction to severe stress, unspecified"
      },
      {
         "F44.0", "Dissociative amnesia"
      },
      {
         "F44.1", "Dissociative fugue"
      },
      {
         "F44.2", "Dissociative stupor"
      },
      {
         "F44.4", "Conversion disorder with motor symptom or deficit"
      },
      {
         "F44.5", "Conversion disorder with seizures or convulsions"
      },
      {
         "F44.6", "Conversion disorder with sensory symptom or deficit"
      },
      {
         "F44.7", "Conversion disorder with mixed symptom presentation"
      },
      {
         "F44.9", "Dissociative and conversion disorder, unspecified"
      },
      {
         "F45.0", "Somatization disorder"
      },
      {
         "F45.1", "Undifferentiated somatoform disorder"
      },
      {
         "F45.8", "Other somatoform disorders"
      },
      {
         "F45.9", "Somatoform disorder, unspecified"
      },
      {
         "F48.1", "Depersonalization-derealization syndrome"
      },
      {
         "F48.2", "Pseudobulbar affect"
      },
      {
         "F48.8", "Other specified nonpsychotic mental disorders"
      },
      {
         "F48.9", "Nonpsychotic mental disorder, unspecified"
      },
      {
         "F50.2", "Bulimia nervosa"
      },
      {
         "F50.8", "Other eating disorders"
      },
      {
         "F50.9", "Eating disorder, unspecified"
      },
      {
         "F51.3", "Sleepwalking somnambulism"
      },
      {
         "F51.4", "Sleep terrors night terrors"
      },
      {
         "F51.5", "Nightmare disorder"
      },
      {
         "F51.8", "Other sleep disorders not due to a substance or known physiological condition"
      },
      {
         "F51.9", "Sleep disorder not due to a substance or known physiological condition, unspecified"
      },
      {
         "F52.0", "Hypoactive sexual desire disorder"
      },
      {
         "F52.1", "Sexual aversion disorder"
      },
      {
         "F52.4", "Premature ejaculation"
      },
      {
         "F52.5", "Vaginismus not due to a substance or known physiological condition"
      },
      {
         "F52.6", "Dyspareunia not due to a substance or known physiological condition"
      },
      {
         "F52.8", "Other sexual dysfunction not due to a substance or known physiological condition"
      },
      {
         "F52.9", "Unspecified sexual dysfunction not due to a substance or known physiological condition"
      },
      {
         "F53.0", "Postpartum depression"
      },
      {
         "F53.1", "Puerperal psychosis"
      },
      {
         "F55.0", "Abuse of antacids"
      },
      {
         "F55.1", "Abuse of herbal or folk remedies"
      },
      {
         "F55.2", "Abuse of laxatives"
      },
      {
         "F55.3", "Abuse of steroids or hormones"
      },
      {
         "F55.4", "Abuse of vitamins"
      },
      {
         "F55.8", "Abuse of other non-psychoactive substances"
      },
      {
         "F60.0", "Paranoid personality disorder"
      },
      {
         "F60.1", "Schizoid personality disorder"
      },
      {
         "F60.2", "Antisocial personality disorder"
      },
      {
         "F60.3", "Borderline personality disorder"
      },
      {
         "F60.4", "Histrionic personality disorder"
      },
      {
         "F60.5", "Obsessive-compulsive personality disorder"
      },
      {
         "F60.6", "Avoidant personality disorder"
      },
      {
         "F60.7", "Dependent personality disorder"
      },
      {
         "F60.9", "Personality disorder, unspecified"
      },
      {
         "F63.0", "Pathological gambling"
      },
      {
         "F63.1", "Pyromania"
      },
      {
         "F63.2", "Kleptomania"
      },
      {
         "F63.3", "Trichotillomania"
      },
      {
         "F63.9", "Impulse disorder, unspecified"
      },
      {
         "F64.0", "Transsexualism"
      },
      {
         "F64.1", "Dual role transvestism"
      },
      {
         "F64.2", "Gender identity disorder of childhood"
      },
      {
         "F64.8", "Other gender identity disorders"
      },
      {
         "F64.9", "Gender identity disorder, unspecified"
      },
      {
         "F65.0", "Fetishism"
      },
      {
         "F65.1", "Transvestic fetishism"
      },
      {
         "F65.2", "Exhibitionism"
      },
      {
         "F65.3", "Voyeurism"
      },
      {
         "F65.4", "Pedophilia"
      },
      {
         "F65.9", "Paraphilia, unspecified"
      },
      {
         "F68.8", "Other specified disorders of adult personality and behavior"
      },
      {
         "F68.A", "Factitious disorder imposed on another"
      },
      {
         "F80.0", "Phonological disorder"
      },
      {
         "F80.1", "Expressive language disorder"
      },
      {
         "F80.2", "Mixed receptive-expressive language disorder"
      },
      {
         "F80.4", "Speech and language development delay due to hearing loss"
      },
      {
         "F80.9", "Developmental disorder of speech and language, unspecified"
      },
      {
         "F81.0", "Specific reading disorder"
      },
      {
         "F81.2", "Mathematics disorder"
      },
      {
         "F81.9", "Developmental disorder of scholastic skills, unspecified"
      },
      {
         "F84.2", "Rett's syndrome"
      },
      {
         "F84.3", "Other childhood disintegrative disorder"
      },
      {
         "F84.5", "Asperger's syndrome"
      },
      {
         "F84.8", "Other pervasive developmental disorders"
      },
      {
         "F84.9", "Pervasive developmental disorder, unspecified"
      },
      {
         "F90.0", "Attention-deficit hyperactivity disorder, predominantly inattentive type"
      },
      {
         "F90.1", "Attention-deficit hyperactivity disorder, predominantly hyperactive type"
      },
      {
         "F90.2", "Attention-deficit hyperactivity disorder, combined type"
      },
      {
         "F90.8", "Attention-deficit hyperactivity disorder, other type"
      },
      {
         "F90.9", "Attention-deficit hyperactivity disorder, unspecified type"
      },
      {
         "F91.0", "Conduct disorder confined to family context"
      },
      {
         "F91.1", "Conduct disorder, childhood-onset type"
      },
      {
         "F91.2", "Conduct disorder, adolescent-onset type"
      },
      {
         "F91.3", "Oppositional defiant disorder"
      },
      {
         "F91.8", "Other conduct disorders"
      },
      {
         "F91.9", "Conduct disorder, unspecified"
      },
      {
         "F93.0", "Separation anxiety disorder of childhood"
      },
      {
         "F93.8", "Other childhood emotional disorders"
      },
      {
         "F93.9", "Childhood emotional disorder, unspecified"
      },
      {
         "F94.0", "Selective mutism"
      },
      {
         "F94.1", "Reactive attachment disorder of childhood"
      },
      {
         "F94.2", "Disinhibited attachment disorder of childhood"
      },
      {
         "F94.8", "Other childhood disorders of social functioning"
      },
      {
         "F94.9", "Childhood disorder of social functioning, unspecified"
      },
      {
         "F95.0", "Transient tic disorder"
      },
      {
         "F95.1", "Chronic motor or vocal tic disorder"
      },
      {
         "F95.2", "Tourette's disorder"
      },
      {
         "F95.8", "Other tic disorders"
      },
      {
         "F95.9", "Tic disorder, unspecified"
      },
      {
         "F98.0", "Enuresis not due to a substance or known physiological condition"
      },
      {
         "F98.1", "Encopresis not due to a substance or known physiological condition"
      },
      {
         "F98.3", "Pica of infancy and childhood"
      },
      {
         "F98.4", "Stereotyped movement disorders"
      },
      {
         "F98.5", "Adult onset fluency disorder"
      },
      {
         "F98.8", "Other specified behavioral and emotional disorders with onset usually occurring in childhood and adolescence"
      },
      {
         "F98.9", "Unspecified behavioral and emotional disorders with onset usually occurring in childhood and adolescence"
      },
      {
         "F30.10", "Manic episode without psychotic symptoms, unspecified"
      },
      {
         "F30.11", "Manic episode without psychotic symptoms, mild"
      },
      {
         "F30.12", "Manic episode without psychotic symptoms, moderate"
      },
      {
         "F30.13", "Manic episode, severe, without psychotic symptoms"
      },
      {
         "F31.10", "Bipolar disorder, current episode manic without psychotic features, unspecified"
      },
      {
         "F31.11", "Bipolar disorder, current episode manic without psychotic features, mild"
      },
      {
         "F31.12", "Bipolar disorder, current episode manic without psychotic features, moderate"
      },
      {
         "F31.13", "Bipolar disorder, current episode manic without psychotic features, severe"
      },
      {
         "F31.30", "Bipolar disorder, current episode depressed, mild or moderate severity, unspecified"
      },
      {
         "F31.31", "Bipolar disorder, current episode depressed, mild"
      },
      {
         "F31.32", "Bipolar disorder, current episode depressed, moderate"
      },
      {
         "F31.60", "Bipolar disorder, current episode mixed, unspecified"
      },
      {
         "F31.61", "Bipolar disorder, current episode mixed, mild"
      },
      {
         "F31.62", "Bipolar disorder, current episode mixed, moderate"
      },
      {
         "F31.63", "Bipolar disorder, current episode mixed, severe, without psychotic features"
      },
      {
         "F31.64", "Bipolar disorder, current episode mixed, severe, with psychotic features"
      },
      {
         "F31.70", "Bipolar disorder, currently in remission, most recent episode unspecified"
      },
      {
         "F31.71", "Bipolar disorder, in partial remission, most recent episode hypomanic"
      },
      {
         "F31.72", "Bipolar disorder, in full remission, most recent episode hypomanic"
      },
      {
         "F31.73", "Bipolar disorder, in partial remission, most recent episode manic"
      },
      {
         "F31.74", "Bipolar disorder, in full remission, most recent episode manic"
      },
      {
         "F31.75", "Bipolar disorder, in partial remission, most recent episode depressed"
      },
      {
         "F31.76", "Bipolar disorder, in full remission, most recent episode depressed"
      },
      {
         "F31.77", "Bipolar disorder, in partial remission, most recent episode mixed"
      },
      {
         "F31.78", "Bipolar disorder, in full remission, most recent episode mixed"
      },
      {
         "F31.81", "Bipolar II disorder"
      },
      {
         "F31.89", "Other bipolar disorder"
      },
      {
         "F32.81", "Premenstrual dysphoric disorder"
      },
      {
         "F32.89", "Other specified depressive episodes"
      },
      {
         "F33.40", "Major depressive disorder, recurrent, in remission, unspecified"
      },
      {
         "F33.41", "Major depressive disorder, recurrent, in partial remission"
      },
      {
         "F33.42", "Major depressive disorder, recurrent, in full remission"
      },
      {
         "F34.81", "Disruptive mood dysregulation disorder"
      },
      {
         "F34.89", "Other specified persistent mood disorders"
      },
      {
         "F40.00", "Agoraphobia, unspecified"
      },
      {
         "F40.01", "Agoraphobia with panic disorder"
      },
      {
         "F40.02", "Agoraphobia without panic disorder"
      },
      {
         "F40.10", "Social phobia, unspecified"
      },
      {
         "F40.11", "Social phobia, generalized"
      },
      {
         "F43.10", "Post-traumatic stress disorder, unspecified"
      },
      {
         "F43.11", "Post-traumatic stress disorder, acute"
      },
      {
         "F43.12", "Post-traumatic stress disorder, chronic"
      },
      {
         "F43.20", "Adjustment disorder, unspecified"
      },
      {
         "F43.21", "Adjustment disorder with depressed mood"
      },
      {
         "F43.22", "Adjustment disorder with anxiety"
      },
      {
         "F43.23", "Adjustment disorder with mixed anxiety and depressed mood"
      },
      {
         "F43.24", "Adjustment disorder with disturbance of conduct"
      },
      {
         "F43.25", "Adjustment disorder with mixed disturbance of emotions and conduct"
      },
      {
         "F43.29", "Adjustment disorder with other symptoms"
      },
      {
         "F44.81", "Dissociative identity disorder"
      },
      {
         "F44.89", "Other dissociative and conversion disorders"
      },
      {
         "F45.20", "Hypochondriacal disorder, unspecified"
      },
      {
         "F45.21", "Hypochondriasis"
      },
      {
         "F45.22", "Body dysmorphic disorder"
      },
      {
         "F45.29", "Other hypochondriacal disorders"
      },
      {
         "F45.41", "Pain disorder exclusively related to psychological factors"
      },
      {
         "F45.42", "Pain disorder with related psychological factors"
      },
      {
         "F50.00", "Anorexia nervosa, unspecified"
      },
      {
         "F50.01", "Anorexia nervosa, restricting type"
      },
      {
         "F50.02", "Anorexia nervosa, binge eating/purging type"
      },
      {
         "F50.81", "Binge eating disorder"
      },
      {
         "F50.82", "Avoidant/restrictive food intake disorder"
      },
      {
         "F50.89", "Other specified eating disorder"
      },
      {
         "F51.01", "Primary insomnia"
      },
      {
         "F51.02", "Adjustment insomnia"
      },
      {
         "F51.03", "Paradoxical insomnia"
      },
      {
         "F51.04", "Psychophysiologic insomnia"
      },
      {
         "F51.05", "Insomnia due to other mental disorder"
      },
      {
         "F51.09", "Other insomnia not due to a substance or known physiological condition"
      },
      {
         "F51.11", "Primary hypersomnia"
      },
      {
         "F51.12", "Insufficient sleep syndrome"
      },
      {
         "F51.13", "Hypersomnia due to other mental disorder"
      },
      {
         "F51.19", "Other hypersomnia not due to a substance or known physiological condition"
      },
      {
         "F52.21", "Male erectile disorder"
      },
      {
         "F52.22", "Female sexual arousal disorder"
      },
      {
         "F52.31", "Female orgasmic disorder"
      },
      {
         "F52.32", "Male orgasmic disorder"
      },
      {
         "F60.81", "Narcissistic personality disorder"
      },
      {
         "F60.89", "Other specific personality disorders"
      },
      {
         "F63.81", "Intermittent explosive disorder"
      },
      {
         "F63.89", "Other impulse disorders"
      },
      {
         "F65.50", "Sadomasochism, unspecified"
      },
      {
         "F65.51", "Sexual masochism"
      },
      {
         "F65.52", "Sexual sadism"
      },
      {
         "F65.81", "Frotteurism"
      },
      {
         "F65.89", "Other paraphilias"
      },
      {
         "F68.10", "Factitious disorder imposed on self, unspecified"
      },
      {
         "F68.11", "Factitious disorder imposed on self, with predominantly psychological signs and symptoms"
      },
      {
         "F68.12", "Factitious disorder imposed on self, with predominantly physical signs and symptoms"
      },
      {
         "F68.13", "Factitious disorder imposed on self, with combined psychological and physical signs and symptoms"
      },
      {
         "F80.81", "Childhood onset fluency disorder"
      },
      {
         "F80.82", "Social pragmatic communication disorder"
      },
      {
         "F80.89", "Other developmental disorders of speech and language"
      },
      {
         "F81.81", "Disorder of written expression"
      },
      {
         "F81.89", "Other developmental disorders of scholastic skills"
      },
      {
         "F98.21", "Rumination disorder of infancy"
      },
      {
         "F98.29", "Other feeding disorders of infancy and early childhood"
      }
    };

        public Dictionary<string, string> DiagnosisTypes = new Dictionary<string, string>() {
            {
                "ABF", "ICD-10-CM Diagnosis"
            },
            {
                "ABJ", "ICD-10-CM Admitting Diagnosis"
            },
            {
                "ABK", "ICD-10-CM Principal Diagnosis"
            },
            {
                "APR", "ICD-10-CM Patient's Reason for Visit"
            },
            {
                "BF", "ICD-9-CM Diagnosis"
            },
            {
                "BJ", "ICD-9-CM Admitting Diagnosis"
            },
            {
                "BK", "ICD-9-CM Principal Diagnosis"
            },
            {
                "DR", "Diagnosis Related Group (DRG)"
            },
            {
                "LOI", "Logical Observation Identifier Names and Codes"
            },
            {
                "PR", "ICD-9-CM Patient's Reason for Visit"
            }
        };

        #endregion

        #region CFARS

        public List<string> CfarsSeverityRatings = new List<string>()
        {
            "1: No Problem","2: Less than Slight","3: Slight Problem","4: Slight to Moderate","5: Moderate Problem","6: Moderate to Severe","7: Severe Problem","8: Severe to Extreme","9: Extreme"
        };

        public List<string> EducationalCategory = new List<string>()
        {
            "Non-degree tech","AA degree tech","Unlicensed Bachelor's Degree","Unlicensed Master's Degree","Licensed CSW/MFT/MHC/AARNP//PA","Ph. D Ed. D or Licensed Psychologist","M.D., D.O., Licensed Board Certified Psychiatrist"
        };

        public List<string> Depression = new List<string>()
        {
            "Depression Mood","An hedonic","Sad","Worthless","Hopeless","Happy","Lonely","Sleep Problems","Anti-depression Mood"
        };

        public List<string> HyperAffect = new List<string>(){
            "Manic","Sleep Deficit","Pressured Speech","Elevated Mood","Overactive","Relaxed","Agitated","Mood Swings","Anti-manic Meds"
        };

        public List<string> CognitivePerformance = new List<string>()
        {
            "Poor Memory","Short Attention","Insightful","Not Oriented to Person","Now Oriented to Time","Low Self-Awareness","Developmental Disability","Poor Concentration",
            "Not Oriented to Place","Not Oriented to Circumstance","Impaired Judgment","Slow Processing","Oriented Times 4"
        };

        public List<string> TraumaticStress = new List<string>()
        {
            "Acute","Chronic","Avoidant","Upsetting Memories","Dreams/Nightmares","Detached","Repression/Amnesia"
        };

        public List<string> InterpersonalRelationships = new List<string>()
        {
            "Problems with Friends","Poor Social Skills","Adequate Social Skills","Difficulty Establishing","Difficulty Maintaining","Supportive"
        };

        public List<string> FamilyEnvironment = new List<string>()
        {
            "Family Instability","Family Legal Problems","Single Parent","Separation","Stable Home","Birth in Family","Custody Problem","Divorce","Death in Family"
        };

        public List<string> WorkSchool = new List<string>()
        {
            "Absenteeism","Dropped Out","Employed","Disabled","Poor Performance","Seeking Employment","Doesn't Read/Write","Learning Disabilities","Attends School","Tardiness","Not Employed"
        };

        public List<string> AbilityToCareForSelf = new List<string>()
        {
            "Able to Care for Self","Suffers from Neglect","Not Able to Survive without Help","Risk of Harm","Refuses to Care for Self","Alternative Care Not Available"
        };

        public List<string> DangerToOthers = new List<string>()
        {
            "Violent Temper","Physical Abuser","Hostile","Assaultive","Threatens Others","Homicidal Ideations","Homicidal Threats","Homicide Attempt"
        };

        public List<string> Anxiety = new List<string>()
        {
            "Anxious","Tense","Obsessive","Calm","Fearful","Panic","Guilt","Anti-anxiety Meds"
        };

        public List<string> ThoughtProcess = new List<string>()
        {
            "Illogical","Paranoid","Derailed Thinking","Delusional","Ruminative","Loose Associations","Hallucinations","Intact","Anti-psych Medication"
        };

        public List<string> MedicalPhysical = new List<string>()
        {
            "Acute Illness","CNS Disorder","Pregnant","Eating Disorder","Handicap or Permanent Disability","Chronic Illness","Poor Nutrition","Seizures","Good Health",
            "Need Medication","Need Dental Care","Enuretic/Encopretic","Stress-related Illness"
        };

        public List<string> SubstanceAbuse = new List<string>()
        {
            "Alcohol Abuse","DUI","Recovery","Drug(s)","Family History","Abstinent","Interfere with Functioning","Dependence","Cravings/Urges","Medication Control","I.V. Drugs"
        };

        public List<string> FamilyRelationships = new List<string>()
        {
            "No Contact with Family","Difficulty with Partner","Conflict with Relative","Poor Parenting Skills","Acting Out","Difficulty with Child"
        };

        public List<string> SocioLegal = new List<string>()
        {
            "Disregards Rules","Dishonesty","Property Offense","Probation","Use/Con Other(s)","Person Offense","Pending Charges","Reliable"
        };

        public List<string> AdlFunctioning = new List<string>()
        {
            "Money Management","Personal Hygiene Problems","Problem Obtaining/Maintain","Meal Preparation Difficulties","Transportation Problems","Problem Obtaining/Maintaing Housing"
        };

        public List<string> DangerToSelf = new List<string>()
        {
            "Suicidal Ideations","Past Attempt","Current Plan","Self-Injury","Recent Attempt","Self-Mutilation"
        };

        public List<string> SecurityManagementNeeds = new List<string>()
        {
            "Home without Supervision","Behavioral Contract","Protection From Others","Home with Supervision","Restraint","Suicide Watch","Locked Unit","Seclusion","Run/Escape Risk","Involuntary Exam/Commitment"
        };

        #endregion

        public List<string> WidgetStyles = new List<string>()
        {
            "Assignments", "Authorizations", "Billing", "Clients", "Employees", "Files", "Intakes", "Policies", "Supervisions"
        };

        public List<string> Medications = new List<string>()
        {
            "Acetaminophen",
            "Acyclovir",
            "Adalimumab",
            "Albuterol",
            "Albuterol Sulfate",
            "Alendronate Sodium",
            "Alfuzosin Hydrochloride",
            "Allopurinol",
            "Alprazolam",
            "Amiodarone Hydrochloride",
            "Amitriptyline",
            "Amlodipine",
            "Amlodipine Besylate",
            "Amoxicillin",
            "Amphetamine",
            "Amphetamine Aspartate",
            "Anastrozole",
            "Apixaban",
            "Aripiprazole",
            "Aspirin",
            "Atenolol",
            "Atomoxetine Hydrochloride",
            "Atorvastatin",
            "Azelastine Hydrochloride",
            "Azithromycin",
            "Bacitracin",
            "Baclofen",
            "Beclomethasone",
            "Benazepril Hydrochloride",
            "Benzonatate",
            "Benztropine Mesylate",
            "Betamethasone Dipropinate",
            "Bimatoprost",
            "Bisoprolol Fumarate",
            "Brimonidine Tartrate",
            "Budesonide",
            "Bumetanide",
            "Buprenorphine",
            "Bupropion",
            "Buspirone Hydrochloride",
            "Butalbital",
            "Calcitriol",
            "Calcium",
            "Cholecalciferol",
            "Canagliflozin",
            "Carbamazepine",
            "Carbidopa",
            "Levodopa",
            "Carisoprodol",
            "Carvedilol",
            "Cefdinir",
            "Celecoxib",
            "Cephalexin",
            "Cetirizine",
            "Chlorhexidine",
            "Chlorthalidone",
            "Ciprofloxacin",
            "Citalopram",
            "Clavulanate Potassium",
            "Clindamycin",
            "Clobetasol Propionate",
            "Clonazepam",
            "Clonidine",
            "Clopidogrel Bisulfate",
            "Clotrimazole",
            "Codeine Phosphate",
            "Colchicine",
            "Conjugated Estrogens",
            "Cyanocobalamin",
            "Cyclobenzaprine",
            "Cyclosporine",
            "Dapagliflozin",
            "Desogestrel",
            "Desvenlafaxine",
            "Dexlansoprazole",
            "Dexmethylphenidate Hydrochloride",
            "Dextroamphetamine",
            "Dextroamphetamine Saccharate",
            "Diazepam",
            "Diclofenac",
            "Dicyclomine Hydrochloride",
            "Digoxin",
            "Diltiazem Hydrochloride",
            "Diphenhydramine Hydrochloride",
            "Divalproex Sodium",
            "Docusate",
            "Donepezil Hydrochloride",
            "Dorzolamide Hydrochloride",
            "Doxazosin Mesylate",
            "Doxepin Hydrochloride",
            "Doxycycline",
            "Drospirenone",
            "Dulaglutide",
            "Duloxetine",
            "Dutasteride",
            "Empagliflozin",
            "Emtricitabine",
            "Enalapril Maleate",
            "Enoxaparin Sodium",
            "Epinephrine",
            "Ergocalciferol",
            "Erythromycin",
            "Escitalopram Oxalate",
            "Esomeprazole",
            "Estradiol",
            "Estrogens, Conjugated",
            "Eszopiclone",
            "Ethinyl Estradiol",
            "Etonogestrel",
            "Exenatide",
            "Ezetimibe",
            "Simvastatin",
            "Famotidine",
            "Fenofibrate",
            "Fentanyl",
            "Ferrous Sulfate",
            "Fexofenadine Hydrochloride",
            "Finasteride",
            "Flecainide Acetate",
            "Fluconazole",
            "Fluoxetine Hydrochloride",
            "Fluticasone",
            "Fluticasone Propionate",
            "Folic Acid",
            "Formoterol",
            "Formoterol Fumarate",
            "Furosemide",
            "Gabapentin",
            "Gemfibrozil",
            "Glimepiride",
            "Glipizide",
            "Glyburide",
            "Guaifenesin",
            "Guanfacine",
            "Haloperidol",
            "Hydralazine Hydrochloride",
            "Hydrochlorothiazide",
            "Hydrocodone Bitartrate",
            "Hydrocortisone",
            "Hydromorphone Hydrochloride",
            "Hydroxychloroquine Sulfate",
            "Hydroxyzine",
            "Ibuprofen",
            "Indomethacin",
            "Insulin Aspart",
            "Insulin Degludec",
            "Insulin Detemir",
            "Insulin Glargine",
            "Insulin Human",
            "Insulin Lispro",
            "Ipratropium",
            "Ipratropium Bromide",
            "Irbesartan",
            "Isosorbide Mononitrate",
            "Ketoconazole",
            "Ketorolac Tromethamine",
            "Labetalol",
            "Lamotrigine",
            "Lansoprazole",
            "Latanoprost",
            "Levetiracetam",
            "Levocetirizine Dihydrochloride",
            "Levofloxacin",
            "Levonorgestrel",
            "Levothyroxine",
            "Lidocaine",
            "Linaclotide",
            "Linagliptin",
            "Liothyronine Sodium",
            "Liraglutide",
            "Lisdexamfetamine Dimesylate",
            "Lisinopril",
            "Lithium",
            "Loratadine",
            "Lorazepam",
            "Losartan Potassium",
            "Lovastatin",
            "Lurasidone Hydrochloride",
            "Magnesium",
            "Meclizine Hydrochloride",
            "Medroxyprogesterone",
            "Medroxyprogesterone Acetate",
            "Meloxicam",
            "Memantine Hydrochloride",
            "Menthol",
            "Mesalamine",
            "Metformin Hydrochloride",
            "Methimazole",
            "Methocarbamol",
            "Methotrexate",
            "Methylcellulose (4000 Mpa.S)",
            "Methylphenidate",
            "Methylprednisolone",
            "Metoclopramide Hydrochloride",
            "Metoprolol",
            "Metronidazole",
            "Minocycline Hydrochloride",
            "Mirabegron",
            "Mirtazapine",
            "Mometasone",
            "Mometasone Furoate",
            "Montelukast",
            "Morphine",
            "Mupirocin",
            "Mycophenolate Mofetil",
            "Naphazoline Hydrochloride",
            "Naproxen",
            "Naloxone",
            "Nebivolol Hydrochloride",
            "Neomycin",
            "Niacin",
            "Nifedipine",
            "Nitrofurantoin",
            "Nitroglycerin",
            "Norethindrone",
            "Norgestimate",
            "Norgestrel",
            "Nortriptyline Hydrochloride",
            "Nystatin",
            "Ofloxacin",
            "Olanzapine",
            "Olmesartan Medoxomil",
            "Olopatadine",
            "Omega-3-acid Ethyl Esters",
            "Omeprazole",
            "Ondansetron",
            "Oseltamivir Phosphate",
            "Oxcarbazepine",
            "Oxybutynin",
            "Oxycodone",
            "Pancrelipase Amylase",
            "Pancrelipase Lipase",
            "Pancrelipase Protease",
            "Pantoprazole Sodium",
            "Paroxetine",
            "Penicillin V",
            "Pheniramine Maleate",
            "Phentermine",
            "Phenytoin",
            "Pioglitazone",
            "Polyethylene Glycol 3350",
            "Polymyxin B",
            "Potassium",
            "Pramipexole Dihydrochloride",
            "Pravastatin Sodium",
            "Prazosin Hydrochloride",
            "Prednisolone",
            "Prednisone",
            "Pregabalin",
            "Primidone",
            "Progesterone",
            "Promethazine Hydrochloride",
            "Propranolol Hydrochloride",
            "Pseudoephedrine Hydrochloride",
            "Quetiapine Fumarate",
            "Quinapril",
            "Rabeprazole Sodium",
            "Ramipril",
            "Ranitidine",
            "Ranolazine",
            "Risperidone",
            "Rivaroxaban",
            "Rizatriptan Benzoate",
            "Ropinirole Hydrochloride",
            "Rosuvastatin",
            "Salmeterol Xinafoate",
            "Sennosides",
            "Sertraline Hydrochloride",
            "Sildenafil",
            "Sitagliptin Phosphate",
            "Sodium",
            "Sodium Fluoride",
            "Solifenacin Succinate",
            "Sotalol Hydrochloride",
            "Spironolactone",
            "Sucralfate",
            "Sulfamethoxazole",
            "Sumatriptan",
            "Tadalafil",
            "Tamoxifen Citrate",
            "Tamsulosin Hydrochloride",
            "Telmisartan",
            "Temazepam",
            "Terazosin",
            "Testosterone",
            "Thyroid",
            "Timolol",
            "Timolol Maleate",
            "Tiotropium",
            "Tizanidine",
            "Tolterodine Tartrate",
            "Topiramate",
            "Torsemide",
            "Tramadol Hydrochloride",
            "Travoprost",
            "Trazodone Hydrochloride",
            "Tretinoin",
            "Triamcinolone",
            "Triamterene",
            "Triazolam",
            "Trimethoprim",
            "Valacyclovir",
            "Valsartan",
            "Venlafaxine",
            "Verapamil Hydrochloride",
            "Vilazodone Hydrochloride",
            "Warfarin",
            "Ziprasidone",
            "Zolpidem Tartrate"


        };

        public List<string> WidgetViews = new List<string>()
        {
            "card","chart-bar"
        };

        public List<string> AllowedDocs = new List<string>()
        {
            ".xlsx",".docx",".pdf",".png",".jpg",".jpeg"
        };

        public string[] DocumentExpiration =
        {
          "One years","Two years","Three years","Varied","Never Expires"
        };

        public string[] ApplicationViews =
        {
          "Authorizations","Intakes","Employees","Clients","Assignments","Appointments","Supervisions","Insurance Checks","Quality Assurance","File Inbox","File Dropbox"
        };
    }
}
