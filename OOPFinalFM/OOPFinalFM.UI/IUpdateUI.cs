using System;

namespace OOPFinalFM.UI
{
    internal interface IUpdateUI
    {
        void UpdateDirArea(string [] stringDirArea, string nameDir);
        void UpdateDataArea(string [] stringDataArea);

        string[] UpdateInputArea(ConsoleKeyInfo userKey, int sizeInputCommands, string[] inputCommands);

    }
}
