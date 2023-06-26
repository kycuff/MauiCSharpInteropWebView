using IeuanWalker.Maui.StateButton;
using IeuanWalker.Maui.StateButton.Enums;
using System;
using System.Windows.Input;

namespace TestMauiMap.Controls.Buttons;

public partial class NextBack : ContentView
{
    #region properties

    public static readonly BindableProperty HasBackButtonProperty = BindableProperty.Create(nameof(HasBackButton), typeof(bool), typeof(NextBack), true);

    public bool HasBackButton
    {
        get => (bool)GetValue(HasBackButtonProperty);
        set => SetValue(HasBackButtonProperty, value);
    }

    public static readonly BindableProperty HasContinueButtonProperty = BindableProperty.Create(nameof(HasContinueButton), typeof(bool), typeof(NextBack), true);

    public bool HasContinueButton
    {
        get => (bool)GetValue(HasContinueButtonProperty);
        set => SetValue(HasContinueButtonProperty, value);
    }

    public static readonly BindableProperty IsSubmitProperty = BindableProperty.Create(nameof(IsSubmit), typeof(bool), typeof(NextBack), false);

    public bool IsSubmit
    {
        get => (bool)GetValue(IsSubmitProperty);
        set => SetValue(IsSubmitProperty, value);
    }

    // ClickedEvent
    // ClickedCommand

    #endregion properties

    #region Events

    public event EventHandler Clicked;

    public event EventHandler BackClicked;

    #endregion Events

    #region Commands

    public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(NextBack), null);

    public ICommand ClickedCommand
    {
        get => (ICommand)GetValue(ClickedCommandProperty);
        set => SetValue(ClickedCommandProperty, value);
    }

    #endregion Commands

    public NextBack()
    {
        InitializeComponent();
    }

    private async void BtnBack_Clicked(object sender, EventArgs e)
    {
        BackClicked?.Invoke(this, e);
        if (BackClicked == null)
        {
            await Navigation.PopAsync();
        }
    }

    private void ContinueButton_Clicked(object sender, EventArgs e)
    {
        Clicked?.Invoke(this, e);
        ClickedCommand?.Execute(null);
    }

    private void BackBtn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("State"))
        {
            ButtonState state = ((StateButton)sender).State;

            switch (state)
            {
                case ButtonState.Pressed:
                    BackBtnFrame.SetDynamicResource(BackgroundColorProperty, "AccentColour");
                    BackBtnIconLbl.SetDynamicResource(Label.TextColorProperty, "TextButtonColour"); ;
                    BackBtnTextLbl.SetDynamicResource(Label.TextColorProperty, "TextButtonColour"); ;
                    break;
                case ButtonState.NotPressed: 
                    BackBtnFrame.BackgroundColor = Colors.Transparent;
                    BackBtnIconLbl.SetDynamicResource(Label.TextColorProperty, "HomepageIcon"); ;
                    BackBtnTextLbl.SetDynamicResource(Label.TextColorProperty, "TextPrimaryColour"); ;
                    break;
            }
        }
    }

    private void NextBtn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("State"))
        {
            ButtonState state = ((StateButton)sender).State;

            switch (state)
            {
                case ButtonState.Pressed:
                    NextBtnFrame.SetDynamicResource(BackgroundColorProperty, "AccentColour");
                    NextBtnTextLbl.SetDynamicResource(Label.TextColorProperty, "TextButtonColour");
                    NextBtnIconLbl.SetDynamicResource(Label.TextColorProperty, "TextButtonColour");
                    break;
                case ButtonState.NotPressed:
                    NextBtnFrame.BackgroundColor = Colors.Transparent;
                    NextBtnTextLbl.SetDynamicResource(Label.TextColorProperty, "TextPrimaryColour");
                    NextBtnIconLbl.SetDynamicResource(Label.TextColorProperty, "HomepageIcon");
                    break;
            }
        }
    }
}