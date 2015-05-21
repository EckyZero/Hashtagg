package md51a54a76fe52d978a79fb75ce751ab5d1;


public class InventiveCantCompleteDoctorVerifyActivity
	extends md5fc8f15564f59e5798e474b44ecdf5b28.IncentiveAttestationInformationBaseActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onOptionsItemSelected:(Landroid/view/MenuItem;)Z:GetOnOptionsItemSelected_Landroid_view_MenuItem_Handler\n" +
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.Activities.Incentives.MarkCannotComplete.InventiveCantCompleteDoctorVerifyActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", InventiveCantCompleteDoctorVerifyActivity.class, __md_methods);
	}


	public InventiveCantCompleteDoctorVerifyActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == InventiveCantCompleteDoctorVerifyActivity.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.Activities.Incentives.MarkCannotComplete.InventiveCantCompleteDoctorVerifyActivity, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public boolean onOptionsItemSelected (android.view.MenuItem p0)
	{
		return n_onOptionsItemSelected (p0);
	}

	private native boolean n_onOptionsItemSelected (android.view.MenuItem p0);

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
