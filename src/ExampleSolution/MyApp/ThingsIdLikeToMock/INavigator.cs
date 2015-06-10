using System.Collections.Generic;

namespace MyApp.ThingsIdLikeToMock
{
    public interface INavigator
    {
        void NavigateTo(string uri);
        List<int> GetSomeInts(string xxx);
    }
}
