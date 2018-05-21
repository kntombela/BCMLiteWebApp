using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCMLiteWebApp.Models.ViewModels
{

    public class PostResponseViewModel
    {
        public List<int> Ids { get; set; }

        public string Message { get; set; }
    }
}