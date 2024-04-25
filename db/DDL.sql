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