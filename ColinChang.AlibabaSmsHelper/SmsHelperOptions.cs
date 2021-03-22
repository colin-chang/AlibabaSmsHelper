using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ColinChang.AlibabaSmsHelper
{
    public class SmsHelperOptions
    {
        [Required] public string AccessKeyId { get; set; }
        [Required] public string AccessKeySecret { get; set; }
        [Required] public string Endpoint { get; set; }
        [Required] public string SignName { get; set; }
        [Required] public IDictionary<string, string> TemplateCodes { get; set; }
    }
}