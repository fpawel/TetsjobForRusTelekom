using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Books
{
    public class BookViewModel : ViewModelBase
    {
        public readonly Book book;

        readonly public ObservableCollection<BookViewModel> books;
        readonly public ObservableCollection<SubscriberViewModel> subs;
        public BookViewModel(Book b, ObservableCollection<BookViewModel> books_,
            ObservableCollection<SubscriberViewModel> subs_)
        {
            book = b;
            books = books_;
            subs = subs_;            
        }       

        public ICommand GetBack
        {
            get
            {
                return Command.create(() =>
                {
                    Order = null;
                });
            }
        }


        public ICommand Remove
        {
            get
            {
                return Command.create(()=>
                {
                    foreach(var sub in subs)
                    {
                        sub.Orders.Remove(sub.Orders.FirstOrDefault((x) => x.Book == book));
                    }
                    books.Remove(this);

                    book.Orders.Clear();
                    DbCtx.dbCtx.Books.Remove(book);                    
                    DbCtx.dbCtx.SaveChanges();
                    
                });
            }
        }

        public Book Book
        {
            get
            {
                return book;
            }
        }

        public int Raiting
        {
            get
            {
                return DbCtx.dbCtx.Hists.ToArray().Count((x) => x.Book==book);
            }
        }

        
        public Order Order
        {
            get
            {
                var orders = book.Orders;
                if (orders == null)
                {
                    return null;
                }
                return orders.FirstOrDefault();
            }
            set
            {
                if(value == Order)
                {
                    return;
                }

                foreach (var sub in subs)
                {
                    var y = sub.Orders.FirstOrDefault((x) => x.Book == book);
                    sub.Orders.Remove(y);
                }

                book.Orders.Clear();
                if (value != null)
                {                    
                    book.Orders.Add(value);
                    DbCtx.dbCtx.Hists.Add(new Hist { HistBookId = book.BookId, HistSubsId = value.Sub.SubsId, HistDate = DateTime.Now });
                }

                                
                DbCtx.dbCtx.SaveChanges();                
                RaisePropertyChanged("Order");
                RaisePropertyChanged("Sub");
                RaisePropertyChanged("Raiting");

                
                if (value != null)
                {
                    
                    var sub = subs.FirstOrDefault((x) => x.Sub == Order.Sub);
                    if (sub != null)
                    {
                        sub.Orders.Add(Order);
                    }
                }

            }
        }

        public SubscriberViewModel Sub
        {
            get
            {
                if (Order == null)
                    return null;
                else
                    return subs.FirstOrDefault((x) => x.Sub == Order.Sub);
            }
            set
            {
                if (value == null)                
                    Order = null;
                else
                    Order = new Order { Book = book, Sub = value.Sub, OrderDate = DateTime.Now };

            }
        }


    }
}
