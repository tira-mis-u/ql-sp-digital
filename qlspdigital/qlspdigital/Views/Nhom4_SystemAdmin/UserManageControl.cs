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
            dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;
            LoadUsers();
        }

        private void DgvUsers_SelectionChanged(object? sender, EventArgs e)
        {
            UpdateActionButtons();
        }

        public void LoadUsers()
        {
            dgvUsers.Rows.Clear();
            string? role = cboRole.SelectedItem?.ToString();
            string keyword = txtSearch.Text.Trim();

            List<NguoiDungDTO> list = _userService.LayDanhSachNguoiDung(role, keyword);
            foreach (NguoiDungDTO u in list)
            {
                dgvUsers.Rows.Add(u.MaNguoiDung, u.TenDangNhap, u.HoTen, u.Email, u.SoDienThoai ?? "---", u.VaiTro == "QuanTriVien" ? "Quản Trị Viên" : "Khách Hàng", FormatHelper.FormatCurrency(u.SoDuVi), u.TrangThai ? "Hoạt động" : "Bị khóa");
            }
            UpdateActionButtons();
        }

        private void SetButtonState(Button btn, bool enabled, Color activeColor, string tooltipText)
        {
            if (btn == null) return;
            if (enabled)
            {
                btn.BackColor = ThemeHelper.GetButtonColor(activeColor);
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
            }
            else
            {
                btn.BackColor = ThemeHelper.GetDisabledButtonColor();
                btn.ForeColor = ThemeHelper.GetDisabledButtonTextColor();
                btn.Cursor = Cursors.No;
            }
            _toolTip.SetToolTip(btn, tooltipText);
        }

        private void UpdateActionButtons()
        {
            if (dgvUsers == null || btnLock == null || btnUnlock == null) return;

            if (dgvUsers.SelectedRows.Count == 0)
            {
                _canLock = false;
                _canUnlock = false;
                SetButtonState(btnLock, false, Color.FromArgb(180, 40, 40), "Vui lòng chọn một người dùng");
                SetButtonState(btnUnlock, false, Color.FromArgb(90, 160, 45), "Vui lòng chọn một người dùng");
                return;
            }

            DataGridViewRow row = dgvUsers.SelectedRows[0];
            if (row == null || row.Index < 0)
            {
                _canLock = false;
                _canUnlock = false;
                SetButtonState(btnLock, false, Color.FromArgb(180, 40, 40), "Vui lòng chọn một người dùng");
                SetButtonState(btnUnlock, false, Color.FromArgb(90, 160, 45), "Vui lòng chọn một người dùng");
                return;
            }

            object? valId = (dgvUsers.Columns.Contains("MaNguoiDung") ? row.Cells["MaNguoiDung"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
            int id = valId != null ? Convert.ToInt32(valId) : 0;
            string status = (dgvUsers.Columns.Contains("TrangThai") ? row.Cells["TrangThai"].Value : null)?.ToString()
                ?? (row.Cells.Count > 7 ? row.Cells[7].Value?.ToString() : "") ?? "";

            bool isSelf = UserSession.IsLoggedIn && UserSession.CurrentUserId == id;
            bool isActive = status.Contains("Hoạt động", StringComparison.OrdinalIgnoreCase);

            if (isSelf)
            {
                _canLock = false;
                _canUnlock = false;
                SetButtonState(btnLock, false, Color.FromArgb(180, 40, 40), "Không thể tự khóa tài khoản quản trị đang đăng nhập");
                SetButtonState(btnUnlock, false, Color.FromArgb(90, 160, 45), "Tài khoản của bạn đang hoạt động");
            }
            else if (isActive)
            {
                _canLock = true;
                _canUnlock = false;
                SetButtonState(btnLock, true, Color.FromArgb(180, 40, 40), "Khóa tài khoản này");
                SetButtonState(btnUnlock, false, Color.FromArgb(90, 160, 45), "Tài khoản này đang hoạt động bình thường");
            }
            else
            {
                _canLock = false;
                _canUnlock = true;
                SetButtonState(btnLock, false, Color.FromArgb(180, 40, 40), "Tài khoản này đã bị khóa từ trước");
                SetButtonState(btnUnlock, true, Color.FromArgb(90, 160, 45), "Mở khóa cho tài khoản này");
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

            DataGridViewRow row = dgvUsers.SelectedRows[0];
            if (row == null || row.Index < 0) return;
            object? val = (dgvUsers.Columns.Contains("MaNguoiDung") ? row.Cells["MaNguoiDung"].Value : null) ?? (row.Cells.Count > 0 ? row.Cells[0].Value : null);
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
