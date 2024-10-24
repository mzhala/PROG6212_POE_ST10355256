CREATE TABLE Claims(
	claim_id int identity(1,1) primary key,
	lecturer_id varchar(20) not null,
	lecturer_name varchar(30) not null,
	lecturer_surname varchar(30) not null,
	manager_id varchar(5),
	program_code varchar(5) not null,
	module_code varchar(5) not null,
	hours int not null,
	status VARCHAR(50) DEFAULT 'Pending',
	claim_date DATETIME DEFAULT GETDATE(),
	rate_per_hour DECIMAL(10, 2) not null, 
	last_mod_date DATETIME DEFAULT GETDATE(),
	notes varchar(255) not null,
	total_amount AS (rate_per_hour * hours),
	month varchar(9) not null,
	year int not null,
	support_document varchar(255)
);