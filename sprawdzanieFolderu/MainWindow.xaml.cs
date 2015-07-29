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

namespace sprawdzanieFolderu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DirectoryMonitor dirMonitor;
        public MainWindow()
        {
            
            InitializeComponent();
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Name";
            col1.Binding = new Binding("Name");
            GridItemList.Columns.Add(col1);
            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Address";
            col2.Binding = new Binding("Address");
            GridItemList.Columns.Add(col2);
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            dirMonitor = new DirectoryMonitor(addressBox.Text, endBox.Text);
            dirMonitor.Files.CollectionChanged += Files_CollectionChanged;
            GridRefresh();
        }

        void Files_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            GridRefresh();
            EventListRefresh();
        }

        private void GridRefresh()
        {
            GridItemList.Dispatcher.Invoke((Action)(() => GridItemList.Items.Clear()));
            if (dirMonitor.Files != null)
            {
                foreach (FileToMonitor item in dirMonitor.Files)
                {
                    GridItemList.Dispatcher.Invoke((Action)(() => GridItemList.Items.Add(new FileToMonitor(item.Name, item.Address) { Name = item.Name, Address = item.Address })));
                }
            }
        }

        private void EventListRefresh()
        {
            EventListBox.Dispatcher.Invoke((Action)(() => EventListBox.Text=dirMonitor.whatHappened));
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            dirMonitor.StopWatch();
        }

        
    }
}
