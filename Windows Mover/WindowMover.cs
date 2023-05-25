using System;
using System.Runtime.InteropServices;

namespace Windows_Mover;
public class WindowMover
{
    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    private const int SM_CXSCREEN = 0;
    private const int SM_CYSCREEN = 1;
    
    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    private const uint SWP_SHOWWINDOW = 0x0040;
    private static readonly IntPtr HWND_TOP = IntPtr.Zero;

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public static bool MoveWindowToCenter(IntPtr hWnd)
    {
        RECT windowRect;
        if (GetWindowRect(hWnd, out windowRect))
        {
            int screenWidth = GetScreenWidth();
            int screenHeight = GetScreenHeight();

            int windowWidth = windowRect.Right - windowRect.Left;
            int windowHeight = windowRect.Bottom - windowRect.Top;

            int centerX = screenWidth / 2 - windowWidth / 2;
            int centerY = screenHeight / 2 - windowHeight / 2;

            return SetWindowPos(hWnd, HWND_TOP, centerX, centerY, windowWidth, windowHeight, SWP_SHOWWINDOW);
        }

        return false;
    }

    private static int GetScreenWidth()
    {
        int screenWidth = GetSystemMetrics(SM_CXSCREEN);
        return screenWidth;
    }

    private static int GetScreenHeight()
    {
        int screenHeight = GetSystemMetrics(SM_CYSCREEN);
        return screenHeight;
    }
}