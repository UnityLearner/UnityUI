using UnityEngine;
using System.Collections;
public enum MybuttonVariety
{
    UserChoseFollowGameobjects,
    AutoInitialization
}
public class Mybutton : MonoBehaviour { 
    public GameObject Target;
    public string TargetMethod;
    public MybuttonVariety variety;
    public GameObject ForeGameobject;
    public GameObject BackGameobject;  
    public bool DestroyItself = false;
    public float DelayDestroyTime;   
    void initialazition(MybuttonVariety var)
    {
        switch (var)
        {
            case MybuttonVariety.AutoInitialization:
                if (this.transform.childCount == 1)
                {
                    ForeGameobject = this.gameObject.transform.GetChild(0).gameObject;
                    BackGameobject = null;
                }
                if (this.transform.childCount > 1)
                {
                    ForeGameobject = this.gameObject.transform.GetChild(0).gameObject;
                    BackGameobject = this.gameObject.transform.GetChild(1).gameObject;
                }
                ForeGameobject.SetActive(true);
                if (!ForeGameobject.GetComponent<Collider>())
                {
                    Debug.LogError("Check the ForeGameobject`s collider11111");
                    return;
                }
                if (BackGameobject != null)
                    BackGameobject.SetActive(false);
                break;
            case MybuttonVariety.UserChoseFollowGameobjects:
                if (!ForeGameobject.GetComponent<Collider>())
                {
                    Debug.LogError("Check the ForeGameobject`s collider222222222");
                    return;
                }
                if (ForeGameobject == null)
                {
                    Debug.LogError("Check fore");
                    return;
                }
                break;
        }
    }
    public void bttonClick(GameObject go)
    {
        ForeGameobject.SetActive(false);
        if (BackGameobject!=null)
            BackGameobject.SetActive(true);
        if (Target!=null&&TargetMethod!=null)
        Target.SendMessage(TargetMethod);
        if(DestroyItself)
        {
            if (DelayDestroyTime > 0)
                StartCoroutine(destroy());
            else
                Destroy(this.gameObject);
        }
    }
    IEnumerator destroy(){
        yield return new WaitForSeconds(DelayDestroyTime);
        Destroy(this.gameObject);
    }
	void Start () {
        initialazition(variety);
        UIEventListener.Get(ForeGameobject).onClick = bttonClick;
	}
}
