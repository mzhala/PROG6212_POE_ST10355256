CREATE TRIGGER trg_UpdateLastModDate
ON Claims
AFTER UPDATE
AS
BEGIN
    UPDATE Claims
    SET last_mod_date = GETDATE()
    FROM Inserted
    WHERE Claims.claim_id = Inserted.claim_id;
END;