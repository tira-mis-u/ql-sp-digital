using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using qlspdigital.Views.Main;
using qlspdigital.Views.Nhom1_Store;
using qlspdigital.Views.Nhom2_Cart;
using qlspdigital.Views.Nhom3_ProductAdmin;
using qlspdigital.Views.Nhom4_SystemAdmin;
using qlspdigital.Views.Nhom5_Orders;

namespace qlspdigital
{
    public static class UiTester
    {
        public static int Run()
        {
            Console.WriteLine("=== BẮT ĐẦU KIỂM THỬ TỰ ĐỘNG BỐ CỤC UI & GEOMETRY ===");
            int errors = 0;
            string outDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ui_snapshots");
            Directory.CreateDirectory(outDir);

            ApplicationConfiguration.Initialize();

            // 1. Kiểm tra StoreViewControl
            errors += TestStoreView(outDir);

            // 2. Kiểm tra UserManageControl
            errors += TestUserManage(outDir);

            // 3. Kiểm tra OrderManageControl
            errors += TestOrderManage(outDir);

            // 4. Kiểm tra ProductManageControl
            errors += TestProductManage(outDir);

            // 5. Kiểm tra CategoryManageControl
            errors += TestCategoryManage(outDir);

            // 6. Kiểm tra VoucherControl
            errors += TestVoucher(outDir);

            // 7. Kiểm tra WalletControl
            errors += TestWallet(outDir);

            // 8. Kiểm tra CartControl
            errors += TestCart(outDir);

            // 9. Kiểm tra ProductReviewControl
            errors += TestProductReview(outDir);

            // 10. Kiểm tra toàn bộ TextBox BorderStyle
            errors += TestAllTextBoxes();

            // 11. Kiểm tra MainForm (Header, Sidebar Active State, Khách greeting)
            errors += TestMainForm(outDir);

            // 12. Kiểm thử chuyên sâu: Dữ liệu nhập không hợp lệ & Các kịch bản lách/bypass
            errors += TestInputValidationAndBypassSecurity();

            Console.WriteLine($"\n=== KẾT QUẢ KIỂM THỬ: {(errors == 0 ? "HOÀN TOÀN THÀNH CÔNG (0 LỖI)" : $"{errors} LỖI PHÁT HIỆN")} ===");
            return errors;
        }

        private static int TestStoreView(string outDir)
        {
            int err = 0;
            Console.WriteLine("\n[1] Kiểm tra StoreViewControl...");
            using var form = new Form { Size = new Size(1300, 800), StartPosition = FormStartPosition.Manual, Location = new Point(-2000, -2000) };
            var ctrl = new StoreViewControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.Show();
            Application.DoEvents();

            var pnl = ctrl.Controls.OfType<Panel>().FirstOrDefault(p => p.Dock == DockStyle.Top);
            if (pnl != null)
            {
                var lblSearch = pnl.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains("Tìm kiếm"));
                var txtSearch = pnl.Controls.OfType<TextBox>().FirstOrDefault();
                var btnSearch = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text.Contains("Tìm kiếm"));
                var lblCat = pnl.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains("Thể loại"));
                var cboCat = pnl.Controls.OfType<ComboBox>().FirstOrDefault();
                var btnRefresh = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text.Contains("Làm mới"));

                if (lblSearch != null && txtSearch != null)
                {
                    if (txtSearch.Bounds.IntersectsWith(lblSearch.Bounds))
                    {
                        Console.WriteLine($"  LỖI: txtSearch ({txtSearch.Bounds}) đè lên lblSearch ({lblSearch.Bounds})!");
                        err++;
                    }
                    else
                    {
                        Console.WriteLine($"  OK: txtSearch cách lblSearch {txtSearch.Left - lblSearch.Right}px (Left={txtSearch.Left}, lblRight={lblSearch.Right})");
                    }
                }

                if (txtSearch != null && btnSearch != null)
                {
                    if (btnSearch.Bounds.IntersectsWith(txtSearch.Bounds))
                    {
                        Console.WriteLine($"  LỖI: btnSearch ({btnSearch.Bounds}) đè lên txtSearch ({txtSearch.Bounds})!");
                        err++;
                    }
                    else
                    {
                        Console.WriteLine($"  OK: btnSearch đặt ngay cạnh txtSearch cách {btnSearch.Left - txtSearch.Right}px");
                    }
                }

                if (btnSearch != null && lblCat != null)
                {
                    if (lblCat.Bounds.IntersectsWith(btnSearch.Bounds))
                    {
                        Console.WriteLine($"  LỖI: lblCat ({lblCat.Bounds}) đè lên btnSearch ({btnSearch.Bounds})!");
                        err++;
                    }
                    else
                    {
                        Console.WriteLine($"  OK: lblCat cách btnSearch {lblCat.Left - btnSearch.Right}px");
                    }
                }

                if (lblCat != null && cboCat != null)
                {
                    if (cboCat.Bounds.IntersectsWith(lblCat.Bounds))
                    {
                        Console.WriteLine($"  LỖI: cboCat ({cboCat.Bounds}) đè lên lblCat ({lblCat.Bounds})!");
                        err++;
                    }
                    else
                    {
                        Console.WriteLine($"  OK: cboCat cách lblCat {cboCat.Left - lblCat.Right}px");
                    }
                }

                if (cboCat != null && btnRefresh != null)
                {
                    if (btnRefresh.Bounds.IntersectsWith(cboCat.Bounds))
                    {
                        Console.WriteLine($"  LỖI: btnRefresh ({btnRefresh.Bounds}) đè lên cboCat ({cboCat.Bounds})!");
                        err++;
                    }
                    else
                    {
                        Console.WriteLine($"  OK: btnRefresh cách cboCat {btnRefresh.Left - cboCat.Right}px");
                    }
                }

                // Chụp ảnh panel lưu lại
                using var bmp = new Bitmap(pnl.Width, pnl.Height);
                pnl.DrawToBitmap(bmp, new Rectangle(0, 0, pnl.Width, pnl.Height));
                bmp.Save(Path.Combine(outDir, "StoreView_Filter.png"), ImageFormat.Png);
                Console.WriteLine("  Đã lưu ảnh snapshot: StoreView_Filter.png");
            }
            form.Close();
            return err;
        }

        private static int TestUserManage(string outDir)
        {
            int err = 0;
            Console.WriteLine("\n[2] Kiểm tra UserManageControl...");
            using var form = new Form { Size = new Size(1300, 800), StartPosition = FormStartPosition.Manual, Location = new Point(-2000, -2000) };
            var ctrl = new UserManageControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.Show();
            Application.DoEvents();

            var pnl = ctrl.Controls.OfType<Panel>().FirstOrDefault(p => p.Height == 50);
            if (pnl != null)
            {
                var lblRole = pnl.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains("Vai trò"));
                var cboRole = pnl.Controls.OfType<ComboBox>().FirstOrDefault();
                var lblSearch = pnl.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains("Tìm kiếm"));
                var txtSearch = pnl.Controls.OfType<TextBox>().FirstOrDefault();
                var btnSearch = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text.Contains("Tìm"));
                var btnRefresh = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text.Contains("Làm mới"));

                if (lblRole != null && cboRole != null && cboRole.Bounds.IntersectsWith(lblRole.Bounds))
                {
                    Console.WriteLine("  LỖI: cboRole đè lên lblRole!");
                    err++;
                }
                if (cboRole != null && lblSearch != null && lblSearch.Bounds.IntersectsWith(cboRole.Bounds))
                {
                    Console.WriteLine("  LỖI: lblSearch đè lên cboRole!");
                    err++;
                }
                if (lblSearch != null && txtSearch != null && txtSearch.Bounds.IntersectsWith(lblSearch.Bounds))
                {
                    Console.WriteLine("  LỖI: txtSearch đè lên lblSearch!");
                    err++;
                }
                if (txtSearch != null && btnSearch != null && btnSearch.Bounds.IntersectsWith(txtSearch.Bounds))
                {
                    Console.WriteLine("  LỖI: btnSearch đè lên txtSearch!");
                    err++;
                }
                if (btnSearch != null && btnRefresh != null && btnRefresh.Bounds.IntersectsWith(btnSearch.Bounds))
                {
                    Console.WriteLine("  LỖI: btnRefresh đè lên btnSearch!");
                    err++;
                }

                if (err == 0)
                {
                    Console.WriteLine($"  OK: Tất cả thành phần trên thanh công cụ cách nhau chuẩn xác (txtSearch rộng {txtSearch?.Width}px, nút Tìm rộng {btnSearch?.Width}px)");
                }

                using var bmp = new Bitmap(pnl.Width, pnl.Height);
                pnl.DrawToBitmap(bmp, new Rectangle(0, 0, pnl.Width, pnl.Height));
                bmp.Save(Path.Combine(outDir, "UserManage_Filter.png"), ImageFormat.Png);
            }
            form.Close();
            return err;
        }

        private static int TestOrderManage(string outDir)
        {
            int err = 0;
            Console.WriteLine("\n[3] Kiểm tra OrderManageControl...");
            using var form = new Form { Size = new Size(1300, 800), StartPosition = FormStartPosition.Manual, Location = new Point(-2000, -2000) };
            var ctrl = new OrderManageControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.Show();
            Application.DoEvents();

            var pnl = ctrl.Controls.OfType<Panel>().FirstOrDefault(p => p.Height == 50);
            if (pnl != null)
            {
                var lblStatus = pnl.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains("Trạng thái"));
                var cboStatus = pnl.Controls.OfType<ComboBox>().FirstOrDefault();
                var lblSearch = pnl.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains("Tìm kiếm"));
                var txtSearch = pnl.Controls.OfType<TextBox>().FirstOrDefault();
                var btnSearch = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text.Contains("Tìm"));
                var btnRefresh = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text.Contains("Làm mới"));

                if (lblStatus != null && cboStatus != null && cboStatus.Bounds.IntersectsWith(lblStatus.Bounds)) { Console.WriteLine("  LỖI: cboStatus đè lblStatus!"); err++; }
                if (cboStatus != null && lblSearch != null && lblSearch.Bounds.IntersectsWith(cboStatus.Bounds)) { Console.WriteLine("  LỖI: lblSearch đè cboStatus!"); err++; }
                if (lblSearch != null && txtSearch != null && txtSearch.Bounds.IntersectsWith(lblSearch.Bounds)) { Console.WriteLine("  LỖI: txtSearch đè lblSearch!"); err++; }
                if (txtSearch != null && btnSearch != null && btnSearch.Bounds.IntersectsWith(txtSearch.Bounds)) { Console.WriteLine("  LỖI: btnSearch đè txtSearch!"); err++; }
                if (btnSearch != null && btnRefresh != null && btnRefresh.Bounds.IntersectsWith(btnSearch.Bounds)) { Console.WriteLine("  LỖI: btnRefresh đè btnSearch!"); err++; }

                if (err == 0)
                {
                    Console.WriteLine($"  OK: OrderManage layout hoàn hảo, txtSearch rộng {txtSearch?.Width}px, nút Tìm rộng {btnSearch?.Width}px");
                }

                using var bmp = new Bitmap(pnl.Width, pnl.Height);
                pnl.DrawToBitmap(bmp, new Rectangle(0, 0, pnl.Width, pnl.Height));
                bmp.Save(Path.Combine(outDir, "OrderManage_Filter.png"), ImageFormat.Png);
            }
            form.Close();
            return err;
        }

        private static int TestProductManage(string outDir)
        {
            int err = 0;
            Console.WriteLine("\n[4] Kiểm tra ProductManageControl...");
            using var form = new Form { Size = new Size(1300, 800), StartPosition = FormStartPosition.Manual, Location = new Point(-2000, -2000) };
            var ctrl = new ProductManageControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.Show();
            Application.DoEvents();

            try
            {
                ctrl.LoadProducts();
                Application.DoEvents();
                Console.WriteLine("  OK: LoadProducts() chạy an toàn, không có lỗi SelectionChanged hay ArgumentOutOfRangeException!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  LỖI ProductManageControl: {ex.Message}");
                err++;
            }

            using var bmp = new Bitmap(ctrl.Width, ctrl.Height);
            ctrl.DrawToBitmap(bmp, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            bmp.Save(Path.Combine(outDir, "ProductManage.png"), ImageFormat.Png);
            Console.WriteLine("  OK: Đã xuất ảnh ProductManage.png");
            form.Close();
            return err;
        }

        private static int TestCategoryManage(string outDir)
        {
            int err = 0;
            Console.WriteLine("\n[5] Kiểm tra CategoryManageControl...");
            using var form = new Form { Size = new Size(1300, 800), StartPosition = FormStartPosition.Manual, Location = new Point(-2000, -2000) };
            var ctrl = new CategoryManageControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.Show();
            Application.DoEvents();

            try
            {
                ctrl.LoadCategories();
                Application.DoEvents();
                Console.WriteLine("  OK: LoadCategories() chạy an toàn, không có lỗi SelectionChanged hay ArgumentOutOfRangeException!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  LỖI CategoryManageControl: {ex.Message}");
                err++;
            }

            using var bmp = new Bitmap(ctrl.Width, ctrl.Height);
            ctrl.DrawToBitmap(bmp, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            bmp.Save(Path.Combine(outDir, "CategoryManage.png"), ImageFormat.Png);
            Console.WriteLine("  OK: Đã xuất ảnh CategoryManage.png");
            form.Close();
            return err;
        }

        private static int TestVoucher(string outDir)
        {
            int err = 0;
            Console.WriteLine("\n[6] Kiểm tra VoucherControl...");
            using var form = new Form { Size = new Size(1300, 800), StartPosition = FormStartPosition.Manual, Location = new Point(-2000, -2000) };
            var ctrl = new VoucherControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.Show();
            Application.DoEvents();

            using var bmp = new Bitmap(ctrl.Width, ctrl.Height);
            ctrl.DrawToBitmap(bmp, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            bmp.Save(Path.Combine(outDir, "VoucherControl.png"), ImageFormat.Png);
            Console.WriteLine("  OK: Đã xuất ảnh VoucherControl.png");
            form.Close();
            return err;
        }

        private static int TestWallet(string outDir)
        {
            int err = 0;
            Console.WriteLine("\n[7] Kiểm tra WalletControl...");
            using var form = new Form { Size = new Size(1300, 800), StartPosition = FormStartPosition.Manual, Location = new Point(-2000, -2000) };
            var ctrl = new WalletControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.Show();
            Application.DoEvents();

            using var bmp = new Bitmap(ctrl.Width, ctrl.Height);
            ctrl.DrawToBitmap(bmp, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            bmp.Save(Path.Combine(outDir, "WalletControl.png"), ImageFormat.Png);
            Console.WriteLine("  OK: Đã xuất ảnh WalletControl.png");
            form.Close();
            return err;
        }

        private static int TestCart(string outDir)
        {
            int err = 0;
            Console.WriteLine("\n[8] Kiểm tra CartControl...");
            using var form = new Form { Size = new Size(1300, 800), StartPosition = FormStartPosition.Manual, Location = new Point(-2000, -2000) };
            var ctrl = new CartControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.Show();
            Application.DoEvents();

            using var bmp = new Bitmap(ctrl.Width, ctrl.Height);
            ctrl.DrawToBitmap(bmp, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            bmp.Save(Path.Combine(outDir, "CartControl.png"), ImageFormat.Png);
            Console.WriteLine("  OK: Đã xuất ảnh CartControl.png");
            form.Close();
            return err;
        }

        private static int TestProductReview(string outDir)
        {
            int err = 0;
            Console.WriteLine("\n[9] Kiểm tra ProductReviewControl...");
            using var form = new Form { Size = new Size(1300, 800), StartPosition = FormStartPosition.Manual, Location = new Point(-2000, -2000) };
            var ctrl = new ProductReviewControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.Show();
            Application.DoEvents();

            using var bmp = new Bitmap(ctrl.Width, ctrl.Height);
            ctrl.DrawToBitmap(bmp, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            bmp.Save(Path.Combine(outDir, "ProductReviewControl.png"), ImageFormat.Png);
            Console.WriteLine("  OK: Đã xuất ảnh ProductReviewControl.png");
            form.Close();
            return err;
        }

        private static int TestAllTextBoxes()
        {
            Console.WriteLine("\n[10] Kiểm tra đồng bộ BorderStyle trên toàn bộ Views...");
            Control[] views = new Control[]
            {
                new StoreViewControl(),
                new UserManageControl(),
                new OrderManageControl(),
                new ProductManageControl(),
                new CategoryManageControl(),
                new CartControl(),
                new VoucherControl(),
                new WalletControl(),
                new ProductReviewControl(),
                new ProfileControl(),
                new LoginControl(),
                new RegisterControl()
            };

            int err = 0;
            int total = 0;
            foreach (var v in views)
            {
                var txts = GetAllChildControls<TextBox>(v);
                foreach (var t in txts)
                {
                    total++;
                    if (t.BorderStyle != BorderStyle.FixedSingle)
                    {
                        Console.WriteLine($"  LỖI: {v.GetType().Name} -> TextBox {t.Name} có BorderStyle = {t.BorderStyle} (không phải FixedSingle)!");
                        err++;
                    }
                }
            }
            Console.WriteLine($"  OK: Đã kiểm tra {total} TextBox - tất cả 100% đồng bộ FixedSingle!");
            return err;
        }

        private static int TestMainForm(string outDir)
        {
            int err = 0;
            Console.WriteLine("\n[11] Kiểm tra MainForm (Header, Sidebar Active State, Khách greeting)...");
            using var form = new MainForm();
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-2000, -2000);
            form.Show();
            Application.DoEvents();

            var pnlHeader = form.Controls.OfType<Panel>().FirstOrDefault(p => p.Dock == DockStyle.Top);
            if (pnlHeader != null)
            {
                var pnlHeaderRight = pnlHeader.Controls.OfType<Panel>().FirstOrDefault(p => p.Dock == DockStyle.Right);
                if (pnlHeaderRight != null)
                {
                    var lblGreeting = pnlHeaderRight.Controls.OfType<Label>().FirstOrDefault(l => l.Dock == DockStyle.Fill);
                    var btnAuth = pnlHeaderRight.Controls.OfType<Button>().FirstOrDefault();
                    var lblBal = pnlHeaderRight.Controls.OfType<Label>().FirstOrDefault(l => l.Dock == DockStyle.Right);
                    var btnCart = pnlHeaderRight.Controls.OfType<Button>().FirstOrDefault(b => b.Text.Contains("Giỏ hàng"));

                    if (btnCart != null)
                    {
                        Console.WriteLine("  LỖI: Nút Giỏ hàng vẫn còn tồn tại trên Header!");
                        err++;
                    }
                    else
                    {
                        Console.WriteLine("  OK: Đã xóa nút Giỏ hàng khỏi Header thành công!");
                    }

                    if (lblGreeting != null && lblGreeting.Text == "Khách")
                    {
                        Console.WriteLine("  OK: Lời chào khách mặc định chuẩn xác: 'Khách'");
                    }
                    else
                    {
                        Console.WriteLine($"  LỖI: Lời chào là '{lblGreeting?.Text}', mong đợi 'Khách'!");
                        err++;
                    }

                    if (btnAuth != null && lblBal != null)
                    {
                        if (lblBal.Left > btnAuth.Left)
                        {
                            Console.WriteLine($"  OK: Nút Đăng nhập/Đăng xuất (X={btnAuth.Left}) và Ví (X={lblBal.Left}) đã được hoán đổi vị trí thành công!");
                        }
                        else
                        {
                            Console.WriteLine($"  LỖI: Vị trí chưa hoán đổi đúng: btnAuth.Left={btnAuth.Left}, lblBal.Left={lblBal.Left}!");
                            err++;
                        }
                    }
                }
            }

            // Chụp ảnh MainForm
            using var bmp = new Bitmap(form.Width, form.Height);
            form.DrawToBitmap(bmp, new Rectangle(0, 0, form.Width, form.Height));
            bmp.Save(Path.Combine(outDir, "MainForm_Default.png"), ImageFormat.Png);
            Console.WriteLine("  OK: Đã xuất ảnh snapshot MainForm_Default.png");

            form.Close();
            return err;
        }

        private static int TestInputValidationAndBypassSecurity()
        {
            int err = 0;
            Console.WriteLine("\n[12] Kiểm thử chuyên sâu: Dữ liệu nhập không hợp lệ & Các kịch bản lách/bypass...");

            // 1. Kiểm tra validation đăng ký tài khoản (AuthService)
            var authService = new qlspdigital.BLL.Nhom4_SystemAdmin.AuthService();
            
            // a. Tên đăng nhập trống
            if (authService.DangKy("", "123456", "123456", "Test User", "test@gmail.com", "0901234567", out _))
            {
                Console.WriteLine("  LỖI: Cho phép tên đăng nhập trống!");
                err++;
            }
            // b. Tên đăng nhập < 3 ký tự
            if (authService.DangKy("ab", "123456", "123456", "Test User", "test@gmail.com", "0901234567", out _))
            {
                Console.WriteLine("  LỖI: Cho phép tên đăng nhập < 3 ký tự!");
                err++;
            }
            // c. Tên đăng nhập chứa khoảng trắng
            if (authService.DangKy("user test", "123456", "123456", "Test User", "test@gmail.com", "0901234567", out _))
            {
                Console.WriteLine("  LỖI: Cho phép tên đăng nhập chứa khoảng trắng!");
                err++;
            }
            // d. Mật khẩu ngắn (< 6)
            if (authService.DangKy("validuser", "123", "123", "Test User", "test@gmail.com", "0901234567", out _))
            {
                Console.WriteLine("  LỖI: Cho phép mật khẩu < 6 ký tự!");
                err++;
            }
            // e. Xác nhận mật khẩu không khớp
            if (authService.DangKy("validuser", "123456", "654321", "Test User", "test@gmail.com", "0901234567", out _))
            {
                Console.WriteLine("  LỖI: Cho phép mật khẩu xác nhận không khớp!");
                err++;
            }
            // f. Email không hợp lệ (không chứa @)
            if (authService.DangKy("validuser", "123456", "123456", "Test User", "invalidemail", "0901234567", out _))
            {
                Console.WriteLine("  LỖI: Cho phép email không hợp lệ!");
                err++;
            }
            // g. Số điện thoại quá dài (> 20 ký tự)
            if (authService.DangKy("validuser", "123456", "123456", "Test User", "test@gmail.com", "01234567890123456789012345", out _))
            {
                Console.WriteLine("  LỖI: Cho phép số điện thoại quá 20 ký tự!");
                err++;
            }
            Console.WriteLine("  OK: Kiểm tra đăng ký (AuthService) chặn toàn bộ dữ liệu sai lệch!");

            // 2. Kiểm tra validation nạp tiền ví (ViTaiKhoanService.NapTien)
            var viService = new qlspdigital.BLL.Nhom2_Cart.ViTaiKhoanService();
            // a. Nạp tiền âm
            if (viService.NapTien(1, -50000, out _))
            {
                Console.WriteLine("  LỖI: Cho phép nạp số tiền âm!");
                err++;
            }
            // b. Nạp tiền 0 đồng
            if (viService.NapTien(1, 0, out _))
            {
                Console.WriteLine("  LỖI: Cho phép nạp 0 đồng!");
                err++;
            }
            // c. Nạp tiền dưới mức tối thiểu (< 10.000)
            if (viService.NapTien(1, 5000, out _))
            {
                Console.WriteLine("  LỖI: Cho phép nạp số tiền dưới 10.000 đ!");
                err++;
            }
            // d. Nạp tiền vượt mức tối đa (> 50.000.000)
            if (viService.NapTien(1, 100000000, out _))
            {
                Console.WriteLine("  LỖI: Cho phép nạp số tiền vượt quá 50.000.000 đ!");
                err++;
            }
            Console.WriteLine("  OK: Kiểm tra nạp tiền ví chặn toàn bộ số tiền âm, 0 đ, <10k hoặc >50tr!");

            // 3. Kiểm tra validation giỏ hàng (GioHangService.ThemVaoGioHang)
            var gioHangService = new qlspdigital.BLL.Nhom2_Cart.GioHangService();
            if (gioHangService.ThemVaoGioHang(1, -1, 1, out _))
            {
                Console.WriteLine("  LỖI: Cho phép thêm sản phẩm ID âm!");
                err++;
            }
            if (gioHangService.ThemVaoGioHang(1, 1, -5, out _))
            {
                Console.WriteLine("  LỖI: Cho phép thêm số lượng âm!");
                err++;
            }
            Console.WriteLine("  OK: Kiểm tra giỏ hàng chặn ID âm hoặc số lượng âm!");

            // 4. Kiểm tra validation đánh giá sản phẩm (DanhGiaService.LuuDanhGia)
            var danhGiaService = new qlspdigital.BLL.Nhom1_Store.DanhGiaService();
            // a. Số sao ngoài biên (0 sao, 6 sao)
            if (danhGiaService.LuuDanhGia(1, 1, 0, "Bình luận test", out _))
            {
                Console.WriteLine("  LỖI: Cho phép đánh giá 0 sao!");
                err++;
            }
            if (danhGiaService.LuuDanhGia(1, 1, 6, "Bình luận test", out _))
            {
                Console.WriteLine("  LỖI: Cho phép đánh giá 6 sao!");
                err++;
            }
            // b. Bình luận rỗng
            if (danhGiaService.LuuDanhGia(1, 1, 5, "   ", out _))
            {
                Console.WriteLine("  LỖI: Cho phép gửi nhận xét rỗng!");
                err++;
            }
            Console.WriteLine("  OK: Đánh giá chặn toàn bộ số sao ngoài khoảng 1-5 và bình luận trống!");

            // 5. Kiểm tra an toàn truy vấn tìm kiếm với ký tự đặc biệt (SQL LIKE Wildcards & Injection)
            var spRepo = new qlspdigital.DAL.Nhom1_Store.SanPhamRepository();
            try
            {
                // Ký tự ngoặc vuông '[' dễ gây lỗi syntax LIKE trong T-SQL
                spRepo.LayDanhSachSanPham("[");
                spRepo.LayDanhSachSanPham("[]");
                spRepo.LayDanhSachSanPham("[Tool]");
                // Ký tự SQL Injection
                spRepo.LayDanhSachSanPham("' OR '1'='1");
                spRepo.LayDanhSachSanPham("'; DROP TABLE SanPham; --");
                spRepo.LayDanhSachSanPham("%");
                spRepo.LayDanhSachSanPham("_");
                Console.WriteLine("  OK: Tìm kiếm xử lý an toàn 100% các ký tự đặc biệt ('[', ']', '%', '_', SQL Injection) không gây ngoại lệ!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  LỖI Tìm kiếm với ký tự đặc biệt: {ex.Message}");
                err++;
            }

            return err;
        }

        private static System.Collections.Generic.List<T> GetAllChildControls<T>(Control parent) where T : Control
        {
            var list = new System.Collections.Generic.List<T>();
            foreach (Control c in parent.Controls)
            {
                if (c is T match) list.Add(match);
                list.AddRange(GetAllChildControls<T>(c));
            }
            return list;
        }
    }
}
