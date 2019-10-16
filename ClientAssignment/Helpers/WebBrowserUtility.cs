using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ClientAssignment.Helpers
{
    public static class WebBrowserUtility
    {
        public static readonly DependencyProperty BindableHTMLProperty =
            DependencyProperty.RegisterAttached("BindableHTML",
                typeof(string),
                typeof(WebBrowserUtility),
                new FrameworkPropertyMetadata(null, BindableHTMLPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetBindableHTML(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableHTMLProperty);
        }

        public static void SetBindableHTML(DependencyObject obj, string value)
        {
            obj.SetValue(BindableHTMLProperty, value);
        }

        public static void BindableHTMLPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (!(o is WebBrowser webBrowser))
                {
                    return;
                }

                webBrowser.NavigateToString(e.NewValue as string ?? "&nbsp;");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
