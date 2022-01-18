namespace 함수테스트.Components {
    public class 레벨그래프Component : TestComponent {
        public static string GetName() => "레벨 그래프 테스트";

        public 레벨그래프Component() {
            ViewSize = new Size(400, 400);
        }

        protected override void onPaint(Graphics g, int w, int h) {
            var points = new List<PointF>();

            var ff = new List<double>();
            for (int wi = w - 1; wi >= 0; wi--)
                ff.Add(wi / (w - 1d));

            g.DrawLines(Pens.Red, ConvertScale(Funcx(x => 1 - x, ff), w, h));

            g.DrawLines(Pens.Blue, ConvertScale(Funcx(x => Math.Sqrt(-Math.Pow((1 - x), 2) + (2 * (1 - x))), ff), w, h));
            g.DrawLines(Pens.Green, ConvertScale(Funcx(x => av(1 - x, Math.Sqrt(-Math.Pow((1 - x), 2) + (2 * (1 - x))), 3, 7), ff), w, h));
            g.DrawLines(Pens.Purple, ConvertScale(Funcx(x => av(1 - x, Math.Sqrt(-Math.Pow((1 - x), 2) + (2 * (1 - x))), 3, 1), ff), w, h));

            g.DrawLines(Pens.Orange, ConvertScale(Funcx(x => 1 - Math.Pow(128, x - 1), ff), w, h));
            g.DrawLines(Pens.DeepSkyBlue, ConvertScale(Funcx(x => av(1 - x, 1 - Math.Pow(128, x - 1), 35, 65), ff), w, h));
        }

        double av(double a, double b, int cxA, int cxB) {
            double val = 0;
            val += a * cxA;
            val += b * cxB;

            return val / (cxA + cxB);
        }

        PointF[] Funcx(Func<double, double> func, List<double> ff) {
            var fs = new List<PointF>();
            Console.WriteLine("============================");
            for (int i = 0; i < ff.Count; i++) {
                var x = ff[i];
                var y = func.Invoke(x);
                fs.Add(new PointF((float)x, (float)y));
                Console.WriteLine("y = {0}, x = {1}", y, x);
            }
            return fs.ToArray();
        }

        PointF[] ConvertScale(PointF[] arr, int x, int y) {
            for (int i = 0; i < arr.Length; i++) {
                var data = arr[i];
                arr[i] = new PointF(data.X * x, data.Y * y);
            }
            return arr;
        }
    }
}
