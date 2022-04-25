using System;
using OOPFinalFM.Configuration;
using OOPFinalFM.Data;
using OOPFinalFM.Handler;

namespace OOPFinalFM.UI
{
    public class ConsoleUI
    {

        
        private static ConsoleUpdateUI _consoleUpdate = new ConsoleUpdateUI();
        private static int _countCommand = 10; //кол-во хронимых введенных комманд
        private static string[] _inputCommands = new string[_countCommand]; //массив последних введеных 10 комманд
        private static ConfigConsole _conf = new ConfigConsole(); //читаю из файла конфигурации настройки

        public static void StartConsoleUI()
        {
            
            Console.SetWindowSize(_conf.WidthConsole,_conf.HighConsole);
            _consoleUpdate.ShowFrame();
            //получаю путь до последнего просмотренного каталога и записываю как комманду
            string[] inputCommand = { "ls", _conf.LastDir, "0" }; 
            CommandEntity commandEntityListing = new CommandEntity(inputCommand, CommandEntity.CommandType.Listing); //создаю сущность комманды
            //получаю содержимое каталога
            commandEntityListing = InputString.InputCommand(commandEntityListing);
            inputCommand[0]="help";
            
            CommandEntity commandEntityHelp = new CommandEntity(inputCommand, CommandEntity.CommandType.Listing);
            commandEntityHelp = InputString.InputCommand(commandEntityHelp);

            if (commandEntityListing.CurCommandType == CommandEntity.CommandType.Listing)
            {
                ShowDirPage(commandEntityListing);
            }
            _consoleUpdate.UpdateDataArea(commandEntityHelp.ResultCommand);
            ConsoleUI.waitEnterCommandConsoleUI(commandEntityListing);
        }
        /// <summary>
        /// Ожидает нажатия клавиш, реагирует в соответствии с нажатыми
        /// </summary>
        /// <param name="commandEntityListing"></param>
        /// <returns></returns>
        private static void waitEnterCommandConsoleUI(CommandEntity commandEntityListing)
        {
            
            do
            {
                CommandEntity commandEntityNone = new CommandEntity();
                var inKey = Console.ReadKey(true); // ожидаем ввода следующей клавиши
                int numPage = Int32.Parse(commandEntityListing.InputCommand[2]);
                switch (inKey.Key)
                {
                    //пагинация каталога
                    case ConsoleKey.PageUp:
                        numPage--;
                        if (numPage < 0) numPage = 0;
                        commandEntityListing.InputCommand[2] = numPage.ToString();
                        ShowDirPage(commandEntityListing);
                        break;
                    case ConsoleKey.PageDown:
                        numPage++;
                        if (commandEntityListing.ResultCommand.Length < _conf.DirAreaRows * numPage || _conf.DirAreaRows * numPage > _conf.MaxRowsDir + 1 ) numPage--;
                        commandEntityListing.InputCommand[2] = numPage.ToString();
                        ShowDirPage(commandEntityListing);
                        break;

                    case ConsoleKey.Enter:
                        commandEntityNone.InputCommand[0] = _inputCommands[_countCommand - 1];
                       commandEntityNone = InputString.InputCommand(commandEntityNone);//отправляю на анализ и исполнение комманды
                        ShowResultCommand(commandEntityNone);
                        break;
                }
                _inputCommands = _consoleUpdate.UpdateInputArea(inKey, _countCommand, _inputCommands);

            }
            while (_inputCommands[_countCommand - 1] != "exit");
        }

        /// <summary>
        /// По типу комманды определяет распределяет данные для отображения
        /// </summary>
        /// <param name="commandEntity"></param>
        private static void ShowResultCommand(CommandEntity commandEntity)
        {
            if (commandEntity.CurCommandType == CommandEntity.CommandType.Listing)
            {
                ShowDirPage(commandEntity);
            }
            else
            {
                if (commandEntity.CurCommandType != CommandEntity.CommandType.None) _consoleUpdate.UpdateDataArea(commandEntity.ResultCommand);
            }
            
        }

        /// <summary>
        /// Отправляет на отображенние соответствующую страницу пагинации
        /// </summary>
        /// <param name="commandEntityListing"></param>
        private static void ShowDirPage(CommandEntity commandEntityListing)
        {
            int numPage = Int32.Parse(commandEntityListing.InputCommand[2]);
            string[] onePage = new string[_conf.DirAreaRows + 1];
            onePage[0] = commandEntityListing.InputCommand[0] + commandEntityListing.InputCommand[1];
            int sourceIndex = _conf.DirAreaRows * numPage; //начиная с заданного индекса источника, начинается копирование массива
            if (sourceIndex + _conf.DirAreaRows < _conf.MaxRowsDir)
            {
                Array.Copy(commandEntityListing.ResultCommand, _conf.DirAreaRows * numPage, onePage, 1, _conf.DirAreaRows);
                _consoleUpdate.UpdateDirArea(onePage, commandEntityListing.InputCommand[1]);
            }
            else
            {
                if(sourceIndex < _conf.MaxRowsDir)
                {
                    int lenghtCopy = _conf.MaxRowsDir - sourceIndex;
                    Array.Copy(commandEntityListing.ResultCommand, _conf.DirAreaRows * numPage, onePage, 1, lenghtCopy);
                    _consoleUpdate.UpdateDirArea(onePage, commandEntityListing.InputCommand[1]);
                }
               
            }
            
            
        }
    }
}
