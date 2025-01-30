﻿using System.Collections.ObjectModel;

// ReSharper disable InconsistentNaming
namespace Zek.ISO3166
{
    /// <summary>
    /// Country codes ISO 3166 <see href="https://en.wikipedia.org/wiki/ISO_3166-1" />
    /// </summary>
    public static class ISO3166
    {
        /// <summary>
        /// Obtain ISO3166-1 Country based on its alpha2 code.
        /// </summary>
        /// <param name="alpha2"></param>
        /// <returns></returns>
        public static ISO3166Country? FromAlpha2(string alpha2)
        {
            if (string.IsNullOrEmpty(alpha2))
                return null;
            alpha2 = alpha2.ToUpperInvariant();
            return Countries.FirstOrDefault(p => p.Alpha2 == alpha2);
        }

        /// <summary>
        /// Obtain ISO3166-1 Country based on its alpha3 code.
        /// </summary>
        /// <param name="alpha3"></param>
        /// <returns></returns>
        public static ISO3166Country? FromAlpha3(string alpha3)
        {
            if (string.IsNullOrEmpty(alpha3))
                return null;
            alpha3 = alpha3.ToUpperInvariant();
            return Countries.FirstOrDefault(p => p.Alpha3 == alpha3);
        }

        /// <summary>
        /// Obtain ISO3166-1 Country based on its alpha3 code.
        /// </summary>
        /// <param name="numericCode"></param>
        /// <returns></returns>
        public static ISO3166Country? FromNumericCode(int numericCode)
        {
            return Countries.FirstOrDefault(p => p.NumericCode == numericCode);
        }

        #region Country Collection

        /// <summary>
        /// This collection built from Wikipedia entry on ISO3166-1 on 9th Feb 2016
        /// </summary>
        public static readonly Collection<ISO3166Country> Countries =
        [
            new ISO3166Country("Afghanistan", "AF", "AFG", 4),
            new ISO3166Country("Åland Islands", "AX", "ALA", 248),
            new ISO3166Country("Albania", "AL", "ALB", 8),
            new ISO3166Country("Algeria", "DZ", "DZA", 12),
            new ISO3166Country("American Samoa", "AS", "ASM", 16),
            new ISO3166Country("Andorra", "AD", "AND", 20),
            new ISO3166Country("Angola", "AO", "AGO", 24),
            new ISO3166Country("Anguilla", "AI", "AIA", 660),
            new ISO3166Country("Antarctica", "AQ", "ATA", 10),
            new ISO3166Country("Antigua and Barbuda", "AG", "ATG", 28),
            new ISO3166Country("Argentina", "AR", "ARG", 32),
            new ISO3166Country("Armenia", "AM", "ARM", 51),
            new ISO3166Country("Aruba", "AW", "ABW", 533),
            new ISO3166Country("Australia", "AU", "AUS", 36),
            new ISO3166Country("Austria", "AT", "AUT", 40),
            new ISO3166Country("Azerbaijan", "AZ", "AZE", 31),
            new ISO3166Country("Bahamas", "BS", "BHS", 44),
            new ISO3166Country("Bahrain", "BH", "BHR", 48),
            new ISO3166Country("Bangladesh", "BD", "BGD", 50),
            new ISO3166Country("Barbados", "BB", "BRB", 52),
            new ISO3166Country("Belarus", "BY", "BLR", 112),
            new ISO3166Country("Belgium", "BE", "BEL", 56),
            new ISO3166Country("Belize", "BZ", "BLZ", 84),
            new ISO3166Country("Benin", "BJ", "BEN", 204),
            new ISO3166Country("Bermuda", "BM", "BMU", 60),
            new ISO3166Country("Bhutan", "BT", "BTN", 64),
            new ISO3166Country("Bolivia (Plurinational State of)", "BO", "BOL", 68),
            new ISO3166Country("Bonaire, Sint Eustatius and Saba", "BQ", "BES", 535),
            new ISO3166Country("Bosnia and Herzegovina", "BA", "BIH", 70),
            new ISO3166Country("Botswana", "BW", "BWA", 72),
            new ISO3166Country("Bouvet Island", "BV", "BVT", 74),
            new ISO3166Country("Brazil", "BR", "BRA", 76),
            new ISO3166Country("British Indian Ocean Territory", "IO", "IOT", 86),
            new ISO3166Country("Brunei Darussalam", "BN", "BRN", 96),
            new ISO3166Country("Bulgaria", "BG", "BGR", 100),
            new ISO3166Country("Burkina Faso", "BF", "BFA", 854),
            new ISO3166Country("Burundi", "BI", "BDI", 108),
            new ISO3166Country("Cabo Verde", "CV", "CPV", 132),
            new ISO3166Country("Cambodia", "KH", "KHM", 116),
            new ISO3166Country("Cameroon", "CM", "CMR", 120),
            new ISO3166Country("Canada", "CA", "CAN", 124),
            new ISO3166Country("Cayman Islands", "KY", "CYM", 136),
            new ISO3166Country("Central African Republic", "CF", "CAF", 140),
            new ISO3166Country("Chad", "TD", "TCD", 148),
            new ISO3166Country("Chile", "CL", "CHL", 152),
            new ISO3166Country("China", "CN", "CHN", 156),
            new ISO3166Country("Christmas Island", "CX", "CXR", 162),
            new ISO3166Country("Cocos (Keeling) Islands", "CC", "CCK", 166),
            new ISO3166Country("Colombia", "CO", "COL", 170),
            new ISO3166Country("Comoros", "KM", "COM", 174),
            new ISO3166Country("Congo", "CG", "COG", 178),
            new ISO3166Country("Congo (Democratic Republic of the)", "CD", "COD", 180),
            new ISO3166Country("Cook Islands", "CK", "COK", 184),
            new ISO3166Country("Costa Rica", "CR", "CRI", 188),
            new ISO3166Country("Côte d'Ivoire", "CI", "CIV", 384),
            new ISO3166Country("Croatia", "HR", "HRV", 191),
            new ISO3166Country("Cuba", "CU", "CUB", 192),
            new ISO3166Country("Curaçao", "CW", "CUW", 531),
            new ISO3166Country("Cyprus", "CY", "CYP", 196),
            new ISO3166Country("Czech Republic", "CZ", "CZE", 203),
            new ISO3166Country("Denmark", "DK", "DNK", 208),
            new ISO3166Country("Djibouti", "DJ", "DJI", 262),
            new ISO3166Country("Dominica", "DM", "DMA", 212),
            new ISO3166Country("Dominican Republic", "DO", "DOM", 214),
            new ISO3166Country("Ecuador", "EC", "ECU", 218),
            new ISO3166Country("Egypt", "EG", "EGY", 818),
            new ISO3166Country("El Salvador", "SV", "SLV", 222),
            new ISO3166Country("Equatorial Guinea", "GQ", "GNQ", 226),
            new ISO3166Country("Eritrea", "ER", "ERI", 232),
            new ISO3166Country("Estonia", "EE", "EST", 233),
            new ISO3166Country("Ethiopia", "ET", "ETH", 231),
            new ISO3166Country("Falkland Islands (Malvinas)", "FK", "FLK", 238),
            new ISO3166Country("Faroe Islands", "FO", "FRO", 234),
            new ISO3166Country("Fiji", "FJ", "FJI", 242),
            new ISO3166Country("Finland", "FI", "FIN", 246),
            new ISO3166Country("France", "FR", "FRA", 250),
            new ISO3166Country("French Guiana", "GF", "GUF", 254),
            new ISO3166Country("French Polynesia", "PF", "PYF", 258),
            new ISO3166Country("French Southern Territories", "TF", "ATF", 260),
            new ISO3166Country("Gabon", "GA", "GAB", 266),
            new ISO3166Country("Gambia", "GM", "GMB", 270),
            new ISO3166Country("Georgia", "GE", "GEO", 268),
            new ISO3166Country("Germany", "DE", "DEU", 276),
            new ISO3166Country("Ghana", "GH", "GHA", 288),
            new ISO3166Country("Gibraltar", "GI", "GIB", 292),
            new ISO3166Country("Greece", "GR", "GRC", 300),
            new ISO3166Country("Greenland", "GL", "GRL", 304),
            new ISO3166Country("Grenada", "GD", "GRD", 308),
            new ISO3166Country("Guadeloupe", "GP", "GLP", 312),
            new ISO3166Country("Guam", "GU", "GUM", 316),
            new ISO3166Country("Guatemala", "GT", "GTM", 320),
            new ISO3166Country("Guernsey", "GG", "GGY", 831),
            new ISO3166Country("Guinea", "GN", "GIN", 324),
            new ISO3166Country("Guinea-Bissau", "GW", "GNB", 624),
            new ISO3166Country("Guyana", "GY", "GUY", 328),
            new ISO3166Country("Haiti", "HT", "HTI", 332),
            new ISO3166Country("Heard Island and McDonald Islands", "HM", "HMD", 334),
            new ISO3166Country("Holy See", "VA", "VAT", 336),
            new ISO3166Country("Honduras", "HN", "HND", 340),
            new ISO3166Country("Hong Kong", "HK", "HKG", 344),
            new ISO3166Country("Hungary", "HU", "HUN", 348),
            new ISO3166Country("Iceland", "IS", "ISL", 352),
            new ISO3166Country("India", "IN", "IND", 356),
            new ISO3166Country("Indonesia", "ID", "IDN", 360),
            new ISO3166Country("Iran (Islamic Republic of)", "IR", "IRN", 364),
            new ISO3166Country("Iraq", "IQ", "IRQ", 368),
            new ISO3166Country("Ireland", "IE", "IRL", 372),
            new ISO3166Country("Isle of Man", "IM", "IMN", 833),
            new ISO3166Country("Israel", "IL", "ISR", 376),
            new ISO3166Country("Italy", "IT", "ITA", 380),
            new ISO3166Country("Jamaica", "JM", "JAM", 388),
            new ISO3166Country("Japan", "JP", "JPN", 392),
            new ISO3166Country("Jersey", "JE", "JEY", 832),
            new ISO3166Country("Jordan", "JO", "JOR", 400),
            new ISO3166Country("Kazakhstan", "KZ", "KAZ", 398),
            new ISO3166Country("Kenya", "KE", "KEN", 404),
            new ISO3166Country("Kiribati", "KI", "KIR", 296),
            new ISO3166Country("Korea (Democratic People's Republic of)", "KP", "PRK", 408),
            new ISO3166Country("Korea (Republic of)", "KR", "KOR", 410),
            new ISO3166Country("Kuwait", "KW", "KWT", 414),
            new ISO3166Country("Kyrgyzstan", "KG", "KGZ", 417),
            new ISO3166Country("Lao People's Democratic Republic", "LA", "LAO", 418),
            new ISO3166Country("Latvia", "LV", "LVA", 428),
            new ISO3166Country("Lebanon", "LB", "LBN", 422),
            new ISO3166Country("Lesotho", "LS", "LSO", 426),
            new ISO3166Country("Liberia", "LR", "LBR", 430),
            new ISO3166Country("Libya", "LY", "LBY", 434),
            new ISO3166Country("Liechtenstein", "LI", "LIE", 438),
            new ISO3166Country("Lithuania", "LT", "LTU", 440),
            new ISO3166Country("Luxembourg", "LU", "LUX", 442),
            new ISO3166Country("Macao", "MO", "MAC", 446),
            new ISO3166Country("Macedonia (the former Yugoslav Republic of)", "MK", "MKD", 807),
            new ISO3166Country("Madagascar", "MG", "MDG", 450),
            new ISO3166Country("Malawi", "MW", "MWI", 454),
            new ISO3166Country("Malaysia", "MY", "MYS", 458),
            new ISO3166Country("Maldives", "MV", "MDV", 462),
            new ISO3166Country("Mali", "ML", "MLI", 466),
            new ISO3166Country("Malta", "MT", "MLT", 470),
            new ISO3166Country("Marshall Islands", "MH", "MHL", 584),
            new ISO3166Country("Martinique", "MQ", "MTQ", 474),
            new ISO3166Country("Mauritania", "MR", "MRT", 478),
            new ISO3166Country("Mauritius", "MU", "MUS", 480),
            new ISO3166Country("Mayotte", "YT", "MYT", 175),
            new ISO3166Country("Mexico", "MX", "MEX", 484),
            new ISO3166Country("Micronesia (Federated States of)", "FM", "FSM", 583),
            new ISO3166Country("Moldova (Republic of)", "MD", "MDA", 498),
            new ISO3166Country("Monaco", "MC", "MCO", 492),
            new ISO3166Country("Mongolia", "MN", "MNG", 496),
            new ISO3166Country("Montenegro", "ME", "MNE", 499),
            new ISO3166Country("Montserrat", "MS", "MSR", 500),
            new ISO3166Country("Morocco", "MA", "MAR", 504),
            new ISO3166Country("Mozambique", "MZ", "MOZ", 508),
            new ISO3166Country("Myanmar", "MM", "MMR", 104),
            new ISO3166Country("Namibia", "NA", "NAM", 516),
            new ISO3166Country("Nauru", "NR", "NRU", 520),
            new ISO3166Country("Nepal", "NP", "NPL", 524),
            new ISO3166Country("Netherlands", "NL", "NLD", 528),
            new ISO3166Country("New Caledonia", "NC", "NCL", 540),
            new ISO3166Country("New Zealand", "NZ", "NZL", 554),
            new ISO3166Country("Nicaragua", "NI", "NIC", 558),
            new ISO3166Country("Niger", "NE", "NER", 562),
            new ISO3166Country("Nigeria", "NG", "NGA", 566),
            new ISO3166Country("Niue", "NU", "NIU", 570),
            new ISO3166Country("Norfolk Island", "NF", "NFK", 574),
            new ISO3166Country("Northern Mariana Islands", "MP", "MNP", 580),
            new ISO3166Country("Norway", "NO", "NOR", 578),
            new ISO3166Country("Oman", "OM", "OMN", 512),
            new ISO3166Country("Pakistan", "PK", "PAK", 586),
            new ISO3166Country("Palau", "PW", "PLW", 585),
            new ISO3166Country("Palestine, State of", "PS", "PSE", 275),
            new ISO3166Country("Panama", "PA", "PAN", 591),
            new ISO3166Country("Papua New Guinea", "PG", "PNG", 598),
            new ISO3166Country("Paraguay", "PY", "PRY", 600),
            new ISO3166Country("Peru", "PE", "PER", 604),
            new ISO3166Country("Philippines", "PH", "PHL", 608),
            new ISO3166Country("Pitcairn", "PN", "PCN", 612),
            new ISO3166Country("Poland", "PL", "POL", 616),
            new ISO3166Country("Portugal", "PT", "PRT", 620),
            new ISO3166Country("Puerto Rico", "PR", "PRI", 630),
            new ISO3166Country("Qatar", "QA", "QAT", 634),
            new ISO3166Country("Réunion", "RE", "REU", 638),
            new ISO3166Country("Romania", "RO", "ROU", 642),
            new ISO3166Country("Russian Federation", "RU", "RUS", 643),
            new ISO3166Country("Rwanda", "RW", "RWA", 646),
            new ISO3166Country("Saint Barthélemy", "BL", "BLM", 652),
            new ISO3166Country("Saint Helena, Ascension and Tristan da Cunha", "SH", "SHN", 654),
            new ISO3166Country("Saint Kitts and Nevis", "KN", "KNA", 659),
            new ISO3166Country("Saint Lucia", "LC", "LCA", 662),
            new ISO3166Country("Saint Martin (French part)", "MF", "MAF", 663),
            new ISO3166Country("Saint Pierre and Miquelon", "PM", "SPM", 666),
            new ISO3166Country("Saint Vincent and the Grenadines", "VC", "VCT", 670),
            new ISO3166Country("Samoa", "WS", "WSM", 882),
            new ISO3166Country("San Marino", "SM", "SMR", 674),
            new ISO3166Country("Sao Tome and Principe", "ST", "STP", 678),
            new ISO3166Country("Saudi Arabia", "SA", "SAU", 682),
            new ISO3166Country("Senegal", "SN", "SEN", 686),
            new ISO3166Country("Serbia", "RS", "SRB", 688),
            new ISO3166Country("Seychelles", "SC", "SYC", 690),
            new ISO3166Country("Sierra Leone", "SL", "SLE", 694),
            new ISO3166Country("Singapore", "SG", "SGP", 702),
            new ISO3166Country("Sint Maarten (Dutch part)", "SX", "SXM", 534),
            new ISO3166Country("Slovakia", "SK", "SVK", 703),
            new ISO3166Country("Slovenia", "SI", "SVN", 705),
            new ISO3166Country("Solomon Islands", "SB", "SLB", 90),
            new ISO3166Country("Somalia", "SO", "SOM", 706),
            new ISO3166Country("South Africa", "ZA", "ZAF", 710),
            new ISO3166Country("South Georgia and the South Sandwich Islands", "GS", "SGS", 239),
            new ISO3166Country("South Sudan", "SS", "SSD", 728),
            new ISO3166Country("Spain", "ES", "ESP", 724),
            new ISO3166Country("Sri Lanka", "LK", "LKA", 144),
            new ISO3166Country("Sudan", "SD", "SDN", 729),
            new ISO3166Country("Suriname", "SR", "SUR", 740),
            new ISO3166Country("Svalbard and Jan Mayen", "SJ", "SJM", 744),
            new ISO3166Country("Swaziland", "SZ", "SWZ", 748),
            new ISO3166Country("Sweden", "SE", "SWE", 752),
            new ISO3166Country("Switzerland", "CH", "CHE", 756),
            new ISO3166Country("Syrian Arab Republic", "SY", "SYR", 760),
            new ISO3166Country("Taiwan, Province of China[a]", "TW", "TWN", 158),
            new ISO3166Country("Tajikistan", "TJ", "TJK", 762),
            new ISO3166Country("Tanzania, United Republic of", "TZ", "TZA", 834),
            new ISO3166Country("Thailand", "TH", "THA", 764),
            new ISO3166Country("Timor-Leste", "TL", "TLS", 626),
            new ISO3166Country("Togo", "TG", "TGO", 768),
            new ISO3166Country("Tokelau", "TK", "TKL", 772),
            new ISO3166Country("Tonga", "TO", "TON", 776),
            new ISO3166Country("Trinidad and Tobago", "TT", "TTO", 780),
            new ISO3166Country("Tunisia", "TN", "TUN", 788),
            new ISO3166Country("Turkey", "TR", "TUR", 792),
            new ISO3166Country("Turkmenistan", "TM", "TKM", 795),
            new ISO3166Country("Turks and Caicos Islands", "TC", "TCA", 796),
            new ISO3166Country("Tuvalu", "TV", "TUV", 798),
            new ISO3166Country("Uganda", "UG", "UGA", 800),
            new ISO3166Country("Ukraine", "UA", "UKR", 804),
            new ISO3166Country("United Arab Emirates", "AE", "ARE", 784),
            new ISO3166Country("United Kingdom of Great Britain and Northern Ireland", "GB", "GBR", 826),
            new ISO3166Country("United States of America", "US", "USA", 840),
            new ISO3166Country("United States Minor Outlying Islands", "UM", "UMI", 581),
            new ISO3166Country("Uruguay", "UY", "URY", 858),
            new ISO3166Country("Uzbekistan", "UZ", "UZB", 860),
            new ISO3166Country("Vanuatu", "VU", "VUT", 548),
            new ISO3166Country("Venezuela (Bolivarian Republic of)", "VE", "VEN", 862),
            new ISO3166Country("Viet Nam", "VN", "VNM", 704),
            new ISO3166Country("Virgin Islands (British)", "VG", "VGB", 92),
            new ISO3166Country("Virgin Islands (U.S.)", "VI", "VIR", 850),
            new ISO3166Country("Wallis and Futuna", "WF", "WLF", 876),
            new ISO3166Country("Western Sahara", "EH", "ESH", 732),
            new ISO3166Country("Yemen", "YE", "YEM", 887),
            new ISO3166Country("Zambia", "ZM", "ZMB", 894),
            new ISO3166Country("Zimbabwe", "ZW", "ZWE", 716)
        ];

        #endregion
    }

    /// <summary>
    /// Representation of an ISO3166-1 Country
    /// </summary>
    public class ISO3166Country
    {
        public ISO3166Country(string name, string alpha2, string alpha3, int numericCode)
        {
            Name = name;
            Alpha2 = alpha2;
            Alpha3 = alpha3;
            NumericCode = numericCode;
        }

        public string Name { get; }

        public string Alpha2 { get; }

        public string Alpha3 { get; }

        public int NumericCode { get; }
    }
}