using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMite.AdventureWorks.Api.DTO
{
    public class DTO_IndexerConfig
    {
        public string IndexName { get; set; }
        public string PrimaryKey { get; set; }
        public string DateController { get; set; }
        public List<string> MeasureFields { get; set; }
        public List<string> LongTextFields { get; set; }
        public List<string> CommaAnalyzerFields { get; set;}

        public DTO_IndexerConfig(string indexName, string primaryKey, string dateController, List<string> measureFields = null, List<string> longTextFields = null, List<string> commaAnalyzerFields = null)
        {
            IndexName = indexName;
            PrimaryKey = primaryKey;
            DateController = dateController;
            MeasureFields = measureFields;
            LongTextFields = longTextFields;
            CommaAnalyzerFields = commaAnalyzerFields;
        }
    }
}
