package compassmobile.droid.phone.activities;


public class ProcedureLookupActivity
	extends compassmobile.droid.phone.activities.GetConnectedLookupBaseActivity_2
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.Activities.ProcedureLookupActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ProcedureLookupActivity.class, __md_methods);
	}


	public ProcedureLookupActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ProcedureLookupActivity.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.Activities.ProcedureLookupActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
