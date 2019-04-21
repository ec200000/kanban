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
        private const string FIRST_COLUMN = "backlog";
        private const string SECOND_COLUMN = "inProgress";
        private const string THIRD_COLUMN = "Done";

        public Board()
        {
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

        public bool removeColumn(string colToRemove)
        {
            if(this.boardColumns.Count == 0)//there are no columns to erase
            {
                return false;
            }
            try
            {
                this.boardColumns.Remove(colToRemove);
            }catch(Exception ex)
            {
                FileLogger.WriteErrorToLog(ex + "in removeColumn function");
                return false;
            }
            return true;
        }

        public bool addNewColumn(string newColToAdd)
        {
            try
            {
                this.boardColumns.Add(newColToAdd, new Column());
            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex + "in addNewColumn function");
                return false;
            }
            return true;
        }

    }
}
