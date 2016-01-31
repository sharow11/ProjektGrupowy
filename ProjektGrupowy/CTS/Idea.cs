using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    [Table("Ideas")]
    public class Idea : Entity
    {
        public bool Deleted { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [StringLength(100000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Description { get; set; }

        [ValidatePicture(ErrorMessage = "Picture's URL is invalid!")]
        public string Picture { get; set; }

        public AspNetUser AspNetUser { get; set; }

        public DateTime TimePosted { get; set; }

        public DateTime TimeValidated { get; set; }

        public DateTime TimeClosed { get; set; }

        public int Score { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ValidatePictureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            HttpWebRequest request;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(value.ToString());
                request.Method = "HEAD";
            }
            catch
            {
                return false;
            }
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK && response.ContentType.StartsWith("image"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}
