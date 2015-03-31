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
using System.Threading;

namespace CPT_TCP_win
{
    class DebugTools
    {
        Window w;
        StackPanel stackPanel;
        static TextBox txt;
        public DebugTools()
        {
            w = new Window();
            stackPanel = new StackPanel { Orientation = Orientation.Vertical };
            txt = new TextBox();
            stackPanel.Height = 500;
            stackPanel.Width = 500;
            txt.Height = stackPanel.Height;
            txt.Width = stackPanel.Width;
            stackPanel.Children.Add(txt);
            txt.AppendText("DEBUG: \n");
            w.Content = stackPanel;
            w.Show();
        }
        public static void setText(string msg)
        {
            txt.Text = msg;
        }
        public static void append(string s)
        {
            txt.Text += s;
        }
    }
}
