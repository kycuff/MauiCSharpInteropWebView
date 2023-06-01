using GeoUK.Coordinates;
using Mopups.Pages;
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

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnClose_Clicked(object sender, EventArgs e)
    {

    }
}