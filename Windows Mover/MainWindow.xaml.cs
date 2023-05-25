///
/// Developer: huzeyfecoskun@hotmail.com
/// 
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Windows_Mover
{
    public partial class MainWindow : Window
    {
        List<IntPtr> windowHandles = WindowHandleFinder.GetWindowHandles();
        List<ProcessItem> ProcessList = new List<ProcessItem>();
        List<string> WindowList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
      
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            foreach (IntPtr handle in windowHandles)
            {
                string title = WindowHandleFinder.GetWindowTitle(handle);
                uint processId = WindowHandleFinder.GetProcessId(handle);
                ProcessList.Add(new ProcessItem
                {
                    ProcessName = title,
                    Handle = handle,
                    ProcessId = processId});
                WindowList.Add($"Title: {title}, Handle: {handle}, Process ID: {processId}");
                // Console.WriteLine($"Title: {title}, Handle: {handle}, Process ID: {processId}");
            }
            listBox.ItemsSource = WindowList;
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = textBox.Text;
            List<string> filteredList = new List<string>();
            foreach (string item in WindowList)
            {
                if (item.Contains(filter))
                {
                    filteredList.Add(item);
                }
            }
            listBox.ItemsSource = filteredList;
        }

        private void ListBox_OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selectedItem = listBox.SelectedItem.ToString();
            string[] split = selectedItem.Split(',');
            string handle = split[1].Split(':')[1].Trim();
            string processId = split[2].Split(':')[1].Trim();
            Console.WriteLine($"Handle: {handle}, Process ID: {processId}");
            IntPtr handleIntPtr = new IntPtr(Convert.ToInt32(handle));
            IntPtr processIdIntPtr = new IntPtr(Convert.ToInt32(processId));
            WindowMover.MoveWindowToCenter(handleIntPtr);
        }
    }
}