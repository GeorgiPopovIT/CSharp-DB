--DECLARE @setOfLeters VARCHAR(10) = 'oistmiahf'
--DECLARE @word VARCHAR(10) = 'Sofia'
--DECLARE @i INT = 1;

CREATE  FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(MAX), @word VARCHAR(MAX))
RETURNS BIT
AS
BEGIN
DECLARE @i INT = 1;
WHILE (@i <= LEN(@word))
BEGIN
	DECLARE	@letter CHAR(1) = SUBSTRING(@word,@i,1)
	IF (CHARINDEX(@letter,@setOfLetters) = 0)
			RETURN 0

	SET @i+=1;
END
RETURN 1
END

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia')
SELECT dbo.ufn_IsWordComprised('oistmiahf', 'halves')
SELECT dbo.ufn_IsWordComprised('bobr', 'Rob')
SELECT dbo.ufn_IsWordComprised('pppp', 'Guy')