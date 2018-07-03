using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCMLiteWebApp.Models.ViewModels
{
    public class UserViewModels
    {
        public class BasicUserViewModel
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public string Designation { get; set; }

            public bool MediaSpokesPerson { get; set; }

            public bool AuthorityToInvoke { get; set; }
        }
    }
}