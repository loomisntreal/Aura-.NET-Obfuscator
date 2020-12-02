using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Aura.Protection.Arithmetic
{
    public abstract class iFunction
    {
        public abstract ArithmeticTypes ArithmeticTypes { get; }

        public abstract ArithmeticVT Arithmetic(Instruction instruction, ModuleDef module);
    }
}