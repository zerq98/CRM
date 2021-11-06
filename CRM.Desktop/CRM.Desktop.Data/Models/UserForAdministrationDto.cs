using static System.Net.Mime.MediaTypeNames;

namespace CRM.Desktop.Data.Models
{
    public class UserForAdministrationDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string StartDate { get; set; }
        public bool CanDelete { get; set; }
        public bool Gender { get; set; }
        public string FullDate
        {
            get
            {
                if (Name!="Dodaj użytkownika")
                {
                    return "Rozpoczęcie: " + StartDate;
                }
                else
                {
                    return "";
                }
            }
        }

        public string ImgSrc
        {
            get
            {
                if (Gender)
                {
                    return "female";
                }
                else
                {
                    if (Name != "Dodaj użytkownika")
                    {
                        return "male";
                    }
                    else
                    {
                        return "add";
                    }
                }
            }
        }
    }
}