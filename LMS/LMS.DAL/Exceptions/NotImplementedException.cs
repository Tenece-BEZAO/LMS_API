﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Exceptions
{
    public class NotImplementedException : Exception
    {
        public NotImplementedException(string msg): base(msg)
        {

        }
    }
}
