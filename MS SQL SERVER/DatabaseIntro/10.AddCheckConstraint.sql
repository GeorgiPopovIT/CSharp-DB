ALTER TABLE Users
ADD CONSTRAINT CH_PasswordIsLeast5Symbols CHECK(LEN([Password]) > 5)