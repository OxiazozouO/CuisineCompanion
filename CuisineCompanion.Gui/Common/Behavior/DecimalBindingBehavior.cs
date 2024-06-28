using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CuisineCompanion.Common.Converter;
using Microsoft.Xaml.Behaviors;

namespace CuisineCompanion.Common.Behavior;

public class DecimalBindingBehavior : Behavior<TextBox>
{
    private readonly DecimalConverter _converter = new();

    public decimal MaxValue
    {
        set => _converter.MaxValue = value;
        get => _converter.MaxValue;
    }

    public decimal MinValue
    {
        set => _converter.MinValue = value;
        get => _converter.MinValue;
    }

    public bool IsPositive
    {
        set => _converter.IsPositive = value;
        get => _converter.IsPositive;
    }

    protected override void OnAttached()
    {
        AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        AssociatedObject.LostFocus += AssociatedObjectOnLostFocus;
        AssociatedObject.Loaded += AssociatedObjectOnLoaded;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
        AssociatedObject.LostFocus -= AssociatedObjectOnLostFocus;
        AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
    }

    private void AssociatedObject_TextChanged(object sender, RoutedEventArgs e)
    {
        var tb = AssociatedObject;
        var text = tb.Text;
        if (!_converter.FilterInput(text, DecimalBindingEx.GetText(tb), out text)) return;

        var ind = tb.CaretIndex;
        tb.Text = text;
        tb.CaretIndex = ind;
    }


    private void AssociatedObjectOnLostFocus(object sender, RoutedEventArgs e)
    {
        var text = _converter.FormatString(AssociatedObject.Text);
        DecimalBindingEx.SetText(AssociatedObject, text);
        AssociatedObject.Text = DecimalBindingEx.GetText(AssociatedObject);
    }

    private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs e)
    {
        var existingBindingExpression = AssociatedObject.GetBindingExpression(TextBox.TextProperty);
        if (existingBindingExpression?.ParentBinding is { } b)
        {
            // 创建一个新的绑定
            // @formatter:off
            var newBinding = new Binding
            {
                Mode                        = b.Mode,
                Path                        = b.Path,
                XPath                       = b.XPath,
                Delay                       = b.Delay,
                IsAsync                     = b.IsAsync,
                AsyncState                  = b.AsyncState,
                Converter                   = _converter,
                ElementName                 = b.ElementName,
                StringFormat                = b.StringFormat,
                FallbackValue               = b.FallbackValue,
                TargetNullValue             = b.TargetNullValue,
                BindingGroupName            = b.BindingGroupName,
                ConverterCulture            = b.ConverterCulture,
                ConverterParameter          = b.ConverterParameter,
                UpdateSourceTrigger         = b.UpdateSourceTrigger,
                NotifyOnSourceUpdated       = b.NotifyOnSourceUpdated,
                NotifyOnTargetUpdated       = b.NotifyOnTargetUpdated,
                ValidatesOnDataErrors       = b.ValidatesOnDataErrors,
                ValidatesOnExceptions       = b.ValidatesOnExceptions,
                BindsDirectlyToSource       = b.BindsDirectlyToSource,
                NotifyOnValidationError     = b.NotifyOnValidationError,
                UpdateSourceExceptionFilter = b.UpdateSourceExceptionFilter,
                ValidatesOnNotifyDataErrors = b.ValidatesOnNotifyDataErrors

            };
            // @formatter:on
            // 应用新的绑定
            AssociatedObject.SetBinding(TextBox.TextProperty, newBinding);
        }
    }
}