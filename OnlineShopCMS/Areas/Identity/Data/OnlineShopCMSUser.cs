using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OnlineShopCMS.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the OnlineShopCMSUser class
    public class OnlineShopCMSUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
        public GenderType Gender { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    public enum GenderType
    {
        Male, Female, Unknown
    }
}
