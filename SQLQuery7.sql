create database canalsat;

CREATE TABLE abonnements_remises_perso (
  id BIGINT IDENTITY(1,1) PRIMARY KEY,
  remise real DEFAULT 0,
  dateDeb date NOT NULL,
  dateFin date
);

CREATE TABLE abonnements_clients (
  id BIGINT IDENTITY(1,1) PRIMARY KEY,
  idAbonnement BIGINT NOT NULL, 
  idClient BIGINT NOT NULL,
  dateDeb date NOT NULL,
  dateFin date NOT NULL
);

CREATE TABLE regions (
  id BIGINT IDENTITY(1,1) PRIMARY KEY,
  description TEXT NOT NULL,
  t_reception_signal real NOT NULL
);

CREATE TABLE chaines (
  id BIGINT IDENTITY(1,1) PRIMARY KEY,
  description TEXT NOT NULL,
  prix real NOT NULL,
  signal real NOT NULL
);


CREATE TABLE abonnements (
  id BIGINT IDENTITY(1,1) PRIMARY KEY,
  description TEXT NOT NULL,
  idClient BIGINT 
);

CREATE TABLE abonnements_remises (
  id BIGINT IDENTITY(1,1) PRIMARY KEY,
  idAbonnement BIGINT,
  remise real DEFAULT 0,
  dateDeb date NOT NULL,
  dateFin date
);

CREATE TABLE abonnements_chaines (
  id BIGINT IDENTITY(1,1) PRIMARY KEY,
  idAbonnement BIGINT NOT NULL, 
  idChaine BIGINT NOT NULL
);

CREATE TABLE clients(
  id BIGINT IDENTITY(1,1) PRIMARY KEY,
  Nom VARCHAR(50) NOT NULL,
  idRegion BIGINT NOT NULL
);


ALTER TABLE abonnements_chaines
ADD CONSTRAINT fk_abonnementschaines_chaines FOREIGN KEY (idChaine)
REFERENCES chaines (id);

ALTER TABLE abonnements_chaines
ADD CONSTRAINT fk_abonnementschaines_abonnements FOREIGN KEY (idAbonnement)
REFERENCES abonnements (id);

ALTER TABLE abonnements_remises
ADD CONSTRAINT fk_abonnementsremises_abonnements FOREIGN KEY (idAbonnement)
REFERENCES abonnements (id);

ALTER TABLE abonnements_clients
ADD CONSTRAINT fk_abonnementsclients_clients FOREIGN KEY (idClient)
REFERENCES clients (id);

ALTER TABLE abonnements_clients
ADD CONSTRAINT fk_abonnementsclients_abonnements FOREIGN KEY (idAbonnement)
REFERENCES abonnements (id);

ALTER TABLE clients
ADD CONSTRAINT fk_client_region FOREIGN KEY (idRegion)
REFERENCES regions (id);

CREATE FUNCTION dbo.getSituation(@DateDebut datetime, @DateFin datetime)
RETURNS int
AS
BEGIN
    DECLARE @Maintenant datetime = GETDATE()
    DECLARE @Resultat int

    IF @DateFin < @Maintenant
        SET @Resultat = -1
    ELSE IF @DateDebut <= @Maintenant AND @DateFin >= @Maintenant
        SET @Resultat = 0
    ELSE
        SET @Resultat = 1

    RETURN @Resultat
END
-----------VIEW--------------------
CREATE VIEW v_ClientListe AS (
SELECT c.id idClient, c.nom, AB.id idAbonnement,AB.description, ABC.dateDeb, ABc.dateFin, dbo.getSituation(ABC.dateDeb, ABc.dateFin) AS situation
FROM clients c
JOIN abonnements_clients ABc ON ABc.idClient = c.id 
JOIN abonnements AB ON AB.id = ABc.idAbonnement
);

/*CREATE VIEW v_abonnements_chaine_signal AS (
SELECT ab.id idAbonnement,ab.description descriab, c.id idChaine,c.description descrichaine, c.signal, c.prix
FROM abonnements ab 
JOIN abonnements_chaines abc ON ab.id = abc.idAbonnement
JOIN chaines c ON c.id = abc.idChaine
);*/

CREATE VIEW v_abonnements_chaine_signal AS (
SELECT ab.id idAbonnement,ab.description descriab, c.id idChaine,c.description descrichaine, c.signal, c.prix
FROM abonnements ab 
JOIN abonnements_chaines abc ON ab.id = abc.idAbonnement
JOIN chaines c ON c.id = abc.idChaine
);

CREATE VIEW v_abonnements_chaine_signalMin_prix AS (
SELECT idAbonnement, MIN(signal) signalMin, SUM(prix) prixTotal from v_abonnements_chaine_signal 
GROUP BY idAbonnement 
);

CREATE VIEW v_client_region AS (
SELECT c.id idClient, c.nom, c.idRegion, r.description, r.t_reception_signal 
FROM clients c 
JOIN regions r ON c.idRegion = r.id
);

CREATE VIEW v_abonnements_chaine_signalMin AS(
  SELECT idAbonnement, MIN(signal) signalMin from v_abonnements_chaine_signal 
  GROUP BY idAbonnement 
);

SELECT vabcsminprix.*, vabcsminprix.prixTotal * (1 - abr.remise/100) AS prixremise,ab.description, abr.remise, abr.dateDeb debRemise, abr.dateFin finRemise  
from v_abonnements_chaine_signalMin_prix vabcsminprix
JOIN abonnements ab on ab.id = vabcsminprix.idAbonnement
JOIN abonnements_remises abr ON ab.id = abr.idAbonnement
WHERE signalMin >= (SELECT t_reception_signal FROM v_client_region WHERE idClient = 1) 
AND (GETDATE() >= abr.dateDeb AND GETDATE() <= abr.dateFin) 
  ;

  --------------INSERT------------------

INSERT INTO regions (description, t_reception_signal) VALUES ('Analamanga', 40);
INSERT INTO regions (description, t_reception_signal) VALUES ('Atsinanana', 55);
INSERT INTO regions (description, t_reception_signal) VALUES ('AtsimoAndrefana', 50);
INSERT INTO regions (description, t_reception_signal) VALUES ('Diana', 60);

INSERT INTO Clients (Nom, IdRegion) VALUES ('Fyfy', 1);
INSERT INTO Clients (Nom, IdRegion) VALUES ('Noah', 2);
INSERT INTO Clients (Nom, IdRegion) VALUES ('Diary', 3);
INSERT INTO Clients (Nom, IdRegion) VALUES ('Thony', 4);

INSERT INTO abonnements (description) VALUES ('Premium');
INSERT INTO abonnements (description) VALUES ('Sport Max');
INSERT INTO abonnements (description) VALUES ('Cinéma');
INSERT INTO abonnements (description) VALUES ('Découverte');
INSERT INTO abonnements (description) VALUES ('Jeunesse');
INSERT INTO abonnements (description) VALUES ('Divertissement');


INSERT INTO abonnements_remises (idAbonnement,remise, dateDeb, dateFin) VALUES (1, 10, '2023-01-01', '2023-12-31');
INSERT INTO abonnements_remises (idAbonnement,remise, dateDeb, dateFin) VALUES (2, 10, '2023-01-01', '2023-12-31');
INSERT INTO abonnements_remises (idAbonnement,remise, dateDeb, dateFin) VALUES (3, 10, '2023-01-01', '2023-12-31');
INSERT INTO abonnements_remises (idAbonnement,remise, dateDeb, dateFin) VALUES (4, 10, '2023-01-01', '2023-12-31');
INSERT INTO abonnements_remises (idAbonnement,remise, dateDeb, dateFin) VALUES (5, 10, '2023-01-01', '2023-12-31');
INSERT INTO abonnements_remises (idAbonnement,remise, dateDeb, dateFin) VALUES (6, 10, '2023-01-01', '2023-12-31');

INSERT INTO abonnements_clients (idAbonnement, idClient, dateDeb, dateFin) VALUES (4, 1, '2023-03-05', '2023-04-05');
INSERT INTO abonnements_clients (idAbonnement, idClient, dateDeb, dateFin) VALUES (3, 2, '2023-03-15', '2023-04-15');
INSERT INTO abonnements_clients (idAbonnement, idClient, dateDeb, dateFin) VALUES (6, 3, '2023-03-12', '2023-04-12');
INSERT INTO abonnements_clients (idAbonnement, idClient, dateDeb, dateFin) VALUES (3, 1, '2023-04-06', '2023-05-06');
INSERT INTO abonnements_clients (idAbonnement, idClient, dateDeb, dateFin) VALUES (2, 2, '2023-02-14', '2023-03-14');
INSERT INTO abonnements_clients (idAbonnement, idClient, dateDeb, dateFin) VALUES (4, 4, '2023-02-14', '2023-03-14');

INSERT INTO chaines (description, prix, signal)
VALUES 
('Canal+', 150, 70),
('OCS', 120, 60),
('beIN Sports', 156, 80),
('Eurosport', 600, 55),
('RMC Sport', 100, 50),
('Ciné+', 105, 70),
('National Geographic', 50, 90),
('Discovery', 500, 55),
('Planète+', 10, 70),
('Disney Channel', 367, 74),
('Cartoon Network', 40, 80),
('Nickelodeon', 46, 30),
('Comédie+', 400, 25),
('Syfy', 70, 95);

INSERT INTO abonnements_chaines (idAbonnement, idChaine)
VALUES
(1, 1),
(1, 2),
(1, 3);

-- Bouquet Cinéma
INSERT INTO abonnements_chaines (idAbonnement, idChaine)
VALUES
(2, 1),
(2, 2),
(2, 6);

-- Bouquet Sport Max
INSERT INTO abonnements_chaines (idAbonnement, idChaine)
VALUES
(3, 3),
(3, 4),
(3, 5);

-- Bouquet Divertissement
INSERT INTO abonnements_chaines (idAbonnement, idChaine)
VALUES
(4, 1),
(4, 13),
(4, 14);

-- Bouquet Découverte
INSERT INTO abonnements_chaines (idAbonnement, idChaine)
VALUES
(5, 7),
(5, 8),
(5, 9);

-- Bouquet Jeunesse
INSERT INTO abonnements_chaines (idAbonnement, idChaine)
VALUES
(6, 10),
(6, 11),
(6, 12);

-----------------REQUETE-------------
select * from chaines;
select * from abonnements_remises;
select * from Clients;
select * from abonnements;
select * from regions;
select * from abonnements_chaines;
select * from v_abonnements_chaine_signal;
select * from v_abonnements_chaine_signalMin;
select * from v_abonnements_chaine_signalMin_prix;
select * from v_client_region;
select * from v_ClientListe;

---------------------TEST---------------------------
SELECT * FROM abonnements_remises where (dateDeb <= GETDATE() AND dateFin >= GETDATE()) AND idAbonnement = 1  AND id = (SELECT MAX(id) 
FROM abonnements_remises where (dateDeb <= GETDATE() AND dateFin >= GETDATE()) AND idAbonnement =1);

select * from v_abonnements_chaine_signal where idAbonnement = 1;

SELECT idClient, nom, idAbonnement, description, dateDeb, dateFin, situation 
FROM v_clientliste where idClient = 1 and dateFin = (select MAX(dateFin) from v_ClientListe where idClient = 1);

SELECT vabcsmin.*, ab.description  from v_abonnements_chaine_signalMin vabcsmin JOIN abonnements ab on ab.id = vabcsmin.idAbonnement 
WHERE signalMin >= (SELECT t_reception_signal FROM v_client_region WHERE idClient = 1);

SELECT idClient, nom, idAbonnement, description, dateDeb, dateFin, situation FROM v_clientliste where idClient = 1 ORDER BY dateFin DESC;

