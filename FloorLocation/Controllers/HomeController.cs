using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FloorLocation.Models;

namespace FloorLocation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(int PageNumber = 1)
    {
        Context context = new();
        List<Location> list = context.GetPagedLocations(5, PageNumber);
        return View(list);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Location _objLocation)
    {
        Context context = new();
        context.AddLocation(_objLocation);
        return RedirectToAction("Index");
    }

    public IActionResult Update(string LocationName = "")
    {
        Context context = new();
        Location _objLocation = context.GetLocation(LocationName);
        return View(_objLocation);
    }

    [HttpPost]
    public IActionResult Update(Location _objLocation)
    {
        Context context = new();
        context.UpdateLocation(_objLocation);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(string LocationName = "")
    {
        Context context = new();
        Location _objLocation = context.GetLocation(LocationName);
        return View(_objLocation);
    }

    [HttpPost]
    public IActionResult Delete(Location _objLocation)
    {
        Context context = new();
        context.DeleteLocation(_objLocation);
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

