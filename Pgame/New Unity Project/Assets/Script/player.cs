using UnityEngine;

public class player : MonoBehaviour
{ 
#region 欄位
[Header("移動速度"), Range(0,1000)]
public float speed = 5;
[Header("跳越高度"), Range(0,1000)]
public int jump = 350 ;
[Header("血量"), Range(0,2000)]
public float hp = 500;

public bool isGround;
    [Header("血量"), Range(0, 2000)]
    public int coin;

 [Header("音效區域")]
    public AudioClip 受傷音效;
    public AudioClip 滑行音效;
    public AudioClip 跳躍音效;
    public AudioClip 金幣音效;
 [Header("動畫區域")]
    public Animator 動畫控制器;
[Header("物理性質")]
public Rigidbody2D 剛體;
    public CapsuleCollider2D 碰撞器;
    #endregion

#region 方法
    /// <summary>
    /// 移動
    /// </summary>
    private void MOVE()
    {

    }
    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {

    }
    /// <summary>
    /// 滑行
    /// </summary>
    private void Slide()
    {

    }
    /// <summary>
    /// 吃金幣
    /// </summary>
    private void EatCoin()
    {

    }
    /// <summary>
    /// 受傷
    /// </summary>
    private void Hit()
    {

    }
    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {

    }
    /// <summary>
    /// 過關
    /// </summary>
    private void Pass()
    {

    }


#endregion

#region 事件






#endregion




}
