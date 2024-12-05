using Shop.BLL.Entities;
using Shop.DAL.Entities;
using Shop.DAL.Implementation;
using System.Text;

namespace Shop.BLL
{
    public class ShopService
    {
        private IImplementation _implementation;
        public ShopService(IImplementation implementation)
        {
            _implementation = implementation;
        }

        public void AddPackProducts(PackProduct packProduct, out string message)
        {
            message = "";
            if (packProduct.Count <= 0)
            {
                message = "Нельзя добавить 0 штук товара";
                return;
            }

            if (packProduct.Product == null)
            {
                message = "Не выбран товар";
                return;
            }

            if (packProduct.Shop == null)
            {
                message = "Не выбран магазин для добавления";
                return;
            }

            if (packProduct.Product.Price <= 0)
            {
                message = "Некорректная цена";
                return;
            }

            if (packProduct.Product.Price != packProduct.Price)
            {
                Product product = new Product(packProduct.Product.TitleProduct, packProduct.Price);
                Task.Run(async () =>
                {
                    await _implementation.CreateNewProduct(product);
                });
                packProduct.Product = product;
            }

            Task.Run(async () =>
            {
                await _implementation.CreateNewAssortment(packProduct.Shop, packProduct.Product, packProduct.Count);
            });
        }
        public string GetWhatCanBui(WhatCanBuy whatCanBuy)
        {
            var _money = whatCanBuy.Money;
            var products = new List<Product>();
            var prod = _implementation.GetAssortments().Where(x => x.Shop.Id == whatCanBuy.Shop.Id).ToList();
            if (prod.Any())
            {
                while (prod.Any(x => x.Product.Price <= _money))
                {
                    foreach (var item in prod)
                    {
                        if (item.Product.Price <= _money)
                        {
                            products.Add(item.Product);
                            _money -= item.Product.Price;
                        }
                    }
                }

                var res = products.GroupBy(x => x.TitleProduct).ToList();
                if (res.Count > 0)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"В магазине '{whatCanBuy.Shop.TitleShop}' на {whatCanBuy.Money} рублей можно купить:");
                    foreach (var item in res)
                    {
                        sb.AppendLine($"{item.Key} - {item.Count()} шт.");
                    }
                    return sb.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public string GetMinPriveProduct(MinPriceProduct minPriceProduct)
        {
            var assortments = _implementation.GetAssortments();
            var shop = assortments.Where(x => x.Product.TitleProduct == minPriceProduct.Product.TitleProduct).OrderBy(x => x.Product.Price).ToList();
            return $"Самый дешевый продукт '{minPriceProduct.Product.TitleProduct}' в магазине '{shop.First().Shop.TitleShop}'";
        }
        public string GetBuyPackProductInShop(BuyPackProductInShop buyPackProductInShop)
        {
            var assortments = _implementation.GetAssortments();
            var assortmentsCurrentShop = assortments.Where(x => x.Shop.Id == buyPackProductInShop.Shop.Id).ToList();
            var currentProducts = assortmentsCurrentShop.Where(x => x.Product.TitleProduct == buyPackProductInShop.Product.TitleProduct).ToList();
            if (currentProducts.FirstOrDefault().Count >= buyPackProductInShop.Count)
            {
                return $"Для покупки {buyPackProductInShop.Count} штук '{buyPackProductInShop.Product.TitleProduct}' понадобится {(currentProducts.First().Product.Price * buyPackProductInShop.Count)} р.";
            }
            else
            {
                return "В данном магазине не хватает товара для покупки";
            }
        }
        public string GetBuyPackLowPrice(BuyPackLowPrice buyPackLowPrice)
        {
            var assortments = _implementation.GetAssortments();
            var shopWithProduct = assortments.Where(x => x.Product.TitleProduct == buyPackLowPrice.Product.TitleProduct).OrderBy(x => x.Product.Price);
            foreach (var shop in shopWithProduct)
            {
                if (shop.Count >= buyPackLowPrice.Count)
                {
                    return $"Дешевле {buyPackLowPrice.Count} штук '{buyPackLowPrice.Product.TitleProduct}' будет купить в магазине '{shop.Shop.TitleShop}'. Общая стоимость: {(shop.Product.Price * buyPackLowPrice.Count)}";
                }
            }
            return "В магазинах нет нужного количества товаров";
        }
    }
}
