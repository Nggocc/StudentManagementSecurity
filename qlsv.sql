use master
go
if DB_ID('qlsv') is not null
	drop database qlsv
go
create database qlsv
go
USE [qlsv]
GO
CREATE TABLE taikhoan (
  tenDangNhap varchar(20) not null primary key,
  matkhau varchar(512) not null,
  hash varchar(512) null
);

CREATE TABLE giangvien (
  maGV int primary key identity,
  tenDangNhap varchar(20) not null,
  hoTen nvarchar(80) NOT NULL,
  sdt varchar(11) NOT NULL,
  diachi nvarchar(50) NOT NULL,
  hinhAnh varchar(50) NOT NULL
);

CREATE TABLE lophoc (
  maLop int primary key ,
  tenLop nvarchar(80) NOT NULL,
  maGV int NOT NULL
);

CREATE TABLE sinhvien (
  maSV int  not null primary key,
  tenDangNhap varchar(20) not null,
  maLop int NOT NULL,
  hoTen nvarchar(80) NOT NULL,
  diachi nvarchar(80) NOT NULL,
  sdt varchar(11) NOT NULL,
  email varchar(30) NOT NULL,
  diemTX1 float NULL,
  diemXT2 float NULL,
  diemThi float NULL,
  hinhAnh varchar(80) NOT NULL
) ;

ALTER TABLE lophoc
  ADD CONSTRAINT lophoc_ibfk_1 FOREIGN KEY (maGV) REFERENCES giangvien (maGV) ;
--
ALTER TABLE sinhvien
  ADD CONSTRAINT sv_tk FOREIGN KEY (tenDangNhap) REFERENCES taikhoan (tenDangNhap) ;
--
ALTER TABLE giangvien
  ADD CONSTRAINT gv_tk FOREIGN KEY (tenDangNhap) REFERENCES taikhoan (tenDangNhap) ;
--
INSERT INTO taikhoan (tenDangNhap, matkhau) VALUES
('2024GV001', 'abc123'),
('2024GV002', 'xyz123'),
('2024GV003', '123abc'),
('2024GV004', 'abcxyz'),
('2024SV001', 'abc123'),
('2024SV002', 'abc123'),
('2024SV003', 'abc123'),
('2024SV004', 'abc123'),
('2024SV005', 'abc123');
INSERT INTO lophoc ( malop, tenLop, maGV) VALUES
( 1, N'Khoa học máy tính', 1),
(2, N'Công nghệ thông tin', 2),
( 3,N'Kỹ thuật phần mềm', 3),
( 4,N'Hệ thống thông tin', 4);


INSERT INTO sinhvien (maSV, tenDangNhap,maLop, hoTen, diachi, sdt, email, diemTX1, diemXT2, diemThi, hinhAnh) VALUES
(1,'2024SV001' , 1, N'Hà Đình Mạnh', N'Nam Định', '01239898677', 'manh123@gmail.com', 9, 10, 8, 'a1.jpg'),
(2,'2024SV002', 1, N'Phó Đình Mạnh', N'Bắc Ninh', '01239898688', 'manh.it@gmail.com', 9, 7, 9, 'a2.jpg'),
(3,'2024SV003' ,3, N'Quách Đức Minh', N'Hà Nội', '09898456778', 'itmanh12@gmail.com', 7, 8, 9, 'a3.jpg'),
(4,'2024SV004',1, N'Nguyễn Thị Na', N'Thanh Hóa', '01239896767', 'nann.it@gmail.com', 9, 7, 10, 'a4.jpg'),
(5,'2024SV005', 3, N'Nguyễn Thị Bích Ngọc', N'Hà Nội', '09768686877', 'ngoc.it.haui@gmail.com', 9, 9, 9, 'a5.jpg');

INSERT INTO giangvien ( tenDangNhap, hoTen, sdt, diachi, hinhAnh) VALUES
 ('2024GV001',N'Hoàng Văn Long', '01239898671', N'Hà Nội', 'a10.jpg'),
( '2024GV002',N'Phan Tuyết Nga', '01239896761', N'Thái Bình', 'a11.jpg'),
('2024GV003',N'Nguyễn Thanh Tú', '01239898672', N'Thái Bình', 'a12.jpg'),
('2024GV004', N'Trần Văn Hùng', '01239896763', N'Thanh Hóa', 'a13.jpg');


  --
select * from lophoc;
select * from giangvien;
select * from sinhvien;
select * from taikhoan;
--
delete from taikhoan where tenDangNhap='2024SV008'
