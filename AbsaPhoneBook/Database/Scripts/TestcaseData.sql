﻿INSERT INTO dbo.Phonebook 
Values
   ('{AEE42FEF-5ED3-4D14-8449-31217DD0F270}', 'IT Crowd'),
   ('{F61567E0-2548-4E59-A6E5-DC0BD931F445}','Dark place')

GO
INSERT INTO dbo.PhonebookEntry
Values
  (NEWID(), '{AEE42FEF-5ED3-4D14-8449-31217DD0F270}','Morris Moss', '012555777'),
  (NEWID(), '{AEE42FEF-5ED3-4D14-8449-31217DD0F270}','Douglas Reynholm', '012555777'), 
  (NEWID(), '{F61567E0-2548-4E59-A6E5-DC0BD931F445}', 'Richard Ayoade ', '033355523'),
  (NEWID(), '{F61567E0-2548-4E59-A6E5-DC0BD931F445}', 'Garth Merenghi', '033335777')
