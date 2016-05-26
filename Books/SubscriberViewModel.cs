using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Books
{
    
    public class SubscriberViewModel : ViewModelBase
    {

        readonly Sub sub;
        readonly ObservableCollection<Order> orders = new ObservableCollection<Order>();
        readonly public ObservableCollection<BookViewModel> books;
        readonly public ObservableCollection<SubscriberViewModel> subs;

        public SubscriberViewModel(Sub sub_, ObservableCollection<BookViewModel> books_,
            ObservableCollection<SubscriberViewModel> subs_)
        {
            sub = sub_;
            books = books_;
            subs = subs_;
            foreach (var o in sub.Orders)
            {
                orders.Add(o);
            }
            
        }
        public ObservableCollection<Order> Orders
        {
            get
            {
                return orders;
            }
        }
        public Sub Sub
        {
            get
            {
                return sub;
            }
        }

        public ICommand Remove
        {
            get
            {
                return Command.create(() =>
                {
                    
                    foreach(var b in books)
                    {
                        if (b.Sub!=null && b.Sub.Sub == sub)
                        {
                            b.Order = null;
                        }
                    }
                    subs.Remove(this);

                    sub.Orders.Clear();
                    DbCtx.dbCtx.Subs.Remove(sub);
                    DbCtx.dbCtx.SaveChanges();
                    
                }); 
            }
        }
    }
}
