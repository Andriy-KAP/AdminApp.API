﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.API.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public OfficeModel Office { get; set; }
    }
}