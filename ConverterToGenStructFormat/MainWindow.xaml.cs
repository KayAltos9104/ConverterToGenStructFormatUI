using GsfConverter;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConverterToGenStructFormat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _openPath = string.Empty;       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Raw files (*.rawst)|*.rawst;|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                _openPath = openFileDialog.FileName;
                lblState.Content = "Загружена структура по адресу \n"+@_openPath;
            }           
        }

        private void btnConvertAndSaveStructure_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbxMultiplier.Text, out int m))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "gsf (*.txt)|*.txt;|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string savePath = saveFileDialog.FileName;
                    var points = Converter.ReadStructure(_openPath);
                    var newPoints = Converter.ADConvert(points, m);
                    Converter.SaveToGsf(savePath, newPoints);
                    lblState.Content = "Структура сохранена по адресу \n" + savePath;
                }
            }
            else
            {
                MessageBox.Show("Введите целочисленный коэффициент масштабирования!", "Ошибка");
            }
            
        }
    }
}
