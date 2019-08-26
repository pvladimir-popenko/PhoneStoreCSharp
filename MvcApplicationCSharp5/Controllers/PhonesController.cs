using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using MvcApplicationCSharp5.Models;
using MvcApplicationCSharp5.Repositories;

namespace MvcApplicationCSharp5.Controllers
{
    public class PhonesController : Controller
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IHostingEnvironment _appEnvironment;

        public PhonesController(
            IPhoneRepository phoneRepository,
            ICompanyRepository companyRepository,
            IHostingEnvironment appEnvironment)
        {
            if(phoneRepository == null)
                throw new ArgumentNullException(nameof(phoneRepository));
            if(companyRepository == null)
                throw new ArgumentNullException(nameof(companyRepository));

            _phoneRepository = phoneRepository;
            _companyRepository = companyRepository;
            _appEnvironment = appEnvironment;
        }

        // GET: Phones
        public IActionResult Index()
        {
            var phones = _phoneRepository.GetAllIncludeCompanies();
            return View(phones);
        }

        // GET: Phones/Details/5
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var phone = _phoneRepository.GetById(id.Value);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // GET: Phones/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_companyRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                _phoneRepository.Add(phone);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_companyRepository.GetAll(), "Id", "Name");
            return View(phone);
        }

        // GET: Phones/Edit/5
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var phone = _phoneRepository.GetById(id.Value);
            if (phone == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_companyRepository.GetAll(), "Id", "Name", phone.CompanyId);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Phone phone)
        {
            if (id != phone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _phoneRepository.Update(phone);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneExists(phone.Id))
                    {
                        return NotFound();
                    }
                    return StatusCode(500);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_companyRepository.GetAll(), "Id", "Name", phone.CompanyId);
            return View(phone);
        }

        // GET: Phones/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = _phoneRepository.GetById(id.Value);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var phone = _phoneRepository.GetById(id);
            _phoneRepository.Delete(phone);
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneExists(int id)
        {
            return _phoneRepository.GetById(id) != null;
        }

        public IActionResult Download(string Name)
        {
            if (Name == null)
            {
                return StatusCode(404);
            }
            string path = Path.Combine(_appEnvironment.ContentRootPath, $"Files/{Name}.pdf");
            string fileType = "application/pdf";
            string fileName = $"{Name}.pdf";
            return PhysicalFile(path, fileType, fileName);
        }

        public IActionResult Forward(string Name)
        {
            if (Name == null)
            {
                return StatusCode(404);
            }

           return Redirect($"http://google.com/search?q={Name}");
        }
    }
}
