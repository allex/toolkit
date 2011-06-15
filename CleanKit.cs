using System;
using Microsoft.Win32;
using System.Runtime.InteropServices; 

public class CleanKit {

    // HKEY_CURRENT_USER
    const string REG_KEY_USER_ASSIST = @"Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist\{75048700-EF1F-11D0-9888-006097DEACF9}\Count";

    /// <summary>
    /// Clean up the user assist program history.
    /// http://personal-computer-tutor.com/abc3/v29/vic29.htm
    /// </summary>
    public static void Main() {
        RegistryKey key = Registry.CurrentUser.OpenSubKey(REG_KEY_USER_ASSIST, true);
        if (key != null) {
            int start = -1;
            string s;
            try {
                foreach(string v in key.GetValueNames()) {
                    start = v.IndexOf(":");
                    if (start > 0) {
                        // decrypt those entries
                        s = decrypt2(v.Substring(start + 1));
                        if (!System.IO.File.Exists(s)) {
                            // Warn(s);
                            // file not exist any more, delete it!!!
                            key.DeleteValue(v);
                        }
                    }
                }
            } catch (Exception exception) {
                Warn(exception.Message);
            }
            key.Close();
        } else {
            Warn("key is not found.");
        }
    }

    // decrypt data in
    // HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist\{75048700-EF1F-11D0-9888-006097DEACF9}\Count
    static char[] decrypt(char[] path) {
        for (int i = 0, l = path.Length; i < l; i++) {
            if (path[i] >= 0x61 && path[i] <= 0x7A) {
                if (path[i] >= 0x61 && path[i] <= 0x6d) {
                    path[i] += (char) 0xd;
                } else {
                    path[i] -= (char) 0xd;
                }
            } else {
                if (path[i] >= 0x41 && path[i] <= 0x5a) {
                    if (path[i] >= 0x41 && path[i] <= 0x4d) path[i] += (char) 0xd;
                    else path[i] -= (char) 0xd;
                }
            }
        }

        return path;
    }
    
    [DllImport("msvcrt.dll", CallingConvention=CallingConvention.Cdecl)]
    private unsafe static extern int strlen(char* pByte);

    static unsafe string decrypt2(string str) {
        fixed (char *path = str) {
            for (int i = 0, l = str.Length; i < l; i++) {
                if (path[i] >= 0x61 && path[i] <= 0x7A) {
                    if (path[i] >= 0x61 && path[i] <= 0x6d) {
                        path[i] += '\x000d';
                    } else {
                        path[i] -= '\x000d';
                    }
                } else {
                    if (path[i] >= 0x41 && path[i] <= 0x5a) {
                        if (path[i] >= 0x41 && path[i] <= 0x4d) path[i] += '\x000d';
                        else path[i] -= '\x000d';
                    }
                }
            }
        }

        return str;
    }

    static void Warn(string str) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(str);
        Console.ResetColor();
    }
}
