using System;
using System.Drawing;
using System.Windows.Forms;
using qlspdigital.BLL.Nhom4_SystemAdmin;
using qlspdigital.DTOs;
using qlspdigital.Shared;

namespace qlspdigital.Views.Nhom4_SystemAdmin
{
    // UI 12: Quản lý danh sách người dùng & Phân quyền / Khóa tài khoản
    // Thành viên phụ trách: Trần Quốc Thuận (Nhóm 4)
    public partial class UserManageControl : UserControl
    {
        private readonly NguoiDungService _userService;
        private readonly ToolTip _toolTip = new ToolTip();
        private bool _canLock = false;
        private bool _canUnlock = false;

        public UserManageControl()
        {
            _userService = new NguoiDungService();

            InitializeComponent();
            AdjustLayout();
            dgvUsers.SelectionChanged += (s, e) => UpdateActionButtons();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadUsers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (panelFilter == null || dgvUsers == null || panelBottomActions == null || txtSearch == null || btnRefresh == null || lblRole == null || lblSearch == null || cboRole == null) return;
            int totalW = Math.Max(600, this.ClientSize.Width);
            int totalH = Math.Max(380, this.ClientSize.Height);

            int fullW = totalW - 50;
            panelFilter.Location = new Point(25, 55);
            panelFilter.Size = new Size(fullW, 50);

            // Bố cục tuần tự tương đối - không bao giờ chồng lấn nhãn
            lblRole.Location = new Point(15, 15);
            cboRole.Location = new Point(lblRole.Right + 12, 10);
            lblSearch.Location = new Point(cboRole.Right + 24, 15);
            txtSearch.Location = new Point(lblSearch.Right + 12, 10);

            int btnSearchW = 100;
            int btnRefreshW = 100;
            int searchW = Math.Clamp(fullW - txtSearch.Left - btnSearchW - btnRefreshW - 45, 180, 520);
            txtSearch.Size = new Size(searchW, 30);

            btnSearch.Location = new Point(txtSearch.Right + 10, 9);
            btnSearch.Size = new Size(btnSearchW, 32);

            btnRefresh.Location = new Point(btnSearch.Right + 10, 9);
            btnRefresh.Size = new Size(btnRefreshW, 32);

            int bottomH = 46;
            int contentH = Math.Max(180, totalH - 115 - bottomH - 18);

            dgvUsers.Location = new Point(25, 115);
            dgvUsers.Size = new Size(fullW, contentH);

            panelBottomActions.Location = new Point(25, 115 + contentH + 8);
            panelBottomActions.Size = new Size(fullW, bottomH);
        }

        public void LoadUsers()
        {
            dgvUsers.Rows.Clear();
            string? role = cboRole.SelectedItem?.ToString();
            string keyword = txtSearch.Text.Trim();

            var list = _userService.LayDanhSachNguoiDung(role, keyword);
            foreach (var u in list)
            {
                dgvUsers.Rows.Add(
                    u.MaNguoiDung,
                    u.TenDangNhap,
                    u.HoTen,
                    u.Email,
                    u.SoDienThoai ?? "---",
                    u.VaiTro == "QuanTriVien" ? "Quản Trị Viên" : "Khách Hàng",
                    FormatHelper.FormatCurrency(u.SoDuVi),
                    u.TrangThai ? "Hoạt động" : "Bị khóa"
                );
            }
            UpdateActionButtons();
        }

        private void SetButtonState(Button btn, bool enabled, Color activeColor, string tooltipText, ref bool flag)
        {
            if (btn == null) return;
            flag = enabled;
            if (enabled)
            {
                btn.BackColor = activeColor;
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
            }
            else
            {
                btn.BackColor = Color.FromArgb(42, 54, 66);
                btn.ForeColor = Color.FromArgb(105, 120, 135);
                btn.Cursor = Cursors.No;
            }
            _toolTip.SetToolTip(btn, tooltipText);
        }

        private void UpdateActionButtons()
        {
            if (dgvUsers == null || btnLock == null || btnUnlock == null) return;

            if (dgvUsers.SelectedRows.Count == 0)
            {
                SetButtonState(btnLock, false, Color.FromArgb(180, 40, 40), "Vui lòng chọn một người dùng", ref _canLock);
                SetButtonState(btnUnlock, false, Color.FromArgb(90, 160, 45), "Vui lòng chọn một người dùng", ref _canUnlock);
                return;
            }

            var row = dgvUsers.SelectedRows[0];
            if (row == null || row.Index < 0)
            {
                SetButtonState(btnLock, false, Color.FromArgb(180, 40, 40), "Vui lòng chọn một người dùng", ref _canLock);
                SetButtonState(btnUnlock, false, Color.FromArgb(90, 160, 45), "Vui lòng chọn một người dùng", ref _canUnlock);
                return;
            }

            var valId = (dgvUsers.Columns.Contains("colMaNguoiDung") ? row.Cells["colMaNguoiDung"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            int id = valId != null ? Convert.ToInt32(valId) : 0;
            string status = (dgvUsers.Columns.Contains("colTrangThai") ? row.Cells["colTrangThai"].Value : null)?.ToString()
                ?? (row.Cells.Count > 7 ? row.Cells[7].Value?.ToString() : "") ?? "";

            bool isSelf = UserSession.IsLoggedIn && UserSession.CurrentUserId == id;
            bool isActive = status.Contains("Hoạt động", StringComparison.OrdinalIgnoreCase);

            if (isSelf)
            {
                SetButtonState(btnLock, false, Color.FromArgb(180, 40, 40), "Không thể tự khóa tài khoản quản trị đang đăng nhập", ref _canLock);
                SetButtonState(btnUnlock, false, Color.FromArgb(90, 160, 45), "Tài khoản của bạn đang hoạt động", ref _canUnlock);
            }
            else if (isActive)
            {
                SetButtonState(btnLock, true, Color.FromArgb(180, 40, 40), "Khóa tài khoản này", ref _canLock);
                SetButtonState(btnUnlock, false, Color.FromArgb(90, 160, 45), "Tài khoản này đang hoạt động bình thường", ref _canUnlock);
            }
            else
            {
                SetButtonState(btnLock, false, Color.FromArgb(180, 40, 40), "Tài khoản này đã bị khóa từ trước", ref _canLock);
                SetButtonState(btnUnlock, true, Color.FromArgb(90, 160, 45), "Mở khóa cho tài khoản này", ref _canUnlock);
            }
        }

        private void ChangeStatus(bool newStatus)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần thay đổi trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!newStatus && !_canLock)
            {
                MessageBox.Show("Không thể thực hiện khóa tài khoản này (có thể do tài khoản đã bị khóa hoặc là tài khoản quản trị đang đăng nhập).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (newStatus && !_canUnlock)
            {
                MessageBox.Show("Tài khoản này hiện đang ở trạng thái Hoạt động, không cần mở khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvUsers.SelectedRows[0];
            if (row == null || row.Index < 0) return;
            var val = (dgvUsers.Columns.Contains("MaNguoiDung") ? row.Cells["MaNguoiDung"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            if (val == null) return;
            int id = Convert.ToInt32(val);
            string username = (dgvUsers.Columns.Contains("TenDangNhap") ? row.Cells["TenDangNhap"].Value : null)?.ToString()
                ?? (row.Cells.Count > 1 ? row.Cells[1].Value?.ToString() : string.Empty) ?? string.Empty;

            string actionText = newStatus ? "Mở khóa" : "Khóa";
            if (MessageBox.Show($"Bạn có chắc chắn muốn {actionText} tài khoản [{username}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ok = newStatus ? _userService.MoKhoaTaiKhoan(id, out string msg) : _userService.KhoaTaiKhoan(id, out msg);
                if (ok)
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void CboRole_SelectedIndexChanged(object? sender, EventArgs e) => LoadUsers();
        private void TxtSearch_TextChanged(object? sender, EventArgs e) => LoadUsers();
        private void BtnSearch_Click(object? sender, EventArgs e) => LoadUsers();
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            cboRole.SelectedIndex = 0;
            LoadUsers();
        }
        private void BtnLock_Click(object? sender, EventArgs e) => ChangeStatus(false);
        private void BtnUnlock_Click(object? sender, EventArgs e) => ChangeStatus(true);
    }
}
