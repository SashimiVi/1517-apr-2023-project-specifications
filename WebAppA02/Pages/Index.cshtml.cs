using Clubs.BLL;
using Clubs.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppA02.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        //create a variable to hang onto the injected service identifier
        private readonly ClubsService _clubsService;
        public IndexModel(ILogger<IndexModel> logger, ClubsService clubsService)
        {
            _logger = logger;
            _clubsService= clubsService;
        }

    }
}