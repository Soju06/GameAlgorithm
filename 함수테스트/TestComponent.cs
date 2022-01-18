namespace 함수테스트 {
    public class TestComponent : Form {
        protected Panel view = new();

        protected int InnerPadding = 8;

        public TestComponent() {
            Padding = new Padding(InnerPadding);
            view.SuspendLayout();
            view.Size = new Size(500, 500);
            ViewSize = new Size(500, 500);
            view.BorderStyle = BorderStyle.FixedSingle;
            view.Paint += GOnPaint;
            view.ResumeLayout();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            Controls.Add(view);
            BackColor = Color.White;
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            GC.Collect();
        }

        public Size ViewSize {
            get => view.Size;
            set {
                view.Size = value;
                ClientSize = new Size(value.Width + (InnerPadding * 2), value.Height + (InnerPadding * 2));
                view.Location = new Point(InnerPadding, InnerPadding);
            }
        }

        void GOnPaint(object? sender, PaintEventArgs e) {
            var g = e.Graphics;
            onPaint(g, view.Width, view.Height);
            base.OnPaint(e);
        }

        protected virtual void onPaint(Graphics g, int viewW, int viewH) {

        }
    }
}
