using ControlzEx.Theming;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using Aura.Protection.Software;
using Aura.Protection.Arithmetic;
using Aura.Protection.CtrlFlow;
using Aura.Protection.INT;
using Aura.Protection.InvalidMD;
using Aura.Protection.LocalF;
using Aura.Protection.Other;
using Aura.Protection.Proxy;
using Aura.Protection.Renamer;
using Aura.Protection.String;
using Aura.Protection.StringOnline;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Brushes = System.Windows.Media.Brushes;

namespace Aura
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static MethodDef Init;
        public static MethodDef Init2;

        public string DirectoryName = string.Empty;

        public MainWindow() 
        {
            InitializeComponent();
            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.DoNotSync;
            ThemeManager.Current.SyncTheme();
        }

        public byte MaxValue = byte.MaxValue;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var time = DateTime.Now.ToString("hh:mm:ss");
            ModuleContext modCtx = ModuleDef.CreateModuleContext();
            var module = ModuleDefMD.Load(Program.Text, modCtx);

            ConsoleLog.Foreground = Brushes.Black;
            ConsoleLog.AppendText($"{time} Starting obfuscation{Environment.NewLine}");

            if (String_Encryption.IsChecked == true)
            {
                StringEncPhase.Execute(module);
                ConsoleLog.AppendText($"{time} Processing string encryption{Environment.NewLine}");
            }

            if (Online_Decryption.IsChecked == true)
            {
                OnlinePhase.Execute(module);
                ConsoleLog.AppendText($"{time} Processing online decryption{Environment.NewLine}");
            }

            if (Cflow.IsChecked == true)
            {
                ControlFlowObfuscation.Execute(module);
                ConsoleLog.AppendText($"{time} Processing control flow{Environment.NewLine}");
            }

            if (IntConf.IsChecked == true)
            {
                AddIntPhase.Execute2(module);
                ConsoleLog.AppendText($"{time} Processing integer confusion{Environment.NewLine}");
            }

            if (SUC.IsChecked == true)
            {
                StackUnfConfusion.Execute(module);
                ConsoleLog.AppendText($"{time} Processing stack confusion{Environment.NewLine}");
            }

            if (Ahri.IsChecked == true)
            {
                Arithmetic.Execute(module);
                ConsoleLog.AppendText($"{time} Processing math calculations{Environment.NewLine}");
            }

            if (LF.IsChecked == true)
            {
                L2F.Execute(module);
                ConsoleLog.AppendText($"{time} Processing constant fields{Environment.NewLine}");
            }

            if (LFV2.IsChecked == true)
            {
                L2FV2.Execute(module);
                ConsoleLog.AppendText($"{time} Processing local fields{Environment.NewLine}");
            }

            if (Calli_.IsChecked == true)
            {
                Calli.Execute(module);
                ConsoleLog.AppendText($"{time} Processing calli conversion{Environment.NewLine}");
            }

            if (Proxy_String.IsChecked == true)
            {
                ProxyString.Execute(module);
                ConsoleLog.AppendText($"{time} Processing proxy strings{Environment.NewLine}");
            }

            if (ProxyConstants.IsChecked == true)
            {
                ProxyINT.Execute(module);
                ConsoleLog.AppendText($"{time} Processing proxy constants{Environment.NewLine}");
            }

            if (Proxy_Meth.IsChecked == true)
            {
                ProxyMeth.Execute(module);
                ConsoleLog.AppendText($"{time} Processing proxy methods{Environment.NewLine}");
            }

            if (Anti_De4dot.IsChecked == true)
            {
                AntiDecompile.Execute(module.Assembly);
                ConsoleLog.AppendText($"{time} Processing anti-decompile{Environment.NewLine}");
            }

            if (JumpCflow.IsChecked == true)
            {
                JumpCFlow.Execute(module);
                ConsoleLog.AppendText($"{time} Processing flow conversion{Environment.NewLine}");
            }

            if (AntiDebug.IsChecked == true)
            {
                Anti_Debug.Execute(module);
                ConsoleLog.AppendText($"{time} Processing anti-debug{Environment.NewLine}");
            }

            if (Anti_Dump.IsChecked == true)
            {
                AntiDump.Execute(module);
                ConsoleLog.AppendText($"{time} Processing anti-dump{Environment.NewLine}");
            }

            if (AntiTamper.IsChecked == true)
            {
                Protection.Software.AntiTamper.Execute(module);
                ConsoleLog.AppendText($"{time} Processing anti-tamper{Environment.NewLine}");
            }

            if (InvalidMD.IsChecked == true)
            {
                InvalidMDPhase.Execute(module.Assembly);
                ConsoleLog.AppendText($"{time} Processing invalid metadata{Environment.NewLine}");
            }

            var text2 = Path.GetDirectoryName(Program.Text);
            if (text2 != null && !text2.EndsWith("\\"))
            {
                text2 += "\\";
            }

            var path = $"{text2}{Path.GetFileNameWithoutExtension(Program.Text)}_protected{Path.GetExtension(Program.Text)}";

            module.Write(path,
                         new ModuleWriterOptions(module)
                         { PEHeadersOptions = { NumberOfRvaAndSizes = 13 }, Logger = DummyLogger.NoThrowInstance });

            ConsoleLog.AppendText($"{time} File: {path}{Environment.NewLine}{Environment.NewLine}");

            if (AntiTamper.IsChecked == true)
            {
                Protection.Software.AntiTamper.Sha256(path);
            }
        }

        private void LoadBox_DragEnter(object sender, DragEventArgs e)
        { e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None; }

        private void LoadBox_Drop(object sender, DragEventArgs e)
        {
            try
            {
                var array = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (array == null)
                    return;
                var text = array.GetValue(0).ToString();
                var num = text.LastIndexOf(".", StringComparison.Ordinal);
                if (num == -1)
                    return;
                var text2 = text.Substring(num);
                text2 = text2.ToLower();
                if (string.Compare(text2, ".exe", StringComparison.Ordinal) != 0 && string.Compare(text2, ".dll", StringComparison.Ordinal) != 0)
                {
                    return;
                }

                Activate();
                Program.Text = text;
                var num2 = text.LastIndexOf("\\", StringComparison.Ordinal);
                if (num2 != -1)
                {
                    DirectoryName = text.Remove(num2, text.Length - num2);
                }

                if (DirectoryName.Length == 2)
                {
                    DirectoryName += "\\";
                }
            }
            catch
            {
                /* ignored */
            }
        }

        private void LoadBox_PreviewDragOver(object sender, DragEventArgs e) { e.Handled = true; }
    }
}
