using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.BusinessLogic.ViewModel
{
    public class ContactVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "required")]
        public byte TitleId { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "required")]
        public string LastName { get; set; }
        [Display(Name = "Contract Type")]
        [Required(ErrorMessage = "required")]
        public byte ContractType { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Display(Name = "Phone No.")]
        [Required(ErrorMessage = "required")]
        public string PhoneNo { get; set; }
        public string Department { get; set; }
        public bool Status { get; set; }
        public int CompanyId { get; set; }
        public CompanyVM company { get; set; }
        [Display(Name="Full Name")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", Title, FirstName, LastName);
            }
        }
    }
    public class TitleVM
    {
        public byte Id { get; set; }
        public string Title { get; set; }
    }
}
