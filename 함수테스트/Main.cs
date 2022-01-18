using System.Reflection;
using 함수테스트.Components;

namespace 함수테스트 {
    public partial class Main : Form {
        static Type componentClass = typeof(_dummy_class);

        public Main() {
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e) {
            InitComponentList();
        }

        Dictionary<string, Type> Components = new();

        void InitComponentList() {
            list.Items.Clear();
            Components.Clear();
            foreach (var item in from item in componentClass.Assembly.GetTypes()
                                 where item.Namespace?.Contains(componentClass.Namespace ?? "와샌즈아시는구나") == true &&
                                       item.BaseType == typeof(TestComponent) &&
                                       item.GetMethod("GetName", BindingFlags.Public | BindingFlags.Static) != null
                                 select 
                                 new Tuple<string, Type>(item.GetMethod("GetName")?.Invoke(null, null) as string ?? "unknown", item)) {
                list.Items.Add(item.Item1);
                Components.Add(item.Item1, item.Item2);
            }
        }

        private void onDoubleClick(object sender, EventArgs e) {
            var item = list.SelectedItem;
            if (item is string name) {
                if (Activator.CreateInstance(Components[name]) is TestComponent com) {
                    com.ShowDialog();
                } else {
                    MessageBox.Show($"{name}이 올바른 컴포넌트가 아닌것같습니다.");
                }
            }
        }
    }
}