﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.BLL.DTO
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        //public IEnumerable<ClientDTO> Clients { get; set; }
        public string UserName { get; set; }
        //public List<ClientDTO> Clients { get; set; }
    }
}