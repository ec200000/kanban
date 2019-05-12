using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kanban.BL;
namespace Kanban.InterfaceLayer
{
    public class InterfaceLayerBoard
    {
        //public Board board { get; private set; }
        public Dictionary<string,InterfaceLayerColumn> boardColumns { get; private set; }
        public InterfaceLayerBoard(Dictionary<string,InterfaceLayerColumn> boardColumns )
        {
            //this.board = board;
            this.boardColumns = boardColumns;
        }
    }
}
