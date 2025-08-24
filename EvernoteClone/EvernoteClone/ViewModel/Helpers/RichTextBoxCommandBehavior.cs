using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace EvernoteClone.ViewModel.Helpers
{
    public class RichTextBoxCommandBehavior : Behavior<RichTextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
            base.OnDetaching();
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            // Тут можна оновлювати DP або викликати команди, якщо захочеш.
            // Напр., зчитати поточні стилі вибірки:
            var rtb = AssociatedObject;
            if (rtb == null) return;

            TextRange tr = new TextRange(rtb.Selection.Start, rtb.Selection.End);

            // приклад читання властивостей (на майбутнє):
            object w = tr.GetPropertyValue(TextElement.FontWeightProperty);
            bool isBold = w != DependencyProperty.UnsetValue && (FontWeight)w == FontWeights.Bold;

            object s = tr.GetPropertyValue(TextElement.FontStyleProperty);
            bool isItalic = s != DependencyProperty.UnsetValue && (FontStyle)s == FontStyles.Italic;

            object d = tr.GetPropertyValue(Inline.TextDecorationsProperty);
            bool isUnderline = d != DependencyProperty.UnsetValue && d is TextDecorationCollection tdc && tdc == TextDecorations.Underline;

            // за необхідності – пробросити ці значення кудись через DependencyProperty
        }
    }
}
