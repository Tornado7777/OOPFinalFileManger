# OOPFinalFileManger

 Реализовать простейший файловый менеджер с использованием ООП (классы, наследование и прочее).
### Файловый менеджер должен иметь возможность:
* показывать содержимое дисков;
* создавать папки/файлы;
* удалять папки/файлы;
* переименовывать папки/файлы;
* копировать/переносить папки/файлы;
* вычислять размер папки/файла;
* производить поиск по маске (с поиском по подпапкам);

Поддерживаемый функционал:

Просмотр файловой структуры комманда: ls c:\project
Копирование файлов, каталогов: cp c:\project\example.txt c:\project\test\
Поддержка удаление файлов, каталогов: rm c:\project\example.txt
Получение информации о размерах, системных атрибутов файла, каталога file c:\project\example.txt, dir c:\project

Вывод файловой структуры постраничный два листа (кол-во строк в одном листе указывается в конфигурации)
При выходе  сохраняется, последнее состояние
сохранение ошибки в текстовом файле в каталоге errors/random_name_exception.txt
Реализованно движение по истории команд 10 последних комманд(стрелочки влево, вправо)
