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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using B1TestApp.Functions;
using B1TestApp.Services;
using B1TestApp.Services.Implementations;
using B1TestApp.Services.Interfaces;
using Microsoft.Win32;

namespace B1TestApp
{
    
    
    public partial class MainWindow : Window
    {
        private readonly Storyboard spinnerStoryboard;
        private readonly ExcelParser _excelParser;
        private readonly FileService _fileSerivce;
        private readonly BankAccountDataService _bankAccountDataService;
        
        public MainWindow()
        {
            InitializeComponent();
            spinnerStoryboard = (Storyboard)this.Resources["SpinnerStoryboard"];
            _excelParser = App.GetService<ExcelParser>();
            _fileSerivce = App.GetService<FileService>();
            _bankAccountDataService = App.GetService<BankAccountDataService>();
        }

        private async void ButtonImportExcel_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx", 
                Title = "Select an Excel File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ToggleUi(false);
                SpinnerCanvas.Visibility = Visibility.Visible;
                spinnerStoryboard.Begin();
                string filePath = openFileDialog.FileName;

                try
                {
                    await _excelParser.ImportAsync(filePath);
                    MessageBox.Show("Файл успешно загружен в БД");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
                finally
                {
                    spinnerStoryboard.Stop();
                    SpinnerCanvas.Visibility = Visibility.Collapsed;
                    ToggleUi(true);
                }
            }
        }
        private async void ButtonGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            ToggleUi(false);
            SpinnerCanvas.Visibility = Visibility.Visible;
            spinnerStoryboard.Begin();
            try
            {
                await GenerateFunction.GenerateFilesAsync();
                MessageBox.Show($"Создание файлов завершено");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при объединении файлов: {ex.Message}");
            }
            finally
            {
                spinnerStoryboard.Stop();
                SpinnerCanvas.Visibility = Visibility.Collapsed;
                ToggleUi(true);
            }
            
        }
        
        private async void ButtonMerge_OnClick(object sender, RoutedEventArgs e)
        {
            string searchTerm = TextBoxSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Введите текст для поиска.");
                return;
            }

            ToggleUi(false);
            SpinnerCanvas.Visibility = Visibility.Visible;
            spinnerStoryboard.Begin();
            try
            {
                var deletedLines = await MergeFiles.MergeFilesAndRemoveLinesAsync(searchTerm);
                MessageBox.Show($"Объединение завершено. Удалённых строк: {deletedLines}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при объединении файлов: {ex.Message}");
            }
            finally
            {
                spinnerStoryboard.Stop();
                SpinnerCanvas.Visibility = Visibility.Collapsed;

                ToggleUi(true);
            }
        }
        
        private async void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            var progress = new Progress<(double progress, string message)>(status =>
            {
                Dispatcher.Invoke(() =>
                {
                    ProgressBarSave.Value = status.progress;
                    ProgressText.Text = status.message;
                });
            });

            ToggleUi(false);
            SpinnerCanvas.Visibility = Visibility.Visible;
            spinnerStoryboard.Begin();
            try
            {
                await SaveToDb.ImportToDatabaseAsync(progress);
                MessageBox.Show($"Строки записаны в БД");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка при записи в БД: {ex.Message}");
            }
            finally
            {
                spinnerStoryboard.Stop();
                SpinnerCanvas.Visibility = Visibility.Collapsed;

                ToggleUi(true);
                
            }
            
        }

        private void ToggleUi(bool isEnabled)
        {
            ButtonGenerate.IsEnabled = isEnabled;
            ButtonSave.IsEnabled = isEnabled;
            ButtonMerge.IsEnabled = isEnabled;
            ButtonImportExcel.IsEnabled = isEnabled;
            ButtonShowFiles.IsEnabled = isEnabled;
            ButtonShowData.IsEnabled = isEnabled;

        }

        private async void ButtonShowFiles_OnClick(object sender, RoutedEventArgs e)
        {
            var infoWindow = new infoWindow();
            ToggleUi(false);
            SpinnerCanvas.Visibility = Visibility.Visible;
            spinnerStoryboard.Begin();
            try
            {
                var names = await _fileSerivce.GetFilesNamesAsync();

                infoWindow.InfoTextBlock.Text = string.Join(Environment.NewLine, names);

                infoWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
            finally
            {
                spinnerStoryboard.Stop();
                SpinnerCanvas.Visibility = Visibility.Collapsed;
                ToggleUi(true);
                
            }
            
        }

        private async void ButtonShowData_OnClick(object sender, RoutedEventArgs e)
        {
            var infoWindow = new dataWindow();
            ToggleUi(false);
            SpinnerCanvas.Visibility = Visibility.Visible;
            spinnerStoryboard.Begin();
            try
            {
                var data = await _bankAccountDataService.GetBankAccountDataAsync();
            
                var formattedData = data.Select(d => new
                {
                    BankAccountNumber = d.BankAccountNumber,
                    IncomingBalanceActive = d.IncomingBalance?.Assets ?? 0,
                    IncomingBalancePassive = d.IncomingBalance?.Liabilities ?? 0,
                    TurnoverDebit = d.Turnover?.Debit ?? 0,
                    TurnoverCredit = d.Turnover?.Credit ?? 0,
                    OutcomingBalanceActive = d.OutcomingBalance?.Assets ?? 0,
                    OutcomingBalancePassive = d.OutcomingBalance?.Liabilities ?? 0,
                    ReportYear = d.ReportYear,
                    Bank = d.Bank.Name
                }).ToList();

                infoWindow.DataListView.ItemsSource = formattedData;

                infoWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
            finally
            {
                spinnerStoryboard.Stop();
                SpinnerCanvas.Visibility = Visibility.Collapsed;
                ToggleUi(true);
                
            }
            
        }
    }
}