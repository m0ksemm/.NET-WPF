using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace EvernoteClone.ViewModel.Helpers
{
    public static class RichTextBoxHelper
    {
        private static bool _isInternalUpdate; // захист від рекурсії

        public static readonly DependencyProperty BoundRtfProperty =
            DependencyProperty.RegisterAttached(
                "BoundRtf",
                typeof(string),
                typeof(RichTextBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundRtfChanged));

        public static string GetBoundRtf(DependencyObject obj)
        {
            return (string)obj.GetValue(BoundRtfProperty);
        }

        public static void SetBoundRtf(DependencyObject obj, string value)
        {
            obj.SetValue(BoundRtfProperty, value);
        }

        private static void OnBoundRtfChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox rtb)
            {
                rtb.TextChanged -= RichTextBox_TextChanged;

                var newText = e.NewValue as string;

                _isInternalUpdate = true;

                // 🟢 отримуємо поточний RTF з RichTextBox
                var currentRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                using var currentStream = new MemoryStream();
                currentRange.Save(currentStream, DataFormats.Rtf);
                string currentRtf;
                currentStream.Position = 0;
                using (var reader = new StreamReader(currentStream))
                    currentRtf = reader.ReadToEnd();

                // якщо текст такий самий — нічого не робимо
                if (currentRtf != newText)
                {
                    if (string.IsNullOrEmpty(newText))
                    {
                        rtb.Document = new FlowDocument();
                    }
                    else
                    {
                        try
                        {
                            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(newText));
                            var range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                            range.Load(stream, DataFormats.Rtf);
                        }
                        catch
                        {
                            rtb.Document = new FlowDocument(new Paragraph(new Run(newText)));
                        }
                    }
                }

                _isInternalUpdate = false;

                rtb.TextChanged += RichTextBox_TextChanged;
            }
        }

        private static void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isInternalUpdate) return; // ⛔ не оновлюємо VM, якщо це було викликано VM

            if (sender is RichTextBox rtb)
            {
                var range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);

                using var stream = new MemoryStream();
                range.Save(stream, DataFormats.Rtf);
                stream.Position = 0;

                using var reader = new StreamReader(stream);
                string rtf = reader.ReadToEnd();

                // 🟢 це оновлення йде з UI → VM
                SetBoundRtf(rtb, rtf);
            }
        }
    }
}