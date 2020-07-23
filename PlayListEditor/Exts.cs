using System;
using System.Windows.Forms;

namespace PlayListEditor
{
    public class CustomListView : ListView
    {
        public event EventHandler<CustomEventArgs> UpdateListViewCounts;

        public void ClearList()
        {
            this.Items.Clear();
            CustomEventArgs e = new CustomEventArgs(Items.Count);
            UpdateListViewCounts(this, e);
        }
        public void AddItem(ListViewItem item)
        {
            // You may have to modify this depending on the
            // Complexity of your Items
            this.Items.Add(item);
            CustomEventArgs e = new CustomEventArgs(Items.Count);
            UpdateListViewCounts(this, e);
        }
        public void RemoveItem(ListViewItem item)
        {
            // You may have to modify this depending on the
            // Complexity of your Items
            this.Items.Remove(item);
            CustomEventArgs e = new CustomEventArgs(Items.Count);
            UpdateListViewCounts(this, e);
        }
    }
    public class CustomEventArgs : EventArgs
    {
        private int _count;
        public CustomEventArgs(int count)
        {
            _count = count;
        }
        public int Count
        {
            get { return _count; }
        }
    }
}
