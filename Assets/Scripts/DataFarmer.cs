using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataFarmer {

    private static DataFarmer me = null;
    private List<IDataFarmerObject> data = new List<IDataFarmerObject>();
    private static int BUFFER_FULL = 100;

	// Use this for initialization
	public static DataFarmer GetInstance () {
		if (me == null)
        {
            me = new DataFarmer();
        }
        return me;
	}

	private DataFarmer()
    {

    }

	public void Save (IDataFarmerObject thingToSave) {
        data.Add(thingToSave);
        if (data.Count == BUFFER_FULL)
        {
            Debug.Log("Data Moved to File!");
            using (StreamWriter file =
                   File.AppendText(@"C:\Users\CSLUser\Desktop\df.csv"))
            {
                foreach (IDataFarmerObject o in data)
                {
                    file.Write(o.Serialize());
                }
            }
            data.Clear();
        }
	}
}
