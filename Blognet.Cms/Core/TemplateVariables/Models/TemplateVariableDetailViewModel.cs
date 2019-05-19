using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Blognet.Cms.Core.Model;

namespace Blognet.Cms.Core.TemplateVariables.Models
{
    public class TemplateVariableDetailViewModel
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

        public List<TemplateVariableDTO> Variables { get; set; }
    }
}
