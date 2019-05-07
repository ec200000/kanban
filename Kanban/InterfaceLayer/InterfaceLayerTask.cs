using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.InterfaceLayer
{
    class InterfaceLayerTask
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string DueDate { get; private set; }
        public string CreationTime { get; private set; }
        public string CurrCol { get; private set; }

        public InterfaceLayerTask(string title, string description, string duedate, string creationtime, string currcol)
        {
            this.Title = title;
            this.Description = description;
            this.DueDate = duedate;
            this.CreationTime = creationtime;
            this.CurrCol = currcol;
        }
    }
}
