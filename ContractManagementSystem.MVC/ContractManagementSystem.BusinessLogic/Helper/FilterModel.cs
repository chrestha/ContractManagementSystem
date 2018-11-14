using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementSystem.BusinessLogic.Helper
{
    /// <summary>
    /// Filter model that support multiple view model
    /// </summary>
    /// <typeparam name="ItemToShowModel">Entity view Model</typeparam>
    public class SimpleMoldelFilter<ItemToShowModel>
      where ItemToShowModel : class
    {
        public SimpleMoldelFilter()
        {
            ListOfItemsToShow = new List<ItemToShowModel>();
            Pager = new Pager();
        }
         

        public string field1 { get; set; }
        public string field2 { get; set; }
        public byte field3 { get; set; }
        public string field4 { get; set; }
        //[Display(ResourceType = typeof(Res.ModelsItemsNames), Name = "SearchOption")]

        public IEnumerable<ItemToShowModel> ListOfItemsToShow { get; set; }
        public Pager Pager { get; set; }
    }
}
