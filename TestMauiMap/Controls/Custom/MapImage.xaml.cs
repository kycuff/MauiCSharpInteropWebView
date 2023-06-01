using System.Runtime.CompilerServices;

namespace TestMauiMap.Controls.Custom;

public partial class MapImage : ContentView
{       
    #region properties
        public static readonly BindableProperty EastingProperty = BindableProperty.Create(nameof(Easting), typeof(double), typeof(MapImage), 0d, BindingMode.OneWay);
        public double Easting
        {
            get => (double)GetValue(EastingProperty);
            set => SetValue(EastingProperty, value);
        }

        public static readonly BindableProperty NorthingProperty = BindableProperty.Create(nameof(Northing), typeof(double), typeof(MapImage), 0d, BindingMode.OneWay);
        public double Northing
        {
            get => (double)GetValue(NorthingProperty);
            set => SetValue(NorthingProperty, value);
        }

        public static readonly BindableProperty ZoomProperty = BindableProperty.Create(nameof(Zoom), typeof(int), typeof(MapImage), 150, BindingMode.OneWay);
        public int Zoom
        {
            get => (int)GetValue(ZoomProperty);
            set => SetValue(ZoomProperty, value);
        }

    #endregion

	public MapImage()
	{
		InitializeComponent();
		Image.Source = ImageSource.FromUri(new Uri(GenerateImageLink()));
	}

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (!propertyName.Equals(nameof(Easting)) && !propertyName.Equals(nameof(Northing)) && !propertyName.Equals(nameof(Zoom))) return;
        if (Image != null)
        {
            Image.Source = ImageSource.FromUri(new Uri(GenerateImageLink()));
        }
    }

    private string GenerateImageLink()
    {
        return $"insertURL.jpg";
    }
}