package md52e17be2daa5e376d6191a4b197e52018;


public class ParallaxScrollView
	extends android.view.ViewGroup
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_addView:(Landroid/view/View;)V:GetAddView_Landroid_view_View_Handler\n" +
			"n_addView:(Landroid/view/View;I)V:GetAddView_Landroid_view_View_IHandler\n" +
			"n_addView:(Landroid/view/View;ILandroid/view/ViewGroup$LayoutParams;)V:GetAddView_Landroid_view_View_ILandroid_view_ViewGroup_LayoutParams_Handler\n" +
			"n_addView:(Landroid/view/View;II)V:GetAddView_Landroid_view_View_IIHandler\n" +
			"n_addView:(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V:GetAddView_Landroid_view_View_Landroid_view_ViewGroup_LayoutParams_Handler\n" +
			"n_onFinishInflate:()V:GetOnFinishInflateHandler\n" +
			"n_onMeasure:(II)V:GetOnMeasure_IIHandler\n" +
			"n_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\n" +
			"n_generateLayoutParams:(Landroid/util/AttributeSet;)Landroid/view/ViewGroup$LayoutParams;:GetGenerateLayoutParams_Landroid_util_AttributeSet_Handler\n" +
			"n_generateLayoutParams:(Landroid/view/ViewGroup$LayoutParams;)Landroid/view/ViewGroup$LayoutParams;:GetGenerateLayoutParams_Landroid_view_ViewGroup_LayoutParams_Handler\n" +
			"n_generateDefaultLayoutParams:()Landroid/view/ViewGroup$LayoutParams;:GetGenerateDefaultLayoutParamsHandler\n" +
			"n_checkLayoutParams:(Landroid/view/ViewGroup$LayoutParams;)Z:GetCheckLayoutParams_Landroid_view_ViewGroup_LayoutParams_Handler\n" +
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Controls.ParallaxScrollView, CompassMobile.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", ParallaxScrollView.class, __md_methods);
	}


	public ParallaxScrollView (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == ParallaxScrollView.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Controls.ParallaxScrollView, CompassMobile.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public ParallaxScrollView (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == ParallaxScrollView.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Controls.ParallaxScrollView, CompassMobile.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public ParallaxScrollView (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == ParallaxScrollView.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Controls.ParallaxScrollView, CompassMobile.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void addView (android.view.View p0)
	{
		n_addView (p0);
	}

	private native void n_addView (android.view.View p0);


	public void addView (android.view.View p0, int p1)
	{
		n_addView (p0, p1);
	}

	private native void n_addView (android.view.View p0, int p1);


	public void addView (android.view.View p0, int p1, android.view.ViewGroup.LayoutParams p2)
	{
		n_addView (p0, p1, p2);
	}

	private native void n_addView (android.view.View p0, int p1, android.view.ViewGroup.LayoutParams p2);


	public void addView (android.view.View p0, int p1, int p2)
	{
		n_addView (p0, p1, p2);
	}

	private native void n_addView (android.view.View p0, int p1, int p2);


	public void addView (android.view.View p0, android.view.ViewGroup.LayoutParams p1)
	{
		n_addView (p0, p1);
	}

	private native void n_addView (android.view.View p0, android.view.ViewGroup.LayoutParams p1);


	public void onFinishInflate ()
	{
		n_onFinishInflate ();
	}

	private native void n_onFinishInflate ();


	public void onMeasure (int p0, int p1)
	{
		n_onMeasure (p0, p1);
	}

	private native void n_onMeasure (int p0, int p1);


	public void onLayout (boolean p0, int p1, int p2, int p3, int p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (boolean p0, int p1, int p2, int p3, int p4);


	public android.view.ViewGroup.LayoutParams generateLayoutParams (android.util.AttributeSet p0)
	{
		return n_generateLayoutParams (p0);
	}

	private native android.view.ViewGroup.LayoutParams n_generateLayoutParams (android.util.AttributeSet p0);


	public android.view.ViewGroup.LayoutParams generateLayoutParams (android.view.ViewGroup.LayoutParams p0)
	{
		return n_generateLayoutParams (p0);
	}

	private native android.view.ViewGroup.LayoutParams n_generateLayoutParams (android.view.ViewGroup.LayoutParams p0);


	public android.view.ViewGroup.LayoutParams generateDefaultLayoutParams ()
	{
		return n_generateDefaultLayoutParams ();
	}

	private native android.view.ViewGroup.LayoutParams n_generateDefaultLayoutParams ();


	public boolean checkLayoutParams (android.view.ViewGroup.LayoutParams p0)
	{
		return n_checkLayoutParams (p0);
	}

	private native boolean n_checkLayoutParams (android.view.ViewGroup.LayoutParams p0);

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
