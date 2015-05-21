TimesSquare is a calendar view for your iPhone or iPad app. Although you
will likely display a Gregorian calendar most of the time, TimesSquare
is able to display any calendar that NSCalendar supports.

Here's an example on how to display a Gregorian Calendar:

```csharp
using TimesSquare.iOS;
...

public override void ViewDidLoad ()
{
	base.ViewDidLoad ();

	var calendarView = new TSQCalendarView (View.Bounds) {
		Calendar = new NSCalendar (NSCalendarType.Gregorian),
		FirstDate = NSDate.Now,
		LastDate = NSDate.FromTimeIntervalSinceNow (60 * 60 * 24 * 365 * 5),
		BackgroundColor = UIColor.LightTextColor,
		PagingEnabled = true
	};
			
	calendarView.DidSelectDate += (sender, e) => {
		var netDate = (DateTime) e.Date;
		new UIAlertView ("You selected", netDate.ToLongDateString(), null, "Ok", null).Show ();
	};
			
	View.Add (calendarView);
}
```

Here's an example on how to display a Japanese Calendar:

```csharp
using TimesSquare.iOS;
...

public override void ViewDidLoad ()
{
	base.ViewDidLoad ();

	var calendarView = new TSQCalendarView (View.Bounds) {
		Calendar = new NSCalendar (NSCalendarType.Japanese),
		FirstDate = NSDate.Now,
		LastDate = NSDate.FromTimeIntervalSinceNow (60 * 60 * 24 * 365 * 5),
		BackgroundColor = UIColor.LightTextColor,
		PagingEnabled = true
	};
			
	calendarView.DidSelectDate += (sender, e) => {
		var netDate = (DateTime) e.Date;
		new UIAlertView ("You selected", netDate.ToLongDateString(), null, "Ok", null).Show ();
	};
			
	View.Add (calendarView);
}
```
