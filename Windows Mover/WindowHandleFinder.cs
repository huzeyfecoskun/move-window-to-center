using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Windows_Mover;

public class WindowHandleFinder
{
    private delegate bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumWindows(EnumWindowsCallback lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    public static List<IntPtr> GetWindowHandles()
    {
        List<IntPtr> windowHandles = new List<IntPtr>();

        EnumWindows((hWnd, lParam) =>
        {
            // Exclude the current window
            if (hWnd != IntPtr.Zero && hWnd != Process.GetCurrentProcess().MainWindowHandle)
            {
                windowHandles.Add(hWnd);
            }

            return true; // Continue enumeration
        }, IntPtr.Zero);

        return windowHandles;
    }

    public static string GetWindowTitle(IntPtr hWnd)
    {
        const int maxTitleLength = 1024;
        StringBuilder titleBuilder = new StringBuilder(maxTitleLength);
        GetWindowText(hWnd, titleBuilder, maxTitleLength);
        return titleBuilder.ToString();
    }

    public static uint GetProcessId(IntPtr hWnd)
    {
        uint processId;
        GetWindowThreadProcessId(hWnd, out processId);
        return processId;
    }
}