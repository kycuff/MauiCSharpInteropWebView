using Mopups.Pages;

namespace TestMauiMap.Pages.General;

public partial class LoadingPopup : PopupPage
{
    public LoadingPopup(string loadingText = null)
    {
        InitializeComponent();

        if (loadingText == null)
        {
            loadingText = "LblLoading";
        }

        LoadingLbl.Text = loadingText;
    }

    protected override void OnAppearingAnimationEnd()
    {
        base.OnAppearingAnimationEnd();

        Spinner.SetSemanticFocus();
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