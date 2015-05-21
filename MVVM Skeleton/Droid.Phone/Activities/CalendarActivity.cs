using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Telerik.Widget.Calendar;
using Droid;
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using Java.Lang;
using CalendarSelectionMode = Shared.VM.CalendarSelectionMode;

namespace Droid.Phone.Activities
{
    [Activity(Label = "CalendarActivity")]
    public class CalendarActivity : BaseActivity
    {
        private CalendarParameters _calParams;

        private RadCalendarView _calendarView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Calendar);

            _calParams = _navigationService.GetAndRemoveParameter<CalendarParameters>(Intent);

            StyleCalendar();

            FindViewById<Button>(Resource.Id.calendar_SelectButton).Click += CalendarButtonClicked;
        }
 
        private void CalendarButtonClicked(object sender, EventArgs args)
        {
            _calParams.OnDateSelected(((long)_calendarView.SelectedDates[0]).ToDateTime());
            GoBack();
        }

        private void StyleCalendar()
        {
            _calendarView = FindViewById<RadCalendarView>(Resource.Id.calendar_calendar);

            _calendarView.MinDate = _calParams.MinDate.ToEPOCH() - 1000;
            _calendarView.MaxDate = _calParams.MaxDate.ToEPOCH() - 1000;
            _calendarView.SelectionMode = _calParams.SelectionMode == CalendarSelectionMode.Multiple
                ? Com.Telerik.Widget.Calendar.CalendarSelectionMode.Multiple
                    : _calParams.SelectionMode == CalendarSelectionMode.Range
                        ? Com.Telerik.Widget.Calendar.CalendarSelectionMode.Range
                        : Com.Telerik.Widget.Calendar.CalendarSelectionMode.Single;
            _calendarView.SelectedDates = new List<Long> { (Long)(_calParams.SelectedDate.ToEPOCH() - 1000) };
        }
    }
        
}