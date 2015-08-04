using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using RandomSchool.Models;

namespace RandomSchool.DAL
{
    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var department = new List<Department>
            {
                new Department{Name="Art", Budget=35000.00M, StartDate=DateTime.Parse("2014-09-01"), InstructorId=null},
                new Department{Name="English", Budget=12000.00M, StartDate=DateTime.Parse("2014-09-01"), InstructorId=null},
                new Department{Name="Foreign Language", Budget=32000.00M, StartDate=DateTime.Parse("2014-09-01"), InstructorId=null},
                new Department{Name="Mathematics", Budget=20000.00M, StartDate=DateTime.Parse("2014-09-01"), InstructorId=null},
                new Department{Name="Computing", Budget=20000.00M, StartDate=DateTime.Parse("2014-09-01"), InstructorId=null},
                new Department{Name="History", Budget=10000.00M, StartDate=DateTime.Parse("2014-09-01"), InstructorId=null}
            };

            department.ForEach(s => context.Departments.Add(s));

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorM = ex.EntityValidationErrors;
            }

            var schoolTypeCode = new List<SchoolTypeCode>
            {
            new SchoolTypeCode{Description="Faith School"},
            new SchoolTypeCode{Description="Free School"},
            new SchoolTypeCode{Description="Academies"},
            new SchoolTypeCode{Description="City technology"},
            new SchoolTypeCode{Description="State boarding school"},
            new SchoolTypeCode{Description="Private school"}
            };
            schoolTypeCode.ForEach(s => context.SchoolTypeCodes.Add(s));

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorM = ex.EntityValidationErrors;
            }

            var school = new List<School>
            {
            new School{ SchoolTypeId=1, SchoolName="Random High School"},
            new School{ SchoolTypeId=6, SchoolName="Specific High School"}
            };
            school.ForEach(s => context.Schools.Add(s));

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorM = ex.EntityValidationErrors;
            }


            var persons = new List<Person>
            {
            new Person{FirstMidName="Carson",LastName="Alexander", SchoolId = 1},
            new Person{FirstMidName="Meredith",LastName="Alonso", SchoolId = 1},
            new Person{FirstMidName="Arturo",LastName="Anand", SchoolId = 1},
            new Person{FirstMidName="Gytis",LastName="Barzdukas", SchoolId = 1},
            new Person{FirstMidName="Yan",LastName="Li", SchoolId = 1},
            new Person{FirstMidName="Peggy",LastName="Justice", SchoolId = 1},
            new Person{FirstMidName="Laura",LastName="Norman", SchoolId = 1},
            new Person{FirstMidName="Nino",LastName="Olivetto", SchoolId = 1}
            };

            persons.ForEach(s => context.People.Add(s));
            context.SaveChanges();

            var rooms = new List<Room>
            {
            new Room{ Description="Room 5A"},
            new Room{ Description="Room 2C"},
            new Room{ Description="Room 9Q"},
            new Room{ Description="Room 8A"},
            new Room{ Description="Room 3G"},
            new Room{ Description="Room 6Y"},
            new Room{ Description="Room 1S"},
            new Room{ Description="Room 8K"},
            };
            rooms.ForEach(s => context.Rooms.Add(s));

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorM = ex.EntityValidationErrors;
            }


            var courses = new List<Course>
            {
            new Course{QAN="100-1211-X",Title="Literature", DepartmentId=2, Description="English Literature", SubjectCode="C511", RoomId=1},
            new Course{QAN="100-1212-X",Title="Comprehension", DepartmentId=2, Description="English Comprehension", SubjectCode="A211", RoomId=2},
            new Course{QAN="100-1213-X",Title="Culinary Arts I", DepartmentId=1, Description="Culinary Arts I", SubjectCode="E543", RoomId=1},
            new Course{QAN="100-1214-X",Title="Algebra I", DepartmentId=4, Description="Mathematics - Algebra I", SubjectCode="C278", RoomId=3},
            new Course{QAN="100-1215-X",Title="Trigonometry", DepartmentId=4, Description="Mathematics - Trigonometry", SubjectCode="F121", RoomId=4},
            new Course{QAN="100-1216-X",Title="Composition", DepartmentId=2, Description="English Composition", SubjectCode="G332", RoomId=1},
            new Course{QAN="100-1217-X",Title="Latin III", DepartmentId=3, Description="Foreign Language - Latin III", SubjectCode="F112", RoomId=1},
            new Course{QAN="100-1218-X",Title="World History I", DepartmentId=6, Description="World History Section I", SubjectCode="S352", RoomId=2},
            new Course{QAN="100-1218-X",Title="Introduction to computing", DepartmentId=5, Description="Computing - Introduction to computing part I", SubjectCode="C218", RoomId=3}
            };
            courses.ForEach(s => context.Courses.Add(s));

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorM = ex.EntityValidationErrors;
            }



            var years = new List<Year>
            {
            new Year{SchoolYear="2012"},
            new Year{SchoolYear="2013"},
            new Year{SchoolYear="2014"},
            new Year{SchoolYear="2015"},
            new Year{SchoolYear="2016"},
            new Year{SchoolYear="2017"}
            };
            years.ForEach(s => context.Years.Add(s));

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorM = ex.EntityValidationErrors;
            }

            var grading = new List<Grading>
            {
            new Grading{GradeLetter="A"},
            new Grading{GradeLetter="B"},
            new Grading{GradeLetter="C"},
            new Grading{GradeLetter="D"},
            new Grading{GradeLetter="E"},
            new Grading{GradeLetter="F"}
            };
            grading.ForEach(s => context.Gradings.Add(s));

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorM = ex.EntityValidationErrors;
            }

            var nations = new List<Nation>
            {
                new Nation{Country="Andorra", Code="AD"},
                new Nation{Country="United Arab Emirates", Code="AE"},
                new Nation{Country="Afghanistan", Code="AF"},
                new Nation{Country="Antigua and Barbuda", Code="AG"},
                new Nation{Country="Anguilla", Code="AI"},
                new Nation{Country="Albania", Code="AL"},
                new Nation{Country="Armenia", Code="AM"},
                new Nation{Country="Angola", Code="AO"},
                new Nation{Country="Antarctica", Code="AQ"},
                new Nation{Country="Argentina", Code="AR"},
                new Nation{Country="American Samoa", Code="AS"},
                new Nation{Country="Austria", Code="AT"},
                new Nation{Country="Australia", Code="AU"},
                new Nation{Country="Aruba", Code="AW"},
                new Nation{Country="Åland Islands", Code="AX"},
                new Nation{Country="Azerbaijan", Code="AZ"},
                new Nation{Country="Bosnia and Herzegovina", Code="BA"},
                new Nation{Country="Barbados", Code="BB"},
                new Nation{Country="Bangladesh", Code="BD"},
                new Nation{Country="Belgium", Code="BE"},
                new Nation{Country="Burkina Faso", Code="BF"},
                new Nation{Country="Bulgaria", Code="BG"},
                new Nation{Country="Bahrain", Code="BH"},
                new Nation{Country="Burundi", Code="BI"},
                new Nation{Country="Benin", Code="BJ"},
                new Nation{Country="Saint Barthélemy", Code="BL"},
                new Nation{Country="Bermuda", Code="BM"},
                new Nation{Country="Brunei Darussalam", Code="BN"},
                new Nation{Country="Bolivia", Code="BO"},
                new Nation{Country="Caribbean Netherlands ", Code="BQ"},
                new Nation{Country="Brazil", Code="BR"},
                new Nation{Country="Bahamas", Code="BS"},
                new Nation{Country="Bhutan", Code="BT"},
                new Nation{Country="Bouvet Island", Code="BV"},
                new Nation{Country="Botswana", Code="BW"},
                new Nation{Country="Belarus", Code="BY"},
                new Nation{Country="Belize", Code="BZ"},
                new Nation{Country="Canada", Code="CA"},
                new Nation{Country="Cocos (Keeling) Islands", Code="CC"},
                new Nation{Country="Congo, Democratic Republic of", Code="CD"},
                new Nation{Country="Central African Republic", Code="CF"},
                new Nation{Country="Congo", Code="CG"},
                new Nation{Country="Switzerland", Code="CH"},
                new Nation{Country="Côte d'Ivoire", Code="CI"},
                new Nation{Country="Cook Islands", Code="CK"},
                new Nation{Country="Chile", Code="CL"},
                new Nation{Country="Cameroon", Code="CM"},
                new Nation{Country="China", Code="CN"},
                new Nation{Country="Colombia", Code="CO"},
                new Nation{Country="Costa Rica", Code="CR"},
                new Nation{Country="Cuba", Code="CU"},
                new Nation{Country="Cape Verde", Code="CV"},
                new Nation{Country="Curaçao", Code="CW"},
                new Nation{Country="Christmas Island", Code="CX"},
                new Nation{Country="Cyprus", Code="CY"},
                new Nation{Country="Czech Republic", Code="CZ"},
                new Nation{Country="Germany", Code="DE"},
                new Nation{Country="Djibouti", Code="DJ"},
                new Nation{Country="Denmark", Code="DK"},
                new Nation{Country="Dominica", Code="DM"},
                new Nation{Country="Dominican Republic", Code="DO"},
                new Nation{Country="Algeria", Code="DZ"},
                new Nation{Country="Ecuador", Code="EC"},
                new Nation{Country="Estonia", Code="EE"},
                new Nation{Country="Egypt", Code="EG"},
                new Nation{Country="Western Sahara", Code="EH"},
                new Nation{Country="Eritrea", Code="ER"},
                new Nation{Country="Spain", Code="ES"},
                new Nation{Country="Ethiopia", Code="ET"},
                new Nation{Country="Finland", Code="FI"},
                new Nation{Country="Fiji", Code="FJ"},
                new Nation{Country="Falkland Islands", Code="FK"},
                new Nation{Country="Micronesia, Federated States of", Code="FM"},
                new Nation{Country="Faroe Islands", Code="FO"},
                new Nation{Country="France", Code="FR"},
                new Nation{Country="Gabon", Code="GA"},
                new Nation{Country="United Kingdom", Code="GB"},
                new Nation{Country="Grenada", Code="GD"},
                new Nation{Country="Georgia", Code="GE"},
                new Nation{Country="French Guiana", Code="GF"},
                new Nation{Country="Guernsey", Code="GG"},
                new Nation{Country="Ghana", Code="GH"},
                new Nation{Country="Gibraltar", Code="GI"},
                new Nation{Country="Greenland", Code="GL"},
                new Nation{Country="Gambia", Code="GM"},
                new Nation{Country="Guinea", Code="GN"},
                new Nation{Country="Guadeloupe", Code="GP"},
                new Nation{Country="Equatorial Guinea", Code="GQ"},
                new Nation{Country="Greece", Code="GR"},
                new Nation{Country="South Georgia and the South Sandwich Islands", Code="GS"},
                new Nation{Country="Guatemala", Code="GT"},
                new Nation{Country="Guam", Code="GU"},
                new Nation{Country="Guinea-Bissau", Code="GW"},
                new Nation{Country="Guyana", Code="GY"},
                new Nation{Country="Hong Kong", Code="HK"},
                new Nation{Country="Heard and McDonald Islands", Code="HM"},
                new Nation{Country="Honduras", Code="HN"},
                new Nation{Country="Croatia", Code="HR"},
                new Nation{Country="Haiti", Code="HT"},
                new Nation{Country="Hungary", Code="HU"},
                new Nation{Country="Indonesia", Code="ID"},
                new Nation{Country="Ireland", Code="IE"},
                new Nation{Country="Israel", Code="IL"},
                new Nation{Country="Isle of Man", Code="IM"},
                new Nation{Country="India", Code="IN"},
                new Nation{Country="British Indian Ocean Territory", Code="IO"},
                new Nation{Country="Iraq", Code="IQ"},
                new Nation{Country="Iran", Code="IR"},
                new Nation{Country="Iceland", Code="IS"},
                new Nation{Country="Italy", Code="IT"},
                new Nation{Country="Jersey", Code="JE"},
                new Nation{Country="Jamaica", Code="JM"},
                new Nation{Country="Jordan", Code="JO"},
                new Nation{Country="Japan", Code="JP"},
                new Nation{Country="Kenya", Code="KE"},
                new Nation{Country="Kyrgyzstan", Code="KG"},
                new Nation{Country="Cambodia", Code="KH"},
                new Nation{Country="Kiribati", Code="KI"},
                new Nation{Country="Comoros", Code="KM"},
                new Nation{Country="Saint Kitts and Nevis", Code="KN"},
                new Nation{Country="North Korea", Code="KP"},
                new Nation{Country="South Korea", Code="KR"},
                new Nation{Country="Kuwait", Code="KW"},
                new Nation{Country="Cayman Islands", Code="KY"},
                new Nation{Country="Kazakhstan", Code="KZ"},
                new Nation{Country="Lao People's Democratic Republic", Code="LA"},
                new Nation{Country="Lebanon", Code="LB"},
                new Nation{Country="Saint Lucia", Code="LC"},
                new Nation{Country="Liechtenstein", Code="LI"},
                new Nation{Country="Sri Lanka", Code="LK"},
                new Nation{Country="Liberia", Code="LR"},
                new Nation{Country="Lesotho", Code="LS"},
                new Nation{Country="Lithuania", Code="LT"},
                new Nation{Country="Luxembourg", Code="LU"},
                new Nation{Country="Latvia", Code="LV"},
                new Nation{Country="Libya", Code="LY"},
                new Nation{Country="Morocco", Code="MA"},
                new Nation{Country="Monaco", Code="MC"},
                new Nation{Country="Moldova", Code="MD"},
                new Nation{Country="Montenegro", Code="ME"},
                new Nation{Country="Saint-Martin (France)", Code="MF"},
                new Nation{Country="Madagascar", Code="MG"},
                new Nation{Country="Marshall Islands", Code="MH"},
                new Nation{Country="Macedonia", Code="MK"},
                new Nation{Country="Mali", Code="ML"},
                new Nation{Country="Myanmar", Code="MM"},
                new Nation{Country="Mongolia", Code="MN"},
                new Nation{Country="Macau", Code="MO"},
                new Nation{Country="Northern Mariana Islands", Code="MP"},
                new Nation{Country="Martinique", Code="MQ"},
                new Nation{Country="Mauritania", Code="MR"},
                new Nation{Country="Montserrat", Code="MS"},
                new Nation{Country="Malta", Code="MT"},
                new Nation{Country="Mauritius", Code="MU"},
                new Nation{Country="Maldives", Code="MV"},
                new Nation{Country="Malawi", Code="MW"},
                new Nation{Country="Mexico", Code="MX"},
                new Nation{Country="Malaysia", Code="MY"},
                new Nation{Country="Mozambique", Code="MZ"},
                new Nation{Country="Namibia", Code="NA"},
                new Nation{Country="New Caledonia", Code="NC"},
                new Nation{Country="Niger", Code="NE"},
                new Nation{Country="Norfolk Island", Code="NF"},
                new Nation{Country="Nigeria", Code="NG"},
                new Nation{Country="Nicaragua", Code="NI"},
                new Nation{Country="The Netherlands", Code="NL"},
                new Nation{Country="Norway", Code="NO"},
                new Nation{Country="Nepal", Code="NP"},
                new Nation{Country="Nauru", Code="NR"},
                new Nation{Country="Niue", Code="NU"},
                new Nation{Country="New Zealand", Code="NZ"},
                new Nation{Country="Oman", Code="OM"},
                new Nation{Country="Panama", Code="PA"},
                new Nation{Country="Peru", Code="PE"},
                new Nation{Country="French Polynesia", Code="PF"},
                new Nation{Country="Papua New Guinea", Code="PG"},
                new Nation{Country="Philippines", Code="PH"},
                new Nation{Country="Pakistan", Code="PK"},
                new Nation{Country="Poland", Code="PL"},
                new Nation{Country="St. Pierre and Miquelon", Code="PM"},
                new Nation{Country="Pitcairn", Code="PN"},
                new Nation{Country="Puerto Rico", Code="PR"},
                new Nation{Country="Palestine, State of", Code="PS"},
                new Nation{Country="Portugal", Code="PT"},
                new Nation{Country="Palau", Code="PW"},
                new Nation{Country="Paraguay", Code="PY"},
                new Nation{Country="Qatar", Code="QA"},
                new Nation{Country="Réunion", Code="RE"},
                new Nation{Country="Romania", Code="RO"},
                new Nation{Country="Serbia", Code="RS"},
                new Nation{Country="Russian Federation", Code="RU"},
                new Nation{Country="Rwanda", Code="RW"},
                new Nation{Country="Saudi Arabia", Code="SA"},
                new Nation{Country="Solomon Islands", Code="SB"},
                new Nation{Country="Seychelles", Code="SC"},
                new Nation{Country="Sudan", Code="SD"},
                new Nation{Country="Sweden", Code="SE"},
                new Nation{Country="Singapore", Code="SG"},
                new Nation{Country="Saint Helena", Code="SH"},
                new Nation{Country="Slovenia", Code="SI"},
                new Nation{Country="Svalbard and Jan Mayen Islands", Code="SJ"},
                new Nation{Country="Slovakia", Code="SK"},
                new Nation{Country="Sierra Leone", Code="SL"},
                new Nation{Country="San Marino", Code="SM"},
                new Nation{Country="Senegal", Code="SN"},
                new Nation{Country="Somalia", Code="SO"},
                new Nation{Country="Suriname", Code="SR"},
                new Nation{Country="South Sudan", Code="SS"},
                new Nation{Country="Sao Tome and Principe", Code="ST"},
                new Nation{Country="El Salvador", Code="SV"},
                new Nation{Country="Sint Maarten (Dutch part)", Code="SX"},
                new Nation{Country="Syria", Code="SY"},
                new Nation{Country="Swaziland", Code="SZ"},
                new Nation{Country="Turks and Caicos Islands", Code="TC"},
                new Nation{Country="Chad", Code="TD"},
                new Nation{Country="French Southern Territories", Code="TF"},
                new Nation{Country="Togo", Code="TG"},
                new Nation{Country="Thailand", Code="TH"},
                new Nation{Country="Tajikistan", Code="TJ"},
                new Nation{Country="Tokelau", Code="TK"},
                new Nation{Country="Timor-Leste", Code="TL"},
                new Nation{Country="Turkmenistan", Code="TM"},
                new Nation{Country="Tunisia", Code="TN"},
                new Nation{Country="Tonga", Code="TO"},
                new Nation{Country="Turkey", Code="TR"},
                new Nation{Country="Trinidad and Tobago", Code="TT"},
                new Nation{Country="Tuvalu", Code="TV"},
                new Nation{Country="Taiwan", Code="TW"},
                new Nation{Country="Tanzania", Code="TZ"},
                new Nation{Country="Ukraine", Code="UA"},
                new Nation{Country="Uganda", Code="UG"},
                new Nation{Country="United States Minor Outlying Islands", Code="UM"},
                new Nation{Country="United States", Code="US"},
                new Nation{Country="Uruguay", Code="UY"},
                new Nation{Country="Uzbekistan", Code="UZ"},
                new Nation{Country="Vatican", Code="VA"},
                new Nation{Country="Saint Vincent and the Grenadines", Code="VC"},
                new Nation{Country="Venezuela", Code="VE"},
                new Nation{Country="Virgin Islands (British)", Code="VG"},
                new Nation{Country="Virgin Islands (U.S.)", Code="VI"},
                new Nation{Country="Vietnam", Code="VN"},
                new Nation{Country="Vanuatu", Code="VU"},
                new Nation{Country="Wallis and Futuna Islands", Code="WF"},
                new Nation{Country="Samoa", Code="WS"},
                new Nation{Country="Yemen", Code="YE"},
                new Nation{Country="Mayotte", Code="YT"},
                new Nation{Country="South Africa", Code="ZA"},
                new Nation{Country="Zambia", Code="ZM"},
                new Nation{Country="Zimbabwe", Code="ZW"},

            };

            nations.ForEach(s => context.Nations.Add(s));

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorM = ex.EntityValidationErrors;
            }
        }
    }
}