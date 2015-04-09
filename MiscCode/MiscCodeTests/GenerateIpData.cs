using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Spatial;
using System.Linq;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class GenerateIpData
    {

        [Test]
        public void CanGenerateData()
        {
            var db = new GeoIpRepository();
            db.Database.ExecuteSqlCommand(@"
IF EXISTS(SELECT * FROM sys.indexes WHERE object_id = object_id('dbo.geoip_ranges') AND NAME ='IX_geoip_ranges_FromIp_ToIp')
    DROP INDEX [IX_geoip_ranges_FromIp_ToIp] ON geoip_ranges;
CREATE NONCLUSTERED INDEX [IX_geoip_ranges_FromIp_ToIp] 
    ON [dbo].[geoip_ranges]([FromIp], [ToIp]) INCLUDE ([GeoIpLocationId])
            ");
            var count = 0;
            string line;
            var numbers = new HashSet<char>("0123456789".ToCharArray());
            using (var stream = new StreamReader("../../Files/GeoLiteCity-Location.csv"))
            {
                while((line = stream.ReadLine()) != null)
                {
                    if (line.Length == 0 || !numbers.Contains(line[0])) continue;

                    ++count;
                    var parts = line.Split(',');
                    string name;
                    var i = 0;
                    var code = parts[1].Trim('"');
                    var loc = new GeoIpLocation
                    {
                        Id = Int32.Parse(parts[0]),
                        CountryCode = code,
                        Country = CountryNameByCode.TryGetValue(code, out name) ? name : code ,
                        Region = parts[2].Trim('"'),
                        City = parts[3].Trim('"'),
                        PostalCode = parts[4].Trim('"'),
                        Geo = DbGeography.PointFromText("POINT("+Single.Parse(parts[6])+" "+Single.Parse(parts[5])+")",
                                DbGeography.DefaultCoordinateSystemId),
                        MetroCode = Int32.TryParse(parts[7], out i) ? i : (int?)null,
                        AreaCode = Int32.TryParse(parts[8], out i) ? i : (int?)null,
                    };
                    db.GeoIpLocations.Add(loc);

                    if (count%1000 == 0)
                    {   
                        db.SaveChanges();
                        Console.WriteLine("loc"+count);
                        db = new GeoIpRepository();
                    }
                }
            }
            db.SaveChanges();

            using (var stream = new StreamReader("../../Files/GeoLiteCity-Blocks.csv"))
            {
                while((line = stream.ReadLine()) != null)
                {
                    if (line.Length == 0 || line[0] != '"') continue;

                    var parts = line.Split(',');
                    var range = new GeoIpRange
                    {
                        FromIp = Int64.Parse(parts[0].Trim('"')),
                        ToIp = Int64.Parse(parts[1].Trim('"')),
                        GeoIpLocationId = Int32.Parse(parts[2].Trim('"'))
                    };
                    db.GeoIpRanges.Add(range);

                    if (count%1000 == 0)
                    {
                        db.SaveChanges();
                        Console.WriteLine("block"+count);
                        db = new GeoIpRepository(); 
                    }
                }
            }
            db.SaveChanges();

            

    // line is not visible here.




            Console.WriteLine("DONE "+db.GeoIpLocations.Count());
        }

        #region Country Name By Code
        private readonly static IDictionary<string,string> CountryNameByCode = new Dictionary<string, string>
        {
            {"A1","Anonymous Proxy"},
            {"A2","Satellite Provider"},
            {"O1","Other Country"},
            {"AD","Andorra"},
            {"AE","United Arab Emirates"},
            {"AF","Afghanistan"},
            {"AG","Antigua and Barbuda"},
            {"AI","Anguilla"},
            {"AL","Albania"},
            {"AM","Armenia"},
            {"AO","Angola"},
            {"AP","Asia/Pacific Region"},
            {"AQ","Antarctica"},
            {"AR","Argentina"},
            {"AS","American Samoa"},
            {"AT","Austria"},
            {"AU","Australia"},
            {"AW","Aruba"},
            {"AX","Aland Islands"},
            {"AZ","Azerbaijan"},
            {"BA","Bosnia and Herzegovina"},
            {"BB","Barbados"},
            {"BD","Bangladesh"},
            {"BE","Belgium"},
            {"BF","Burkina Faso"},
            {"BG","Bulgaria"},
            {"BH","Bahrain"},
            {"BI","Burundi"},
            {"BJ","Benin"},
            {"BL","Saint Bartelemey"},
            {"BM","Bermuda"},
            {"BN","Brunei Darussalam"},
            {"BO","Bolivia"},
            {"BQ","Bonaire - Saint Eustatius and Saba"},
            {"BR","Brazil"},
            {"BS","Bahamas"},
            {"BT","Bhutan"},
            {"BV","Bouvet Island"},
            {"BW","Botswana"},
            {"BY","Belarus"},
            {"BZ","Belize"},
            {"CA","Canada"},
            {"CC","Cocos (Keeling) Islands"},
            {"CD","Congo - The Democratic Republic of the"},
            {"CF","Central African Republic"},
            {"CG","Congo"},
            {"CH","Switzerland"},
            {"CI","Cote d'Ivoire"},
            {"CK","Cook Islands"},
            {"CL","Chile"},
            {"CM","Cameroon"},
            {"CN","China"},
            {"CO","Colombia"},
            {"CR","Costa Rica"},
            {"CU","Cuba"},
            {"CV","Cape Verde"},
            {"CW","Curacao"},
            {"CX","Christmas Island"},
            {"CY","Cyprus"},
            {"CZ","Czech Republic"},
            {"DE","Germany"},
            {"DJ","Djibouti"},
            {"DK","Denmark"},
            {"DM","Dominica"},
            {"DO","Dominican Republic"},
            {"DZ","Algeria"},
            {"EC","Ecuador"},
            {"EE","Estonia"},
            {"EG","Egypt"},
            {"EH","Western Sahara"},
            {"ER","Eritrea"},
            {"ES","Spain"},
            {"ET","Ethiopia"},
            {"EU","Europe"},
            {"FI","Finland"},
            {"FJ","Fiji"},
            {"FK","Falkland Islands (Malvinas)"},
            {"FM","Micronesia - Federated States of"},
            {"FO","Faroe Islands"},
            {"FR","France"},
            {"GA","Gabon"},
            {"GB","United Kingdom"},
            {"GD","Grenada"},
            {"GE","Georgia"},
            {"GF","French Guiana"},
            {"GG","Guernsey"},
            {"GH","Ghana"},
            {"GI","Gibraltar"},
            {"GL","Greenland"},
            {"GM","Gambia"},
            {"GN","Guinea"},
            {"GP","Guadeloupe"},
            {"GQ","Equatorial Guinea"},
            {"GR","Greece"},
            {"GS","South Georgia and the South Sandwich Islands"},
            {"GT","Guatemala"},
            {"GU","Guam"},
            {"GW","Guinea-Bissau"},
            {"GY","Guyana"},
            {"HK","Hong Kong"},
            {"HM","Heard Island and McDonald Islands"},
            {"HN","Honduras"},
            {"HR","Croatia"},
            {"HT","Haiti"},
            {"HU","Hungary"},
            {"ID","Indonesia"},
            {"IE","Ireland"},
            {"IL","Israel"},
            {"IM","Isle of Man"},
            {"IN","India"},
            {"IO","British Indian Ocean Territory"},
            {"IQ","Iraq"},
            {"IR","Iran - Islamic Republic of"},
            {"IS","Iceland"},
            {"IT","Italy"},
            {"JE","Jersey"},
            {"JM","Jamaica"},
            {"JO","Jordan"},
            {"JP","Japan"},
            {"KE","Kenya"},
            {"KG","Kyrgyzstan"},
            {"KH","Cambodia"},
            {"KI","Kiribati"},
            {"KM","Comoros"},
            {"KN","Saint Kitts and Nevis"},
            {"KP","Korea - Democratic People's Republic of"},
            {"KR","Korea - Republic of"},
            {"KW","Kuwait"},
            {"KY","Cayman Islands"},
            {"KZ","Kazakhstan"},
            {"LA","Lao People's Democratic Republic"},
            {"LB","Lebanon"},
            {"LC","Saint Lucia"},
            {"LI","Liechtenstein"},
            {"LK","Sri Lanka"},
            {"LR","Liberia"},
            {"LS","Lesotho"},
            {"LT","Lithuania"},
            {"LU","Luxembourg"},
            {"LV","Latvia"},
            {"LY","Libyan Arab Jamahiriya"},
            {"MA","Morocco"},
            {"MC","Monaco"},
            {"MD","Moldova - Republic of"},
            {"ME","Montenegro"},
            {"MF","Saint Martin"},
            {"MG","Madagascar"},
            {"MH","Marshall Islands"},
            {"MK","Macedonia"},
            {"ML","Mali"},
            {"MM","Myanmar"},
            {"MN","Mongolia"},
            {"MO","Macao"},
            {"MP","Northern Mariana Islands"},
            {"MQ","Martinique"},
            {"MR","Mauritania"},
            {"MS","Montserrat"},
            {"MT","Malta"},
            {"MU","Mauritius"},
            {"MV","Maldives"},
            {"MW","Malawi"},
            {"MX","Mexico"},
            {"MY","Malaysia"},
            {"MZ","Mozambique"},
            {"NA","Namibia"},
            {"NC","New Caledonia"},
            {"NE","Niger"},
            {"NF","Norfolk Island"},
            {"NG","Nigeria"},
            {"NI","Nicaragua"},
            {"NL","Netherlands"},
            {"NO","Norway"},
            {"NP","Nepal"},
            {"NR","Nauru"},
            {"NU","Niue"},
            {"NZ","New Zealand"},
            {"OM","Oman"},
            {"PA","Panama"},
            {"PE","Peru"},
            {"PF","French Polynesia"},
            {"PG","Papua New Guinea"},
            {"PH","Philippines"},
            {"PK","Pakistan"},
            {"PL","Poland"},
            {"PM","Saint Pierre and Miquelon"},
            {"PN","Pitcairn"},
            {"PR","Puerto Rico"},
            {"PS","Palestinian Territory"},
            {"PT","Portugal"},
            {"PW","Palau"},
            {"PY","Paraguay"},
            {"QA","Qatar"},
            {"RE","Reunion"},
            {"RO","Romania"},
            {"RS","Serbia"},
            {"RU","Russian Federation"},
            {"RW","Rwanda"},
            {"SA","Saudi Arabia"},
            {"SB","Solomon Islands"},
            {"SC","Seychelles"},
            {"SD","Sudan"},
            {"SE","Sweden"},
            {"SG","Singapore"},
            {"SH","Saint Helena"},
            {"SI","Slovenia"},
            {"SJ","Svalbard and Jan Mayen"},
            {"SK","Slovakia"},
            {"SL","Sierra Leone"},
            {"SM","San Marino"},
            {"SN","Senegal"},
            {"SO","Somalia"},
            {"SR","Suriname"},
            {"SS","South Sudan"},
            {"ST","Sao Tome and Principe"},
            {"SV","El Salvador"},
            {"SX","Sint Maarten"},
            {"SY","Syrian Arab Republic"},
            {"SZ","Swaziland"},
            {"TC","Turks and Caicos Islands"},
            {"TD","Chad"},
            {"TF","French Southern Territories"},
            {"TG","Togo"},
            {"TH","Thailand"},
            {"TJ","Tajikistan"},
            {"TK","Tokelau"},
            {"TL","Timor-Leste"},
            {"TM","Turkmenistan"},
            {"TN","Tunisia"},
            {"TO","Tonga"},
            {"TR","Turkey"},
            {"TT","Trinidad and Tobago"},
            {"TV","Tuvalu"},
            {"TW","Taiwan"},
            {"TZ","Tanzania - United Republic of"},
            {"UA","Ukraine"},
            {"UG","Uganda"},
            {"UM","United States Minor Outlying Islands"},
            {"US","United States"},
            {"UY","Uruguay"},
            {"UZ","Uzbekistan"},
            {"VA","Holy See (Vatican City State)"},
            {"VC","Saint Vincent and the Grenadines"},
            {"VE","Venezuela"},
            {"VG","Virgin Islands - British"},
            {"VI","Virgin Islands - U.S."},
            {"VN","Vietnam"},
            {"VU","Vanuatu"},
            {"WF","Wallis and Futuna"},
            {"WS","Samoa"},
            {"YE","Yemen"},
            {"YT","Mayotte"},
            {"ZA","South Africa"},
            {"ZM","Zambia"},
            {"ZW","Zimbabwe"},
        };
        #endregion
    }
    public class GeoIpRange
    {
        public long Id { get; set; }
        public long FromIp { get; set; }
        public long ToIp { get; set; }
        public int GeoIpLocationId { get; set; }
        public GeoIpLocation GeoIpLocation { get; set; }
    }
    public class GeoIpLocation
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public DbGeography Geo { get; set; }
        public int? MetroCode { get; set; }
        public int? AreaCode { get; set; }
        public virtual ICollection<GeoIpRange> GeoIpRanges { get; set; }
    }
    public class GeoIpRepository : DbContext
    {
        private const string Varchar = "VARCHAR";

//        static GeoIpRepository()
//        {
//            Database.SetInitializer(new DropCreateDatabaseAlways<GeoIpRepository>());
//        }
        public GeoIpRepository() : base("name=Default")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public virtual IDbSet<GeoIpRange> GeoIpRanges { get; set; }
        public virtual IDbSet<GeoIpLocation> GeoIpLocations { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            mb.Configurations.Add(new GeoIpRangesMap());
            mb.Configurations.Add(new GeoIpLocationsMap());
        }
        public class GeoIpRangesMap : EntityTypeConfiguration<GeoIpRange>
        {
            public GeoIpRangesMap()
            {
                ToTable("geoip_ranges");
                HasKey(e => e.Id);
                Property(e => e.Id)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                
            }
        }
        public class GeoIpLocationsMap : EntityTypeConfiguration<GeoIpLocation>
        {
            public GeoIpLocationsMap()
            {
                ToTable("geoip_locations");
                HasKey(e => e.Id);
                Property(e => e.CountryCode)
                    .HasColumnType(Varchar)
                    .HasMaxLength(16)
                    .IsRequired();
                Property(e => e.Country)
                    .HasMaxLength(128)
                    .IsRequired();
                Property(e => e.Region)
                    .HasMaxLength(128);
                Property(e => e.City)
                    .HasMaxLength(128);
                Property(e => e.PostalCode)
                    .HasMaxLength(128);
                HasMany(e => e.GeoIpRanges)
                    .WithRequired(r => r.GeoIpLocation)
                    .HasForeignKey(r => r.GeoIpLocationId)
                    .WillCascadeOnDelete(false);
            }
        }
    }
}