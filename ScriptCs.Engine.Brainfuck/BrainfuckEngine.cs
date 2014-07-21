using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using brainfucker;
using Mono.Cecil;
using ScriptCs.Contracts;

namespace ScriptCs.Engine.Brainfuck
{
    public class BrainfuckEngine : IScriptEngine
    {
        public string BaseDirectory { get; set; }
        public string CacheDirectory { get; set; }
        public string FileName { get; set; }

        public ScriptResult Execute(string code, string[] scriptArgs, AssemblyReferences references, IEnumerable<string> namespaces,
            ScriptPackSession scriptPackSession)
        {
            try
            {
                var def = Compiler.Compile("code", code, false, false, false);

                using (var exeStream = new MemoryStream())
                {
                    AssemblyFactory.SaveAssembly(def, exeStream);
                    var exeBytes = exeStream.ToArray();
                    var assembly = Assembly.Load(exeBytes);
                    assembly.EntryPoint.Invoke(null, new object[] { new string[0] });
                }

            }
            catch (CompilerException e)
            {
                throw new Exception("Error compiling", e);
            }

            return new ScriptResult();
        }
    }
}
