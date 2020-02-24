CREATE TABLE [dbo].[PhonebookEntry]
(
    [phonebookentryid] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [phonebookid] UNIQUEIDENTIFIER NOT NULL,
    [name] VARCHAR(255),
    [phonenumber] VARCHAR(50),
    CONSTRAINT [fk_phonebook_phonebookentry_phonebookentryid] FOREIGN KEY ([phonebookid]) REFERENCES [dbo].[Phonebook] ([phonebookid])
);

GO
CREATE NONCLUSTERED INDEX [phonebookentry_phonebookid] 
    ON [dbo].[PhonebookEntry]([phonebookid] ASC)
