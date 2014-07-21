using System;
using ScriptCs.Contracts;

namespace ScriptCs.Engine.Brainfuck
{
    [Module("brainfuck")]
    public class BrainfuckModule : IModule
    {
        public void Initialize(IModuleConfiguration config)
        {
            Console.WriteLine("brainfuck module initialized");
            config.ScriptEngine<BrainfuckEngine>();
        }
    }
}