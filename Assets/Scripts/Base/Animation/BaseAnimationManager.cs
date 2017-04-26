using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public enum _AnimationState
{
    idle = 1, // trạng thái đứng yên -> giống tên idle trong list animation cuarua no
    run = 2,
    walk =3,
    attack = 4
}


public class BaseAnimationManager : MonoBehaviour {
    public Animation animationOfThis; // component animation
    public _AnimationState currState; // trạng thái hiện tại 

    public string currStrState = null; //tên của trạng thái hiện tại 

    // time
    private const float minLimiteTime = 5f;
    private const float maxLimiteTime = 7f;

    private float timeOfAttack = 0;
    void Awake()
    {
        InitGame();
        animationOfThis = gameObject.GetComponent<Animation>();
        //thoi gian attack cua object
        //timeOfAttack = animationOfThis[_AnimationState.attack.ToString()].clip.length;
    }

    void OnEnable()
    {
        if(!GameConfig.m_isStart)
        {
            return;
        }
        ResetAnimation();
        // set audio when tracked
        string name = gameObject.name;
        AudioManager.Instance.PlayAudioEnglishFromResoures(name, false);
        onEnable();
    }
    public virtual void onEnable()
    {

    }

    // ban dau animation 
    public void ResetAnimation()
    {
        SetAnimationByType(_AnimationState.idle);
        //StartCoroutine(RandomState());
    }

    public IEnumerator RandomState()
    {
        while(true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minLimiteTime, maxLimiteTime));

            /*do
            {
                currState = GetRandomEnum<_AnimationState>();
            }
            while (currState == _AnimationState.attack);*/

            if (currStrState != null)
            {
                StopCoroutine(currStrState);
            }

            //switch (currState)
            //{
            //    case _AnimationState.idle:
            //        currStrState = "UpdateIdle";
            //        break;
            //    case _AnimationState.walk:
            //        currStrState = "UpdateWalk";
            //        break;
            //    case _AnimationState.attack:
            //        currStrState = "UpdateAttack";
            //        break;
            //    case _AnimationState.run:
            //        currStrState = "UpdateRun";
            //        break;
            //}
            //StartCoroutine(currStrState);
            SetAnimationByType(currState);
        }
    }

    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

	// hàm này là hám chuyển trạng thái của con đó nha 
	// tham số là kiển animation của con đó : 
    public void SetAnimationByType(_AnimationState _type)
    {
		this.currState= _type;
       	animationOfThis.CrossFade(_type.ToString()); // hàm này nó sẽ chuyển trạng thái 
    }


	public string getNextState(){
		switch (currStrState) {
		case "idle":
			return "walk";
		case "walk":
			return "run";
		case "run":
			return "attack";
		case "attack":
			return "idle";
		break;	
		}
		return "walk";
	}

	// hàm này là hám chuyển trạng thái của con đó nha, giống hàm trên nhưng khác tham số thôi

	public void SetAnimation(string state)
	{
		//this.animationOfThis.
		this.currStrState = state;
		Debug.Log("State ne: " + state);
		animationOfThis.CrossFade(state);
	}
	public string GetName()
	{
		return this.gameObject.name;
	}

    public void Attack()
    {
        StopAllCoroutines();
        SetAnimationByType(_AnimationState.attack);
        Invoke("ResetAnimation", timeOfAttack - 0.1f);
    }
    
	// dưới này đừng quan tâm 
    #region COROUTINES
    private IEnumerator UpdateRun()
    {
        while(true)
        {
            if(currState != _AnimationState.run) // ???
            {
                break;
            }
            DoRun();
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator UpdateWalk()
    {
        while (true)
        {
            if (currState != _AnimationState.walk)
            {
                break;
            }
            DoWalk();
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator UpdateAttack()
    {
        while (true)
        {
            if (currState != _AnimationState.walk)
            {
                break;
            }

            yield return new WaitForFixedUpdate();
        }
    }


    private IEnumerator UpdateIdle()
    {
        while (true)
        {
            if (currState != _AnimationState.idle)
            {
                break;
            }
            DoIdle();
            yield return new WaitForFixedUpdate();
        }
    }

    #endregion

    #region OVERRIDE
    // do some thing object when state move
    public virtual void DoRun()
    {

    }


    //do something with object when state idle
    public virtual void DoIdle()
    {

    }

    public virtual void DoWalk()
    {

    }

    public virtual void DoAttack()
    {

    }
    #endregion


    // Update is called once per frame
	void Update () {
        UpdateGame();
        //Debug.Log(animationOfThis["idle"].clip.length);
	}

    //override
    public virtual void InitGame()
    {
        return;
    }
    public virtual void UpdateGame()
    {
        return;
    }


}
