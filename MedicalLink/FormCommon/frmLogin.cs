﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;
using System.Net;
using System.Diagnostics;
using MedicalLink.Base;

namespace MedicalLink.FormCommon
{
    public partial class frmLogin : Form
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string adminuser = MedicalLink.Base.KeyTrongPhanMem.AdminUser_key;
        string adminpass = MedicalLink.Base.KeyTrongPhanMem.AdminPass_key;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                KiemTraInsertMayTram();
                // Mã hóa thông tin để so sánh trong DB
                string en_txtUsername = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUsername.Text.Trim(), true);
                string en_txtPassword = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtPassword.Text.Trim(), true);

                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Bạn chưa điền đầy đủ thông tin", "Có lỗi xảy ra!");
                    txtUsername.Focus();
                    return;
                }
                // tạo 1 tài khoản ở trên PM, không chứa trong DB để làm tài khoản admin
                else if (txtUsername.Text == adminuser && txtPassword.Text == adminpass)
                {
                    SessionLogin.SessionUsercode = txtUsername.Text;
                    SessionLogin.SessionUsername = "Administrator";
                    frmMain frmm = new frmMain();
                    frmm.Show();
                    this.Visible = false;
                    MedicalLink.Base.Logging.Info("Application open successfull. Time=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"));
                }
                else
                {
                    try
                    {
                        // Querry
                        string command = "SELECT usercode, username, userpassword FROM tools_tbluser WHERE usercode='" + en_txtUsername + "' and userpassword='" + en_txtPassword + "';";
                        DataView dv = new DataView(condb.getDataTable(command));
                        if (dv != null && dv.Count > 0)
                        {
                            MedicalLink.FormCommon.DangKyBanQuyen.kiemTraLicenseHopLe.KiemTraLicenseHopLe();
                            SessionLogin.SessionUsercode = txtUsername.Text;
                            SessionLogin.SessionUsername = MedicalLink.Base.EncryptAndDecrypt.Decrypt(dv[0]["username"].ToString(), true);
                            SessionLogin.SessionlstPhanQuyen = MedicalLink.Base.CheckPermission.GetPhanQuyen();
                            frmMain frmm = new frmMain();
                            frmm.Show();
                            this.Visible = false;
                            MedicalLink.Base.Logging.Info("Application open successfull. Time=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"));
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng", "Có lỗi xảy ra");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng", "Có lỗi xảy ra");
                        txtUsername.Focus();
                    }
                }

                // Khi được check vào nút ghi nhớ thì sẽ lưu tên đăng nhập và mật khẩu vào file config
                if (checkEditNhoPass.Checked)
                {
                    Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    _config.AppSettings.Settings["checkEditNhoPass"].Value = Convert.ToString(checkEditNhoPass.Checked);
                    _config.AppSettings.Settings["LoginUser"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUsername.Text.Trim(), true);
                    _config.AppSettings.Settings["LoginPassword"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtPassword.Text.Trim(), true);
                    _config.Save(ConfigurationSaveMode.Modified);

                    ConfigurationManager.RefreshSection("appSettings");
                }
                // không thì sẽ xóa giá trị đã lưu trong file congfig đi
                else
                {
                    Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    _config.AppSettings.Settings["checkEditNhoPass"].Value = "false";
                    _config.AppSettings.Settings["LoginUser"].Value = "";
                    _config.AppSettings.Settings["LoginPassword"].Value = "";
                    _config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn("Dang nhap " + ex.ToString());
            }
        }

        // Khi nhập username và nhấn enter thì forcus vào ô nhập pass
        private void txtUsername_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        // Sau khi nhập password và ấn enter thì đăng nhập luôn
        private void txtPassword_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        // Load giá trị đăng nhập đã nhớ trong file config
        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["LoginUser"].ToString() != "" && ConfigurationManager.AppSettings["LoginPassword"].ToString() != "")
                {
                    this.txtUsername.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["LoginUser"].ToString(), true);
                    this.txtPassword.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["LoginPassword"].ToString(), true);
                    this.checkEditNhoPass.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["checkEditNhoPass"]);
                }
                else
                {
                    this.txtUsername.Text = "";
                    this.txtPassword.Text = "";
                }

                txtUsername.Focus();

                // Lưu biến Session
                SessionLogin.SessionMachineName = Environment.MachineName;
                // Địa chỉ Ip
                String strHostName = Dns.GetHostName();
                IPHostEntry iphostentry = Dns.GetHostByName(strHostName);
                //int nIP = 0;
                string listIP = "";
                for (int i = 0; i < iphostentry.AddressList.Count(); i++)
                {
                    listIP += iphostentry.AddressList[i] + ";";
                }
                SessionLogin.SessionMyIP = listIP;
                //foreach (IPAddress ipaddress in iphostentry.AddressList)
                //{
                //    SessionLogin.SessionMyIP = ipaddress.ToString();
                //}
                // Lấy version PM
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                SessionLogin.SessionVersion = fvi.FileVersion;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        // nếu viết vào ô username = "config" thì mở ra bảng để cấu hình DB
        private void txtUsername_EditValueChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text == "config")
            {
                frmConnectDB frmcon = new frmConnectDB();
                frmcon.Dock = System.Windows.Forms.DockStyle.Bottom;
                frmcon.ShowDialog();
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Application.Exit();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void KiemTraInsertMayTram()
        {
            try
            {
                SessionLogin.MaMayTinhNguoiDungMaHoa = MedicalLink.FormCommon.DangKyBanQuyen.kiemTraLicenseHopLe.LayThongTinMaMayVaMaHoa();
                string tenmay = MedicalLink.FormCommon.DangKyBanQuyen.HardwareInfo.GetComputerName();
                string license_trang = MedicalLink.Base.EncryptAndDecrypt.Encrypt("", true);

                string kiemtra_client = "SELECT * FROM tools_clients WHERE clientcode='" + SessionLogin.MaMayTinhNguoiDungMaHoa + "' ;";
                DataView dv = new DataView(condb.getDataTable(kiemtra_client));
                if (dv != null && dv.Count > 0)
                {
                    //Kiem tra license
                    //MedicalLink.FormCommon.DangKyBanQuyen.kiemTraLicenseHopLe.KiemTraLicenseHopLe();
                }
                else
                {
                    string insert_client = "INSERT INTO tools_clients(clientcode, clientname, clientlicense, clientstatus, clientnhom, clientnote) VALUES ('" + SessionLogin.MaMayTinhNguoiDungMaHoa + "','" + tenmay + "','" + license_trang + "','1','1','Client');";
                    condb.ExecuteNonQuery(insert_client);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra" + ex.ToString());
            }
        }

        private void linkTroGiup_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Liên hệ với tác giả để được trợ giúp! \nAuthor: Hồng Minh Nhất \nE-mail: hongminhnhat15@gmail.com \nPhone: 0868-915-456", "Thông tin về tác giả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}