using AsyncAwaitBestPractices;
using Mopups.Pages;
using Mopups.Services;
using TestMauiMap.Services.Accessibility;

namespace TestMauiMap.Pages.General;

public partial class MapHelpPopup : PopupPage
{
    private readonly EventHandler _skipMapEvent = null;
    private readonly IAccessibilityService _accessibilityService;
    public MapHelpPopup(EventHandler skipMap = null)
    {
        InitializeComponent();

        _accessibilityService = DependencyService.Get<IAccessibilityService>();


        if (skipMap != null)
        {
            _skipMapEvent = skipMap;

            divider.IsVisible = true;
            skipMapBtn.IsVisible = true;
        }
    }
    protected override void OnAppearingAnimationEnd()
    {
        base.OnAppearingAnimationEnd();

        _accessibilityService.SetFocus(PopupTitle).SafeFireAndForget();
        
    }

    private async void BtnClose_OnClicked(object sender, EventArgs e)
    {
        try
        {
            await MopupService.Instance.PopAllAsync();
        }
        catch (Exception)
        {
            // ignore
            // Can happen if the user clicks the button multiple times before its closed, causing an exception 'No popups in stack'
        }
    }

    private async void SkipMap_Clicked(object sender, EventArgs e)
    {
        try
        {
            await MopupService.Instance.PopAllAsync();
        }
        catch (Exception)
        {
            // ignore
            // Can happen if the user clicks the button multiple times before its closed, causing an exception 'No popups in stack'
        }
        _skipMapEvent?.Invoke(this, e);
    }
}