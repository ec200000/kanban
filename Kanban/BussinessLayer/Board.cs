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
            columnsOrder.AddFirst(THIRD_COLUMN);
            columnsOrder.AddFirst(SECOND_COLUMN);
            columnsOrder.AddFirst(FIRST_COLUMN);
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

        public bool RemoveColumn(string colToRemove)
        {
            if (this.boardColumns.Count == 0)//there are no columns to erase
            {
                return false;
            }
            try
            {
                this.columnsOrder.Remove(colToRemove);
                this.boardColumns.Remove(colToRemove);
            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex + "in removeColumn function");
                return false;
            }
            return true;
        }

        public bool addNewColumnToBoard(string colBefore, string newCol)
        {
            try
            {
                if (this.boardColumns.ContainsKey(newCol))//can't add column with the same name
                {
                    FileLogger.WriteErrorToLog("can't create column because there is already one with the same name!");
                    return false;
                }
                if (colBefore.ToLower().Equals("empty"))//if the user wants to create a first new column
                {
                    this.columnsOrder.AddFirst(newCol);
                    this.boardColumns.Add(newCol, new Column());
                }
                else
                {
                    LinkedListNode<string> colBeforeNode = columnsOrder.Find(colBefore);
                    if (colBeforeNode == null)//couldn't find the column that the user entered
                    {
                        FileLogger.WriteNullObjectExceptionToLogger<string>("function addNewColumnToBoard");
                        return false;
                    }
                    this.columnsOrder.AddAfter(colBeforeNode, newCol);
                }

            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex + " in adaddNewColumnToBoard");
                return false;
            }
            return true;
        }

        public bool swapColumnsPosition(string colBefore, string colToMove)
        {
            try
            {
                if (colToMove == null | colBefore == null)
                {
                    FileLogger.WriteNullObjectExceptionToLogger<string>("swapColumnsPosition function");
                    return false;
                }
                RemoveColumn(colToMove);
                addNewColumnToBoard(colBefore, colToMove);
            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex + "in swapColumnsPosition function");
                return false;
            }

            return true;
        }
    }
}