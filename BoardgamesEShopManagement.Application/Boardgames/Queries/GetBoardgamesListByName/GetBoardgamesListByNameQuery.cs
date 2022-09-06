﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByName
{
    public class GetBoardgamesListByNameQuery : IRequest<List<Boardgame>>
    {
        public string BoardgameNameCharacters { get; set; } = null!;
        public int BoardgamePageIndex { get; set; }
        public int BoardgamePageSize { get; set; }
        public BoardgamesSortOrdersEnum BoardgameSortOrder { get; set; }
    }
}