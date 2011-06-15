using System;
using Microsoft.Win32;
using System.Runtime.InteropServices; 

public class regdel {
    public static void Main(string[] args) {
        if (args.Length > 0) {
            Delete(args[0]);
        } else {
            Warn("Error: Registry path not set");
        }
    }

    static void Delete(string path) {
        int index = path.IndexOf("\\");
        string prefix = path.Substring(0, index);
        path = path.Substring(index + 1);

        RegistryKey root = GetRegistryKey(prefix).OpenSubKey(path, true);
        if (root != null) {
            Message(string.Format("\"{0}\" cleaned up.", root.Name));
            try {
                // delete the subkey and its tree.
                foreach(string subKeyName in root.GetSubKeyNames()) {
                    root.DeleteSubKeyTree(subKeyName);
                }
                // delete the key values.
                foreach(string valueName in root.GetValueNames()) {
                    root.DeleteValue(valueName);
                }
            } catch (Exception exception) {
                Warn(exception.Message);
            }
            root.Close();
        }
    }

    static RegistryKey GetRegistryKey(string key) {
        RegistryKey reg = null;
        switch (key) {
            case "HKLM":
            case "HKEY_LOCAL_MACHINE":
                reg = Registry.LocalMachine;
                break;
            case "HKCR":
            case "HKEY_CLASSES_ROOT":
                reg = Registry.ClassesRoot;
                break;
            case "HKU":
            case "HKEY_USERS":
                reg = Registry.Users;
                break;
            case "HKCC":
            case "HKEY_CURRENT_CONFIG":
                reg = Registry.CurrentConfig;
                break;
            case "HKCU":
            case "HKEY_CURRENT_USER":
            default:
                reg = Registry.CurrentUser;
                break;
        }

        return reg;
    }

    static void Warn(string str) {
        WriteLine(str, ConsoleColor.Red);
        Console.ResetColor();
    }

    static void Message(string str) {
        WriteLine(str, ConsoleColor.DarkGreen);
        Console.ResetColor();
    }

    static void WriteLine(string str, ConsoleColor color) {
        Console.ForegroundColor = color;
        Console.WriteLine(str);
        Console.ResetColor();
    }
}
