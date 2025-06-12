using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

//[Authorize(Policy = "RequireElevatedPrivilleges")]
public class AuthorController : Controller
{
    private IAuthorService _authorService;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorService authorService, IMapper mapper)
    {
        _authorService = authorService;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var authors = _authorService.GetAllAuthors()
            .ToList();
        return View(authors);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AuthorViewModel model)
    {
        if (ModelState.IsValid)
        {
            _authorService.InsertAuthor(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    public IActionResult Edit(int id)
    {
        var author = _authorService.GetAuthorById(id);
        if (author == null)
        {
            return NotFound();
        }

        var model = _mapper.Map<AuthorViewModel>(author);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AuthorViewModel model)
    {
        if (ModelState.IsValid)
        {
            _authorService.UpdateAuthor(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _authorService.DeleteAuthor(id);
        return RedirectToAction("Index");
    }
}