package md5d7f0962782a324dbc9992a12c8322ca0;


public class PagerSlidingTabStrip_MyOnGlobalLayoutListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.ViewTreeObserver.OnGlobalLayoutListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onGlobalLayout:()V:GetOnGlobalLayoutHandler:Android.Views.ViewTreeObserver/IOnGlobalLayoutListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("com.refractored.PagerSlidingTabStrip/MyOnGlobalLayoutListener, Refractored.PagerSlidingTabStrip, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", PagerSlidingTabStrip_MyOnGlobalLayoutListener.class, __md_methods);
	}


	public PagerSlidingTabStrip_MyOnGlobalLayoutListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PagerSlidingTabStrip_MyOnGlobalLayoutListener.class)
			mono.android.TypeManager.Activate ("com.refractored.PagerSlidingTabStrip/MyOnGlobalLayoutListener, Refractored.PagerSlidingTabStrip, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public PagerSlidingTabStrip_MyOnGlobalLayoutListener (md5d7f0962782a324dbc9992a12c8322ca0.PagerSlidingTabStrip p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == PagerSlidingTabStrip_MyOnGlobalLayoutListener.class)
			mono.android.TypeManager.Activate ("com.refractored.PagerSlidingTabStrip/MyOnGlobalLayoutListener, Refractored.PagerSlidingTabStrip, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "com.refractored.PagerSlidingTabStrip, Refractored.PagerSlidingTabStrip, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onGlobalLayout ()
	{
		n_onGlobalLayout ();
	}

	private native void n_onGlobalLayout ();

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
