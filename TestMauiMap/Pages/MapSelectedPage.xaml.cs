using GeoUK.Coordinates;
using Mopups.Services;
using System.Net;
using TestMauiMap.Exceptions;

namespace TestMauiMap.Pages;

public partial class MapSelectedPage : ContentPage
{
    public MapSelectedPage()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        MopupService.Instance.PushAsync(new MapSelectedPopUpPage());
    }
}