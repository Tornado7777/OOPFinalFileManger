using System;
using System.Collections.Generic;
using OOPFinalFM.Configuration;


namespace OOPFinalFM.UI
{
    internal class ConsoleUpdateUI : IUpdateUI
    {
        private static ConfigConsole _conf = new ConfigConsole(); //создаем поле класса конфигурации
        private static char _lineHorizont = '═';
        //private static string[] _inputLastRows = new string[10];

        /// <summary>
        /// Метод рисует рамку 
        /// </summary>
        public void ShowFrame()
        {
            //создаю словарь с символам для оформления
            Dictionary<string, char> frame = new Dictionary<string, char>();
            frame.Add("lineHorizont", '═');
            frame.Add("lineVert", '║');
            frame.Add("lineVertHorLeft", '╠');
            frame.Add("lineVertHorRight", '╣');
            frame.Add("angleUpLeft", '╔');
            frame.Add("angleUpRight", '╗');
            frame.Add("angleDownLeft", '╚');
            frame.Add("angleDownRight", '╝');
            frame.Add("space", ' ');
            Console.Clear();
            //рисую верхнюю часть рамки
            Console.SetCursorPosition(0, 0);
            Console.Write(frame["angleUpLeft"]);
            for (int i = 1; i < _conf.WidthConsole - 1; i++) Console.Write(frame["lineHorizont"]);
            Console.Write(frame["angleUpRight"]);

            //рисую часть рамки отображения каталогов и файлов
            for (int i = 1; i <= _conf.DirAreaRows; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(frame["lineVert"]);
                Console.SetCursorPosition(_conf.WidthConsole - 1, i);
                Console.Write(frame["lineVert"]);
            }

            //рисую перемычку между двумя областями
            Console.SetCursorPosition(0, _conf.DirAreaRows + 1);
            Console.Write(frame["lineVertHorLeft"]);
            for (int i = 1; i < _conf.WidthConsole - 1; i++) Console.Write(frame["lineHorizont"]);
            Console.Write(frame["lineVertHorRight"]);

            //рисую часть рамки отображения данных
            for (int i = 1; i <= _conf.DataAreaRows; i++)
            {
                Console.SetCursorPosition(0, _conf.DirAreaRows + 1 + i);
                Console.Write(frame["lineVert"]);
                Console.SetCursorPosition(_conf.WidthConsole - 1, _conf.DirAreaRows + 1 + i);
                Console.Write(frame["lineVert"]);
            }

            //рисую нижнюю часть рамки перед строкой введения комманд
            Console.SetCursorPosition(0, _conf.HighConsole - 2);
            Console.Write(frame["angleDownLeft"]);
            for (int i = 1; i < _conf.WidthConsole - 1; i++) Console.Write(frame["lineHorizont"]);
            Console.Write(frame["angleDownRight"]);
            string message_exit = "Для выхода наберите exit и нажмите Enter";
            Console.SetCursorPosition(_conf.WidthConsole / 2 - message_exit.Length / 2 , _conf.HighConsole - 2);
            Console.Write(message_exit); 
        }


        public void UpdateDirArea(string[] rowsDirArea, string nameDir)
        {
            rowsDirArea = ChekLenghtString(rowsDirArea);
            //обновляю верхнюю горизонтальную линию для написания названия католога, который отображается
            Console.SetCursorPosition(1, 0);
            for (int i = 1; i < _conf.WidthConsole - 2; i++) Console.Write(_lineHorizont);
            //отображаю название католога и очишаю область, который отображается
            for (int i = 1; i < _conf.DirAreaRows; i++)
            {
                Console.SetCursorPosition(1, i);
                for (int j = 0; j < _conf.WidthConsole - 2; j++) Console.Write(" ");
            }

            Console.SetCursorPosition(1, 0);
            Console.Write(nameDir.PadLeft((_conf.WidthConsole / 2 + nameDir.Length / 2 - 2), _lineHorizont));

            for (int i = 1; i < _conf.DirAreaRows; i++)
            {
                if (rowsDirArea[i] != null)
                {
                    Console.SetCursorPosition(1, i);
                    Console.Write(rowsDirArea[i].Remove(0,nameDir.Length));
                }
                else
                {
                    Console.SetCursorPosition(1, i);
                    for(int j = 0; j < _conf.WidthConsole - 2; j++) Console.Write(" ");
                }

            }
        }
        public void UpdateDataArea(string[] rowsDataArea)
        {
            rowsDataArea = ChekLenghtString(rowsDataArea);

            //обновляю верхнюю горизонтальную линию для написания названия католога, который отображается
            Console.SetCursorPosition(1, _conf.DirAreaRows + 1);
            for (int i = 1; i < _conf.WidthConsole - 3; i++) Console.Write(_lineHorizont);
            for (int i = _conf.DirAreaRows + 2; i < _conf.DirAreaRows+2+_conf.DataAreaRows; i++)
            {
                Console.SetCursorPosition(1, i);
                for (int j = 0; j < _conf.WidthConsole - 2; j++) Console.Write(" ");
            }

            //отображаю название данных, которые отображаются
            Console.SetCursorPosition(1, _conf.DirAreaRows + 1);
            Console.Write(rowsDataArea[0].PadLeft((_conf.WidthConsole / 2 + rowsDataArea[0].Length / 2 - 2), _lineHorizont));

            for (int i =0 ; i < _conf.DataAreaRows ; i++)
            {
               

                if (rowsDataArea.Length > i+1 && rowsDataArea[i+1] != null)
                {
                    Console.SetCursorPosition(1, _conf.DirAreaRows + 2 + i);
                    Console.Write(rowsDataArea[i+1]);
                }

            }
        }

        public string[] UpdateInputArea(ConsoleKeyInfo userKey, int sizeInputCommands, string[] inputCommands)
        {

            string symString = "1 2 3 4 5 6 7 8 9 0 q w e r t y u i o p a s d f g h j k l z x c v b n m : . _ $ \\ й ц у к е н г ш щ з х ъ ф ы в а п р о л д ж э я ч с м и т ь б ю";
            string[] Symbol = symString.Split(' ');
            for (int i = 0; i < Symbol.Length; i++)
            {
                string keyC = userKey.KeyChar.ToString();
                if (keyC.Equals(Symbol[i]))
                {
                    inputCommands[sizeInputCommands - 1] +=  Symbol[i];
                }
            }
            switch (userKey.Key)
            {
                case ConsoleKey.Spacebar:
                    inputCommands[sizeInputCommands - 1] = inputCommands[sizeInputCommands - 1] + " ";
                    break;
                case ConsoleKey.Backspace:
                    if (inputCommands[sizeInputCommands - 1].Length > 0) inputCommands[sizeInputCommands - 1] = inputCommands[sizeInputCommands - 1].Remove(inputCommands[sizeInputCommands - 1].Length - 1);
                    break;
                case ConsoleKey.LeftArrow:
                    string lastElemet = inputCommands[sizeInputCommands - 1];
                    for (int i = 0; i < sizeInputCommands - 1; i++)
                    {
                        inputCommands[sizeInputCommands -1- i] = inputCommands[sizeInputCommands - 2 - i];

                    }
                    inputCommands[0] = lastElemet;
                    break;
                case ConsoleKey.RightArrow:
                    string firstElement = inputCommands[0] ;
                    for (int i = 0; i < sizeInputCommands-1; i++)
                    {
                        inputCommands[i] = inputCommands[i+1];

                    }
                    inputCommands[sizeInputCommands - 1] = firstElement;
                    break;
                case ConsoleKey.Enter:
                    for (int i = 0; i < sizeInputCommands - 1; i++)//сдвигаю комманды, освобождая место для новой
                    {
                        inputCommands[i] = inputCommands[i + 1];

                    }
                    inputCommands[sizeInputCommands - 1] = "";//пустая строка для введения новой комманды
                    break;
            }
            if (inputCommands != null && inputCommands[sizeInputCommands - 1] != null) ShowInputString(inputCommands[sizeInputCommands - 1]);
            else ShowInputString("");
            return inputCommands;
        }

        private void ShowInputString (string inputString)
        {
            Console.SetCursorPosition(0, _conf.HighConsole-1);
            Console.Write(inputString);
            if (inputString.Length > 0)
            {
                for (int i = 0; i < _conf.WidthConsole - 1 - inputString.Length; i++) Console.Write(" ");
                Console.SetCursorPosition(inputString.Length, _conf.HighConsole - 1);
            }
            else
            {
                string emptyString = "";
                for (int j = 0; j < _conf.WidthConsole - 2; j++) emptyString += " ";
                Console.Write(emptyString);
                Console.SetCursorPosition(0, _conf.HighConsole - 1);
            }
        }
        

        /// <summary>
        /// Если длина строки больше строки укорачиваю и привожу к виду диск:\...\остатки,общей длиной _conf.WidthConsole - 2
        /// </summary>
        /// <param name="chekRows">массив строк, который нужно укоротить и изменить</param>
        /// <returns></returns>
        private string[] ChekLenghtString(string[] chekRows)
        {
            for (int i = 0; i < chekRows.Length; i++)
            {
                string res = "";
                if (chekRows[i] != null && chekRows[i].Length > _conf.WidthConsole - 2)
                {
                    //если длина строки больше строки укорачиваю и привожу к виду диск:\...\остатки,
                    //общей длиной _conf.WidthConsole - 2
                    res = chekRows[i];
                    res = res[0] + res[1] + res[2] + @"...\" + res.Remove(0, (chekRows[i].Length - _conf.WidthConsole - 2) + 7);
                    chekRows[i] = res;
                }
            }
            return chekRows;
        }
    }
}
