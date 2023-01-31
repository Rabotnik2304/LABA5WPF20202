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
using static LABA5WPF20202.MainWindow;

namespace LABA5WPF20202
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private Dictionary<Scheme, Table> schemesAndTablesDict = new Dictionary<Scheme, Table>();

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

            foreach (string filePath in Directory.EnumerateFiles(folderPath))
            {
                if (filePath.Substring(filePath.Length - 4, 4) == "json")
                {
                    Scheme schemeOfTable = Scheme.ReadJson(filePath);
                    
                    string tableName = schemeOfTable.Name;                    
                    string pathTable = filePath.Substring(0, filePath.Length - 12)+".csv";
                    
                    Table table = TableReader.TableRead(schemeOfTable, pathTable);
                    

                    schemesAndTablesDict.Add(schemeOfTable,table);
                    
                    TreeViewItem tableTree = new TreeViewItem();

                    tableTree.Header = tableName;
                    
                    tableTree.Selected += TableTreeSelected;
                    
                    tableTree.Unselected += TableTreeUnselected;
                    
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
            DataTable.Columns.Clear();
            string tableName = ((TreeViewItem)sender).Header.ToString();

            foreach(var schemeAndTable in schemesAndTablesDict)
            {
                if (schemeAndTable.Key.Name==tableName)
                {
                    
                    foreach (SchemeColumn column in schemeAndTable.Key.Columns)
                    {
                        DataGridTextColumn tableTextColumn = new DataGridTextColumn();
                        tableTextColumn.Header = column.Name;
                        DataTable.Columns.Add(tableTextColumn);
                        
                        foreach (Row row in schemeAndTable.Value.Rows)
                        {
                             DataTable.Items.Add("12");
                        }    
                    }
                    break;
                }
            }   
        }
        private void TableTreeUnselected(object sender, RoutedEventArgs e)
        {
            DataTable.Columns.Clear();
        }
    }
}
