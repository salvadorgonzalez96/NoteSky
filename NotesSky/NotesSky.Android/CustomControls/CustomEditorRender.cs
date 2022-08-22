using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using NotesSky;
using NotesSky.Droid.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRender))]
namespace NotesSky.Droid.CustomControls
{
    class CustomEditorRender : EditorRenderer
    {
        public CustomEditorRender(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackgroundDrawable(gd);
                this.Control.ShowSoftInputOnFocus = false;
                this.Control.SetTextIsSelectable(true);
                Control.CustomSelectionActionModeCallback = new CustomSelectionActionModeCallback();
                Control.CustomInsertionActionModeCallback = new CustomInsertionActionModeCallback();
                //this.Control.Focusable = false;
                //this.Control.FocusableInTouchMode = false;
                //this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                //Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
            }
        }
        private class CustomInsertionActionModeCallback : Java.Lang.Object, ActionMode.ICallback
        {
            public bool OnCreateActionMode(ActionMode mode, IMenu menu) => false;

            public bool OnActionItemClicked(ActionMode m, IMenuItem i) => false;

            public bool OnPrepareActionMode(ActionMode mode, IMenu menu) => true;

            public void OnDestroyActionMode(ActionMode mode) { }
        }

        private class CustomSelectionActionModeCallback : Java.Lang.Object, ActionMode.ICallback
        {
            private const int CopyId = Android.Resource.Id.Copy;

            public bool OnActionItemClicked(ActionMode m, IMenuItem i) => false;

            public bool OnCreateActionMode(ActionMode mode, IMenu menu) => true;

            public void OnDestroyActionMode(ActionMode mode) { }

            public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
            {
                try
                {
                    var copyItem = menu.FindItem(CopyId);
                    var title = copyItem.TitleFormatted;
                    menu.Clear();
                    menu.Add(0, CopyId, 0, title);
                }
                catch
                {
                    // ignored
                }

                return true;
            }
        }
    }
}