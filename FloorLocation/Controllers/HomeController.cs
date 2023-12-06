using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using FloorLocation.Models;
using OfficeOpenXml;

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
            Console.WriteLine("HomeController.cs, at line 47: " + ex.StackTrace);
        }
        return View(list);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Location _objLocation)
    {
        bool found = false;
        int rowCount = 0;
        FileInfo file = new FileInfo(@"/Users/carlolsen/projects/FloorLocation/FloorLocation/FLOOR_LOCATION.xlsx");
        try
        {
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    if (_objLocation.LocationName == worksheet.Cells[row, 1].Value.ToString()!)
                    {
                        found = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("HomeController.cs, at line 80: " + ex.StackTrace);
        }
        if(found == true)
        {
            // Validation Failed: Duplicate Location Name
            return RedirectToAction("Index");
        }
        else
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                    int newRow = rowCount + 1;
                    worksheet.InsertRow(newRow, 1);
                    string A = @"A" + newRow.ToString();
                    string B = @"B" + newRow.ToString();
                    string C = @"C" + newRow.ToString();
                    worksheet.Cells[A].Value = _objLocation.LocationName;
                    worksheet.Cells[B].Value = _objLocation.LocationId;
                    worksheet.Cells[C].Value = _objLocation.IsClearance;
                    package.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("HomeController.cs, at line 105: " + ex.StackTrace);
            }
            // Validation Successful: Location Name New
            return RedirectToAction("Index");
        }
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
            Console.WriteLine("HomeController.cs, at line 125: " + ex.StackTrace);
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
            Console.WriteLine("HomeController.cs, at line 167: " + ex.StackTrace);
        }
        return View(item);
    }

    [HttpPost]
    public IActionResult Delete(Location _objLocation)
    {
        FileInfo file = new FileInfo(@"/Users/carlolsen/projects/FloorLocation/FloorLocation/FLOOR_LOCATION.xlsx");
        try
        {
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    if (_objLocation.LocationName == worksheet.Cells[row, 1].Value.ToString()!)
                    {
                        worksheet.DeleteRow(row, 1);
                        package.Save();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("HomeController.cs, at line 80: " + ex.StackTrace);
        }
        // Validation Successful: Location Name Found
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

