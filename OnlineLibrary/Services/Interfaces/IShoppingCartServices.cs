﻿using OnlineLibrary.Models;
using System.Threading.Tasks;

namespace OnlineLibrary.Services.Interfaces
{
    public interface IShoppingCartServices
    {
        Task CreateCartAsync(ApplicationUser buyer);
        Task AddItemToCartAsync(int bookId);
        Task IncreaseItemQuantityAsync(int itemId);
        Task RemoveItemFromCartAsync(int itemId);
        Task DecreaseItemQuantityAsync(int itemId);
        Task CancelCartAsync();
    }
}
