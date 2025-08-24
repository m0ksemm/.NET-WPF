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
        #region DependencyProperties

        public bool IsBold
        {
            get => (bool)GetValue(IsBoldProperty);
            set => SetValue(IsBoldProperty, value);
        }
        public static readonly DependencyProperty IsBoldProperty =
            DependencyProperty.Register(nameof(IsBold), typeof(bool), typeof(RichTextBoxCommandBehavior), new PropertyMetadata(false));

        public bool IsItalic
        {
            get => (bool)GetValue(IsItalicProperty);
            set => SetValue(IsItalicProperty, value);
        }
        public static readonly DependencyProperty IsItalicProperty =
            DependencyProperty.Register(nameof(IsItalic), typeof(bool), typeof(RichTextBoxCommandBehavior), new PropertyMetadata(false));

        public bool IsUnderline
        {
            get => (bool)GetValue(IsUnderlineProperty);
            set => SetValue(IsUnderlineProperty, value);
        }
        public static readonly DependencyProperty IsUnderlineProperty =
            DependencyProperty.Register(nameof(IsUnderline), typeof(bool), typeof(RichTextBoxCommandBehavior), new PropertyMetadata(false));

        #endregion

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
            var rtb = AssociatedObject;
            if (rtb == null) return;

            TextRange tr = new TextRange(rtb.Selection.Start, rtb.Selection.End);

            // Bold
            object w = tr.GetPropertyValue(TextElement.FontWeightProperty);
            IsBold = (w != DependencyProperty.UnsetValue && (FontWeight)w == FontWeights.Bold);

            // Italic
            object s = tr.GetPropertyValue(TextElement.FontStyleProperty);
            IsItalic = (s != DependencyProperty.UnsetValue && (FontStyle)s == FontStyles.Italic);

            // Underline
            object d = tr.GetPropertyValue(Inline.TextDecorationsProperty);
            IsUnderline = (d != DependencyProperty.UnsetValue && d is TextDecorationCollection tdc && tdc == TextDecorations.Underline);
        }
    }
}
