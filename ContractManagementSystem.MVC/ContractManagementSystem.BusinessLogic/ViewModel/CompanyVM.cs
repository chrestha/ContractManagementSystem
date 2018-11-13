using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.BusinessLogic.ViewModel
{
    public class CompanyVM
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name= "Company ABN/CAN")]
        public string CompanyABN_CAN { get; set; }
        public string Description { get; set; }
        [Required]
        [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$", ErrorMessage = "Please Enter valid Url 'https://abc.com'")]
        public string URL { get; set; }
        public  List<ContactVM> Contacts { get; set; }
    }
}
