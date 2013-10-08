using UnityEngine;
using System.Collections;

public class Login_ : MonoBehaviour {
	public tk2dUITextInput login_email;
	public tk2dUITextInput login_password;
	
	public tk2dUIItem loginBtr;
	public tk2dUIItem createBtr;
	public tk2dUIItem backBtr;
	public tk2dUIItem confirmBtr;
	public Camera cam;
	
	WWW_ www;
	
	void Start () {
		www = GetComponent<WWW_>();
	} 
	
    void OnEnable() {
        loginBtr.OnClick += Login;
		confirmBtr.OnClick += Confirm;
		createBtr.OnClick += GoToCreateAccount;
		backBtr.OnClick += GoToBack;
	}
	
    private void GoToCreateAccount() {
        GoToLocation(cam, new Vector3(3, 1, -10));
	}
	
	private void GoToBack() {
		GoToLocation(cam, new Vector3(0, 1, -10));
	}
	
	private void Confirm() {
		www.CreateAccount(login_email.Text, login_password.Text);
	}
	
	private void Login() {
		www.Login(login_email.Text, login_password.Text, LoadLevel);
	}
	
	public void LoadLevel(string str){
		if(str == "ok")
			Application.LoadLevel("Menu");
	}
	
	private void GoToLocation(Camera obj, Vector3 loc) {
		Transform t = obj.transform;
    	StartCoroutine(TransformTo(t, 0.3f, loc));
    }
	
	IEnumerator TransformTo( Transform transform, float time, Vector3 toPos) {
        Vector3 fromPos = transform.localPosition;
        for (float t = 0; t < time; t += tk2dUITime.deltaTime) {
            float nt = Mathf.Clamp01( t / time );
            nt = Mathf.Sin(nt * Mathf.PI * 0.5f);

            transform.localPosition = Vector3.Lerp( fromPos, toPos, nt );
            yield return 0;
        }
		transform.localPosition = toPos;
    }
}
