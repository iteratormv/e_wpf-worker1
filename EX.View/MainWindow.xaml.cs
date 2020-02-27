using EX.Model.DTO;
using EX.Model.Exel;
using EX.Model.Repositories;
using EX.Model.Repositories.ForVisitor;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Excel;

namespace EX.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            stbo.Focus();
 //           lb.ItemsSource = new string[] {"sfdas", "asdf", "fdafdsfa" };
 //           lb.SelectedItem = "sfdas";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VisitorRepositoryDTO visitorRepository = new VisitorRepositoryDTO();
            var col = visitorRepository.GetAllVisitors().ToList();
            foreach(var v in col)
            {
                if(v.CurrentStatus == "actual" | v.CurrentStatus == "create")
                {
                    v.Column15 = v.Column12;
                    visitorRepository.AddOrUpdateVisitor(v);
                }

            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var rep = new VisitorRepositoryDTO();
            var visitorsInDB = rep.GetAllVisitors().ToList();

            FileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == true)
            {
                ExelData exelData = new ExelData(fileDialog.FileName, 20);
                for(int i = 1; i<exelData.excelWorksheetRow; i++)
                {
                    try
                    {
                        var v = visitorsInDB
                        .Where(s => s.Column1 == exelData.data[i, 0])
                        .FirstOrDefault();
                        if (v.CurrentStatus == "registered")
                        {
                            v.Column15 = exelData.data[i, 19];
                            rep.AddOrUpdateVisitor(v);
                        }
                    }
                    catch { }
                }
            } 
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var rep = new VisitorRepositoryDTO();
            var visitorsInDB = rep.GetAllVisitors().ToList();

            FileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                ExelData exelData = new ExelData(fileDialog.FileName, 20);
                for (int i = 1; i < exelData.excelWorksheetRow; i++)
                {
                    try
                    {
                        var v = visitorsInDB
                        .Where(s => s.Column1 == exelData.data[i, 0])
                        .FirstOrDefault();
                        if (v.CurrentStatus == "actual")
                        {
                            v.Column12 = exelData.data[i, 11];
                            rep.AddOrUpdateVisitor(v);
                        }
                    }
                    catch { }
                }
            }
        }






   //     SaveFileDialog sfd = new SaveFileDialog();
   //     DialogResult res = sfd.ShowDialog();
			//if (res == DialogResult.OK)
			//{
			//	string path = sfd.FileName + ".xlsx";
   //     Microsoft.Office.Interop.Excel.Application exelapp = new Microsoft.Office.Interop.Excel.Application();
   //     Microsoft.Office.Interop.Excel.Workbook workbook = exelapp.Workbooks.Add();
   //     Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;

   //     pb.Visible = true;
			//	pb.Maximum = dgv_visitors.RowCount-1;

			//	worksheet.Rows[1].Columns[1] = "Имя";
			//	worksheet.Rows[1].Columns[2] = "Фамилия";
			//	worksheet.Rows[1].Columns[3] = "Отчество";
			//	worksheet.Rows[1].Columns[4] = "Компания";
			//	worksheet.Rows[1].Columns[5] = "Должность";
			//	worksheet.Rows[1].Columns[6] = "Штрихкод";
			//	worksheet.Rows[1].Columns[7] = "Вы являетесь";
			//	worksheet.Rows[1].Columns[8] = "Телефон мобильный";
			//	worksheet.Rows[1].Columns[9] = "Телефон рабочий";
			//	worksheet.Rows[1].Columns[10] = "E-Mail";
			//	worksheet.Rows[1].Columns[11] = "Дата";
			//	worksheet.Rows[1].Columns[12] = "Выставка";
			//	worksheet.Rows[1].Columns[13] = "Доклад";
			//	worksheet.Rows[1].Columns[14] = "Город";

			//	for (int i = 1; i<dgv_visitors.RowCount; i++)
			//	{
			//		pb.Value = i;					
			//		worksheet.Rows[i + 1].Columns[1] = dgv_visitors.Rows[i - 1].Cells[1].Value;
			//		worksheet.Rows[i + 1].Columns[2] = dgv_visitors.Rows[i - 1].Cells[2].Value;
			//		worksheet.Rows[i + 1].Columns[3] = dgv_visitors.Rows[i - 1].Cells[3].Value;
			//		worksheet.Rows[i + 1].Columns[4] = dgv_visitors.Rows[i - 1].Cells[4].Value;
			//		worksheet.Rows[i + 1].Columns[5] = dgv_visitors.Rows[i - 1].Cells[5].Value;
			//		worksheet.Rows[i + 1].Columns[6] = dgv_visitors.Rows[i - 1].Cells[6].Value;
			//		worksheet.Rows[i + 1].Columns[7] = dgv_visitors.Rows[i - 1].Cells[7].Value;
			//		worksheet.Rows[i + 1].Columns[8] = dgv_visitors.Rows[i - 1].Cells[8].Value;
			//		worksheet.Rows[i + 1].Columns[9] = dgv_visitors.Rows[i - 1].Cells[9].Value;
			//		worksheet.Rows[i + 1].Columns[10] = dgv_visitors.Rows[i - 1].Cells[10].Value;
			//		worksheet.Rows[i + 1].Columns[11] = dgv_visitors.Rows[i - 1].Cells[11].Value;
			//		worksheet.Rows[i + 1].Columns[12] = dgv_visitors.Rows[i - 1].Cells[12].Value;
			//		worksheet.Rows[i + 1].Columns[13] = dgv_visitors.Rows[i - 1].Cells[13].Value;
			//		worksheet.Rows[i + 1].Columns[14] = dgv_visitors.Rows[i - 1].Cells[14].Value;
			//	}
   // pb.Visible = false;
			//	exelapp.AlertBeforeOverwriting = false;
			//	workbook.SaveAs(path);
			//	exelapp.Quit();
			//}





private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FileDialog openFileDialog = new OpenFileDialog();
            List<VisitorDTO> addedInfVisitors;
            if (openFileDialog.ShowDialog() == true)
            {
                ExelData exelData = new ExelData(openFileDialog.FileName, 20);
                addedInfVisitors = new List<VisitorDTO>();
                for(int i = 0; i<exelData.excelWorksheetRow; i++)
                {
                    var nv = new VisitorDTO
                    {
                        Column1 = exelData.data[i, 0],
                        Column2 = exelData.data[i, 1],
                        Column3 = exelData.data[i, 2],
                        Column4 = exelData.data[i, 3],
                        Column5 = exelData.data[i, 4],
                        Column6 = exelData.data[i, 5],
                        Column7 = exelData.data[i, 6],
                        Column8 = exelData.data[i, 7],
                        Column9 = exelData.data[i, 8],
                        Column10 = exelData.data[i, 14],
                        Column11 = exelData.data[i, 15],
                        Column12 = exelData.data[i, 16],
                        Column13 = exelData.data[i, 17],
                        Column14 = exelData.data[i, 18],
                        Column15 = exelData.data[i, 19],
                        CurrentStatus = "registered"
                    };
                    addedInfVisitors.Add(nv);
                }

                FileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    var path = saveFileDialog.FileName + ".xlsx";
                    var repv = new VisitorRepositoryDTO();
                    var vindb = repv.GetAllVisitors();
                    var reps = new StatusRepositoryDTO();
                    var sindb = reps.GetAllStatuses();

                    Microsoft.Office.Interop.Excel.Application exelapp =
                        new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workbook = 
                        exelapp.Workbooks.Add();
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = 
                        workbook.ActiveSheet;
                    int i = 0;
                    foreach (var stat in sindb)
                    {
                        var vis = vindb.Where(s => s.Id == stat.VisitorId).FirstOrDefault();
                        var visAddInf = addedInfVisitors.Where(s => s.Column1 == vis.Column1).FirstOrDefault();
                        var choice = vis.Column1.Split('-')[0];
                        switch (choice)
                        {
                            case "VHS":
                                worksheet.Rows[i + 1].Columns[1] = vis.Column1;
                                worksheet.Rows[i + 1].Columns[2] = vis.Column2;
                                worksheet.Rows[i + 1].Columns[3] = vis.Column3;
                                worksheet.Rows[i + 1].Columns[4] = vis.Column4;
                                worksheet.Rows[i + 1].Columns[5] = vis.Column5;
                                worksheet.Rows[i + 1].Columns[6] = stat.Name;
                                worksheet.Rows[i + 1].Columns[7] = vis.Column7;
                                worksheet.Rows[i + 1].Columns[8] = vis.Column8;
                                worksheet.Rows[i + 1].Columns[9] = vis.Column9;
                                worksheet.Rows[i + 1].Columns[10] = vis.Column10;
                                worksheet.Rows[i + 1].Columns[11] = vis.Column11;
                                worksheet.Rows[i + 1].Columns[12] = stat.ActionTime;
                                worksheet.Rows[i + 1].Columns[12] = vis.Column12;
                                worksheet.Rows[i + 1].Columns[13] = vis.Column13;
                                worksheet.Rows[i + 1].Columns[14] = vis.Column14;
                                worksheet.Rows[i + 1].Columns[15] = " ";
                                worksheet.Rows[i + 1].Columns[16] = " ";
                                worksheet.Rows[i + 1].Columns[17] = " ";
                                worksheet.Rows[i + 1].Columns[18] = " ";
                                worksheet.Rows[i + 1].Columns[19] = " ";
                                worksheet.Rows[i + 1].Columns[20] = " ";
                                worksheet.Rows[i + 1].Columns[21] = stat.ActionTime;
                                break;
                            default:
                                worksheet.Rows[i + 1].Columns[1] = vis.Column1;
                                worksheet.Rows[i + 1].Columns[2] = vis.Column2;
                                worksheet.Rows[i + 1].Columns[3] = vis.Column3;
                                worksheet.Rows[i + 1].Columns[4] = vis.Column4;
                                worksheet.Rows[i + 1].Columns[5] = vis.Column5;
                                worksheet.Rows[i + 1].Columns[6] = stat.Name;
                                worksheet.Rows[i + 1].Columns[7] = vis.Column7;
                                worksheet.Rows[i + 1].Columns[8] = vis.Column8;
                                worksheet.Rows[i + 1].Columns[9] = vis.Column9;
                                worksheet.Rows[i + 1].Columns[10] = vis.Column10;
                                worksheet.Rows[i + 1].Columns[11] = vis.Column11;
                                worksheet.Rows[i + 1].Columns[12] = vis.Column12;
                                worksheet.Rows[i + 1].Columns[13] = vis.Column13;
                                worksheet.Rows[i + 1].Columns[14] = vis.Column14;
                                worksheet.Rows[i + 1].Columns[15] = visAddInf.Column10;
                                worksheet.Rows[i + 1].Columns[16] = visAddInf.Column11;
                                worksheet.Rows[i + 1].Columns[17] = visAddInf.Column12;
                                worksheet.Rows[i + 1].Columns[18] = visAddInf.Column13;
                                worksheet.Rows[i + 1].Columns[19] = visAddInf.Column14;
                                worksheet.Rows[i + 1].Columns[20] = visAddInf.Column15;
                                switch (stat.Name)
                                {
                                    case "registered":
                                        var temp = visAddInf.Column15;
                                        var time = temp.Replace("T", " ");
                                        var time_correct = time.Split('+')[0] + ":00";
                                        worksheet.Rows[i + 1].Columns[21] = time_correct;
                                        break;
                                    default:
                                        worksheet.Rows[i + 1].Columns[21] = stat.ActionTime;
                                        break;
                                }
 //                               worksheet.Rows[i + 1].Columns[21] = stat.ActionTime;
                                break;
                        }
                        i++;
                    }
                    
                    exelapp.AlertBeforeOverwriting = false;
                    workbook.SaveAs(path);
                    exelapp.Quit();
                }
            }            
        }

        //private void stbo_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if(e.Key == Key.Down)
        //    {
        //        testTB.Text = "проверка";
        //    }
        //}

   //     private void stbo_KeyUp(object sender, KeyEventArgs e)
   //     {
   //         if (e.Key == Key.Down)
   //         {
   ////             testTB.Text = "проверка";
   //             lb.Focus();
   //         }
   //     }

        private void stbo_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                lb.Focus();
            }
        }

        private void lb_KeyDown(object sender, KeyEventArgs e)
        {
            EX.ViewModel.MainVM vm = this.DataContext as EX.ViewModel.MainVM;
            if (e.Key == Key.Enter&&btn_save_desctop.IsEnabled == false)
            {
 
                if ((vm != null) && (vm.AddVisitorToFact.CanExecute(null)))
                {
                    stbo.Text = "";
                    vm.AddVisitorToFact.Execute(null);
                }
                    
            }
            else if(e.Key == Key.Enter&&btn_save_desctop.IsEnabled == true)
            {
                vm.SaveEditVisitor.Execute(null);
                stbo.Focus();
            }
        }

        private void TabItem_KeyUp(object sender, KeyEventArgs e)
        {
            EX.ViewModel.MainVM vm = this.DataContext as EX.ViewModel.MainVM;
            if (e.Key == Key.Escape)
            {
                vm.ResetEditDesctopVisitor();
            }
        }

        private void workgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void CheckBox_Checked(object sender, RoutedEventArgs e)
        //{
        //    EX.ViewModel.MainVM vm = this.DataContext as EX.ViewModel.MainVM;
        //    var ds = vm
        //        .DisplaySettingsForm
        //        .Where(s => s.Intendant == "form")
        //        .Where(s => s.IsSelected = true)
        //        .FirstOrDefault();

        //    var dsc = vm
        //        .DSCollumnSettingDTORepository
        //        .GetAllDSCollumnSettingDTOs()
        //        .Where(s => s.Intendant == "form")
        //        .Where(s=>s.DisplaySettingId == ds.Id);

        //    vm.initLabelsForm(new System.Collections.ObjectModel
        //        .ObservableCollection<Model.DTO.Setting
        //        .DSCollumnSettingDTO>(dsc));
        //}
    }
}
