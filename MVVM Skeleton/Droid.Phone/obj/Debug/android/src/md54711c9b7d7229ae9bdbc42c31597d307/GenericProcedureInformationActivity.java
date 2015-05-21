package md54711c9b7d7229ae9bdbc42c31597d307;


public class GenericProcedureInformationActivity
	extends md5834dcf54229d19ec5e643af1c52da67a.BaseActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.Activities.GenericProcedureInformationActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GenericProcedureInformationActivity.class, __md_methods);
	}


	public GenericProcedureInformationActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GenericProcedureInformationActivity.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.Activities.GenericProcedureInformationActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
