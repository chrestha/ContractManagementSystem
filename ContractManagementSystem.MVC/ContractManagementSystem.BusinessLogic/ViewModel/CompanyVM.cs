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
        public string Name { get; set; }
        [Required]
        public string CompanyABN_CAN { get; set; }
        public string Description { get; set; }
        [Required]
        public string URL { get; set; }
        public  List<ContactVM> Tbl_Contact { get; set; }
    }
}
