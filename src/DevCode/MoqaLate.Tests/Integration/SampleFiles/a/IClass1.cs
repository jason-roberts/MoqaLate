using System;
using System.Collections.Generic;
using System.IO.Compression;

namespace MoqaLate.Tests.Integration.SampleFiles.a
{
    public interface IClass1
    {
        string Prop1 { get; set; }
        string Prop2 { get; }
        string Prop3 { set; }

        string DoIt2(int p1, List<int> p2);
        string DoIt(int p1, List<int> p2);
        void LetsDoIt(Func<bool, int> p1, string p2);
        Func<bool, int> DStuff4();

        event EventHandler E1;
        event Action E2;
        event Action<int> E3;
        event Action<int,string> E4;
        event EventHandler<AssemblyLoadEventArgs> E5;
    }
}
