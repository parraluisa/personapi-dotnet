-- Crear tabla persona
CREATE TABLE persona (
  cc int PRIMARY KEY NOT NULL,
  nombre varchar(45) NOT NULL,
  apellido varchar(45) NOT NULL,
  genero CHAR(1) NOT NULL CHECK (genero IN ('F', 'M')), -- Restricción de verificación para género
  edad int
);
 
-- Crear tabla telefono
CREATE TABLE telefono (
  num varchar(15) PRIMARY KEY NOT NULL,
  oper varchar(45) NOT NULL,
  duenio int NOT NULL,
  FOREIGN KEY (duenio) REFERENCES persona(cc)
);
 
-- Crear tabla profesion
CREATE TABLE profesion (
  id int PRIMARY KEY,
  nom varchar(90) NOT NULL,
  des text
);
 
-- Crear tabla estudios
CREATE TABLE estudios (
  id_prof int NOT NULL,
  cc_per int NOT NULL,
  fecha date,
  univer varchar(50),
  PRIMARY KEY (id_prof, cc_per),
  FOREIGN KEY (cc_per) REFERENCES persona(cc),
  FOREIGN KEY (id_prof) REFERENCES profesion(id)
);


INSERT INTO persona (cc, nombre, apellido, genero, edad) VALUES
(123456789, 'María', 'González', 'F', 25),
(987654321, 'Juan', 'Martínez', 'M', 30),
(111222333, 'Ana', 'López', 'F', 40);


INSERT INTO telefono (num, oper, duenio) VALUES
('1234567890', 'Movistar', 123456789),
('9876543210', 'Claro', 987654321),
('1112223330', 'Tigo', 111222333);


INSERT INTO profesion (id, nom, des) VALUES
(1, 'Ingeniero', 'Ingeniero en Sistemas'),
(2, 'Doctor', 'Médico Cirujano'),
(3, 'Abogado', 'Especialista en Derecho Civil');


INSERT INTO estudios (id_prof, cc_per, fecha, univer) VALUES
(1, 123456789, '2015-05-10', 'Universidad Nacional'),
(2, 987654321, '2010-09-15', 'Universidad de la Ciudad'),
(3, 111222333, '2008-03-20', 'Universidad Central');
