using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;

namespace allex.util {
    /// <summary>
    /// Shell for the sample.
    /// </summary>
    class Exec {
        const string ConfigFileName = "applist.dat";

        /// <summary>
        /// Uses the ProcessStartInfo class to start new processes, both in a minimized mode.
        /// </summary>
        static void OpenWithStartInfo(string exePath, string args) {
            ProcessStartInfo startInfo = new ProcessStartInfo(exePath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = args;
            Process.Start(startInfo);
        }

        static string GetApplicationPathBySID(string sid) {
            string cfgFile = AppDomain.CurrentDomain.BaseDirectory + ConfigFileName;

            if (File.Exists(cfgFile)) {
                try {
                    // Create an instance of StreamReader to read from a file.
                    // The using statement also closes the StreamReader.
                    using(StreamReader sr = new StreamReader(cfgFile)) {
                        String line;
                        string[] keyPair = new string[2];
                        char[] sepChars = new Char [] {'|'};

                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null) {
                            line = line.Trim();
                            // Console.WriteLine(line);
                            if (line.IndexOf("#") == 0) continue;
                            keyPair = line.Split(sepChars);
                            if ((keyPair = line.Split(sepChars))[1] == sid) return keyPair[0].Trim();
                        }
                    }
                }
                catch(Exception e) {
                    // Let the user know what went wrong.
                    Console.WriteLine("Anylize config file fails");
                    Console.WriteLine(e.Message);
                }
            }

            return null;
        }

        static void Main(string[] args) {
            string exePath = string.Empty, exeArgs = string.Empty;
            int len = args.Length;
            if (len > 0) {
                exePath = GetApplicationPathBySID(args[0]);
                if (len > 1) exeArgs = args[1];
                if (!string.IsNullOrEmpty(exePath)) {
                    int index = exePath.IndexOf("\" ");
                    if (index != -1) {
                        string xargs = exePath.Substring(index + 2);
                        exePath = exePath.Substring(0, index + 1);
                        exeArgs += string.IsNullOrEmpty(exeArgs) ? xargs : " " + xargs;
                    }
                    // Console.WriteLine(exePath + ":" + exeArgs);
                    OpenWithStartInfo(exePath, exeArgs);
                    return;
                }
            }
            Console.WriteLine("Missing or invalid argument.");
        }
    }
}
