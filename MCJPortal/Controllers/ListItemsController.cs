using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCJPortal.Application.Services.Interfaces;
using MCJPortal.Domain.Models.Enums;
using MCJPortal.ViewModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCJPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListItemsController : ControllerBase
    {
        private readonly IListItemsService _listItemsService;

        public ListItemsController(IListItemsService listItemsService)
        {
            this._listItemsService = listItemsService;
        }

        [HttpGet("{typeId}")]
        public async Task<List<ListItemViewModel>> GetListItemsAsync(ListTypeEnum typeId)
        {
            return await _listItemsService.GetItemsForTypeAsync(typeId);
        }
    }
}