using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace qlspdigital.Shared
{
    public enum ThemeMode
    {
        Dark,
        Light
    }

    /// <summary>
    /// Hệ thống quản lý giao diện (Theme System) tập trung cho toàn bộ ứng dụng DPMS.
    /// Hỗ trợ chuyển đổi mượt mà giữa Dark Theme (Steam Navy mặc định) và Light Theme (Clean Crisp Modern),
    /// đồng thời lưu trữ cấu hình người dùng và đồng bộ hóa với hệ điều hành Windows (DWM & uxtheme).
    /// </summary>
    public static class ThemeHelper
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string? pszSubAppName, string? pszSubIdList);

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        private static readonly string ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "theme.cfg");

        public static ThemeMode CurrentTheme { get; private set; } = LoadThemePreference();

        public static bool IsDarkMode => CurrentTheme == ThemeMode.Dark;

        public static event Action? ThemeChanged;

        private static ThemeMode LoadThemePreference()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    string mode = File.ReadAllText(ConfigFilePath).Trim();
                    if (string.Equals(mode, "light", StringComparison.OrdinalIgnoreCase))
                        return ThemeMode.Light;
                }
            }
            catch { }
            return ThemeMode.Dark; // Mặc định luôn là Dark Theme đã được duyệt
        }

        private static void SaveThemePreference(ThemeMode mode)
        {
            try
            {
                File.WriteAllText(ConfigFilePath, mode == ThemeMode.Light ? "light" : "dark");
            }
            catch { }
        }

        public static void ToggleTheme(Form? rootForm = null)
        {
            SetTheme(IsDarkMode ? ThemeMode.Light : ThemeMode.Dark, rootForm);
        }

        public static void SetTheme(ThemeMode mode, Form? rootForm = null)
        {
            CurrentTheme = mode;
            SaveThemePreference(mode);

            if (rootForm != null)
            {
                ApplyTheme(rootForm);
            }
            else
            {
                foreach (Form openForm in Application.OpenForms)
                {
                    ApplyTheme(openForm);
                }
            }

            ThemeChanged?.Invoke();
        }

        // Tương thích ngược với các lệnh gọi ApplyDarkMode cũ
        public static void ApplyDarkMode(Control control) => ApplyTheme(control);

        public static void ApplyTheme(Control control)
        {
            if (control == null) return;

            ApplyStyleToControlTree(control, IsDarkMode);

            control.ControlAdded -= Control_ControlAdded;
            control.ControlAdded += Control_ControlAdded;
        }

        private static void Control_ControlAdded(object? sender, ControlEventArgs e)
        {
            if (e.Control != null)
            {
                ApplyTheme(e.Control);
            }
        }

        private static void ApplyStyleToControlTree(Control control, bool isDark)
        {
            ApplyStyleToControl(control, isDark);
            ApplyWindowThemeCore(control, isDark);

            // Nếu control là ProductCardControl, các thành phần con đã được áp dụng style chuyên biệt
            if (control is Controls.ProductCardControl)
            {
                return;
            }

            foreach (Control child in control.Controls)
            {
                ApplyStyleToControlTree(child, isDark);
            }
        }

        private static void ApplyStyleToControl(Control control, bool isDark)
        {
            // Bỏ qua nút theme toggle vì form tự quản lý style của nó trong UpdateThemeToggleVisuals
            if (control.Name == "btnThemeToggle")
            {
                return;
            }

            // 1. THẺ SẢN PHẨM (Steam Card / Clean Crisp Card)
            if (control is Controls.ProductCardControl card)
            {
                ApplyProductCardStyle(card, isDark);
                return;
            }

            // 2. ROOT CANVASES (Form & UserControl chính)
            if (control is Form || control is UserControl)
            {
                control.BackColor = isDark ? Color.FromArgb(23, 26, 33) : Color.FromArgb(242, 244, 247);
                control.ForeColor = isDark ? Color.White : Color.FromArgb(15, 23, 42);
                control.Invalidate();
                return;
            }

            // 3. PANELS & CONTAINERS
            if (control is Panel pnl)
            {
                ApplyPanelStyle(pnl, isDark);
                control.Invalidate();
                return;
            }

            // 4. BUTTONS
            if (control is Button btn)
            {
                ApplyButtonStyle(btn, isDark);
                control.Invalidate();
                return;
            }

            // 5. LABELS
            if (control is Label lbl)
            {
                ApplyLabelStyle(lbl, isDark);
                control.Invalidate();
                return;
            }

            // 6. INPUT CONTROLS (TextBox, ComboBox, NumericUpDown, RichTextBox)
            if (control is TextBox || control is ComboBox || control is NumericUpDown || control is RichTextBox)
            {
                control.BackColor = isDark ? Color.FromArgb(27, 40, 56) : Color.FromArgb(255, 255, 255);
                control.ForeColor = isDark ? Color.White : Color.FromArgb(15, 23, 42);
                control.Invalidate();
                return;
            }

            // 7. DATAGRIDVIEW
            if (control is DataGridView dgv)
            {
                ApplyDataGridViewStyle(dgv, isDark);
                control.Invalidate();
                return;
            }

            // 8. STATUS STRIP
            if (control is StatusStrip ss)
            {
                ApplyStatusStripStyle(ss, isDark);
                control.Invalidate();
                return;
            }

            // 9. CHECKBOX & RADIOBUTTON
            if (control is CheckBox || control is RadioButton)
            {
                control.ForeColor = isDark ? Color.White : Color.FromArgb(15, 23, 42);
                control.Invalidate();
                return;
            }

            control.Invalidate();
        }

        private static void ApplyProductCardStyle(Controls.ProductCardControl card, bool isDark)
        {
            card.BackColor = isDark ? Color.FromArgb(30, 45, 62) : Color.FromArgb(255, 255, 255);
            card.ForeColor = isDark ? Color.White : Color.FromArgb(15, 23, 42);

            foreach (Control child in card.Controls)
            {
                if (child.Name == "panelHeader")
                {
                    child.BackColor = isDark ? Color.FromArgb(23, 36, 50) : Color.FromArgb(226, 237, 248);
                    foreach (Control hChild in child.Controls)
                    {
                        if (hChild.Name == "lblThumb")
                            hChild.ForeColor = isDark ? Color.FromArgb(102, 192, 244) : Color.FromArgb(2, 132, 199);
                    }
                }
                else if (child.Name == "lblTitle")
                {
                    child.ForeColor = isDark ? Color.White : Color.FromArgb(15, 23, 42);
                }
                else if (child.Name == "lblCategory")
                {
                    child.ForeColor = isDark ? Color.FromArgb(161, 181, 196) : Color.FromArgb(100, 116, 139);
                }
                else if (child.Name == "lblRating")
                {
                    child.ForeColor = isDark ? Color.FromArgb(243, 156, 18) : Color.FromArgb(217, 119, 6);
                }
                else if (child.Name == "lblPrice")
                {
                    child.ForeColor = isDark ? Color.FromArgb(164, 208, 7) : Color.FromArgb(22, 163, 74);
                }
                else if (child.Name == "panelButtons")
                {
                    child.BackColor = isDark ? Color.FromArgb(30, 45, 62) : Color.FromArgb(255, 255, 255);
                    foreach (Control bChild in child.Controls)
                    {
                        if (bChild.Name == "btnDetail")
                        {
                            bChild.BackColor = isDark ? Color.FromArgb(49, 98, 130) : Color.FromArgb(14, 116, 144);
                            bChild.ForeColor = Color.White;
                        }
                        else if (bChild.Name == "btnAddToCart")
                        {
                            bChild.BackColor = isDark ? Color.FromArgb(90, 160, 45) : Color.FromArgb(22, 163, 74);
                            bChild.ForeColor = Color.White;
                        }
                    }
                }
            }

            card.Invalidate(true);
        }

        private static void ApplyPanelStyle(Panel pnl, bool isDark)
        {
            // Panel con của ProductCardControl được xử lý riêng
            if (pnl.Parent is Controls.ProductCardControl)
            {
                if (pnl.Name == "panelHeader")
                    pnl.BackColor = isDark ? Color.FromArgb(23, 36, 50) : Color.FromArgb(226, 237, 248);
                else
                    pnl.BackColor = isDark ? Color.FromArgb(30, 45, 62) : Color.FromArgb(255, 255, 255);
                return;
            }

            // Main application header bar
            if (pnl.Name == "panelHeader" || pnl.Name == "panelHeaderRight")
            {
                pnl.BackColor = isDark ? Color.FromArgb(16, 24, 34) : Color.FromArgb(255, 255, 255);
                return;
            }

            // Sidebar navigation container
            if (pnl.Name == "panelSidebar")
            {
                pnl.BackColor = isDark ? Color.FromArgb(18, 25, 35) : Color.FromArgb(248, 249, 251);
                return;
            }

            // Main central content area & store flow canvas
            if (pnl.Name == "panelMainContent" || pnl.Name == "flowProducts" || pnl.Name == "panelBottomActions")
            {
                pnl.BackColor = isDark ? Color.FromArgb(23, 26, 33) : Color.FromArgb(242, 244, 247);
                return;
            }

            // Store search & category filter bar
            if (pnl.Name == "panelFilter")
            {
                pnl.BackColor = isDark ? Color.FromArgb(22, 32, 45) : Color.FromArgb(255, 255, 255);
                return;
            }

            // Login / Register centered card
            if (pnl.Name == "panelCenter")
            {
                pnl.BackColor = isDark ? Color.FromArgb(27, 40, 56) : Color.FromArgb(255, 255, 255);
                return;
            }

            // Demo credentials box
            if (pnl.Name == "panelDemo")
            {
                pnl.BackColor = isDark ? Color.FromArgb(18, 25, 35) : Color.FromArgb(248, 249, 251);
                return;
            }

            // Security warning banner
            if (pnl.Name == "panelSecurityBanner")
            {
                pnl.BackColor = isDark ? Color.FromArgb(22, 32, 45) : Color.FromArgb(241, 245, 249);
                return;
            }

            // Detail panels / Data grids container panels
            if (pnl.Name == "panelDetails" || pnl.Name == "panelDetail")
            {
                pnl.BackColor = isDark ? Color.FromArgb(27, 40, 56) : Color.FromArgb(255, 255, 255);
                return;
            }

            // Nhận diện theo mức sáng hiện tại nếu không trùng tên
            Color current = pnl.BackColor;
            if (isDark)
            {
                if (current == Color.FromArgb(248, 249, 251))
                    pnl.BackColor = Color.FromArgb(18, 25, 35);
                else if (current == Color.FromArgb(242, 244, 247))
                    pnl.BackColor = Color.FromArgb(23, 26, 33);
                else if (current == Color.FromArgb(255, 255, 255) || current == SystemColors.Control)
                    pnl.BackColor = Color.FromArgb(27, 40, 56);
            }
            else
            {
                if (current == Color.FromArgb(18, 25, 35))
                    pnl.BackColor = Color.FromArgb(248, 249, 251);
                else if (current == Color.FromArgb(23, 26, 33))
                    pnl.BackColor = Color.FromArgb(242, 244, 247);
                else if (current == Color.FromArgb(27, 40, 56) || current == Color.FromArgb(22, 32, 45) || current == Color.FromArgb(30, 45, 62))
                    pnl.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private static void ApplyButtonStyle(Button btn, bool isDark)
        {
            // 1. Sidebar navigation menu buttons
            bool isSidebarBtn = (btn.Parent != null && btn.Parent.Name == "panelSidebar") || btn.Name.StartsWith("btnMenu");
            if (isSidebarBtn)
            {
                bool isActive = false;
                if (btn.FindForm() is Views.Main.MainForm mf && mf.ActiveMenuButton == btn)
                {
                    isActive = true;
                }
                else
                {
                    // Fallback nhận diện nếu đang mang màu active
                    isActive = btn.BackColor == Color.FromArgb(49, 98, 130) || btn.BackColor == Color.FromArgb(14, 116, 144);
                }

                if (isActive)
                {
                    btn.BackColor = isDark ? Color.FromArgb(49, 98, 130) : Color.FromArgb(14, 116, 144);
                    btn.ForeColor = Color.White;
                }
                else
                {
                    btn.BackColor = isDark ? Color.FromArgb(18, 25, 35) : Color.FromArgb(248, 249, 251);
                    btn.ForeColor = isDark ? Color.FromArgb(199, 213, 224) : Color.FromArgb(71, 85, 105);
                }
                return;
            }

            // 2. Link buttons trong suốt (Đăng ký / Đăng nhập / Xem ví Voucher)
            if (btn.Name == "btnGoToRegister" || btn.Name == "btnBackToLogin" || btn.Name == "btnOpenVouchers")
            {
                btn.BackColor = Color.Transparent;
                btn.ForeColor = isDark ? Color.FromArgb(102, 192, 244) : Color.FromArgb(2, 100, 175);
                btn.FlatAppearance.MouseOverBackColor = isDark ? Color.FromArgb(38, 54, 75) : Color.FromArgb(241, 245, 249);
                btn.FlatAppearance.MouseDownBackColor = isDark ? Color.FromArgb(28, 42, 60) : Color.FromArgb(226, 232, 240);
                return;
            }

            // 3. Header Auth button (Đăng nhập / Đăng xuất)
            if (btn.Name == "btnHeaderAuth")
            {
                bool isLogout = btn.Text.Contains("xuất", StringComparison.OrdinalIgnoreCase);
                if (isLogout)
                {
                    btn.BackColor = isDark ? Color.FromArgb(180, 40, 40) : Color.FromArgb(220, 38, 38);
                    btn.ForeColor = Color.White;
                }
                else
                {
                    btn.BackColor = isDark ? Color.FromArgb(49, 98, 130) : Color.FromArgb(14, 116, 144);
                    btn.ForeColor = Color.White;
                }
                return;
            }

            // 4. Demo Login shortcuts
            if (btn.Name == "btnDemoAdmin")
            {
                btn.BackColor = Color.FromArgb(217, 119, 6);
                btn.ForeColor = Color.White;
                return;
            }
            if (btn.Name == "btnDemoCustomer")
            {
                btn.BackColor = isDark ? Color.FromArgb(16, 120, 70) : Color.FromArgb(22, 163, 74);
                btn.ForeColor = Color.White;
                return;
            }

            // 5. Disabled button state
            bool isDisabled = btn.Cursor == Cursors.No || !btn.Enabled ||
                             btn.BackColor == Color.FromArgb(42, 54, 66) || btn.BackColor == Color.FromArgb(241, 245, 249) ||
                             btn.BackColor == Color.FromArgb(60, 65, 75);
            if (isDisabled && btn.Cursor == Cursors.No)
            {
                btn.BackColor = isDark ? Color.FromArgb(42, 54, 66) : Color.FromArgb(241, 245, 249);
                btn.ForeColor = isDark ? Color.FromArgb(105, 120, 135) : Color.FromArgb(148, 163, 184);
                btn.FlatAppearance.BorderColor = isDark ? Color.FromArgb(42, 54, 66) : Color.FromArgb(226, 232, 240);
                return;
            }

            // 6. Action Buttons theo nhóm màu chuẩn
            int r = btn.BackColor.R, g = btn.BackColor.G, b = btn.BackColor.B;

            // Green Success buttons (Thêm giỏ, Duyệt đơn, Thanh toán, Lưu, Đăng ký)
            if ((r == 90 && g == 160 && b == 45) || (r == 22 && g == 163 && b == 74))
            {
                btn.BackColor = isDark ? Color.FromArgb(90, 160, 45) : Color.FromArgb(22, 163, 74);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = btn.BackColor;
            }
            // Red Danger buttons (Hủy đơn, Xóa, Đăng xuất)
            else if ((r == 180 && g == 40 && b == 40) || (r == 140 && g == 30 && b == 30) || (r == 220 && g == 38 && b == 38))
            {
                btn.BackColor = isDark ? Color.FromArgb(180, 40, 40) : Color.FromArgb(220, 38, 38);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = btn.BackColor;
            }
            // Secondary Slate / Neutral buttons (Làm mới, Đóng, Quay lại)
            else if ((r == 60 && g == 75 && b == 90) || (r == 226 && g == 232 && b == 240))
            {
                btn.BackColor = isDark ? Color.FromArgb(60, 75, 90) : Color.FromArgb(226, 232, 240);
                btn.ForeColor = isDark ? Color.White : Color.FromArgb(30, 41, 59);
                btn.FlatAppearance.BorderColor = isDark ? Color.FromArgb(70, 85, 100) : Color.FromArgb(203, 213, 225);
            }
            // Primary Blue buttons (Tìm kiếm, Chi tiết, Đăng nhập, Xác nhận)
            else
            {
                btn.BackColor = isDark ? Color.FromArgb(49, 98, 130) : Color.FromArgb(14, 116, 144);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = btn.BackColor;
            }
        }

        private static void ApplyLabelStyle(Label lbl, bool isDark)
        {
            // Các label đặc biệt trong thanh Header của MainForm
            if (lbl.Name == "lblLogo" || lbl.Name == "lblUserGreeting")
            {
                lbl.ForeColor = isDark ? Color.FromArgb(102, 192, 244) : Color.FromArgb(2, 132, 199);
                return;
            }
            if (lbl.Name == "lblAppTitle")
            {
                lbl.ForeColor = isDark ? Color.FromArgb(199, 213, 224) : Color.FromArgb(15, 23, 42);
                return;
            }
            if (lbl.Name == "lblBalance")
            {
                lbl.ForeColor = isDark ? Color.FromArgb(164, 208, 7) : Color.FromArgb(22, 163, 74);
                return;
            }

            // Tiêu đề phân mục trên Sidebar
            if (lbl.Name == "lblClientSection" || lbl.Name == "lblAdminSection")
            {
                lbl.ForeColor = isDark ? Color.FromArgb(243, 156, 18) : Color.FromArgb(180, 83, 9);
                return;
            }

            // Tiêu đề tìm kiếm và tổng số lượng trên Store / Orders
            if (lbl.Name == "lblTotalCount")
            {
                lbl.ForeColor = isDark ? Color.FromArgb(102, 192, 244) : Color.FromArgb(2, 132, 199);
                return;
            }

            int r = lbl.ForeColor.R, g = lbl.ForeColor.G, b = lbl.ForeColor.B;

            // Cyan Accent
            if ((r == 102 && g == 192 && b == 244) || (r == 2 && g == 132 && b == 199))
            {
                lbl.ForeColor = isDark ? Color.FromArgb(102, 192, 244) : Color.FromArgb(2, 132, 199);
            }
            // Green Price / Balance
            else if ((r == 164 && g == 208 && b == 7) || (r == 22 && g == 163 && b == 74))
            {
                lbl.ForeColor = isDark ? Color.FromArgb(164, 208, 7) : Color.FromArgb(22, 163, 74);
            }
            // Gold Rating Stars / Notice
            else if ((r == 243 && g == 156 && b == 18) || (r == 217 && g == 119 && b == 6) || (r == 180 && g == 83 && b == 9))
            {
                lbl.ForeColor = isDark ? Color.FromArgb(243, 156, 18) : Color.FromArgb(217, 119, 6);
            }
            // Red Error
            else if ((r == 231 && g == 76 && b == 60) || (r == 220 && g == 38 && b == 38))
            {
                lbl.ForeColor = isDark ? Color.FromArgb(231, 76, 60) : Color.FromArgb(220, 38, 38);
            }
            // Muted Subtitle / Descriptions
            else if ((r == 199 && g == 213 && b == 224) || (r == 161 && g == 181 && b == 196) || (r == 140 && g == 155 && b == 170) || (r == 100 && g == 116 && b == 139))
            {
                lbl.ForeColor = isDark ? Color.FromArgb(199, 213, 224) : Color.FromArgb(100, 116, 139);
            }
            // Disabled text
            else if ((r == 105 && g == 120 && b == 135) || (r == 148 && g == 163 && b == 184))
            {
                lbl.ForeColor = isDark ? Color.FromArgb(105, 120, 135) : Color.FromArgb(148, 163, 184);
            }
            // Standard Title / Headings (White trong Dark -> Deep Slate trong Light)
            else
            {
                lbl.ForeColor = isDark ? Color.White : Color.FromArgb(15, 23, 42);
            }
        }

        private static void ApplyDataGridViewStyle(DataGridView dgv, bool isDark)
        {
            if (isDark)
            {
                dgv.BackgroundColor = Color.FromArgb(20, 29, 40);
                dgv.GridColor = Color.FromArgb(35, 48, 68);

                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(27, 40, 56);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(199, 213, 224);
                dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(27, 40, 56);
                dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(199, 213, 224);

                dgv.DefaultCellStyle.BackColor = Color.FromArgb(27, 40, 56);
                dgv.DefaultCellStyle.ForeColor = Color.White;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;

                if (dgv.AlternatingRowsDefaultCellStyle != null)
                {
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(24, 36, 48);
                    dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
                    dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 98, 130);
                    dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
                }

                dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(27, 40, 56);
                dgv.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(199, 213, 224);
            }
            else
            {
                dgv.BackgroundColor = Color.FromArgb(255, 255, 255);
                dgv.GridColor = Color.FromArgb(226, 232, 240);

                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
                dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
                dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);

                dgv.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
                dgv.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 242, 254);
                dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(3, 105, 161);

                if (dgv.AlternatingRowsDefaultCellStyle != null)
                {
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
                    dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
                    dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 242, 254);
                    dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(3, 105, 161);
                }

                dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
                dgv.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.DefaultCellStyle != null)
                {
                    int r = row.DefaultCellStyle.ForeColor.R, g = row.DefaultCellStyle.ForeColor.G, b = row.DefaultCellStyle.ForeColor.B;
                    if ((r == 164 && g == 208 && b == 7) || (r == 22 && g == 163 && b == 74))
                    {
                        row.DefaultCellStyle.ForeColor = isDark ? Color.FromArgb(164, 208, 7) : Color.FromArgb(22, 163, 74);
                    }
                    else if ((r == 140 && g == 140 && b == 140) || (r == 148 && g == 163 && b == 184))
                    {
                        row.DefaultCellStyle.ForeColor = isDark ? Color.FromArgb(140, 140, 140) : Color.FromArgb(148, 163, 184);
                    }
                }
            }
        }

        private static void ApplyStatusStripStyle(StatusStrip ss, bool isDark)
        {
            ss.BackColor = isDark ? Color.FromArgb(16, 24, 34) : Color.FromArgb(241, 245, 249);
            foreach (ToolStripItem item in ss.Items)
            {
                if (item.Name == "lblDbStatus")
                {
                    bool isOk = item.Text.Contains("thành công", StringComparison.OrdinalIgnoreCase);
                    if (isDark)
                        item.ForeColor = isOk ? Color.FromArgb(164, 208, 7) : Color.FromArgb(231, 76, 60);
                    else
                        item.ForeColor = isOk ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38);
                }
                else
                {
                    item.ForeColor = isDark ? Color.FromArgb(199, 213, 224) : Color.FromArgb(71, 85, 105);
                }
            }
        }

        private static void ApplyWindowThemeCore(Control control, bool isDark)
        {
            try
            {
                if (control is Form form && form.IsHandleCreated)
                {
                    int darkMode = isDark ? 1 : 0;
                    DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref darkMode, sizeof(int));
                }
                if ((control is FlowLayoutPanel || control is Panel || control is DataGridView ||
                    control is TextBox || control is RichTextBox || control is ListBox || control is ScrollableControl)
                    && control.IsHandleCreated)
                {
                    SetWindowTheme(control.Handle, isDark ? "DarkMode_Explorer" : "Explorer", null);
                }
            }
            catch { }
        }

        // ==========================================
        // CÁC HÀM TIỆN ÍCH DÀNH CHO CODE THỜI GIAN CHẠY (RUNTIME HELPERS)
        // ==========================================

        public static Color GetButtonColor(Color originalDarkColor)
        {
            if (IsDarkMode) return originalDarkColor;

            int r = originalDarkColor.R, g = originalDarkColor.G, b = originalDarkColor.B;
            if (r == 49 && g == 98 && b == 130) return Color.FromArgb(14, 116, 144);
            if (r == 90 && g == 160 && b == 45) return Color.FromArgb(22, 163, 74);
            if (r == 180 && g == 40 && b == 40) return Color.FromArgb(220, 38, 38);
            if (r == 60 && g == 75 && b == 90) return Color.FromArgb(226, 232, 240);
            if (r == 60 && g == 65 && b == 75) return Color.FromArgb(241, 245, 249);
            return originalDarkColor;
        }

        public static Color GetDisabledButtonColor()
        {
            return IsDarkMode ? Color.FromArgb(42, 54, 66) : Color.FromArgb(241, 245, 249);
        }

        public static Color GetDisabledButtonTextColor()
        {
            return IsDarkMode ? Color.FromArgb(105, 120, 135) : Color.FromArgb(148, 163, 184);
        }

        public static Color GetHeaderBorderColor()
        {
            return IsDarkMode ? Color.FromArgb(35, 48, 68) : Color.FromArgb(226, 232, 240);
        }

        public static Color GetCardHoverColor()
        {
            return IsDarkMode ? Color.FromArgb(42, 71, 94) : Color.FromArgb(241, 245, 249);
        }

        public static Color GetCardNormalColor()
        {
            return IsDarkMode ? Color.FromArgb(30, 45, 62) : Color.FromArgb(255, 255, 255);
        }

        public static Color GetTextColor(Color originalDarkColor)
        {
            if (IsDarkMode) return originalDarkColor;

            int r = originalDarkColor.R, g = originalDarkColor.G, b = originalDarkColor.B;
            if (r >= 240 && g >= 240 && b >= 240) return Color.FromArgb(15, 23, 42);
            if (r == 102 && g == 192 && b == 244) return Color.FromArgb(2, 132, 199);
            if (r == 164 && g == 208 && b == 7) return Color.FromArgb(22, 163, 74);
            if (r == 243 && g == 156 && b == 18) return Color.FromArgb(217, 119, 6);
            if (r == 231 && g == 76 && b == 60) return Color.FromArgb(220, 38, 38);
            if (r == 199 && g == 213 && b == 224) return Color.FromArgb(100, 116, 139);
            return originalDarkColor;
        }
    }
}
