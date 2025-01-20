using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery_WPF_Exam.Model
{
    public class TreeViewItemModel
    {
        public string Name { get; set; }
        public List<TreeViewItemModel> Details { get; set; } = new List<TreeViewItemModel>();
    }
}
