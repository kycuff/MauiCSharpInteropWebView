using GeoUK.Coordinates;
using Mopups.Services;
using System.Net;
using TestMauiMap.Exceptions;
using TestMauiMap.ViewModels;

namespace TestMauiMap.Pages;

public partial class MapSelectedPage : ContentPage
{
    private readonly MapPopupViewModel _viewModel;
    public MapSelectedPage()
    {
        InitializeComponent();
        _viewModel = new MapPopupViewModel();
        BindingContext = _viewModel;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        EastingNorthing result = await IShare.GetLocation();
        _viewModel.Request.Easting = result.Easting;
        _viewModel.Request.Northing = result.Northing;

        await MopupService.Instance.PushAsync(new MapSelectedPopUpPage(_viewModel));
    }
}