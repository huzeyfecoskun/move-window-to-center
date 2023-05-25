using System;

namespace Windows_Mover;

public class ProcessItem
{
    public string ProcessName { get; set; }
    public IntPtr Handle { get; set; }
    public uint ProcessId { get; set; }
}