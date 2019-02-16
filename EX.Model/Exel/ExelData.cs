using EX.Model.DbLayer;
using EX.Model.Infrastructure;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data.Entity;

namespace EX.Model.Exel
{
    public class ExelData
    {

        public int excelWorksheetRow { get; }
        public int excelWorksheetCol { get; }
        public string[,] data { get; }

        public ExelData()
        {
            Microsoft.Office.Interop.Excel.Application excel_app =
                new Microsoft.Office.Interop.Excel.Application();

            string file_name = "C:\\Exhibition\\ExhibitionData.xls";

            Workbook work_book = excel_app.Workbooks.Open(file_name, Type.Missing);

            Worksheet work_shet = (Worksheet)work_book.Worksheets[1];

            Range excelRange = work_shet.UsedRange;

            object[,] vallueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

            excelWorksheetRow = work_shet.UsedRange.Rows.Count;
            excelWorksheetCol = 15;
            //      excelWorksheetCol = work_shet.UsedRange.Columns.Count;
            data = new string[excelWorksheetRow, excelWorksheetCol];

            for (int row = 1; row <= excelWorksheetRow; ++row)
            {
                for (int col = 1; col <= excelWorksheetCol; ++col)
                {
                    try
                    {
                        if (vallueArray[row, col] == null) data[row - 1, col - 1] = "empty";
                        else data[row - 1, col - 1] = vallueArray[row, col].ToString();
                    }
                    catch { data[row - 1, col - 1] = "none"; }
                }
            }
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
                        if (vallueArray[row, col] == null) data[row - 1, col - 1] = "empty";
                        else data[row - 1, col - 1] = vallueArray[row, col].ToString();
                    }
                    catch { data[row - 1, col - 1] = "none"; }
                }
            }
        }

        public ExelData(string fName, Action<Progress_Bar> progressChanged_)
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
                progress.Progress = (row * 100 / excelWorksheetRow);
                progressChanged_(progress);
                for (int col = 1; col <= excelWorksheetCol; ++col)
                {
                    try
                    {
                        if (vallueArray[row, col] == null) data[row - 1, col - 1] = "empty";
                        else data[row - 1, col - 1] = vallueArray[row, col].ToString();
                    }
                    catch { data[row - 1, col - 1] = "none"; }
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
    }
}
