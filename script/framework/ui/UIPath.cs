using System;
namespace Framework.UI {
    public class UIPath : Attribute {
        public string desc { get; set; }
        public UIPath() {
            Console.WriteLine("UIAttribute构造函数");
        }
        public UIPath(string desc) {
            this.desc = desc;
            Console.WriteLine("UIAttribute有参构造函数");
        }
    }
}