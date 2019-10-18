using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using CefSharp;
using CefSharp.Wpf;

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
                if (!(o is ChromiumWebBrowser webBrowser))
                {
                    return;
                }

                string html = e.NewValue as string;
                if (webBrowser.Dispatcher.CheckAccess())
                {
                    webBrowser.LoadHtml(html);
                }
                else
                {
                    webBrowser.Dispatcher.BeginInvoke(new Action(() => webBrowser.LoadHtml(html)));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
