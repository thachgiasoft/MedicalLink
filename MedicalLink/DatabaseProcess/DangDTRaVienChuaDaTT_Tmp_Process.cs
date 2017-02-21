﻿using MedicalLink.DatabaseProcess.FilterDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.DatabaseProcess
{
    public class DangDTRaVienChuaDaTT_Tmp_Process
    {
        private static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();


        internal static void SQLChay_DangDT_Tmp(DangDTRaVienChuaDaTTFilterDTO dangDTFilter)
        {
            try
            {
                String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string departmentgroupid = "";
                string loaivienphiid = "";

                if (dangDTFilter.departmentgroupid != 0)
                {
                    departmentgroupid = " and vpm.departmentgroupid='" + dangDTFilter.departmentgroupid + "' ";
                }
                if (dangDTFilter.loaivienphiid > -1)
                {
                    loaivienphiid = " and vpm.loaivienphiid='" + dangDTFilter.loaivienphiid + "' ";
                }

                string updateTmp = "INSERT INTO tools_dangdt_tmp(departmentgroupid, bn_chuyendi, bn_chuyenden, ravien_slbn, dangdt_slbn_bh, dangdt_slbn_vp, dangdt_slbn, dangdt_tienkb, dangdt_tienxn, dangdt_tiencdhatdcn, dangdt_tienpttt, dangdt_tiendvktc, dangdt_tiengiuong, dangdt_tienkhac, dangdt_tienvattu, dangdt_tienmau, dangdt_tienthuoc_bhyt, dangdt_tienthuoc_vp, dangdt_tienthuoc, dangdt_tongtien_bhyt, dangdt_tongtien_vp, dangdt_tongtien, dangdt_tamung, dangdt_date, loaibaocao, khoangdl_tu, chaytudong) SELECT vpm.departmentgroupid, '0' as bn_chuyendi, '0' as bn_chuyenden, '0' as ravien_slbn, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as dangdt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as dangdt_slbn_vp, count(vpm.*) as dangdt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as dangdt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as dangdt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as dangdt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as dangdt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as dangdt_tiendvktc, round(cast(sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as dangdt_tiengiuong, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as dangdt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as dangdt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as dangdt_tienmau,round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as dangdt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as dangdt_tienthuoc_vp, round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as dangdt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as dangdt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as dangdt_tongtien_vp,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as dangdt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as dangdt_tamung, '" + datetimeNow + "' as dangdt_date, '" + dangDTFilter.loaiBaoCao + "', '" + dangDTFilter.dateKhoangDLTu + "', '" + dangDTFilter.chayTuDong + "'  FROM vienphi_money vpm WHERE vpm.vienphistatus=0 " + loaivienphiid + departmentgroupid + " and vpm.vienphidate>='" + dangDTFilter.dateKhoangDLTu + "' GROUP BY vpm.departmentgroupid;";

                string sqlxoadulieuTmp = "DELETE FROM tools_dangdt_tmp vpm WHERE dangdt_date < '" + datetimeNow + "' and loaibaocao='" + dangDTFilter.loaiBaoCao + "' and khoangdl_tu='" + dangDTFilter.dateKhoangDLTu + "' " + departmentgroupid + " and chaytudong=" + dangDTFilter.chayTuDong + " ;";

                condb.ExecuteNonQuery(updateTmp);
                condb.ExecuteNonQuery(sqlxoadulieuTmp);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        internal static void SQLChay_RaVienChuaTT_Tmp(DangDTRaVienChuaDaTTFilterDTO RaVienChuaTTFilter)
        {
            try
            {
                String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string departmentgroupid = "";
                string loaivienphiid = "";

                if (RaVienChuaTTFilter.departmentgroupid != 0)
                {
                    departmentgroupid = " and vpm.departmentgroupid='" + RaVienChuaTTFilter.departmentgroupid + "' ";
                }
                if (RaVienChuaTTFilter.loaivienphiid > -1)
                {
                    loaivienphiid = " and vpm.loaivienphiid='" + RaVienChuaTTFilter.loaivienphiid + "' ";
                }

                string updateTmp = "INSERT INTO tools_ravienchuatt_tmp(departmentgroupid, ravienchuatt_slbn_bh, ravienchuatt_slbn_vp, ravienchuatt_slbn, ravienchuatt_tienkb, ravienchuatt_tienxn, ravienchuatt_tiencdhatdcn, ravienchuatt_tienpttt, ravienchuatt_tiendvktc, ravienchuatt_tiengiuong, ravienchuatt_tienkhac, ravienchuatt_tienvattu, ravienchuatt_tienmau, ravienchuatt_tienthuoc_bhyt, ravienchuatt_tienthuoc_vp, ravienchuatt_tienthuoc, ravienchuatt_tongtien_bhyt, ravienchuatt_tongtien_vp, ravienchuatt_tongtien, ravienchuatt_tamung, ravienchuatt_date, loaibaocao, khoangdl_tu, chaytudong) SELECT vpm.departmentgroupid, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as ravienchuatt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as ravienchuatt_slbn_vp, count(vpm.*) as ravienchuatt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as ravienchuatt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as ravienchuatt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as ravienchuatt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as ravienchuatt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as ravienchuatt_tiendvktc, round(cast(sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as ravienchuatt_tiengiuong, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as ravienchuatt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as ravienchuatt_tienmau,round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as ravienchuatt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as ravienchuatt_tienthuoc_vp, round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as ravienchuatt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as ravienchuatt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tongtien_vp,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as ravienchuatt_tamung, '" + datetimeNow + "' as ravienchuatt_date, '" + RaVienChuaTTFilter.loaiBaoCao + "', '" + RaVienChuaTTFilter.dateKhoangDLTu + "', '" + RaVienChuaTTFilter.chayTuDong + "' FROM vienphi_money vpm WHERE COALESCE(vpm.vienphistatus_vp,0)=0 " + loaivienphiid + departmentgroupid + " and vpm.vienphistatus<>0 and vpm.vienphidate>='" + RaVienChuaTTFilter.dateKhoangDLTu + "' GROUP BY vpm.departmentgroupid;";

                string sqlxoadulieuTmp = "DELETE FROM tools_ravienchuatt_tmp vpm WHERE ravienchuatt_date < '" + datetimeNow + "' and khoangdl_tu='" + RaVienChuaTTFilter.dateKhoangDLTu + "' and loaibaocao='" + RaVienChuaTTFilter.loaiBaoCao + "' " + departmentgroupid + " and chaytudong=" + RaVienChuaTTFilter.chayTuDong + " ;";
                condb.ExecuteNonQuery(updateTmp);
                condb.ExecuteNonQuery(sqlxoadulieuTmp);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        internal static void SQLChay_RaVienDaTT_Tmp(DangDTRaVienChuaDaTTFilterDTO RaVienDaTTFilter)
        {
            try
            {
                String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string departmentgroupid = "";
                string loaivienphiid = "";

                if (RaVienDaTTFilter.departmentgroupid != 0)
                {
                    departmentgroupid = " and vpm.departmentgroupid='" + RaVienDaTTFilter.departmentgroupid + "' ";
                }
                if (RaVienDaTTFilter.loaivienphiid > -1)
                {
                    loaivienphiid = " and vpm.loaivienphiid='" + RaVienDaTTFilter.loaivienphiid + "' ";
                }

                string updateTmp = "INSERT INTO tools_raviendatt_tmp(departmentgroupid, raviendatt_slbn_bh, raviendatt_slbn_vp, raviendatt_slbn, raviendatt_tienkb, raviendatt_tienxn, raviendatt_tiencdhatdcn, raviendatt_tienpttt, raviendatt_tiendvktc, raviendatt_tiengiuong, raviendatt_tienkhac, raviendatt_tienvattu, raviendatt_tienmau, raviendatt_tienthuoc_bhyt, raviendatt_tienthuoc_vp, raviendatt_tienthuoc, raviendatt_tongtien_bhyt, raviendatt_tongtien_vp, raviendatt_tongtien, raviendatt_tamung, loaibaocao, raviendatt_date, chaytudong) SELECT vpm.departmentgroupid, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as raviendatt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as raviendatt_slbn_vp, count(vpm.*) as raviendatt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as raviendatt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as raviendatt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as raviendatt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as raviendatt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as raviendatt_tiendvktc, round(cast(sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as raviendatt_tiengiuong, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as raviendatt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as raviendatt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as raviendatt_tienmau, round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as raviendatt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as raviendatt_tienthuoc_vp,round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as raviendatt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as raviendatt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as raviendatt_tongtien_vp, round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as raviendatt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as raviendatt_tamung, '" + RaVienDaTTFilter.loaiBaoCao + "', '" + datetimeNow + "' as raviendatt_date, " + RaVienDaTTFilter.chayTuDong + " FROM vienphi_money vpm WHERE COALESCE(vpm.vienphistatus_vp,0)=1 " + loaivienphiid + departmentgroupid + " and vpm.duyet_ngayduyet_vp >= '" + RaVienDaTTFilter.dateTu + "' and vpm.duyet_ngayduyet_vp <= '" + RaVienDaTTFilter.dateDen + "' GROUP BY vpm.departmentgroupid;";

                string sqlxoadulieuTmp = "DELETE FROM tools_RaVienDaTT_Tmp WHERE raviendatt_date < '" + datetimeNow + "' and loaibaocao='" + RaVienDaTTFilter.loaiBaoCao + "' and chaytudong=" + RaVienDaTTFilter.chayTuDong + " ;";

                condb.ExecuteNonQuery(updateTmp);
                condb.ExecuteNonQuery(sqlxoadulieuTmp);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }



    }
}