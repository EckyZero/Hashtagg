package compassmobile.droid.phone.fragments;


public class MyHealthDetailsConditionsFragment
	extends compassmobile.droid.phone.fragments.BaseFragment
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onPause:()V:GetOnPauseHandler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.Fragments.MyHealthDetailsConditionsFragment, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyHealthDetailsConditionsFragment.class, __md_methods);
	}


	public MyHealthDetailsConditionsFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MyHealthDetailsConditionsFragment.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.Fragments.MyHealthDetailsConditionsFragment, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onPause ()
	{
		n_onPause ();
	}

	private native void n_onPause ();


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);

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
