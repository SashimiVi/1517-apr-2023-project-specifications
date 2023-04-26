using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Clubs.BLL;
using Clubs.Entities;

namespace WebAppA02.Pages
{
    public class QueryModel : PageModel
    {
        #region Constructor Injection Depedency setup
        //create a variable to hang onto the injected service identifier
        private readonly EmployeesService _employeesService;
        private readonly ClubsService _clubsService;

        public QueryModel(EmployeesService employeesService, ClubsService clubsService)
        {
            _employeesService = employeesService;
            _clubsService= clubsService;
        }
        #endregion
        [BindProperty]
        public string clubStatus { get; set; }

        public List<Club> ClubList { get; set; }
        public List<Employee> EmployeeList { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }

   

        public void OnGet()
        {

        }
        public IActionResult OnPostSearch()
        {
            bool status = false;
            if (clubStatus == "Active")
            {
                status = true;
            }
            else if (clubStatus == "In-Active")
            {
                status = false;
            }
            ClubList = _clubsService.GetClubListByStatus(status);
            EmployeeList = _employeesService.GetEmployeeList();

            return Page();
        }
    }
}
