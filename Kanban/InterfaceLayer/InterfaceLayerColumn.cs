using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.InterfaceLayer
{
    public class InterfaceLayerColumn
    {
        public string Name { get; private set; }
        public List<InterfaceLayerTask> tasks { get; private set; }
        public int maxNumOfTaskInColumn { get; private set; }
        public InterfaceLayerColumn(string name , List<InterfaceLayerTask> tasks,int maxNumOfTaskInColumn)
        {
            this.Name = name;
            this.tasks = tasks;
            this.maxNumOfTaskInColumn = maxNumOfTaskInColumn; 
        }
    }
}
