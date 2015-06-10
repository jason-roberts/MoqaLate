using System.Collections.Generic;
using MyApp.ThingsIdLikeToMock;
namespace MoqaLate.Autogenerated
{
public partial class NavigatorMoqaLate : INavigator
{


// -------------- NavigateTo ------------


        private int _navigateToNumberOfTimesCalled;        

        public string NavigateToParameter_uri_LastCalledWith;

        public virtual bool NavigateToWasCalled()
{
   return _navigateToNumberOfTimesCalled > 0;
}


public virtual bool NavigateToWasCalled(int times)
{
   return _navigateToNumberOfTimesCalled == times;
}


public virtual int NavigateToTimesCalled()
{
   return _navigateToNumberOfTimesCalled;
}


public virtual bool NavigateToWasCalledWith(string uri){
return (
uri.Equals(NavigateToParameter_uri_LastCalledWith) );
}


        public void NavigateTo(string uri)
        {
            _navigateToNumberOfTimesCalled++;            

            NavigateToParameter_uri_LastCalledWith = uri;
        }


// -------------- GetSomeInts ------------ 

private List<int> _getSomeIntsReturnValue;

        private int _getSomeIntsNumberOfTimesCalled;

        public string GetSomeIntsParameter_xxx_LastCalledWith;
        
        public virtual void GetSomeIntsSetReturnValue(List<int> value)
        {
            _getSomeIntsReturnValue = value;
        }    


        public virtual bool GetSomeIntsWasCalled()
{
   return _getSomeIntsNumberOfTimesCalled > 0;
}


public virtual bool GetSomeIntsWasCalled(int times)
{
   return _getSomeIntsNumberOfTimesCalled == times;
}


public virtual int GetSomeIntsTimesCalled()
{
   return _getSomeIntsNumberOfTimesCalled;
}


public virtual bool GetSomeIntsWasCalledWith(string xxx){
return (
xxx.Equals(GetSomeIntsParameter_xxx_LastCalledWith) );
}
 

             public List<int> GetSomeInts(string xxx)
        {
            _getSomeIntsNumberOfTimesCalled++;            

            GetSomeIntsParameter_xxx_LastCalledWith = xxx;

            return _getSomeIntsReturnValue;
        }}
}