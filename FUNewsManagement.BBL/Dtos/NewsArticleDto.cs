using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagement.BLL.Dtos
{
    public class NewsArticleDto
    {
        public string NewsArticleId { get; set; } = string.Empty;
        public string NewsTitle { get; set; } = string.Empty;
        public string Headline { get; set; } = string.Empty;
        public string NewsContent { get; set; } = string.Empty;
        public string NewsSource { get; set; } = string.Empty;
        public short? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public bool NewsStatus { get; set; }
        public short? CreatedById { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public IReadOnlyList<int> TagIds { get; set; } = Array.Empty<int>();
        public IReadOnlyList<string> TagNames { get; set; } = Array.Empty<string>();
    }
}
