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

CREATE TABLE Clients(
  id BIGINT IDENTITY(1,1) PRIMARY KEY,
  Nom VARCHAR(50) NOT NULL,
  idRegion BIGINT NOT NULL
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


ALTER TABLE abonnements_clients
ADD CONSTRAINT fk_abonnementsclients_abonnements FOREIGN KEY (idAbonnement)
REFERENCES abonnements (id);

ALTER TABLE abonnements_clients
ADD CONSTRAINT fk_abonnementsclients_clients FOREIGN KEY (idClient)
REFERENCES clients (id);

ALTER TABLE abonnements_remises
ADD CONSTRAINT fk_abonnementsremises_abonnements FOREIGN KEY (idAbonnement)
REFERENCES abonnements (id);

ALTER TABLE clients
ADD CONSTRAINT fk_client_region FOREIGN KEY (idRegion)
REFERENCES regions (id);

ALTER TABLE abonnements_chaines
ADD CONSTRAINT fk_abonnementschaines_chaines FOREIGN KEY (idChaine)
REFERENCES chaines (id);

ALTER TABLE abonnements_chaines
ADD CONSTRAINT fk_abonnementschaines_abonnements FOREIGN KEY (idAbonnement)
REFERENCES abonnements (id);


