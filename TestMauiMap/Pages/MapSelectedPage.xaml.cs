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

    private void Button_Clicked(object sender, EventArgs e)
    {
        EastingNorthing result = new EastingNorthing(312729, 181313);
        _viewModel.Request.Easting = result.Easting;
        _viewModel.Request.Northing = result.Northing;

        MopupService.Instance.PushAsync(new MapSelectedPopUpPage(_viewModel));
    }
}