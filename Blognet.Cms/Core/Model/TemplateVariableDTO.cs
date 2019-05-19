using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;

namespace Blognet.Cms.Core.Model
{
    public class TemplateVariableDTO
    {

        public int Id { get; set; }
        public string Label { get; set; }

        public string Content { get; set; }

        public ProjectVariableType Type { get; set; }

        public bool ShowRaw { get; set; }

        public Status Status { get; set; }

        public int? ParentTemplateVariableId { get; set; }

        public TemplateVariableDTO ParentTemplateVariable { get; set; }

        public List<TemplateVariableDTO> TemplateVariables { get; set; }

        public int ProjectId { get; set; }

        public string TemplateVariableTree
        {
            get
            {
                StringBuilder s = new StringBuilder();
                s.Insert(0, $" {Label}");
                TemplateVariableDTO parent = ParentTemplateVariable;

                while (parent != null)
                {
                    s.Insert(0, $" {parent.Label} - ");
                    parent = parent.ParentTemplateVariable;
                }

                return s.ToString().Trim();
            }
        }
    }
}
