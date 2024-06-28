using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Xaml.Behaviors;

namespace CuisineCompanion.Common.Behavior;

public class ClearContentBehavior : Behavior<UIElement>
{
    public static readonly DependencyProperty TargetProperty =
        DependencyProperty.Register(nameof(Target), typeof(DispatcherObject), typeof(ClearContentBehavior),
            new PropertyMetadata(null));

    public bool IsClearClose { get; set; }

    public DispatcherObject Target
    {
        get => (DispatcherObject)GetValue(TargetProperty);
        set => SetValue(TargetProperty, value);
    }

    protected override void OnAttached()
    {
        AssociatedObject.MouseDown += AssociatedObject_MouseDown;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
    }

    private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs args)
    {
        switch (Target)
        {
            //删除任何内容
            case TextBox textBox:
                textBox.Clear();
                break;
            case PasswordBox passwordBox:
                passwordBox.Clear();
                break;
            case ComboBox comboBox:
                comboBox.SelectedIndex = -1;
                break;
            case DatePicker datePicker:
                datePicker.SelectedDate = null;
                break;
            case CheckBox checkBox:
                checkBox.IsChecked = false;
                break;
            case RadioButton radioButton:
                radioButton.IsChecked = false;
                break;
            case ToggleButton toggleButton:
                toggleButton.IsChecked = false;
                break;
            case Slider slider:
                slider.Value = 0;
                break;
            case ProgressBar progressBar:
                progressBar.Value = 0;
                break;
            case ListBox listBox:
                listBox.SelectedIndex = -1;
                break;
            case TabControl tabControl:
                tabControl.SelectedIndex = -1;
                break;
            case DataGrid dataGrid:
                dataGrid.SelectedIndex = -1;
                break;
            case RichTextBox richTextBox:
                richTextBox.Document.Blocks.Clear();
                break;
            case TextBlock textBlock:
                textBlock.Text = string.Empty;
                break;
            default:
                return;
        }

        if (IsClearClose) AssociatedObject.Visibility = Visibility.Collapsed;
    }
}