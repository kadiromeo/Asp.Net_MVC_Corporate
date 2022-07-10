using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dentexpert.Models
{
    public class sliderServices
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Services> Services { get; set; }
        public IEnumerable<About> Abouts { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}