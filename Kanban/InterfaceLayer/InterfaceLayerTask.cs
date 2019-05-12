using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.InterfaceLayer
{
    public class InterfaceLayerTask
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public string CreationTime { get; private set; }
        public string CurrCol { get; private set; }

        public InterfaceLayerTask(string title, string description, DateTime duedate, string creationTime, string currCol)
        {
            this.Title = title;
            this.Description = description;
            this.DueDate = duedate;
            this.CreationTime = creationTime;
            this.CurrCol = currCol;
        }
    }
}
