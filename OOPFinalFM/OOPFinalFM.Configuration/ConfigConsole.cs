using System;
using System.Configuration;

namespace OOPFinalFM.Configuration
{
    public class ConfigConsole
    {
        private int _widthConsole;
        private int _highConsole;
        private int _dirAreaRows;
        private int _dataAreaRows;
        private int _maxRowsDir;
        private string _lastDir;

        public int WidthConsole { get { return _widthConsole; } }
        public int HighConsole { get { return _highConsole; } }
        public int DirAreaRows { get { return _dirAreaRows; } }
        public int DataAreaRows { get { return _dataAreaRows; } }
        public int MaxRowsDir { get { return _maxRowsDir; } }
        public string LastDir
        {
            get { return _lastDir; }
            set
            {
                AddUpdateAppSettings("Last_Dir", value);
                _lastDir = value;
            }
        }
        public ConfigConsole()
        {
            _widthConsole = int.Parse(ReadSetting("Width_Console_Symbol"));
            _dirAreaRows = int.Parse(ReadSetting("Dir_Area_Rows"));
            _dataAreaRows = int.Parse(ReadSetting("Data_Area_Rows"));
            _highConsole = int.Parse(ReadSetting("High_Console"));
            _maxRowsDir = int.Parse(ReadSetting("Max_Rows_Dir"));
            _lastDir = ReadSetting("Last_Dir");

        }
        public ConfigConsole(int widthConsoleSymbol,  int dirAreaRows,int dataAreaRows,int maxRowsDir, string lastDir)
        {
            _widthConsole = widthConsoleSymbol;
            AddUpdateAppSettings("Width_Console_Symbol", _widthConsole.ToString());
            _dataAreaRows = dataAreaRows;
            AddUpdateAppSettings("Data_Area_Rows", _dataAreaRows.ToString());
            _dirAreaRows = dirAreaRows;
            AddUpdateAppSettings("Dir_Area_Rows", _dirAreaRows.ToString());
            _highConsole = dirAreaRows + dataAreaRows + 4; //высота консоли равна сумме строк области отображения каталогов и файлов плюс область отбражения данных + рамка + 1 строка для ввыдения комманд
            AddUpdateAppSettings("High_Console", _highConsole.ToString());
            _lastDir = lastDir;
            AddUpdateAppSettings("Max_Rows_Dir", _maxRowsDir.ToString());
            _maxRowsDir = maxRowsDir;
            AddUpdateAppSettings("Last_Dir", _lastDir);
        }


        private string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return null;
            }
        }
        private void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
