﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.Base;

namespace MedicalLink.FormCommon
{
    public partial class ucChucNang : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public string CurrentTabPage { get; set; }
        public int SelectedTabPageIndex { get; set; }
        internal frmMain frmMain;

        // khai báo 1 hàm delegate
        public delegate void GetString(string thoigian);
        // khai báo 1 kiểu hàm delegate
        public GetString MyGetData;

        #endregion
        public ucChucNang()
        {
            InitializeComponent();
        }

        #region Load
        private void ucChucNang_Load(object sender, EventArgs e)
        {
            try
            {
                //  KiemTraLicense_ChucNang();
                LoadDataDSChucNang();
                LoadDataDSBaoCao();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void LoadDataDSChucNang()
        {
            try
            {
                gridControlDSChucNang.DataSource = Base.SessionLogin.SessionLstPhanQuyen_ChucNang;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDataDSBaoCao()
        {
            try
            {
                gridControlDSBaoCao.DataSource = Base.SessionLogin.SessionLstPhanQuyen_Report;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Tabcontrol function
        //Dong tab
        private void xtraTabControlChucNang_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                XtraTabControl xtab = (XtraTabControl)sender;
                int i = xtab.SelectedTabPageIndex;
                DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs arg = e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs;
                xtab.TabPages.Remove((arg.Page as XtraTabPage));
                xtab.SelectedTabPageIndex = i - 1;
                //(arg.Page as XtraTabPage).PageVisible = false;
                System.GC.Collect();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void xtraTabControlChucNang_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                frmMain = new frmMain();
                this.CurrentTabPage = e.Page.Name;
                XtraTabControl xtab = new XtraTabControl();
                xtab = (XtraTabControl)sender;
                if (xtab != null)
                {
                    this.SelectedTabPageIndex = xtab.SelectedTabPageIndex;
                    //delegate - thong tin chuc nang
                    if (MyGetData != null)
                    {// tại đây gọi nó
                        MyGetData(xtab.TabPages[xtab.SelectedTabPageIndex].Tooltip);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Grid DS Chuc Nang
        private void gridViewDSChucNang_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridViewDSChucNang_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == gridColumnstt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void gridControlDSChucNang_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDSChucNang.FocusedRowHandle;
                txtThongTinChiTiet.Text = gridViewDSChucNang.GetRowCellValue(rowHandle, "permissionnote").ToString();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void gridViewDSChucNang_DoubleClick(object sender, EventArgs e)
        {
            UserControl ucControlActive = new UserControl();
            try
            {
                var rowHandle = gridViewDSChucNang.FocusedRowHandle;
                string code = gridViewDSChucNang.GetRowCellValue(rowHandle, "permissioncode").ToString();
                string name = gridViewDSChucNang.GetRowCellValue(rowHandle, "permissionname").ToString();
                string note = gridViewDSChucNang.GetRowCellValue(rowHandle, "permissionnote").ToString();
                if (Convert.ToBoolean(gridViewDSChucNang.GetRowCellValue(rowHandle, "permissioncheck")))
                {
                    //Chon ucControl
                    ucControlActive = TabControlProcess.SelectUCControlActive(code);
                    MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, code, name, note, ucControlActive);
                    ucControlActive.Show();
                }
                else
                {
                    MessageBox.Show("Bạn không được phân quyền sử dụng chức năng này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void gridViewDSChucNang_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                //GridView View = sender as GridView;
                //if (e.RowHandle >= 0)
                //{
                //    string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["permissioncheck"]);
                //    if (category == "Checked")
                //    {
                //        e.Appearance.BackColor = Color.DeepSkyBlue;
                //        e.Appearance.BackColor2 = Color.LightCyan;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Gird DS Bao Cao
        private void gridViewDSBaoCao_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridViewDSBaoCao_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == gridDSBCColumeStt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void gridViewDSBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDSBaoCao.FocusedRowHandle;
                txtThongTinChiTiet.Text = gridViewDSBaoCao.GetRowCellValue(rowHandle, "permissionnote").ToString();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void gridViewDSBaoCao_DoubleClick(object sender, EventArgs e)
        {
            UserControl ucControlActive = new UserControl();
            try
            {
                var rowHandle = gridViewDSBaoCao.FocusedRowHandle;
                string code = gridViewDSBaoCao.GetRowCellValue(rowHandle, "permissioncode").ToString();
                string name = gridViewDSBaoCao.GetRowCellValue(rowHandle, "permissionname").ToString();
                string note = gridViewDSBaoCao.GetRowCellValue(rowHandle, "permissionnote").ToString();
                if (Convert.ToBoolean(gridViewDSBaoCao.GetRowCellValue(rowHandle, "permissioncheck"))) //xemlai...
                {
                    //Chon ucControl
                    ucControlActive = TabControlProcess.SelectUCControlActive(code);
                    MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, code, name, note, ucControlActive);
                    ucControlActive.Show();
                }
                else
                {
                    MessageBox.Show("Bạn không được phân quyền sử dụng chức năng này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void gridViewDSBaoCao_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                //GridView View = sender as GridView;
                //if (e.RowHandle >= 0)
                //{
                //    string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["permissioncheck"]);
                //    if (category == "Checked")
                //    {
                //        e.Appearance.BackColor = Color.DeepSkyBlue;
                //        e.Appearance.BackColor2 = Color.LightCyan;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion





    }
}
