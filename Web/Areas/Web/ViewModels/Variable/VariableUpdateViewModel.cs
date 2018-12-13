using Core.Entities;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Web.ViewModels.Variables
{
    public class VariableUpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Label { get; set; }
        public string Content { get; set; }
        [Required]
        public ProjectVariableType Type { get; set; }
        public bool ShowRaw { get; set; }

        public int? ParentTemplateVariableId { get; set; }
        public List<TemplateVariable> Variables { get; set; }
    }
}
