using Mopups.Pages;

namespace TestMauiMap.Services.Loading;

public interface ILoadingService
{
    /// <summary>
    /// Show default loader
    /// </summary>
    /// <returns></returns>
    Task ShowLoader();
    /// <summary>
    /// Show default loader with custom text
    /// </summary>
    /// <param name="loadingText"></param>
    /// <returns></returns>
    Task ShowLoader(string loadingText);
    /// <summary>
    /// Update the text on the default loader
    /// </summary>
    /// <param name="loadingText"></param>
    void UpdateText(string loadingText);
    /// <summary>
    /// Show custom loader
    /// </summary>
    /// <param name="loadingPage"></param>
    /// <returns></returns>
    Task ShowLoader(PopupPage loader);
    /// <summary>
    /// Hides loader
    /// </summary>
    /// <returns></returns>
    Task HideLoader();
}
