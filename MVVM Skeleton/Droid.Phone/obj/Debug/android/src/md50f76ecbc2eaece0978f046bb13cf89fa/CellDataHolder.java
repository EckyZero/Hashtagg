package md50f76ecbc2eaece0978f046bb13cf89fa;


public class CellDataHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.UIHelpers.ViewHolders.CellDataHolder, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CellDataHolder.class, __md_methods);
	}


	public CellDataHolder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CellDataHolder.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.UIHelpers.ViewHolders.CellDataHolder, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
