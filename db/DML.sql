use persona_db;
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
