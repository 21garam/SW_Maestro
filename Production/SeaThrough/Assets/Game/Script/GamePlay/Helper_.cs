using UnityEngine;
using System.Collections;

public static class Helper_{
	public static int ConvertStringtoInt(string s) {
		int j;
		bool result = System.Int32.TryParse(s, out j);
		if (true == result) {
			return j;
		} else {
			Debug.Log("Error...");
		    return 0;
		}
	}
	
	public static double ConvertStringtoFloat(string s) {
		double j;
		bool result = System.Double.TryParse(s, out j);//System.Int32.TryParse(s, out j);
		if (true == result) {
			return j;
		} else {
			Debug.Log("Error...");
		    return 0;
		}
	}
}