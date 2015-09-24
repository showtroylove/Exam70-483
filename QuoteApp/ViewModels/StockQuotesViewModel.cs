using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuoteApp.Reuters;
using QuoteApp.Models;

namespace QuoteApp.ViewModels
{
    public class StockQuotesViewModel : BindableBase
    {
        ObservableCollection<StockQuote> stockQuotes;
        public ObservableCollection<StockQuote> StockQuotes
        {
            get { return stockQuotes; }
            private set
            {
                stockQuotes = value;
                OnPropertyChanged(nameof(StockQuotes));
            }
        }
        public StockQuotesViewModel()
        {
            Init();
        }

        private void Init()
        {            
            using (var db = new StockQuotesDbContext())
            {
                var query = from b in db.StockQuotes
                            orderby b.Name
                            select b;
                stockQuotes = new ObservableCollection<StockQuote>(query);
            }
        }
    }
}
