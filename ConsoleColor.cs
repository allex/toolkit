using System;
using System.Runtime.InteropServices;

namespace ConsoleColor
{
    /// Summary description for Class2.
    public class Class2
    {
        private int hConsoleHandle;
        private COORD ConsoleOutputLocation;
        private CONSOLE_SCREEN_BUFFER_INFO ConsoleInfo;
        private int OriginalColors;

        private const int  STD_OUTPUT_HANDLE = -11;

        [DllImport("kernel32.dll", EntryPoint="GetStdHandle", SetLastError=true,
            CharSet=CharSet.Auto,
            CallingConvention=CallingConvention.StdCall)]
        private static extern int GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", EntryPoint="GetConsoleScreenBufferInfo",
            SetLastError=true, CharSet=CharSet.Auto,
            CallingConvention=CallingConvention.StdCall)]
        private static extern int GetConsoleScreenBufferInfo(int hConsoleOutput,
            ref CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

        [DllImport("kernel32.dll", EntryPoint="SetConsoleTextAttribute",
            SetLastError=true, CharSet=CharSet.Auto,
            CallingConvention=CallingConvention.StdCall)]
        private static extern int SetConsoleTextAttribute(int hConsoleOutput, int wAttributes);

        public enum Foreground
        {			
            Blue = 0x00000001,
            Green = 0x00000002,
            Red = 0x00000004,
            Intensity = 0x00000008
        }

        public enum Background
        {
            Blue = 0x00000010,
            Green = 0x00000020,
            Red = 0x00000040,
            Intensity = 0x00000080
        }

        [StructLayout(LayoutKind.Sequential)] private struct COORD
        {
            short X;
            short Y;
        }

        [StructLayout(LayoutKind.Sequential)] private struct SMALL_RECT
        {
            short Left;
            short Top;
            short Right;
            short Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CONSOLE_SCREEN_BUFFER_INFO
        {
            public COORD dwSize;
            public COORD dwCursorPosition;
            public int wAttributes;
            public SMALL_RECT srWindow;
            public COORD dwMaximumWindowSize;
        }

        // Constructor.
        public Class2()
        {
            ConsoleInfo = new CONSOLE_SCREEN_BUFFER_INFO();
            ConsoleOutputLocation = new COORD();
            hConsoleHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            GetConsoleScreenBufferInfo(hConsoleHandle, ref ConsoleInfo);
            OriginalColors = ConsoleInfo.wAttributes;
        }		

        public void TextColor(int color)
        {
            SetConsoleTextAttribute(hConsoleHandle, color);
        }

        public void ResetColor()
        {
            SetConsoleTextAttribute(hConsoleHandle, OriginalColors);
        }

        [STAThread]
        static void Main(string[] args)
        {
            Class2 TextChange = new Class2();
            Console.WriteLine("Original Colors");
            Console.WriteLine("Press Enter to Begin");

            Console.ReadLine();
            TextChange.TextColor((int)Class2.Foreground.Green +
                    (int)Class2.Foreground.Intensity);
            Console.WriteLine("THIS TEXT IS GREEN");
            Console.WriteLine("Press Enter to change colors again");

            Console.ReadLine();
            TextChange.TextColor((int)Class2.Foreground.Red +
                    (int)Class2.Foreground.Blue +
                    (int)Class2.Foreground.Intensity);
            Console.WriteLine("NOW THE TEXT IS PURPLE");
            Console.WriteLine("Press Enter to change colors again");

            Console.ReadLine();
            TextChange.TextColor((int)Class2.Foreground.Blue +
                    (int)Class2.Foreground.Intensity +
                    (int)Class2.Background.Green +
                    (int)Class2.Background.Intensity);

            Console.WriteLine("NOW THE TEXT IS BLUE AND BACKGROUND OF IT IS GREEN");
            Console.WriteLine("Press Enter change everything back to normal");
            Console.ReadLine();

            TextChange.ResetColor();
            Console.WriteLine("Back to Original Colors");
            Console.WriteLine("Press Enter to Terminate");
            Console.ReadLine();
        }
    }
}

// [DllImport("kernel32.dll")]
// public static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, int wAttributes);

// [DllImport("kernel32.dll")]
// public static extern IntPtr GetStdHandle(uint nStdHandle);
 
// public static void Main(string[] args) {
//     uint STD_OUTPUT_HANDLE = 0xfffffff5;
//     IntPtr hConsole = GetStdHandle(STD_OUTPUT_HANDLE);

//     // increase k for more color options
//     // SetConsoleTextAttribute (h, BACKGROUND_RED | 8);
//     for (int k = 1; k < 255; k++) {
//         SetConsoleTextAttribute(hConsole, k);
//         Console.WriteLine("{0:d3} I want to be nice today!",k);
//     }

//     // final setting
//     SetConsoleTextAttribute(hConsole, 236);

//     Console.ResetColor();
     
//     Console.WriteLine("Press Enter to exit ...");
//     Console.Read(); // wait
// }
