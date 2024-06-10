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