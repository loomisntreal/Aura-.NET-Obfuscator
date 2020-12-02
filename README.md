<p align="center"><img width=60% src="https://i.vgy.me/2q0t5S.png"></p>
<p align="center">"Keep decompilers at bay, forever"</p>

![Build Status](https://github.com/aura-systems/Aura-Operating-System/workflows/.NET%20Core/badge.svg)
[![Issues](https://img.shields.io/github/issues/loomisntreal/Aura-.NET-Obfuscator.svg)](https://github.com/loomisntreal/Aura-.NET-Obfuscator/issues)
[![Pull requests](https://img.shields.io/github/issues-pr/loomisntreal/Aura-.NET-Obfuscator.svg)](https://github.com/loomisntreal/Aura-.NET-Obfuscator/pulls)

A secure based .NET Obfuscator developped in C# made by loom (loomisntreal).

## Current features

* String Encryption (encodes strings in the program)
* Online Encryption (encodes strings in the program & decrypts from the online method inside of the source-code)
* Control Flow (mangles with the methods inside of the code so decompilers cannot decompile the methods)
* Integer Confusion (this will add calculations inside of all integers)
* Math Calculations (this adds arithmetic inside of all constants)
* Constant Fields (converts all constants to fields with randomly selected names)
* Local Fields (converts all locals to fields with randomly selected names)
* Calli Conversion (converts all calls & calculations to calli calculations)
* Proxy Strings (hides string references that were referenced to a type, method, or field)
* Proxy Constants (hides constant references that were referenced to a type, method, or field)
* Proxy Methods (hides references that were referenced to a type, method, or field)
* Index Fields (indexes types/methods/fields)
* Flow Conversion (floods decompilers)
* Anti-Debug (prevents the assembly from being debugged or profiled)
* Anti-Dump (prevents the assembly from being dumped from memory)
* Anti-Tamper (ensures the integrity of the application)
* Anti-Decompile (prevents decompilers from working)
* Invalid Metadata (adds invalid metadata to modules to prevent disassemblers/decompilers from opening the application)
* Stack Conversion (adds a piece of code in front of all methods and converts them to a stack)
* Resource Conversion (converts all resources in the application to fields)

## Screenshots

<p align="center"><img width=60% src="https://i.vgy.me/Svxjzm.png"></p>

<p align="center"><img width=60% src="https://i.vgy.me/Qxn6xi.png"></p>

## Contribution
Do you want to add awesome features to Aura? Here's how:

- Fork Aura-.NET-Obfuscator repository
- Commit & push a new feature to the forked repository
- Open a pull request from your fork to Aura .NET Obfuscator
- We will review it and merge it
