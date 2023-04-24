namespace TestMauiMap.Pages;

public partial class TestWebViewConnectionsPage : ContentPage
{
	public TestWebViewConnectionsPage()
	{
		InitializeComponent();
        myHybridWebView.AddLocalCallback(this, nameof(CallMeFromScript));
	}

    async void Button_Clicked(object sender, EventArgs e)
    {
        var result = await myHybridWebView.EvaluateJavaScriptAsync("executeMe(\"DEF\")");
        await DisplayAlert("Title", result, "Ok");
    }

    private async void CallMeFromScript(string message, int value)
    {
        MainThread.BeginInvokeOnMainThread(async () => 
        { 
            await DisplayAlert("Called From Javascript", $"I'm a .NET method called from JavaScript with message='{message}' and value={value}, using a local registration", "OK!"); 
        });
        
    }
}