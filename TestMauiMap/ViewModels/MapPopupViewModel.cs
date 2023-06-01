using TestMauiMap.Models;

namespace TestMauiMap.ViewModels;

public class MapPopupViewModel : BindableObject
{
    public MapPopupViewModel()
    {
        Request = new RequestModel
        {
            CheckPrivateLand = true,
            CheckSchoolLand = true,
            CheckHousingLand = true,
            CheckHighspeedRoutes = true,
            CheckParksLand = true,
            CheckOutsideCardiff = true
        };
    }

    public RequestModel Request { get; set; }
}
