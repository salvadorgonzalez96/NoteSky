package crc64905cdd8c940fe19d;


public class CustomEntry
	extends crc647c4c06b10f3352ff.MaterialEntryRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("NotesSky.Droid.CustomControls.CustomEntry, NotesSky.Android", CustomEntry.class, __md_methods);
	}


	public CustomEntry (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomEntry.class)
			mono.android.TypeManager.Activate ("NotesSky.Droid.CustomControls.CustomEntry, NotesSky.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public CustomEntry (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CustomEntry.class)
			mono.android.TypeManager.Activate ("NotesSky.Droid.CustomControls.CustomEntry, NotesSky.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public CustomEntry (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CustomEntry.class)
			mono.android.TypeManager.Activate ("NotesSky.Droid.CustomControls.CustomEntry, NotesSky.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}

	private java.util.ArrayList refList;
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
