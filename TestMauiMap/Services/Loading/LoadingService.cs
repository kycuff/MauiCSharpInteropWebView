using Mopups.Pages;
using Mopups.Services;
using TestMauiMap.Pages.General;

namespace TestMauiMap.Services.Loading;

public class LoadingService : ILoadingService
{
    private readonly LoadingPopup _defaultLoadingPopup = new LoadingPopup();
    private readonly string _defaultLoadingText = "Loading....";

    public async Task ShowLoader()
    {
        try
        {

            _defaultLoadingPopup.UpdateText(_defaultLoadingText);
            await MopupService.Instance.PushAsync(_defaultLoadingPopup);
        }
        catch (Exception)
        {
            //! Important - Loading pop up is already open. Hide and reshow the loader.
            // This mainly happens on page navigation.
            // i.e. Loader is open on the previous page and is not closed in time before the OnAppearing method is triggered on the next page

            await HideLoader();

            await ShowLoader();
        }
    }
    public async Task ShowLoader(string loadingText)
    {
        try
        {
            _defaultLoadingPopup.UpdateText(loadingText);
            await MopupService.Instance.PushAsync(_defaultLoadingPopup);
        }
        catch (Exception)
        {
            //! Important - Loading pop up is already open. Hide and reshow the loader.
            // This mainly happens on page navigation.
            // i.e. Loader is open on the previous page and is not closed in time before the OnAppearing method is triggered on the next page

            await HideLoader();

            await ShowLoader(loadingText);
        }
    }
    public void UpdateText(string loadingText)
    {
        _defaultLoadingPopup.UpdateText(loadingText);
    }

    private PopupPage customLoader = null;
    public async Task ShowLoader(PopupPage loader)
    {
        customLoader = loader;
        await MopupService.Instance.PushAsync(loader);
    }

    public async Task HideLoader()
    {
        try
        {
            if (customLoader is null)
            {
                await MopupService.Instance.RemovePageAsync(_defaultLoadingPopup);
            }
            else
            {
                await MopupService.Instance.RemovePageAsync(customLoader);
                customLoader = null;
            }
        }
        catch (Exception)
        {
            //! Important - thrown if loading pop up has already been removed
        }
    }
}
