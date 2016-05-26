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


namespace Books
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowDataContext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            DocReportRowGroup.Rows.Clear();
            var xs = (DataContext as MainWindowDataContext).ReportHist;
            foreach (var x in xs)
            {
                var row = new TableRow();
                DocReportRowGroup.Rows.Add(row);

                var cell = new TableCell();
                row.Cells.Add(cell);
                var p = new Paragraph();
                cell.Blocks.Add(p);
                var r = new Run(x.Item1.Book.BookName);
                p.Inlines.Add(r);

                cell = new TableCell();
                row.Cells.Add(cell);
                p = new Paragraph();
                cell.Blocks.Add(p);
                r = new Run(x.Item2.ToString());
                p.Inlines.Add(r);

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                var paginator = ((IDocumentPaginatorSource)this.document).DocumentPaginator;

                try
                {
                    printDialog.PrintDocument(paginator, "Печать");
                }
                catch (Exception)
                {
                    MessageBox.Show(App.Current.MainWindow, "Ошибка печати. Проверьте настройки принтера.", "Ошибка печати",
                            MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }
        }
    }
}
