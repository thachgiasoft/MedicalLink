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
using System.IO;
using DevExpress.XtraSplashScreen;

namespace MedicalLink.ChucNang
{
    public partial class ucBCTimPhieuTHYL : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public ucBCTimPhieuTHYL()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã dịch vụ
            mmeMaPhieuYC.ForeColor = SystemColors.GrayText;
            mmeMaPhieuYC.Text = "Nhập mã phiếu thuốc/vật tư cách nhau bởi dấu phẩy (,)";
            this.mmeMaPhieuYC.Leave += new System.EventHandler(this.mmeMaDV_Leave);
            this.mmeMaPhieuYC.Enter += new System.EventHandler(this.mmeMaDV_Enter);
            // Hiển thị Text Hint Mã viện phí
            txtMaVP.ForeColor = SystemColors.GrayText;
            txtMaVP.Text = "Mã viện phí";
            this.txtMaVP.Leave += new System.EventHandler(this.txtMaVP_Leave);
            this.txtMaVP.Enter += new System.EventHandler(this.txtMaVP_Enter);
            // Hiển thị Text Hint Mã BN
            txtMaBN.ForeColor = SystemColors.GrayText;
            txtMaBN.Text = "Mã bệnh nhân";
            this.txtMaBN.Leave += new System.EventHandler(this.txtMaBN_Leave);
            this.txtMaBN.Enter += new System.EventHandler(this.txtMaBN_Enter);
        }

        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Leave(object sender, EventArgs e)
        {
            if (mmeMaPhieuYC.Text.Length == 0)
            {
                mmeMaPhieuYC.Text = "Nhập mã phiếu thuốc/vật tư cách nhau bởi dấu phẩy (,)";
                mmeMaPhieuYC.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Enter(object sender, EventArgs e)
        {
            if (mmeMaPhieuYC.Text == "Nhập mã phiếu thuốc/vật tư cách nhau bởi dấu phẩy (,)")
            {
                mmeMaPhieuYC.Text = "";
                mmeMaPhieuYC.ForeColor = SystemColors.WindowText;
            }
        }
        // Hiển thị Text Hint Mã viện phí
        private void txtMaVP_Leave(object sender, EventArgs e)
        {
            if (txtMaVP.Text.Length == 0)
            {
                txtMaVP.Text = "Mã viện phí";
                txtMaVP.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã viện phí
        private void txtMaVP_Enter(object sender, EventArgs e)
        {
            if (txtMaVP.Text == "Mã viện phí")
            {
                txtMaVP.Text = "";
                txtMaVP.ForeColor = SystemColors.WindowText;
            }
        }

        // Hiển thị Text Hint Mã bệnh nhân
        private void txtMaBN_Leave(object sender, EventArgs e)
        {
            if (txtMaBN.Text.Length == 0)
            {
                txtMaBN.Text = "Mã bệnh nhân";
                txtMaBN.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã bệnh nhân
        private void txtMaBN_Enter(object sender, EventArgs e)
        {
            if (txtMaBN.Text == "Mã bệnh nhân")
            {
                txtMaBN.Text = "";
                txtMaBN.ForeColor = SystemColors.WindowText;
            }
        }

        //Sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            lblThongBao.Visible = false;
            gridControlDS_THYL.DataSource = null;
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            string[] dsdv_temp;
            string dsphieu = "";
            string sqlquerry = "";

            if ((mmeMaPhieuYC.Text == "Nhập mã phiếu thuốc/vật tư cách nhau bởi dấu phẩy (,)") && (txtMaBN.Text == "Mã bệnh nhân") && (txtMaVP.Text == "Mã viện phí"))
            {
                timerThongBao.Start();
                lblThongBao.Visible = true;
                lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin";
            }
            // Tìm kiếm theo mã phiếu thuốc/vt
            else if (mmeMaPhieuYC.Text != "Nhập mã phiếu thuốc/vật tư cách nhau bởi dấu phẩy (,)")
            {
                try
                {
                    // Lấy dữ liệu danh sách dịch vụ nhập vào
                    dsdv_temp = mmeMaPhieuYC.Text.Split(',');
                    for (int i = 0; i < dsdv_temp.Length - 1; i++)
                    {
                        dsphieu += "'" + dsdv_temp[i].ToString() + "',";
                    }
                    dsphieu += "'" + dsdv_temp[dsdv_temp.Length - 1].ToString() + "'";

                    sqlquerry = "SELECT maubenhpham.maubenhphamid as maphieuchidinh, medicine_store_bill.medicinestorebillcode as maphieutonghop, maubenhpham.medicalrecordid as madieutri, tools_depatment.departmentgroupname as khoachidinh, tools_depatment.departmentname as phongchidinh, maubenhpham.maubenhphamdate as thoigianchidinh, medicine_store_bill.medicinestorebilldate as thoigianlinh, hosobenhan.patientid as mabn, maubenhpham.vienphiid as mavp, hosobenhan.patientname as tenbn, departmentgroup.departmentgroupname as tenkhoaravien, hosobenhan.hosobenhandate as tgvaovien, hosobenhan.hosobenhandate_ravien as tgravien FROM maubenhpham, medicine_store_bill, hosobenhan, tools_depatment, departmentgroup WHERE maubenhpham.hosobenhanid = hosobenhan.hosobenhanid and maubenhpham.departmentid = tools_depatment.departmentid and hosobenhan.departmentgroupid = departmentgroup.departmentgroupid and maubenhpham.medicinestorebillid = medicine_store_bill.medicinestorebillid and maubenhpham.maubenhphamid in(" + dsphieu + ") ORDER BY madieutri ;";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra" + ex.ToString(), "Thông báo !");
                }

            }
            // Tìm kiếm theo mã viện phí
            else if (txtMaVP.Text != "Mã viện phí")
            {
                sqlquerry = "SELECT maubenhpham.maubenhphamid as maphieuchidinh, medicine_store_bill.medicinestorebillcode as maphieutonghop, maubenhpham.medicalrecordid as madieutri, tools_depatment.departmentgroupname as khoachidinh, tools_depatment.departmentname as phongchidinh, maubenhpham.maubenhphamdate as thoigianchidinh, medicine_store_bill.medicinestorebilldate as thoigianlinh, hosobenhan.patientid as mabn, maubenhpham.vienphiid as mavp, hosobenhan.patientname as tenbn, departmentgroup.departmentgroupname as tenkhoaravien,  hosobenhan.hosobenhandate as tgvaovien, hosobenhan.hosobenhandate_ravien as tgravien FROM maubenhpham, medicine_store_bill, hosobenhan, tools_depatment, departmentgroup WHERE maubenhpham.hosobenhanid = hosobenhan.hosobenhanid and maubenhpham.departmentid = tools_depatment.departmentid and hosobenhan.departmentgroupid = departmentgroup.departmentgroupid and maubenhpham.medicinestorebillid = medicine_store_bill.medicinestorebillid and maubenhpham.vienphiid = " + txtMaVP.Text.Trim() + " ORDER BY madieutri; ";

            }
            // Tìm kiếm theo mã bệnh nhân
            else if (txtMaBN.Text != "Mã bệnh nhân")
            {
                sqlquerry = "SELECT maubenhpham.maubenhphamid as maphieuchidinh, medicine_store_bill.medicinestorebillcode as maphieutonghop, maubenhpham.medicalrecordid as madieutri, tools_depatment.departmentgroupname as khoachidinh, tools_depatment.departmentname as phongchidinh, maubenhpham.maubenhphamdate as thoigianchidinh, medicine_store_bill.medicinestorebilldate as thoigianlinh, hosobenhan.patientid as mabn, maubenhpham.vienphiid as mavp, hosobenhan.patientname as tenbn, departmentgroup.departmentgroupname as tenkhoaravien,  hosobenhan.hosobenhandate as tgvaovien, hosobenhan.hosobenhandate_ravien as tgravien FROM maubenhpham, medicine_store_bill, hosobenhan, tools_depatment, departmentgroup WHERE maubenhpham.hosobenhanid = hosobenhan.hosobenhanid and maubenhpham.departmentid = tools_depatment.departmentid and hosobenhan.departmentgroupid = departmentgroup.departmentgroupid and maubenhpham.medicinestorebillid = medicine_store_bill.medicinestorebillid and maubenhpham.patientid = " + txtMaBN.Text.Trim() + " ORDER BY madieutri; ";
            }

            try
            {
                DataView dv = new DataView(condb.getDataTable(sqlquerry));
                gridControlDS_THYL.DataSource = dv;

                if (gridViewDS_THYL.RowCount == 0)
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Không tìm thấy hồ sơ nào như yêu cầu \n             Vui lòng kiểm tra lại.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra" + ex.ToString(), "Thông báo !");
            }

            SplashScreenManager.CloseForm();
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            if (gridViewDS_THYL.RowCount > 0)
            {
                try
                {
                    using (SaveFileDialog saveDialog = new SaveFileDialog())
                    {
                        saveDialog.Filter = "Excel 2003 (.xls)|*.xls|Excel 2010 (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                        if (saveDialog.ShowDialog() != DialogResult.Cancel)
                        {
                            string exportFilePath = saveDialog.FileName;
                            string fileExtenstion = new FileInfo(exportFilePath).Extension;

                            switch (fileExtenstion)
                            {
                                case ".xls":
                                    gridControlDS_THYL.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridControlDS_THYL.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridControlDS_THYL.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridControlDS_THYL.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridControlDS_THYL.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridControlDS_THYL.ExportToMht(exportFilePath);
                                    break;
                                default:
                                    break;
                            }
                            timerThongBao.Start();
                            lblThongBao.Visible = true;
                            lblThongBao.Text = "Export dữ liệu thành công!";
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra", "Thông báo !");
                }
            }
            else
            {
                timerThongBao.Start();
                lblThongBao.Visible = true;
                lblThongBao.Text = "Không có dữ liệu!";
            }

        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem.PerformClick();
            }
        }

        private void txtMaBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMaVP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem.PerformClick();
            }
        }

        private void txtMaVP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMaVP_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaVP.Text != "Mã viện phí")
            {
                // Hiển thị Text Hint Mã BN
                txtMaBN.ForeColor = SystemColors.GrayText;
                mmeMaPhieuYC.ForeColor = SystemColors.GrayText;
                txtMaBN.Text = "Mã bệnh nhân";
                mmeMaPhieuYC.Text = "Nhập mã phiếu thuốc/vật tư cách nhau bởi dấu phẩy (,)";
                this.txtMaBN.Leave += new System.EventHandler(this.txtMaBN_Leave);
                this.txtMaBN.Enter += new System.EventHandler(this.txtMaBN_Enter);
                this.mmeMaPhieuYC.Leave += new System.EventHandler(this.mmeMaDV_Leave);
                this.mmeMaPhieuYC.Enter += new System.EventHandler(this.mmeMaDV_Enter);
            }
        }

        private void txtMaBN_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaBN.Text != "Mã bệnh nhân")
            {
                // Hiển thị Text Hint Mã BN
                txtMaVP.ForeColor = SystemColors.GrayText;
                mmeMaPhieuYC.ForeColor = SystemColors.GrayText;
                txtMaVP.Text = "Mã viện phí";
                mmeMaPhieuYC.Text = "Nhập mã phiếu thuốc/vật tư cách nhau bởi dấu phẩy (,)";
                this.txtMaVP.Leave += new System.EventHandler(this.txtMaVP_Leave);
                this.txtMaVP.Enter += new System.EventHandler(this.txtMaVP_Enter);
                this.mmeMaPhieuYC.Leave += new System.EventHandler(this.mmeMaDV_Leave);
                this.mmeMaPhieuYC.Enter += new System.EventHandler(this.mmeMaDV_Enter);
            }
        }

        private void mmeMaPhieuYC_EditValueChanged(object sender, EventArgs e)
        {
            if (mmeMaPhieuYC.Text != "")
            {
                // Hiển thị Text Hint Mã BN
                txtMaVP.ForeColor = SystemColors.GrayText;
                txtMaBN.ForeColor = SystemColors.GrayText;
                txtMaVP.Text = "Mã viện phí";
                txtMaBN.Text = "Mã bệnh nhân";
                this.txtMaVP.Leave += new System.EventHandler(this.txtMaVP_Leave);
                this.txtMaVP.Enter += new System.EventHandler(this.txtMaVP_Enter);
                this.txtMaBN.Leave += new System.EventHandler(this.txtMaBN_Leave);
                this.txtMaBN.Enter += new System.EventHandler(this.txtMaBN_Enter);
            }
        }

        private void gridViewDS_THYL_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        // Chỉ cho nhập số và ký từ điểu khiển và dấu phẩy.
        private void mmeMaPhieuYC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar!=',')
            {
                e.Handled = true;
            }
        }

    }
}