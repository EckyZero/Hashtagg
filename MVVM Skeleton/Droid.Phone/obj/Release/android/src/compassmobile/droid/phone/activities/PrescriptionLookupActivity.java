package compassmobile.droid.phone.activities;


public class PrescriptionLookupActivity
	extends compassmobile.droid.phone.activities.GetConnectedLookupBaseActivity_2
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.Activities.PrescriptionLookupActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PrescriptionLookupActivity.class, __md_methods);
	}


	public PrescriptionLookupActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PrescriptionLookupActivity.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.Activities.PrescriptionLookupActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
