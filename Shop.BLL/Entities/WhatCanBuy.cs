namespace Shop.BLL.Entities
{
    public class WhatCanBuy
    {
        public Shop.DAL.Entities.Shop Shop { get; set; }
        private double _money;
        public double Money { get; set; }
    }
}
