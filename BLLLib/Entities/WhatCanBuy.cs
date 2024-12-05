using DALLib.Entities;
using DALLib.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLLib.Entities
{
    public class WhatCanBuy
    {
        public Shop Shop { get; set; }
        private double _money;
        public double Money { get; set; }
    }
}
