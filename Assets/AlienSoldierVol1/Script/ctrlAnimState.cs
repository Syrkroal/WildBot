using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ctrlAnimState : MonoBehaviour {

    public void StateAct()
    {
        mgrAnim.instance.disBtnAct();
    }

    public void RetrunSitIdleAct()
    {
        mgrAnim.instance.SitDeathAct();
    }

    public void RetrunIdleAct()
    {
        mgrAnim.instance.JumpAct();
    }
}
