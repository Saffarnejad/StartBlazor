using StartBlazor.Data;

namespace StartBlazor.Helpers.Meppers
{
    public static class ShoppingCartMapper
    {
        public static List<OrderItem> ToOrderItems(List<ShoppingCart> shoppingCarts)
        {
            return shoppingCarts
                .Select(shoppingCart => new OrderItem
                {
                    ProductId = shoppingCart.ProductId,
                    Count = shoppingCart.Count,
                    Price = shoppingCart.Product.Price,
                    ProductName = shoppingCart.Product.Name
                })
                .ToList();
        }
    }
}
