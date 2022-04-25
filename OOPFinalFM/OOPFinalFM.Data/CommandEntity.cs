

namespace OOPFinalFM.Data
{
    public class CommandEntity
    {

        private string[] _inputCommand;
        private string[] _resultCommand;
        private CommandType _commandType;

        public string[] InputCommand { get { return _inputCommand; } set { _inputCommand = value; } }
        public string[] ResultCommand { get { return _resultCommand; } set { _resultCommand = value; } }
        public CommandType CurCommandType { get { return _commandType; } set { _commandType = value; } }
        
        public CommandEntity() 
        {
            _inputCommand = new string[5]  ;
            _resultCommand = new string[] { };
            _commandType = CommandType.None;
        }
        public CommandEntity(string[] inputCommand) : this (inputCommand,CommandType.None)  { }

        public CommandEntity(string[] inputCommand, CommandType commandType) 
        {
            _inputCommand = inputCommand;
            _commandType = commandType;
        }
        public CommandEntity(string[] inputCommand, string[] resultCommand, CommandType commandType)
        {
            _inputCommand = inputCommand;
            _resultCommand = resultCommand;
            _commandType = commandType;
        }


        public enum CommandType
        {
            None,
            Listing,
            Copy,
            Remove,
            FileInfo,
            DirInfo,
            Help
        }
    }
}
