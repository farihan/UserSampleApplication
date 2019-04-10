using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hans.UserSample.Core.Entities;
using Hans.UserSample.Core.Interfaces;
using Hans.UserSample.Web.Helpers;
using Hans.UserSample.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hans.UserSample.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepository<User> repository;

        public UsersController(IRepository<User> repository)
        {
            this.repository = repository;
        }

        // GET: Users
        public async Task<IActionResult> Index(int? page, string sort = "username", bool asc = true, string query = "")
        {
            return RedirectToAction(nameof(Index));
        }

        // GET: Users
        public async Task<IActionResult> List(int? page, string sort = "username", bool asc = true, string query = "")
        {
            var count = await repository.CountAsync();

            var model = new UserListModel();
            model.PageSize = int.Parse("10");
            model.TotalPages = (int)Math.Ceiling((double)count / model.PageSize);
            model.CurrentPage = page ?? 1;
            model.PageIndex = model.PageSize * (model.CurrentPage - 1);
            model.Sort = sort;
            model.IsAsc = asc;
            model.Users = await repository.SkipAndTakeAsync((model.CurrentPage - 1) * model.PageSize, model.PageSize);

            return View(model);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (!id.HasValue)
                return NotFound();

            var user = await repository.FindOneByAsync(p => p.Id == id.Value);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (!id.HasValue)
                return NotFound();

            var user = await repository.FindOneByAsync(p => p.Id == id.Value);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (!id.HasValue)
                return NotFound();

            var user = await repository.FindOneByAsync(p => p.Id == id.Value);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}