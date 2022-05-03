using System;
using System.IO;


namespace OOPFinalFM.Core
{
    public class Command
    {
        
        /// <summary>
        /// функция чтения содержания каталога
        /// </summary>
        /// <param name="path">путь до каталога</param>
        /// <param name="maxRows">макисмально хранимое кол-во строк</param>
        /// <returns></returns>
        public static string[] Listing(string path, int maxRows)
        {
            string[] ls_dir = Directory.GetDirectories(path); //получаем список каталогов в каталоге
            for(int i = 0; i < ls_dir.Length; i++) ls_dir[i] += @"\"; //добавляю знак чтобы отличать каталоги от файлов

            string[] ls_file = Directory.GetFiles(path); //получаем список файлов в каталоге
            int a = ls_dir.Length + ls_file.Length; //общая максимальная получаемя длина массива
            string[] ls = new string[maxRows]; //создаем массив для соединения результатов +1 (для названия каталога)
            //ls[0] = "ls " + path; //заносим название католога
                          //соединяем результаты
            if (a > (maxRows)) //если общее кол-во каталогов и файлов больше допустимого
            {
                if (ls_dir.Length > ((maxRows) - 1)) //если общее кол-во катологов больше допустимого
                {
                    ls_dir.CopyTo(ls, 0);
                }
                else //если общее кол-во каталогов меньше 
                {
                    ls_dir.CopyTo(ls, 0);
                    if (ls_dir.Length < maxRows) 
                    {
                        for (int i = ls_dir.Length; i < maxRows; i++) ls[i] = ls_file[i - ls_dir.Length];
                    }
                }
            }
            else //если общее кол-во каталогово и файлов меньше 
            {
                ls_dir.CopyTo(ls, 0);
                ls_file.CopyTo(ls, ls_dir.Length);
            }
            return ls;
        }
        /// <summary>
        /// функция копирования фалов и каталогов 
        /// </summary>
        /// <param name="path">текущий путь</param>
        /// <param name="new_path">пуьт куда копировать</param>
        /// <returns></returns>
        public static string Copy(string path, string new_path)
        {
            FileInfo file_inf = new FileInfo(path); //создаем переменную для файла path по конструктуру
            DirectoryInfo dir_inf = new DirectoryInfo(path);
            string result;
            if (file_inf.Exists && Directory.Exists(new_path)) //проверяем на существование
            {
                file_inf.CopyTo(new_path + "\\" + file_inf.Name, true); //копируем
                result = "Скопирован файл " + file_inf.Name + " в папку " + new_path;
            }
            else
            {
                if (dir_inf.Exists && Directory.Exists(new_path))
                {
                    result = "Каталог " + dir_inf.FullName + "скопирована в " + new_path;
                    dir_inf.MoveTo(new_path + dir_inf.Name);
                }
                else
                {
                    result = "Каталог или файл не найден или недоступен путь копирования " + new_path;
                }
            }
            return result;
        }

        /// <summary>
        /// функция удаления файлов и каталогов
        /// </summary>
        /// <param name="path">путь</param>
        /// <returns></returns>
        public static string Remove(string path)
        {
            string result;
            FileInfo file_inf = new FileInfo(path);
            if (file_inf.Exists) //проверяем файл ли это
            {
                file_inf.Delete();
                result = "Файл " + path + " был удален";
            }
            else
            {
                if (Directory.Exists(path)) //проверяем каталог ли это
                {
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        dirInfo.Delete(true);
                        result = "Каталог " + path + " удален";
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }
                }
                else
                {
                    result = "Файл или каталог не найден";
                }
            }
            return result;
        }
        /// <summary>
        /// функция получения данных о файле
        /// </summary>
        /// <param name="path">путь  до файла</param>
        /// <returns></returns>
        public static string[] FileInfo(string path)
        {
            string[] result = new string[4];
            FileInfo file_inf = new FileInfo(path);
            if (file_inf.Exists)
            {
                result[0] = "Имя файла " + file_inf.Name;
                result[1] = "Время создания файла " + file_inf.CreationTime;
                result[2] = "Размер файла " + file_inf.Length;
                result[3] = "Атрибуты файла " + file_inf.Attributes;
            }
            else
            {
                result[0] = "Файл " + path + " не найден";
            }
            return result;
        }
        /// <summary>
        /// функция получения данных о каталоге
        /// </summary>
        /// <param name="path">путь до каталогоа</param>
        /// <returns></returns>
        public static string[] DirInfo(string path)
        {
            string[] result = new string[3];
            DirectoryInfo dir_inf = new DirectoryInfo(path);
            if (dir_inf.Exists)
            {
                result[0] = "Имя каталога " + dir_inf.Name;
                result[1] = "Время создания каталога " + dir_inf.CreationTime;
                result[2] = "Атрибуты каталога " + dir_inf.Attributes;
            }
            else
            {
                result[0] = "Каталог " + path + " не найден";
            }
            return result;
        }
    }

}

