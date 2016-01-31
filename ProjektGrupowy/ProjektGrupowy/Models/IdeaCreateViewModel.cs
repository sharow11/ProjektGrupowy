using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CTS;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;

namespace ProjektGrupowy.Models
{
    public class IdeaCreateViewModel
    {
        public Idea Idea { get; set; }

        [ValidatePicture(ErrorMessage = "Picture's URL is invalid!")]
        public string Picture { get; set; }

        public string Tags { get; set; }
    }
}