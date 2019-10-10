using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace _3MPOS
{
    class ShellViewModel : Conductor<Screen>
    {
        public ShellViewModel()
        {
            ActivateItem(new LoginViewModel());
        }

    }
}
