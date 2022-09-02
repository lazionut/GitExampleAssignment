﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgame
{
    public class GetReviewsListPerBoardgameQuery : IRequest<List<Review>>
    {
        public int ReviewBoardgameId { get; set; }
        public int ReviewPageIndex { get; set; }
        public int ReviewPageSize { get; set; }
    }
}