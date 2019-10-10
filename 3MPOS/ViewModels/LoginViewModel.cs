﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace _3MPOS
{
    public class LoginViewModel: Screen
    {
        public void Login()
        {
            ((IConductor)Parent).ActivateItem(new BillUIViewModel());
        }
    }
}
