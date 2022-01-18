using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 함수테스트.Components {
    public class 스폰알고리즘Component : TestComponent {
        public static string GetName() => "스폰 알고리즘";
        public 스폰알고리즘Component() {
            InitializeComponent();
            view.Click += View_Click;
            view.DoubleClick += View_DoubleClick;
        }

        private void View_DoubleClick(object? sender, EventArgs e) {
            timer1.Stop();
            pfs.Clear();
            view.Refresh();
        }

        private void View_Click(object? sender, EventArgs e) {
            if (timer1.Enabled) timer1.Stop();
            else timer1.Start();
        }

        private Random random = new();
        int playerCount;
        List<Point> spawns = new();
        List<PointF> pfs = new();
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;
        int borderSize;

        PointF nextSpawn() {
            var pos = newSpawn();
            spawns.Add(pos);

            var dis = borderSize / (playerCount * 2);
            var x = (pos.X * 2 * (double)dis) + random.Next(0, dis);
            var y = (pos.Y * 2 * (double)dis) + random.Next(0, dis);
            return new PointF((float)x, (float)y);
        }

        Point newSpawn() {
            var x = random.Next(0, playerCount);
            var y = random.Next(0, playerCount);
            if (spawns.Any(point => point.X == x && point.Y == y)) return newSpawn();
            Console.WriteLine($"X:{x}, Y:{y}");
            return new Point(x, y);
        }

        protected override void onPaint(Graphics g, int viewW, int viewH) {
            spawns.Clear();

            borderSize = Math.Min(viewW, viewH);
            playerCount = 8;

            Console.WriteLine($"======================");
            for (int i = 0; i < playerCount; i++) {
                pfs.Add(nextSpawn());
            }

            foreach (var item in pfs) {
                g.FillEllipse(Brushes.BlueViolet, new RectangleF(item.X - 4, item.Y - 4, 8, 8));
            }
        }

        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.ResumeLayout(false);

        }

        private void timer1_Tick(object? sender, EventArgs e) {
            view.Refresh();
        }
    }
}
