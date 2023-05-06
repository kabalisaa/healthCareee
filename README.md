# healthCareee
health care

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [id]
      ,[name]
      ,[email]
      ,[phone]
      ,[address]
      ,[createdAt]
  FROM [kabalisaHealthCareDB].[dbo].[Patients]
