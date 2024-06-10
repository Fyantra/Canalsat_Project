DROP VIEW IF EXISTS v_abonnements_dispo;
CREATE VIEW v_abonnements_dispo AS (
SELECT c.id idClient ,c.nom, r.id idRegion,r.description,r.t_reception_signal,vabcs.descriab, vabcsmin.idAbonnement,vabcsmin.signalMin 
FROM clients c
WHERE vabcsmin.signalMin >= r.t_reception_signal 
);

SELECT vabcsminprix.*, vabcsminprix.prixTotal * (1 - abr.remise/100) AS prixremise,ab.description, abr.remise, abr.dateDeb debRemise, abr.dateFin finRemise  
from v_abonnements_chaine_signalMin_prix vabcsminprix
JOIN abonnements ab on ab.id = vabcsminprix.idAbonnement
JOIN abonnements_remises abr ON ab.id = abr.idAbonnement
WHERE signalMin >= (SELECT t_reception_signal FROM v_client_region WHERE idClient = 1) 
AND (GETDATE() >= abr.dateDeb AND GETDATE() <= abr.dateFin) 
  ;

CREATE VIEW v_client_region AS (
SELECT c.id idClient, c.nom, c.idRegion, r.description, r.t_reception_signal 
FROM clients c 
JOIN regions r ON c.idRegion = r.id
);

DROP VIEW IF EXISTS v_abonnements_chaine_signalMin_prix;
CREATE VIEW v_abonnements_chaine_signalMin_prix AS (
SELECT idAbonnement, MIN(signal) signalMin, SUM(prix) prixTotal from v_abonnements_chaine_signal 
GROUP BY idAbonnement 
);

CREATE VIEW v_abonnements_chaine_signalMin AS(
  SELECT idAbonnement, MIN(signal) signalMin from v_abonnements_chaine_signal 
  GROUP BY idAbonnement 
);

DROP VIEW IF EXISTS v_abonnements_chaine_signal;
CREATE VIEW v_abonnements_chaine_signal AS (
SELECT ab.id idAbonnement,ab.description descriab, c.id idChaine,c.description descrichaine, c.signal, c.prix
FROM abonnements ab 
JOIN abonnements_chaines abc ON ab.id = abc.idAbonnement
JOIN chaines c ON c.id = abc.idChaine
);

SELECT * from v_ClientListe;

DROP VIEW IF EXISTS v_ClientListe;

CREATE VIEW v_ClientListe AS (
SELECT c.id idClient, c.nom, AB.id idAbonnement,AB.description, ABC.dateDeb, ABc.dateFin, dbo.getSituation(ABC.dateDeb, ABc.dateFin) AS situation
FROM clients c
JOIN abonnements_clients ABc ON ABc.idClient = c.id 
JOIN abonnements AB ON AB.id = ABc.idAbonnement
);

