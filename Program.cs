using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNATest {
    class Program {
#if XNA
        [STAThread]
#endif
        public static void Main (string[] args) {
            using (var g = new TestGame())
                g.Run();
        }
    }
}
