using System;
using OfficeOpenXml;

namespace FloorLocation.Models
{
	public class Context

	{
		public FileInfo file = new(@"/Users/carlolsen/projects/FloorLocation/FloorLocation/FLOOR_LOCATION.xlsx");
        public List<Location> GetLocations() {
            List<Location> list = new();
            try
            {
                using ExcelPackage package = new(file);
                ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    string[] values = new string[3];
                    for (int col = 1; col <= 3; col++)
                    {
                        values[col - 1] = worksheet.Cells[row, col].Value.ToString()!;
                    }
                    Location item = new()
                    {
                        LocationName = values[0],
                        LocationId = values[1],
                        IsClearance = values[2]
                    };
                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Context.cs, at line 37: " + ex.StackTrace);
            }
            return list;
        }
        public void AddLocation(Location _objLocation)
        {
            bool found = false;
            int rowCount = 0;
            try
            {
                using ExcelPackage package = new(file);
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
            catch (Exception ex)
            {
                Console.WriteLine("Context.cs, at line 58: " + ex.StackTrace);
            }
            if (found != true)
            {
                try
                {
                    using ExcelPackage package = new(file);
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
                catch (Exception ex)
                {
                    Console.WriteLine("Context.cs, at line 78: " + ex.StackTrace);
                }
            }
        }
        public Location GetLocation(string _locationName = "")
        {
            Location _objLocation = new();
            try
            {
                using ExcelPackage package = new(file);
                ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    if (_locationName == worksheet.Cells[row, 1].Value.ToString()!)
                    {
                        string[] values = new string[3];
                        for (int col = 1; col <= 3; col++)
                        {
                            values[col - 1] = worksheet.Cells[row, col].Value.ToString()!;
                        }
                        _objLocation = new Location
                        {
                            LocationName = values[0],
                            LocationId = values[1],
                            IsClearance = values[2]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Context.cs, at line 111: " + ex.StackTrace);
            }
            return _objLocation;
        }
        public void UpdateLocation(Location _objLocation)
        {
            try
            {
                using ExcelPackage package = new(file);
                ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    if (_objLocation.LocationName == worksheet.Cells[row, 1].Value.ToString()!)
                    {
                        string A = @"A" + row.ToString();
                        string B = @"B" + row.ToString();
                        string C = @"C" + row.ToString();
                        worksheet.Cells[A].Value = _objLocation.LocationName;
                        worksheet.Cells[B].Value = _objLocation.LocationId;
                        worksheet.Cells[C].Value = _objLocation.IsClearance;
                        package.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Context.cs, at line 139: " + ex.StackTrace);
            }
        }
        public void DeleteLocation(Location _objLocation)
        {
            try
            {
                using ExcelPackage package = new(file);
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
            catch (Exception ex)
            {
                Console.WriteLine("Context.cs, at line 158: " + ex.StackTrace);
            }
        }
    }
}

