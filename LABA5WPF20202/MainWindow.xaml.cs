using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;
using System;
using System.Windows.Input;
using System.Windows.Documents;

namespace LABA5WPF20202
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {   
        private class Data
        {
            public object Content { get; set; }
            public List<Data> Items { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MenuOpenFileClick(object sender, RoutedEventArgs e)
        {
            
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            string folderPath = string.Empty;
            if (openFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = openFolderDialog.SelectedPath;  //selected folder path
            }
            List<Data> datas = new List<Data>();

            foreach (string fileName in Directory.EnumerateFiles(folderPath))
            {
                if (fileName.Substring(fileName.Length - 4, 4) == "json")
                {
                    Scheme schemeOfTable = Scheme.ReadJson(fileName);
                    
                    string tableName = fileName.Substring(folderPath.Length+1, fileName.Length - folderPath.Length-13);
                    
                    Data tableData = new Data();

                    tableData.Content = tableName;
                    List<Data> listOfFields = new List<Data>();
                    
                    foreach (SchemeColumn key in schemeOfTable.Columns)
                    {
                        listOfFields.Add(new Data() { Content = key.Name +"---"+ key.Type });
                    }
                    tableData.Items = listOfFields;
                    datas.Add(tableData);
                }
            }
            TableTree.ItemsSource = datas;
        }
    }
}
