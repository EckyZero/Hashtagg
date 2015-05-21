package compassmobile.droid.phone.activities.incentives;


public abstract class IncentiveAttestationInformationBaseActivity
	extends compassmobile.droid.activities.ActionBarBaseActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.Activities.Incentives.IncentiveAttestationInformationBaseActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", IncentiveAttestationInformationBaseActivity.class, __md_methods);
	}


	public IncentiveAttestationInformationBaseActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == IncentiveAttestationInformationBaseActivity.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.Activities.Incentives.IncentiveAttestationInformationBaseActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
