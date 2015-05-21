using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V13.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.V7.Internal.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using com.refractored;
using Shared.BL;
using Shared.Common;
using Shared.VM;
using Java.Lang;
using Microsoft.Practices.Unity;
using FragmentPagerAdapter = Android.Support.V4.App.FragmentPagerAdapter;
using Object = Java.Lang.Object;

namespace Droid.Phone.Fragments
{
    public class MyHealthDetailsFragment : BaseFragment
    {
        private ViewGroup _parentContainer;

        private LayoutInflater _inflater;
        
        private View _tabHostLayout;
        
        private HealthDetailsViewModel _viewModel;
        

        public MyHealthDetailsFragment(HealthDetailsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _parentContainer = container;
            _inflater = inflater;

            _tabHostLayout = _inflater.Inflate(Resource.Layout.MyHealthDetails, container, false);

            // Initialize the ViewPager and set an adapter
            var pager = _tabHostLayout.FindViewById<ViewPager>(Resource.Id.pager);
            
            pager.Adapter = new MyPagerAdapter(ChildFragmentManager,_viewModel);
            pager.SetCurrentItem(_viewModel.InitialTab,false);
            pager.OffscreenPageLimit = pager.Adapter.Count-1;

            // Bind the tabs to the ViewPager
            var tabs = _tabHostLayout.FindViewById<PagerSlidingTabStrip>(Resource.Id.tabs);

            Typeface tf = Typeface.CreateFromAsset(_tabHostLayout.Context.Assets, "Fonts/CenturyGothicBold.ttf");
            tabs.SetTypeface(tf, TypefaceStyle.Normal);

            tabs.SetViewPager(pager);

            return _tabHostLayout;

        }


    }
    public class MyPagerAdapter : FragmentPagerAdapter
    {
        private string[] Titles = {ApplicationResources.Procedures, ApplicationResources.Conditions, ApplicationResources.Results};

        private HealthDetailsViewModel _viewModel;
        
        public MyPagerAdapter(Android.Support.V4.App.FragmentManager fm, HealthDetailsViewModel viewModel) : base(fm)
        {
            _viewModel = viewModel;

            Titles = _viewModel.HasIncentives ? Titles : Titles.Take(Titles.Length - 1).ToArray();
        }

        public override void DestroyItem(ViewGroup container, int position, Object @object)
        {
            container.RemoveView(@object as View);
            base.DestroyItem(container, position, @object);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(Titles[position]);
        }
        #region implemented abstract members of PagerAdapter
        public override int Count
        {
            get
            {
                return Titles.Length;
            }
        }
        #endregion
        #region implemented abstract members of FragmentPagerAdapter
        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    var procedurePromptViewModel = new HealthDetailsProcedurePromptViewModel();
                    //Initialization of Initial Tab is from the Hamubruger Menu and is used for HudService/Choosing first tab
                    return new MyHealthDetailsProcedureFragment(procedurePromptViewModel,_viewModel.InitialTab == position);
                case 1:
                    var conditionsPromptViewModel = new HealthDetailsConditionPromptViewModel();
                    return new MyHealthDetailsConditionsFragment(conditionsPromptViewModel, _viewModel.InitialTab == position);
                case 2:
					var resultsPromptViewModel = new HealthDetailsResultPromptViewModel();
					return new MyHealthDetailsResultsFragment(resultsPromptViewModel,_viewModel.InitialTab == position);
                default:
                    //Incase we fall to default show the first tab
                    var defaultProcedurePromptViewModel = new HealthDetailsProcedurePromptViewModel();
                    return new MyHealthDetailsProcedureFragment(defaultProcedurePromptViewModel, _viewModel.InitialTab == position);
            }
            
        }
        #endregion
    }
}