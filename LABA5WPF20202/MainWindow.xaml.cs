using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;
using System;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Controls;
using Newtonsoft.Json.Schema;

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

            string folderName = folderPath.Split("\\")[folderPath.Split("\\").Length - 1];



            dataTree.Header = folderName;

            foreach (string fileName in Directory.EnumerateFiles(folderPath))
            {
                if (fileName.Substring(fileName.Length - 4, 4) == "json")
                {
                    Scheme schemeOfTable = Scheme.ReadJson(fileName);
                    
                    string tableName = schemeOfTable.Name;
                    
                    //string pathTable = fileName.Substring(0, fileName.Length - 12);
                    //Table table = TableReader.TableRead(schemeOfTable, pathTable);

                    TreeViewItem tableTree = new TreeViewItem();
                    tableTree.Selected += TableTreeSelected;
                    tableTree.Header = tableName;
                    
                    foreach (SchemeColumn key in schemeOfTable.Columns)
                    {
                        tableTree.Items.Add(key.Name +"---"+ key.Type);
                    }

                    dataTree.Items.Add(tableTree);
                }
            }    
        }

        private void TableTreeSelected(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
