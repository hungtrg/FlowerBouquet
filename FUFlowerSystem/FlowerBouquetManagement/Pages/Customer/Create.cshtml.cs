using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLayer.Models;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Customer
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _repo;

        public CreateModel(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DataLayer.Models.Customer Customer { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repo.AddCustomer(Customer);

            return RedirectToPage("./Index");
        }
    }
}
