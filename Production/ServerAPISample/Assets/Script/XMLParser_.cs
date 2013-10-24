using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public static class Util_{
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
}

public class UserRankingInfo{
	public int ranking;
	public string id;
	public int score;
	
	public UserRankingInfo(){
		this.Set(-1, "", 0);
	}
	
	public UserRankingInfo(int ranking, string id, int score){
		this.Set(ranking, id, score);
	}
	
	public void Set(int ranking, string id, int score){
		this.ranking = ranking; this.id = id; this.score = score;
	}
	
	public override string ToString (){
		return string.Format (ranking.ToString() + ". " + id.ToString() + ", " + score.ToString());
	}
}

public class UserAccountInfo{
	public string id;
	public string password;
	public int score;
	public int soft_cash;
	public int hard_cash;
	public int equipment;
	public int item;
	
	public UserAccountInfo(){
		this.Set("", "", 0, 0, 0, 0, 0);
	}
	
	public UserAccountInfo(string id, string password, int score, int soft_cash, int hard_cash, int equipment, int item){
		this.Set(id, password, score, soft_cash, hard_cash, equipment, item);
	}
	
	public void Set(string id, string password, int score, int soft_cash, int hard_cash, int equipment, int item){
		this.id = id; this.password = password; 
		this.score = score; this.soft_cash = soft_cash; 
		this.hard_cash = hard_cash; this.equipment = equipment; 
		this.item = item;	
	}
	
	public override string ToString (){
		return string.Format (id.ToString() + ", " + password.ToString() + ", " + score.ToString() + ", " + 
							  soft_cash.ToString() + ", " + hard_cash.ToString() + ", " + 
							  equipment.ToString() + ", " + item.ToString()); 
	}
}

public class XMLParser_ {
	private static List<UserRankingInfo> otherUserInfoList = new List<UserRankingInfo>();
	private static UserRankingInfo playerRankingInfo = new UserRankingInfo();
	private static UserAccountInfo playerAccountInfo = new UserAccountInfo();
	
	static XMLParser_(){
	}
	
	public static UserAccountInfo PlayerAccountInfo{
		get{return playerAccountInfo;}
	}
	
	public static List<UserRankingInfo> OtherUserRankingInfoList{
		get{return otherUserInfoList;}
	}
	
	public static UserRankingInfo PlayerUserRankingInfo{
		get{return playerRankingInfo;}
	}
	
	public static bool PlayerInfoXMLParse(string xml){
		XmlDocument doc;
		XmlNode root;
		
		doc = new XmlDocument();
		doc.LoadXml(xml); 
		
		root = doc.FirstChild.NextSibling;
		
		if (root.HasChildNodes && root != null) {
			XmlAttribute idAtt = root.ChildNodes[0].Attributes["id"];
			XmlAttribute passwordAtt = root.ChildNodes[0].Attributes["password"];
			XmlAttribute scoreAtt = root.ChildNodes[0].Attributes["score"];
			XmlAttribute soft_cashAtt = root.ChildNodes[0].Attributes["soft_cash"];
			XmlAttribute hard_cashAtt = root.ChildNodes[0].Attributes["hard_cash"];
			XmlAttribute equipmentAtt = root.ChildNodes[0].Attributes["equipment"];
			XmlAttribute itemAtt = root.ChildNodes[0].Attributes["item"];
			playerAccountInfo.Set((string)idAtt.Value, (string)passwordAtt.Value, 
							Util_.ConvertStringtoInt((string)scoreAtt.Value), 
							Util_.ConvertStringtoInt((string)soft_cashAtt.Value), Util_.ConvertStringtoInt((string)hard_cashAtt.Value), 
							Util_.ConvertStringtoInt((string)equipmentAtt.Value), Util_.ConvertStringtoInt((string)itemAtt.Value));
			return true;
		}
		return false;
	}
	
	public static bool RankingInfoXMLParse(string xml) {
		XmlDocument doc;
		XmlNode root;
	
		doc = new XmlDocument();
		doc.LoadXml(xml); 
		
		root = doc.FirstChild.NextSibling;
		
		if (root.HasChildNodes && root != null) {
			int userLength = root.ChildNodes.Count;
			otherUserInfoList.Clear();
			for (int i = 0; i < userLength; i++) {
				XmlAttribute rankingAtt = root.ChildNodes[i].Attributes["ranking"];
				XmlAttribute idAtt = root.ChildNodes[i].Attributes["id"];
				XmlAttribute scoreAtt = root.ChildNodes[i].Attributes["score"];
				if(i != userLength-1){
					UserRankingInfo userInfo = new UserRankingInfo(Util_.ConvertStringtoInt((string)rankingAtt.Value), 
													  (string)idAtt.Value, 
													  Util_.ConvertStringtoInt((string)scoreAtt.Value));
					otherUserInfoList.Add(userInfo);
				}
				else{
					playerRankingInfo.Set(Util_.ConvertStringtoInt((string)rankingAtt.Value),
									   (string)idAtt.Value,
									   Util_.ConvertStringtoInt((string)scoreAtt.Value));
				}
			}
			return true;
		}
		return false;
	}
}
