using Blognet.Cms.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blognet.Cms.Domain.Entities
{
    [Table("web_template_variable")]
    public class TemplateVariable : BaseEntity
    {
        public string Label { get; set; }
        public string Content { get; set; }
        public ProjectVariableType Type { get; set; }
        public bool ShowRaw { get; set; }
        public Status Status { get; set; }

        public int? ParentTemplateVariableId { get; set; }
        public TemplateVariable ParentTemplateVariable { get; set; }
        public List<TemplateVariable> TemplateVariables { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public string TemplateVariableTree
        {
            get
            {
                return GetTemplateVariableTree();
            }
        }

        private string GetTemplateVariableTree()
        {
            StringBuilder s = new StringBuilder();
            s.Insert(0, $" {Label}");
            TemplateVariable parent = ParentTemplateVariable;

            while (parent != null)
            {
                s.Insert(0, $" {parent.Label} - ");
                parent = parent.ParentTemplateVariable;
            }

            return s.ToString().Trim();
        }

    }
}
