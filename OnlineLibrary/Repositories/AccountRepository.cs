﻿using OnlineLibrary.Data;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(ApplicationUser user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeUserToAuthor(ApplicationUser user)
        {
            Author author = user as Author;
            _context.Remove(user);
            await _context.AddAsync(author);
            await _context.SaveChangesAsync();
        }
    }
}