using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using Microsoft.Xaml.Behaviors;

namespace CuisineCompanion.Common.Behavior;

public class ContentChangedBehavior : Behavior<UIElement>
{
    private readonly Action<object?, EventArgs?, object?>? _contentChangedAction;

    public ContentChangedBehavior(Action<object?, EventArgs?, object?>? contentChangedAction)
    {
        _contentChangedAction = contentChangedAction;
    }

    private void AssociatedObject_ContentChanged(object? sender, EventArgs? args)
    {
        if (_contentChangedAction is null) return;
        object? obj = null;
        switch (AssociatedObject)
        {
            // 删除任何内容
            case TextBox textBox:
                obj = textBox.Text;
                break;
            case PasswordBox passwordBox:
                obj = passwordBox.Password;
                break;
            case ComboBox comboBox:
                obj = comboBox.SelectedItem;
                break;
            case DatePicker datePicker:
                obj = datePicker.SelectedDate;
                break;
            case CheckBox checkBox:
                obj = checkBox.IsChecked;
                break;
            case RadioButton radioButton:
                obj = radioButton.IsChecked;
                break;
            case ToggleButton toggleButton:
                obj = toggleButton.IsChecked;
                break;
            case Slider slider:
                obj = slider.Value;
                break;
            case ProgressBar progressBar:
                obj = progressBar.Value;
                break;
            case ListBox listBox:
                obj = listBox.SelectedItem;
                break;
            case TabControl tabControl:
                obj = tabControl.SelectedItem;
                break;
            case DataGrid dataGrid:
                obj = dataGrid.SelectedItem;
                break;
            case RichTextBox richTextBox:
                obj = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                break;
            default:
                return;
        }

        _contentChangedAction(sender, args, obj);
    }

// @formatter:off
    protected override void OnAttached()
    {
        if (_contentChangedAction is null) return;
        switch (AssociatedObject)
        {
            // 检测改变事件
            case TextBox textBox:
                textBox.TextChanged            += AssociatedObject_ContentChanged;
                break;
            case PasswordBox passwordBox:
                passwordBox.PasswordChanged    += AssociatedObject_ContentChanged;
                break;
            case ComboBox comboBox:
                comboBox.SelectionChanged      += AssociatedObject_ContentChanged;
                break;
            case DatePicker datePicker:
                datePicker.SelectedDateChanged += AssociatedObject_ContentChanged;
                break;
            case CheckBox checkBox:
                checkBox.Checked               += AssociatedObject_ContentChanged;
                checkBox.Unchecked             += AssociatedObject_ContentChanged;
                break;
            case RadioButton radioButton:
                radioButton.Checked            += AssociatedObject_ContentChanged;
                radioButton.Unchecked          += AssociatedObject_ContentChanged;
                break;
            case ToggleButton toggleButton:
                toggleButton.Checked           += AssociatedObject_ContentChanged;
                toggleButton.Unchecked         += AssociatedObject_ContentChanged;
                break;
            case Slider slider:
                slider.ValueChanged            += AssociatedObject_ContentChanged;
                break;
            case ProgressBar progressBar:
                progressBar.ValueChanged       += AssociatedObject_ContentChanged;
                break;
            case ListBox listBox:
                listBox.SelectionChanged       += AssociatedObject_ContentChanged;
                break;
            case TabControl tabControl:
                tabControl.SelectionChanged    += AssociatedObject_ContentChanged;
                break;
            case DataGrid dataGrid:
                dataGrid.SelectionChanged      += AssociatedObject_ContentChanged;
                break;
            case RichTextBox richTextBox:
                richTextBox.TextChanged        += AssociatedObject_ContentChanged;
                break;
            default:
                return;
        }
    }


    protected override void OnDetaching()
    {
        if (_contentChangedAction is null) return;
        switch (AssociatedObject)
        {
            // 检测改变事件
            case TextBox textBox:
                textBox.TextChanged            -= AssociatedObject_ContentChanged;
                break;
            case PasswordBox passwordBox:
                passwordBox.PasswordChanged    -= AssociatedObject_ContentChanged;
                break;
            case ComboBox comboBox:
                comboBox.SelectionChanged      -= AssociatedObject_ContentChanged;
                break;
            case DatePicker datePicker:
                datePicker.SelectedDateChanged -= AssociatedObject_ContentChanged;
                break;
            case CheckBox checkBox:
                checkBox.Checked               -= AssociatedObject_ContentChanged;
                checkBox.Unchecked             -= AssociatedObject_ContentChanged;
                break;
            case RadioButton radioButton:
                radioButton.Checked            -= AssociatedObject_ContentChanged;
                radioButton.Unchecked          -= AssociatedObject_ContentChanged;
                break;
            case ToggleButton toggleButton:
                toggleButton.Checked           -= AssociatedObject_ContentChanged;
                toggleButton.Unchecked         -= AssociatedObject_ContentChanged;
                break;
            case Slider slider:
                slider.ValueChanged            -= AssociatedObject_ContentChanged;
                break;
            case ProgressBar progressBar:
                progressBar.ValueChanged       -= AssociatedObject_ContentChanged;
                break;
            case ListBox listBox:
                listBox.SelectionChanged       -= AssociatedObject_ContentChanged;
                break;
            case TabControl tabControl:
                tabControl.SelectionChanged    -= AssociatedObject_ContentChanged;
                break;
            case DataGrid dataGrid:
                dataGrid.SelectionChanged      -= AssociatedObject_ContentChanged;
                break;
            case RichTextBox richTextBox:
                richTextBox.TextChanged        -= AssociatedObject_ContentChanged;
                break;
            default:
                return;
        }
    }
// @formatter:on
}