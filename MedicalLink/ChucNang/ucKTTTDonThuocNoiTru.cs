﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using System.IO;

namespace MedicalLink.ChucNang
{
    public partial class ucKTTTDonThuocNoiTru : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string datetungay = "";
        string datedenngay = "";
        long soluonghoso = 0;
        //DataView dv_bhytgroup;
        public ucKTTTDonThuocNoiTru()
        {
            InitializeComponent();
        }

        // Mở file Excel hiển thị lên DataGridView
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                gridControlHSBA.DataSource = null;

                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                if (dateTuNgay.Text != "" && dateDenNgay.Text != "")
                {
                    // Lấy từ ngày, đến ngày
                    datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                    string sql_timkiem = "SELECT mbp.maubenhphamid, mbp.sophieu, vp.vienphiid, vp.patientid, hsba.hosobenhanid, hsba.patientname, bhyt.bhytcode, kcd.departmentgroupname, pcd.departmentname, mbp.maubenhphamdate, vp.duyet_ngayduyet_vp as vienphidate, case mbp.maubenhphamgrouptype when 5 then 'Thuốc' else 'Vật tư' end as maubenhphamgrouptype, case mbp.maubenhphamstatus when 1 then 'Đang yêu cầu' else 'Đã tổng hợp y lệnh' end as maubenhphamstatus FROM vienphi vp INNER JOIN maubenhpham mbp ON vp.vienphiid=mbp.vienphiid and mbp.isloaidonthuoc='0' and mbp.maubenhphamstatus in (1,4) and mbp.maubenhphamgrouptype in (5,6) INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pcd ON pcd.departmentid=mbp.departmentid INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=mbp.departmentgroupid INNER JOIN hosobenhan hsba ON hsba.hosobenhanid=vp.hosobenhanid INNER JOIN bhyt ON bhyt.bhytid=vp.bhytid INNER JOIN medicine_store ms ON ms.medicinestoreid=mbp.medicinestoreid and ms.medicinestoretype<>4 WHERE vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ORDER BY vp.duyet_ngayduyet_vp;";

                    DataView dv = new DataView(condb.GetDataTable_HIS(sql_timkiem));
                    if (dv != null && dv.Count > 0)
                    {
                        gridControlHSBA.DataSource = dv;
                        soluonghoso = dv.Count;
                    }
                    else
                    {
                        soluonghoso = 0;
                        gridControlHSBA.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }

                }
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


        private void ucUpdateDataSerPrice_Load(object sender, EventArgs e)
        {
            try
            {
                //Lấy thời gian lấy BC mặc định là ngày hiện tại
                DateTime date_tu = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateTuNgay.Value = date_tu;
                DateTime date_den = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                dateDenNgay.Value = date_den;
                gridControlHSBA.DataSource = null;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewHSBA.RowCount > 0)
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
                                        gridViewHSBA.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        gridViewHSBA.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        gridViewHSBA.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        gridViewHSBA.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        gridViewHSBA.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        gridViewHSBA.ExportToMht(exportFilePath);
                                        break;
                                    default:
                                        break;
                                }
                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                                frmthongbao.Show();
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
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }

        }

        private void gridViewDichVu_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
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

        private void btnSuaTTHSBA_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                if (dateTuNgay.Text != "" && dateDenNgay.Text != "")
                {
                    string sqlupdate = "UPDATE medicalrecord SET medicalrecordstatus ='99' WHERE medicalrecordid IN (SELECT medicalrecord.medicalrecordid FROM vienphi INNER JOIN medicalrecord ON vienphi.medicalrecordid_end=medicalrecord.medicalrecordid  INNER JOIN department ON medicalrecord.departmentid=department.departmentid INNER JOIN hosobenhan ON hosobenhan.hosobenhanid=vienphi.hosobenhanid INNER JOIN bhyt ON bhyt.bhytid=vienphi.bhytid  WHERE medicalrecord.medicalrecordstatus <>'99' and vienphi.duyet_ngayduyet_vp > '" + datetungay + "' and vienphi.duyet_ngayduyet_vp < '" + datedenngay + "') ;";
                    condb.ExecuteNonQuery_HIS(sqlupdate);

                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Cập nhật thành công [ " + soluonghoso.ToString() + " ] hồ sơ");
                    frmthongbao.Show();
                }
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
                SplashScreenManager.CloseForm();
            }
        }
    }
}
