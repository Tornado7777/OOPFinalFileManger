using OOPFinalFM.Data;
using OOPFinalFM.Core;
using OOPFinalFM.Configuration;
using System.IO;

namespace OOPFinalFM.Handler
{
    public class InputString
    {

        //метод обработки введенной комманды
        public static CommandEntity InputCommand(CommandEntity commandEntity)
        {
            var conf = new ConfigConsole(); //для обращения к конфигурационному файлу создаем переменную по классу
            string[] command; // переменная для разбивки строки на отдельные компоненты
            if (commandEntity.CurCommandType == CommandEntity.CommandType.None && commandEntity.InputCommand[0] != null)
            {
                //разбиваем строку на состовляющие разделенные пробелом
                command = commandEntity.InputCommand[0].Split(' ');
                if (command.Length>0) command.CopyTo(commandEntity.InputCommand, 0); 
            }
            else command = commandEntity.InputCommand;
            //проверяю на ссоответсвие коммандам заданным
            //переделать на  swith case
            if (command[0].Equals("ls"))
            {
                if ((command.Length >= 2) && Directory.Exists(command[1]))
                {
                    conf.LastDir = command[1]; //делается запись в конфигурайионный файл
                    commandEntity.ResultCommand = Command.Listing(command[1], conf.MaxRowsDir); //обращаемя к функции чтения содержания катлога
                    commandEntity.CurCommandType = CommandEntity.CommandType.Listing;
                    commandEntity.InputCommand[2] = "0";
                }
                else
                {
                    commandEntity.ResultCommand = new string[] { "Каталог задан неверно или не существует" };
                }
            }
            else if ((command.Length == 3) && command[0].Equals("cp"))
            {
                commandEntity.ResultCommand = new string[2];
                commandEntity.ResultCommand[0] = "cp " + command[1] + command[2];
                commandEntity.ResultCommand[1] = Command.Copy(command[1], command[2]); //обращаемся к функции копирования файлов и каталогов (нужно доработать)
                commandEntity.CurCommandType = CommandEntity.CommandType.Copy;
            }
            else if ((command.Length > 1) && command[0].Equals("rm"))
            {
                commandEntity.ResultCommand = new string[2];
                commandEntity.ResultCommand[0] = "rm " + command[1];
                commandEntity.ResultCommand[1] = Command.Remove(command[1]); // обращаемя к функции удаления файлов и каталогов
                commandEntity.CurCommandType = CommandEntity.CommandType.Remove;
            }
            else if ((command.Length > 1) && command[0].Equals("file"))
            {
                commandEntity.ResultCommand = new string[5];
                commandEntity.ResultCommand[0] = "file " + command[1];
                string[] resultFileInfo = Command.FileInfo(command[1]); //обращение к функии получения информации о файле
                for (int i = 0; i< resultFileInfo.Length; i++) commandEntity.ResultCommand[i+1] = resultFileInfo[i];
                commandEntity.CurCommandType = CommandEntity.CommandType.FileInfo;
            }
            else if ((command.Length > 1) && command[0].Equals("dir"))
            {
                commandEntity.ResultCommand = new string[5];
                commandEntity.ResultCommand[0] = "dir " + command[1];
                string[] resultDirInfo = Command.DirInfo(command[1]); //обращение к функии получения информации о каталоге
                for (int i = 0; i < resultDirInfo.Length; i++) commandEntity.ResultCommand[i + 1] = resultDirInfo[i];
                commandEntity.CurCommandType = CommandEntity.CommandType.DirInfo;
            }
            else if ((command.Length > 0) && command[0].Equals("help"))
            {
                commandEntity.CurCommandType = CommandEntity.CommandType.Help;
                commandEntity.ResultCommand =new string[]  
                {
                    "help",
                    @"Просмотр файловой структуры комманда: ls c:\project",
                    @"Копирование файлов, каталогов: cp c:\project\example.txt c:\project\test\",
                    @"Поддержка удаление файлов, каталогов: rm c:\project\example.txt",
                    @"Получение информации о размерах, системных атрибутов файла, каталога file c:\project\example.txt, dir c:\project"
                };
            }
                return commandEntity;

        }

    }
}
