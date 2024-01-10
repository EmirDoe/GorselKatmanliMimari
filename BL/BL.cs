namespace BL
{
    public class BusinessLogic
    {

        public string[] cities =
        {
            "Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Aksaray", "Amasya", "Ankara", "Antalya", "Ardahan",
            "Artvin", "Aydın", "Balıkesir", "Bartın", "Batman", "Bayburt", "Bilecik", "Bingöl", "Bitlis", "Bolu",
            "Burdur", "Bursa", "Çanakkale", "Çankırı", "Çorum", "Denizli", "Diyarbakır", "Düzce", "Edirne", "Elazığ",
            "Erzincan", "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Iğdır",
            "Isparta", "İstanbul", "İzmir", "Kahramanmaraş", "Karabük", "Karaman", "Kars", "Kastamonu", "Kayseri",
            "Kırıkkale", "Kırklareli", "Kırşehir", "Kilis", "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa",
            "Mardin", "Mersin", "Muğla", "Muş", "Nevşehir", "Niğde", "Ordu", "Osmaniye", "Rize", "Sakarya",
            "Samsun", "Siirt", "Sinop", "Sivas", "Şanlıurfa", "Şırnak", "Tekirdağ", "Tokat", "Trabzon",
            "Tunceli", "Uşak", "Van", "Yalova", "Yozgat", "Zonguldak"
        };
        public string[] months =
        {
            "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
        };
        public static string[] years =
        {
            "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024"
        };

        public string[] bloodTypes =
            { "A Rh+", "A Rh-", "B Rh+", "B Rh-", "AB Rh+", "AB Rh-", "O Rh+", "O Rh-" };

        public string MembersTable()
        {
            string query = "CREATE TABLE Members(" +
                           "ID AUTOINCREMENT," +
                           "TC varchar(11) UNIQUE," +
                           "FullName varchar(50)," +
                           "BirthDate Date," +
                           "Gender varchar(10)," +
                           "BloodType varchar(10)," +
                           "PhoneNumber varchar(15)," +
                           "EmailAddress varchar(30)," +
                           "City varchar(20)," +
                           "Address varchar(100)," +
                           "MembershipDate DateTime," +
                           "IsActive YesNo," +
                           "CONSTRAINT PK_Members PRIMARY KEY (TC)" +
                           ");";
            return query;
        }

        public string DuesTable()
        {
            string query = "CREATE TABLE Dues (" +
                           "DueID AUTOINCREMENT," +
                           "TC varchar(11)," +
                           "PaymentAmount Double," +
                           "PaymentDate DateTime," +
                           "PaymentStatus YesNo," +
                           "FOREIGN KEY (TC) REFERENCES Members(TC)" +
                           " );";
            return query;
        }
        public string AddMember(string tcno, string fullname, DateTime birthDate, string gender, string bloodType, string phone, string email, string city, string address)
        {
            string query =
                "INSERT INTO Members " +
                "(TC, FullName, BirthDate, Gender, BloodType, PhoneNumber, EmailAddress, City, Address, MembershipDate, IsActive) " +
                $"VALUES('{tcno}', '{fullname}', #{birthDate:yyyy-MM-dd}#, '{gender}', '{bloodType}', '{phone}','{email}', '{city}', '{address}', '{DateTime.Now}', TRUE)";
            return query;
        }
        public string ListMembers(string tcno, string bloodType = null, string city = null, string isActive = null, string phone = null)
        {
            string query = "SELECT * FROM Members WHERE 1=1";

            if (!string.IsNullOrEmpty(tcno))
                query += $" AND TC = '{tcno}'";

            if (!string.IsNullOrEmpty(bloodType))
                query += $" AND BloodType = '{bloodType}'";

            if (!string.IsNullOrEmpty(city))
                query += $" AND City = '{city}'";

            if (!string.IsNullOrEmpty(phone))
                query += $" AND PhoneNumber = '{phone}'";

            if (!string.IsNullOrEmpty(isActive))
                query += $" AND IsActive = {isActive}";

            return query;
        }


        public string ShowMembers()
        {
            string query = "SELECT * FROM Members";
            return query;
        }

        public string DeleteMember(string tcno)
        {
            string query = $"DELETE FROM Members WHERE TC = '{tcno}';";
            return query;
        }

        public string DeactivateMember(string tcno)
        {
            string query = $"UPDATE Members SET IsActive = False WHERE TC = '{tcno}';";
            return query;
        }

        public string ActivateMember(string tcno)
        {
            string query = $"UPDATE Members SET IsActive = True WHERE TC = '{tcno}';";
            return query;
        }

        public string CreateDue(string tc, string amount, string date)
        {
            string query;

            if (!string.IsNullOrEmpty(date))
            {
                query = "INSERT INTO Dues (TC, PaymentAmount, PaymentDate, PaymentStatus) " +
                    $"VALUES ('{tc}', {amount}, '{date}', True);";
            }
            else
            {
                query = "INSERT INTO Dues (TC, PaymentAmount, PaymentDate, PaymentStatus) " +
                    $"VALUES ('{tc}', {amount}, NULL, False);";
            }
            return query;
        }
        public string UpdateDue(string dueID, string amount, string date, string status)
        {
            string query;

            if (!string.IsNullOrEmpty(date))
            {
                query = "UPDATE Dues " +
                    $"SET PaymentAmount = {amount}, " +
                    $"PaymentDate = '{date}', " +
                    $"PaymentStatus = {status} " +
                    $"WHERE DueID = {dueID};";
            }
            else
            {
                query = "UPDATE Dues " +
                    $"SET PaymentAmount = {amount}, " +
                    $"PaymentDate = NULL, " +
                    $"PaymentStatus = {status} " +
                    $"WHERE DueID = {dueID};";
            }

            return query;
        }

        public string ShowDues()
        {
            string query = "SELECT Dues.DueID, Members.FullName, Members.EmailAddress, Dues.TC, PaymentAmount, " +
                "PaymentDate, PaymentStatus FROM Dues INNER JOIN Members ON Dues.TC = Members.TC;";
            return query;
        }
        public string SearchDue()
        {
            string query = "SELECT Dues.DueID, Members.FullName, Members.EmailAddress, Dues.TC, " +
                "Dues.PaymentAmount, Dues.PaymentDate, Dues.PaymentStatus FROM Dues INNER JOIN Members ON Dues.TC = Members.TC";
            return query;
        }
        public string DeleteDue(string dueID)
        {
            string query = $"DELETE FROM Dues WHERE DueID = {dueID}";
            return query;
        }
        public string DeleteDueByMember(string tcno)
        {
            string query = $"DELETE FROM Dues WHERE TC = '{tcno}'";
            return query;

        }
    }
}

