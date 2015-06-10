namespace SamplePhoneApp
{
    public class MainViewModel
    {
        private readonly ITwitterGateway _twitterGateway;
        private readonly ILocalStorageProvider _localStorageProvider;
        private readonly INavigator _navigator;

        public MainViewModel(ITwitterGateway twitterGateway, ILocalStorageProvider localStorageProvider, INavigator navigator)
        {
            _twitterGateway = twitterGateway;
            _localStorageProvider = localStorageProvider;
            _navigator = navigator;
        }
    }
}
