using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TransportManagment.Models;
using TransportManagment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using TransportManagment_DAL.Models;

namespace TransportManagment.Controllers
{
    [Authorize(Roles="admin")]
    public class UsersController : Controller
    {
        UserManager<Company> _userManager;

        public UsersController(UserManager<Company> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());  
         

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Company company = new Company { Email = model.Email, UserName = model.Name, PhoneNumber = model.Phone };
                var result = await _userManager.CreateAsync(company, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        #region Edit
        public async Task<IActionResult> Edit(string id)
        {
            Company company = await _userManager.FindByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = company.Id, Email = company.Email, Phone = company.PhoneNumber,Name=company.UserName };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Company Company = await _userManager.FindByIdAsync(model.Id);
                if (Company != null)
                {
                    Company.Email = model.Email;
                    Company.UserName = model.Name;
                    Company.PhoneNumber = model.Phone;

                    var result = await _userManager.UpdateAsync(Company);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
        #endregion

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            Company Company = await _userManager.FindByIdAsync(id);
            if (Company != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(Company);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> ChangePassword(string id)
        {
            Company user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, UserName = user.UserName };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                Company company = await _userManager.FindByIdAsync(model.Id);
                if (company != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(company, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User ");
                }
            }
            return View(model);
        }

    }
}
