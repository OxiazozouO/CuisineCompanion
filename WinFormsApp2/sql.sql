create schema `medical_management_system`;
use medical_management_system;

CREATE TABLE Patients
(
    PatientID     INT AUTO_INCREMENT PRIMARY KEY COMMENT '患者ID，主键',
    PatientName   VARCHAR(100) NOT NULL COMMENT '患者姓名',
    Gender        VARCHAR(10) COMMENT '性别',
    DateOfBirth   DATE COMMENT '出生日期',
    ContactNumber CHAR(11)     NOT NULL COMMENT '联系电话',
    Email         VARCHAR(100) COMMENT '电子邮件',
    RegDate       DATE         NOT NULL DEFAULT (NOW()) COMMENT '注册日期'
) COMMENT '患者信息';

CREATE TABLE Doctors
(
    DoctorID      INT AUTO_INCREMENT PRIMARY KEY COMMENT '医生ID，主键',
    DoctorName    VARCHAR(50)  NOT NULL COMMENT '医生姓名',
    Specialty     VARCHAR(100) NOT NULL COMMENT '专科',
    ContactNumber VARCHAR(20) COMMENT '联系电话',
    Email         VARCHAR(100) COMMENT '电子邮件',
    HireDate      DATE         NOT NULL DEFAULT (NOW()) COMMENT '入职日期',
    Degree        VARCHAR(50) COMMENT '学位'
) COMMENT '医生信息';

CREATE TABLE Appointments
(
    AppointmentID   INT AUTO_INCREMENT PRIMARY KEY COMMENT '预约ID，主键',
    PatientID       INT COMMENT '患者ID，外键',
    DoctorID        INT COMMENT '医生ID，外键',
    AppointmentDate DATE NOT NULL COMMENT '预约日期',
    AppointmentTime TIME NOT NULL COMMENT '预约时间',
    Status          INT  NOT NULL DEFAULT 0 COMMENT '预约状态',

# 已预约（1）：患者已经预约但尚未到达预约时间。
# 已取消（2）：患者或医生取消了预约。
# 已完成（3）：预约已经完成，患者已经接受了相应的服务。
    Notes           VARCHAR(100) COMMENT '备注',
    FOREIGN KEY (PatientID) REFERENCES Patients (PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors (DoctorID)
) COMMENT '预约信息';

CREATE TABLE MedicalRecords
(
    RecordID   INT AUTO_INCREMENT PRIMARY KEY COMMENT '病历ID，主键',
    PatientID  INT COMMENT '患者ID，外键',
    DoctorID   INT COMMENT '医生ID，外键',
    Diagnosis  TEXT NOT NULL COMMENT '诊断',
    Treatment  TEXT NOT NULL COMMENT '治疗方案',
    RecordDate DATE NOT NULL COMMENT '记录日期',
    FollowUp   VARCHAR(100) COMMENT '随访情况',
    FOREIGN KEY (PatientID) REFERENCES Patients (PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors (DoctorID)
) COMMENT '病历信息';

-- 插入患者数据
INSERT INTO Patients (PatientName, Gender, DateOfBirth, ContactNumber, Email, RegDate)
VALUES ('张三', '男', '1980-01-01', '13000057513', 'zhangsan@example.com', '2024-05-01'),
       ('李四', '女', '1990-02-02', '13000057514', 'lisi@example.com', '2024-05-02'),
       ('王五', '男', '1975-03-15', '13000057515', 'wangwu@example.com', '2024-05-03'),
       ('赵六', '女', '1985-04-20', '13000057516', 'zhaoliu@example.com', '2024-05-04'),
       ('孙七', '男', '1995-05-25', '13000057517', 'sunqi@example.com', '2024-05-05'),
       ('周八', '女', '1970-06-30', '13000057518', 'zhouba@example.com', '2024-05-06');

-- 插入医生数据
INSERT INTO Doctors (DoctorName, Specialty, ContactNumber, Email, HireDate, Degree)
VALUES ('张医生', '心脏病学', '19000057589', 'zhang@example.com', '2015-05-15', '医学博士'),
       ('李医生', '神经学', '19000057590', 'li@example.com', '2018-03-20', '博士'),
       ('王医生', '内分泌学', '19000057591', 'wang@example.com', '2020-07-10', '硕士'),
       ('赵医生', '皮肤科', '19000057592', 'zhao@example.com', '2017-11-25', '博士'),
       ('孙医生', '肾脏病学', '19000057593', 'sun@example.com', '2016-09-05', '博士'),
       ('周医生', '呼吸科', '19000057594', 'zhou@example.com', '2019-02-14', '硕士');

-- 插入预约数据
INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, AppointmentTime, Status, Notes)
VALUES (1, 1, '2024-06-01', '09:00:00', 1, '已发送提醒'),
       (2, 2, '2024-06-02', '10:00:00', 3, NULL),
       (3, 3, '2024-06-03', '11:00:00', 2, '需要携带最近的血液检查报告'),
       (4, 4, '2024-06-04', '14:00:00', 1, '已发送提醒'),
       (5, 5, '2024-06-05', '15:00:00', 1, '请准时到达并携带病历'),
       (6, 6, '2024-06-06', '16:00:00', 2, '请携带最近的X光片');

-- 插入病历数据
INSERT INTO MedicalRecords (PatientID, DoctorID, Diagnosis, Treatment, RecordDate, FollowUp)
VALUES (1, 1, '高血压', '药物治疗，每天服用洛尔血压药，并监测血压变化', '2024-06-01', '需要监测血压，每月复查一次'),
       (2, 2, '偏头痛', '止痛药和休息，建议减少工作压力，保持良好作息', '2024-06-02', '两周后复诊，观察是否有缓解'),
       (3, 3, '糖尿病', '胰岛素注射，严格控制饮食并监测血糖水平', '2024-06-03', '一个月后复查血糖，调整治疗方案'),
       (4, 4, '湿疹', '外用药膏，保持皮肤清洁和干燥，避免过敏原', '2024-06-04', '一周后复诊，看是否需要更换药物'),
       (5, 5, '慢性肾炎', '口服药物和饮食调理，限制蛋白质摄入', '2024-06-05', '每两周复查一次，监测肾功能变化'),
       (6, 6, '哮喘', '吸入剂治疗，避免过敏源，定期做肺功能测试', '2024-06-06', '每月复诊，评估病情控制情况');