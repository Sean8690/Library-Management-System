using System.Linq;
using Library.ViewModels.Branch;
using LibraryData;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BranchController : Controller
    {
        private ILibraryBranch _branch;

        public BranchController(ILibraryBranch branch)
        {
            _branch = branch;
        }

        public IActionResult Index()
        {
            var ViewModel = _branch.GetAll()
                .Select(branch => new BranchViewModel
                {
                    Id = branch.Id, 
                    BranchName = branch.Name, 
                    IsOpen = _branch.IsBranchOpen(branch.Id), 
                    NumberOfAssets = _branch.GetAssets(branch.Id).Count(),
                    NumberOfPatrons = _branch.GetPatrons(branch.Id).Count()
                }).ToList();

            return View(ViewModel);
        }

        public IActionResult Detail(int id)
        {
            var branch = _branch.Get(id);

            if (branch == null)
            {
                return NotFound();
            }

            var model = new BranchViewModel
            {
                BranchName = branch.Name,
                Description = branch.Description,
                Address = branch.Address,
                Telephone = branch.Telephone,
                BranchOpenedDate = branch.OpenDate.ToString("yyyy-MM-dd"),
                NumberOfAssets = _branch.GetAssets(id).Count(),
                NumberOfPatrons = _branch.GetPatrons(id).Count(),
                TotalAssetValue = _branch.GetAssets(id).Sum(a => a.Cost),
                ImageUrl = branch.ImageUrl,
                HoursOpen = _branch.GetBranchHours(id)
            };

            return View(model);
        }
    }
}