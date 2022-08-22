using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NotesSky
{
    public partial class LabelControl: Label
    {
        public TapGestureRecognizer _tapped;
        public event EventHandler Tapped;
        public event EventHandler LongClicked;

        public LabelControl() : base()
        {
            _tapped = new TapGestureRecognizer();
            _tapped.Tapped += _tapped_Tapped;
            this.GestureRecognizers.Add(_tapped);
        }

        private void _tapped_Tapped(object sender, EventArgs e)
        {
            Tapped?.Invoke(sender, e);
        }
        public void OnLongClicked()
        {
            if (LongClicked != null)
                LongClicked(this, new EventArgs());
        }
    }
}
