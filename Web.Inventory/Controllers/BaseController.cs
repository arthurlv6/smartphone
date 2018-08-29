using AutoMapper;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Infrastructure.Contracts;

namespace Web.API.Controllers
{
    public class BaseController<T> : Controller
    {
        internal readonly InventoryContext _context;
        internal readonly IMapper _mapper;
        internal readonly IDataShapeFactory _dataShapeFactory;
        internal readonly ILogger<T> _logger;
        internal const int maxPageSize = 10;

        public BaseController(
            InventoryContext context, 
            IMapper mapper, 
            IDataShapeFactory dataShapeFactory,
            ILogger<T> logger
            )
        {
            _context = context;
            _mapper = mapper;
            _dataShapeFactory = dataShapeFactory;
            _logger = logger;
        }
    }
}
