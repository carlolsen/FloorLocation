using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FloorLocation.Models;
using System;

namespace FloorLocation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(int PageSize = 5,int PageNumber = 1)
    {
        Context context = new();
        int recordCount = context.GetRecordCount();
        int pageCount = context.GetPageCount(PageSize);
        List<Location> list = context.GetPagedLocations(PageSize, PageNumber);
        ViewData["PageSize"] = PageSize;
        ViewData["PageNumber"] = PageNumber;
        ViewData["RecordCount"] = recordCount;
        ViewData["PageCount"] = pageCount;
        return View(list);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Location _objLocation, int PageSize = 5)
    {
        Context context = new();
        int pageCount = context.GetPageCount();
        context.AddLocation(_objLocation);
        return RedirectToAction("Index", new { PageSize, PageNumber = pageCount });
    }

    public IActionResult Update(string LocationName = "", int PageSize = 5, int PageNumber = 1)
    {
        Context context = new();
        Location _objLocation = context.GetLocation(LocationName);
        ViewData["PageSize"] = PageSize;
        ViewData["PageNumber"] = PageNumber;
        return View(_objLocation);
    }

    [HttpPost]
    public IActionResult Update(Location _objLocation, int PageSize = 5, int PageNumber = 1)
    {
        Context context = new();
        context.UpdateLocation(_objLocation);
        return RedirectToAction("Index", new { PageSize, PageNumber });
    }

    public IActionResult Delete(string LocationName = "", int PageSize = 5, int PageNumber = 1)
    {
        Context context = new();
        Location _objLocation = context.GetLocation(LocationName);
        ViewData["PageSize"] = PageSize;
        ViewData["PageNumber"] = PageNumber;
        return View(_objLocation);
    }

    [HttpPost]
    public IActionResult Delete(Location _objLocation, int PageSize = 5, int PageNumber = 1)
    {
        Context context = new();
        context.DeleteLocation(_objLocation);
        return RedirectToAction("Index", new { PageSize, PageNumber });
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

