using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CTS;

namespace ProjektGrupowy.Models
{
    public class IdeaCreateViewModel
    {
        public Idea Idea { get; set; }

        public string Picture { get; set; }

        public string Tags { get; set; }
    }
}