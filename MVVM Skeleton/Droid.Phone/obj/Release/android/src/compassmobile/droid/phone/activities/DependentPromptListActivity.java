package compassmobile.droid.phone.activities;


public class DependentPromptListActivity
	extends compassmobile.droid.phone.activities.GetConnectedPromptListBaseActivity_2
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.Activities.DependentPromptListActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DependentPromptListActivity.class, __md_methods);
	}


	public DependentPromptListActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DependentPromptListActivity.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.Activities.DependentPromptListActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
