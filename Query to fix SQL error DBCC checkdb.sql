USE "ITAssetsDatabase.Models.ITAssetsDatabaseDBContext"
EXEC sp_resetstatus 'ITAssetsDatabase.Models.ITAssetsDatabaseDBContext'
ALTER DATABASE "ITAssetsDatabase.Models.ITAssetsDatabaseDBContext" SET EMERGENCY
DBCC checkdb ("ITAssetsDatabase.Models.ITAssetsDatabaseDBContext")
ALTER DATABASE "ITAssetsDatabase.Models.ITAssetsDatabaseDBContext" SET SINGLE_USER WITH ROLLBACK IMMEDIATE
DBCC CheckDB ("ITAssetsDatabase.Models.ITAssetsDatabaseDBContext",REPAIR_ALLOW_DATA_LOSS)
ALTER DATABASE "ITAssetsDatabase.Models.ITAssetsDatabaseDBContext" SET MULTI_USER