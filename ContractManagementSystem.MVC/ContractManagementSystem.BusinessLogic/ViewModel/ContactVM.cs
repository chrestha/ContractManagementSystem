namespace ContractManagementSystem.BusinessLogic.ViewModel
{
    public class ContactVM
    {
        public int ID { get; set; }
        public byte TitleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ContractType { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Department { get; set; }
        public int CompanyId { get; set; }
        public CompanyVM company { get; set; }
    }
}
