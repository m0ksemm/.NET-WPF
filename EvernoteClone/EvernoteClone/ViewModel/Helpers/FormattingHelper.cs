using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Helpers
{
    public class FormattingHelper
    {
        public static void ToggleBold(RichTextBox rtb)
        {
            if (rtb.Selection != null)
            {
                var weight = rtb.Selection.GetPropertyValue(TextElement.FontWeightProperty);
                if (weight != DependencyProperty.UnsetValue && (FontWeight)weight == FontWeights.Bold)
                    rtb.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                else
                    rtb.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }

        public static void ToggleItalic(RichTextBox rtb)
        {
            if (rtb.Selection != null)
            {
                var style = rtb.Selection.GetPropertyValue(TextElement.FontStyleProperty);
                if (style != DependencyProperty.UnsetValue && (FontStyle)style == FontStyles.Italic)
                    rtb.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                else
                    rtb.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }

        public static void ToggleUnderline(RichTextBox rtb)
        {
            if (rtb.Selection != null)
            {
                var deco = rtb.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                if (deco != DependencyProperty.UnsetValue && deco == TextDecorations.Underline)
                    rtb.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                else
                    rtb.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        public static void ApplyFontFamily(RichTextBox rtb, string fontFamily)
        {
            if (!string.IsNullOrEmpty(fontFamily))
                rtb.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, fontFamily);
        }

        public static void ApplyFontSize(RichTextBox rtb, double fontSize)
        {
            if (fontSize > 0)
                rtb.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
        }
    }
}
