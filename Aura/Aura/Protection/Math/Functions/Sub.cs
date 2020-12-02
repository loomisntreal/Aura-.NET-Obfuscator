﻿using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Aura.Protection.Arithmetic.Utils;

namespace Aura.Protection.Arithmetic.Functions
{
    public class Sub : iFunction
    {
        public override ArithmeticTypes ArithmeticTypes => ArithmeticTypes.Sub;

        public override ArithmeticVT Arithmetic(Instruction instruction, ModuleDef module)
        {
            if (!ArithmeticUtils.CheckArithmetic(instruction)) return null;
            var arithmeticEmulator = new ArithmeticEmulator(instruction.GetLdcI4Value(), ArithmeticUtils.GetY(instruction.GetLdcI4Value()), ArithmeticTypes);
            return (new ArithmeticVT(new Value(arithmeticEmulator.GetValue(), arithmeticEmulator.GetY()), new Token(OpCodes.Sub), ArithmeticTypes));
        }
    }
}