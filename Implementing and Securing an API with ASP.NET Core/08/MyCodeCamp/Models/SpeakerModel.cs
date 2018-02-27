using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCodeCamp.Models
{
  public class SpeakerModel
  {
    public string Url { get; set; }
    [Required]
    [MinLength(5)]
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string PhoneNumber { get; set; }
    public string WebsiteUrl { get; set; }
    public string TwitterName { get; set; }
    public string GitHubName { get; set; }
    [Required]
    [MinLength(25)]
    [MaxLength(4000)]
    public string Bio { get; set; }
    public string HeadShotUrl { get; set; }

    public ICollection<TalkModel> Talks { get; set; }

  }
}
