-- create data base if not exist
SET @database_name = "EitanMedicalDb";
SET @CreateDb =  CONCAT('CREATE DATABASE IF NOT EXISTS `', @database_name, '`');

PREPARE stmp from @createDb;
EXECUTE stmp;
DEALLOCATE PREPARE stmp;


USE EitanMedicalDb;

SET @CreateTablePatients = 'CREATE TABLE IF NOT EXISTS Patients (
	id int auto_increment primary key,
	name varchar(255) not null,
	age int not null,
	gender varchar(255) not null,,
	request int not null,
	constraint users_pk unique (name)
)

';

SET @CreateTableHeartRateReadings = 'CREATE TABLE IF NOT EXISTS HeartRateReadings (
	id int auto_increment primary key,
	patient_id int not null,
	timestamp datetime not null,
	heart_rate int not null,
	constraint HeartRateReadings_user_id_fk foreign key (patient_id) references Patients (id)
)

';
PREPARE stmt FROM @CreateTablePatients;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

PREPARE stmp from @CreateTableHeartRateReadings;
EXECUTE stmp;
DEALLOCATE PREPARE stmp;

