package md573acadcb15e8c3baaeb9b7ff93e95396;


public class BrowserActivity
	extends md5834dcf54229d19ec5e643af1c52da67a.BaseActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.Activities.BrowserActivity, CompassMobile.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", BrowserActivity.class, __md_methods);
	}


	public BrowserActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BrowserActivity.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.Activities.BrowserActivity, CompassMobile.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
