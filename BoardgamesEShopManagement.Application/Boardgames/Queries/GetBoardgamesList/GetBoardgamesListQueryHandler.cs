﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList
{
    public class GetBoardgameQueryHandler : IRequestHandler<GetBoardgamesListQuery, IEnumerable<BoardgamesListVm>>
    {
        private readonly IBoardgameRepository _boardgameRepository;

        public GetBoardgameQueryHandler(IBoardgameRepository boardgameRepository)
        {
            _boardgameRepository = boardgameRepository;
        }

        public Task<IEnumerable<BoardgamesListVm>> Handle(GetBoardgamesListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<BoardgamesListVm> result = _boardgameRepository.GetAll().Select(boardgame => new BoardgamesListVm
            {
                Id = boardgame.Id,
                BoardgameImage = boardgame.Image,
                BoardgameName = boardgame.Name,
                BoardgameDescription = boardgame.Description,
                BoardgamePrice = boardgame.Price,
                BoardgameLink = boardgame.Link
            });

            return Task.FromResult(result);
        }
    }
}
