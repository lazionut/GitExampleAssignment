﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class BoardgameRepository : GenericRepository<Boardgame>, IBoardgameRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<Boardgame> _logger;

        public BoardgameRepository(ShopContext context, ILogger<Boardgame> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Boardgame>?> GetBoardgamesSorted(int pageIndex, int pageSize, BoardgamesSortOrdersEnum sortOrder)
        {
            _logger.LogInformation("Getting the list of boardgames...");
            IQueryable<Boardgame> boardgame = _context.Boardgames;

            if (pageIndex <= 0 || pageSize <= 0 || !Enum.IsDefined(typeof(BoardgamesSortOrdersEnum), sortOrder))
            {
                return null;
            }

            switch (sortOrder)
            {
                case BoardgamesSortOrdersEnum.ReleaseYearDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by date...");
                    boardgame = boardgame.OrderByDescending(b => b.ReleaseYear);
                    break;
                case BoardgamesSortOrdersEnum.PriceAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by price...");
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;
                case BoardgamesSortOrdersEnum.PriceDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by price...");
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;
                case BoardgamesSortOrdersEnum.NameAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by name...");
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;
                case BoardgamesSortOrdersEnum.NameDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by name...");
                    boardgame = boardgame.OrderByDescending(b => b.Name);
                    break;
            }

            return await boardgame
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Boardgame>?> GetBoardgamesPerCategory(int categoryId, int pageIndex, int pageSize, BoardgamesSortOrdersEnum sortOrder)
        {
            _logger.LogInformation("Getting the list of boardgames per category selected by it's identifier...");
            IQueryable<Boardgame> boardgame = _context.Boardgames
                .Where(boardgame => boardgame.CategoryId == categoryId);

            if (pageIndex <= 0 || pageSize <= 0 || !Enum.IsDefined(typeof(BoardgamesSortOrdersEnum), sortOrder))
            {
                return null;
            }

            switch (sortOrder)
            {
                case BoardgamesSortOrdersEnum.ReleaseYearDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by date...");
                    boardgame = boardgame.OrderByDescending(b => b.ReleaseYear);
                    break;
                case BoardgamesSortOrdersEnum.PriceAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by price...");
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;
                case BoardgamesSortOrdersEnum.PriceDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by price...");
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;
                case BoardgamesSortOrdersEnum.NameAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by name...");
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;
                case BoardgamesSortOrdersEnum.NameDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by name...");
                    boardgame = boardgame.OrderByDescending(b => b.Name);
                    break;
            }

            return await boardgame
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Boardgame>?> GetBoardgamesByName(string characters, int pageIndex, int pageSize, BoardgamesSortOrdersEnum sortOrder)
        {
            _logger.LogInformation("Getting the list of boardgames by searched keywords...");
            IQueryable<Boardgame> boardgame = _context.Boardgames
                .Where(boardgame => boardgame.Name.Contains(characters));

            if (pageIndex <= 0 || pageSize <= 0 || !Enum.IsDefined(typeof(BoardgamesSortOrdersEnum), sortOrder))
            {
                return null;
            }

            switch (sortOrder)
            {
                case BoardgamesSortOrdersEnum.ReleaseYearDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by date...");
                    boardgame = boardgame.OrderByDescending(b => b.ReleaseYear);
                    break;
                case BoardgamesSortOrdersEnum.PriceAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by price...");
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;
                case BoardgamesSortOrdersEnum.PriceDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by price...");
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;
                case BoardgamesSortOrdersEnum.NameAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by name...");
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;
                case BoardgamesSortOrdersEnum.NameDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by name...");
                    boardgame = boardgame.OrderByDescending(b => b.Name);
                    break;
            }

            return await boardgame
               .Skip((pageIndex - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }
    }
}
