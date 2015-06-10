namespace SamplePhoneApp
{
    public interface INavigator
    {        
        string CurrentUri { get; }
        bool CheckUriBeforeNavigating { get; set; }
    }
}