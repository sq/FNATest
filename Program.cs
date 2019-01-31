using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNATest {
    class Program {
        public static void Main (string[] args) {
            using (var g = new TestGame())
                g.Run();
        }
    }
}
