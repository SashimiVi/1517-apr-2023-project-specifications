using Clubs.DAL;
using Clubs.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Clubs.BLL
{
    public class ClubsService
    {
        #region set up context connection variable and class constructor 
        //limit use of the variable within this class
        private readonly StarTEDContext _context;

        //constructor to be used in the creation of the instance of this class
        internal ClubsService(StarTEDContext regcontext) { _context = regcontext; }
        #endregion

        #region Queries
        public List<Club> Club_List()
        {
            return _context.Clubs.OrderBy(x => x.Active).ToList();
        }
       public List<Club> GetClubListByStatus(bool active)
        {
            return _context.Clubs
                .Where(x => x.Active == active)
                .OrderBy(x => x.ClubName).ToList();
        }
        public Club Club_GetById(string clubid)
        {
            return _context.Clubs
                        .Where(x => x.ClubID.Equals(clubid))
                        .FirstOrDefault();
        }

        #endregion

        #region Add, Update and Delete(C U and D in CRUD)
        public string Club_AddClub(Club item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("club data is missing");
            }

            Club exists = _context.Clubs
                .Where(x => x.ClubName.ToUpper().Equals(item.ClubName.ToUpper()))
                .FirstOrDefault();
            if (exists != null)
            {
                throw new Exception($"{item.ClubName} already exist.");
            }


            //Club available_Staff = _context.Clubs
            //        .Where(x => x.EmployeeID == 3 || x.EmployeeID == 4 || x.EmployeeID == 5)
            //            .OrderBy(x => x.Employee.LastName).FirstOrDefault();

            //if (available_Staff != null)
            //{
            //    throw new Exception($"{available_Staff.Employee.FirstName} {available_Staff.Employee.LastName} is already on file");
            //}

            _context.Clubs.Add(item);
            _context.SaveChanges();
            return item.ClubID;
        }

        public int Club_UpdateClub(Club item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("club id is missing");
            }

            bool available_Staff = _context.Clubs.Any(x => x.ClubID == item.ClubID);


            if (!available_Staff)
            {
                throw new Exception($"{item.Employee.FirstName} {item.Employee.FirstName} is no longer on the system");
            }

            EntityEntry<Club> updating = _context.Entry(item);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _context.SaveChanges();
        }

        public int Club_DeactivateClub(Club item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("employee id is missing");
            }

            bool available_Staff = _context.Clubs.Any(x => x.ClubID == item.ClubID);


            if (!available_Staff)
            {
                throw new Exception($"{item.Employee.FirstName} {item.Employee.FirstName} is no longer on the system");
            }
            item.Active = false;
            EntityEntry<Club> updating = _context.Entry(item);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _context.SaveChanges();
        }
        #endregion
    }
}
