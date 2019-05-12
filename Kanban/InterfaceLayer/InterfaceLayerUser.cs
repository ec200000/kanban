using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kanban.BL;
namespace Kanban.InterfaceLayer
{
    public class InterfaceLayerUser
    {
        public string Email { get; private set; }
        public InterfaceLayerBoard Board { get; private set; }
     
        public InterfaceLayerUser(string email,InterfaceLayerBoard board)
        {
            Email = email;
            Board = board;
        }
    }
}
