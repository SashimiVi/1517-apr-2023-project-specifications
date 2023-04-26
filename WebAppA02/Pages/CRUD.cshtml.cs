using Clubs.BLL;
using Clubs.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebAppA02.Pages
{
    public class CRUDModel : PageModel
    {
        #region Constructor Injection Depedency setup
        //create a variable to hang onto the injected service identifier
        private readonly EmployeesService _employeesService;
        private readonly ClubsService _clubsService;

        public CRUDModel(EmployeesService employeesService, ClubsService clubsService)
        {
            _employeesService = employeesService;
            _clubsService = clubsService;
        }
        #endregion
        [BindProperty(SupportsGet = true)]
        public string? clubid { get; set; }
        public string Feedback { get; set; }
        public bool HasFeedback { get { return !string.IsNullOrWhiteSpace(Feedback); } }

        [BindProperty]
        public Club clubInfo { get; set; }
        public List<Club> clubList { get; set; }

        public List<Employee> employeeList { get; set; }
        public void OnGet()
        {
            if (clubid != null)
            {
                clubInfo = _clubsService.Club_GetById(clubid);
            }

            PopulateLists();
        }

        public void PopulateLists()
        {
            clubList = _clubsService.Club_List();
            employeeList = _employeesService.Employee_List();
        }
        public IActionResult OnPost()
        {
            PopulateLists();
            return Page();
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("./Query");
        }
        public IActionResult OnPostInsert()
        {
            if (clubInfo.EmployeeID == 0) // if no staff is selected
            {
                ModelState.AddModelError(nameof(clubInfo.EmployeeID), "Select a staff for the club");
            }
            if (String.IsNullOrEmpty(clubInfo.ClubID))
            {
                ModelState.AddModelError(nameof(clubInfo.ClubID), "Input a ClubID");

            }
            if (String.IsNullOrEmpty(clubInfo.ClubName))
            {
                ModelState.AddModelError(nameof(clubInfo.ClubName), "ClubName cannot be empty.");

            }
            if (ModelState.IsValid)
            {
                try
                {
                    string newclubid = _clubsService.Club_AddClub(clubInfo);
                    Feedback = $"Club (id:{clubInfo.ClubID}) has been added to the system";
                    PopulateLists();
                }
                catch (ArgumentNullException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                    PopulateLists();
                    return Page();
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                    PopulateLists();
                    return Page();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                    PopulateLists();
                    return Page();
                }
            }
            else
            {

                PopulateLists();
            }

            return Page();
        }

        public IActionResult OnPostUpdate()
        {
            if (clubInfo.EmployeeID == 0)
            {
                ModelState.AddModelError(nameof(clubInfo.EmployeeID), "Select a club for the employee");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int rowsaffected = _clubsService.Club_UpdateClub(clubInfo);
                    if (rowsaffected > 0)
                    {
                        Feedback = $"Club (id:{clubInfo.ClubID}) has been updated to the system";
                    }
                    else
                    {
                        Feedback = $"Club (id:{clubInfo.ClubID}) has been removed from the system. Return to the search page.";
                    }
                    PopulateLists();
                }
                catch (ArgumentNullException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                    PopulateLists();
                    return Page();
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                    PopulateLists();
                    return Page();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                    PopulateLists();
                    return Page();
                }
            }
            else
            {

                PopulateLists();
            }

            return Page();
        }
        public IActionResult OnPostDeactivate()
        {
            if (clubInfo.ClubID == null)
            {
                ModelState.AddModelError(nameof(clubInfo.ClubID), "Select a category for the product");
            }

            if (ModelState.IsValid)
            {
                try
                {

                    int rowsaffected = _clubsService.Club_DeactivateClub(clubInfo);
                    if (rowsaffected > 0)
                    {
                        Feedback = $"Club (id:{clubInfo.ClubID}) has been deactivate";
                    }


                    PopulateLists();

                }
                catch (ArgumentNullException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                    PopulateLists();
                    return Page();
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                    PopulateLists();
                    return Page();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(clubInfo.ClubName), GetInnerException(ex).Message);
                    PopulateLists();
                    return Page();
                }
            }
            else
            {
                PopulateLists();
            }

            return Page();
        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

    }
}
