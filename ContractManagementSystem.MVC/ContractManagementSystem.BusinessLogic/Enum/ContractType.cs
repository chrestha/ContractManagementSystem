using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.BusinessLogic.Enum
{
    /// <summary>
    /// Contract Type Its value is fixed so no need to store it in database
    /// </summary>
    public enum ContractType
    {
        [Display(Name ="Master Contract")]
        MasterContact=1,
        [Display(Name = "Standard Contract")]
        StandardContract = 2
    }
}
