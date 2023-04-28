using GeoUK.Coordinates;
using SkiaSharp.Extended.UI.Controls;
using System.Diagnostics;
using TestMauiMap.Exceptions;
using TestMauiMap.Services.GeoLocation;

namespace TestMauiMap.Pages;

public partial class iShareTestPage : ContentPage
{
    #region Properties

    public static readonly BindableProperty LayersProperty = BindableProperty.Create(nameof(Layers), typeof(string), typeof(Map), "cardiffcounty", BindingMode.OneTime);

    public string Layers
    {
        get => (string)GetValue(LayersProperty);
        set => SetValue(LayersProperty, value);
    }

    public static readonly BindableProperty DefaultEastingProperty = BindableProperty.Create(nameof(DefaultEasting), typeof(double?), typeof(Map), null, BindingMode.OneTime);

    public double? DefaultEasting
    {
        get => (double?)GetValue(DefaultEastingProperty);
        set => SetValue(DefaultEastingProperty, value);
    }

    public static readonly BindableProperty DefaultNorthingProperty = BindableProperty.Create(nameof(DefaultNorthing), typeof(double?), typeof(Map), null, BindingMode.OneTime);

    public double? DefaultNorthing
    {
        get => (double?)GetValue(DefaultNorthingProperty);
        set => SetValue(DefaultNorthingProperty, value);
    }

    public static readonly BindableProperty DefaultZoomProperty = BindableProperty.Create(nameof(DefaultZoom), typeof(int), typeof(Map), 400, BindingMode.OneTime);

    public int DefaultZoom
    {
        get => (int)GetValue(DefaultZoomProperty);
        set => SetValue(DefaultZoomProperty, value);
    }

    public static readonly BindableProperty MapTypeProperty = BindableProperty.Create(nameof(MapMode), typeof(MapModeEnum), typeof(Map), MapModeEnum.SelectLocation, BindingMode.OneTime);

    public MapModeEnum MapMode
    {
        get => (MapModeEnum)GetValue(MapTypeProperty);
        set => SetValue(MapTypeProperty, value);
    }

    public static readonly BindableProperty ShowMapHelpButtonProperty = BindableProperty.Create(nameof(ShowMapHelpButton), typeof(bool), typeof(Map), true, BindingMode.OneTime);

    public bool ShowMapHelpButton
    {
        get => (bool)GetValue(ShowMapHelpButtonProperty);
        set => SetValue(ShowMapHelpButtonProperty, value);
    }

    public static readonly BindableProperty AccessibleMapNameProperty = BindableProperty.Create(nameof(AccessibleMapName), typeof(string), typeof(Map), "put this in resx", BindingMode.OneTime);

    public string AccessibleMapName
    {
        get => (string)GetValue(AccessibleMapNameProperty);
        set => SetValue(AccessibleMapNameProperty, value);
    }

    #endregion Properties

    #region Events

    public event EventHandler SkipMapClicked;

    public event EventHandler MapKeyClicked;

    #endregion Events

    private readonly IGeoLocationService _geoLocationService;
    //private readonly IAccessibilityService _accessibilityService;
    //private readonly ILoadingService _loadingService;

    public iShareTestPage()
    {
        InitializeComponent();

        _geoLocationService = new GeoLocationService();
        //_accessibilityService = DependencyService.Get<IAccessibilityService>();
        //_loadingService = new LoadingService();

        if (MapKeyClicked != null)
        {
            MapKeyBtn.IsVisible = true;
        }
    }

    protected override void OnDisappearing()
    {
        //IshareView.RemoveAllLocalCallbacks();
        Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
    }

    protected override void OnAppearing()
    {
        if (MapMode == MapModeEnum.Static)
        {
            MoveMapMessage.IsVisible = false;
        }
        else
        {
            IshareView.AddLocalCallback(this, nameof(MapMovedCSharp));
        }

        IshareView.AddLocalCallback(this, nameof(DOMContentLoadedCSharp));
        IshareView.AddLocalCallback(this, nameof(MapLoadedCSharp));
        IshareView.AddLocalCallback(this, nameof(CoordinatesCSharp));


        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            //LoaderAnimation.Animation = "NoConnection.json";
            LoaderAnimation.RepeatCount = -1;
            NoConnection.IsVisible = true;
            LoaderAnimation.HeightRequest = 150;
            LoaderAnimation.WidthRequest = 150;
        }

        Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
    }

    //public void SetFocusToMap()
    //{
    //    _accessibilityService.SetFocus(MapContainer).SafeFireAndForget();
    //}

    private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        {
            //LoaderAnimation.Source = "Location.json";
            LoaderAnimation.RepeatCount = -1;
            LoaderBackground.IsVisible = true;
            NoConnection.IsVisible = false;
            LoaderAnimation.HeightRequest = 300;
            LoaderAnimation.WidthRequest = 300;

            //IshareView.Refresh();
        }
        else
        {
            //LoaderAnimation.Source = "NoConnection.json";
            LoaderAnimation.RepeatCount = -1;
            LoaderBackground.IsVisible = true;
            NoConnection.IsVisible = true;
            LoaderAnimation.HeightRequest = 150;
            LoaderAnimation.WidthRequest = 150;
            LoaderBackground.Opacity = 1;
            LoaderAnimation.Opacity = 1;
            LoaderAnimation.Scale = 1;
        }
    }

    private double? Easting { get; set; }
    private double? Northing { get; set; }

    /// <summary>
    /// The hybrid WebView only supports one way communication to get around this,
    /// this method triggers a JS function, then waits for  goto:Easting goto:Northing properties to be populated
    ///
    /// The JS function then calls the goto:CoordinatesCSharp method which updates goto:Easting goto:Northing
    /// </summary>
    public async Task<EastingNorthing> GetLocation()
    {
        Easting = null; Northing = null;// Trigger JS to get location
        var result = await IshareView.InvokeJsMethodAsync("getLocation").ConfigureAwait(true);

        if(result is not null)
        {
            string[] eastNorth = result.Split(',');
            Easting = double.Parse(eastNorth[0]);
            Northing = double.Parse(eastNorth[1]);
        }
        
        return new EastingNorthing(Easting ?? 0, Northing ?? 0);
    }

        private void CoordinatesCSharp(double? easting, double? northing)
    {
        if (easting is null || northing is null)
        {
            return;
        }
        Easting = easting;
        Northing = northing;
    }


    private async Task DOMContentLoadedCSharp()
    {
        MainThread.BeginInvokeOnMainThread(async () => await IshareView.InvokeJsMethodAsync("loadmap", Layers).ConfigureAwait(true));
    }

    private void MapLoadedCSharp()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {

            await IshareView.EvaluateJavaScriptAsync($"loadmap('{Layers}')").ConfigureAwait(true);
            // Page load animation
            Animation loadingAnimation = new Animation
                {
                    { 0, 1, new Animation(_ => LoaderBackground.Opacity = _, 1, 0, Easing.Linear) },
                    { 0, 1, new Animation(_ => LoaderAnimation.Opacity = _, 1, 0, Easing.Linear) },
                    { 0, 1, new Animation(_ => LoaderAnimation.Scale = _, 1, 2, Easing.Linear) },
                };
            loadingAnimation.Commit(this, nameof(loadingAnimation), 16, 500u, finished: (_, __) => LoaderBackground.IsVisible = false);

            try
            {
                //await _loadingService.ShowLoader(DefaultEasting == null ? AppResources.LblGettingLocation : AppResources.LblLoading);

                double? eastingPosition = DefaultEasting;
                double? northingPosition = DefaultNorthing;
                int zoom = DefaultZoom;

                // If no default location, use GPS/ Users address or castle
                if (eastingPosition == null || northingPosition == null)
                {
                    try
                    {
                        // Try get GPS
                        Location gps = await _geoLocationService.GetGPSPosition().ConfigureAwait(true);
                        EastingNorthing eastingNorthing = _geoLocationService.LonLatToNorthingEasting(new LatitudeLongitude(gps.Latitude, gps.Longitude));

                        eastingPosition = eastingNorthing.Easting;
                        northingPosition = eastingNorthing.Northing;
                        zoom = 200;
                    }
                    catch (Exception)
                    {
                        // If GPS errors (denied permission/ GPS off etc), set default location to users saved address or the castle
                        EastingNorthing defaultPosition = _geoLocationService.GetDefaultEastingNorthing();

                        eastingPosition = defaultPosition.Easting;
                        northingPosition = defaultPosition.Northing;
                        zoom = 400;
                    }
                }

                //! Important - Used to validate that map position has been updated
                double currentEasting = 0;
                double currentNorthing = 0;

                // Loop setting map position until GetLocation() returns the correct position. Times out after 10s.
                Stopwatch timer = Stopwatch.StartNew();
                do
                {
                    await UpdateMap(eastingPosition ?? 0, northingPosition ?? 0, zoom, MapMode.Equals(MapModeEnum.Static)).ConfigureAwait(true);

                    try
                    {
                        EastingNorthing currentLocation = await GetLocation();
                        currentEasting = currentLocation?.Easting ?? 0;
                        currentNorthing = currentLocation?.Northing ?? 0;
                    }
                    catch (FailedToPullLocationFromIshareException)
                    {
                    }

                    if (timer.Elapsed > TimeSpan.FromSeconds(10))
                    {
                        break;
                    }
                    //! Important - Need to round double (ignoring everything after the decimal point), becuase center of the map doesnt quite line up with the default location position 
                } while ((int)Math.Round(currentEasting, 0) != (int)Math.Round(eastingPosition ?? 0, 0) && (int)Math.Round(currentNorthing, 0) != (int)Math.Round(northingPosition ?? 0, 0));
            }
            finally
            {
                //await _loadingService.HideLoader();
            }
        });
    }

    private async void MapMovedCSharp()
    {
        IshareView.RemoveLocalCallback("mapMovedCSharp");

        await Task.Delay(1500).ConfigureAwait(false);

        Animation mapMovedMessageAnimation = new Animation
            {
                { 0, 1, new Animation(_ => MoveMapMessage.Opacity = _, 1, 0, Easing.Linear) },
            };
        mapMovedMessageAnimation.Commit(this, nameof(mapMovedMessageAnimation), 16, 500u, finished: (_, __) => LoaderBackground.IsVisible = false);
    }

    public async Task UpdateMap(double easting, double northing, int zoom, bool removePin = false)
    {
        await IshareView.EvaluateJavaScriptAsync($"updateLocation({easting}, {northing}, {zoom}, {removePin.ToString().ToLower()})").ConfigureAwait(true);
    }

    //private async void HelpBtn_OnClicked(object sender, EventArgs e)
    //{
    //    await PopupNavigation.Instance.PushAsync(new MapHelpPopup(SkipMapClicked));
    //}

    //private void MapKeyBtn_OnClicked(object sender, EventArgs e)
    //{
    //    MapKeyClicked?.Invoke(this, e);
    //}

    public enum MapModeEnum
    {
        SelectLocation,
        Static
    }
}