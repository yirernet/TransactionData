using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionData.Core.Messeges
{
    public class ExcelMessages
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public bool IsErrored { get; set; }

        public List<ExcelMessages> Messages { get; set; }

        public ExcelMessages()
        {
            Messages = new List<ExcelMessages>();
        }
    }
}
