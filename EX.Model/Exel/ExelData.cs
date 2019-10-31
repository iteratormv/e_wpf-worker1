using EX.Model.DbLayer;
using EX.Model.Infrastructure;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EX.Model.Exel
{
    public class ExelData
    {
        public int excelWorksheetRow { get; }
        public int excelWorksheetCol { get; }
        public string[,] data { get; }

        public ExelData()
        {
            //Microsoft.Office.Interop.Excel.Application excel_app =
            //    new Microsoft.Office.Interop.Excel.Application();

            //string file_name = "C:\\Exhibition\\ExhibitionData.xls";

            //Workbook work_book = excel_app.Workbooks.Open(file_name, Type.Missing);

            //Worksheet work_shet = (Worksheet)work_book.Worksheets[1];

            //Range excelRange = work_shet.UsedRange;

            //object[,] vallueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

            //excelWorksheetRow = work_shet.UsedRange.Rows.Count;
            //excelWorksheetCol = 15;
            ////      excelWorksheetCol = work_shet.UsedRange.Columns.Count;
            //data = new string[excelWorksheetRow, excelWorksheetCol];

            //for (int row = 1; row <= excelWorksheetRow; ++row)
            //{
            //    for (int col = 1; col <= excelWorksheetCol; ++col)
            //    {
            //        try
            //        {
            //            if (vallueArray[row, col] == null) data[row - 1, col - 1] = " ";
            //            else data[row - 1, col - 1] = vallueArray[row, col].ToString();
            //        }
            //        catch { data[row - 1, col - 1] = " "; }
            //    }
            //}
        }
        public ExelData(string fName)
        {
            Progress_Bar progress = new Progress_Bar { Visible = true, Progress = 0, Status = "Extracr data forom file" };


            Microsoft.Office.Interop.Excel.Application excel_app =
                new Microsoft.Office.Interop.Excel.Application();

            string[] partsPath = fName.Split('\\');
            foreach (string s in partsPath)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\n" + partsPath.Length.ToString());
            string res = "";
            for (int i = 0; i < partsPath.Length - 1; i++)
            {
                res += partsPath[i] + "\\";
            }
            res += partsPath[partsPath.Length - 1];

            string file_name = res;

            Workbook work_book = excel_app.Workbooks.Open(file_name, Type.Missing);

            Worksheet work_shet = (Worksheet)work_book.Worksheets[1];

            Range excelRange = work_shet.UsedRange;

            object[,] vallueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

            excelWorksheetRow = work_shet.UsedRange.Rows.Count;
            excelWorksheetCol = 15;
            //           excelWorksheetCol = work_shet.UsedRange.Columns.Count;

            data = new string[excelWorksheetRow, excelWorksheetCol];

            for (int row = 1; row <= excelWorksheetRow; ++row)
            {
                //               progress.Progress = (row * 100 / excelWorksheetRow);
                for (int col = 1; col <= excelWorksheetCol; ++col)
                {
                    try
                    {
                        if (vallueArray[row, col] == null) data[row - 1, col - 1] = " ";
                        else data[row - 1, col - 1] = vallueArray[row, col].ToString();
                    }
                    catch { data[row - 1, col - 1] = " "; }
                }
            }
        }
        public ExelData(string fName, int maxcol)
        {
            Progress_Bar progress = new Progress_Bar { Visible = true, Progress = 0, Status = "Extracr data forom file" };

            Microsoft.Office.Interop.Excel.Application excel_app =
                new Microsoft.Office.Interop.Excel.Application();

            string[] partsPath = fName.Split('\\');
            foreach (string s in partsPath)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\n" + partsPath.Length.ToString());
            string res = "";
            for (int i = 0; i < partsPath.Length - 1; i++)
            {
                res += partsPath[i] + "\\";
            }
            res += partsPath[partsPath.Length - 1];

            string file_name = res;

            Workbook work_book = excel_app.Workbooks.Open(file_name, Type.Missing);

            Worksheet work_shet = (Worksheet)work_book.Worksheets[1];

            Range excelRange = work_shet.UsedRange;

            object[,] vallueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

            excelWorksheetRow = work_shet.UsedRange.Rows.Count;
            excelWorksheetCol = maxcol;
            //           excelWorksheetCol = work_shet.UsedRange.Columns.Count;

            data = new string[excelWorksheetRow, excelWorksheetCol];

            for (int row = 1; row <= excelWorksheetRow; ++row)
            {
                //               progress.Progress = (row * 100 / excelWorksheetRow);
                for (int col = 1; col <= excelWorksheetCol; ++col)
                {
                    try
                    {
                        if (vallueArray[row, col] == null) data[row - 1, col - 1] = " ";
                        else data[row - 1, col - 1] = vallueArray[row, col].ToString();
                    }
                    catch { data[row - 1, col - 1] = " "; }
                }
            }
        }
        public ExelData(string fName, Action<Progress_Bar> progressChanged_)
        {
            Progress_Bar progress = new Progress_Bar { Visible = true, Progress = 0, Status = "Extract data from file" };


            Microsoft.Office.Interop.Excel.Application excel_app =
                new Microsoft.Office.Interop.Excel.Application();

            string[] partsPath = fName.Split('\\');
            foreach (string s in partsPath)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\n" + partsPath.Length.ToString());
            string res = "";
            for (int i = 0; i < partsPath.Length - 1; i++)
            {
                res += partsPath[i] + "\\";
            }
            res += partsPath[partsPath.Length - 1];

            string file_name = res;

            Workbook work_book = excel_app.Workbooks.Open(file_name, Type.Missing);

            Worksheet work_shet = (Worksheet)work_book.Worksheets[1];

            Range excelRange = work_shet.UsedRange;

            object[,] vallueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

            excelWorksheetRow = work_shet.UsedRange.Rows.Count;
            excelWorksheetCol = 17;
            //           excelWorksheetCol = work_shet.UsedRange.Columns.Count;

            data = new string[excelWorksheetRow, excelWorksheetCol];

            for (int row = 1; row <= excelWorksheetRow; ++row)
            {
                progress.Progress = (row * 100 / excelWorksheetRow);
                progressChanged_(progress);
                for (int col = 1; col <= excelWorksheetCol; ++col)
                {
                    try
                    {
                        if (vallueArray[row, col] == null) data[row - 1, col - 1] = " ";
                        else data[row - 1, col - 1] = vallueArray[row, col].ToString();
                    }
                    catch { data[row - 1, col - 1] = " "; }
                }
            }
        }
        public void setDataToCollection(DbSet<Visitor> visitorCollection)
        {
            for (int row = 0; row < excelWorksheetRow; row++)
            {
                Visitor visitor = new Visitor();

                if (data[row, 0] != null) visitor.Column1 = data[row, 0]; else visitor.Column1 = "none";
                if (data[row, 1] != null) visitor.Column2 = data[row, 1]; else visitor.Column2 = "none";
                if (data[row, 2] != null) visitor.Column3 = data[row, 2]; else visitor.Column3 = "none";
                if (data[row, 3] != null) visitor.Column4 = data[row, 3]; else visitor.Column4 = "none";
                if (data[row, 4] != null) visitor.Column5 = data[row, 4]; else visitor.Column5 = "none";
                if (data[row, 5] != null) visitor.Column6 = data[row, 5]; else visitor.Column6 = "none";
                if (data[row, 6] != null) visitor.Column7 = data[row, 6]; else visitor.Column7 = "none";
                if (data[row, 7] != null) visitor.Column8 = data[row, 7]; else visitor.Column8 = "none";
                if (data[row, 8] != null) visitor.Column9 = data[row, 8]; else visitor.Column9 = "none";
                if (data[row, 9] != null) visitor.Column10 = data[row, 9]; else visitor.Column10 = "none";
                if (data[row, 10] != null) visitor.Column11 = data[row, 10]; else visitor.Column11 = "none";
                if (data[row, 11] != null) visitor.Column12 = data[row, 11]; else visitor.Column12 = "none";
                if (data[row, 12] != null) visitor.Column13 = data[row, 12]; else visitor.Column13 = "none";
                if (data[row, 13] != null) visitor.Column14 = data[row, 13]; else visitor.Column14 = "none";
                if (data[row, 14] != null) visitor.Column15 = data[row, 14]; else visitor.Column15 = "none";
                visitorCollection.Add(visitor);
            }
        }
        public void setDataToCollection(DbSet<Visitor> visitorCollection, Action<Progress_Bar> progressChanged_)
        {
            Progress_Bar progress = new Progress_Bar { Visible = true, Progress = 0, Status = "Add Exel data to database" };

            for (int row = 0; row < excelWorksheetRow; row++)
            {
                Visitor visitor = new Visitor();
                progress.Progress = row * 100 / excelWorksheetRow;
                progressChanged_(progress);

                if (data[row, 0] != null) visitor.Column1 = data[row, 0]; else visitor.Column1 = "none";
                if (data[row, 1] != null) visitor.Column2 = data[row, 1]; else visitor.Column2 = "none";
                if (data[row, 2] != null) visitor.Column3 = data[row, 2]; else visitor.Column3 = "none";
                if (data[row, 3] != null) visitor.Column4 = data[row, 3]; else visitor.Column4 = "none";
                if (data[row, 4] != null) visitor.Column5 = data[row, 4]; else visitor.Column5 = "none";
                if (data[row, 5] != null) visitor.Column6 = data[row, 5]; else visitor.Column6 = "none";
                if (data[row, 6] != null) visitor.Column7 = data[row, 6]; else visitor.Column7 = "none";
                if (data[row, 7] != null) visitor.Column8 = data[row, 7]; else visitor.Column8 = "none";
                if (data[row, 8] != null) visitor.Column9 = data[row, 8]; else visitor.Column9 = "none";
                if (data[row, 9] != null) visitor.Column10 = data[row, 9]; else visitor.Column10 = "none";
                if (data[row, 10] != null) visitor.Column11 = data[row, 10]; else visitor.Column11 = "none";
                if (data[row, 11] != null) visitor.Column12 = data[row, 11]; else visitor.Column12 = "none";
                if (data[row, 12] != null) visitor.Column13 = data[row, 12]; else visitor.Column13 = "none";
                if (data[row, 13] != null) visitor.Column14 = data[row, 13]; else visitor.Column14 = "none";
                if (data[row, 14] != null) visitor.Column15 = data[row, 14]; else visitor.Column15 = "none";

                visitorCollection.Add(visitor);
            }
        }
        public void importDataToCollection(DbSet<Visitor> visitorCollection, Action<Progress_Bar> progressChanged_)
        {
            for (int row = 0; row < excelWorksheetRow; row++)
            {
                Progress_Bar progress = new Progress_Bar { Visible = true, Progress = 0, Status = "import Exel data to database" };

                Visitor visitor = new Visitor();
                progress.Progress = row * 100 / excelWorksheetRow;
                progressChanged_(progress);

                if (data[row, 1] != null) visitor.Column1 = data[row, 1]; else visitor.Column1 = "none";
                if (data[row, 2] != null) visitor.Column2 = data[row, 2]; else visitor.Column2 = "none";
                if (data[row, 3] != null) visitor.Column3 = data[row, 3]; else visitor.Column3 = "none";
                if (data[row, 4] != null) visitor.Column4 = data[row, 4]; else visitor.Column4 = "none";
                if (data[row, 5] != null) visitor.Column5 = data[row, 5]; else visitor.Column5 = "none";
                if (data[row, 6] != null) visitor.Column6 = data[row, 6]; else visitor.Column6 = "none";
                if (data[row, 7] != null) visitor.Column7 = data[row, 7]; else visitor.Column7 = "none";
                if (data[row, 8] != null) visitor.Column8 = data[row, 8]; else visitor.Column8 = "none";
                if (data[row, 9] != null) visitor.Column9 = data[row, 9]; else visitor.Column9 = "none";
                if (data[row,10] != null) visitor.Column10 = data[row, 10]; else visitor.Column10 = "none";
                if (data[row, 11] != null) visitor.Column11 = data[row, 11]; else visitor.Column11 = "none";
                if (data[row, 12] != null) visitor.Column12 = data[row, 12]; else visitor.Column12 = "none";
                if (data[row, 13] != null) visitor.Column13 = data[row, 13]; else visitor.Column13 = "none";
                if (data[row, 14] != null) visitor.Column14 = data[row, 14]; else visitor.Column14 = "none";
                if (data[row, 15] != null) visitor.Column15 = data[row, 15]; else visitor.Column15 = "none";
                if (data[row, 16] != null) visitor.CurrentStatus = data[row, 16]; else visitor.CurrentStatus = "none";

                visitorCollection.Add(visitor);
            }
        }
        public void importDataStatusToCollection(DbSet<Status> statusCollection, Action<Progress_Bar> progressChanged_)
        {
            for (int row = 0; row < excelWorksheetRow; row++)
            {
                Progress_Bar progress = new Progress_Bar { Visible = true, Progress = 0, Status = "import Exel data statuses to database" };

                Status status = new Status();
                progress.Progress = row * 100 / excelWorksheetRow;
                progressChanged_(progress);
                status.Name = data[row, 1];
                status.ActionTime = data[row, 2];
                status.UserId = int.Parse(data[row, 3]);
                status.VisitorId = int.Parse(data[row, 4]);
                statusCollection.Add(status);
            }
        }
        public void exportDataFromFile(DbSet<Visitor> visitorCollection)
        {

            for (int row = 0; row < excelWorksheetRow; row++)
            {
                Visitor visitor = new Visitor();
                if (data[row, 0] != null) visitor.Column1 = data[row, 0]; else visitor.Column1 = "none";
                if (data[row, 1] != null) visitor.Column2 = data[row, 1]; else visitor.Column2 = "none";
                if (data[row, 2] != null) visitor.Column3 = data[row, 2]; else visitor.Column3 = "none";
                if (data[row, 3] != null) visitor.Column4 = data[row, 3]; else visitor.Column4 = "none";
                if (data[row, 4] != null) visitor.Column5 = data[row, 4]; else visitor.Column5 = "none";
                if (data[row, 5] != null) visitor.Column6 = data[row, 5]; else visitor.Column6 = "none";
                if (data[row, 6] != null) visitor.Column7 = data[row, 6]; else visitor.Column7 = "none";
                if (data[row, 7] != null) visitor.Column8 = data[row, 7]; else visitor.Column8 = "none";
                if (data[row, 8] != null) visitor.Column9 = data[row, 8]; else visitor.Column9 = "none";
                if (data[row, 9] != null) visitor.Column10 = data[row, 9]; else visitor.Column10 = "none";
                if (data[row, 10] != null) visitor.Column11 = data[row, 10]; else visitor.Column11 = "none";
                if (data[row, 11] != null) visitor.Column12 = data[row, 11]; else visitor.Column12 = "none";
                if (data[row, 12] != null) visitor.Column13 = data[row, 12]; else visitor.Column13 = "none";
                if (data[row, 13] != null) visitor.Column14 = data[row, 13]; else visitor.Column14 = "none";
                if (data[row, 14] != null) visitor.Column15 = data[row, 14]; else visitor.Column15 = "none";
                if (data[row, 15] != null) visitor.CurrentStatus = data[row, 15]; else visitor.CurrentStatus = "none";

                visitorCollection.Add(visitor);
            }
        }
        public void importVisitorsToCollectionWithId(List<Visitor> visitorCollection, Action<Progress_Bar> progressChanged_)
        {
            for (int row = 0; row < excelWorksheetRow; row++)
            {
                Progress_Bar progress = new Progress_Bar { Visible = true, Progress = 0, Status = "import Exel data to database" };

                Visitor visitor = new Visitor();
                progress.Progress = row * 100 / excelWorksheetRow;
                progressChanged_(progress);

                try { visitor.Id = int.Parse(data[row, 0]); }
                catch { visitor.Id = 0; }
                if (data[row, 1] != null) visitor.Column1 = data[row, 1]; else visitor.Column1 = "none";
                if (data[row, 2] != null) visitor.Column2 = data[row, 2]; else visitor.Column2 = "none";
                if (data[row, 3] != null) visitor.Column3 = data[row, 3]; else visitor.Column3 = "none";
                if (data[row, 4] != null) visitor.Column4 = data[row, 4]; else visitor.Column4 = "none";
                if (data[row, 5] != null) visitor.Column5 = data[row, 5]; else visitor.Column5 = "none";
                if (data[row, 6] != null) visitor.Column6 = data[row, 6]; else visitor.Column6 = "none";
                if (data[row, 7] != null) visitor.Column7 = data[row, 7]; else visitor.Column7 = "none";
                if (data[row, 8] != null) visitor.Column8 = data[row, 8]; else visitor.Column8 = "none";
                if (data[row, 9] != null) visitor.Column9 = data[row, 9]; else visitor.Column9 = "none";
                if (data[row, 10] != null) visitor.Column10 = data[row, 10]; else visitor.Column10 = "none";
                if (data[row, 11] != null) visitor.Column11 = data[row, 11]; else visitor.Column11 = "none";
                if (data[row, 12] != null) visitor.Column12 = data[row, 12]; else visitor.Column12 = "none";
                if (data[row, 13] != null) visitor.Column13 = data[row, 13]; else visitor.Column13 = "none";
                if (data[row, 14] != null) visitor.Column14 = data[row, 14]; else visitor.Column14 = "none";
                if (data[row, 15] != null) visitor.Column15 = data[row, 15]; else visitor.Column15 = "none";
                if (data[row, 16] != null) visitor.CurrentStatus = data[row, 16]; else visitor.CurrentStatus = "none";

                visitorCollection.Add(visitor);
            }
        }
        public void importSatausToCollectionWithId(List<Status> statusCollection, Action<Progress_Bar> progressChanged_)
        {
            for (int row = 0; row < excelWorksheetRow; row++)
            {
                Progress_Bar progress = new Progress_Bar { Visible = true, Progress = 0, Status = "import Exel data to database" };

                Status status = new Status();
                progress.Progress = row * 100 / excelWorksheetRow;
                progressChanged_(progress);

                try { status.Id = int.Parse(data[row, 0]); }
                catch { status.Id = 0; }

                status.Name = data[row, 1];
                status.ActionTime = data[row, 2];
                try { status.UserId = int.Parse(data[row, 3]); }
                catch { status.UserId = 0; }
                try { status.VisitorId = int.Parse(data[row, 4]); }
                catch { status.VisitorId = 0; }

                statusCollection.Add(status);
            }
        }
        public void saveVisitorsTofile(IEnumerable<Visitor> visitors)
        {
            var _visitors = visitors.ToArray();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName + ".xlsx";
                Microsoft.Office.Interop.Excel.Application exelapp = 
                    new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = exelapp.Workbooks.Add();
                Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;

                worksheet.Rows[1].Columns[1] = "Id";
                worksheet.Rows[1].Columns[2] = "Magik_ID";
                worksheet.Rows[1].Columns[3] = "Payment_Status";
                worksheet.Rows[1].Columns[4] = "none";
                worksheet.Rows[1].Columns[5] = "Категория";
                worksheet.Rows[1].Columns[6] = "none";
                worksheet.Rows[1].Columns[7] = "Замена";
                worksheet.Rows[1].Columns[8] = "Коментарий";
                worksheet.Rows[1].Columns[9] = "Name";
                worksheet.Rows[1].Columns[10] = "SurName";
                worksheet.Rows[1].Columns[11] = "Position";
                worksheet.Rows[1].Columns[12] = "Company";
                worksheet.Rows[1].Columns[13] = "Date";
                worksheet.Rows[1].Columns[14] = "none";
                worksheet.Rows[1].Columns[15] = "none";
                worksheet.Rows[1].Columns[16] = "none";
                worksheet.Rows[1].Columns[17] = "status";

                for (int i = 1; i < visitors.Count(); i++)
                {
                    worksheet.Rows[i + 1].Columns[1] = _visitors[i].Id;
                    worksheet.Rows[i + 1].Columns[2] = _visitors[i].Column1;
                    worksheet.Rows[i + 1].Columns[3] = _visitors[i].Column2;
                    worksheet.Rows[i + 1].Columns[4] = _visitors[i].Column3;
                    worksheet.Rows[i + 1].Columns[5] = _visitors[i].Column4;
                    worksheet.Rows[i + 1].Columns[6] = _visitors[i].Column5;
                    worksheet.Rows[i + 1].Columns[7] = _visitors[i].Column6;
                    worksheet.Rows[i + 1].Columns[8] = _visitors[i].Column7;
                    worksheet.Rows[i + 1].Columns[9] = _visitors[i].Column8;
                    worksheet.Rows[i + 1].Columns[10] = _visitors[i].Column9;
                    worksheet.Rows[i + 1].Columns[11] = _visitors[i].Column10;
                    worksheet.Rows[i + 1].Columns[12] = _visitors[i].Column11;
                    worksheet.Rows[i + 1].Columns[13] = _visitors[i].Column12;
                    worksheet.Rows[i + 1].Columns[14] = _visitors[i].Column13;
                    worksheet.Rows[i + 1].Columns[15] = _visitors[i].Column14;
                    worksheet.Rows[i + 1].Columns[16] = _visitors[i].Column15;
                    worksheet.Rows[i + 1].Columns[17] = _visitors[i].CurrentStatus;
                }

                exelapp.AlertBeforeOverwriting = false;
                workbook.SaveAs(path);
                exelapp.Quit();
            }
        }
        public void saveStatusesToFile(IEnumerable<Status> statuses)
        {
            var _statuses = statuses.ToArray();
            SaveFileDialog sfd = new SaveFileDialog();
            if(sfd.ShowDialog() == true)
            {
                string path = sfd.FileName + ".xlsx";
                Microsoft.Office.Interop.Excel.Application exelapp = 
                    new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = exelapp.Workbooks.Add();
                Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;

                worksheet.Rows[1].Columns[1] = "Id";
                worksheet.Rows[1].Columns[2] = "Статус";
                worksheet.Rows[1].Columns[3] = "Время";
                worksheet.Rows[1].Columns[4] = "UserId";
                worksheet.Rows[1].Columns[5] = "VisitorId";

                for(int i = 1; i<statuses.Count(); i++)
                {
                    worksheet.Rows[i + 1].Columns[1] = _statuses[i].Id;
                    worksheet.Rows[i + 1].Columns[2] = _statuses[i].Name;
                    worksheet.Rows[i + 1].Columns[3] = _statuses[i].ActionTime;
                    worksheet.Rows[i + 1].Columns[4] = _statuses[i].UserId;
                    worksheet.Rows[i + 1].Columns[5] = _statuses[i].VisitorId;
                }

                exelapp.AlertBeforeOverwriting = false;
                workbook.SaveAs(path);
                exelapp.Quit();
            }
        }
        public void saveVisitorsWithStatusesToFile
            (IEnumerable<Visitor> visitors, IEnumerable<Status> statuses)
        {
            var _visitors = visitors.ToArray();
            var _statuses = statuses.ToArray();
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                string path = sfd.FileName + ".xlsx";
                Microsoft.Office.Interop.Excel.Application exelapp =
                    new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = exelapp.Workbooks.Add();
                Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;

                worksheet.Rows[1].Columns[1] = "Id";
                worksheet.Rows[1].Columns[2] = "Статус";
                worksheet.Rows[1].Columns[3] = "Время";
                worksheet.Rows[1].Columns[4] = "UserId";
                worksheet.Rows[1].Columns[5] = "VisitorId";

                for (int i = 1; i < statuses.Count(); i++)
                {
                    worksheet.Rows[i + 1].Columns[1] = _statuses[i].Id;
                    worksheet.Rows[i + 1].Columns[2] = _statuses[i].Name;
                    worksheet.Rows[i + 1].Columns[3] = _statuses[i].ActionTime;
                    worksheet.Rows[i + 1].Columns[4] = _statuses[i].UserId;
                    worksheet.Rows[i + 1].Columns[5] = _statuses[i].VisitorId;
                }

                exelapp.AlertBeforeOverwriting = false;
                workbook.SaveAs(path);
                exelapp.Quit();
            }
        }


    }
}
