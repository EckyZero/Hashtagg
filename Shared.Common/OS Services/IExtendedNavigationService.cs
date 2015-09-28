using System;
using GalaSoft.MvvmLight.Views;
using System.Collections.Generic;

namespace Shared.Common
{
    public interface IExtendedNavigationService : INavigationService
    {
        void NavigateTo(string pageKey, object parameter, List<NavigationFlag> navigationFlags, AnimationFlag animationFlags = AnimationFlag.Forward);
        void GoBack(AnimationFlag animationFlag);
    }

    public enum NavigationFlag
    {
        SingleInstance, //Only one copy should exist in stack
        ClearStack, // We should remove all controllers/activities after this activity (nothing forward should remain, back does remain)
        NewTask, // Launch a new task (Launcher or drawer effect in android)
        ClearTask, //Remove all activities/controllers in all directions and the current one will be the only one
    }

    public enum AnimationFlag
    {
        Forward,
        Back,
        Up,
        Down,
        Grow,
        None
    }
}