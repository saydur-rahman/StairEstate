using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using STAIR.Model.Models;
using STAIR.Service;
using Helpers;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Office.Interop.Excel;
using System.Web.Mvc;
using System.Configuration;
using System.Net;
using STAIR.Web.Controllers;
using System.Collections;
using STAIR.Service.Enums;


namespace STAIR.Web.Helpers
{
    public class ReportService : Controller
    {
        public readonly IReportConfigurationService reportConfigurationService;
        public ReportService()
        {
        }

        public ReportService(IReportConfigurationService reportConfigurationService)
        {
            this.reportConfigurationService = reportConfigurationService;
        }

        public string GenerateReportInXLS(ItemIssue itemIssueObj, dynamic recordListToSave, string reportConfigurationName)
        {
            /*foreach (var ord in recordListToSave) test code for getting list
            {
                   var orderDetailsTemp = ord.GetType().GetProperty("OrderDetails").GetValue(ord, null);
            } */



            if (recordListToSave == null)
                return Resources.ResourceReportConfiguration.MgsListEmpty;

            var reportConfObj = this.reportConfigurationService.GetAllReportConfiguration().Where(rc => rc.Name == reportConfigurationName).FirstOrDefault();
            if (reportConfObj == null)
                return string.Format(Resources.ResourceReportConfiguration.MgsNoReportConfFound, reportConfigurationName);
            else
            {
                if (reportConfObj.ReportConfigurationDetails.Count() == 0)
                {
                    return string.Format(Resources.ResourceReportConfiguration.MgsReportConfDetailNotFoundInDb, reportConfigurationName);
                }
            }

            try
            {
                Type firstObject = null;
                foreach (var ord in recordListToSave)
                {
                    firstObject = ord.GetType();
                    break;
                }

                Type classType = firstObject.GetType();
                PropertyInfo[] fields = firstObject.GetProperties(BindingFlags.Public |
                                                                BindingFlags.Instance);

                var reportConfDetailsObj = reportConfObj.ReportConfigurationDetails;
                var cellCollumnNames = new List<string>();
                var listOfPro = new List<string>();

                foreach (var aField in fields.ToList())
                {
                    if (aField.GetAccessors()[0].IsVirtual == true)
                    {
                        fields = fields.Where(f => f.Name != aField.Name).ToArray();
                        continue;
                    }
                    var aReportConfDetObj = reportConfDetailsObj.Where(rcd => rcd.TableColumnName == aField.Name).FirstOrDefault();
                    if (aReportConfDetObj != null)
                    {
                        listOfPro.Add(aField.Name);
                        cellCollumnNames.Add(aReportConfDetObj.RoportColumnName);
                    }
                }




                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlApp.Visible = false;  //xls app window hiding
                xlApp.DisplayAlerts = false;  //no alert will be shown

                if (xlApp == null)
                {
                    xlApp.Quit();
                    return Resources.ResourceReportConfiguration.MgsExcelCouldNotStart;
                }

                Workbook workbook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
                worksheet.Name = "Report";

                if (worksheet == null)
                {
                    workbook.Close(true, Missing.Value, Missing.Value);
                    xlApp.Quit();
                    return Resources.ResourceReportConfiguration.MgsWorksheetCouldNotCreate;
                }

                //System.IO.File.Open(Server.MapPath("~/Demand Issue/demand-issue-voucher-templae.xlsx",System.IO.File.Open));

                var pathStr = Path.Combine(HttpRuntime.AppDomainAppPath + "\\Demand Issue\\demand-issue-voucher-templae.xlsx");
                if (System.IO.File.Exists(pathStr))
                {
                    workbook = xlApp.Workbooks.Open(pathStr, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);
                }





                int row = 9;
                string lastcolumn = GetExcelColumnName(cellCollumnNames.Count());
                //set title from conf.
                if (reportConfObj.HeadingTitle != null)
                {
                    Range rng2 = worksheet.get_Range("A" + row.ToString(), lastcolumn + row.ToString());
                    rng2.Font.Name = "Arial";
                    rng2.Font.Size = 18;
                    rng2.Font.Bold = true;
                    //rng2.Font.Underline = true;
                    //rng2.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rng2.Value = reportConfObj.HeadingTitle;
                    rng2.Merge();
                    row++;
                }

                //column setting start
                Range rngColumns = worksheet.get_Range("A" + row.ToString(), lastcolumn + row.ToString());
                rngColumns.Columns.AutoFit();
                rngColumns.Value = cellCollumnNames.ToArray();
                //rngColumns.Columns.AutoFit();
                //column setting end

                foreach (var ord in recordListToSave)
                {
                    row++;

                    Microsoft.Office.Interop.Excel.Borders border = rngColumns.Borders;
                    border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    border.Weight = 2d;

                    Range rng2 = worksheet.get_Range("A" + row.ToString(), lastcolumn + row.ToString());

                    //border cell strat

                    //border cell end
                    rng2.Insert();

                    //rngColumns.Value = cellCollumnNames.ToArray();

                    for (int i = 0; i < cellCollumnNames.Count(); i++)
                    {
                        worksheet.Cells[row, GetExcelColumnName(i + 1)] = ord.GetType().GetProperty(listOfPro[i]).GetValue(ord, null);
                    }
                }



                //demand info
                if (itemIssueObj.DemandNo != null)
                {
                    var demandObj = itemIssueObj.ItemDemand;
                    xlApp.Cells[7, 1] = demandObj.DemandNo;

                    if (demandObj.DepartmentId != null)
                        xlApp.Cells[7, 2] = demandObj.Department.Code;

                    if (demandObj.Priority != null)
                    {
                        try
                        {
                            PriorityEnum priority = (PriorityEnum)demandObj.Priority;
                            xlApp.Cells[7, 3] = priority.ToString();
                        }
                        catch (Exception e)
                        {
                            //
                        }
                    }
                    if (demandObj.LocationId != null)
                        xlApp.Cells[7, 4] = demandObj.Location.Code;

                    xlApp.Cells[7, 5] = demandObj.WorkOrderNo;
                    if (demandObj.AircraftOverviewId != null)
                        xlApp.Cells[7, 6] = demandObj.AircraftOverview.Registration;

                    //cell 7 have to set
                    //xlApp.Cells[7, 5] = demandObj.;

                    if (demandObj.DemandOn != null)
                        xlApp.Cells[7, 10] = demandObj.DemandOn;


                    //after dynamic grid 
                    //demand for 
                    xlApp.Cells[row + 3, 3] = demandObj.DemandFor;

                    //demand approve by
                    if (demandObj.ApprovedBy != null)
                    {
                        xlApp.Cells[row + 10, 5] = xlApp.Cells[row + 10, 5].Value.ToString() + demandObj.Employee.Code;
                    }
                    //demand approve on
                    if (demandObj.ApprovedOn != null)
                    {
                        xlApp.Cells[row + 11, 5] = xlApp.Cells[row + 11, 5].Value.ToString() + demandObj.ApprovedOn.Value;
                    }

                }

                if (itemIssueObj.RoomId != null)
                    xlApp.Cells[7, 7] = itemIssueObj.Room.Code; //stock room
                xlApp.Cells[7, 8] = itemIssueObj.IssueVoucherNo;  //vourcher no

                //issue approve by
                if (itemIssueObj.IssueApprovedBy != null)
                {
                    xlApp.Cells[row + 10, 6] = xlApp.Cells[row + 10, 6].Value.ToString() + itemIssueObj.Employee.Code;
                }
                //issue approve on
                if (itemIssueObj.IssueApprovedOn != null)
                {
                    xlApp.Cells[row + 11, 6] = xlApp.Cells[row + 11, 6].Value.ToString() + itemIssueObj.IssueApprovedOn;
                }


                //issue store keeper by
                if (itemIssueObj.IssuingStoreKeeper != null)
                {
                    xlApp.Cells[row + 7, 7] = xlApp.Cells[row + 7, 7].Value.ToString() + itemIssueObj.Employee.FullName;  //Name
                    xlApp.Cells[row + 10, 7] = xlApp.Cells[row + 10, 7].Value.ToString() + itemIssueObj.Employee1.Code;   //ID 
                }
                //issue store keeper on
                if (itemIssueObj.IssuingDate != null)
                {
                    xlApp.Cells[row + 11, 7] = xlApp.Cells[row + 11, 7].Value.ToString() + itemIssueObj.IssuingDate;
                }


                //issue received by
                if (itemIssueObj.MaterialReceivedBy != null)
                {
                    xlApp.Cells[row + 7, 8] = xlApp.Cells[row + 7, 8].Value.ToString() + itemIssueObj.Employee2.FullName;   //Name
                    xlApp.Cells[row + 10, 8] = xlApp.Cells[row + 10, 8].Value.ToString() + itemIssueObj.Employee2.Code;    //ID
                }
                //issue recceive on
                if (itemIssueObj.IssuingDate != null)
                {
                    xlApp.Cells[row + 11, 8] = xlApp.Cells[row + 11, 8].Value.ToString() + itemIssueObj.MaterialReceiveOn;
                }
                //bin card info have to set,,,, 


                string filePathAndName = null;
                if (reportConfObj.DestinationFolder != null)
                {
                    if (System.IO.Directory.Exists(@reportConfObj.DestinationFolder))
                        filePathAndName = @reportConfObj.DestinationFolder + "\\" + reportConfObj.Name;
                    else
                    {
                        workbook.Close(true, Missing.Value, Missing.Value);
                        xlApp.Quit();
                        return string.Format(Resources.ResourceReportConfiguration.MgsDircetoryNotFound, reportConfObj.DestinationFolder);
                    }
                }
                else
                {
                    filePathAndName = @"C:\Users\Administrator\Documents" + "\\" + reportConfObj.Name;
                }

                var fileExt = reportConfObj.OutputFileExtension;
                object outputFileType = null;

                if (fileExt == "csv" || fileExt == "CSV")
                {
                    outputFileType = Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV;
                }
                else if (fileExt == "xls" || fileExt == "xlsx" || fileExt == "XLS" || fileExt == "XLSX")
                {
                    outputFileType = Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook;
                }
                else
                {

                    workbook.Close(true, Missing.Value, Missing.Value);
                    xlApp.Quit();
                    return Resources.ResourceReportConfiguration.MgsCheckFileExt;
                }
                int? fileNo = 1;
                string appendFileNo = "";

                if (System.IO.File.Exists(filePathAndName + "." + fileExt))
                {
                    while (System.IO.File.Exists(filePathAndName + " (" + fileNo.ToString() + ")." + fileExt))
                    {
                        fileNo++;
                    }
                    workbook.SaveAs(filePathAndName + " (" + fileNo.ToString() + ")", outputFileType, Missing.Value, Missing.Value, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
                    appendFileNo = " (" + fileNo.ToString() + ")";
                }
                else
                {
                    workbook.SaveAs(filePathAndName, outputFileType, Missing.Value, Missing.Value, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
                }
                workbook.Close(true, Missing.Value, Missing.Value);
                xlApp.Quit();
                return string.Format(Resources.ResourceReportConfiguration.MgsReportGeneratedToThePath, (filePathAndName + appendFileNo), fileExt);
            }
            catch (Exception e)
            {
                return Resources.ResourceReportConfiguration.MgsCantGenerateReport + " " + e.Message;
            }
        }

        public string GenerateReport(dynamic recordListToSave, string reportConfigurationName)
        {
            /*foreach (var ord in recordListToSave) test code for getting list
            {
                   var orderDetailsTemp = ord.GetType().GetProperty("OrderDetails").GetValue(ord, null);
            } */

            if (recordListToSave == null)
                return Resources.ResourceReportConfiguration.MgsListEmpty;

            var reportConfObj = this.reportConfigurationService.GetAllReportConfiguration().Where(rc => rc.Name == reportConfigurationName).FirstOrDefault();
            if (reportConfObj == null)
                return string.Format(Resources.ResourceReportConfiguration.MgsNoReportConfFound, reportConfigurationName);
            else
            {
                if (reportConfObj.ReportConfigurationDetails.Count() == 0)
                {
                    return string.Format(Resources.ResourceReportConfiguration.MgsReportConfDetailNotFoundInDb, reportConfigurationName);
                }
            }

            try
            {
                Type firstObject = null;
                foreach (var ord in recordListToSave)
                {
                    firstObject = ord.GetType();
                    break;
                }

                Type classType = firstObject.GetType();
                PropertyInfo[] fields = firstObject.GetProperties(BindingFlags.Public |
                                                                BindingFlags.Instance);

                var reportConfDetailsObj = reportConfObj.ReportConfigurationDetails;
                var cellCollumnNames = new List<string>();
                var listOfPro = new List<string>();

                foreach (var aField in fields.ToList())
                {
                    if (aField.GetAccessors()[0].IsVirtual == true)
                    {
                        fields = fields.Where(f => f.Name != aField.Name).ToArray();
                        continue;
                    }
                    var aReportConfDetObj = reportConfDetailsObj.Where(rcd => rcd.TableColumnName == aField.Name).FirstOrDefault();
                    if (aReportConfDetObj != null)
                    {
                        listOfPro.Add(aField.Name);
                        cellCollumnNames.Add(aReportConfDetObj.RoportColumnName);
                    }
                }

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlApp.Visible = false;  //xls app window hiding
                xlApp.DisplayAlerts = false;  //no alert will be shown

                if (xlApp == null)
                {
                    xlApp.Quit();
                    return Resources.ResourceReportConfiguration.MgsExcelCouldNotStart;
                }

                Workbook workbook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
                worksheet.Name = "Report";

                if (worksheet == null)
                {
                    workbook.Close(true, Missing.Value, Missing.Value);
                    xlApp.Quit();
                    return Resources.ResourceReportConfiguration.MgsWorksheetCouldNotCreate;
                }

                int row = 1;
                string lastcolumn = GetExcelColumnName(cellCollumnNames.Count());
                //set title from conf.
                if (reportConfObj.HeadingTitle != null)
                {
                    Range rng2 = worksheet.get_Range("A" + row.ToString(), lastcolumn + row.ToString());
                    rng2.Font.Name = "Arial";
                    rng2.Font.Size = 18;
                    rng2.Font.Bold = true;
                    //rng2.Font.Underline = true;
                    rng2.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rng2.Value = reportConfObj.HeadingTitle;
                    rng2.Merge();
                    row++;
                }

                //column setting start
                Range rng = worksheet.get_Range("A" + row.ToString(), lastcolumn + row.ToString());
                rng.Value = cellCollumnNames.ToArray();
                //rng.Columns.AutoFit();
                //column setting end

                foreach (var ord in recordListToSave)
                {
                    row++;
                    for (int i = 0; i < cellCollumnNames.Count(); i++)
                    {
                        worksheet.Cells[row, GetExcelColumnName(i + 1)] = ord.GetType().GetProperty(listOfPro[i]).GetValue(ord, null);
                    }
                }

                string filePathAndName = null;
                if (reportConfObj.DestinationFolder != null)
                {
                    if (System.IO.Directory.Exists(@reportConfObj.DestinationFolder))
                        filePathAndName = @reportConfObj.DestinationFolder + "\\" + reportConfObj.Name;
                    else
                    {
                        workbook.Close(true, Missing.Value, Missing.Value);
                        xlApp.Quit();
                        return string.Format(Resources.ResourceReportConfiguration.MgsDircetoryNotFound, reportConfObj.DestinationFolder);
                    }
                }
                else
                {
                    filePathAndName = @"C:\Users\Administrator\Documents" + "\\" + reportConfObj.Name;
                }

                var fileExt = reportConfObj.OutputFileExtension;
                object outputFileType = null;

                if (fileExt == "csv" || fileExt == "CSV")
                {
                    outputFileType = Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV;
                }
                else if (fileExt == "xls" || fileExt == "xlsx" || fileExt == "XLS" || fileExt == "XLSX")
                {
                    outputFileType = Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook;
                }
                else
                {

                    workbook.Close(true, Missing.Value, Missing.Value);
                    xlApp.Quit();
                    return Resources.ResourceReportConfiguration.MgsCheckFileExt;
                }
                int? fileNo = 1;
                string appendFileNo = "";

                if (System.IO.File.Exists(filePathAndName + "." + fileExt))
                {
                    while (System.IO.File.Exists(filePathAndName + " (" + fileNo.ToString() + ")." + fileExt))
                    {
                        fileNo++;
                    }
                    workbook.SaveAs(filePathAndName + " (" + fileNo.ToString() + ")", outputFileType, Missing.Value, Missing.Value, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
                    appendFileNo = " (" + fileNo.ToString() + ")";
                }
                else
                {
                    workbook.SaveAs(filePathAndName, outputFileType, Missing.Value, Missing.Value, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
                }
                workbook.Close(true, Missing.Value, Missing.Value);
                xlApp.Quit();
                return string.Format(Resources.ResourceReportConfiguration.MgsReportGeneratedToThePath, (filePathAndName + appendFileNo), fileExt);
            }
            catch (Exception e)
            {
                return Resources.ResourceReportConfiguration.MgsCantGenerateReport + " " + e.Message;
            }
        }
        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }
            return columnName;
        }

    }
}
