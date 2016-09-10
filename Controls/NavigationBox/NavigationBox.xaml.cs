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

namespace Controls.NavigationBox
{
    /// <summary>
    /// NavigationBox.xaml 的交互逻辑
    /// </summary>
    public partial class NavigationBox : UserControl
    {
        private const int GRAY_INCREMENT = 30;
        private List<NavigationItem> Items = new List<NavigationItem>();
        private double LastTopValue = 0;
        private NavigationItem SelectedItem;
        public delegate void GridNavigation(Grid newGrid, Grid oldGrid, NavigationBox sender);
        public GridNavigation NavigationEvent;

        public void AddItem(NavigationItem item)
        {
            item.Width = this.Width;
            item.HideRect();
            item.MouseEnter += (sender, e) =>
            {
                if (sender as NavigationItem == SelectedItem) return;
                (sender as NavigationItem).ShowRect();
            };
            item.MouseLeave += (sender, e) =>
            {
                if (sender as NavigationItem == SelectedItem) return;
                (sender as NavigationItem).HideRect();
            };
            item.MouseUp += OnItemClicked;
            item.Margin = new Thickness(0, LastTopValue, 0, this.Height - LastTopValue - item.Height);
            LastTopValue += item.Height;

            Items.Add(item);
            ItemsGird.Children.Add(item);

            if(SelectedItem == null)
                NavigateToItem(item);
        }

        private void OnItemClicked(object sender, EventArgs e)
        {
            if (sender as NavigationItem == SelectedItem) return;
            NavigateToItem(sender as NavigationItem);
        }

        private void NavigateToItem(NavigationItem newItem)
        {
            if (SelectedItem != null)
            {
                if(SelectedItem.Page != null)
                    SelectedItem.Page.Visibility = Visibility.Collapsed;
                SelectedItem.HideRect();
                SelectedItem.BackColorOpacity = newItem.BackColorOpacity - 0.2 < 0 ? 0 : newItem.BackColorOpacity - 0.2;
            }
            if (newItem != null)
            {
                if(newItem.Page != null)
                    newItem.Page.Visibility = Visibility.Visible;
                newItem.ShowRect();
                newItem.BackColorOpacity = newItem.BackColorOpacity + 0.2 > 1 ? 1 : newItem.BackColorOpacity + 0.2;
            }
            SelectedItem = newItem;
        }
        
        private void NavigationGrid(Grid newGrid, Grid oldGrid, NavigationBox sender)
        {
            if(newGrid!=null)
                newGrid.Visibility = Visibility.Visible;
            if (oldGrid != null)
                oldGrid.Visibility = Visibility.Collapsed;
        }

        public NavigationBox()
        {
            InitializeComponent();
            NavigationEvent = NavigationGrid;
        }
    }
}
