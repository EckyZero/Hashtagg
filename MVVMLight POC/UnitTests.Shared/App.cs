using System;

namespace UnitTests.Shared
{
    public static class App
    {
        public static Action InitializeAction { get; set; }

        public static void Initialize()
        {
            InitializeAction();
        }
    }
}