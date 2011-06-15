using System;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Diagnostics;
using System.ComponentModel;

class test {
    static Regex findPattern = new Regex(@"^Find(\s\d+)?$");

    static void Main(string[] args) {

        // Get all instances of Notepad running on the local computer.
        Process[] localByName = Process.GetProcessesByName("devenv");
        if (localByName.Length > 0) {
            Warn("Detected VS is running, end the VS process first.");
            return;
        }

        Clean();
    }

    private static void Warn(string str) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(str);
        Console.ResetColor();
    }

    private static void Clean() {
        RegistryKey findKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\VisualStudio\9.0\Find", true);
        if (findKey != null) {
            try {
                foreach(string valueName in findKey.GetValueNames()) {
                    if (findPattern.IsMatch(valueName)) findKey.DeleteValue(valueName);
                }
            } catch(Exception ex) {
                Warn(ex.Message);
            }

            findKey.Close();
        } else {
            Warn("key is not found.");
        }
    }
}
