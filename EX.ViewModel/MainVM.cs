﻿using AutoMapper;
using EX.Client;
using EX.Model.DbLayer.Settings;
using EX.Model.DTO;
using EX.Model.DTO.Setting;
using EX.Model.Infrastructure;
using EX.Model.Repositories;
using EX.Model.Repositories.Administration;
using EX.Model.Repositories.ForVisitor;
using EX.Model.Repositories.Setting;
using EX.Service;
using EX.ViewModel.Infrastructure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace EX.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        #region Context for Administration
        #region Visibte Tab Settigs
        int visibleManageUserRole;
        public int VisibleManageUserRole
        {
            get { return visibleManageUserRole; }
            set { visibleManageUserRole = value; OnPropertyChanged(nameof(VisibleManageUserRole)); }
        }
        #endregion
        #region Repositories for Administration
        UserRepositoryDTO userRepository;
        RoleRepositoryDTO roleRepository;
        UserInRoleRepositoryDTO userInRoleRepository;
        CommandRepositoryDTO commandRepository;
        TabRepositoryDTO tabRepository;
        StatusRepositoryDTO statusRepository;
        #endregion
        #region Collection for Administration
        ObservableCollection<UserDTO> users;
        ObservableCollection<RoleDTO> roles;
        ObservableCollection<CommandDTO> commands;
        ObservableCollection<TabDTO> tabs;
        ObservableCollection<SubTabDTO> subTabs;

        public ObservableCollection<UserDTO> Users { get { return users; } set { users = value; OnPropertyChanged(nameof(Users)); } }
        public ObservableCollection<RoleDTO> Roles { get { return roles; } set { roles = value; OnPropertyChanged(nameof(Roles)); } }
        public ObservableCollection<CommandDTO> Commands { get { return commands; } set { commands = value; OnPropertyChanged(nameof(Commands)); } }
        public ObservableCollection<TabDTO> Tabs { get { return tabs; } set { tabs = value; OnPropertyChanged(nameof(Tabs)); } }
        public ObservableCollection<SubTabDTO> SubTabs { get { return subTabs; } set { subTabs = value; OnPropertyChanged(nameof(SubTabs)); } }
        #endregion
        #region Fields for Administration
        UserDTO selectedUser;
        UserDTO regisrationUser;
        UserDTO authorizedUser;
        UserDTO defaultUser;
        RoleDTO selectedRole;
        RoleDTO defaultRole;
        RoleDTO addedRole;

        public UserDTO SelectedUser { get { return selectedUser; } set { selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); } }
        public UserDTO RegistrationUser { get { return regisrationUser; } set { regisrationUser = value; OnPropertyChanged(nameof(RegistrationUser)); } }
        public UserDTO AuthorizedUser { get { return authorizedUser; } set { authorizedUser = value; OnPropertyChanged(nameof(AuthorizedUser)); } }
        public UserDTO DefaultUser { get { return defaultUser; } set { defaultUser = value; OnPropertyChanged(nameof(DefaultUser)); } }
        public RoleDTO SelectedRole { get { return selectedRole; } set { selectedRole = value; OnPropertyChanged(nameof(SelectedRole)); } }
        public RoleDTO DefaultRole { get { return defaultRole; } set { defaultRole = value; OnPropertyChanged(nameof(DefaultRole)); } }
        public RoleDTO AddedRole { get { return addedRole; } set { addedRole = value; OnPropertyChanged(nameof(AddedRole)); } }
        #endregion
        #region Service fields for Administration
        string statusRegistration;
        string statusAuthorisation;
        string loginInUser;

        public string StatusRegistration { get { return statusRegistration; } set { statusRegistration = value; OnPropertyChanged(nameof(StatusRegistration)); } }
        public string StatusAuthorisation { get { return statusAuthorisation; } set { statusAuthorisation = value; OnPropertyChanged(nameof(StatusAuthorisation)); } }
        public string LoginInUser { get { return loginInUser; } set { loginInUser = value; OnPropertyChanged(nameof(LoginInUser)); } }
        #endregion
        #region Commands for Administration
        RelayCommand addUser;
        public RelayCommand AddUser { get { return addUser; } }

        RelayCommand delUser;
        public RelayCommand DelUser { get { return delUser; } }

        RelayCommand setDefaultUser;
        public RelayCommand SetDefaultUser { get { return setDefaultUser; } }

        RelayCommand userChanged;
        public RelayCommand UserChanged { get { return userChanged; } }

        RelayCommand addRole;
        public RelayCommand AddRole { get { return addRole; } }

        RelayCommand delRole;
        public RelayCommand DelRole { get { return delRole; } }

        RelayCommand roleChanged;
        public RelayCommand RoleChanged { get { return roleChanged; } }

        RelayCommand saveChanges;
        public RelayCommand SaveChanges { get { return saveChanges; } }

        RelayCommand setUserRole;
        public RelayCommand SetUserRole { get { return setUserRole; } }

        RelayCommand setDefaultRole;
        public RelayCommand SetDefaulRole { get { return setDefaultRole; } }

        RelayCommand authorisationUser;
        public RelayCommand AutorisitionUser { get { return authorisationUser; } }
        #endregion
        #endregion
        #region Context for Settings
        #region Repositories for Settings
        DisplaySettingDTORepository displaySettingDTORepository;
        DSCollumnSettingDTORepository dSCollumnSettingDTORepository;
        public DisplaySettingDTORepository DisplaySettingDTORepository
        {
            get { return displaySettingDTORepository;  }
            set { displaySettingDTORepository = value;
                OnPropertyChanged(nameof(DisplaySettingDTORepository));
            }
        }
        public DSCollumnSettingDTORepository DSCollumnSettingDTORepository
        {
            get { return dSCollumnSettingDTORepository;}
            set { dSCollumnSettingDTORepository = value;
                OnPropertyChanged(nameof(DSCollumnSettingDTORepository));
            }
        }

        PrintSettingRepositoryDTO printSettingRepositoryDTO;
        public PrintSettingRepositoryDTO PrintSettingRepositoryDTO
        {
            get { return printSettingRepositoryDTO; }
            set
            {
                printSettingRepositoryDTO = value;
                OnPropertyChanged(nameof(PrintSettingRepositoryDTO));

            }
        }
        PrintStringSettingRepositoryDTO printStringSettingRepositoryDTO;
        public PrintStringSettingRepositoryDTO PrintStringSettingRepositoryDTO
        {
            get { return printStringSettingRepositoryDTO; }
            set
            {
                printStringSettingRepositoryDTO = value;
                OnPropertyChanged(nameof(PrintStringSettingRepositoryDTO));
            }
        }
        #endregion
        #region Collection for Settings
        #region Raport
        ObservableCollection<DisplaySettingDTO> displaySettingsRaport;
        ObservableCollection<DSCollumnSettingDTO> dSCollumnSettingsRaport;
        public ObservableCollection<DisplaySettingDTO> DisplaySettingsRaport
        {
            get { return displaySettingsRaport; }
            set { displaySettingsRaport = value;
                OnPropertyChanged(nameof(DisplaySettingsRaport));
            }
        }
        public ObservableCollection<DSCollumnSettingDTO> DSCollumnSettingsRaport
        {
            get { return dSCollumnSettingsRaport; }
            set
            {
                dSCollumnSettingsRaport = value;
                initHeaderRaport(dSCollumnSettingsRaport);
                OnPropertyChanged(nameof(DSCollumnSettingsRaport));
            }
        }
        #endregion
        #region Desctop
        ObservableCollection<DisplaySettingDTO> displaySettingsDesctop;
        ObservableCollection<DSCollumnSettingDTO> dSCollumnSettingsDesctop;
        public ObservableCollection<DisplaySettingDTO> DisplaySettingsDesctop
        {
            get { return displaySettingsDesctop; }
            set
            {
                displaySettingsDesctop = value;
                OnPropertyChanged(nameof(DisplaySettingsDesctop));
            }
        }
        public ObservableCollection<DSCollumnSettingDTO> DSCollumnSettingsDesctop
        {
            get { return dSCollumnSettingsDesctop; }
            set
            {
                dSCollumnSettingsDesctop = value;
                initHeaderDesctop(dSCollumnSettingsDesctop);
                OnPropertyChanged(nameof(DSCollumnSettingsDesctop));
            }
        }

        #endregion
        #region Form
        ObservableCollection<DisplaySettingDTO> displaySettingsForm;
        ObservableCollection<DSCollumnSettingDTO> dSCollumnSettingsForm;
        public ObservableCollection<DisplaySettingDTO> DisplaySettingsForm
        {
            get { return displaySettingsForm; }
            set
            {
                displaySettingsForm = value;
                OnPropertyChanged(nameof(DisplaySettingsForm));
            }
        }
        public ObservableCollection<DSCollumnSettingDTO> DSCollumnSettingsForm
        {
            get { return dSCollumnSettingsForm; }
            set
            {
                dSCollumnSettingsForm = value;
                initLabelsForm(dSCollumnSettingsForm);
                OnPropertyChanged(nameof(DSCollumnSettingsForm));
            }
        }
        #endregion
        #region PrintBage
        ObservableCollection<PrintSettingDTO> printSettingDTOs;
        public ObservableCollection<PrintSettingDTO> PrintSettingDTOs
        {
            get { return printSettingDTOs; }
            set
            {
                printSettingDTOs = value;
                OnPropertyChanged(nameof(PrintSettingDTOs));
            }
        }
        ObservableCollection<PrintStringSettingDTO> printStringSettingDTOs;
        public ObservableCollection<PrintStringSettingDTO> PrintStringSettingDTOs
        {
            get { return printStringSettingDTOs; }
            set
            {
                printStringSettingDTOs = value;
                OnPropertyChanged(nameof(PrintStringSettingDTOs));
            }
        }
        #endregion
        #endregion
        #region Fields for Settings
        #region Raport
        DisplaySettingDTO selectedDisplaySettingRaport;
        DSCollumnSettingDTO selectedCollumnSettingRaport;
        public DisplaySettingDTO SelectedDisplaySettingRaport
        {
            get { return selectedDisplaySettingRaport; }
            set { selectedDisplaySettingRaport = value;
                OnPropertyChanged(nameof(SelectedDisplaySettingRaport)); }
        }
        public DSCollumnSettingDTO SelectedCollumnSettingRaport
        {
            get { return selectedCollumnSettingRaport; }
            set { selectedCollumnSettingRaport = value;
                OnPropertyChanged(nameof(SelectedCollumnSettingRaport)); }
        }
        #endregion
        #region Desctop
        DisplaySettingDTO selectedDisplaySettingDesctop;
        public DisplaySettingDTO SelectedDisplaySettingDesctop
        {
            get { return selectedDisplaySettingDesctop; }
            set
            {
                selectedDisplaySettingDesctop = value;
                OnPropertyChanged(nameof(SelectedDisplaySettingDesctop));
            }
        }
        DSCollumnSettingDTO selectedCollumnSettingDesctop;
        public DSCollumnSettingDTO SelectedCollumnSettingDesctop
        {
            get { return selectedCollumnSettingDesctop; }
            set
            {
                selectedCollumnSettingDesctop = value;
                OnPropertyChanged(nameof(SelectedCollumnSettingDesctop));
            }
        }
        #endregion
        #region Form
        DisplaySettingDTO selectedDisplaySettingForm;
        DSCollumnSettingDTO selectedCollumnSettingForm;
        public DisplaySettingDTO SelectedDisplaySettingForm
        {
            get { return selectedDisplaySettingForm; }
            set
            {
                selectedDisplaySettingForm = value;
                OnPropertyChanged(nameof(SelectedDisplaySettingForm));
            }
        }
        public DSCollumnSettingDTO SelectedCollumnSettingForm
        {
            get { return selectedCollumnSettingForm; }
            set
            {
                selectedCollumnSettingForm = value;
                OnPropertyChanged(nameof(SelectedCollumnSettingForm));
            }
        }
        #endregion
        #region PrintBage
        PrintSettingDTO selectedPrintSetting;
        public PrintSettingDTO SelectedPrintSetting
        {
            get { return selectedPrintSetting; }
            set
            {
                selectedPrintSetting = value;
                OnPropertyChanged(nameof(SelectedPrintSetting));
            }
        }
        PrintStringSettingDTO selectedPrintStringSetting;
        public PrintStringSettingDTO SelectedPrintStringSetting
        {
            get { return selectedPrintStringSetting; }
            set
            {
                selectedPrintStringSetting = value;
                OnPropertyChanged(nameof(SelectedPrintStringSetting));
            }
        }
        string selectedFont;
        public string SelectedFont
        {
            get { return selectedFont; }
            set
            {
                selectedFont = value;
                OnPropertyChanged(nameof(SelectedFont));
            }
        }
        #endregion
        #endregion
        #region Service fields for Setting
        #region Desctop
        const int count_headers = 16;
        bool[] columnCheckedDesctop;
        public bool[] ColumnCheckedDesctop
        {
            get { return columnCheckedDesctop; }
            set {
                columnCheckedDesctop = value;
                OnPropertyChanged(nameof(ColumnCheckedDesctop)); }
        }
        string[] aliasDesctop;
        public string[] AliasDesctop
        {
            get { return aliasDesctop; }
            set {
                aliasDesctop = value;
                OnPropertyChanged(nameof(AliasDesctop)); }
        }
        int[] widthDesctop;
        public int[] WidthDesctop
        {
            get { return widthDesctop; }
            set {
                widthDesctop = value;
                OnPropertyChanged(nameof(WidthDesctop)); }
        }
        #endregion
        #region Raport
        const int count_raport_headers = 16;
        bool[] columnCheckedRaport;
        public bool[] ColumnCheckedRaport
        {
            get { return columnCheckedRaport; }
            set
            {
                columnCheckedRaport = value;
                OnPropertyChanged(nameof(ColumnCheckedRaport));
            }
        }
        string[] aliasRaport;
        public string[] AliasRaport
        {
            get { return aliasRaport; }
            set
            {
                aliasRaport = value;
                OnPropertyChanged(nameof(AliasRaport));
            }
        }
        int[] widthRaport;
        public int[] WidthRaport
        {
            get { return widthRaport; }
            set
            {
                widthRaport = value;
                OnPropertyChanged(nameof(WidthRaport));
            }
        }
        #endregion
        #region Form
        const int count_form_fields = 16;
        bool[] rowCheckedForm;
        public bool[] RowChedForm
        {
            get { return rowCheckedForm; }
            set
            {
                rowCheckedForm = value;
                OnPropertyChanged(nameof(RowChedForm));
            }
        }
        string[] aliasForm;
        public string[] AliasForm
        {
            get { return aliasForm; }
            set
            {
                aliasForm = value;
                OnPropertyChanged(nameof(AliasForm));
            }
        }
        int[] heightForm;
        public int[] HeightForm
        {
            get { return heightForm; }
            set
            {
                heightForm = value;
                OnPropertyChanged(nameof(HeightForm));
            }
        }

        #endregion
        #endregion
        #region Commands for Setings
        #region Raport
        RelayCommand addCollumnRaport;
        public RelayCommand AddCollumnRaport
        {
            get { return addCollumnRaport; }
        }

        RelayCommand delCollumnRaport;
        public RelayCommand DelCollumnRaport
        {
            get { return delCollumnRaport; }
        }

        RelayCommand addSettingRaport;
        public RelayCommand AddSettingRaport
        {
            get { return addSettingRaport; }
        }

        RelayCommand delSettingRaport;
        public RelayCommand DelSettingRaport
        {
            get { return delSettingRaport; }
        }

        RelayCommand saveSettingChangesRaport;
        public RelayCommand SaveSettingChangesRaport
        {
            get { return saveSettingChangesRaport; }
        }

        RelayCommand changeDisplaySettingDefaultRaport;
        public RelayCommand ChangeDisplaySettingDefaultRaport
        {
            get { return changeDisplaySettingDefaultRaport; }
        }
        #endregion
        #region Desctop
        RelayCommand addCollumnDesctop;
        public RelayCommand AddCollumnDesctop
        {
            get { return addCollumnDesctop; }
        }
        RelayCommand delCollumnDesctop;
        public RelayCommand DelCollumnDesctop
        {
            get { return delCollumnDesctop; }
        }
        RelayCommand addSettingDesctop;
        public RelayCommand AddSettingDesctop
        {
            get { return addSettingDesctop; }
        }
        RelayCommand delSettingDesctop;
        public RelayCommand DelSettingDesctop
        {
            get { return delSettingDesctop; }
        }
        RelayCommand saveSettingChangesDesctop;
        public RelayCommand SaveSettingChangesDesctop
        {
            get { return saveSettingChangesDesctop; }
        }
        RelayCommand changeDisplaySettingDefaultDesctop;
        public RelayCommand ChangeDisplaySettingDefaultDesctop
        {
            get { return changeDisplaySettingDefaultDesctop; }
        }
        #endregion
        #region Form
        RelayCommand addCollumnForm;
        public RelayCommand AddCollumnForm
        {
            get { return addCollumnForm; }
        }
        RelayCommand delCollumnForm;
        public RelayCommand DelCollumnForm
        {
            get { return delCollumnForm; }
        }
        RelayCommand addSettingForm;
        public RelayCommand AddSettingForm
        {
            get { return addSettingForm; }
        }
        RelayCommand delSettingForm;
        public RelayCommand DelSettingForm
        {
            get { return delSettingForm; }
        }
        RelayCommand saveSettingChangesForm;
        public RelayCommand SaveSettingChangesForm
        {
            get { return saveSettingChangesForm; }
        }
        RelayCommand changeDisplaySettingDefaultForm;
        public RelayCommand ChangeDisplaySettingDefaultForm
        {
            get { return changeDisplaySettingDefaultForm; }
        }
        #endregion
        #region PrintBage
        RelayCommand addPrintStringSetting;
        public RelayCommand AddPrintStringSetting
        {
            get { return addPrintStringSetting; }
        }
        RelayCommand delPrintStringSetting;
        public RelayCommand DelPrintStringSetting
        {
            get { return delPrintStringSetting; }
        }
        RelayCommand addPrintSetting;
        public RelayCommand AddPrintSetting
        {
            get { return addPrintSetting; }
        }
        RelayCommand delPrintSetting;
        public RelayCommand DelPrintSetting
        {
            get { return delPrintSetting; }
        }
        RelayCommand changePrintSettingDefault;
        public RelayCommand ChangePrintSettingDefault
        {
            get { return changePrintSettingDefault; }
        }
        RelayCommand savePrintSettingChanges;
        public RelayCommand SavePrintSettingChanges
        {
            get { return savePrintSettingChanges; }
        }
        #endregion
        #endregion
        #endregion
        #region Context for File
        #region Repository for File
        VisitorRepositoryDTO visitorRepositoryDTO;
   //     StatusRepositoryDTO statusRepositoryDTO;
        #endregion
        #region Collection for File
        ObservableCollection<VisitorDTO> visitors;
        public ObservableCollection<VisitorDTO> Visitors
        {
            get { return visitors; }
            set { visitors = value; OnPropertyChanged(nameof(Visitors)); }
        }
        ObservableCollection<StatusDTO> statuses;
        public ObservableCollection<StatusDTO> Statuses
        {
            get { return statuses; }
            set { statuses = value;
                OnPropertyChanged(nameof(Statuses));
            }
        }
        #endregion
        #region Filds for File
        Progress_Bar progressBar;
        public Progress_Bar _ProgressBar
        {
            get { return progressBar; }
            set { progressBar = value; OnPropertyChanged(nameof(_ProgressBar)); }
        }
        #endregion
        #region Commands for File
        RelayCommand addDataFromFileToDatabase;
        public RelayCommand AddDataFromFileToDatabase
        {
            get { return addDataFromFileToDatabase; }
        }
        RelayCommand importDataFromFileToDatabase;
        public RelayCommand ImportDataFromFileToDataBase
        {
            get { return importDataFromFileToDatabase; }
        }
        RelayCommand importVisitorFromFileWithId;
        public RelayCommand ImportVisitorFromFileWithId
        {
            get { return importVisitorFromFileWithId; }
        }
        #endregion
        #endregion
        #region Context for Sevice
        #region Fields for Service
        ServiceExecutor serviceExecutor;
        string dataMode;
        string serverStatus;
        bool isEnabledDataModeChecker;

        public string DataMode
        {
            get
            {
                if(dataMode!= "Сервер службы баз данных")
                {
                    ServerStatus = "";
                }
                return dataMode;
            }
            set { dataMode = value; OnPropertyChanged(nameof(DataMode)); }
        }
        public string ServerStatus
        {
            get { return serverStatus; }
            set { serverStatus = value; OnPropertyChanged(nameof(ServerStatus)); }
        }
        public bool IsEnabledDataModeChecker
        {
            get { return isEnabledDataModeChecker; }
            set { isEnabledDataModeChecker = value; OnPropertyChanged(nameof(IsEnabledDataModeChecker)); }
        }
        #endregion
        #region Command for Service
        RelayCommand changeDataMode;
        public RelayCommand ChangeDataMode { get { return changeDataMode; } }

        RelayCommand startServer;
        public RelayCommand StartServer
        {
            get { return startServer; }
        }

        RelayCommand stopServer;
        public RelayCommand StopServer
        {
            get { return stopServer; }
        }
        #endregion
        #endregion
        #region Context for Client
        ClientExecutor clientExecutor;
        IMapper mapper;
        string errorConnectServer;
        public string ErrorConnectServer
        {
            get { return errorConnectServer; }
            set
            {
                errorConnectServer = value;
                OnPropertyChanged(nameof(ErrorConnectServer));
            }
        }
        #endregion
        #region Context Desctop Operation
        #region Collection for Desctop Operation
        ObservableCollection<VisitorDTO> desctopVisitors;
        public ObservableCollection<VisitorDTO> DesctopVisitors
        {
            get { return desctopVisitors; }
            set
            {
                desctopVisitors = value;
                OnPropertyChanged(nameof(DesctopVisitors));
            }
        }
        ObservableCollection<string> combo1Collection;
        public ObservableCollection<string> Combo1Collection
        {
            get { return combo1Collection; }
            set
            {
                combo1Collection = value;
                OnPropertyChanged(nameof(Combo1Collection));
            }
        }
        ObservableCollection<string> combo2Collection;
        public ObservableCollection<string> Combo2Collection
        {
            get { return combo2Collection; }
            set
            {
                combo2Collection = value;
                OnPropertyChanged(nameof(Combo2Collection));
            }
        }
        ObservableCollection<string> combo3Collection;
        public ObservableCollection<string> Combo3Collection
        {
            get { return combo3Collection; }
            set
            {
                combo3Collection = value;
                OnPropertyChanged(nameof(Combo3Collection));
            }
        }
        ObservableCollection<VisitorDTO> searchVisitorCollection;
        public ObservableCollection<VisitorDTO> SearchVisitorCollection
        {
            get { return searchVisitorCollection; }
            set
            {
                searchVisitorCollection = value;
                OnPropertyChanged(nameof(SearchVisitorCollection));
            }
        }
        #endregion
        #region Fields for Desctop Operation
        int countRegistredVisitors;
        public int CountRegistredVisitors
        {
            get { return countRegistredVisitors; }
            set
            {
                countRegistredVisitors = value;
                OnPropertyChanged(nameof(CountRegistredVisitors));
            }
        }
        int countActualVisitors;
        public int CountActualVisitors
        {
            get { return countActualVisitors; }
            set
            {
                countActualVisitors = value;
                OnPropertyChanged(nameof(CountActualVisitors));
            }
        }
        int countCreateVisitors;
        public int CountCreateVisitors
        {
            get { return countCreateVisitors; }
            set
            {
                countCreateVisitors = value;
                OnPropertyChanged(nameof(CountCreateVisitors));
            }
        }
        int countNotcameVisitors;
        public int CountNotcameVisitors
        {
            get { return countNotcameVisitors; }
            set
            {
                countNotcameVisitors = value;
                OnPropertyChanged(nameof(CountNotcameVisitors));
            }
        }
        int countAllVisitors;
        public int CountAllVisitors
        {
            get { return countAllVisitors; }
            set
            {
                countAllVisitors = value;
                OnPropertyChanged(nameof(CountAllVisitors));
            }
        }
        string authDate;
        public string AuthDate
        {
            get { return authDate; }
            set
            {
                authDate = value;
                OnPropertyChanged(nameof(AuthDate));
            }
        }
        VisitorDTO selectDesctopVisitor;
        public VisitorDTO SelectDesctopVisitor
        {
            get { return selectDesctopVisitor; }
            set
            {
                selectDesctopVisitor = value;
                OnPropertyChanged(nameof(SelectDesctopVisitor));
            }
        }
        VisitorDTO editDesctopVisitor;
        public VisitorDTO EditDesctopVisitor
        {
            get { return editDesctopVisitor; }
            set
            {
                editDesctopVisitor = value;
                OnPropertyChanged(nameof(EditDesctopVisitor));
            }
        }
        VisitorDTO createDesctopVisitor;
        public VisitorDTO CreateDesctopVisitor
        {
            get { return createDesctopVisitor; }
            set
            {
                createDesctopVisitor = value;
                OnPropertyChanged(nameof(CreateDesctopVisitor));

            }
        }
        string statusForm;
        public string StatusForm
        {
            get { return statusForm; }
            set
            {
                statusForm = value;
                OnPropertyChanged(nameof(StatusForm));
            }
        }
        string statusFormColor;
        public string StatusFormColor
        {
            get { return statusFormColor; }
            set
            {
                statusFormColor = value;
                OnPropertyChanged(nameof(StatusFormColor));
            }
        }
        string currentTime;
        public string CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }
        string searchVisitor;
        public string SearchVisitor
        {
            get { return searchVisitor; }
            set
            {
                searchVisitor = value;
                CheckSearchVisitor(searchVisitor);
                OnPropertyChanged(nameof(SearchVisitor));
            }
        }
        VisitorDTO selectedSearchVisitor;
        public VisitorDTO SelectedSearchVisitor
        {
            get { return selectedSearchVisitor; }
            set
            {
                selectedSearchVisitor = value;
                OnPropertyChanged(nameof(SelectedSearchVisitor));
            }
        }
        string paymentStatusPresenter;
        public string PaymentStatusPresenter
        {
            get { return paymentStatusPresenter; }
            set
            {
                paymentStatusPresenter = value;
                OnPropertyChanged(nameof(PaymentStatusPresenter));
            }
        }
        int paymentStatusFontsize;
        public int PaymentStatusFontsize
        {
            get { return paymentStatusFontsize; }
            set
            {
                paymentStatusFontsize = value;
                OnPropertyChanged(nameof(PaymentStatusFontsize));
            }
        }
        string paymentStatusForegraund;
        public string PaymentStatusForegraund
        {
            get { return paymentStatusForegraund; }
            set
            {
                paymentStatusForegraund = value;
                OnPropertyChanged(nameof(PaymentStatusForegraund));
            }
        }
        #endregion
        #region Service Fields for Desctop Operation
        bool canExecuteCreateVisitor;
        public bool CanExecuteCreateVisitor
        {
            get { return canExecuteCreateVisitor; }
            set
            {
                canExecuteCreateVisitor = value;
                OnPropertyChanged(nameof(CanExecuteCreateVisitor));
            }
        }
        bool canExecuteEditVisitor;
        public bool CanExecuteEditVisitor
        {
            get { return canExecuteEditVisitor; }
            set
            {
                canExecuteEditVisitor = value;
                OnPropertyChanged(nameof(CanExecuteEditVisitor));
            }
        }
        bool canExecuteSaveEditVisitor;
        public bool CanExecuteSaveEditVisitor
        {
            get { return canExecuteSaveEditVisitor; }
            set
            {
                canExecuteSaveEditVisitor = value;
                OnPropertyChanged(nameof(CanExecuteSaveEditVisitor));
            }
        }
        bool isShowChanger;
        public bool IsShowChanger
        {
            get { return isShowChanger; }
            set
            {
                isShowChanger = value;
                OnPropertyChanged(nameof(IsShowChanger));
            }
        }
        bool find = false;
        bool Find
        {
            get { return find; }
            set
            {
                find = value;
                OnPropertyChanged(nameof(Find));
            }
        }
        PrintDialog printDialog;
        string bagePresenter;
        public string BagePresenter
        {
            get { return bagePresenter; }
            set
            {
                bagePresenter = value;
                OnPropertyChanged(nameof(BagePresenter));
            }
        }
        string bagePresenterBackGround;
        public string BagePresenterBackGround
        {
            get { return bagePresenterBackGround; }
            set
            {
                bagePresenterBackGround = value;
                OnPropertyChanged(nameof(BagePresenterBackGround));
            }
        }

        bool isListFocuce;
        public bool IsListFocuce
        {
            get { return isListFocuce; }
            set
            {
                isListFocuce = value;
                OnPropertyChanged(nameof(IsListFocuce));
            }
        }

        string searchVisitorForegraund;
        public string SearchVisitorForegraund
        {
            get { return searchVisitorForegraund; }
            set
            {
                searchVisitorForegraund = value;
                OnPropertyChanged(nameof(SearchVisitorForegraund));
            }
        }
        string searchVisitorBackGround;
        public string SearchVisitorBackGround
        {
            get { return searchVisitorBackGround; }
            set
            {
                searchVisitorBackGround = value;
                OnPropertyChanged(nameof(SearchVisitorBackGround));
            }
        }
        int searchVisitorFontsize;
        public int SearchVisitorFontsize
        {
            get { return searchVisitorFontsize; }
            set
            {
                searchVisitorFontsize = value;
                OnPropertyChanged(nameof(SearchVisitorFontsize));
            }
        }
        #endregion
        #region Commands for Desctop Operation
        RelayCommand createVisitor;
        public RelayCommand CreateVisitor
        {
            get { return createVisitor; }
        }
        RelayCommand editVisitor;
        public RelayCommand EditVisitor
        {
            get { return editVisitor; }
        }
        RelayCommand saveEditVisitor;
        public RelayCommand SaveEditVisitor
        {
            get { return saveEditVisitor; }
        }
        RelayCommand addVisitorToFact;
        public RelayCommand AddVisitorToFact
        {
            get { return addVisitorToFact; }
        }
        RelayCommand saveVisitorsToFile;
        public RelayCommand SaveVisitorsToFile
        {
            get { return saveVisitorsToFile; }
        }
        RelayCommand saveStatusesToFile;
        public RelayCommand SaveStatusesToFile
        {
            get { return saveStatusesToFile; }
        }
        #endregion
        #endregion
        #region Context Raport
        #region RaportCollections
        ObservableCollection<VisitorDTO> raportRegisteredVisitors;
        public ObservableCollection<VisitorDTO> RaportRegisteredVisitors
        {
            get { return raportRegisteredVisitors; }
            set
            {
                raportRegisteredVisitors = value;
                OnPropertyChanged(nameof(RaportRegisteredVisitors));
            }
        }
        ObservableCollection<VisitorDTO> raportActualVisitors;
        public ObservableCollection<VisitorDTO> RaportActualVisitors
        {
            get { return raportActualVisitors; }
            set
            {
                raportActualVisitors = value;
                OnPropertyChanged(nameof(RaportActualVisitors));
            }
        }
        ObservableCollection<VisitorDTO> raportUnActualVisitors;
        public ObservableCollection<VisitorDTO> RaportUnActualVisitors
        {
            get { return raportUnActualVisitors; }
            set
            {
                raportUnActualVisitors = value;
                OnPropertyChanged(nameof(RaportUnActualVisitors));
            }
        }
        ObservableCollection<VisitorDTO> raportCreateVisitors;
        public ObservableCollection<VisitorDTO> RaportCreateVisitors
        {
            get { return raportCreateVisitors; }
            set
            {
                raportCreateVisitors = value;
                OnPropertyChanged(nameof(RaportCreateVisitors));
            }
        }
        ObservableCollection<VisitorDTO> raportAllVisitors;
        public ObservableCollection<VisitorDTO> RaportAllVisitors
        {
            get { return raportAllVisitors; }
            set
            {
                raportAllVisitors = value;
                OnPropertyChanged(nameof(RaportAllVisitors));
            }
        }
        #endregion
        #region RaportFields
        string selectedRaportRegistredCategory;
        public string SelectedRaportRegistredCategory
        {
            get { return selectedRaportRegistredCategory; }
            set
            {
                selectedRaportRegistredCategory = value;
                OnPropertyChanged(nameof(SelectedRaportRegistredCategory));
            }
        }
        string selectedRaportActualCategory;
        public string SelectedRaportActualCategory
        {
            get { return selectedRaportActualCategory; }
            set
            {
                selectedRaportActualCategory = value;
                OnPropertyChanged(nameof(SelectedRaportActualCategory));
            }
        }
        string selectedRaportUnActualCategory;
        public string SelectedRaportUnActualCategory
        {
            get { return selectedRaportUnActualCategory; }
            set
            {
                selectedRaportUnActualCategory = value;
                OnPropertyChanged(nameof(SelectedRaportUnActualCategory));

            }
        }
        string selectedRaportCreateCategory;
        public string SelectedRaportCreateCategory
        {
            get { return selectedRaportCreateCategory; }
            set
            {
                selectedRaportCreateCategory = value;
                OnPropertyChanged(nameof(SelectedRaportCreateCategory));

            }
        }
        string selectedRaportAllCategory;
        public string SelectedRaportAllCategory
        {
            get { return selectedRaportAllCategory; }
            set
            {
                selectedRaportAllCategory = value;
                OnPropertyChanged(nameof(SelectedRaportAllCategory));
            }
        }

        string selectedRaportRegistredPaymentStatus;
        public string SelectedRaportRegistredPaymentStatus
        {
            get { return selectedRaportRegistredPaymentStatus; }
            set
            {
                selectedRaportRegistredPaymentStatus = value;
                OnPropertyChanged(nameof(SelectedRaportRegistredPaymentStatus));
            }
        }
        string selectedRaportActualPaymentStatus;
        public string SelectedRaportActualPaymentStatus
        {
            get { return selectedRaportActualPaymentStatus; }
            set
            {
                selectedRaportActualPaymentStatus = value;
                OnPropertyChanged(nameof(SelectedRaportActualPaymentStatus));
            }
        }
        string selectedRaportUnActualPaymentStatus;
        public string SelectedRaportUnActualPaymentStatus
        {
            get { return selectedRaportUnActualPaymentStatus; }
            set
            {
                selectedRaportUnActualPaymentStatus = value;
                OnPropertyChanged(nameof(SelectedRaportUnActualPaymentStatus));

            }
        }
        string selectedRaportCreatePaymentStatus;
        public string SelectedRaportCreatePaymentStatus
        {
            get { return selectedRaportCreatePaymentStatus; }
            set
            {
                selectedRaportCreatePaymentStatus = value;
                OnPropertyChanged(nameof(SelectedRaportCreatePaymentStatus));
            }
        }
        string selectedRaportAllPaymentStatus;
        public string SelectedRaportAllPaymentStatus
        {
            get { return selectedRaportAllPaymentStatus; }
            set
            {
                selectedRaportAllPaymentStatus = value;
                OnPropertyChanged(nameof(SelectedRaportAllPaymentStatus));
            }
        }

        string selectedRaportRegistredChanged;
        public string SelectedRaportRegistredChanged
        {
            get { return selectedRaportRegistredChanged; }
            set
            {
                selectedRaportRegistredChanged = value;
                OnPropertyChanged(nameof(SelectedRaportRegistredChanged));
            }
        }
        string selectedRaportActualChanged;
        public string SelectedRaportActualChanged
        {
            get { return selectedRaportActualChanged; }
            set
            {
                selectedRaportActualChanged = value;
                OnPropertyChanged(nameof(SelectedRaportActualChanged));
            }
        }
        string selectedRaportUnActualChanged;
        public string SelectedRaportUnActualChanged
        {
            get { return selectedRaportUnActualChanged; }
            set
            {
                selectedRaportUnActualChanged = value;
                OnPropertyChanged(nameof(SelectedRaportUnActualChanged));
            }
        }
        string selectedRaportCreateChanged;
        public string SelectedRaportCreateChanged
        {
            get { return selectedRaportCreateChanged; }
            set
            {
                selectedRaportCreateChanged = value;
                OnPropertyChanged(nameof(SelectedRaportCreateChanged));
            }
        }
        string selectedRaportAllChanged;
        public string SelectedRaportAllChanged
        {
            get { return selectedRaportAllChanged; }
            set
            {
                selectedRaportAllChanged = value;
                OnPropertyChanged(nameof(SelectedRaportAllChanged));
            }
        }

        int countRaportRegistredVisitors;
        public int CountRaportRegistredVisitors
        {
            get { return countRaportRegistredVisitors; }
            set
            {
                countRaportRegistredVisitors = value;
                OnPropertyChanged(nameof(CountRaportRegistredVisitors));
            }
        }
        int countRaportActualVisitors;
        public int CountRaportActualVisitors
        {
            get { return countRaportActualVisitors; }
            set
            {
                countRaportActualVisitors = value;
                OnPropertyChanged(nameof(CountRaportActualVisitors));
            }
        }
        int countRaportUnActualVisitors;
        public int CountRaportUnActualVisitors
        {
            get { return countRaportUnActualVisitors; }
            set
            {
                countRaportUnActualVisitors = value;
                OnPropertyChanged(nameof(CountRaportUnActualVisitors));
            }
        }
        int countRaportCreateVisitors;
        public int CountRaportCreateVisitors
        {
            get { return countRaportCreateVisitors; }
            set
            {
                countRaportCreateVisitors = value;
                OnPropertyChanged(nameof(CountRaportCreateVisitors));
            }
        }
        int countRaportAllVisitors;
        public int CountRaportAllVisitors
        {
            get { return countRaportAllVisitors; }
            set
            {
                countRaportAllVisitors = value;
                OnPropertyChanged(nameof(CountRaportAllVisitors));
            }
        }
        #endregion
        #region RaportCommand
        RelayCommand printRegistredVisitors;
        public RelayCommand PrintRegistredVisitors
        {
            get { return printRegistredVisitors;}
        }
        RelayCommand exportToFileRegistredVisitors;
        public RelayCommand ExportToFileRegistredVisitors
        {
            get { return exportToFileRegistredVisitors; }
        }
        RelayCommand changeCategoryRegistredVisitors;
        public RelayCommand ChangeCategoryRegistredVisitors
        {
            get { return changeCategoryRegistredVisitors; }
        }
        RelayCommand changePaymentStatusRegistredVisitors;
        public RelayCommand ChangePaymentStatusRegistredVisitors
        {
            get { return changePaymentStatusRegistredVisitors; }
        }
        RelayCommand changeChangedRegistredVisitors;
        public RelayCommand ChangeChangedRegistredVisitors
        {
            get { return changeChangedRegistredVisitors; }
        }

        RelayCommand printActualVisitors;
        public RelayCommand PrintActualVisitors
        {
            get { return printActualVisitors; }
        }
        RelayCommand exportToFileActualVisitors;
        public RelayCommand ExportToFileActualVisitors
        {
            get { return exportToFileActualVisitors; }
        }
        RelayCommand changeCategoryActualVisitors;
        public RelayCommand ChangeCategoryActualVisitors
        {
            get { return changeCategoryActualVisitors; }
        }
        RelayCommand changePaymentStatusActualVisitors;
        public RelayCommand ChangePaymentStatusActualVisitors
        {
            get { return changePaymentStatusActualVisitors; }
        }
        RelayCommand changeChangedActualVisitors;
        public RelayCommand ChangeChangedActualVisitors
        {
            get { return changeChangedActualVisitors; }
        }

        RelayCommand printUnActualVisitors;
        public RelayCommand PrintUnActualVisitors
        {
            get { return printUnActualVisitors; }
        }
        RelayCommand exportToFileUnActualVisitors;
        public RelayCommand ExportToFileUnActualVisitors
        {
            get { return exportToFileUnActualVisitors; }
        }
        RelayCommand changeCategoryUnActualVisitors;
        public RelayCommand ChangeCategoryUnActualVisitors
        {
            get { return changeCategoryUnActualVisitors; }
        }
        RelayCommand changePaymentStatusUnActualVisitors;
        public RelayCommand ChangePaymentStatusUnActualVisitors
        {
            get { return changePaymentStatusUnActualVisitors; }
        }
        RelayCommand changeChangedUnActualVisitors;
        public RelayCommand ChangeChangedUnActualVisitors
        {
            get { return changeChangedUnActualVisitors; }
        }

        RelayCommand printCreateVisitors;
        public RelayCommand PrintCreateVisitors
        {
            get { return printCreateVisitors; }
        }
        RelayCommand exportToFileCreateVisitors;
        public RelayCommand ExportToFileCreateVisitors
        {
            get { return exportToFileCreateVisitors; }
        }
        RelayCommand changeCategoryCreateVisitors;
        public RelayCommand ChangeCategoryCreateVisitors
        {
            get { return changeCategoryCreateVisitors; }
        }
        RelayCommand changePaymentStatusCreateVisitors;
        public RelayCommand ChangePaymentStatusCreateVisitors
        {
            get { return changePaymentStatusCreateVisitors; }
        }
        RelayCommand changeChangedCreateVisitors;
        public RelayCommand ChangeChangedCreateVisitors
        {
            get { return changeChangedCreateVisitors; }
        }

        RelayCommand printAllVisitors;
        public RelayCommand PrintAllVisitors
        {
            get { return printAllVisitors; }
        }
        RelayCommand exportToFileAllVisitors;
        public RelayCommand ExportToFileAllVisitors
        {
            get { return exportToFileAllVisitors; }
        }
        RelayCommand changeCategoryAllVisitors;
        public RelayCommand ChangeCategoryAllVisitors
        {
            get { return changeCategoryAllVisitors; }
        }
        RelayCommand changePaymentStatusAllVisitors;
        public RelayCommand ChangePaymentStatusAllVisitors
        {
            get { return changePaymentStatusAllVisitors; }
        }
        RelayCommand changeChangedAllVisitors;
        public RelayCommand ChangeChangedAllVisitors
        {
            get { return changeChangedAllVisitors; }
        }
        #endregion
        #endregion

        public MainVM()
        {
            #region Init value for Desctop Operation (first part)
            isListFocuce = false;
            DataMode = "Локальная база данных";
            visitorRepositoryDTO = new VisitorRepositoryDTO();
            printDialog = new PrintDialog();
            Visitors = new ObservableCollection<VisitorDTO>(visitorRepositoryDTO.GetAllVisitors());
            UpdateAllVisitorFields(visitors);
            StartShowTime();
            statusFormColor = "Red";
            statusForm = "";
            #endregion
            #region Init value for Administration
            userRepository = new UserRepositoryDTO();
            roleRepository = new RoleRepositoryDTO();
            userInRoleRepository = new UserInRoleRepositoryDTO();
            tabRepository = new TabRepositoryDTO();
            commandRepository = new CommandRepositoryDTO();

            StatusRegistration = "Пройдите регистрацию";
            regisrationUser = new UserDTO();

            var checkUser = userRepository.GetUserDTOs().FirstOrDefault();
            if (checkUser == null)
            {
                var newUser = new UserDTO
                {
                    FirstName = "admin",
                    LastName = "admin",
                    Login = "admin",
                    Password = "admin".GetHashCode().ToString(),
                    IsSelected = true,
                    IsDefault = true
                };
                checkUser = userRepository.AddOrUpdate(newUser);
            }
            AuthorizedUser = checkUser;

            addedRole = new RoleDTO { Name = "NewRole1", IsDefault = false, IsSelected = false };
            var checkRole = roleRepository.GetAllRoles().FirstOrDefault();
            if (checkRole == null)
            {
                checkRole = roleRepository.AddOrUpdate(new RoleDTO { Name = "admin", IsSelected = true, IsDefault = true });
            }
            else
            {
                CheckAndCorrectAddedRoleIfNeed(addedRole);
            }

            var checkUserInRole = userInRoleRepository.GetAllUserInRolesDTOs().FirstOrDefault();
            if (checkUserInRole == null)
            {
                UserInRoleDTO userInRoleDTO = new UserInRoleDTO
                {
                    UserId = checkUser.Id,
                    RoleId = checkRole.Id
                };
                userInRoleRepository.AddOrUpdate(userInRoleDTO);
            }

            Users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
            Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
            SelectedUser = users.Where(u => u.IsSelected == true).FirstOrDefault();
            SelectedRole = roles.Where(r => r.IsSelected == true).FirstOrDefault();
            DefaultRole = roles.Where(r => r.IsDefault == true).FirstOrDefault();
            DefaultUser = userRepository.GetUserDTOs().Where(u => u.IsDefault == true).FirstOrDefault();
            Commands = new ObservableCollection<CommandDTO>(commandRepository.GetAllCommands().Where(c => c.RoleId == selectedRole.Id));
            Tabs = new ObservableCollection<TabDTO>(tabRepository.GetTabDTOs().Where(t => t.RoleId == selectedRole.Id));

            SetComandAndTabSettings(defaultUser);

            StatusAuthorisation = "Вы авторизированы как - " +
                defaultUser.Login + "(" + defaultUser.FirstName + " " + defaultUser.LastName + ")";
            //            AuthorizedUser = new UserDTO();
            #endregion
            #region Init value for Settings
            #region AllSettingCollection
            displaySettingDTORepository = new DisplaySettingDTORepository();
            dSCollumnSettingDTORepository = new DSCollumnSettingDTORepository();
            printSettingRepositoryDTO = new PrintSettingRepositoryDTO();
            printStringSettingRepositoryDTO = new PrintStringSettingRepositoryDTO();
            #endregion
            #region Raport
            columnCheckedRaport = new bool[count_raport_headers];
            aliasRaport = new string[count_raport_headers];
            widthRaport = new int[count_raport_headers];


            displaySettingsRaport = new ObservableCollection<DisplaySettingDTO>
                (displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Intendant == "raport"));
            DisplaySettingDTO defaultDisplayRaportSetting;
            if (displaySettingsRaport.Count() == 0)
            {
                defaultDisplayRaportSetting = new DisplaySettingDTO
                {
                    Name = "default",
                    IsSelected = true,
                    Intendant = "raport"
                };
            }
            else defaultDisplayRaportSetting = displaySettingDTORepository.
                  GetAllDisplaySettingDTOs().
                  Where(s => s.IsSelected == true && s.Intendant == "raport").
                  FirstOrDefault();
            var defaultDisplayRaportSettingId = displaySettingDTORepository.
                AddOrUpdate(defaultDisplayRaportSetting).Id;

            dSCollumnSettingsRaport = new ObservableCollection<DSCollumnSettingDTO>
                (dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                Where(s => s.Intendant == "raport"));

            if (dSCollumnSettingsRaport.Count() == 0)
            {
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Id",
                    Alias = "№",
                    Width = 100,
                    Visible = true,
                    IsSelected = true,
                    Intendant = "raport",
                    DisplaySettingId = defaultDisplayRaportSettingId
                });

                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "FirstName",
                    Alias = "Имя",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "raport",
                    DisplaySettingId = defaultDisplayRaportSettingId
                });

                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "LastName",
                    Alias = "Фамилия",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "raport",
                    DisplaySettingId = defaultDisplayRaportSettingId
                });

                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Сompany",
                    Alias = "Компания",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "raport",
                    DisplaySettingId = defaultDisplayRaportSettingId
                });

                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Jobtitle",
                    Alias = "Должность",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "raport",
                    DisplaySettingId = defaultDisplayRaportSettingId
                });
            }
            updateAllSettingsRaport("raport");
            #endregion
            #region Desctop
            columnCheckedDesctop = new bool[count_headers];
            aliasDesctop = new string[count_headers];
            widthDesctop = new int[count_headers];

            displaySettingsDesctop = new ObservableCollection<DisplaySettingDTO>
                (displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Intendant == "desctop"));
            DisplaySettingDTO defaultDisplayDesctopSetting;
            if (displaySettingsDesctop.Count() == 0)
            {
                defaultDisplayDesctopSetting = new DisplaySettingDTO
                {
                    Name = "default",
                    IsSelected = true,
                    Intendant = "desctop"
                };
            }
            else defaultDisplayDesctopSetting = displaySettingDTORepository.
                  GetAllDisplaySettingDTOs().
                  Where(s => s.IsSelected == true && s.Intendant == "desctop").
                  FirstOrDefault();
            var defaultDisplayDesctopSettingId = displaySettingDTORepository.
                AddOrUpdate(defaultDisplayDesctopSetting).Id;
            dSCollumnSettingsDesctop = new ObservableCollection<DSCollumnSettingDTO>
                (dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                Where(s => s.Intendant == "desctop"));
            if (dSCollumnSettingsDesctop.Count() == 0)
            {
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Id",
                    Alias = "№",
                    Width = 0,
                    Visible = false,
                    IsSelected = true,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Magik ID",
                    Alias = "Magik ID",
                    Width = 80,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Payment Statuse",
                    Alias = "Payment Status",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Пусто",
                    Alias = "Пусто",
                    Width = 0,
                    Visible = false,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Категория",
                    Alias = "Категория",
                    Width = 80,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Пусто",
                    Alias = "Пусто",
                    Width = 0,
                    Visible = false,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Замена",
                    Alias = "Замена",
                    Width = 70,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Коментарий",
                    Alias = "Коментарий",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Name",
                    Alias = "Имя",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "SurName",
                    Alias = "Фамилия ",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Position",
                    Alias = "Должность",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Company",
                    Alias = "Компания",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Дата регистрации",
                    Alias = "Дата регистрации",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Резерв",
                    Alias = "Резерв",
                    Width = 0,
                    Visible = false,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Резерв",
                    Alias = "Резерв",
                    Width = 0,
                    Visible = false,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
            }
            updateAllSettingsDesctop("desctop");
            #endregion
            #region Form           
            rowCheckedForm = new bool[count_form_fields];
            aliasForm = new string[count_form_fields];
            heightForm = new int[count_form_fields];

            displaySettingsForm = new ObservableCollection<DisplaySettingDTO>
                (displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Intendant == "form"));
            DisplaySettingDTO defaultDisplayFormSetting;
            if (displaySettingsForm.Count() == 0)
            {
                defaultDisplayFormSetting = new DisplaySettingDTO
                {
                    Name = "default",
                    IsSelected = true,
                    Intendant = "form"
                };
            }
            else defaultDisplayFormSetting = displaySettingDTORepository.
                  GetAllDisplaySettingDTOs().
                  Where(s => s.IsSelected == true && s.Intendant == "form").
                  FirstOrDefault();
            var defaultDisplayFormSettingId = displaySettingDTORepository.
                AddOrUpdate(defaultDisplayFormSetting).Id;
            dSCollumnSettingsForm = new ObservableCollection<DSCollumnSettingDTO>
                (dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs()
                .Where(s => s.DisplaySettingId == defaultDisplayFormSettingId)
                /*.Where(s => s.Intendant == "form")*/);
            if (dSCollumnSettingsForm.Count() == 0)
            {
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Id",
                    Alias = "№",
                    Width = 30,
                    Visible = false,
                    IsSelected = true,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Magik ID",
                    Alias = "Magik ID",
                    Width = 30,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Payment Status",
                    Alias = "Статус оплаты",
                    Width = 30,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Пусто",
                    Alias = "Пусто",
                    Width = 0,
                    Visible = false,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Категория",
                    Alias = "Категория",
                    Width = 30,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Пусто",
                    Alias = "Пусто",
                    Width = 0,
                    Visible = false,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Замена",
                    Alias = "Замена",
                    Width = 30,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Коментарий Замены",
                    Alias = "Коментарий",
                    Width = 0,
                    Visible = false,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Name",
                    Alias = "Имя",
                    Width = 30,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "SurName",
                    Alias = "Фамилия",
                    Width = 30,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Position",
                    Alias = "Должность",
                    Width = 30,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Company",
                    Alias = "Компания",
                    Width = 30,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Резерв",
                    Alias = "Резерв",
                    Width = 0,
                    Visible = false,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Резерв",
                    Alias = "Резерв",
                    Width = 0,
                    Visible = false,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Резерв",
                    Alias = "Резерв",
                    Width = 0,
                    Visible = false,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
            }
            updateAllSettingsForm("form");
            #endregion
            #region PrintBage
            printSettingDTOs = new ObservableCollection<PrintSettingDTO>
                (printSettingRepositoryDTO.GetAllPrintSettingDTOs());
            PrintSettingDTO defaultPrintSetting;
            if (printSettingDTOs.Count() == 0)
            {
                defaultPrintSetting = new PrintSettingDTO
                {
                    Name = "default",
                    IsSelected = true
                };
            }
            else defaultPrintSetting = printSettingRepositoryDTO.
                GetAllPrintSettingDTOs().
                FirstOrDefault();
            var defaultPrintSettingId = printSettingRepositoryDTO.
                AddOrUpdate(defaultPrintSetting).Id;
            printStringSettingDTOs = new ObservableCollection<PrintStringSettingDTO>
                (printStringSettingRepositoryDTO.GetAllPrintStringSettingDTOs()
                .Where(s => s.PrintSettingId == defaultPrintSettingId));
            if(printStringSettingDTOs.Count() == 0)
            {
                printStringSettingRepositoryDTO.AddOrUpdate(new PrintStringSettingDTO
                {
                    Name = "Column8",
                    FontName = "Verdana",
                    FontSize = 18,
                    FontWeight = 400,
                    ToUpper = true,
                    Visible = true,
                    IsSelected = true,
                    PrintSettingId = defaultPrintSettingId
                });
                printStringSettingRepositoryDTO.AddOrUpdate(new PrintStringSettingDTO
                {
                    Name = "Column9",
                    FontName = "Verdana",
                    FontSize = 18,
                    FontWeight = 400,
                    ToUpper = true,
                    Visible = true,
                    IsSelected = false,
                    PrintSettingId = defaultPrintSettingId
                });
                printStringSettingRepositoryDTO.AddOrUpdate(new PrintStringSettingDTO
                {
                    Name = "Column11",
                    FontName = "Verdana",
                    FontSize = 18,
                    FontWeight = 900,
                    ToUpper = true,
                    Visible = true,
                    IsSelected = false,
                    PrintSettingId = defaultPrintSettingId
                });
            }
            updateAllPrintSettings();
            #endregion
            #endregion
            #region Init value for File
            visitorRepositoryDTO = new VisitorRepositoryDTO();
            visitorRepositoryDTO.progressChanged += ProgressChanged;
            statusRepository = new StatusRepositoryDTO();
            #endregion
            #region Init value for Service
            IsEnabledDataModeChecker = true;
            serviceExecutor = new ServiceExecutor(/*"192.168.0.27"*/);
            serviceExecutor.statusChanged += StatusChanged;
            #endregion
            #region Init value for Client
            clientExecutor = new ClientExecutor();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VisitorDTO, EX.Client.ServiceReference1.VisitorDTO>();
                cfg.CreateMap<EX.Client.ServiceReference1.VisitorDTO, VisitorDTO>();
                cfg.CreateMap<StatusDTO, EX.Client.ServiceReference1.StatusDTO>();
                cfg.CreateMap<EX.Client.ServiceReference1.StatusDTO, StatusDTO>();
            });
            mapper = config.CreateMapper();
            errorConnectServer = "";
            #endregion
            #region Init value for Desctop Operation (second part)
            selectDesctopVisitor = new VisitorDTO();
            editDesctopVisitor = new VisitorDTO();
            canExecuteCreateVisitor = true;
            canExecuteEditVisitor = true;
            canExecuteSaveEditVisitor = false;

            combo1Collection = new ObservableCollection<string>(new List<string>
            {
                "PAID", "UNPAID", "FOC", "CXLD"
            });
            combo2Collection = new ObservableCollection<string>(new List<string>
            {
                "3 DAY PACKAGE", "2 DAY PACKAGE", "FOCUS DAY", "MEDIA",
                "SPEAKER", "PRESS PASS", "SPEX GUEST", "GUEST", "ORGANIZER"
            });
            combo3Collection = new ObservableCollection<string>(new List<string>
            {
                "Замена"
            });
            IsShowChanger = false;

            bagePresenter = "Цвет бейджа";
            bagePresenterBackGround = "#FFE5E5E5";
            paymentStatusPresenter = "Статус Оплаты";
            paymentStatusFontsize = 12;
            paymentStatusForegraund = "Gray";
            searchVisitor = "Введите информацию для поиска";
            searchVisitorFontsize = 25;
            searchVisitorBackGround = "White";
            searchVisitorForegraund = "Gray";
            searchVisitorCollection = new ObservableCollection<VisitorDTO>();
            #endregion
            #region Init value for Raports
            if (dataMode != "Клиент службы баз данных")
            {
                raportRegisteredVisitors = new ObservableCollection<VisitorDTO>
                (visitorRepositoryDTO.GetAllVisitors()
                   .Where(s => s.CurrentStatus == "registered" ||
                   s.CurrentStatus == "actual"));
                raportActualVisitors = new ObservableCollection<VisitorDTO>
                    (visitorRepositoryDTO.GetAllVisitors()
                       .Where(s => s.CurrentStatus == "actual"));
                raportUnActualVisitors = new ObservableCollection<VisitorDTO>
                    (visitorRepositoryDTO.GetAllVisitors()
                       .Where(s => s.CurrentStatus == "registered"));
                raportCreateVisitors = new ObservableCollection<VisitorDTO>
                    (visitorRepositoryDTO.GetAllVisitors()
                       .Where(s => s.CurrentStatus == "create"));
                raportAllVisitors = new ObservableCollection<VisitorDTO>
                    (visitorRepositoryDTO.GetAllVisitors()
                       .Where(s => s.CurrentStatus == "registered" ||
                       s.CurrentStatus == "actual" || s.CurrentStatus == "create"));
            }
            else
            {
                var client = clientExecutor.GetClient();
                var _client_visitors = clientExecutor.GetClient().GetAllVisitors();
                var _visitors = _client_visitors.Select(s => mapper.Map<VisitorDTO>(s));

                raportRegisteredVisitors = new ObservableCollection<VisitorDTO>
                      (_visitors
                      .Where(s => s.CurrentStatus == "registered" ||
                         s.CurrentStatus == "actual"));
                raportActualVisitors = new ObservableCollection<VisitorDTO>
                     (_visitors
                       .Where(s => s.CurrentStatus == "actual"));
                raportUnActualVisitors = new ObservableCollection<VisitorDTO>
                    (_visitors
                       .Where(s => s.CurrentStatus == "registered"));
                raportCreateVisitors = new ObservableCollection<VisitorDTO>
                    (_visitors
                       .Where(s => s.CurrentStatus == "create"));
                raportAllVisitors = new ObservableCollection<VisitorDTO>
                    (_visitors
                       .Where(s => s.CurrentStatus == "registered" ||
                       s.CurrentStatus == "actual" || s.CurrentStatus == "create"));
            }
            countRaportRegistredVisitors = raportRegisteredVisitors.Count();
            countRaportActualVisitors = raportActualVisitors.Count();
            countRaportUnActualVisitors = raportUnActualVisitors.Count();
            countRaportCreateVisitors = raportCreateVisitors.Count();
            countRaportAllVisitors = raportAllVisitors.Count();
            #endregion
            #region Implementation cammands for Administration
            addUser = new RelayCommand(c =>
            {
                System.Windows.Controls.PasswordBox p = (System.Windows.Controls.PasswordBox)c;
                var isLoginExist = userRepository.GetUserDTOs().Where(u => u.Login == regisrationUser.Login).Count() != 0;
                if (isLoginExist) StatusRegistration = "Пользователь с логином " + regisrationUser.Login + " уже зарегистрирован";
                else
                {
                    regisrationUser.Password = p.Password.GetHashCode().ToString();
                    regisrationUser.IsSelected = true;
                    var _selectedUser = userRepository.GetUserDTOs().Where(u => u.IsSelected == true).FirstOrDefault();
                    _selectedUser.IsSelected = false;
                    userRepository.AddOrUpdate(_selectedUser);
                    userRepository.AddOrUpdate(regisrationUser);
                    Users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
                    SelectedUser = Users.Where(u => u.IsSelected == true).FirstOrDefault();
                    userInRoleRepository.AddOrUpdate(new UserInRoleDTO
                    {
                        UserId = selectedUser.Id,
                        RoleId = defaultRole.Id
                    });
                    StatusRegistration = "Вы успешно прошли регистрацию\n" +
                    "Ваши регистрационные данные:\n" +
                    "Имя - " + SelectedUser.FirstName + "\n" +
                    "Фамилия - " + SelectedUser.LastName + "\n" +
                    "Логин - " + SelectedUser.Login + "\n" +
                    "Идентификатор - " + SelectedUser.Id + "\n";
                    RegistrationUser.FirstName = "";
                    RegistrationUser.LastName = "";
                    RegistrationUser.Login = "";
                    p.Clear();
                    var oldSelectedRole = roleRepository.GetAllRoles().Where(r => r.IsSelected == true).FirstOrDefault();
                    oldSelectedRole.IsSelected = false;
                    roleRepository.AddOrUpdate(oldSelectedRole);
                    var newSelectedRole = roleRepository.GetAllRoles().Where(r => r.IsDefault == true).FirstOrDefault();
                    newSelectedRole.IsSelected = true;
                    roleRepository.AddOrUpdate(newSelectedRole);
                    Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                    SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
                }
            });
            delUser = new RelayCommand(c =>
            {
                var delUserInRole = userInRoleRepository.GetAllUserInRolesDTOs().
                      Where(ur => ur.UserId == selectedUser.Id).FirstOrDefault();
                userInRoleRepository.RemoveUserInRoleDTO(delUserInRole);
                userRepository.RemoveUserDTO(selectedUser);
                if (selectedUser.IsSelected == true)
                {
                    var newSelectedUser = userRepository.GetUserDTOs().FirstOrDefault();
                    newSelectedUser.IsSelected = true;
                    var nsuid = userRepository.AddOrUpdate(newSelectedUser).Id;
                    users.Where(u => u.Id == nsuid).FirstOrDefault().IsSelected = true;
                }
                Users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
                SelectedUser = Users.Where(u => u.IsSelected == true).FirstOrDefault();
                var oldSelectedRole = roleRepository.GetAllRoles().
                 Where(r => r.IsSelected == true).FirstOrDefault();
                oldSelectedRole.IsSelected = false;
                roleRepository.AddOrUpdate(oldSelectedRole);
                var newSelectedRoleId = userInRoleRepository.GetAllUserInRolesDTOs().Where
                    (ur => ur.UserId == selectedUser.Id).Select(r => r.Id).FirstOrDefault();
                var newSelectedRole = roleRepository.GetAllRoles().Where
                       (r => r.Id == newSelectedRoleId).FirstOrDefault();
                newSelectedRole.IsSelected = true;
                roleRepository.AddOrUpdate(newSelectedRole);
                Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
            });
            setDefaultUser = new RelayCommand(c =>
            {
                var oldDefaultUser = userRepository.GetUserDTOs().Where(u => u.IsDefault == true).FirstOrDefault();
                if (selectedUser.Id != oldDefaultUser.Id)
                {
                    oldDefaultUser.IsDefault = false;
                    var newDefaultUser = userRepository.GetUserDTOs().Where(u => u.Id == selectedUser.Id).FirstOrDefault();
                    newDefaultUser.IsDefault = true;
                    userRepository.AddOrUpdate(oldDefaultUser);
                    userRepository.AddOrUpdate(newDefaultUser);

                    DefaultUser = userRepository.GetUserDTOs().Where(u => u.IsDefault == true).FirstOrDefault();
                }
            });
            addRole = new RelayCommand(c =>
            {
                var isRoleExist = roleRepository.GetAllRoles().Where(r => r.Name == addedRole.Name).Count() > 0;
                if (!isRoleExist)
                {
                    roleRepository.AddOrUpdate(addedRole);
                    Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                    CheckAndCorrectAddedRoleIfNeed(addedRole);
                }
                else { CheckAndCorrectAddedRoleIfNeed(addedRole); }
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
            });
            delRole = new RelayCommand(c =>
            {
                var isUseSelectedRole = userInRoleRepository.GetAllUserInRolesDTOs().Where(ur => ur.RoleId == SelectedRole.Id).Count() > 0;
                if (isUseSelectedRole)
                {
                    MessageBox.Show("Данная роль не может быть удалена, так как предназначена как мимнимум одному пользователю");
                }
                else if (selectedRole.IsDefault == true)
                {
                    MessageBox.Show("Данная роль не может быть удалена так как определена как роль по умолчанию");
                }
                else
                {
                    roleRepository.RemoveRoleDTO(selectedRole);
                    tabRepository.RomoveCurrentTabRepository(selectedRole.Id);
                    commandRepository.RemoveCommandRepository(selectedRole.Id);
                }
                Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
            });
            setDefaultRole = new RelayCommand(c =>
            {
                var oldDefaultRole = roleRepository.GetAllRoles().Where(r => r.IsDefault == true).FirstOrDefault();
                oldDefaultRole.IsDefault = false;
                roleRepository.AddOrUpdate(oldDefaultRole);
                var newDefaultRole = roleRepository.GetAllRoles().Where(r => r.Id == selectedRole.Id).FirstOrDefault();
                newDefaultRole.IsDefault = true;
                roleRepository.AddOrUpdate(newDefaultRole);
                Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                DefaultRole = Roles.Where(r => r.IsDefault == true).FirstOrDefault();
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
            });
            setUserRole = new RelayCommand(c =>
            {
                var oldSelectedUser = userRepository.GetUserDTOs().Where(u => u.IsSelected == true).FirstOrDefault();
                if (SelectedUser.Id != oldSelectedUser.Id)
                {
                    oldSelectedUser.IsSelected = false;
                    var newSelectedUser = userRepository.GetUserDTOs().Where(u => u.Id == SelectedUser.Id).FirstOrDefault();
                    newSelectedUser.IsSelected = true;
                    userRepository.AddOrUpdate(oldSelectedUser);
                    userRepository.AddOrUpdate(newSelectedUser);
                }
                var oldSelectedRole = roleRepository.GetAllRoles().Where(r => r.IsSelected == true).FirstOrDefault();
                if (SelectedRole.Id != oldSelectedRole.Id)
                {
                    oldSelectedRole.IsSelected = false;
                    var newSelectedRole = roleRepository.GetAllRoles().Where(r => r.Id == SelectedRole.Id).FirstOrDefault();
                    newSelectedRole.IsSelected = true;
                    roleRepository.AddOrUpdate(oldSelectedRole);
                    roleRepository.AddOrUpdate(newSelectedRole);
                }
                var correctedUserInRole = userInRoleRepository.GetAllUserInRolesDTOs().Where(ur => ur.UserId == SelectedUser.Id).FirstOrDefault();
                correctedUserInRole.RoleId = SelectedRole.Id;
                userInRoleRepository.AddOrUpdate(correctedUserInRole);
                users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
                Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
                selectedUser = Users.Where(u => u.IsSelected == true).FirstOrDefault();
                Commands = new ObservableCollection<CommandDTO>(commandRepository.GetAllCommands().Where(co => co.RoleId == selectedRole.Id));
                Tabs = new ObservableCollection<TabDTO>(tabRepository.GetTabDTOs().Where(t => t.RoleId == selectedRole.Id));
            });
            roleChanged = new RelayCommand(c =>
            {
                int selectedRoleId;
                if (SelectedRole != null) selectedRoleId = SelectedRole.Id;
                else selectedRoleId = userInRoleRepository.GetAllUserInRolesDTOs().Where(ur => ur.UserId == selectedUser.Id).Select(s => s.RoleId).FirstOrDefault();
                Commands = new ObservableCollection<CommandDTO>(commandRepository.GetAllCommands().Where(co => co.RoleId == selectedRoleId));
                Tabs = new ObservableCollection<TabDTO>(tabRepository.GetTabDTOs().Where(t => t.RoleId == selectedRoleId));
            });
            saveChanges = new RelayCommand(с =>
            {
                commandRepository.UpdateCommandRepository(commands);
                tabRepository.UpdateTabRepository(tabs);
            });
            userChanged = new RelayCommand(c =>
            {
                if (selectedUser != null)
                {
                    var isSelectuserHaveRole = userInRoleRepository.GetAllUserInRolesDTOs().Select(s => s.UserId).Contains(selectedUser.Id);
                    if (isSelectuserHaveRole)
                    {
                        var oldSelectedUser = userRepository.GetUserDTOs().Where(u => u.IsSelected == true).FirstOrDefault();
                        oldSelectedUser.IsSelected = false;
                        var newSelectedUser = userRepository.GetUserDTOs().Where(u => u.Id == selectedUser.Id).FirstOrDefault();
                        newSelectedUser.IsSelected = true;
                        userRepository.AddOrUpdate(oldSelectedUser);
                        userRepository.AddOrUpdate(newSelectedUser);


                        var newRoleId = userInRoleRepository.GetAllUserInRolesDTOs().Where(u => u.UserId == SelectedUser.Id).FirstOrDefault().RoleId;
                        var oldSelectedRole = roleRepository.GetAllRoles().Where(r => r.IsSelected == true).FirstOrDefault();
                        if (oldSelectedRole.Id != newRoleId)
                        {
                            oldSelectedRole.IsSelected = false;
                            var newSelectedRole = roleRepository.GetAllRoles().Where(r => r.Id == newRoleId).FirstOrDefault();
                            newSelectedRole.IsSelected = true;
                            roleRepository.AddOrUpdate(oldSelectedRole);
                            roleRepository.AddOrUpdate(newSelectedRole);
                            Roles = new ObservableCollection<RoleDTO>(roleRepository.GetAllRoles());
                            SelectedRole = Roles.Where(r => r.IsSelected == true).FirstOrDefault();
                            Commands = new ObservableCollection<CommandDTO>(commandRepository.GetAllCommands().Where(co => co.RoleId == selectedRole.Id));
                            Tabs = new ObservableCollection<TabDTO>(tabRepository.GetTabDTOs().Where(t => t.RoleId == selectedRole.Id));
                        }
                        users = new ObservableCollection<UserDTO>(userRepository.GetUserDTOs());
                        SelectedUser = users.Where(u => u.IsSelected == true).FirstOrDefault();
                    }
                }
            });
            authorisationUser = new RelayCommand(c =>
            {
                System.Windows.Controls.PasswordBox p = (System.Windows.Controls.PasswordBox)c;
                var checkAuthUser = userRepository.GetUserDTOs().Where(u => u.Login == loginInUser).FirstOrDefault();
                if (checkAuthUser != null)
                {
                    bool checkPass = checkAuthUser.Password == p.Password.GetHashCode().ToString();
                    if (checkPass)
                    {
                        AuthorizedUser = checkAuthUser;
                        StatusAuthorisation = "Вы авторизированы как  - " +
                         authorizedUser.Login + " (" + authorizedUser.FirstName + " " + authorizedUser.LastName + ")";
                        SetComandAndTabSettings(authorizedUser);
                    }
                    else StatusAuthorisation = "Пароль не совпадает с оригиналом";
                }
                else StatusAuthorisation = "Такой логин не зарегистрирован в базе";

                LoginInUser = "";
                p.Clear();
            });
            #endregion
            #region Implementation command for Settings
            #region Raport
            addSettingRaport = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var cur_set = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.IsSelected == true && s.Intendant == _intendant).
                Select(s => s).FirstOrDefault();
                var osid = cur_set.Id;
                string new_set_name = "NewSetting1";
                while (displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Name == new_set_name).Count() > 0)
                {
                    string d_name = new_set_name.Trim(
                        new char[] { 'N', 'e', 'w', 'S', 't', 'i', 'n', 'g' });
                    int d = int.Parse(d_name) + 1;
                    new_set_name = "NewSetting" + d.ToString();
                }
                var _n_set = new DisplaySettingDTO()
                {
                    Name = new_set_name,
                    IsSelected = false,
                    Intendant = _intendant
                };
                var n_set = displaySettingDTORepository.AddOrUpdate(_n_set);
                addNewCollumn(_intendant, n_set.Id, osid);
                if (cur_set != null)
                {
                    cur_set.IsSelected = false;
                    displaySettingDTORepository.AddOrUpdate(cur_set);
                }
                n_set.IsSelected = true;
                displaySettingDTORepository.AddOrUpdate(n_set);
                updateAllSettingsRaport(_intendant);
            });
            delSettingRaport = new RelayCommand(c =>
            {
                var _intendant = c as string;
                DisplaySettingDTO s_ds = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.IsSelected == true && s.Intendant == _intendant).FirstOrDefault();
                var del_cs = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                Where(s => s.Intendant == _intendant).
                Where(s => s.DisplaySettingId == selectedDisplaySettingRaport.Id);
                foreach (var dc in del_cs)
                {
                    dSCollumnSettingDTORepository.RemoveDSCollumnSettingDTO(dc);
                }
                if (selectedDisplaySettingRaport.IsSelected == true)
                {
                    s_ds = displaySettingDTORepository.GetAllDisplaySettingDTOs().FirstOrDefault();
                    s_ds.IsSelected = true;
                    displaySettingDTORepository.AddOrUpdate(s_ds);
                    var new_sel_dsc = dSCollumnSettingDTORepository.
                    GetAllDSCollumnSettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.DisplaySettingId == s_ds.Id).
                    FirstOrDefault();
                    new_sel_dsc.IsSelected = true;
                    dSCollumnSettingDTORepository.AddOrUpdate(new_sel_dsc);
                }
                displaySettingDTORepository.RemoveDisplaySettingDTO(selectedDisplaySettingRaport);
                updateAllSettingsRaport(_intendant);
            }, c => displaySettingsRaport.Count() > 1);
            changeDisplaySettingDefaultRaport = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var oldSelectedDisplaySetting = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.IsSelected == true).
                    FirstOrDefault();
                var oldSelectedCollumnSetting = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.DisplaySettingId == oldSelectedDisplaySetting.Id).
                    Where(s => s.IsSelected == true).
                FirstOrDefault();
                oldSelectedDisplaySetting.IsSelected = false;
                oldSelectedCollumnSetting.IsSelected = false;
                var newSelectedDisplaySetting = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.Id == selectedDisplaySettingRaport.Id).
                    FirstOrDefault();
                var newSelectedCollumnSetting = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.DisplaySettingId == newSelectedDisplaySetting.Id).
                    FirstOrDefault();
                newSelectedDisplaySetting.IsSelected = true;
                newSelectedCollumnSetting.IsSelected = true;
                displaySettingDTORepository.AddOrUpdate(oldSelectedDisplaySetting);
                displaySettingDTORepository.AddOrUpdate(newSelectedDisplaySetting);
                dSCollumnSettingDTORepository.AddOrUpdate(newSelectedCollumnSetting);
                dSCollumnSettingDTORepository.AddOrUpdate(oldSelectedCollumnSetting);
                updateAllSettingsRaport(_intendant);
            });
            addCollumnRaport = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var dsid = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.IsSelected == true && s.Intendant == _intendant).
                    Select(s => s.Id).FirstOrDefault();
                addNewCollumn(_intendant, dsid, dsid);
                updateAllSettingsRaport(_intendant);
            }, c => dSCollumnSettingsRaport.Count() < 15);
            saveSettingChangesRaport = new RelayCommand(c =>
            {
                var _intendant = c as string;
                foreach (var d in DisplaySettingsRaport)
                {
                    displaySettingDTORepository.AddOrUpdate(d);
                }
                foreach (var dc in DSCollumnSettingsRaport)
                {
                    dSCollumnSettingDTORepository.AddOrUpdate(dc);
                }
                updateAllSettingsRaport(_intendant);
            });
            delCollumnRaport = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var sel_ds = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(s => s.IsSelected == true).
                        FirstOrDefault();
                var del_ds = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(d => d.Id == selectedCollumnSettingRaport.Id).
                        FirstOrDefault();
                dSCollumnSettingDTORepository.RemoveDSCollumnSettingDTO(selectedCollumnSettingRaport);
                if (sel_ds.Id == del_ds.Id)
                {
                    var sel_s_id = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(d => d.IsSelected == true).FirstOrDefault().Id;
                    var new_sel_ds = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(s => s.DisplaySettingId == sel_s_id).
                        FirstOrDefault();
                    new_sel_ds.IsSelected = true;
                    dSCollumnSettingDTORepository.AddOrUpdate(new_sel_ds);
                }

                updateAllSettingsRaport(_intendant);
            }, c => dSCollumnSettingsRaport.Count() > 1);
            #endregion
            #region Desctop
            addSettingDesctop = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var cur_set = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.IsSelected == true && s.Intendant == _intendant).
                Select(s => s).FirstOrDefault();
                var osid = cur_set.Id;
                string new_set_name = "NewSetting1";
                while (displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Name == new_set_name).Count() > 0)
                {
                    string d_name = new_set_name.Trim(
                        new char[] { 'N', 'e', 'w', 'S', 't', 'i', 'n', 'g' });
                    int d = int.Parse(d_name) + 1;
                    new_set_name = "NewSetting" + d.ToString();
                }
                var _n_set = new DisplaySettingDTO()
                {
                    Name = new_set_name,
                    IsSelected = false,
                    Intendant = _intendant
                };
                var n_set = displaySettingDTORepository.AddOrUpdate(_n_set);
                addNewCollumn(_intendant, n_set.Id, osid);
                if (cur_set != null)
                {
                    cur_set.IsSelected = false;
                    displaySettingDTORepository.AddOrUpdate(cur_set);
                }
                n_set.IsSelected = true;
                displaySettingDTORepository.AddOrUpdate(n_set);
                updateAllSettingsDesctop(_intendant);
            });
            delSettingDesctop = new RelayCommand(c =>
            {
                var _intendant = c as string;
                DisplaySettingDTO s_ds = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.IsSelected == true && s.Intendant == _intendant).FirstOrDefault();
                var del_cs = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                Where(s => s.Intendant == _intendant).
                Where(s => s.DisplaySettingId == selectedDisplaySettingDesctop.Id);//замена
                foreach (var dc in del_cs)
                {
                    dSCollumnSettingDTORepository.RemoveDSCollumnSettingDTO(dc);
                }
                if (selectedDisplaySettingDesctop.IsSelected == true)
                {
                    s_ds = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    FirstOrDefault();
                    s_ds.IsSelected = true;
                    displaySettingDTORepository.AddOrUpdate(s_ds);
                    var new_sel_dsc = dSCollumnSettingDTORepository.
                    GetAllDSCollumnSettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.DisplaySettingId == s_ds.Id).
                    FirstOrDefault();
                    new_sel_dsc.IsSelected = true;
                    dSCollumnSettingDTORepository.AddOrUpdate(new_sel_dsc);
                }
                displaySettingDTORepository.RemoveDisplaySettingDTO(selectedDisplaySettingDesctop);
                updateAllSettingsDesctop(_intendant);
            }, c => displaySettingsDesctop.Count() > 1);
            changeDisplaySettingDefaultDesctop = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var oldSelectedDisplaySetting = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.IsSelected == true).
                    FirstOrDefault();
                var oldSelectedCollumnSetting = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.DisplaySettingId == oldSelectedDisplaySetting.Id).
                    Where(s => s.IsSelected == true).
                    FirstOrDefault();
                oldSelectedDisplaySetting.IsSelected = false;
                oldSelectedCollumnSetting.IsSelected = false;
                var newSelectedDisplaySetting = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.Id == selectedDisplaySettingDesctop.Id).
                    FirstOrDefault();
                var newSelectedCollumnSetting = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                Where(s => s.DisplaySettingId == newSelectedDisplaySetting.Id).FirstOrDefault();
                newSelectedDisplaySetting.IsSelected = true;
                newSelectedCollumnSetting.IsSelected = true;
                displaySettingDTORepository.AddOrUpdate(oldSelectedDisplaySetting);
                displaySettingDTORepository.AddOrUpdate(newSelectedDisplaySetting);
                dSCollumnSettingDTORepository.AddOrUpdate(newSelectedCollumnSetting);
                dSCollumnSettingDTORepository.AddOrUpdate(oldSelectedCollumnSetting);
                updateAllSettingsDesctop(_intendant);
            });
            addCollumnDesctop = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var dsid = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.IsSelected == true && s.Intendant == _intendant).
                    Select(s => s.Id).FirstOrDefault();
                addNewCollumn(_intendant, dsid, dsid);
                updateAllSettingsDesctop(_intendant);
            }, c => dSCollumnSettingsDesctop.Count() < 15);
            saveSettingChangesDesctop = new RelayCommand(c =>
            {
                var _intendant = c as string;
                foreach (var d in DisplaySettingsDesctop)
                {
                    displaySettingDTORepository.AddOrUpdate(d);
                }
                foreach (var dc in DSCollumnSettingsDesctop)
                {
                    dSCollumnSettingDTORepository.AddOrUpdate(dc);
                }
                updateAllSettingsDesctop(_intendant);
            });
            delCollumnDesctop = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var sel_ds = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(s => s.IsSelected == true).
                        FirstOrDefault();
                var del_ds = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(d => d.Id == selectedCollumnSettingDesctop.Id).
                        FirstOrDefault();
                dSCollumnSettingDTORepository.RemoveDSCollumnSettingDTO(selectedCollumnSettingDesctop);
                if (sel_ds.Id == del_ds.Id)
                {
                    var sel_s_id = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(d => d.IsSelected == true).FirstOrDefault().Id;
                    var new_sel_ds = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(s => s.DisplaySettingId == sel_s_id).
                        FirstOrDefault();
                    new_sel_ds.IsSelected = true;
                    dSCollumnSettingDTORepository.AddOrUpdate(new_sel_ds);
                }
                updateAllSettingsDesctop(_intendant);
            }, c => dSCollumnSettingsDesctop.Count() > 1);
            #endregion
            #region Form
            addSettingForm = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var cur_set = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Intendant == _intendant).
                Where(s => s.IsSelected == true).
                FirstOrDefault();
                var osid = cur_set.Id;
                string new_set_name = "NewSetting1";
                while (displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Name == new_set_name).Count() > 0)
                {
                    string d_name = new_set_name.Trim(
                        new char[] { 'N', 'e', 'w', 'S', 't', 'i', 'n', 'g' });
                    int d = int.Parse(d_name) + 1;
                    new_set_name = "NewSetting" + d.ToString();
                }
                var _n_set = new DisplaySettingDTO()
                {
                    Name = new_set_name,
                    IsSelected = false,
                    Intendant = _intendant
                };
                var n_set = displaySettingDTORepository.AddOrUpdate(_n_set);
                addNewCollumn(_intendant, n_set.Id, osid);
                if (cur_set != null)
                {
                    cur_set.IsSelected = false;
                    displaySettingDTORepository.AddOrUpdate(cur_set);
                }
                n_set.IsSelected = true;
                displaySettingDTORepository.AddOrUpdate(n_set);
                updateAllSettingsForm(_intendant);
            });
            delSettingForm = new RelayCommand(c =>
            {
                var _intendant = c as string;
                DisplaySettingDTO s_ds = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Intendant == _intendant).
                Where(s => s.IsSelected == true).
                FirstOrDefault();
                var del_cs = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                Where(s => s.Intendant == _intendant).
                Where(s => s.DisplaySettingId == selectedDisplaySettingForm.Id);//замена
                foreach (var dc in del_cs)
                {
                    dSCollumnSettingDTORepository.RemoveDSCollumnSettingDTO(dc);
                }
                if (selectedDisplaySettingForm.IsSelected == true)
                {
                    s_ds = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    FirstOrDefault();
                    s_ds.IsSelected = true;
                    displaySettingDTORepository.AddOrUpdate(s_ds);
                    var new_sel_dsc = dSCollumnSettingDTORepository.
                    GetAllDSCollumnSettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.DisplaySettingId == s_ds.Id).
                    FirstOrDefault();
                    new_sel_dsc.IsSelected = true;
                    dSCollumnSettingDTORepository.AddOrUpdate(new_sel_dsc);
                }
                displaySettingDTORepository.RemoveDisplaySettingDTO
                (selectedDisplaySettingForm);
                updateAllSettingsForm(_intendant);
            }, c => displaySettingsForm.Count() > 1);
            changeDisplaySettingDefaultForm = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var oldSelectedDisplaySetting = displaySettingDTORepository.
                GetAllDisplaySettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.IsSelected == true).
                    FirstOrDefault();
                var oldSelectedCollumnSetting = dSCollumnSettingDTORepository.
                GetAllDSCollumnSettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.DisplaySettingId == oldSelectedDisplaySetting.Id).
                    Where(s => s.IsSelected == true).
                    FirstOrDefault();
                oldSelectedDisplaySetting.IsSelected = false;
                oldSelectedCollumnSetting.IsSelected = false;
                var newSelectedDisplaySetting = displaySettingDTORepository.
                GetAllDisplaySettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.Id == selectedDisplaySettingForm.Id).
                    FirstOrDefault();
                var newSelectedCollumnSetting = dSCollumnSettingDTORepository.
                GetAllDSCollumnSettingDTOs().
                Where(s => s.DisplaySettingId == newSelectedDisplaySetting.Id).
                FirstOrDefault();
                newSelectedDisplaySetting.IsSelected = true;
                newSelectedCollumnSetting.IsSelected = true;
                displaySettingDTORepository.AddOrUpdate(oldSelectedDisplaySetting);
                displaySettingDTORepository.AddOrUpdate(newSelectedDisplaySetting);
                dSCollumnSettingDTORepository.AddOrUpdate(newSelectedCollumnSetting);
                dSCollumnSettingDTORepository.AddOrUpdate(oldSelectedCollumnSetting);
                updateAllSettingsForm(_intendant);
            });
            addCollumnForm = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var dsid = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.IsSelected == true).
                    Select(s => s.Id).FirstOrDefault();
                addNewCollumn(_intendant, dsid, dsid);
                updateAllSettingsForm(_intendant);
            }, c => dSCollumnSettingsForm.Count() < 15);
            saveSettingChangesForm = new RelayCommand(c =>///jcn
            {
                var _intendant = c as string;
                foreach (var d in DisplaySettingsForm)
                {
                    displaySettingDTORepository.AddOrUpdate(d);
                }
                foreach (var dc in DSCollumnSettingsForm)
                {
                    dSCollumnSettingDTORepository.AddOrUpdate(dc);
                }
                updateAllSettingsForm(_intendant);
            });
            delCollumnForm = new RelayCommand(c =>
            {
                var _intendant = c as string;
                var sel_ds = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(s => s.IsSelected == true).
                        FirstOrDefault();
                var del_ds = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(d => d.Id == selectedCollumnSettingForm.Id).
                        FirstOrDefault();
                dSCollumnSettingDTORepository.RemoveDSCollumnSettingDTO(selectedCollumnSettingForm);
                if (sel_ds.Id == del_ds.Id)
                {
                    var sel_s_id = displaySettingDTORepository.
                    GetAllDisplaySettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(d => d.IsSelected == true).
                        FirstOrDefault().Id;
                    var new_sel_ds = dSCollumnSettingDTORepository.
                    GetAllDSCollumnSettingDTOs().
                        Where(s => s.Intendant == _intendant).
                        Where(s => s.DisplaySettingId == sel_s_id).
                        FirstOrDefault();
                    new_sel_ds.IsSelected = true;
                    dSCollumnSettingDTORepository.AddOrUpdate(new_sel_ds);
                }
                updateAllSettingsForm(_intendant);
            }, c => dSCollumnSettingsForm.Count() > 1);
            #endregion
            #region PrintBage
            addPrintSetting = new RelayCommand(c =>
            {
                var cur_set = printSettingRepositoryDTO.GetAllPrintSettingDTOs().
                Where(s => s.IsSelected == true).
                FirstOrDefault();
                var osid = cur_set.Id;
                string new_set_name = "NewSetting1";
                while (printSettingRepositoryDTO.GetAllPrintSettingDTOs().
                Where(s => s.Name == new_set_name).Count() > 0)
                {
                    string d_name = new_set_name.Trim(
                        new char[] { 'N', 'e', 'w', 'S', 't', 'i', 'n', 'g' });
                    int d = int.Parse(d_name) + 1;
                    new_set_name = "NewSetting" + d.ToString();
                }
                var _n_set = new PrintSettingDTO()
                {
                    Name = new_set_name,
                    IsSelected = false
                };
                var n_set = printSettingRepositoryDTO.AddOrUpdate(_n_set);
                addNewCollumn(n_set.Id, osid);
                if (cur_set != null)
                {
                    cur_set.IsSelected = false;
                    printSettingRepositoryDTO.AddOrUpdate(cur_set);
                }
                n_set.IsSelected = true;
                printSettingRepositoryDTO.AddOrUpdate(n_set);
                updateAllPrintSettings();
            });
            delPrintSetting = new RelayCommand(c =>
            {
                PrintSettingDTO s_ds = printSettingRepositoryDTO.GetAllPrintSettingDTOs().
                Where(s => s.IsSelected == true).
                FirstOrDefault();
                var del_cs = printStringSettingRepositoryDTO.GetAllPrintStringSettingDTOs().
                Where(s => s.PrintSettingId == selectedPrintSetting.Id);//замена
                foreach (var dc in del_cs)
                {
                    printStringSettingRepositoryDTO.RemovePrintStringSettingDTO(dc);
                }
                if (selectedPrintSetting.IsSelected == true)
                {
                    s_ds = printSettingRepositoryDTO.GetAllPrintSettingDTOs().
                    FirstOrDefault();
                    s_ds.IsSelected = true;
                    printSettingRepositoryDTO.AddOrUpdate(s_ds);
                    var new_sel_dsc = printStringSettingRepositoryDTO.
                    GetAllPrintStringSettingDTOs().
                    Where(s => s.PrintSettingId == s_ds.Id).
                    FirstOrDefault();
                    new_sel_dsc.IsSelected = true;
                    printStringSettingRepositoryDTO.AddOrUpdate(new_sel_dsc);
                }
                printSettingRepositoryDTO.RemovePrintSettingDTO
                (selectedPrintSetting);
                updateAllPrintSettings();
            }, c => printSettingDTOs.Count() > 1);
            changePrintSettingDefault = new RelayCommand(c =>
            {
                var oldSelectedPrintSetting = printSettingRepositoryDTO.
                GetAllPrintSettingDTOs().
                    Where(s => s.IsSelected == true).
                    FirstOrDefault();
                var oldSelectedPrintStringSetting = PrintStringSettingRepositoryDTO.
                GetAllPrintStringSettingDTOs().
                    Where(s => s.PrintSettingId == oldSelectedPrintSetting.Id).
                    Where(s => s.IsSelected == true).
                    FirstOrDefault();
                oldSelectedPrintSetting.IsSelected = false;
                oldSelectedPrintStringSetting.IsSelected = false;
                var newSelectedPrintSetting = printSettingRepositoryDTO.
                GetAllPrintSettingDTOs().
                    Where(s => s.Id == selectedPrintSetting.Id).
                    FirstOrDefault();
                var newSelectedPrintStringSetting = PrintStringSettingRepositoryDTO.
                GetAllPrintStringSettingDTOs().
                Where(s => s.PrintSettingId == newSelectedPrintSetting.Id).
                FirstOrDefault();
                newSelectedPrintSetting.IsSelected = true;
                newSelectedPrintStringSetting.IsSelected = true;
                printSettingRepositoryDTO.AddOrUpdate(oldSelectedPrintSetting);
                printSettingRepositoryDTO.AddOrUpdate(newSelectedPrintSetting);
                PrintStringSettingRepositoryDTO.AddOrUpdate(newSelectedPrintStringSetting);
                PrintStringSettingRepositoryDTO.AddOrUpdate(oldSelectedPrintStringSetting);
                updateAllPrintSettings();
            });
            addPrintStringSetting = new RelayCommand(c =>
            {
                var dsid = printSettingRepositoryDTO.GetAllPrintSettingDTOs().
                    Where(s => s.IsSelected == true).
                    Select(s => s.Id).FirstOrDefault();
                addNewCollumn(dsid, dsid);
                updateAllPrintSettings();
            }, c => printStringSettingDTOs.Count() < 15);
            savePrintSettingChanges = new RelayCommand(c =>
            {
                foreach (var d in PrintSettingDTOs)
                {
                    printSettingRepositoryDTO.AddOrUpdate(d);
                }
                foreach (var dc in printStringSettingDTOs)
                {
                    PrintStringSettingRepositoryDTO.AddOrUpdate(dc);
                }
     //           System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
     //           Application.Current.Shutdown();
            });
            delPrintStringSetting = new RelayCommand(c =>
            {
                var sel_ds = printStringSettingRepositoryDTO.GetAllPrintStringSettingDTOs().
                        Where(s => s.IsSelected == true).
                        FirstOrDefault();
                var del_ds = PrintStringSettingRepositoryDTO.GetAllPrintStringSettingDTOs().
                        Where(d => d.Id == selectedPrintStringSetting.Id).
                        FirstOrDefault();
                printStringSettingRepositoryDTO.RemovePrintStringSettingDTO
                (selectedPrintStringSetting);
                if (sel_ds.Id == del_ds.Id)
                {
                    var sel_s_id = printSettingRepositoryDTO.
                    GetAllPrintSettingDTOs().
                        Where(d => d.IsSelected == true).
                        FirstOrDefault().Id;
                    var new_sel_ds = PrintStringSettingRepositoryDTO.
                    GetAllPrintStringSettingDTOs().
                        Where(s => s.PrintSettingId == sel_s_id).
                        FirstOrDefault();
                    new_sel_ds.IsSelected = true;
                    PrintStringSettingRepositoryDTO.AddOrUpdate(new_sel_ds);
                }
                updateAllPrintSettings();
            }, c => printStringSettingDTOs.Count() > 1);
            #endregion
            #endregion
            #region Implementation command for File
            addDataFromFileToDatabase = new RelayCommand(c =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                _ProgressBar = new Progress_Bar
                {
                    Visible = true, Progress = 1, Status = "Start"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    Task.Factory.StartNew(() =>
                    {
                        visitorRepositoryDTO.InitRepositoryFromFole
                        (openFileDialog.FileName);
                        visitors = new ObservableCollection<VisitorDTO>
                        (visitorRepositoryDTO.GetAllVisitors());
                        _ProgressBar.Status = "All data added to database";
                        _ProgressBar.Progress = 0;
                        Thread.Sleep(1000);
                        _ProgressBar.Status = "Update visitor statuses";
                        statusRepository = new StatusRepositoryDTO();
                        int f = 0, f_m = visitors.Count();

                        foreach (var v in visitors)
                        {
                            var newStatus = new StatusDTO
                            {
                                Name = "registered",
                                UserId = authorizedUser.Id,
                                VisitorId = v.Id,
                                ActionTime = DateTime.Now.ToString()

                            };

                            _ProgressBar.Progress = f * 100 / f_m;
                            f++;
                            v.CurrentStatus = newStatus.Name;
                            visitorRepositoryDTO.AddOrUpdateVisitor(v);
                            statusRepository.Add(newStatus);
                        }
                        Statuses = new ObservableCollection<StatusDTO>
                            (statusRepository.GetAllStatuses());
                        _ProgressBar.Visible = false;
                        Visitors = new ObservableCollection<VisitorDTO>
                        (visitorRepositoryDTO.GetAllVisitors());
                        UpdateAllVisitorFields(visitors);
                        _ProgressBar.Status = "All operation complite";
                        _ProgressBar.Progress = 0;
                    });
                }
            });
            importDataFromFileToDatabase = new RelayCommand(c =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                _ProgressBar = new Progress_Bar
                {
                    Visible = true,
                    Progress = 1,
                    Status = "Start"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    Task.Factory.StartNew(() =>
                    {
                        visitorRepositoryDTO.ImportRepositoryFromFole
                        (openFileDialog.FileName);
                        visitors = new ObservableCollection<VisitorDTO>
                        (visitorRepositoryDTO.GetAllVisitors());
                        _ProgressBar.Status = "All operation complite";
                        _ProgressBar.Progress = 0;
                    });
                }
            });
            importVisitorFromFileWithId = new RelayCommand(c =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                _ProgressBar = new Progress_Bar
                {
                    Visible = true,
                    Progress = 1,
                    Status = "Start"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    Task.Factory.StartNew(() =>
                    {
                        visitorRepositoryDTO.ImportVisitorsRepositoryFromFileWithId
                        (openFileDialog.FileName);
                        visitors = new ObservableCollection<VisitorDTO>
                        (visitorRepositoryDTO.GetAllVisitors());
                        _ProgressBar.Status = "All operation complite";
                        _ProgressBar.Progress = 0;
                    });
                }
            });
            #endregion
            #region Implementation command for Service
            changeDataMode = new RelayCommand(c =>
            {
                ErrorConnectServer = "";
                DataMode = (string)c;
                if (DataMode == "Клиент службы баз данных")
                {
                    //  clientExecutor.GetClient().InnerChannel.i
                    try
                    {
                        var _visitors = clientExecutor.GetClient().GetAllVisitors();
                        VisitorDTO visitorDTO = new VisitorDTO();
                        Visitors = new ObservableCollection<VisitorDTO>();
                        foreach (var v in _visitors) { Visitors.Add(mapper.Map<VisitorDTO>(v)); }
                        ErrorConnectServer = "";
                    }
                    catch
                    {
                        DataMode = "Локальная база данных";
                        ErrorConnectServer = "Сервер недоступен";
                    }

                }
                else Visitors = new ObservableCollection<VisitorDTO>
                    (visitorRepositoryDTO.GetAllVisitors());
                UpdateAllVisitorFields(visitors);
            });
            startServer = new RelayCommand(c =>
            {
                IsEnabledDataModeChecker = false;
                Task.Factory.StartNew(serviceExecutor.Start);
                Thread.Sleep(200);
            }, c =>
                 DataMode == "Сервер службы баз данных" && ServerStatus != "listerning....");
            stopServer = new RelayCommand(c =>
            {
                IsEnabledDataModeChecker = true;
                Task.Factory.StartNew(serviceExecutor.Stop);
                Thread.Sleep(200);
            }, c => ServerStatus == "listerning....");
            #endregion
            #region Implementation command for Desctop Operation
            createVisitor = new RelayCommand(c =>
            {
                if (editDesctopVisitor.Column4 != null &&
                    editDesctopVisitor.Column8 != null &&
                    editDesctopVisitor.Column9 != null &&
                    editDesctopVisitor.Column11 != null)
                {
                    createDesctopVisitor = editDesctopVisitor;
                    createDesctopVisitor.CurrentStatus = "create";
                    IsShowChanger = false;
                    createDesctopVisitor.Column12 = DateTime.Now.ToString(); // time collumn
                    if (dataMode != "Клиент службы баз данных")
                    {
                        var _createVisitor = visitorRepositoryDTO
                        .AddOrUpdateVisitor(createDesctopVisitor);
                        PrintVisitor(_createVisitor);
                        statusRepository.Add(new StatusDTO
                        {
                            Name = "create",
                            UserId = AuthorizedUser.Id,
                            VisitorId = _createVisitor.Id,
                            ActionTime = DateTime.Now.ToString()
                        });
                        Visitors = new ObservableCollection<VisitorDTO>
                        (visitorRepositoryDTO.GetAllVisitors());
                        UpdateAllVisitorFields(visitors, _createVisitor.Id);
                    }
                    else
                    {
                        var _createVisitor = clientExecutor.GetClient()
                        .AddOrUpdateVisitor(mapper.Map<EX.Client
                        .ServiceReference1.VisitorDTO>(createDesctopVisitor));
                        PrintVisitor(mapper.Map<VisitorDTO>(_createVisitor));
                        var _visitors = clientExecutor.GetClient().GetAllVisitors();
                        clientExecutor.GetClient().AddStatus(mapper.Map
                            <EX.Client.ServiceReference1.StatusDTO>(new StatusDTO
                            {
                                Name = "create",
                                UserId = AuthorizedUser.Id,
                                VisitorId = _createVisitor.Id,
                                ActionTime = DateTime.Now.ToString()
                            }));
                        //                    Visitors.Add(mapper.Map<VisitorDTO>(_createVisitor));
                        Visitors = new ObservableCollection<VisitorDTO>();
                        foreach (var v in _visitors) { Visitors.Add(mapper.Map<VisitorDTO>(v)); }
                        UpdateAllVisitorFields(visitors, _createVisitor.Id);
                    }
                    CreateDesctopVisitor = new VisitorDTO();
                    EditDesctopVisitor = new VisitorDTO();
                } else
                {
                    Task.Factory.StartNew(delegate
                    {
                        for (var i = 0; i < 10; i++)
                        {
                            if (i % 2 == 0)
                            {
                                StatusForm = "Обязательные поля";
                            }
                            else
                            {
                                StatusForm = "";
                            }
                            Thread.Sleep(500);
                        }
                        StatusForm = "";
                    });
                }
            }, c => canExecuteCreateVisitor);
            editVisitor = new RelayCommand(c =>
            {
                IsShowChanger = true;
                if (selectDesctopVisitor == null || selectDesctopVisitor.Column4 == null)
                {

                }
                else
                {
                    EditDesctopVisitor = selectDesctopVisitor;
                    CanExecuteCreateVisitor = false;
                    CanExecuteEditVisitor = false;
                    CanExecuteSaveEditVisitor = true;
                    InitColorInformation(editDesctopVisitor);
                }
            }, c => canExecuteEditVisitor);
            saveEditVisitor = new RelayCommand(c =>
            {
                IsShowChanger = false;
                //                editDesctopVisitor.CurrentStatus = "edited";
                editDesctopVisitor.Column12 = DateTime.Now.ToString();
                PrintVisitor(editDesctopVisitor);

                if (dataMode != "Клиент службы баз данных")
                {
                    if (editDesctopVisitor.Column6 == "Замена")
                    {
                        var oldEditVisitor = visitorRepositoryDTO
                        .GetAllVisitors()
                        .Where(s => s.Id == editDesctopVisitor.Id)
                        .FirstOrDefault();
                        editDesctopVisitor.Column7 =
                        oldEditVisitor.Column8 + " " +
                        oldEditVisitor.Column9;
                    }

                    var _editVisitor = visitorRepositoryDTO
                    .AddOrUpdateVisitor(editDesctopVisitor);
                    statusRepository.Add(new StatusDTO
                    {
                        Name = _editVisitor.CurrentStatus,
                        UserId = authorizedUser.Id,
                        VisitorId = _editVisitor.Id,
                        ActionTime = DateTime.Now.ToString()
                    });

                    Visitors = new ObservableCollection<VisitorDTO>
                    (visitorRepositoryDTO.GetAllVisitors());
                    UpdateAllVisitorFields(visitors, _editVisitor.Id);
                }
                else
                {
                    if (editDesctopVisitor.Column6 == "Замена")
                    {
                        var oldEditVisitor = clientExecutor.GetClient()
                        .GetAllVisitors()
                        .Where(s => s.Id == editDesctopVisitor.Id)
                        .FirstOrDefault();
                        editDesctopVisitor.Column7 =
                        oldEditVisitor.Column8 + " " +
                        oldEditVisitor.Column9;
                    }

                    var _editVisitor = clientExecutor.GetClient()
                    .AddOrUpdateVisitor(mapper.Map<EX.Client
                    .ServiceReference1.VisitorDTO>(editDesctopVisitor));

                    var _visitors = clientExecutor.GetClient().GetAllVisitors();
                    clientExecutor.GetClient().AddStatus(mapper.Map
                        <EX.Client.ServiceReference1.StatusDTO>(new StatusDTO
                        {
                            Name = _editVisitor.CurrentStatus,
                            UserId = authorizedUser.Id,
                            VisitorId = _editVisitor.Id,
                            ActionTime = DateTime.Now.ToString()
                        }));
                    Visitors = new ObservableCollection<VisitorDTO>();
                    foreach (var v in _visitors) { Visitors.Add(mapper.Map<VisitorDTO>(v)); }
                    UpdateAllVisitorFields(visitors, _editVisitor.Id);
                }
                CanExecuteCreateVisitor = true;
                CanExecuteEditVisitor = true;
                CanExecuteSaveEditVisitor = false;
                EditDesctopVisitor = new VisitorDTO();
                BagePresenter = "Цвет бейджа";
                BagePresenterBackGround = "#FFE5E5E5";
                PaymentStatusPresenter = "Статус Оплаты";
                PaymentStatusFontsize = 12;
                PaymentStatusForegraund = "Gray";
            }, c => canExecuteSaveEditVisitor);
            addVisitorToFact = new RelayCommand(c =>
            {
                IsShowChanger = true;
                EditDesctopVisitor = selectedSearchVisitor;
                EditDesctopVisitor.CurrentStatus = "actual";
                CanExecuteCreateVisitor = false;
                CanExecuteEditVisitor = false;
                CanExecuteSaveEditVisitor = true;

                InitColorInformation(editDesctopVisitor);
                SearchVisitor = "Введите информацию для поиска";
                SearchVisitorFontsize = 25;
                SearchVisitorBackGround = "White";
                SearchVisitorForegraund = "Gray";
                SearchVisitorCollection = new ObservableCollection<VisitorDTO>();
            });
            saveVisitorsToFile = new RelayCommand(c =>
            {
                visitorRepositoryDTO.SaveVisitorsToFile();
            });
            saveStatusesToFile = new RelayCommand(c =>
            {
                statusRepository.SaveStatusestoFile();
            });
            #endregion
            #region Implementation command for Raport
            changeCategoryRegistredVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("registred");
            });
            changePaymentStatusRegistredVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("registred");
            });
            changeChangedRegistredVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("registred");
            });
            changeCategoryActualVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("actual");
            });
            changePaymentStatusActualVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("actual");
            });
            changeChangedActualVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("actual");
            });
            changeCategoryUnActualVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("unactual");
            });
            changePaymentStatusUnActualVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("unactual");
            });
            changeChangedUnActualVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("unactual");
            });
            changeCategoryCreateVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("create");
            });
            changePaymentStatusCreateVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("create");
            });
            changeChangedCreateVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("create");
            });
            changeCategoryAllVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("all");
            });
            changePaymentStatusAllVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("all");
            });
            changeChangedAllVisitors = new RelayCommand(c =>
            {
                InitRaportInformation("all");
            });
            #endregion
        }
        #region Implemetation methods
        private void addNewCollumn(int dsid, int osid)
        {
            string _name = "NewCollumn1";
            while (printStringSettingRepositoryDTO.GetAllPrintStringSettingDTOs().
                Where(s => s.Name == _name).Select(s => s).Count() > 0)
            {
                string d_name = _name.Trim(new char[] {
                        'N', 'e', 'w', 'C', 'o', 'l', 'u', 'm', 'n' });
                int d = int.Parse(d_name) + 1;
                _name = "NewCollumn" + d.ToString();
            }
            var oldSelectedPrintStringSetting = printStringSettingRepositoryDTO.GetAllPrintStringSettingDTOs().
                Where(s => s.IsSelected == true && s.PrintSettingId == osid).
                FirstOrDefault();
            oldSelectedPrintStringSetting.IsSelected = false;
            var newSelectedPrintStringSetting = printStringSettingRepositoryDTO.AddOrUpdate(new
                PrintStringSettingDTO
            {
                Name = _name,
                FontName = "Verdana",
                FontSize = 18,
                FontWeight = 400,
                Visible = true,
                ToUpper = true,
                PrintSettingId = dsid,
                IsSelected = true
            });
            printStringSettingRepositoryDTO.AddOrUpdate(oldSelectedPrintStringSetting);
            printStringSettingRepositoryDTO.AddOrUpdate(newSelectedPrintStringSetting);
        }
        private void addNewCollumn(string _intendant, int dsid, int osid)
        {
            string _name = "NewCollumn1";
            string _alias = "NewAlias1";
            while (dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                Where(s=>s.Intendant == _intendant).
                Where(s => s.Name == _name).Select(s => s).Count() > 0)
            {
                string d_name = _name.Trim(new char[] {
                        'N', 'e', 'w', 'C', 'o', 'l', 'u', 'm', 'n' });
                int d = int.Parse(d_name) + 1;
                _name = "NewCollumn" + d.ToString();
                _alias = "NewAlias" + d.ToString();
            }
            var oldSelectedCollumnSetting = dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
                Where(s => s.Intendant == _intendant).
                Where(s => s.IsSelected == true && s.DisplaySettingId == osid).
                FirstOrDefault();
            oldSelectedCollumnSetting.IsSelected = false;
            var newSelectedCollumnSetting = dSCollumnSettingDTORepository.AddOrUpdate(new
                DSCollumnSettingDTO
            {
                Name = _name,
                Alias = _alias,
                Visible = true,
                DisplaySettingId = dsid,
                Width = 100,
                Intendant = _intendant,
                IsSelected = true
            });
            dSCollumnSettingDTORepository.AddOrUpdate(oldSelectedCollumnSetting);
            dSCollumnSettingDTORepository.AddOrUpdate(newSelectedCollumnSetting);
        }
        private void SetComandAndTabSettings(UserDTO user)
        {
            var _roleId = userInRoleRepository.GetAllUserInRolesDTOs().
                        Where(r => r.UserId == user.Id).FirstOrDefault().RoleId;
            var commands = commandRepository.GetAllCommands().Where(c => c.RoleId == _roleId);
            var tabs = tabRepository.GetTabDTOs().Where(t => t.RoleId == _roleId);
            VisibleManageUserRole = tabs.Where(t => t.Name == "Управление доступом (Администрирование)")
                .Select(s => s.IsChecked).FirstOrDefault() ? 30 : 0;
        }
        private void CheckAndCorrectAddedRoleIfNeed(RoleDTO _addedRole)
        {
            if (roleRepository.GetAllRoles().Where(r => r.Name == _addedRole.Name).Count() > 0)
            {
                addedRole.Name = "NewRole1";
                while (roleRepository.GetAllRoles().Where(r => r.Name == addedRole.Name).Count() > 0)
                {
                    addedRole.Name = addedRole.Name.Trim(new char[] { 'N', 'e', 'w', 'R', 'o', 'l', 'e' });
                    int d = int.Parse(addedRole.Name) + 1;
                    addedRole.Name = "NewRole" + d.ToString();
                }
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }
        private void updateAllSettingsRaport(string intendant)
        {
            DisplaySettingsRaport = new ObservableCollection<DisplaySettingDTO>
                (displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s=>s.Intendant == intendant));
            SelectedDisplaySettingRaport = DisplaySettingsRaport.
                Where(s => s.Intendant == intendant).
                Where(s => s.IsSelected == true).
                FirstOrDefault();
            DSCollumnSettingsRaport = new ObservableCollection<DSCollumnSettingDTO>
            (dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().
            Where(s=>s.Intendant == intendant).
            Where(d=>d.DisplaySettingId == selectedDisplaySettingRaport.Id));
            SelectedCollumnSettingRaport = DSCollumnSettingsRaport.
                Where(s=>s.Intendant == "raport").
                Where(s => s.IsSelected == true).
                FirstOrDefault();
        }
        private void updateAllSettingsDesctop(string intendant)
        {
            DisplaySettingsDesctop = new ObservableCollection<DisplaySettingDTO>
                (displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Intendant == intendant));
            SelectedDisplaySettingDesctop = DisplaySettingsDesctop.
                Where(s => s.Intendant == intendant).
                Where(s => s.IsSelected == true).
                FirstOrDefault();
            DSCollumnSettingsDesctop = new ObservableCollection<DSCollumnSettingDTO>
            (dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().//?
            Where(s => s.Intendant == intendant).
            Where(d => d.DisplaySettingId == selectedDisplaySettingDesctop.Id));
            SelectedCollumnSettingDesctop = DSCollumnSettingsDesctop.
                Where(s => s.IsSelected == true).
                FirstOrDefault();
        }
        private void updateAllSettingsForm(string intendant)
        {
            DisplaySettingsForm = new ObservableCollection<DisplaySettingDTO>
                (displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s => s.Intendant == intendant));
            SelectedDisplaySettingForm = DisplaySettingsForm.
                Where(s => s.Intendant == intendant).
                Where(s => s.IsSelected == true).
                FirstOrDefault();
            DSCollumnSettingsForm = new ObservableCollection<DSCollumnSettingDTO>
            (dSCollumnSettingDTORepository.GetAllDSCollumnSettingDTOs().//?
            Where(s => s.Intendant == intendant).
            Where(ds => ds.DisplaySettingId == selectedDisplaySettingForm.Id));
            SelectedCollumnSettingForm = DSCollumnSettingsForm.
                Where(s => s.IsSelected == true).
                FirstOrDefault();
        }
        private void updateAllPrintSettings()
        {
            PrintSettingDTOs = new ObservableCollection<PrintSettingDTO>
                (printSettingRepositoryDTO.GetAllPrintSettingDTOs());
            SelectedPrintSetting = PrintSettingDTOs.
                Where(s => s.IsSelected == true).
                FirstOrDefault();
            PrintStringSettingDTOs = new ObservableCollection<PrintStringSettingDTO>
            (printStringSettingRepositoryDTO.GetAllPrintStringSettingDTOs().//?
            Where(ds => ds.PrintSettingId == selectedPrintSetting.Id));
            SelectedPrintStringSetting = printStringSettingDTOs.
                Where(s => s.IsSelected == true).
                FirstOrDefault();
            SelectedFont = selectedPrintStringSetting.FontName + " "
                + selectedPrintStringSetting.FontSize.ToString();
        }
        private void ProgressChanged(Progress_Bar progress)
        {
            _ProgressBar.Progress = progress.Progress;
            _ProgressBar.Status = progress.Status;
            _ProgressBar.Visible = progress.Visible;
        }
        private void StatusChanged(string obj)
        {
            ServerStatus = (string)obj;
        }
        private void initHeaderDesctop(ObservableCollection<DSCollumnSettingDTO> _dSColumnSettings)
        {
            var sc = _dSColumnSettings.ToArray();
            //коллекция visible
            bool[] _columnChecked = new bool[sc.Count()];
            for (int i = 0; i < sc.Count(); i++)
            {
                _columnChecked[i] = dSCollumnSettingsDesctop  //????????????
                    .Where(s => s.Id == sc[i].Id)
                    .Select(s => s.Visible)
                    .FirstOrDefault(); }
            for (int i = 0; i < count_headers; i++)
            {
                try { ColumnCheckedDesctop[i] = _columnChecked[i]; }
                catch { ColumnCheckedDesctop[i] = false; }
            }
            ColumnCheckedDesctop = columnCheckedDesctop;
            //коллекция алиасов
            string[] _alias = new string[sc.Count()];
            for (int i = 0; i < sc.Count(); i++)
            {
                _alias[i] = dSCollumnSettingsDesctop  //??????????????
                    .Where(s => s.Id == sc[i].Id)
                    .Select(s => s.Alias).FirstOrDefault();
            }
            for (int i = 0; i < count_headers; i++)
            {
                try { AliasDesctop[i] = _alias[i]; }
                catch { AliasDesctop[i] = "none"; }
            }
            AliasDesctop = aliasDesctop;
            //коллекция width
            int[] _width = new int[sc.Count()];
            for (int i = 0; i < sc.Count(); i++)
            {
                _width[i] = dSCollumnSettingsDesctop//?????????????????
                    .Where(s => s.Id == sc[i].Id)
                    .Select(s => s.Width)
                    .FirstOrDefault();
            }
            for (int i = 0; i < count_headers; i++)
            {
                try { WidthDesctop[i] = _width[i]; }
                catch { WidthDesctop[i] = 100; }
            }
            WidthDesctop = widthDesctop;
        }
        private void initHeaderRaport(ObservableCollection<DSCollumnSettingDTO> _dSColumnSettings)
        {
            var sc = _dSColumnSettings.ToArray();
            //коллекция visible
            var _columnChecked = new bool[sc.Count()];
            for (int i = 0; i < sc.Count(); i++)
            {
                _columnChecked[i] = _dSColumnSettings  //????????????
                    .Where(s => s.Id == sc[i].Id)
                    .Select(s => s.Visible)
                    .FirstOrDefault();
            }
            for (int i = 0; i < count_headers; i++)
            {
                try { ColumnCheckedRaport[i] = _columnChecked[i]; }
                catch { ColumnCheckedRaport[i] = false; }
            }
            ColumnCheckedRaport = columnCheckedRaport;
            //коллекция алиасов
            var _alias = new string[sc.Count()];
            for (int i = 0; i < sc.Count(); i++)
            {
                _alias[i] = _dSColumnSettings  //??????????????
                    .Where(s => s.Id == sc[i].Id)
                    .Select(s => s.Alias).FirstOrDefault();
            }
            for (int i = 0; i < count_headers; i++)
            {
                try { AliasRaport[i] = _alias[i]; }
                catch { AliasRaport[i] = "none"; }
            }
            AliasRaport = aliasRaport;
            //коллекция width
            var _width = new int[sc.Count()];
            for (int i = 0; i < sc.Count(); i++)
            {
                _width[i] = _dSColumnSettings//?????????????????
                    .Where(s => s.Id == sc[i].Id)
                    .Select(s => s.Width)
                    .FirstOrDefault();
            }
            for (int i = 0; i < count_headers; i++)
            {
                try { widthRaport[i] = _width[i]; }
                catch { widthRaport[i] = 100; }
            }
            WidthRaport = widthRaport;
        }
        private void initLabelsForm(ObservableCollection<DSCollumnSettingDTO> _dSCollumnSettingsForm)
        {
            var sr = _dSCollumnSettingsForm.ToArray();
            //коллекция visible
            bool[] _rowChecked = new bool[sr.Count()];
            for(int i=0; i < sr.Count(); i++)
            {
                _rowChecked[i] = dSCollumnSettingsForm
                    .Where(s => s.Id == sr[i].Id)
                    .Select(s => s.Visible)
                    .FirstOrDefault();
            }
            for(int i = 0; i< count_form_fields; i++)
            {
                try { RowChedForm[i] = _rowChecked[i]; }
                catch { RowChedForm[i] = false; }
            }
            RowChedForm = rowCheckedForm;
            //коллекция алиасов
            string[] _alias = new string[sr.Count()];
            for(int i = 0; i < sr.Count(); i++)
            {
                _alias[i] = dSCollumnSettingsForm
                    .Where(s => s.Id == sr[i].Id)
                    .Select(s => s.Alias)
                    .FirstOrDefault();
            }
            for(int i=0; i<count_form_fields; i++)
            {
                try { AliasForm[i] = _alias[i]; }
                catch { AliasForm[i] = "none"; }
            }
            AliasForm = aliasForm;
            //коллекция height
            int[] _height = new int[sr.Count()];
            for(int i = 0; i < sr.Count(); i++)
            {
                _height[i] = dSCollumnSettingsForm
                    .Where(s => s.Id == sr[i].Id)
                    .Select(s => s.Width)
                    .FirstOrDefault();
            }
            for( int i = 0;i < count_form_fields; i++ )
            {
                try { HeightForm[i] = _height[i]; }
                catch { HeightForm[i] = 0; }
            }
            HeightForm = heightForm;
        }
        private void UpdateAllVisitorFields(ObservableCollection<VisitorDTO> _visitors)
        {
            DesctopVisitors = new ObservableCollection<VisitorDTO>
                (_visitors
                .Where(v => v.CurrentStatus == "actual" ||
                v.CurrentStatus == "create" ||
                v.CurrentStatus == "edited"));
            CountActualVisitors = _visitors
                .Where(v => v.CurrentStatus == "actual")
                .Count();
            CountRegistredVisitors = _visitors
                .Where(v => v.CurrentStatus == "registered"
                || v.CurrentStatus == "actual")
                .Count();
            CountCreateVisitors = _visitors
                .Where(v => v.CurrentStatus == "create")
                .Count();
            CountAllVisitors = _visitors.Count();
            CountNotcameVisitors = countRegistredVisitors - countActualVisitors;
        }
        private void UpdateAllVisitorFields(ObservableCollection<VisitorDTO> _visitors, int selectedVisitorId)
        {
            DesctopVisitors = new ObservableCollection<VisitorDTO>
                (_visitors
                .Where(v => v.CurrentStatus == "actual" ||
                v.CurrentStatus == "create" ||
                v.CurrentStatus == "edited"));
  //          SelectDesctopVisitor = DesctopVisitors.Where(s => s.Id == selectedVisitorId).FirstOrDefault();
            CountActualVisitors = _visitors
                .Where(v => v.CurrentStatus == "actual")
                .Count();
            CountRegistredVisitors = _visitors
                .Where(v => v.CurrentStatus == "registered"
                || v.CurrentStatus == "actual")
                .Count();
            CountCreateVisitors = _visitors
                .Where(v => v.CurrentStatus == "create")
                .Count();
            CountAllVisitors = _visitors.Count();
            CountNotcameVisitors = countRegistredVisitors - countActualVisitors;
        }
        private void StartShowTime()
        {
            Task.Factory.StartNew(ChangeCurrentTime);
        }
        private void ChangeCurrentTime()
        {
            while (true)
            {
                CurrentTime = DateTime.Now.ToString();
                Thread.Sleep(1000);
            }
        }
        private void updateDataAsinc()
        {
            while (true)
            {
                if (dataMode == "Клиент службы баз данных")
                {
                    var _visitors = clientExecutor.GetClient().GetAllVisitors();

                    //                   VisitorDTO visitorDTO = new VisitorDTO();
                    Visitors = new ObservableCollection<VisitorDTO>();
                    foreach (var v in _visitors) { Visitors.Add(mapper.Map<VisitorDTO>(v)); }
                }
                else Visitors = new ObservableCollection<VisitorDTO>
                    (visitorRepositoryDTO.GetAllVisitors());
                UpdateAllVisitorFields(visitors);
                Thread.Sleep(10000);
            }
        }
        private void CheckSearchVisitor(string searchVisitor)
        {
            string search;
            if (dataMode != "Клиент службы баз данных")
            {
                if (searchVisitor == "") { find = false; }
                if (searchVisitor.Length <= 3)
                {
                    SearchVisitorBackGround = "White";
                   SearchVisitorForegraund = "Gray";
   //                 SearchVisitor = "";
                    SearchVisitorFontsize = 25;
                    find = false;
                }
                else
                {
                    if (find == false)
                    {
                        IEnumerable<VisitorDTO> _searchVisitors;
                        search = searchVisitor.ToUpper();
                        _searchVisitors = visitorRepositoryDTO
                            .GetAllVisitors()
                            .Where(v => v.Column9.ToUpper().Contains(search) ||
                                        v.Column8.ToUpper().Contains(search) ||
                                        v.Column11.ToUpper().Contains(search));
                        SearchVisitorCollection = new ObservableCollection<VisitorDTO>(_searchVisitors);

                        if (searchVisitorCollection != null)
                        {
                            SearchVisitorFontsize = 15;
                            SearchVisitorBackGround = "Blue";
                            SearchVisitorForegraund = "Black";
                            find = true;
                            var _searchVisitor = SearchVisitorCollection
                            .FirstOrDefault();
                            SearchVisitor = _searchVisitor.Column8.ToUpper() + " " +
                                            _searchVisitor.Column9.ToUpper() + " (" +
                                            _searchVisitor.Column11.ToUpper() + ")";
                            SelectedSearchVisitor = searchVisitorCollection.FirstOrDefault();
                        }
                    }
                    IsListFocuce = true;
                    find = true;
                }
            }
            else
            {
                IEnumerable<EX.Client.ServiceReference1.VisitorDTO> _searchVisitors;
                if (searchVisitor == "") { Find = false; }
                if (searchVisitor.Length <= 3)
                {
                    SearchVisitorBackGround = "White";
                }
                else
                {
                    if (find == false)
                    {
                        search = searchVisitor.ToUpper();
                        _searchVisitors = clientExecutor.GetClient()
                            .GetAllVisitors()
                            .Where(v => v.Column9.ToUpper().Contains(search) ||
                                        v.Column8.ToUpper().Contains(search) ||
                                        v.Column11.ToUpper().Contains(search));
                        SearchVisitorCollection = new ObservableCollection<VisitorDTO>();
                        foreach (var v in _searchVisitors)
                            SearchVisitorCollection.Add(mapper.Map<VisitorDTO>(v));

                        if (searchVisitorCollection != null)
                        {
                            SearchVisitorBackGround = "Blue";
                            SearchVisitorFontsize = 15;
                            SearchVisitorBackGround = "Blue";
                            SearchVisitorForegraund = "Black";
                            find = true;
                            var _searchVisitor = SearchVisitorCollection
                            .FirstOrDefault();
                            SearchVisitor = _searchVisitor.Column8.ToUpper() + " " +
                                            _searchVisitor.Column9.ToUpper() + " (" +
                                            _searchVisitor.Column11.ToUpper() + ")";
                            SelectedSearchVisitor = searchVisitorCollection.FirstOrDefault();
                        }
                    }
                }
            }
        }
        private void PrintVisitor(VisitorDTO visitor)
        {
            if(printDialog.ShowDialog() == true)
            {
                Run runName = new Run(visitor.Column8.ToUpper() + "\n" + visitor.Column9.ToUpper());
                Run runCompany = new Run(visitor.Column11.ToUpper());
                Paragraph paragraphName = new Paragraph(runName);
                Paragraph paragraphCompany = new Paragraph(runCompany);

                paragraphName.LineStackingStrategy = LineStackingStrategy.MaxHeight;
                paragraphName.FontFamily = new FontFamily("Verdana");
                paragraphName.TextAlignment = TextAlignment.Center;
                paragraphName.FontSize = 22;
                paragraphName.FontWeight = FontWeight.FromOpenTypeWeight(400);

                paragraphCompany.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                paragraphCompany.FontFamily = new FontFamily("Verdana");
                paragraphCompany.TextAlignment = TextAlignment.Center;
                paragraphCompany.FontSize = 22;
                paragraphCompany.FontWeight = FontWeight.FromOpenTypeWeight(900);


                FlowDocument flowDocument = new FlowDocument();
                flowDocument.Blocks.Add(paragraphName);
                flowDocument.Blocks.Add(paragraphCompany);
                flowDocument.PagePadding = new Thickness(30);
                flowDocument.PageWidth = printDialog.PrintableAreaWidth;
                flowDocument.Name = "FlowDoc";
                IDocumentPaginatorSource source = flowDocument;
                printDialog.PrintDocument(source.DocumentPaginator, "print Visitor - "
                    + visitor.Column8 + "\n" + visitor.Column9);
            }           
        }
        private void UpdateVistor(VisitorDTO selectedSearchVisitor)
        {
            if (dataMode != "Клиент службы баз данных")
            {
                visitorRepositoryDTO.AddOrUpdateVisitor(selectedSearchVisitor);
                statusRepository.Add(new StatusDTO
                {
                    Name = selectedSearchVisitor.CurrentStatus,
                    UserId = authorizedUser.Id,
                    VisitorId = selectedSearchVisitor.Id,
                    ActionTime = DateTime.Now.ToString()
                });
                UpdateAllVisitorFields(new ObservableCollection<VisitorDTO>
                    (visitorRepositoryDTO.GetAllVisitors()));
                SearchVisitor = "Введите информацию для поиска";
                SearchVisitorFontsize = 25;
                SearchVisitorBackGround = "White";
                SearchVisitorForegraund = "Gray";
                SearchVisitorCollection = new ObservableCollection<VisitorDTO>();
            }
            else
            {
                var client_visitor
                    = mapper.Map<EX.Client.ServiceReference1.VisitorDTO>
                    (selectedSearchVisitor);
                var client = clientExecutor.GetClient();
                client.AddOrUpdateVisitor(client_visitor);
                var _client_visitors = clientExecutor.GetClient().GetAllVisitors();
                var _visitors = _client_visitors.Select(s => mapper.Map<VisitorDTO>(s));
                clientExecutor.GetClient().AddStatus(mapper.Map
                        <EX.Client.ServiceReference1.StatusDTO>(new StatusDTO
                        {
                            Name = selectedSearchVisitor.CurrentStatus,
                            UserId = authorizedUser.Id,
                            VisitorId = selectedSearchVisitor.Id,
                            ActionTime = DateTime.Now.ToString()
                        }));
                UpdateAllVisitorFields(new ObservableCollection<VisitorDTO>(_visitors));
                SearchVisitor = "Введите информацию для поиска";
                SearchVisitorFontsize = 25;
                SearchVisitorBackGround = "White";
                SearchVisitorForegraund = "Gray";
                SearchVisitorCollection = new ObservableCollection<VisitorDTO>();
            }
        }
        private void InitColorInformation(VisitorDTO editDesctopVisitor)
        {
            switch (editDesctopVisitor.Column2)
            {
                case "PAID":
                    PaymentStatusPresenter = "PAID";
                    PaymentStatusFontsize = 36;
                    PaymentStatusForegraund = "Green";
                    break;
                case "UNPAID":
                    PaymentStatusPresenter = "UNPAID";
                    PaymentStatusFontsize = 36;
                    PaymentStatusForegraund = "RED";
                    break;
                case "FOC":
                    PaymentStatusPresenter = "FOC";
                    PaymentStatusFontsize = 36;
                    PaymentStatusForegraund = "Blue";
                    break;
                case "CXLD":
                    PaymentStatusPresenter = "CXLD";
                    PaymentStatusFontsize = 36;
                    PaymentStatusForegraund = "Blue";
                    break;
                default:
                    PaymentStatusPresenter = "Статус Оплаты";
                    PaymentStatusFontsize = 12;
                    PaymentStatusForegraund = "Gray";
                    break;
            }

            if (editDesctopVisitor.Column4.Equals("ORGANIZER"))
            {
                BagePresenter = "";
                BagePresenterBackGround = "Blue";
            }
            else if (editDesctopVisitor.Column4.Equals("SPEX GUESTS"))
            {
                BagePresenter = "";
                BagePresenterBackGround = "Blue";
            }
            else if (editDesctopVisitor.Column4.Equals("GUEST"))
            {
                BagePresenter = "";
                BagePresenterBackGround = "Blue";
            }
            else if (editDesctopVisitor.Column4.Equals("PRESS PASS"))
            {
                BagePresenter = "";
                BagePresenterBackGround = "Yellow";
            }
            else if (editDesctopVisitor.Column4.Equals("MEDIA"))
            {
                BagePresenter = "";
                BagePresenterBackGround = "Blue";
            }
            else if (editDesctopVisitor.Column4.Equals("MEDIA"))
            {
                BagePresenter = "";
                BagePresenterBackGround = "Blue";
            }
            else if (editDesctopVisitor.Column4.Equals("SPEAKER"))
            {
                BagePresenter = "";
                BagePresenterBackGround = "Red";
            }
            else if (editDesctopVisitor.Column4.Trim().Equals("3 DAY PACKAGE"))
            {
                BagePresenter = "";
                BagePresenterBackGround = "#FF00BFFF";
            }
            else if (editDesctopVisitor.Column4.Trim().Equals("2 DAY PACKAGE"))
            {
                BagePresenter = "";
                BagePresenterBackGround = "#FF9400D3";
            }
            else if (editDesctopVisitor.Column4.Trim().Equals("FOCUS DAY"))
            {
                BagePresenter = "";
                BagePresenterBackGround = "Green";
            }
        }
        private void InitRaportInformation(string choce)
        {
            if (dataMode != "Клиент службы баз данных")
            {
                switch (choce)
                {
                    case "registred":
                        if (selectedRaportRegistredCategory == null)
                        {
                            RaportRegisteredVisitors = new ObservableCollection<VisitorDTO>
                                 (visitorRepositoryDTO.GetAllVisitors()
                                 .Where(s => s.CurrentStatus == "registered" ||
                                        s.CurrentStatus == "actual"));
                        }
                        else
                        {
                            RaportRegisteredVisitors = new ObservableCollection<VisitorDTO>
                                 (visitorRepositoryDTO.GetAllVisitors()
                                  .Where(s => s.Column4 == selectedRaportRegistredCategory)
                                  .Where(s => s.CurrentStatus == "registered" ||
                                         s.CurrentStatus == "actual"));
                        }
                        if (selectedRaportRegistredPaymentStatus == null)
                        {
                            RaportRegisteredVisitors = raportRegisteredVisitors;
                        }
                        else
                        {
                            RaportRegisteredVisitors = new ObservableCollection<VisitorDTO>
                                (raportRegisteredVisitors
                                .Where(s => s.Column2 == selectedRaportRegistredPaymentStatus)
                                .Where(s => s.CurrentStatus == "registered" ||
                                         s.CurrentStatus == "actual"));
                        }
                        if (selectedRaportRegistredChanged == null)
                        {
                            RaportRegisteredVisitors = raportRegisteredVisitors;
                        }
                        else
                        {
                            RaportRegisteredVisitors = new ObservableCollection<VisitorDTO>
                                (raportRegisteredVisitors
                                .Where(s => s.Column6 == selectedRaportRegistredChanged)
                                .Where(s => s.CurrentStatus == "registered" ||
                                       s.CurrentStatus == "actual"));
                        }
                        CountRaportRegistredVisitors = raportRegisteredVisitors.Count();
                        break;
                    case "actual":
                        if (selectedRaportActualCategory == null)
                        {
                            RaportActualVisitors = new ObservableCollection<VisitorDTO>
                                 (visitorRepositoryDTO.GetAllVisitors()
                                 .Where(s => s.CurrentStatus == "actual"));
                        }
                        else
                        {
                            RaportActualVisitors = new ObservableCollection<VisitorDTO>
                                 (visitorRepositoryDTO.GetAllVisitors()
                                  .Where(s => s.Column4 == selectedRaportActualCategory)
                                  .Where(s => s.CurrentStatus == "actual"));
                        }
                        if (selectedRaportActualPaymentStatus == null)
                        {
                            RaportActualVisitors = raportActualVisitors;
                        }
                        else
                        {
                            RaportActualVisitors = new ObservableCollection<VisitorDTO>
                                (raportActualVisitors
                                .Where(s => s.Column2 == selectedRaportActualPaymentStatus)
                                .Where(s => s.CurrentStatus == "actual"));
                        }
                        if (selectedRaportActualChanged == null)
                        {
                            RaportActualVisitors = raportActualVisitors;
                        }
                        else
                        {
                            RaportActualVisitors = new ObservableCollection<VisitorDTO>
                                (raportActualVisitors
                                .Where(s => s.Column6 == selectedRaportActualChanged)
                                .Where(s => s.CurrentStatus == "actual"));
                        }
                        CountRaportActualVisitors = raportActualVisitors.Count();
                        break;
                    case "unactual":
                        if (selectedRaportUnActualCategory == null)
                        {
                            RaportUnActualVisitors = new ObservableCollection<VisitorDTO>
                                 (visitorRepositoryDTO.GetAllVisitors()
                                 .Where(s => s.CurrentStatus == "registered"));
                        }
                        else
                        {
                            RaportUnActualVisitors = new ObservableCollection<VisitorDTO>
                                 (visitorRepositoryDTO.GetAllVisitors()
                                  .Where(s => s.Column4 == selectedRaportUnActualCategory)
                                  .Where(s => s.CurrentStatus == "registered"));
                        }
                        if (selectedRaportUnActualPaymentStatus == null)
                        {
                            RaportUnActualVisitors = raportUnActualVisitors;
                        }
                        else
                        {
                            RaportUnActualVisitors = new ObservableCollection<VisitorDTO>
                                (raportUnActualVisitors
                                .Where(s => s.Column2 == selectedRaportUnActualPaymentStatus)
                                .Where(s => s.CurrentStatus == "registered"));
                        }
                        if (selectedRaportUnActualChanged == null)
                        {
                            RaportUnActualVisitors = raportUnActualVisitors;
                        }
                        else
                        {
                            RaportUnActualVisitors = new ObservableCollection<VisitorDTO>
                                (raportUnActualVisitors
                                .Where(s => s.Column6 == selectedRaportUnActualChanged)
                                .Where(s => s.CurrentStatus == "registered"));
                        }
                        CountRaportUnActualVisitors = raportUnActualVisitors.Count();
                        break;
                    case "create":
                        if (selectedRaportCreateCategory == null)
                        {
                            RaportCreateVisitors = new ObservableCollection<VisitorDTO>
                                 (visitorRepositoryDTO.GetAllVisitors()
                                 .Where(s => s.CurrentStatus == "create"));
                        }
                        else
                        {
                            RaportCreateVisitors = new ObservableCollection<VisitorDTO>
                                 (visitorRepositoryDTO.GetAllVisitors()
                                  .Where(s => s.Column4 == selectedRaportCreateCategory)
                                  .Where(s => s.CurrentStatus == "create"));
                        }
                        if (selectedRaportCreatePaymentStatus == null)
                        {
                            RaportCreateVisitors = raportCreateVisitors;
                        }
                        else
                        {
                            RaportCreateVisitors = new ObservableCollection<VisitorDTO>
                                (raportCreateVisitors
                                .Where(s => s.Column2 == selectedRaportCreatePaymentStatus)
                                .Where(s => s.CurrentStatus == "create"));
                        }
                        if (selectedRaportCreateChanged == null)
                        {
                            RaportCreateVisitors = raportCreateVisitors;
                        }
                        else
                        {
                            RaportCreateVisitors = new ObservableCollection<VisitorDTO>
                                (raportCreateVisitors
                                .Where(s => s.Column6 == selectedRaportCreateChanged)
                                .Where(s => s.CurrentStatus == "create"));
                        }
                        CountRaportCreateVisitors = raportCreateVisitors.Count();
                        break;
                    case "all":
                        if (selectedRaportAllCategory == null)
                        {
                            RaportAllVisitors = new ObservableCollection<VisitorDTO>
                                 (visitorRepositoryDTO.GetAllVisitors());
                        }
                        else
                        {
                            RaportAllVisitors = new ObservableCollection<VisitorDTO>
                                 (visitorRepositoryDTO.GetAllVisitors()
                                  .Where(s => s.Column4 == selectedRaportAllCategory));
                        }
                        if (selectedRaportAllPaymentStatus == null)
                        {
                            RaportAllVisitors = raportAllVisitors;
                        }
                        else
                        {
                            RaportAllVisitors = new ObservableCollection<VisitorDTO>
                                (raportAllVisitors
                                .Where(s => s.Column2 == selectedRaportAllPaymentStatus));
                        }
                        if (selectedRaportAllChanged == null)
                        {
                            RaportAllVisitors = raportAllVisitors;
                        }
                        else
                        {
                            RaportAllVisitors = new ObservableCollection<VisitorDTO>
                                (raportAllVisitors
                                .Where(s => s.Column6 == selectedRaportCreateChanged));
                        }
                        CountRaportAllVisitors = raportAllVisitors.Count();
                        break;

                }
            }
            else
            {
                var client = clientExecutor.GetClient();
                var _client_visitors = clientExecutor.GetClient().GetAllVisitors();
                var _visitors = _client_visitors.Select(s => mapper.Map<VisitorDTO>(s));
                switch (choce)
                {
                    case "registred":
                        if (selectedRaportRegistredCategory == null)
                        {
                            _visitors = _visitors
                                    .Where(s => s.CurrentStatus == "registered" ||
                                      s.CurrentStatus == "actual");
                        }
                        else
                        {
                            _visitors = _visitors
                                  .Where(s => s.Column4 == selectedRaportRegistredCategory)
                                  .Where(s => s.CurrentStatus == "registered" ||
                                         s.CurrentStatus == "actual");
                        }
                        RaportRegisteredVisitors = new ObservableCollection<VisitorDTO>(_visitors);
                        if (selectedRaportRegistredPaymentStatus == null)
                        {
                            RaportRegisteredVisitors = raportRegisteredVisitors;
                        }
                        else
                        {
                            RaportRegisteredVisitors = new ObservableCollection<VisitorDTO>
                                (raportRegisteredVisitors
                                .Where(s => s.Column2 == selectedRaportRegistredPaymentStatus)
                                .Where(s => s.CurrentStatus == "registered" ||
                                         s.CurrentStatus == "actual"));
                        }
                        if (selectedRaportRegistredChanged == null)
                        {
                            RaportRegisteredVisitors = raportRegisteredVisitors;
                        }
                        else
                        {
                            RaportRegisteredVisitors = new ObservableCollection<VisitorDTO>
                                (raportRegisteredVisitors
                                .Where(s => s.Column6 == selectedRaportRegistredChanged)
                                .Where(s => s.CurrentStatus == "registered" ||
                                       s.CurrentStatus == "actual"));
                        }
                        CountRaportRegistredVisitors = raportRegisteredVisitors.Count();
                        break;
                    case "actual":
                        if (selectedRaportActualCategory == null)
                        {
                            _visitors = _visitors
                                    .Where(s => s.CurrentStatus == "actual");
                        }
                        else
                        {
                            _visitors = _visitors
                                  .Where(s => s.Column4 == selectedRaportActualCategory)
                                  .Where(s => s.CurrentStatus == "actual");
                        }
                        RaportActualVisitors = new ObservableCollection<VisitorDTO>(_visitors);
                        if (selectedRaportActualPaymentStatus == null)
                        {
                            RaportActualVisitors = raportActualVisitors;
                        }
                        else
                        {
                            RaportActualVisitors = new ObservableCollection<VisitorDTO>
                                (raportActualVisitors
                                .Where(s => s.Column2 == selectedRaportActualPaymentStatus)
                                .Where(s => s.CurrentStatus == "actual"));
                        }
                        if (selectedRaportActualChanged == null)
                        {
                            RaportActualVisitors = raportActualVisitors;
                        }
                        else
                        {
                            RaportActualVisitors = new ObservableCollection<VisitorDTO>
                                (raportActualVisitors
                                .Where(s => s.Column6 == selectedRaportActualChanged)
                                .Where(s => s.CurrentStatus == "actual"));
                        }
                        CountRaportActualVisitors = raportActualVisitors.Count();
                        break;
                    case "unactual":
                        if (selectedRaportUnActualCategory == null)
                        {
                            _visitors = _visitors
                                    .Where(s => s.CurrentStatus == "registered");
                        }
                        else
                        {
                            _visitors = _visitors
                                  .Where(s => s.Column4 == selectedRaportUnActualCategory)
                                  .Where(s => s.CurrentStatus == "registered");
                        }
                        RaportUnActualVisitors = new ObservableCollection<VisitorDTO>(_visitors);
                        if (selectedRaportUnActualPaymentStatus == null)
                        {
                            RaportUnActualVisitors = raportUnActualVisitors;
                        }
                        else
                        {
                            RaportUnActualVisitors = new ObservableCollection<VisitorDTO>
                                (raportUnActualVisitors
                                .Where(s => s.Column2 == selectedRaportUnActualPaymentStatus)
                                .Where(s => s.CurrentStatus == "registered"));
                        }
                        if (selectedRaportUnActualChanged == null)
                        {
                            RaportUnActualVisitors = raportUnActualVisitors;
                        }
                        else
                        {
                            RaportUnActualVisitors = new ObservableCollection<VisitorDTO>
                                (raportUnActualVisitors
                                .Where(s => s.Column6 == selectedRaportUnActualChanged)
                                .Where(s => s.CurrentStatus == "registered"));
                        }
                        CountRaportUnActualVisitors = raportUnActualVisitors.Count();
                        break;
                    case "create":
                        if (selectedRaportCreateCategory == null)
                        {
                            _visitors = _visitors
                                    .Where(s => s.CurrentStatus == "create");
                        }
                        else
                        {
                            _visitors = _visitors
                                  .Where(s => s.Column4 == selectedRaportCreateCategory)
                                  .Where(s => s.CurrentStatus == "create");
                        }
                        RaportCreateVisitors = new ObservableCollection<VisitorDTO>(_visitors);
                        if (selectedRaportCreatePaymentStatus == null)
                        {
                            RaportCreateVisitors = raportCreateVisitors;
                        }
                        else
                        {
                            RaportCreateVisitors = new ObservableCollection<VisitorDTO>
                                (raportCreateVisitors
                                .Where(s => s.Column2 == selectedRaportCreatePaymentStatus)
                                .Where(s => s.CurrentStatus == "create"));
                        }
                        if (selectedRaportCreateChanged == null)
                        {
                            RaportCreateVisitors = raportCreateVisitors;
                        }
                        else
                        {
                            RaportCreateVisitors = new ObservableCollection<VisitorDTO>
                                (raportCreateVisitors
                                .Where(s => s.Column6 == selectedRaportCreateChanged)
                                .Where(s => s.CurrentStatus == "create"));
                        }
                        CountRaportCreateVisitors = raportCreateVisitors.Count();
                        break;
                    case "all":
                        if (selectedRaportAllCategory == null)
                        {
                         //   _visitors = _visitors;
                        }
                        else
                        {
                            _visitors = _visitors
                                  .Where(s => s.Column4 == selectedRaportAllCategory);
                        }
                        RaportAllVisitors = new ObservableCollection<VisitorDTO>(_visitors);
                        if (selectedRaportAllPaymentStatus == null)
                        {
                            RaportAllVisitors = raportAllVisitors;
                        }
                        else
                        {
                            RaportAllVisitors = new ObservableCollection<VisitorDTO>
                                (raportAllVisitors
                                .Where(s => s.Column2 == selectedRaportAllPaymentStatus));
                        }
                        if (selectedRaportAllChanged == null)
                        {
                            RaportAllVisitors = raportAllVisitors;
                        }
                        else
                        {
                            RaportAllVisitors = new ObservableCollection<VisitorDTO>
                                (raportAllVisitors
                                .Where(s => s.Column6 == selectedRaportAllChanged));
                        }
                        CountRaportAllVisitors = raportAllVisitors.Count();
                        break;
                }
            }
        }
        #endregion
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
