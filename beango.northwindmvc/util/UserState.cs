﻿using System;

namespace beango.northwindmvc
{
    public abstract class IUserState
    {
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
    }
}