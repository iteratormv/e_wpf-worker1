using AutoMapper;
using EX.Client;
using EX.Model.DbLayer.Settings;
using EX.Model.DTO;
using EX.Model.DTO.Setting;
using EX.Model.Infrastructure;
using EX.Model.Repositories;
using EX.Model.Repositories.Administration;
using EX.Model.Repositories.Setting;
using EX.Service;
using EX.ViewModel.Infrastructure;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
            set { dSCollumnSettingsRaport = value;
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
                OnPropertyChanged(nameof(DSCollumnSettingsForm));
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
        DSCollumnSettingDTO selectedCollumnSettingDesctop;
        public DisplaySettingDTO SelectedDisplaySettingDesctop
        {
            get { return selectedDisplaySettingDesctop; }
            set
            {
                selectedDisplaySettingDesctop = value;
                OnPropertyChanged(nameof(SelectedDisplaySettingDesctop));
            }
        }
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
        #endregion
        #endregion
        #region Context for File
        #region Repository for File
        VisitorRepositoryDTO visitorRepositoryDTO;
        #endregion
        #region Collection for File
        ObservableCollection<VisitorDTO> visitors;
        public ObservableCollection<VisitorDTO> Visitors
        {
            get { return visitors; }
            set { visitors = value; OnPropertyChanged(nameof(Visitors)); }
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
        public RelayCommand AddDataFromFileToDatabase { get { return addDataFromFileToDatabase; } }
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
        #endregion

        public MainVM()
        {
            DataMode = "Локальная база данных";
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
            AuthorizedUser = new UserDTO();
            #endregion
            #region Init value for Settings
            displaySettingDTORepository = new DisplaySettingDTORepository();
            dSCollumnSettingDTORepository = new DSCollumnSettingDTORepository();
            #region Raport
            displaySettingsRaport = new ObservableCollection<DisplaySettingDTO>
                (displaySettingDTORepository.GetAllDisplaySettingDTOs().
                Where(s=>s.Intendant == "raport"));
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
                Where(s=>s.Intendant == "raport"));

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
                    Width = 100,
                    Visible = true,
                    IsSelected = true,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "FirstName",
                    Alias = "Имя",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "LastName",
                    Alias = "Фамилия",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Сompany",
                    Alias = "Компания",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Jobtitle",
                    Alias = "Должность",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "desctop",
                    DisplaySettingId = defaultDisplayDesctopSettingId
                });
            }
            updateAllSettingsDesctop("desctop");
            #endregion
            #region Form
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
                .Where(s=>s.DisplaySettingId == defaultDisplayFormSettingId)
                /*.Where(s => s.Intendant == "form")*/);
            if (dSCollumnSettingsForm.Count() == 0)
            {
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Id",
                    Alias = "№",
                    Width = 100,
                    Visible = true,
                    IsSelected = true,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "ActualisationDate",
                    Alias = "Дата актуализации",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "FirstName",
                    Alias = "Имя",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "LastName",
                    Alias = "Фамилия",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Сompany",
                    Alias = "Компания",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
                dSCollumnSettingDTORepository.AddOrUpdate(new DSCollumnSettingDTO
                {
                    Name = "Jobtitle",
                    Alias = "Должность",
                    Width = 100,
                    Visible = true,
                    IsSelected = false,
                    Intendant = "form",
                    DisplaySettingId = defaultDisplayFormSettingId
                });
            }
            updateAllSettingsForm("form");
            #endregion
            #endregion
            #region Init value for File
            visitorRepositoryDTO = new VisitorRepositoryDTO();
            visitorRepositoryDTO.progressChanged += ProgressChanged;
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
            });
            mapper = config.CreateMapper();
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
            }, c=> displaySettingsRaport.Count() > 1);
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
            }, c => dSCollumnSettingsRaport.Count() < 17);
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
            }, c => dSCollumnSettingsRaport.Count() >1);
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
                    Where(s=>s.Intendant == _intendant).
                    Where(s => s.DisplaySettingId == oldSelectedDisplaySetting.Id).
                    Where(s => s.IsSelected == true).
                    FirstOrDefault();
                oldSelectedDisplaySetting.IsSelected = false;
                oldSelectedCollumnSetting.IsSelected = false;
                var newSelectedDisplaySetting = displaySettingDTORepository.GetAllDisplaySettingDTOs().
                    Where(s => s.Intendant == _intendant).
                    Where(s => s.Id == selectedDisplaySettingDesctop.Id ).
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
            }, c => dSCollumnSettingsDesctop.Count() < 17);
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
                Where(s=>s.Intendant == _intendant).
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
                Where(s=>s.Intendant == _intendant).
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
                    Where(s=>s.Intendant == _intendant).
                    Where(s => s.IsSelected == true).
                    Select(s => s.Id).FirstOrDefault();
                addNewCollumn(_intendant, dsid, dsid);
                updateAllSettingsForm(_intendant);
            }, c => dSCollumnSettingsForm.Count() < 17);
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
            #endregion
            #region Implementation command for File
            addDataFromFileToDatabase = new RelayCommand(c =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                _ProgressBar = new Progress_Bar
                {
                    Visible = true, Progress = 10, Status = "Start"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    Task.Factory.StartNew(() =>
                    {
                        visitorRepositoryDTO.InitRepositoryFromFole
                        (openFileDialog.FileName);
                        Visitors = new ObservableCollection<VisitorDTO>
                        (visitorRepositoryDTO.GetAllVisitors());
                        _ProgressBar.Status = "All data added to database";
                        _ProgressBar.Progress = 0;
                        Thread.Sleep(3000);
                        _ProgressBar.Visible = false;
                    });
                }
            });
            #endregion
            #region Implementation command for Service
            changeDataMode = new RelayCommand(c =>
            {
                DataMode = (string)c;
                if(DataMode == "Клиент службы баз данных")
                {
                    var _visitors = clientExecutor.GetClient().GetAllVisitors();

                    VisitorDTO visitorDTO = new VisitorDTO();
                    Visitors = new ObservableCollection<VisitorDTO>();
                    foreach (var v in _visitors) { Visitors.Add(mapper.Map<VisitorDTO>(v)); }
                }
            });
            startServer = new RelayCommand(c =>
            {
                IsEnabledDataModeChecker = false;
                Task.Factory.StartNew(serviceExecutor.Start);
                Thread.Sleep(200);
            }, c => 
                 DataMode == "Сервер службы баз данных"&&ServerStatus!= "listerning....");
            stopServer = new RelayCommand(c =>
            {
                IsEnabledDataModeChecker = true;
                Task.Factory.StartNew(serviceExecutor.Stop);
                Thread.Sleep(200);
            }, c => ServerStatus == "listerning....");
            #endregion
        }

        #region Implemetation methods
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
        private void ProgressChanged(Progress_Bar progress)
        {
            _ProgressBar.Progress = progress.Progress;
            _ProgressBar.Status = progress.Status;
            _ProgressBar.Visible = progress.Visible;
 //           Thread.Sleep(25);
        }
        private void StatusChanged(string obj)
        {
            ServerStatus = (string)obj;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
