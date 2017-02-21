﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MedicalLink.Base;

namespace MedicalLink.FormCommon.TabTrangChu
{
    public partial class ucSettingLicense : UserControl
    {
        private string MaDatabase = String.Empty;
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();





        public ucSettingLicense()
        {
            InitializeComponent();
        }

        private void ucSettingLicense_Load(object sender, EventArgs e)
        {
            try
            {
                HienThiThongTinVeLicense();
                LoadFormTaoLicense();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void HienThiThongTinVeLicense()
        {
            try
            {
                MaDatabase = MedicalLink.FormCommon.DangKyBanQuyen.kiemTraLicenseHopLe.LayThongTinMaDatabase();
                txtMaMay.Text = MaDatabase;
                txtMaMay.ReadOnly = true;
                //Load License tu DB ra
                string kiemtra_licensetag = "SELECT datakey, licensekey FROM tools_license WHERE datakey='" + MaDatabase + "' limit 1;";
                DataView dv = new DataView(condb.getDataTable(kiemtra_licensetag));
                if (dv != null && dv.Count > 0)
                {
                    txtKeyKichHoat.Text = dv[0]["licensekey"].ToString();
                }
                txtKeyKichHoat.Focus();
                btnLicenseKiemTra_Click(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadFormTaoLicense()
        {
            try
            {
                if (SessionLogin.SessionUsercode == MedicalLink.Base.KeyTrongPhanMem.AdminUser_key)
                {
                    groupBoxTaoLicense.Visible = true;
                    txtTaoLicensePassword.Focus();
                    btnTaoLicenseTao.Enabled = false;
                    DateTime tuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                    DateTime denNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                    dtTaoLicenseKeyTuNgay.Value = tuNgay;
                    dtTaoLicenseKeyDenNgay.Value = denNgay;
                }
                else
                {
                    groupBoxTaoLicense.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #region License
        private void btnLicenseKiemTra_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtKeyKichHoat.Text.Trim()))
                {
                    //Giai ma
                    string makichhoat_giaima = MedicalLink.FormCommon.DangKyBanQuyen.EncryptAndDecryptLicense.Decrypt(txtKeyKichHoat.Text.Trim(), true);
                    //Tach ma kich hoat:
                    string mamay_keykichhoat = "";
                    long thoigianTu = 0;
                    long thoigianDen = 0;
                    string[] makichhoat_tach = makichhoat_giaima.Split('$');

                    if (makichhoat_tach.Length == 4)
                    {
                        mamay_keykichhoat = makichhoat_tach[1];
                        thoigianTu = Convert.ToInt64((makichhoat_tach[2].ToString().Trim() ?? "0") + "000000");
                        thoigianDen = Convert.ToInt64((makichhoat_tach[3].ToString().Trim() ?? "0") + "235959");
                        //Thoi gian hien tai
                        long datetime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                        string thoigianTu_text = DateTime.ParseExact(thoigianTu.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
                        string thoigianDen_text = DateTime.ParseExact(thoigianDen.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
                        //Kiem tra License hop le
                        if (mamay_keykichhoat == SessionLogin.MaDatabase && datetime < thoigianDen)
                        {
                            SessionLogin.KiemTraLicenseSuDung = true;
                            lblThoiGianSuDung.Text = "Từ: " + thoigianTu_text + " đến: " + thoigianDen_text;
                        }
                        else
                        {
                            SessionLogin.KiemTraLicenseSuDung = false;
                            lblThoiGianSuDung.Text = "Mã kích hoạt hết hạn sử dụng";
                        }
                    }
                    else
                    {
                        SessionLogin.KiemTraLicenseSuDung = false;
                        lblThoiGianSuDung.Text = "Sai mã kích hoạt";
                    }
                }
                else
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Chưa nhập mã kích hoạt!";
                    lblThoiGianSuDung.Text = "none";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }

        }
        private void btnLicenseLuu_Click(object sender, EventArgs e)
        {
            try
            {
                //Luu key kich hoat vao DB
                string update_license = "UPDATE tools_license SET licensekey='" + txtKeyKichHoat.Text.Trim() + "' WHERE datakey='" + MaDatabase + "' ;";
                if (condb.ExecuteNonQuery(update_license))
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Lưu mã kích hoạt thành công";
                }
                else
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnLicenseCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();    //Clear if any old value is there in Clipboard        
                Clipboard.SetText(txtMaMay.Text); //Copy text to Clipboard
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Tao License
        private void btnTaoLicenseTao_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTaoLicenseMaMay.Text != "")
                {
                    // Lấy từ ngày, đến ngày
                    string datetungay = DateTime.ParseExact(dtTaoLicenseKeyTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                    string datedenngay = DateTime.ParseExact(dtTaoLicenseKeyDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

                    string MaDatabaseVaThoiGianSuDung = MedicalLink.Base.KeyTrongPhanMem.SaltEncrypt + "$" + txtTaoLicenseMaMay.Text + "$" + datetungay + "$" + datedenngay;

                    txtTaoLicenseMaKichHoat.Text = MedicalLink.FormCommon.DangKyBanQuyen.EncryptAndDecryptLicense.Encrypt(MaDatabaseVaThoiGianSuDung, true);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnTaoLicenseCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();    //Clear if any old value is there in Clipboard        
                Clipboard.SetText(txtTaoLicenseMaKichHoat.Text); //Copy text to Clipboard
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void txtTaoLicensePassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //Kiem tra pass dung hay sai?
                    if (txtTaoLicensePassword.Text.Trim() == MedicalLink.Base.KeyTrongPhanMem.LayLicense_key && SessionLogin.SessionUsercode == MedicalLink.Base.KeyTrongPhanMem.AdminUser_key)
                    {
                        btnTaoLicenseTao.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }






    }
}