package md54711c9b7d7229ae9bdbc42c31597d307;


public class ProcedurePromptListActivity
	extends md54711c9b7d7229ae9bdbc42c31597d307.GetConnectedPromptListBaseActivity_2
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.Activities.ProcedurePromptListActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ProcedurePromptListActivity.class, __md_methods);
	}


	public ProcedurePromptListActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ProcedurePromptListActivity.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.Activities.ProcedurePromptListActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
