namespace TestMauiMap.Services.Accessibility;

public interface IAccessibilityService
{
    /// <summary>
    /// Force accessibility focus to specified element
    /// </summary>
    /// <param name="element"></param>
    Task SetFocus(VisualElement element);

    /// <summary>
    /// Announces the accessibility text passed based on the VoiceOver/ TalkBack is enabled.
    /// </summary>
    /// <param name="text"></param>
    void Announcement(string text);

    /// <summary>
    /// Announces the accessibility text passed based on the VoiceOver/ TalkBack is enabled.
    /// On iOS the normal announcments are not a priority if the system is reading out name/ helptext of a control.
    /// This method sets the iOS notification type as 'PageScrolled' which is higher priority ensuring the announcement is read out.
    /// </summary>
    /// <param name="text"></param>
    void ForcedAnnouncement(string text);
}
