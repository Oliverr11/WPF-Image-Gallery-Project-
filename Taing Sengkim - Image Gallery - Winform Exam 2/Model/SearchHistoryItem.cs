using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery_WPF_Exam.Model
{
    public class SearchHistoryItem
    {
        public string SearchQuery { get; set; }
        public string SearchType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
