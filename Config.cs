using Exiled.API.Interfaces;

namespace SCP079_Escape
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }
    }
}
