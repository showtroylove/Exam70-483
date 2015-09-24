using QuoteApp.Reuters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace QuoteApp.Models
{       
    public class StockQuotesDbContext : DbContext
    {
        public StockQuotesDbContext() : base("name=QuoteApp.Properties.Settings.StockQuoteDbContext")
        {
        }
        public DbSet<StockQuote> StockQuotes { get; set; }        
    }
}

namespace QuoteApp.Reuters
{
    public partial class StockQuote : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {
        public int StockQuoteId { get; set; }
    }
}

