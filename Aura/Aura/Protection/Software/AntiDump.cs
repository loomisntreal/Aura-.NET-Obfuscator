using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Aura.Protection.Software.Runtime;
using Aura.Services;
using System.Linq;

namespace Aura.Protection.Software
{
    internal class AntiDump
    {
        public static void Execute(ModuleDef mod)
        {
            var typeModule = ModuleDefMD.Load(typeof(AntiDumpHandler).Module);
            var cctor = mod.GlobalType.FindOrCreateStaticConstructor();
            var typeDef = typeModule.ResolveTypeDef(MDToken.ToRID(typeof(AntiDumpHandler).MetadataToken));
            var members = InjectHelper.Inject(typeDef, mod.GlobalType, mod);
            var init = (MethodDef)members.Single(method => method.Name == "Initialize");
            cctor.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Call, init));
            foreach (var md in mod.GlobalType.Methods)
            {
                if (md.Name != ".ctor") continue;
                mod.GlobalType.Remove(md);
                break;
            }
        }
    }
}