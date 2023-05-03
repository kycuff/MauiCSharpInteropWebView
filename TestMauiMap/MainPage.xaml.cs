﻿using TestMauiMap.Pages;

namespace TestMauiMap
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestWebViewConnectionsPage());
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new iShareTestPage());
        }

        async void MapSelected_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapSelectedPage());
        }

        async void MapStatic_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapStaticPage());
        }
    }
}