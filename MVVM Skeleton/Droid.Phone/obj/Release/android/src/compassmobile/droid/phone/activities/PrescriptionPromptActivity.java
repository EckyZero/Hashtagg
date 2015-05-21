package compassmobile.droid.phone.activities;


public class PrescriptionPromptActivity
	extends compassmobile.droid.phone.activities.GetConnectedPromptBaseActivity_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.Activities.PrescriptionPromptActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PrescriptionPromptActivity.class, __md_methods);
	}


	public PrescriptionPromptActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PrescriptionPromptActivity.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.Activities.PrescriptionPromptActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
