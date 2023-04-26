namespace TestMauiMap.Controls.Buttons;

public partial class Tag : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(Tag), string.Empty);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }


    public event EventHandler Clicked;

    public Tag()
	{
		InitializeComponent();
	}

    private void StateButton_Clicked(object sender, EventArgs e)
    {
        Clicked?.Invoke(this, EventArgs.Empty);
    }
}