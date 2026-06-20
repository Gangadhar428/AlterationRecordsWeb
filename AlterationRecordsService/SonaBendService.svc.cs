using AlterationRecordsDataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AlterationRecordsService
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SonaBendService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select SonaBendService.svc or SonaBendService.svc.cs at the Solution Explorer and start debugging.
	[ServiceBehavior(MaxItemsInObjectGraph = 2147483646)]
	public class SonaBendService : ISonaBendService
	{
		public void DoWork()
		{
		}

		string DBConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
		public string GetData(int value)
		{
			return string.Format("You entered: {0}", value);
		}

		public void UpdateFileStatus(ArrayList FileID, long UserID, ArrayList SyncFileLocation)
		{
			try
			{
				for (int i = 0; i < FileID.Count; i++)
				{
					//TODO FileStatus
					int status = SBDataAccess.UpdateFileStatus(DBConnectionString, Convert.ToInt32(FileID[i] != null ? FileID[i] : 0), 2, UserID, SyncFileLocation[i].ToString());
				}
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void UploadFile(DataSet ds)
		{
			try
			{
				if (ds.Tables.Count <= 0)
					return;
				DataSet dsRecruit = SBDataAccess.GetRecruitID(DBConnectionString);
				int iRows = SBDataAccess.UploadDataTODB(DBConnectionString, ds, dsRecruit);
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void ReadWriteStream(FileStream readStream, Stream writeStream)
		{
			int Length = Convert.ToInt32(readStream.Length);
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}

		public DataSet StreamUsers(string DateCreated)
		{
			try
			{
				return SBDataAccess.GetUsersList(DBConnectionString, DateCreated);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// //This is refactored will not be using
		/// </summary>
		/// <param name="Datemodified"></param>
		/// <returns></returns>
		public DataSet GetRecruits(string Datemodified)
		{
			DataSet dsRecruits = new DataSet();
			DataSet dsMeasurements = new DataSet();
			DataTable dtRecruits = new DataTable();
			DataTable dtMeasurements = new DataTable();
			DataSet Result = new DataSet();
			ArrayList aList = new ArrayList();
			Syncronized scyn = new Syncronized();
			try
			{
				aList = SBDataAccess.GetRecruitRecord(DBConnectionString, Datemodified);

				dsRecruits = (DataSet)aList[0];
				dsMeasurements = (DataSet)aList[1];

				dtRecruits = dsRecruits.Tables[0];
				dtMeasurements = dsMeasurements.Tables[0];

				dsRecruits.Tables.Clear();
				dsMeasurements.Tables.Clear();
				Result.Tables.Add(dtRecruits);
				Result.Tables.Add(dtMeasurements);

			}
			catch (Exception)
			{
				throw;
			}
			return Result;
		}

		//This is refactored will not be using
		/// <summary>
		/// Returns both recruit and measurement data based on last sync date
		/// </summary>
		/// <param name="Datemodified"></param>
		/// <param name="dsClient"></param>
		/// <returns></returns>
		public DataSet GetRecruits(string Datemodified, string dsClient)
		{
			DataSet dsRecruits;
			DataSet dsMeasurements;
			DataTable dtRecruits;
			DataTable dtMeasurements;
			DataSet Result = new DataSet();
			ArrayList aList = new ArrayList();

			string sourceTicksString = dsClient;
			long sourceTicks = long.Parse(sourceTicksString);
			// Obtain the UTC offset for the remote computer.            
			long UtcTickslocal = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Ticks;
			// Obtain the time difference between the sender computer and the remote computer.
			long ticksDifference = sourceTicks - UtcTickslocal;
			int milliSeconds = new TimeSpan(ticksDifference).Milliseconds;


			try
			{
				aList = SBDataAccess.GetRecruitRecord(DBConnectionString, Datemodified, milliSeconds);

				dsRecruits = (DataSet)aList[0];
				dsMeasurements = (DataSet)aList[1];

				dtRecruits = dsRecruits.Tables[0];
				dtMeasurements = dsMeasurements.Tables[0];

				dsRecruits.Tables.Clear();
				dsMeasurements.Tables.Clear();
				Result.Tables.Add(dtRecruits);
				Result.Tables.Add(dtMeasurements);

			}
			catch (Exception)
			{
				throw;
			}
			return Result;
		}

		/// <summary>
		/// Returns recruit data based on last sync date
		/// </summary>
		/// <param name="lastSyncDate"></param>
		/// <param name="offsetTicks">Not implemented</param>
		/// <returns></returns>
		public DataSet GetRecruitsData(string lastSyncDate, string offsetTicks)
		{
			return SBDataAccess.GetRecruitsData(DBConnectionString, lastSyncDate, 0);
		}

		/// <summary>
		/// Returns recruit measurement data based on last sync date, lastMeasurementID and chunkSize
		/// </summary>
		/// <param name="lastSyncDate"></param>
		/// <param name="offsetTicks">Not implemented</param>
		/// <param name="lastMeasurementID"></param>
		/// <param name="chunkSize">Max records to return, -1 - all records</param>
		/// <returns></returns>
		public DataSet GetRecruitsMeasurementData(string lastSyncDate, string offsetTicks, long lastMeasurementID, int chunkSize)
		{
			try
			{
				AlterationRecords.FileLogger.WriteEntry("test");
				return SBDataAccess.GetRecruitsMeasurementData(DBConnectionString, lastSyncDate, 0, lastMeasurementID, chunkSize);
			}
			catch (Exception ex)
			{
				AlterationRecords.FileLogger.WriteEntry(ex.Message + ex.StackTrace);
				throw ex;
			}
		}

		public DataSet GetRecruitsMeasurementDataDM(string lastSyncDate)
		{
			try
			{
				AlterationRecords.FileLogger.WriteEntry("GetRecruitsMeasurementDataDM");
				return SBDataAccess.GetRecruitsMeasurementDataDM(DBConnectionString, lastSyncDate);
			}
			catch (Exception ex)
			{
				AlterationRecords.FileLogger.WriteEntry(ex.Message + ex.StackTrace);
				throw ex;
			}
		}

		public DataSet GetRecruitsMeasurementDataPFC(string lastSyncDate)
		{
			try
			{
				AlterationRecords.FileLogger.WriteEntry("GetRecruitsMeasurementDataPFC");
				return SBDataAccess.GetRecruitsMeasurementDataPFC(DBConnectionString, lastSyncDate);
			}
			catch (Exception ex)
			{
				AlterationRecords.FileLogger.WriteEntry(ex.Message + ex.StackTrace);
				throw ex;
			}
		}

		public DataSet GetSFile(string dateUploaded)
		{
			try
			{
				return SBDataAccess.GetSFile(DBConnectionString, dateUploaded);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public int CheckAndUpload(DataTable dsRecruit)
		{
			try
			{
				if (dsRecruit != null && dsRecruit.Rows.Count > 0)
				{
					return SBDataAccess.CheckAndUpload(DBConnectionString, dsRecruit);
				}
				return 0;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public int CheckAndUploadWithDMData(DataTable dsRecruit, DataTable dtDMData, DataTable dtPFCData)
		{
			try
			{
				if (dsRecruit != null && dsRecruit.Rows.Count > 0)
				{
					int count = SBDataAccess.CheckAndUploadWithDMData(DBConnectionString, dsRecruit, dtDMData, dtPFCData);
					return count;
				}
				return 0;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public string InsertMacLog(string MacID, bool syncDownDate, bool syncUpDate)
		{
			try
			{
				string syncdate = DateTime.Now.ToString();
				if (syncDownDate)
					SBDataAccess.insertMacLog(DBConnectionString, MacID, syncdate, "");
				else if (syncUpDate)
					SBDataAccess.insertMacLog(DBConnectionString, MacID, "", syncdate);
				return syncdate;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
