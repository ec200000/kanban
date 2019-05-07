using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.BL
{
    public class Board
    {
        public Dictionary<string, Column> boardColumns;
        public LinkedList<String> columnsOrder;
        private const string FIRST_COLUMN = "backlog";
        private const string SECOND_COLUMN = "inProgress";
        private const string THIRD_COLUMN = "Done";

        public Board()
        {
            columnsOrder = new LinkedList<String>();
            if (!columnsOrder.Any())
            {
                columnsOrder.AddFirst(THIRD_COLUMN);
                columnsOrder.AddFirst(SECOND_COLUMN);
                columnsOrder.AddFirst(FIRST_COLUMN);
            }
            boardColumns = new Dictionary<string, Column>();
            boardColumns.Add(FIRST_COLUMN, new Column());
            boardColumns.Add(SECOND_COLUMN, new Column());
            boardColumns.Add(THIRD_COLUMN, new Column());
        }


        public void LoadBoard()
        {
            foreach (KeyValuePair<string, Column> kvp in boardColumns)
            {
                if (kvp.Key != null)
                {
                    Console.WriteLine(kvp.Key);
                    kvp.Value.Print();
                }
                else Console.WriteLine(" empty ");
            }

        }
    }
}