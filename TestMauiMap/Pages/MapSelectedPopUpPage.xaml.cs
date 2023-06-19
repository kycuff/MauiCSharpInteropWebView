using GeoUK.Coordinates;
using Mopups.Pages;
using Mopups.Services;
using TestMauiMap.ViewModels;

namespace TestMauiMap.Pages;

public partial class MapSelectedPopUpPage
{
    private readonly MapPopupViewModel _viewModel;

    public MapSelectedPopUpPage(MapPopupViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void BtnClose_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}