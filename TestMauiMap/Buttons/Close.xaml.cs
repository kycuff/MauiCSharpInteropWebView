using System.Windows.Input;
using IeuanWalker.Maui.StateButton;

namespace TestMauiMap.Controls.Buttons;

public partial class Close : ContentView
{
    #region Events

    public event EventHandler Clicked;

    #endregion Events

    #region Commands

    public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(NextBack), null);

    public ICommand ClickedCommand
    {
        get => (ICommand)GetValue(ClickedCommandProperty);
        set => SetValue(ClickedCommandProperty, value);
    }

    /// <summary>
    /// Backing BindableProperty for the <see cref="ClickedCommandParameter"/> property.
    /// </summary>
    public static readonly BindableProperty ClickedCommandParameterProperty = BindableProperty.Create(nameof(ClickedCommandParameter), typeof(object), typeof(CallOut));

    /// <summary>
    /// Property that gets returned when  <see cref="ClickedCommand" /> is executed. This is a bindable property.
    /// </summary>
    public object ClickedCommandParameter
    {
        get => GetValue(ClickedCommandParameterProperty);
        set => SetValue(ClickedCommandParameterProperty, value);
    }
    #endregion Commands
    public Close()
    {
        InitializeComponent();
    }

    private void StateButton_Clicked(object sender, EventArgs e)
    {
        Clicked?.Invoke(this, EventArgs.Empty);
        ClickedCommand?.Execute(ClickedCommandParameter);
    }
}