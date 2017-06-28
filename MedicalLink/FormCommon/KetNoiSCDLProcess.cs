﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.FormCommon
{
    internal static class KetNoiSCDLProcess
    {
        private static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        internal static bool CapNhatCoSoDuLieu()
        {
            bool result = true;
            try
            {
                result = KetNoiSCDLProcess.CreateTableTblUser();
                result = KetNoiSCDLProcess.CreateTableTblPermission();
                result = KetNoiSCDLProcess.CreateTableTblDepartment();
                result = KetNoiSCDLProcess.CreateTableTblLog();
                result = KetNoiSCDLProcess.CreateTableTblUpdateKhaDung();
                result = KetNoiSCDLProcess.CreateTableLicense();
                result = KetNoiSCDLProcess.CreateTableTblDVKTBHYTChenh();
                result = KetNoiSCDLProcess.CreateTableTblDVKTBHYTChenhNew();
                result = KetNoiSCDLProcess.CreateTableOption();
                result = KetNoiSCDLProcess.CreateTableTblNhanVien();
                result = KetNoiSCDLProcess.CreateTableUserMedicineStore();
                result = KetNoiSCDLProcess.CreateTableUserMedicinePhongLuu();
                result = KetNoiSCDLProcess.CreateTableOtherTypeList();
                result = KetNoiSCDLProcess.CreateTableOtherList();
                //result = KetNoiSCDLProcess.CreateTableToolsServicepricePttt();


                //result= KetNoiSCDLProcess.UpdateTableUser();
                result = KetNoiSCDLProcess.CreateTableUserDepartmentgroup();
                result = KetNoiSCDLProcess.CreateTableVersion();
                //result = KetNoiSCDLProcess.CreateFunctionByteaImport();
                // result = KetNoiSCDLProcess.CreateView_Tools_Serviceprice_Pttt();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi Update DB" + ex.ToString());
            }
            return result;
        }

        #region Tao bang
        private static bool CreateTableTblUser()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_tbluser ( userid serial NOT NULL, usercode text NOT NULL, username text, userpassword text, userstatus integer, usergnhom integer, usernote text, userhisid integer, CONSTRAINT tools_tbluser_pkey PRIMARY KEY (userid));";
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblUser" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableTblPermission()
        {
            bool result = false;
            try
            {
                string sql_tblper = "CREATE TABLE IF NOT EXISTS tools_tbluser_permission ( userpermissionid serial NOT NULL, permissionid integer, permissioncode text, permissionname text, userid integer, usercode text, permissioncheck boolean, userpermissionnote text, CONSTRAINT userpermissionid_pkey PRIMARY KEY (userpermissionid));";
                if (condb.ExecuteNonQuery_MeL(sql_tblper))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblPermission" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableTblDepartment()
        {
            bool result = false;
            try
            {
                string sql_toolsdepatment = "CREATE TABLE IF NOT EXISTS tools_depatment (tools_depatmentid serial NOT NULL, departmentgroupid integer, departmentgroupcode text, departmentgroupname text, departmentgrouptype integer, departmentid integer, departmentcode text, departmentname text, departmenttype integer, CONSTRAINT tools_depatment_pkey PRIMARY KEY (tools_depatmentid));";
                string sql_deletepatient = "DELETE FROM tools_depatment;";
                string sql_insert = "INSERT INTO tools_depatment(departmentgroupid, departmentgroupcode, departmentgroupname, departmentgrouptype, departmentid, departmentcode, departmentname, departmenttype) SELECT degp.departmentgroupid as departmentgroupid, degp.departmentgroupcode as departmentgroupcode, degp.departmentgroupname as departmentgroupname, degp.departmentgrouptype, de.departmentid as departmentid, de.departmentcode as departmentcode, de.departmentname as departmentname, de.departmenttype FROM departmentgroup degp,department de WHERE de.departmentgroupid = degp.departmentgroupid ORDER BY degp.departmentgroupid;";

                if (condb.ExecuteNonQuery_HIS(sql_toolsdepatment) && condb.ExecuteNonQuery_HIS(sql_deletepatient) && condb.ExecuteNonQuery_HIS(sql_insert))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblDepartment" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableTblLog()
        {
            bool result = false;
            try
            {
                string sql_tbllog = "CREATE TABLE IF NOT EXISTS tools_tbllog (logid serial NOT NULL,loguser text, logvalue text,ipaddress text,computername text,softversion text,logtime timestamp without time zone,CONSTRAINT tools_tbllog_pkey PRIMARY KEY (logid));";
                if (condb.ExecuteNonQuery_MeL(sql_tbllog))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblLog" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableTblUpdateKhaDung()
        {
            bool result = false;
            try
            {
                string sql_tbllog_updatekhadung = "CREATE TABLE IF NOT EXISTS tools_tbllog_updatekhadung (logupdateid serial NOT NULL, tgcapnhat timestamp without time zone, khothuoc_id integer, kho_id integer, kho_ma text, kho_ten text, thuoc_id integer, thuoc_ma text, thuoc_ten text, thuoc_dvt text, slkhadung double precision, sltonkho double precision, slcapnhat double precision, trangthaicapnhat integer, gianhap text, giaban text, loguser text, CONSTRAINT tools_tbllog_updatekhadung_pkey PRIMARY KEY (logupdateid));";
                if (condb.ExecuteNonQuery_MeL(sql_tbllog_updatekhadung))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblUpdateKhaDung" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableColumeBackupDichVu()
        {
            bool result = false;
            try
            {
                //Thêm cột để chứa dữ liệu backup giá dịch vụ cũ (có thể bỏ ở bản sau vì chạy đc 1 lần thôi)
                string sql_insert_colum = "ALTER TABLE ServicePriceRef ADD Tools_TGApDung_bak_1 timestamp without time zone; ALTER TABLE ServicePriceRef ADD Tools_gia_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaNhanDan_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaBHYT_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaNuocNgoai_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_KieuApDung_bak_1 integer DEFAULT 0;";

                if (condb.ExecuteNonQuery_HIS(sql_insert_colum)) // có thể bỏ
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableColumeBackupDichVu" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableTblDVKTBHYTChenh()
        {
            bool result = false;
            try
            {
                string sql_insert_dvbhyt = "CREATE TABLE IF NOT EXISTS tools_dvktbhytchenh (dvktbhytchenhid serial NOT NULL, MaDDVKT_CODE text , MaDVKT_Cu text, TenDVKT_Cu text, MaDVKTBHYT_Cu text, DonGia_Cu double precision, MaDVKT_TuongDuong text, MaDVKT_Moi text, TenDVKT_Moi text, MaDVKTBHYT_Moi text, DonGia_Moi double precision, is_lock double precision, CONSTRAINT tools_dvktbhytchenh_pkey PRIMARY KEY (dvktbhytchenhid));";
                condb.ExecuteNonQuery_MeL(sql_insert_dvbhyt);
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblDVKTBHYTChenh" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableTblDVKTBHYTChenhNew()
        {
            bool result = false;
            try
            {
                string sql_insert_dvbhyt = "CREATE TABLE IF NOT EXISTS tools_dvktbhytchenh_new (dvktbhytchenhnewid SERIAL NOT NULL, servicecode text, dvkt_code_cu text, dvkt_code_moi text, dvkt_ten text, dongia_cu_1 double precision, dongia_hientai  double precision, dongia_moi_2  double precision, CONSTRAINT tools_dvktbhytchenh_new_pkey PRIMARY KEY (dvktbhytchenhnewid));";
                if (condb.ExecuteNonQuery_MeL(sql_insert_dvbhyt))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblDVKTBHYTChenhNew" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableOption()
        {
            bool result = false;
            try
            {
                string sqloption = "CREATE TABLE IF NOT EXISTS tools_option(toolsoptionid serial NOT NULL, toolsoptioncode text, toolsoptionname text, toolsoptionvalue text, toolsoptionnote text, toolsoptionlook integer, toolsoptiondate timestamp without time zone, toolsoptioncreateuser text, CONSTRAINT tools_option_pkey PRIMARY KEY (toolsoptionid));";
                if (condb.ExecuteNonQuery_MeL(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableOption" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableUserDepartmentgroup()
        {
            bool result = false;
            try
            {
                string sqloption = "CREATE TABLE IF NOT EXISTS tools_tbluser_departmentgroup(userdepgid serial NOT NULL, departmentgroupid integer, departmentid integer, departmenttype integer, usercode text,  userdepgidnote text, CONSTRAINT tbluser_departmentgroup_pkey PRIMARY KEY (userdepgid));";
                if (condb.ExecuteNonQuery_MeL(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableUserDepartmentgroup" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableVersion()
        {
            bool result = false;
            try
            {
                string sqloption = "CREATE TABLE IF NOT EXISTS tools_version (versionid serial NOT NULL, appversion text, app_link text,  app_type integer, updateapp bytea, appsize integer, sqlversion text, updatesql bytea, sqlsize integer, sync_flag integer,  update_flag integer,  CONSTRAINT tools_version_pkey PRIMARY KEY (versionid));   ";
                if (condb.ExecuteNonQuery_MeL(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableSersion" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableLicense()
        {
            bool result = false;
            try
            {
                string sql_tbltools_license = "CREATE TABLE IF NOT EXISTS tools_license (licenseid serial NOT NULL, datakey text, licensekey text, CONSTRAINT tools_license_pkey PRIMARY KEY (licenseid));";
                if (condb.ExecuteNonQuery_MeL(sql_tbltools_license))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableLicense" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableTblNhanVien()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_tblnhanvien ( nhanvienid serial NOT NULL, usercode text NOT NULL, username text, userpassword text, userstatus integer, usergnhom integer, usernote text, userhisid integer, CONSTRAINT tools_tblnhanvien_pkey PRIMARY KEY (nhanvienid));";
                if (condb.ExecuteNonQuery_HIS(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblNhanVien" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableUserMedicineStore()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_tbluser_medicinestore( usermestid serial NOT NULL, medicinestoreid integer, medicinestoretype integer, usercode text, userdepgidnote text, CONSTRAINT tools_tbluser_medicinestore_pkey PRIMARY KEY (usermestid) );";
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableUserMedicineStore" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableUserMedicinePhongLuu()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_tbluser_medicinephongluu( userphongluutid serial NOT NULL, medicinephongluuid integer, medicinestoreid integer, usercode text, userdepgidnote text, CONSTRAINT tools_tbluser_medicinephongluu_pkey PRIMARY KEY (userphongluutid) );";
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableUserMedicinePhongLuu" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableToolsServicepricePttt()
        {
            bool result = false;
            try
            {
                string ServicePttt = "CREATE TABLE IF NOT EXISTS tools_serviceprice_pttt ( servicepriceptttid serial NOT NULL, vienphiid integer, patientid integer, bhytid integer, hosobenhanid integer, loaivienphiid integer, vienphistatus integer, khoaravien integer, phongravien integer, doituongbenhnhanid integer, vienphidate timestamp without time zone, vienphidate_ravien timestamp without time zone, duyet_ngayduyet timestamp without time zone, vienphistatus_vp integer, duyet_ngayduyet_vp timestamp without time zone, vienphistatus_bh integer, duyet_ngayduyet_bh timestamp without time zone, bhyt_tuyenbenhvien integer, departmentid integer, departmentgroupid integer, departmentgroup_huong integer, money_khambenh_bh double precision, money_khambenh_vp double precision, money_xetnghiem_bh double precision, money_xetnghiem_vp double precision, money_cdha_bh double precision, money_cdha_vp double precision, money_tdcn_bh double precision, money_tdcn_vp double precision, money_pttt_bh double precision, money_pttt_vp double precision, money_mau_bh double precision, money_mau_vp double precision, money_giuongthuong_bh double precision, money_giuongthuong_vp double precision, money_giuongyeucau_bh double precision, money_giuongyeucau_vp double precision, money_vanchuyen_bh double precision, money_vanchuyen_vp double precision, money_khac_bh double precision, money_khac_vp double precision, money_phuthu_bh double precision, money_phuthu_vp double precision, money_thuoc_bh double precision, money_thuoc_vp double precision, money_vattu_bh double precision, money_vattu_vp double precision, money_vtthaythe_bh double precision, money_vtthaythe_vp double precision, money_dvktc_bh double precision, money_dvktc_vp double precision, money_chiphikhac double precision, money_hpngaygiuong double precision, money_hppttt double precision, money_hpdkpttt_gm_thuoc double precision, money_hpdkpttt_gm_vattu double precision, money_dkpttt_thuoc_bh double precision, money_dkpttt_thuoc_vp double precision, money_dkpttt_vattu_bh double precision, money_dkpttt_vattu_vp double precision, CONSTRAINT tools_serviceprice_pttt_pkey PRIMARY KEY (servicepriceptttid) );";
                if (condb.ExecuteNonQuery_HIS(ServicePttt))
                {

                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableToolsServicepricePttt" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableOtherTypeList()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_othertypelist ( tools_othertypelistid serial NOT NULL, tools_othertypelistcode text, tools_othertypelistname text, tools_othertypeliststatus integer, CONSTRAINT tools_othertypelist_pkey PRIMARY KEY (tools_othertypelistid) ); ";
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableOtherTypeList" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableOtherList()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_otherlist ( tools_otherlistid serial NOT NULL, tools_othertypelistid integer, tools_otherlistcode text, tools_otherlistname text, tools_otherlistvalue text, tools_otherliststatus integer, CONSTRAINT tools_otherlist_pkey PRIMARY KEY (tools_otherlistid) );";
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableOtherList" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region Cap nhat sua chua bang
        #endregion

        #region Tao View

        private static bool CreateView_Tools_Serviceprice_Pttt() //ngay 19/5 sử dụng tools_serviceprice_pttt ngay  v 1.15 ngay 22/5
        {
            bool result = false;
            try
            {
                string sql_insert_bcbndangdt = "  ";
                if (condb.ExecuteNonQuery_HIS(sql_insert_bcbndangdt))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi tao VIEW CreateView_Tools_Serviceprice_Pttt" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region Tao Function

        private static bool CreateFunctionByteaImport()
        {
            bool result = false;
            try
            {
                string sqloption = " create or replace function bytea_import(p_path text, p_result out bytea) language plpgsql as $$ declare l_oid oid; r record; begin p_result := ''; select lo_import(p_path) into l_oid; for r in ( select data from pg_largeobject where loid = l_oid order by pageno ) loop p_result = p_result || r.data; end loop; perform lo_unlink(l_oid); end;$$;";
                if (condb.ExecuteNonQuery_HIS(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateFunctionByteaimport" + ex.ToString());
            }
            return result;
        }
        #endregion
    }
}
