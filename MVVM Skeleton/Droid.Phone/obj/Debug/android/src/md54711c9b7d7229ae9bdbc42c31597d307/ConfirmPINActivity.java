package md54711c9b7d7229ae9bdbc42c31597d307;


public class ConfirmPINActivity
	extends md54711c9b7d7229ae9bdbc42c31597d307.PINBaseActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.Activities.ConfirmPINActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ConfirmPINActivity.class, __md_methods);
	}


	public ConfirmPINActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ConfirmPINActivity.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.Activities.ConfirmPINActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
