using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
namespace PS
{
    class Program
    {
        // dotnet publish --output "/home/mike/Desktop/Code/C#/kc/bin/Release/net6.0/" --runtime win-x64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010; // Invokes for mouseinput and getting the desktop window etc
        public static void Main(string[] args) 
        {
            while (true)
            {
                System.Threading.Thread.Sleep(300);
                Console.WriteLine(GetColorAt(23,23));
            }
            Console.ReadLine();
        }

        public static Color GetColorAt(int x, int y)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int) GetPixel(dc, x, y);
            ReleaseDC(desk, dc);

            Color pixel = Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
            byte green = pixel.G;
            byte blue = pixel.B;
            byte red = pixel.R;
            if (red == 255 && blue == 0 && green == 0){
                Console.WriteLine("Right Click!");
                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            }
            return pixel;
        }
    }
}
