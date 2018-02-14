using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DutchTreat.Data.Entities
{
  public class StoreUser : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

  }
}
