
  departmentid serial NOT NULL,
  departmentgroupid integer,
  departmentgroupid_noitru integer,
  medicinestoreid integer,
  appid integer,
  departmentnumber integer,
  departmentcode text,
  madaudocthe text,
  departmentname text,
  departmenttype integer,
  loaibenhanid integer,
  isphongluu integer,
  departmentremark text,
  listdepartmentlinhthuoc text,
  listdepartmentphongchidinh text,
  thoigianthuchien text,
  version timestamp without time zone,
  departmentnameck text,
  sothutuphongkham integer,
  maphongkham text,
  chuyenkhoaphongkham text,
  iskhonghoatdong integer,
  barcodeformat text,
  sync_flag integer,
  update_flag integer,
  sothutuuutien_max integer,
  sothutuuutien_lock integer,
  sothutuuutien_lastupdate timestamp without time zone,
  isphongcapcuu integer,
  departmentcode_byt text,
  CONSTRAINT department_pkey PRIMARY KEY (departmentid)
)
CREATE TABLE medicalrecord
(
  medicalrecordid serial NOT NULL,
  medicalrecordcode text,
  sothutuid integer,
  sothutunumber integer,
  sothutuphongkhamid integer,
  sothutuphongkhamnumber integer,
  vienphiid integer,
  hosobenhanid integer DEFAULT 0,
  medicalrecordid_next integer DEFAULT 0,
  medicalrecordid_master integer,
  medicalrecordstatus integer,
  departmentgroupid integer,
  departmentid integer,
  giuong text,
  loaibenhanid integer,
  userid integer,
  patientid integer,
  doituongbenhnhanid integer,
  bhytid integer,
  lydodenkham text,
  yeucaukham text,
  thoigianvaovien timestamp without time zone,
  chandoanvaovien text,
  chandoanvaovien_code text,
  chandoanvaovien_kemtheo text,
  chandoanvaovien_kemtheo_code text,
  chandoankkb text,
  chandoankkb_code text,
  chandoanvaokhoa text,
  chandoanvaokhoa_code text,
  chandoanvaokhoa_kemtheo text,
  chandoanvaokhoa_kemtheo_code text,
  isthuthuat integer,
  isphauthuat integer,
  hinhthucvaovienid integer,
  backdepartmentid integer,
  uutienkhamid integer,
  noigioithieuid integer,
  vaoviencungbenhlanthu integer,
  thoigianravien timestamp without time zone,
  chandoanravien text,
  chandoanravien_code text,
  chandoanravien_kemtheo text,
  chandoanravien_kemtheo_code text,
  chandoanravien_kemtheo1 text,
  chandoanravien_kemtheo_code1 text,
  chandoanravien_kemtheo2 text,
  chandoanravien_kemtheo_code2 text,
  xutrikhambenhid integer,
  hinhthucravienid integer,
  ketquadieutriid integer,
  nextdepartmentid integer,
  nexthospitalid integer,
  istaibien integer,
  isbienchung integer,
  giaiphaubenhid integer DEFAULT 0,
  lydovaovien text,
  vaongaythucuabenh integer,
  quatrinhbenhly text,
  tiensubenh_banthan text,
  tiensubenh_giadinh text,
  khambenh_toanthan text,
  khambenh_mach text,
  khambenh_nhietdo text,
  khambenh_huyetap_low text,
  khambenh_huyetap_high text,
  khambenh_nhiptho text,
  khambenh_cannang text,
  khambenh_chieucao text,
  khambenh_vongnguc text,
  khambenh_vongdau text,
  khambenh_bophan text,
  tomtatkqcanlamsang text,
  chandoanbandau text,
  daxuly text,
  tomtatbenhan text,
  chandoankhoakhambenh text,
  daxulyotuyenduoi text,
  medicalrecordremark text,
  lastaccessdate timestamp without time zone,
  version timestamp without time zone,
  chandoantuyenduoi text,
  chandoantuyenduoi_code text,
  noigioithieucode text,
  canlamsangstatus integer,
  sync_flag integer,
  update_flag integer,
  lastuserupdated integer,
  lasttimeupdated timestamp without time zone,
  keylock integer,
  cv_chuyenvien_hinhthucid integer,
  cv_chuyenvien_lydoid integer,
  cv_chuyendungtuyen integer,
  cv_chuyenvuottuyen integer,
  xetnghiemcanthuchienlai text,
  loidanbacsi text,
  chandoanbandau_code text,
  nextbedrefid integer,
  nextbedrefid_nguoinha text,
  thoigianchuyenden timestamp without time zone,
  khambenh_thilucmatphai text,
  khambenh_thilucmattrai text,
  khambenh_klthilucmatphai text,
  khambenh_klthilucmattrai text,
  khambenh_nhanapmatphai text,
  khambenh_nhanapmattrai text,
  CONSTRAINT medicalrecord_pkey PRIMARY KEY (medicalrecordid)
)
CREATE TABLE vienphi
(
  vienphiid serial NOT NULL,
  vienphicode text,
  loaivienphiid integer,
  vienphistatus integer,
  departmentgroupid integer,
  departmentid integer,
  hosobenhanid integer,
  doituongbenhnhanid integer,
  dathutienkham integer,
  dagiuthebhyt integer,
  bhytid integer,
  patientid integer,
  vienphidate timestamp without time zone,
  vienphidate_ravien timestamp without time zone,
  chandoanvaovien text,
  chandoanvaovien_code text,
  chandoanravien text,
  chandoanravien_code text,
  chandoanravien_kemtheo text,
  chandoanravien_kemtheo_code text,
  bhyt_traituyen double precision,
  bhyt_thangluongtoithieu double precision,
  bhyt_gioihanbhyttrahoantoan double precision,
  duyet_ngayduyet timestamp without time zone,
  duyet_nguoiduyet integer,
  duyet_sothutuduyet integer,
  vienphistatus_bh integer,
  duyet_ngayduyet_bh timestamp without time zone,
  duyet_nguoiduyet_bh integer,
  duyet_sothutuduyet_bh integer,
  vienphistatus_vp integer,
  duyet_ngayduyet_vp timestamp without time zone,
  duyet_nguoiduyet_vp integer,
  duyet_sothutuduyet_vp integer,
  vienphistatus_tk integer,
  duyet_ngayduyet_tk timestamp without time zone,
  duyet_nguoiduyet_tk integer,
  duyet_sothutuduyet_tk integer,
  tongtiendichvu double precision,
  tongtiendichvu_bh double precision,
  tongtiendichvu_dv double precision,
  tongtiendichvu_bhyt double precision,
  tongtienmiengiam double precision,
  tongtiendatra double precision,
  tongtiendatra_bh double precision,
  tongtiendatra_dv double precision,
  tongtiendatra_tk double precision,
  isneedupdatemoney integer DEFAULT 0,
  isneedupdateinfo integer DEFAULT 0,
  money_khambenh double precision,
  money_xetnghiem double precision,
  money_chandoanhinhanh double precision,
  money_thamdochucnang double precision,
  money_thuoctrongdanhmuc double precision,
  money_thuocngoaidanhmuc double precision,
  money_mauchephammau double precision,
  money_phauthuatthuthuat double precision,
  money_vattutrongdanhmuc double precision,
  money_vattutrongdanhmuctt double precision,
  money_vattungoaidanhmuc double precision,
  money_dichvukythuatcao double precision,
  money_thuocungthungoaidanhmuc double precision,
  money_ngaygiuongchuyenkhoa double precision,
  money_vanchuyen double precision,
  money_phuthu double precision,
  money_dv_khambenh double precision,
  money_dv_xetnghiem double precision,
  money_dv_chandoanhinhanh double precision,
  money_dv_thamdochucnang double precision,
  money_dv_thuoctrongdanhmuc double precision,
  money_dv_thuocngoaidanhmuc double precision,
  money_dv_mauchephammau double precision,
  money_dv_phauthuatthuthuat double precision,
  money_dv_vattutrongdanhmuc double precision,
  money_dv_vattutrongdanhmuctt double precision,
  money_dv_vattungoaidanhmuc double precision,
  money_dv_dichvukythuatcao double precision,
  money_dv_thuocungthungoaidanhmuc double precision,
  money_dv_ngaygiuongchuyenkhoa double precision,
  money_dv_vanchuyen double precision,
  money_dv_phuthu double precision,
  lastaccessdate timestamp without time zone,
  vienphiremark text,
  version timestamp without time zone,
  duyet_quyduyet text,
  vienphidate_noitru timestamp without time zone,
  medicalrecordid_start integer,
  medicalrecordid_end integer,
  sync_flag integer,
  update_flag integer,
  datronvien integer,
  departmentgroupid_start integer,
  departmentid_start integer,
  bhyt_tuyenbenhvien integer,
  masothue text,
  sotaikhoan text,
  CONSTRAINT vienphi_pkey PRIMARY KEY (vienphiid)
)
bhyt_tuyenbenhvien

CREATE TABLE serviceprice
(
  servicepriceid serial NOT NULL,
  medicalrecordid integer,
  vienphiid integer DEFAULT 0,
  hosobenhanid integer DEFAULT 0,
  maubenhphamid integer,
  maubenhphamphieutype integer DEFAULT 0,
  servicepriceid_master integer DEFAULT 0,
  thuockhobanle integer DEFAULT 0,
  doituongbenhnhanid integer DEFAULT 0,
  loaidoituong_org integer DEFAULT 0,
  loaidoituong_org_remark text,
  loaidoituong integer DEFAULT 0,
  loaiduyetbhyt integer DEFAULT 0,
  loaidoituong_remark text,
  loaidoituong_userid integer DEFAULT 0,
  departmentid integer DEFAULT 0,
  departmentgroupid integer DEFAULT 0,
  servicepricecode text,
  servicepricename text,
  servicepricename_nhandan text,
  servicepricename_bhyt text,
  servicepricename_nuocngoai text,
  servicepricedate timestamp without time zone,
  servicepricestatus integer,
  servicepricedoer text,
  servicepricecomment text,
  servicepricemoney double precision,
  servicepricemoney_nhandan double precision,
  servicepricemoney_bhyt double precision,
  servicepricemoney_nuocngoai double precision,
  servicepricemoney_bhyt_tra double precision,
  servicepricemoney_miengiam double precision,
  servicepricemoney_danop double precision,
  servicepricemoney_miengiam_type integer DEFAULT 0,
  billid_thutien integer DEFAULT 0,
  billid_hoantien integer DEFAULT 0,
  billid_clbh_thutien integer DEFAULT 0,
  billid_clbh_hoantien integer DEFAULT 0,
  billaccountid integer DEFAULT 0,
  soluong double precision,
  soluongbacsi double precision,
  huongdansudung text,
  version timestamp without time zone,
  loaipttt integer DEFAULT 0,
  soluongquyettoan double precision DEFAULT 0,
  servicepriceid_xuattoan double precision DEFAULT 0,
  daduyetthuchiencanlamsang integer DEFAULT 0,
  sync_flag integer,
  update_flag integer,
  servicepricemoney_bhyt_danop double precision,
  servicepricemoney_damiengiam double precision,
  loaidoituong_xuat integer,
  servicepricemoney_tranbhyt double precision,
  servicepricebhytdinhmuc text,
  servicepricebhytquydoi text,
  servicepricebhytquydoi_tt text,
  bhyt_groupcode text,
  huongdanphathuoc text,
  servicepriceid_org integer,
  lankhambenh integer,
  vitrisinhthiet text,
  somanhsinhthiet text,
  stt_theodoithuoc integer,
  chiphidauvao double precision,
  chiphimaymoc double precision,
  chiphipttt double precision,
  mayytedbid integer,
  CONSTRAINT serviceprice_pkey PRIMARY KEY (servicepriceid)
)
WITH (

CREATE TABLE servicepriceref
(
  servicepricerefid serial NOT NULL,
  servicepricegroupcode text,
  servicepricetype integer,
  servicegrouptype integer,
  servicepricecode text,
  bhyt_groupcode text,
  report_groupcode text,
  report_tkcode text,
  servicepricename text,
  servicepricenamenhandan text,
  servicepricenamebhyt text,
  servicepricenamenuocngoai text,
  servicepricebhytquydoi text,
  servicepricebhytquydoi_tt text,
  servicepriceunit text,
  servicepricefee text,
  servicepricefeenhandan text,
  servicepricefeebhyt text,
  servicepricefeenuocngoai text,
  listdepartmentphongthuchien text,
  servicepricefee_old_date timestamp without time zone,
  servicepricefee_old text,
  servicepricefeenhandan_old text,
  servicepricefeebhyt_old text,
  servicepricefeenuocngoai_old text,
  servicepriceprintorder integer DEFAULT 0,
  servicepricerefid_master integer DEFAULT 0,
  servicelock integer DEFAULT 0,
  pttt_hangid integer DEFAULT 0,
  laymauphongthuchien integer DEFAULT 0,
  khongchuyendoituonghaophi integer DEFAULT 0,
  cdha_soluongthuoc double precision DEFAULT 0,
  cdha_soluongvattu double precision DEFAULT 0,
  tylelaichidinh double precision DEFAULT 0,
  tylelaithuchien double precision DEFAULT 0,
  version timestamp without time zone,
  luonchuyendoituonghaophi integer DEFAULT 0,
  tinhtoanlaigiadvktc integer DEFAULT 0,
  servicepricecodeuser text,
  lastuserupdated integer,
  lasttimeupdated timestamp without time zone,
  sync_flag integer,
  update_flag integer,
  servicepricebhytdinhmuc text,
  listdepartmentphongthuchienkhamgoi text,
  ck_groupcode text,
  servicepricecode_ng text,
  pttt_dinhmucvtth double precision DEFAULT 0,
  pttt_dinhmucthuoc double precision DEFAULT 0,
  isremove integer,
  servicepricefee_old_type integer DEFAULT 0,
  servicepricesttuser text,
  pttt_loaiid integer DEFAULT 0,
  CONSTRAINT servicepriceref_pkey PRIMARY KEY (servicepricerefid)
)

CREATE TABLE medicine_ref
(
  medicinerefid serial NOT NULL,
  medicinecode text,
  medicinegroupcode text,
  medicinereftype integer,
  datatype integer,
  medicinetype integer,
  medicinename text,
  tenkhoahoc text,
  donvitinh text,
  donggoi text,
  hangsanxuat text,
  nuocsanxuat text,
  nongdo text,
  lieuluong text,
  hoatchat text,
  dangdung text,
  huongdansudung text,
  soluongngay double precision,
  chuy text,
  medicineprintorder integer,
  gianhap double precision DEFAULT 0,
  giaban double precision DEFAULT 0,
  vatnhap double precision DEFAULT 0,
  vatban double precision DEFAULT 0,
  canhbaosoluong integer DEFAULT 0,
  canhbaohsd integer DEFAULT 0,
  medicinerefid_org integer DEFAULT 0,
  solo text,
  sodangky text,
  hansudung timestamp without time zone,
  version timestamp without time zone NOT NULL DEFAULT now(),
  bietduoc text,
  atc text,
  tylehuhao double precision,
  bhyt_groupcode text,
  report_tkcode text,
  report_groupcode text,
  khongchuyendoituonghaophi integer,
  luonchuyendoituonghaophi integer,
  sync_flag integer,
  update_flag integer,
  stt_thuoc integer,
  servicepricefee double precision DEFAULT 0,
  servicepricefeenhandan double precision DEFAULT 0,
  servicepricefeebhyt double precision DEFAULT 0,
  servicepricefeenuocngoai double precision DEFAULT 0,
  lastuserupdated integer,
  lasttimeupdated timestamp without time zone,
  servicepricefee_loinhuan double precision DEFAULT 0,
  servicepricefeenhandan_loinhuan double precision DEFAULT 0,
  servicepricefeebhyt_loinhuan double precision DEFAULT 0,
  servicepricefeenuocngoai_loinhuan double precision DEFAULT 0,
  servicepricebhytdinhmuc double precision DEFAULT 0,
  listdepartmentphongthuchien text,
  isthuockhangsinh integer,
  mahoatchat text,
  tuongtacthuoc text,
  medicinecodeuser text,
  benhvienapthau text,
  nhacungcapid integer,
  hansudung_year integer DEFAULT 0,
  hansudung_month integer DEFAULT 0,
  iskhongnhapmoi integer,
  nhomquyche text,
  nhombaocao text,
  nhomduocly text,
  nhomtieuduocly text,
  nhomquanly text,
  nhomnghiencuu text,
  nhomabcven text,
  stt_thuoc_chuyeu integer,
  isremove integer,
  servicepricebhytquydoi text,
  servicepricebhytquydoi_tt text,
  goithau text,
  namcungung text,
  thuhoivolo text,
  isthuhoivolo integer,
  ischephamyhoccotruyen integer,
  isvithuocyhoccotruyen integer,
  isthuoctanduoc integer,
  stt_dauthau text,
  nguonchuongtrinhid integer,
  medicinename_byt text,
  ischolanhdaoduyet integer,
  medicinestorebillid integer DEFAULT 0,
  sochungtu text,
  ngaychungtu timestamp without time zone,
  ngaychungtu2 text,
  danhsttdungthuoc integer,
  stt_thuoc_tt40 integer,
  stt_thuoc_tt40text text,
)
CREATE TABLE medicine_store_bill
(
  medicinestorebillid serial NOT NULL,
  medicineperiodid integer DEFAULT 0,
  medicinestoreid integer,
  medicinestorebillid_yc integer,
  medicinestorebillid_im integer,
  medicinestorebillid_ex integer,
  sochungtu text,
  linkcode text,
  isxuattutruc integer,
  partnerid integer,
  partnername text,
  customername text,
  medicinestorebillcode text,
  medicinestorebilldate timestamp without time zone,
  medicinestorebilldoer text,
  medicinestorebilltype integer,
  medicinestorebillstatus integer,
  medicinestorebillfinisheddate timestamp without time zone,
  datatype integer,
  loaithuoc integer,
  isdaduyetphieu integer,
  medicinestorebillapprover text,
  medicinestorebillapprovemessage text,
  medicinestorebillapprovedate timestamp without time zone,
  isdayeucau integer,
  medicinestorebillprocessinger text,
  medicinestorebillprocessingmessage text,
  medicinestorebillprocessingdate timestamp without time zone,
  isremove integer,
  nguoihuyphieu text,
  lydohuyphieu text,
  ngayhuyphieu timestamp without time zone,
  isdathutien integer,
  nguoithutien text,
  lydothutien text,
  ngaythutien timestamp without time zone,
  sothutien double precision,
  medicinestorebillremark text,
  medicinestorebilltotalmoney double precision,
  medicinestorebillphaitramoney double precision,
  medicinestorebilldatramoney double precision,
  medicinestorebillconnomoney double precision,
  medicinestorebilldatemoney timestamp without time zone,
  medicinestorebillchietkhau double precision,
  medicinestorebillthuegtgt double precision,
  ketoanduocid integer,
  listdonthuoc text,
  bill_mode integer,
  lanin integer,
  maubenhphamid integer,
  departmentgroupid integer,
  departmentid integer,
  ngaysudungthuoc timestamp without time zone,
  lanlinhthuoctrongngay integer,
  ylenhlinhthuoc text,
  version timestamp without time zone,
  isphieubuthieu integer,
  lastuserupdated integer,
  lasttimeupdated timestamp without time zone,
  sync_flag integer,
  update_flag integer,
  sothutunumber integer,
  soquaynumber integer,
  iscangoiloa integer,
  ngaychungtu timestamp without time zone,
  keylock integer,
  medicinekiemkeid integer,
  ngayylenh text,
  isbusy integer DEFAULT 0,
  sothuthuphongluu text,
  bacsi text,
  chandoan text,
  medicinekiemkeid_parter integer,
  bacsi_id integer,
  khoa_id integer,
  phong_id integer,
  CONSTRAINT medicine_store_bill_pkey PRIMARY KEY (medicinestorebillid)
)
CREATE TABLE medicine
(
  medicineid serial NOT NULL,
  medicineperiodid integer DEFAULT 0,
  medicinestorerefid integer,
  medicinerefid integer,
  medicinestoreid integer,
  medicinestorebillid integer,
  medicinestorebillcode text,
  medicinestorebilltype integer,
  medicinestorebillstatus integer,
  bill_mode integer,
  ngaysudungthuoc timestamp without time zone,
  partnerid integer,
  medicinestorebillremark text,
  departmentgroupid integer,
  departmentid integer,
  solo text,
  sodangky text,
  hansudung timestamp without time zone,
  xuat_vat double precision,
  xuat_giaban double precision,
  medicinedate timestamp without time zone,
  soluong double precision,
  sodukhadungupdated double precision,
  money double precision,
  vat double precision,
  approve_medicinedate timestamp without time zone,
  approve_soluong double precision,
  approve_money double precision,
  approve_vat double precision,
  accept_medicinedate timestamp without time zone,
  accept_soluong double precision,
  accept_money double precision,
  accept_vat double precision,
  finish_medicinedate timestamp without time zone,
  finish_soluong double precision,
  finish_money double precision,
  finish_vat double precision,
  isremove integer,
  servicepriceid integer,
  listservicepriceid text,
  medicineremark text,
  medicinelinhthuocremark text,
  version timestamp without time zone NOT NULL DEFAULT now(),
  isphieubuthieu integer,
  sync_flag integer,
  update_flag integer,
  medicinekiemkeid integer,
  hansudung_year integer,
  hansudung_month integer,
  goithau text,
  nguonchuongtrinhid integer,
  userid integer,
  medicinekiemkeid_parter integer,
  stt_dauthau text,
  CONSTRAINT medicine_pkey PRIMARY KEY (medicineid)
)
-----------
Hiện giờ có những nhánh sau:

1. Sử dụng x/y dịch vụ: 
vd: A, B, C, D thì tìm BN sử dụng 2/4 dv bất kỳ: BN có dv: A, B hoặc B, C hoặc B, D hoặc A, D 

(a & b) || (a & c) || (a & d) || (b & c) || (b & d) || (c & d)

===>
#2#(a || b || c || d || ...)
#2#((a & b) || (c & d) || (e & f) || (g & h) || ....)	


Là tổ hợp x của y:
vd 2/4:
4!			4*3*2*1		24
-------  = --------- = --- = 6
2!(4-2)!	2*1(2*1)	4

chỉnh hợp: = n(n−1)(n−2)(n−3)...(n−k+1) = 4*3 = 12


2. Sử dụng DV chính A và kèm 1 trong những dịch vụ phụ thuộc (B,C,D...)
===>
(A & B & ...) & (a || b || c || ...)
(A & B & ...) & ((a & b) || (c & d) || ....)

3. Sử dụng n dịch vụ chính: BN sử dụng đồng thời n dịch vụ là dịch vụ PTTT chính (theo nhánh tìm kiếm hiện tại)
====>
A & B & ...

---------
1. Tổ hợp x/(n dịch vụ)

vd: 2/(a,c,c,d)
- Tính tổ hợp 2/4 dịch vụ = (a & b) || (a & c) || (a & d) || (b & c) || (b & d) || (c & d)

chuyen vien :BN000582679

-------=--------------
Khoa danh mục thuốc/vật tư không sử dụng

-----=========

CREATE TABLE tools_benhvien
(
  benhvienid serial NOT NULL,
  benhvienkcbbd text,
  benhviencode text,
  benhvienname text,
  benhvienaddress text,
  benhvienhang text,
  benhvienloai text,
  benhvientuyen text,
  ghichu text,
  matinh text,
  mahuyen text,
  maxa text,
  version timestamp without time zone,
  sync_flag integer,
  update_flag integer,
  CONSTRAINT tools_benhvien_pkey PRIMARY KEY (benhvienid)
)

CREATE INDEX tools_benhvien_benhviencode_idx
  ON tools_benhvien
  USING btree
  (benhviencode);


select benhvienid, benhvienkcbbd, benhviencode, benhvienname from tools_benhvien;

654398718



ALTER TABLE thuchiencls ADD tools_userid integer;
ALTER TABLE thuchiencls ADD tools_username text;

-----
CREATE TABLE tools_serviceprice_ttrieng
(
  serttriengpid serial NOT NULL,
  servicepriceid integer,
  updatetype integer, --1: update di kem - hao phi; 2: update id dv di kem,
  dateupdate timestamp without time zone,
  CONSTRAINT tools_serviceprice_dkhp_pkey PRIMARY KEY (serttriengpid)
)
CREATE INDEX serdkhp_servicepriceid_idx
  ON tools_serviceprice_ttrieng
  USING btree
  (servicepriceid);
CREATE INDEX serdkhp_updatetype_idx
  ON tools_serviceprice_ttrieng
  USING btree
  (updatetype);

CREATE TABLE tools_vienphi_tltt
(
  vienphitlttid serial NOT NULL,
  vienphiid integer,
  thangluong_old double precision,
  dateupdate timestamp without time zone,
  CONSTRAINT tools_vienphi_tltt_pkey PRIMARY KEY (vienphitlttid)
)
CREATE INDEX vienphitltt_vienphiid_idx
  ON tools_vienphi_tltt
  USING btree
  (vienphiid);
 
  
  
servicepriceid in (28252929,
28252930,
28252757,
28252758
)




XUAT_AN
NUOC_SOI

G303TH
G303YC
G350

===========
select ot.tools_othertypelistid, ot.tools_othertypelistcode, ot.tools_othertypelistname, ot.tools_othertypeliststatus, ot.tools_othertypelistnote, o.tools_otherlistid, o.tools_otherlistcode, o.tools_otherlistname, o.tools_otherlistvalue, o.tools_otherliststatus from tools_othertypelist ot inner join tools_otherlist o on o.tools_othertypelistid=ot.tools_othertypelistid;

CREATE TABLE IF NOT EXISTS tools_serviceref
(
  toolsservicerefid serial NOT NULL,
  his_servicepricerefid integer,
  servicegrouptype integer,
  servicepricetype integer,
  bhyt_groupcode text,
  servicepricegroupcode text,
  servicepricecode text,
  servicepricename text,
  servicepricenamenhandan text,
  servicepricenamebhyt text,
  servicepricenamenuocngoai text,
  servicepriceunit text,
  servicepricefee text,
  servicepricefeenhandan text,
  servicepricefeebhyt text,
  servicepricefeenuocngoai text,
  servicelock integer DEFAULT 0,
  servicepricecodeuser text,
  servicepricesttuser text,
  pttt_hangid integer DEFAULT 0,
  pttt_loaiid integer DEFAULT 0,
  tools_otherlistid integer,
  CONSTRAINT tools_serviceref_pkey PRIMARY KEY (toolsservicerefid)
)
  CREATE INDEX tools_serviceref_serrefid_idx
  ON tools_serviceref
  USING btree
  (his_servicepricerefid);  
   CREATE INDEX tools_serviceref_servicepricegroupcode_idx
  ON tools_serviceref
  USING btree
  (servicepricegroupcode); 
  CREATE INDEX tools_serviceref_serrecode_idx
  ON tools_serviceref
  USING btree
  (servicepricecode);  
  CREATE INDEX tools_serviceref_bhytcode_idx
  ON tools_serviceref
  USING btree
  (bhyt_groupcode);  
  CREATE INDEX tools_serviceref_tools_otherlistid_idx
  ON tools_serviceref
  USING btree
  (tools_otherlistid);    


  select se.servicecomment, 
		mbp.usertrakq, 
		mbp.userthuchien, 
		mbp.usertraketqua,
		nv.usercode,
		nv.username 
		
  from service se
	inner join maubenhpham mbp on mbp.maubenhphamid=se.maubenhphamid and maubenhphamgrouptype=0
	left join tools_tblnhanvien nv on nv.userhisid=mbp.userthuchien
  where se.servicecomment<>'' 
  order by mbp.maubenhphamid desc limit 100




