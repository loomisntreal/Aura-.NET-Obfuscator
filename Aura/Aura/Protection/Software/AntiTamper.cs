using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Aura.Protection.Software.Runtime;
using Aura.Services;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Aura.Protection.Software
{
    public static class AntiTamper
    {
        public static void Sha256(string filePath)
        {
            var sha256Bytes = SHA256.Create().ComputeHash(File.ReadAllBytes(filePath));
            using (var stream = new FileStream(filePath, FileMode.Append))
            {
                stream.Write(sha256Bytes, 0, sha256Bytes.Length);
            }
        }

        public static void Execute(ModuleDef module)
        {
            var typeModule = ModuleDefMD.Load(typeof(AntiTamperHandler).Module);
            var cctor = module.GlobalType.FindOrCreateStaticConstructor();
            var typeDef = typeModule.ResolveTypeDef(MDToken.ToRID(typeof(AntiTamperHandler).MetadataToken));
            var members = InjectHelper.Inject(typeDef, module.GlobalType, module);
            var init = (MethodDef)members.Single(method => method.Name == "Initializer");
            cctor.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Call, init));
            foreach (var md in module.GlobalType.Methods)
            {
                if (md.Name != ".ctor") continue;
                module.GlobalType.Remove(md);
                break;
            }
        }
    }
}