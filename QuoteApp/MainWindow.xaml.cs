using QuoteApp.Models;
using QuoteApp.Reuters;
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

namespace QuoteApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DoIt();
        }

        private async void DoIt()
        {
            var quotes = new List<StockQuote>();
            var reuters = new StockQuoteServiceClient();
            reuters.InnerChannel.OperationTimeout = TimeSpan.FromMinutes(20);
            reuters.Open();                        

            using (var db = new StockQuotesDbContext())
            {
                var r = Parallel.ForEach(SymbolLibrary.StockSymbols, (x)=> {

                    var quote = reuters.GetStockQuote(x);
                    if (quote.Name != "N/A")
                        quotes.Add(quote);
                });
                //foreach (var symbol in SymbolLibrary.StockSymbols)
                //{
                //    var quote = await reuters.GetStockQuoteAsync(symbol);
                //    if (quote.Name == "N/A")
                //        continue;
                //    quotes.Add(quote);
                //}
                while (!r.IsCompleted) ;

                db.StockQuotes.AddRange(quotes);                
                await db.SaveChangesAsync();

                var query = from b in db.StockQuotes
                            orderby b.Name
                            select b;
                if (query.Any())
                    Tag = true;
            }

            reuters.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //System.Windows.Data.CollectionViewSource stockQuoteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stockQuoteViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stockQuoteViewSource.Source = [generic data source]
        }
    }
}
