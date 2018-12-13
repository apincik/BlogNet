using Core.Entities;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Web.ViewModels.PageForwards
{
    public class PageForwardUpdateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public string Mask { get; set; }
        public string DestinationUrl { get; set; }
        public string SourceId { get; set; }
        public PageForwardSourceType Type { get; set; }

    }
}
