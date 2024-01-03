using System;
using System.Collections.Generic;
using OfficeOpenXml;

namespace FloorLocation.Models
{
	public class Context

	{
		public FileInfo file = new(@"/Users/carlolsen/projects/FloorLocation/FloorLocation/FLOOR_LOCATION.xlsx");
        public int GetRowCount()
        {
            int rowCount = 0;
            try
            {
                using ExcelPackage package = new(file);
                ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                rowCount = worksheet.Dimension.End.Row;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Context.cs, GetRowCount: " + ex.StackTrace);
            }
            return rowCount;
        }
        public int GetPageCount(int _pageSize = 5)
        {
            int pageCount = 0;
            try
            {
                int rowCount = GetRowCount();
                decimal rawPageCount = rowCount / _pageSize;
                pageCount = (int)Math.Ceiling(rawPageCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Context.cs, GetPageCount: " + ex.StackTrace);
            }
            return pageCount;
        }
        public List<Location> GetLocations() {
            List<Location> list = new();
            try
            {
                using ExcelPackage package = new(file);
                ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                int rowCount = GetRowCount();
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
                Console.WriteLine("Context.cs, GetLocations: " + ex.StackTrace);
            }
            return list;
        }
        public List<Location> GetPagedLocations(int _pageSize = 5, int _pageNumber = 1)
        {
            List<Location> list = new();
            try
            {
                using ExcelPackage package = new(file);
                ExcelWorksheet worksheet = package.Workbook.Worksheets["WMS Location Floor Location"];
                int rowCount = GetRowCount();
                int pageCount = GetPageCount(_pageSize);
                int pageRangeStart = 2; // because row 1 is a header row
                pageRangeStart += (_pageSize * (_pageNumber - 1));
                int pageRangeEnd = pageRangeStart + (_pageSize - 1);
                if (pageRangeEnd > rowCount)
                {
                    pageRangeEnd = rowCount;
                }
                for (int row = 2; row <= rowCount; row++)
                {
                    if (row >= pageRangeStart && row <= pageRangeEnd)
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Context.cs, GetLocations: " + ex.StackTrace);
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

