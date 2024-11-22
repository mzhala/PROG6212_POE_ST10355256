CREATE TABLE Programs(
	program_code varchar(5) not null primary key,
	name	varchar(255) not null
);

CREATE TABLE Modules(
	module_code varchar(5) not null primary key,
	name	varchar(255) not null
);

CREATE TABLE Lecture_Details(
	lecture_id varchar(20) not null primary key,
	name	varchar(30) not null,
	surname varchar(30) not null,
	contactNumber varchar(10) not null,
	email NVARCHAR(255) NOT NULL,

    CONSTRAINT CK_email_Format CHECK (email LIKE '%@%.%' AND email NOT LIKE '%@%@%' AND email NOT LIKE '%.@%' AND email NOT LIKE '@%' AND email NOT LIKE '%.'),
	CONSTRAINT CK_contactNumber_Length CHECK (LEN(contactNumber) = 10 AND contactNumber NOT LIKE '%[^0-9]%')

);


CREATE TABLE Program_Module_Lecturer (
    program_code VARCHAR(5) NOT NULL,
    module_code VARCHAR(5) NOT NULL,
    lecture_id VARCHAR(20) NOT NULL,
    rate_per_hour DECIMAL(10, 2) NOT NULL,

    -- Define foreign key relationships
    CONSTRAINT FK_Program FOREIGN KEY (program_code) REFERENCES Programs(program_code),
    CONSTRAINT FK_Module FOREIGN KEY (module_code) REFERENCES Modules(module_code),
    CONSTRAINT FK_Lecturer FOREIGN KEY (lecture_id) REFERENCES Lecture_Details(lecture_id),

    -- Composite primary key to avoid duplicate assignments
    CONSTRAINT PK_Program_Module_Lecturer PRIMARY KEY (program_code, module_code, lecture_id)
);

INSERT INTO Programs (program_code, name)
VALUES
('P001', 'Software Engineering'),
('P002', 'Computer Science'),
('P003', 'Information Systems'),
('P004', 'Cybersecurity'),
('P005', 'Data Science'),
('P006', 'Network Engineering'),
('P007', 'Artificial Intelligence'),
('P008', 'Robotics'),
('P009', 'Cloud Computing'),
('P010', 'Business Analytics'),
('P011', 'Human-Computer Interaction'),
('P012', 'Digital Forensics'),
('P013', 'Game Development'),
('P014', 'Data Engineering'),
('P015', 'Internet of Things'),
('P016', 'Mobile Application Development'),
('P017', 'Machine Learning'),
('P018', 'Embedded Systems'),
('P019', 'Virtual Reality'),
('P020', 'Blockchain Development');

INSERT INTO Modules (module_code, name)
VALUES
('M001', 'Introduction to Programming'),
('M002', 'Data Structures and Algorithms'),
('M003', 'Operating Systems'),
('M004', 'Database Management Systems'),
('M005', 'Computer Networks'),
('M006', 'Software Engineering Principles'),
('M007', 'Artificial Intelligence'),
('M008', 'Web Development'),
('M009', 'Mobile Computing'),
('M010', 'Machine Learning'),
('M011', 'Computer Graphics'),
('M012', 'Cloud Computing'),
('M013', 'Cybersecurity Essentials'),
('M014', 'Data Science Foundations'),
('M015', 'Data Visualization'),
('M016', 'Blockchain Technology'),
('M017', 'Game Design and Development'),
('M018', 'Embedded Systems Design'),
('M019', 'IoT Systems'),
('M020', 'Digital Forensics and Investigation');

INSERT INTO Lecture_Details (lecture_id, name, surname, contactNumber, email)
VALUES
('L0001', 'John', 'Doe', '0721234567', 'johndoe@email.com'),
('L0002', 'Jane', 'Smith', '0822345678', 'janesmith@email.com'),
('L0003', 'Michael', 'Johnson', '0873456789', 'michaelj@email.com'),
('L0004', 'Emily', 'Davis', '0714567890', 'emilydavis@email.com'),
('L0005', 'David', 'Wilson', '0625678901', 'davidw@email.com'),
('L0006', 'Sarah', 'Brown', '0726789012', 'sarahb@email.com'),
('L0007', 'Chris', 'Taylor', '0827890123', 'christaylor@email.com'),
('L0008', 'Jessica', 'Anderson', '0878901234', 'jessicaa@email.com'),
('L0009', 'Daniel', 'Thomas', '0719012345', 'danielt@email.com'),
('L0010', 'Megan', 'Moore', '0620123456', 'meganm@email.com'),
('L0011', 'Anthony', 'Jackson', '0722345678', 'anthonyj@email.com'),
('L0012', 'Laura', 'Martinez', '0823456789', 'lauram@email.com'),
('L0013', 'Kevin', 'Lee', '0874567890', 'kevinl@email.com'),
('L0014', 'Rachel', 'White', '0715678901', 'rachelw@email.com'),
('L0015', 'Oliver', 'Harris', '0626789012', 'oliverh@email.com'),
('L0016', 'Sophia', 'Clark', '0727890123', 'sophiac@email.com'),
('L0017', 'Benjamin', 'Lewis', '0828901234', 'benjaminl@email.com'),
('L0018', 'Isabella', 'Young', '0879012345', 'isabellay@email.com'),
('L0019', 'William', 'King', '0710123456', 'williamk@email.com'),
('L0020', 'Charlotte', 'Scott', '0621234567', 'charlottes@email.com');

INSERT INTO Program_Module_Lecturer (program_code, module_code, lecture_id, rate_per_hour)
VALUES
('P001', 'M001', 'L0001', 350),
('P001', 'M002', 'L0001', 350),
('P001', 'M003', 'L0001', 350),
('P001', 'M004', 'L0001', 350),
('P001', 'M005', 'L0001', 350),
('P002', 'M006', 'L0002', 400),
('P002', 'M007', 'L0002', 400),
('P002', 'M008', 'L0002', 400),
('P002', 'M009', 'L0002', 400),
('P002', 'M010', 'L0002', 400),
('P003', 'M001', 'L0003', 450),
('P003', 'M002', 'L0003', 450),
('P003', 'M003', 'L0003', 450),
('P003', 'M004', 'L0003', 450),
('P003', 'M005', 'L0003', 450),
('P004', 'M006', 'L0004', 500),
('P004', 'M007', 'L0004', 500),
('P004', 'M008', 'L0004', 500),
('P004', 'M009', 'L0004', 500),
('P004', 'M010', 'L0004', 500),
('P005', 'M001', 'L0005', 375),
('P005', 'M002', 'L0005', 375),
('P005', 'M003', 'L0005', 375),
('P005', 'M004', 'L0005', 375),
('P005', 'M005', 'L0005', 375),
('P006', 'M006', 'L0006', 450),
('P006', 'M007', 'L0006', 450),
('P006', 'M008', 'L0006', 450),
('P006', 'M009', 'L0006', 450),
('P006', 'M010', 'L0006', 450),
('P007', 'M001', 'L0007', 375),
('P007', 'M002', 'L0007', 375),
('P007', 'M003', 'L0007', 375),
('P007', 'M004', 'L0007', 375),
('P007', 'M005', 'L0007', 375),
('P008', 'M006', 'L0008', 425),
('P008', 'M007', 'L0008', 425),
('P008', 'M008', 'L0008', 425),
('P008', 'M009', 'L0008', 425),
('P008', 'M010', 'L0008', 425),
('P009', 'M001', 'L0009', 400),
('P009', 'M002', 'L0009', 400),
('P009', 'M003', 'L0009', 400),
('P009', 'M004', 'L0009', 400),
('P009', 'M005', 'L0009', 400),
('P010', 'M006', 'L0010', 500),
('P010', 'M007', 'L0010', 500),
('P010', 'M008', 'L0010', 500),
('P010', 'M009', 'L0010', 500),
('P010', 'M010', 'L0010', 500),
('P001', 'M001', 'L0011', 350),
('P001', 'M002', 'L0011', 350),
('P001', 'M003', 'L0011', 350),
('P001', 'M004', 'L0011', 350),
('P001', 'M005', 'L0011', 350),
('P002', 'M006', 'L0012', 400),
('P002', 'M007', 'L0012', 400),
('P002', 'M008', 'L0012', 400),
('P002', 'M009', 'L0012', 400),
('P002', 'M010', 'L0012', 400),
('P003', 'M001', 'L0013', 450),
('P003', 'M002', 'L0013', 450),
('P003', 'M003', 'L0013', 450),
('P003', 'M004', 'L0013', 450),
('P003', 'M005', 'L0013', 450),
('P004', 'M006', 'L0014', 500),
('P004', 'M007', 'L0014', 500),
('P004', 'M008', 'L0014', 500),
('P004', 'M009', 'L0014', 500),
('P004', 'M010', 'L0014', 500),
('P005', 'M001', 'L0015', 375),
('P005', 'M002', 'L0015', 375),
('P005', 'M003', 'L0015', 375),
('P005', 'M004', 'L0015', 375),
('P005', 'M005', 'L0015', 375),
('P006', 'M006', 'L0016', 450),
('P006', 'M007', 'L0016', 450),
('P006', 'M008', 'L0016', 450),
('P006', 'M009', 'L0016', 450),
('P006', 'M010', 'L0016', 450),
('P007', 'M001', 'L0017', 375),
('P007', 'M002', 'L0017', 375),
('P007', 'M003', 'L0017', 375),
('P007', 'M004', 'L0017', 375),
('P007', 'M005', 'L0017', 375),
('P008', 'M006', 'L0018', 425),
('P008', 'M007', 'L0018', 425),
('P008', 'M008', 'L0018', 425),
('P008', 'M009', 'L0018', 425),
('P008', 'M010', 'L0018', 425),
('P009', 'M001', 'L0019', 400),
('P009', 'M002', 'L0019', 400),
('P009', 'M003', 'L0019', 400),
('P009', 'M004', 'L0019', 400),
('P009', 'M005', 'L0019', 400),
('P010', 'M006', 'L0020', 500),
('P010', 'M007', 'L0020', 500),
('P010', 'M008', 'L0020', 500),
('P010', 'M009', 'L0020', 500),
('P010', 'M010', 'L0020', 500);

SELECT * FROM [dbo].[Lecture_Details]
SELECT * FROM [dbo].[Modules]
SELECT * FROM [dbo].[Program_Module_Lecturer]
SELECT * FROM [dbo].[Program_Module_Lecturer]