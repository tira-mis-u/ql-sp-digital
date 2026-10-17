using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace qlspdigital.Shared
{
    // Hỗ trợ Dark Mode Scrollbar & Titlebar chuẩn hệ thống Windows 10/11
    public static class ThemeHelper
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string? pszSubIdList);

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        public static void ApplyDarkMode(Control? control)
        {
            if (control == null) return;

            if (control.IsHandleCreated)
            {
                ApplyDarkModeCore(control);
            }
            else
            {
                EventHandler handleCreatedHandler = null!;
                handleCreatedHandler = (s, e) =>
                {
                    control.HandleCreated -= handleCreatedHandler;
                    ApplyDarkModeCore(control);
                };
                control.HandleCreated += handleCreatedHandler;
            }

            control.ControlAdded += (s, e) =>
            {
                if (e.Control != null) ApplyDarkMode(e.Control);
            };

            foreach (Control child in control.Controls)
            {
                ApplyDarkMode(child);
            }
        }

        private static void ApplyDarkModeCore(Control control)
        {
            try
            {
                if (control is Form form)
                {
                    int darkMode = 1;
                    DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref darkMode, sizeof(int));
                }

                if (control is FlowLayoutPanel || control is Panel || control is DataGridView || control is TextBox || control is RichTextBox || control is ListBox || control is ScrollableControl)
                {
                    SetWindowTheme(control.Handle, "DarkMode_Explorer", null);
                }
            }
            catch
            {
                // Bỏ qua lỗi nếu chạy trên phiên bản Windows không hỗ trợ theme này
            }
        }
    }
}
