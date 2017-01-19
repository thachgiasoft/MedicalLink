﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;

namespace MedicalLink.HeThong
{
    public partial class ucDanhSachNhanVien : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string codeid, name;
        string worksheetName = "tools_tbluser";
        private DataView dmUser_Import;

        public ucDanhSachNhanVien()
        {
            InitializeComponent();
            btnNVOK.Enabled = false;
            txtNVID.Enabled = false;
            txtNVName.Enabled = false;
            txtIDHIS.Enabled = false;
        }

        private void btnNVThem_Click(object sender, EventArgs e)
        {
            txtNVID.Text = "";
            txtNVName.Text = "";
            txtIDHIS.Text = "";
            btnNVOK.Enabled = true;
            txtNVID.Enabled = true;
            txtNVName.Enabled = true;
            txtIDHIS.Enabled = true;
            txtNVID.Focus();
        }

        // Load danh sách nhân viên
        private void ucDanhSachNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                string sqldsnv = "SELECT userid as stt, usercode as manv, username as tennv, userhisid FROM tools_tbluser WHERE usergnhom in (2,3) ORDER BY manv";
                DataView dv = new DataView(condb.getDataTable(sqldsnv));
                if (dv.Count > 0)
                {
                    //Giải mã hiển thị lên Gridview
                    for (int i = 0; i < dv.Count; i++)
                    {
                        //itemcode += dataGridView1.Rows[i].Cells[2].Value.ToString();
                        string usercode_de = MedicalLink.Base.EncryptAndDecrypt.Decrypt(dv[i]["manv"].ToString(), true);
                        string username_de = MedicalLink.Base.EncryptAndDecrypt.Decrypt(dv[i]["tennv"].ToString(), true);
                        dv[i]["manv"] = usercode_de;
                        dv[i]["tennv"] = username_de;
                    }
                    gridControlDSNV.DataSource = dv;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gridViewDSNV_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDSNV.FocusedRowHandle;
                codeid = gridViewDSNV.GetRowCellValue(rowHandle, "manv").ToString();
                txtNVID.Enabled = true;
                txtNVName.Enabled = true;
                btnNVOK.Enabled = true;
                txtIDHIS.Enabled = true;
                txtNVID.Text = codeid;
                txtNVName.Text = gridViewDSNV.GetRowCellValue(rowHandle, "tennv").ToString();
                txtIDHIS.Text = gridViewDSNV.GetRowCellValue(rowHandle, "userhisid").ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        // Thêm, sửa danh sách nhân viên
        private void btnNVOK_Click_1(object sender, EventArgs e)
        {
            // Mã hóa tài khoản
            string en_txtNVID = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtNVID.Text.Trim(), true);
            string en_txtNVName = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtNVName.Text.Trim(), true);
            string en_pass = MedicalLink.Base.EncryptAndDecrypt.Encrypt("", true);

            try
            {
                if (txtNVID.Text != codeid)
                {
                    string sql = "INSERT INTO tools_tbluser(usercode, username, userpassword, userstatus, usergnhom, usernote, userhisid) VALUES ('" + en_txtNVID + "','" + en_txtNVName + "','" + en_pass + "','0','2','Nhân viên', '" + txtIDHIS.Text.Trim() + "');";
                    if (condb.ExecuteNonQuery(sql))
                    {
                        HienThiThongBao(MedicalLink.Base.ThongBaoLable.THEM_MOI_THANH_CONG);
                    }
                    gridControlDSNV.DataSource = null;
                    ucDanhSachNhanVien_Load(null, null);
                }
                else
                {
                    string sql = "UPDATE tools_tbluser SET usercode='" + en_txtNVID + "', username='" + en_txtNVName + "', userpassword='" + en_pass + "', userstatus='0', usergnhom='2', usernote='' , userhisid = '" + txtIDHIS.Text.Trim() + "' WHERE usercode='" + en_txtNVID + "';";
                    if (condb.ExecuteNonQuery(sql))
                    {
                        HienThiThongBao(MedicalLink.Base.ThongBaoLable.SUA_THANH_CONG);
                    }
                    gridControlDSNV.DataSource = null;
                    ucDanhSachNhanVien_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void gridViewDSNV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        //Import tu file Excel
        private void btnThemTuExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    MedicalLink.Base.ReadExcelFile _excel = new MedicalLink.Base.ReadExcelFile(openFileDialogSelect.FileName);
                    var data = _excel.GetDataTable("SELECT USERCODE,USERNAME,USERPASSWORD,USERSTATUS,USERGNHOM,USERNOTE,USERHISID FROM [" + worksheetName + "$]");
                    if (data != null)
                    {
                        int dem_update = 0;
                        int dem_insert = 0;
                        //gridViewDichVu_Import.DataSource = data;
                        dmUser_Import = new DataView(data);

                        for (int i = 0; i < dmUser_Import.Count; i++)
                        {
                            // Mã hóa tài khoản
                            string en_txtNVCode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(dmUser_Import[i]["USERCODE"].ToString().Trim(), true);
                            string en_txtNVName = MedicalLink.Base.EncryptAndDecrypt.Encrypt(dmUser_Import[i]["USERNAME"].ToString().Trim(), true);
                            string en_pass = MedicalLink.Base.EncryptAndDecrypt.Encrypt("", true);
                            if (dmUser_Import[i]["USERCODE"].ToString() != "")
                            {
                                condb.connect();
                                string sql_kt = "SELECT usercode FROM tools_tbluser WHERE usercode='" + en_txtNVCode + "';";
                                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                                if (dv_kt.Count > 0) //update
                                {
                                    string sql_updateUser = "UPDATE tools_tbluser SET username='" + en_txtNVName + "', userhisid='" + dmUser_Import[i]["USERHISID"] + "' WHERE usercode='" + en_txtNVCode + "';";
                                    try
                                    {
                                        condb.ExecuteNonQuery(sql_updateUser);
                                        dem_update += 1;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    string sql_insertDVKT = "INSERT INTO tools_tbluser(usercode, username, userpassword, userstatus, usergnhom, usernote,userhisid) VALUES ('" + en_txtNVCode + "','" + en_txtNVName + "','" + en_pass + "','0','3','Nhân viên', '" + dmUser_Import[i]["USERHISID"] + "');";
                                    try
                                    {
                                        condb.ExecuteNonQuery(sql_insertDVKT);
                                        dem_insert += 1;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        MessageBox.Show("Thêm mới [ " + dem_insert + " ] & cập nhật [ " + dem_update + " ] nhân viên thành công.");
                        gridControlDSNV.DataSource = null;
                        ucDanhSachNhanVien_Load(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gridViewDSNV_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception)
            {

            }
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }
        private void HienThiThongBao(string tenThongBao)
        {
            try
            {
                timerThongBao.Start();
                lblThongBao.Visible = true;
                lblThongBao.Text = tenThongBao; 
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
