using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AlterationRecordsService
{
	[DataContract]
	public class Syncronized
	{
		[DataMember]
		public DataSet dataset { get; set; }

		[DataMember]
		public byte[] bytes { get; set; }

		[DataMember]
		public ArrayList aList { get; set; }

		//[DataMember]
		//public List<XmlElementTreeNode> Children { get; set; }
	}
}