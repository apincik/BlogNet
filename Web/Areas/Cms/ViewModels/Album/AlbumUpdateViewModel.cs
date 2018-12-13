using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Cms.ViewModels.Albums
{
    public class AlbumUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
