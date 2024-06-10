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

INSERT INTO regions (description, t_reception_signal) VALUES ('Analamanga', 40);
INSERT INTO regions (description, t_reception_signal) VALUES ('Atsinanana', 55);
INSERT INTO regions (description, t_reception_signal) VALUES ('AtsimoAndrefana', 50);
INSERT INTO regions (description, t_reception_signal) VALUES ('Diana', 60);

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


-- Bouquet Premium
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


