using Mopups.Pages;

namespace TestMauiMap.Pages.General;

public partial class LoadingPopup : PopupPage
{
    //private readonly IAccessibilityService _accessibilityService;
    public LoadingPopup(string loadingText = null)
    {
        InitializeComponent();

        //_accessibilityService = DependencyService.Get<IAccessibilityService>();

        if (loadingText == null)
        {
            loadingText = ResourcesManager.ResourceManager.GetString("LblLoading");
        }

        LoadingLbl.Text = loadingText;
    }

    protected override void OnAppearingAnimationEnd()
    {
        base.OnAppearingAnimationEnd();

        //_accessibilityService.SetFocus(Spinner).SafeFireAndForget();
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
    protected override bool OnBackgroundClicked()
    {
        return false;
    }

    public void UpdateText(string loadingText)
    {
        LoadingLbl.Text = loadingText;
    }
}