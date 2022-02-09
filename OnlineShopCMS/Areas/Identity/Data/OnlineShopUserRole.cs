using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnlineShopCMS.Areas.Identity.Data
{
    public class OnlineShopUserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
    
    public class OnlineShopUserView
    {
        public OnlineShopCMSUser User { get; set; }
        public string RoleName { get; set; }
    }
}
