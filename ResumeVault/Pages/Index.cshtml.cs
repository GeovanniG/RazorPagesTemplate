using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeVault.Infrastructure;

namespace ResumeVault.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public IndexModel(ILogger<IndexModel> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        return RedirectToPage("./Index");
    }
}
