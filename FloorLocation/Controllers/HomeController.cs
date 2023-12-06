using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FloorLocation.Models;
using OfficeOpenXml;
using System.Collections.Generic;

namespace FloorLocation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Location> list = new List<Location>();
        try
        {
            FileInfo file = new FileInfo(@"/Users/carlolsen/projects/FloorLocation/FloorLocation/FLOOR_LOCATION.xlsx");
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    string[] values = new string[3];
                    for (int col = 1; col <= 3; col++)
                    {
                        values[col - 1] = worksheet.Cells[row, col].Value.ToString()!;
                    }
                    Location item = new Location()
                    {
                        LocationName = values[0],
                        LocationId = values[1],
                        IsClearance = values[2]
                    };
                    list.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("LocationContext.cs, at line 45: " + ex.StackTrace);
        }
        return View(list);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Location objLocation)
    {
        // Check to make sure the LocationName does not already exist
        return RedirectToAction("Index");
    }

    public IActionResult Update(string LocationName = "")
    {
        Location item = new Location();
        try
        {
            FileInfo file = new FileInfo(@"/Users/carlolsen/projects/FloorLocation/FloorLocation/FLOOR_LOCATION.xlsx");
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    if (LocationName == worksheet.Cells[row, 1].Value.ToString()!)
                    {
                        string[] values = new string[3];
                        for (int col = 1; col <= 3; col++)
                        {
                            values[col - 1] = worksheet.Cells[row, col].Value.ToString()!;
                        }
                        item = new Location
                        {
                            LocationName = values[0],
                            LocationId = values[1],
                            IsClearance = values[2]
                        };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("LocationContext.cs, at line 45: " + ex.StackTrace);
        }
        return View(item);
    }

    [HttpPost]
    public IActionResult Update(Location objLocation)
    {
        return RedirectToAction("Index");
    }

    public IActionResult Delete(string LocationName = "")
    {
        Location item = new Location();
        try
        {
            FileInfo file = new FileInfo(@"/Users/carlolsen/projects/FloorLocation/FloorLocation/FLOOR_LOCATION.xlsx");
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    if (LocationName == worksheet.Cells[row, 1].Value.ToString()!)
                    {
                        string[] values = new string[3];
                        for (int col = 1; col <= 3; col++)
                        {
                            values[col - 1] = worksheet.Cells[row, col].Value.ToString()!;
                        }
                        item = new Location
                        {
                            LocationName = values[0],
                            LocationId = values[1],
                            IsClearance = values[2]
                        };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("LocationContext.cs, at line 45: " + ex.StackTrace);
        }
        return View(item);
    }

    [HttpPost]
    public IActionResult Delete(Location objLocation)
    {
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

