using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Text.RegularExpressions;

namespace EDS.MVVM.Behaviors
{
    public class TextBoxInputRegExBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(TextBoxInputRegExBehavior), new FrameworkPropertyMetadata(int.MaxValue));

        public static readonly DependencyProperty RegularExpressionProperty =
            DependencyProperty.Register("RegularExpression", typeof(string), typeof(TextBoxInputRegExBehavior), null);

        public int MaxLength
        {
            get
            {
                return (int)GetValue(MaxLengthProperty);
            }
            set
            {
                SetValue(MaxLengthProperty, value);
            }
        }

        public string RegularExpression
        {
            get { return (string)GetValue(RegularExpressionProperty); }
            set { SetValue(RegularExpressionProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.LostFocus += OnLostFocus;
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            DataObject.AddPastingHandler(AssociatedObject, OnPaste);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.LostFocus -= OnLostFocus;
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
        }

        void OnLostFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox != null)
            {

            }
        }

        void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(e.Text, false);
        }

        void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                var text = Convert.ToString(e.DataObject.GetData(DataFormats.Text));

                if (!IsValid(text, true))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool IsValid(string newText, bool paste)
        {
            var isValid = true;

            if (!string.IsNullOrWhiteSpace(RegularExpression))
            {
                isValid = Regex.IsMatch(newText, RegularExpression);
            }

            if (isValid && MaxLength > 0)
            {
                isValid = !ExceedsMaxLength(newText, paste);
            }

            return isValid;
        }

        private bool ExceedsMaxLength(string newText, bool paste)
        {
            var exceedsMaxLength = false;

            if (MaxLength > 0)
            {
                exceedsMaxLength = ModifiedTextLength(newText, paste) > MaxLength;
            }

            return exceedsMaxLength;
        }

        private int ModifiedTextLength(string newText, bool paste)
        {
            var modifiedTextLength = 0;

            var selectedCharCount = AssociatedObject.SelectedText.Length;
            var caretIndex = AssociatedObject.CaretIndex;
            var text = AssociatedObject.Text;

            if (selectedCharCount > 0 || paste)
            {
                text = text.Remove(caretIndex, selectedCharCount);
                modifiedTextLength = text.Length + newText.Length;
            }
            else
            {
                var isInsertToggled = Keyboard.IsKeyToggled(Key.Insert);

                modifiedTextLength = (isInsertToggled && caretIndex < text.Length)
                                                        ? text.Length
                                                        : text.Length + newText.Length;
            }

            return modifiedTextLength;
        }
    }

}
