using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.InterfaceLayer
{
    class InterfaceLayerUser
    {
        public string Email { get; private set; }

        public InterfaceLayerUser(string email)
        {
            Email = email;
        }
    }
}
