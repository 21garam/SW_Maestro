﻿using UnityEngine;
using System.Collections;
using System.IO;

public class FileIO_ {
	public static void WriteStringToFile(string str, string filename){
		string path = pathForDocumentsFile(filename);
		FileStream file = new FileStream (path, FileMode.Create, FileAccess.Write);
		
		StreamWriter sw = new StreamWriter(file);
		sw.WriteLine(str);
		
		sw.Close();
		file.Close();
	}

	public static string ReadStringFromFile(string filename){
		string path = pathForDocumentsFile(filename);

		if(File.Exists(path)){
			FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader( file );
			
			string str = null;
			str = sr.ReadLine();
			
			sr.Close();
			file.Close();
			
			return str;
		}
		else{
			return null;
		}
		//return null;
	}

	private static string pathForDocumentsFile(string filename){ 
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
			path = path.Substring(0, path.LastIndexOf('/'));
			return Path.Combine(Path.Combine(path, "Documents" ), filename);
		}
		else if(Application.platform == RuntimePlatform.Android){
			string path = Application.persistentDataPath; 
			path = path.Substring(0, path.LastIndexOf('/')); 
			return Path.Combine(path, filename);
		}
		else{
			string path = Application.dataPath; 
			path = path.Substring(0, path.LastIndexOf('/'));
			return Path.Combine(path, filename);
		}
	}
}
