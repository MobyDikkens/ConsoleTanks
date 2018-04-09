

using System;

namespace ConsoleTanks.Exceptions
{
    class PlayerControllerException:Exception
    {
        public PlayerControllerException(string message) : base(message)
        { }
    }
}
