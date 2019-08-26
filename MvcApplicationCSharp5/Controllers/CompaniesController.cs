using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcApplicationCSharp5.Models;
using MvcApplicationCSharp5.Repositories;

namespace MvcApplicationCSharp5.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IActionResult Index()
        {
            var companies = _companyRepository.GetAll();
            return View(companies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            _companyRepository.Add(company);

            return RedirectToAction(nameof(Index));
        }
    }
}