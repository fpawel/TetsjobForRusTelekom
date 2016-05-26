using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Input;


namespace Books
{
    
    public static class DbCtx
    {
        public static readonly Books.LibraryDbEntities3 dbCtx = new Books.LibraryDbEntities3();
    }

    class HistBookComparer : System.Collections.Generic.IEqualityComparer<Hist> 
    {
        public bool Equals(Hist x, Hist y)
        {
            return x.HistBookId == y.HistBookId;
        }

        public int GetHashCode(Hist obj)
        {
            return obj.HistBookId;
        }
    }

    public class MainWindowDataContext
    {
        public ObservableCollection<BookViewModel> Books { get; set; }
        public ObservableCollection<SubscriberViewModel> Subs { get; set; }
        

        BookViewModel createBookViewModel(Book book)
        {
            return new BookViewModel(book, Books, Subs);

        }

        SubscriberViewModel createSubscriberViewModell(Sub sub)
        {
            return new SubscriberViewModel(sub, Books, Subs);

        }


        public MainWindowDataContext()
        {
            Books = new ObservableCollection<BookViewModel>();
            Subs = new ObservableCollection<SubscriberViewModel>();
            ReportDateFrom = DateTime.Now.AddYears(-10);
            ReportDateTo = DateTime.Now.AddYears(10);

            foreach ( var x in DbCtx.dbCtx.Books)
            {
                Books.Add(createBookViewModel(x));
            }
            foreach (var x in DbCtx.dbCtx.Subs)
            {
                Subs.Add(createSubscriberViewModell(x));
            }
            
        }
        
        
        public string NewSubscriberName { get; set; }

        
        public ICommand AddNewSubscriber
        {
            get
            {
                return new Command(() =>
                {
                    var sub = new Sub { SubsName = NewSubscriberName };
                    DbCtx.dbCtx.Subs.Add(sub);
                    Subs.Add(createSubscriberViewModell(sub));
                    DbCtx.dbCtx.SaveChanges();
                });
            }
        }

        public string NewBook { get; set; }

        public ICommand AddNewBook
        {
            get
            {
                return new Command(() =>
                {
                    var book = new Book { BookName = NewBook };
                    DbCtx.dbCtx.Books.Add(book);
                    DbCtx.dbCtx.SaveChanges();
                    Books.Add(createBookViewModel(book));
                });
            }
        }

        public DateTime ReportDateFrom { get; set; }
        public DateTime ReportDateTo { get; set; }

        public System.Collections.Generic.List< Tuple<Hist, int>> ReportHist
        {
            get
            {
                var hists =
                    (from x in DbCtx.dbCtx.Hists
                     where x.HistDate >= ReportDateFrom && x.HistDate <= ReportDateTo
                     select x).ToList();
                var hists1 =
                    hists
                    .Distinct(new HistBookComparer())
                    .ToList()
                    .Take(10)
                    .ToList();
                var hists2 =
                    (   from x in hists1
                        select new Tuple<Hist,int>( x,hists.Count((y) => y.Book == x.Book) ))
                        .ToList()
                        .OrderBy((x) => -x.Item2)
                        .ToList();
                

                return hists2;
            }
        }

    }
}
