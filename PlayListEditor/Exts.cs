using System;
using System.Windows.Forms;

namespace ExtensionMethods
{
    public static class Extensions 
    {
        public enum MoveDirection { Up = -1, Down = 1 };
        
        internal static bool MoveListviewItems(ListView sender, MoveDirection direction)
        {
            int dir = (int)direction;

            bool valid = sender.SelectedItems.Count > 0 &&
                            ((direction == MoveDirection.Down && (sender.SelectedItems[sender.SelectedItems.Count - 1].Index < sender.Items.Count - 1))
                        || (direction == MoveDirection.Up && (sender.SelectedItems[0].Index > 0)));

            if (valid)
            {
                foreach (ListViewItem item in sender.SelectedItems)
                {
                    int index = item.Index + dir;
                    sender.Items.RemoveAt(item.Index);
                    sender.Items.Insert(index, item);
                }
            }
            return valid;
        }
    }
}
