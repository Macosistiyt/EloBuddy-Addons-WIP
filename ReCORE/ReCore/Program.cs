using EloBuddy;
using EloBuddy.SDK.Events;
using ReCORE.ReCore;
using System;

namespace ReCORE
{
    class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //VersionChecker.Check();
            Loader.Initialize();
        }
    }
}
