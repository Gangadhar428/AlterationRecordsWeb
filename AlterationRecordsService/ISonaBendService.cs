using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AlterationRecordsService
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISonaBendService" in both code and config file together.
	[ServiceContract]
	public interface ISonaBendService
	{
		[OperationContract]
		void DoWork();

		[OperationContract]
		string GetData(int value);

		[OperationContract]
		void UpdateFileStatus(ArrayList FileID, long UserID, ArrayList SyncFileLocation);

		[OperationContract]
		DataSet StreamUsers(string dateCreated);

		//This is refactored will not be using
		[OperationContract]
		DataSet GetRecruits(string Datemodified, string dsClient);

		//This is refactored will not be using
		//[OperationContract]
		//DataSet GetRecruits(string Datemodified);

		// Returns recruits details based on lastSync date
		[OperationContract]
		DataSet GetRecruitsData(string lastSyncDate, string offsetTicks);



		/// <summary>
		/// Returns recruits details based on lastSync date
		/// </summary>
		/// <param name="lastSyncDate"></param>
		/// <param name="offsetTicks"></param>
		/// <param name="lastMeasurementID">last measurement id</param>
		/// <param name="chunkSize">Max records to return, -1 - all records</param>
		/// <returns></returns>
		[OperationContract]
		DataSet GetRecruitsMeasurementData(string lastSyncDate, string offsetTicks, long lastMeasurementID, int chunkSize);

		[OperationContract]
		DataSet GetRecruitsMeasurementDataDM(string lastSyncDate);

		[OperationContract]
		DataSet GetRecruitsMeasurementDataPFC(string lastSyncDate);

		[OperationContract]
		DataSet GetSFile(string dateUploaded);

		[OperationContract]
		void UploadFile(DataSet ds);

		[OperationContract]
		int CheckAndUpload(DataTable dsRecruit);

		[OperationContract]
		int CheckAndUploadWithDMData(DataTable dsRecruit, DataTable dtDMData, DataTable dtPFCData);

		[OperationContract]
		string InsertMacLog(string MacID, bool syncDownDate, bool syncUpDate);
	}
}
