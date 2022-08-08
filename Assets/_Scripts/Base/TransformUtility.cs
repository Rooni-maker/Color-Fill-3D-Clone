//Shady
using UnityEngine;

public static class TransformUtility
{
    /// <summary>
    /// Set Active state of the GameObject
    /// </summary>
    public static void SetActive(this Transform Target, bool Toggle) => Target.gameObject.SetActive(Toggle);

    /// Global Position Methods
    /// <summary>
    /// Reset Global position to (0,0,0)
    /// </summary>
    public static void ResetGlobalPos(this Transform Target)            => Target.position      = Vector3.zero;

    /// <summary>
    /// Set Global Position on X axis and the rest stay the same.
    /// </summary>
    public static void SetGlobalPosX(this Transform Target, float PosX) => Target.position = new Vector3(PosX, Target.position.y, Target.position.z);
    
    /// <summary>
    /// Set Global Position on Y axis and the rest stay the same.
    /// </summary>
    public static void SetGlobalPosY(this Transform Target, float PosY) => Target.position = new Vector3(Target.position.x, PosY, Target.position.z);

    /// <summary>
    /// Set Global Position on Z axis and the rest stay the same.
    /// </summary>
    public static void SetGlobalPosZ(this Transform Target, float PosZ) => Target.position = new Vector3(Target.position.x, Target.position.y, PosZ);

    /// Local Position Methods
    /// <summary>
    /// Reset Local position to (0,0,0)
    /// </summary>
    public static void ResetLocalPos(this Transform Target)             => Target.localPosition = Vector3.zero;

    /// <summary>
    /// Set Local Position on X axis and the rest stay the same.
    /// </summary>
    public static void SetLocalPosX(this Transform Target, float PosX)  => Target.localPosition = new Vector3(PosX, Target.localPosition.y, Target.localPosition.z);
    
    /// <summary>
    /// Set Local Position on Y axis and the rest stay the same.
    /// </summary>
    public static void SetLocalPosY(this Transform Target, float PosY)  => Target.localPosition = new Vector3(Target.localPosition.x, PosY, Target.localPosition.z);

    /// <summary>
    /// Set Local Position on Z axis and the rest stay the same.
    /// </summary>
    public static void SetLocalPosZ(this Transform Target, float PosZ)  => Target.localPosition = new Vector3(Target.localPosition.x, Target.localPosition.y, PosZ);

    /// <summary>
    /// Set Local Position on X axis and set the rest to zero.
    /// </summary>
    public static void SetOnlyLocalPosX(this Transform Target, float PosX)  => Target.localPosition = Vector3.right * PosX;

    /// <summary>
    /// Set Local Position on Y axis and set the rest to zero.
    /// </summary>
    public static void SetOnlyLocalPosY(this Transform Target, float PosY)  => Target.localPosition = Vector3.up * PosY;

    /// <summary>
    /// Set Local Position on Z axis and set the rest to zero.
    /// </summary>
    public static void SetOnlyLocalPosZ(this Transform Target, float PosZ)  => Target.localPosition = Vector3.forward * PosZ;

    /// Global Rotation Methods
    /// <summary>
    /// Set Global Euler Angles on X axis and set the rest stay same.
    /// </summary>
    public static void SetGlobalEulerX(this Transform Target, float RotX)  => Target.eulerAngles = new Vector3(RotX, Target.eulerAngles.y, Target.eulerAngles.z);
    
    /// <summary>
    /// Set Global Euler Angles on Y axis and set the rest stay same.
    /// </summary>
    public static void SetGlobalEulerY(this Transform Target, float RotY)  => Target.eulerAngles = new Vector3(Target.eulerAngles.x, RotY, Target.eulerAngles.z);

    /// <summary>
    /// Set Global Euler Angles on Z axis and set the rest stay same.
    /// </summary>
    public static void SetGlobalEulerZ(this Transform Target, float RotZ)  => Target.eulerAngles = new Vector3(Target.eulerAngles.x, Target.eulerAngles.y, RotZ);

    /// <summary>
    /// Set Global Euler Angles on X axis and set the rest to zero.
    /// </summary>
    public static void SetOnlyGlobalEulerX(this Transform Target, float RotX)  => Target.eulerAngles = new Vector3(RotX, 0f, 0f);
    
    /// <summary>
    /// Set Global Euler Angles on Y axis and set the rest to zero.
    /// </summary>
    public static void SetOnlyGlobalEulerY(this Transform Target, float RotY)  => Target.eulerAngles = new Vector3(0f, RotY, 0f);

    /// <summary>
    /// Set Global Euler Angles on Z axis and set the rest to zero.
    /// </summary>
    public static void SetOnlyGlobalEulerZ(this Transform Target, float RotZ)  => Target.eulerAngles = new Vector3(0f, 0f, RotZ);

    /// Reset Local Rotation to (0,0,0)
    /// </summary>
    public static void ResetLocalRot(this Transform Target)             => Target.localEulerAngles = Vector3.zero;

    /// Local Rotation Methods
    /// <summary>
    /// Set Local Euler Angles on X axis and set the rest stay same.
    /// </summary>
    public static void SetLocalEulerX(this Transform Target, float RotX)  => Target.localEulerAngles = new Vector3(RotX, Target.localEulerAngles.y, Target.localEulerAngles.z);
    
    /// <summary>
    /// Set Local Euler Angles on Y axis and set the rest stay same.
    /// </summary>
    public static void SetLocalEulerY(this Transform Target, float RotY)  => Target.localEulerAngles = new Vector3(Target.localEulerAngles.x, RotY, Target.localEulerAngles.z);

    /// <summary>
    /// Set Local Euler Angles on Z axis and set the rest stay same.
    /// </summary>
    public static void SetLocalEulerZ(this Transform Target, float RotZ)  => Target.localEulerAngles = new Vector3(Target.localEulerAngles.x, Target.localEulerAngles.y, RotZ);

        /// <summary>
    /// Set Local Euler Angles on X axis and set the rest to zero.
    /// </summary>
    public static void SetOnlyLocalEulerX(this Transform Target, float RotX)  => Target.localEulerAngles = new Vector3(RotX, 0f, 0f);
    
    /// <summary>
    /// Set Local Euler Angles on Y axis and set the rest to zero.
    /// </summary>
    public static void SetOnlyLocalEulerY(this Transform Target, float RotY)  => Target.localEulerAngles = new Vector3(0f, RotY, 0f);

    /// <summary>
    /// Set Local Euler Angles on Z axis and set the rest to zero.
    /// </summary>
    public static void SetOnlyLocalEulerZ(this Transform Target, float RotZ)  => Target.localEulerAngles = new Vector3(0f, 0f, RotZ);

    /// Scale Methods
    /// <summary>
    /// Reset Local Scale of Transform to (1,1,1)
    /// </summary>
    public static void ResetScale(this Transform Target) => Target.localScale = Vector3.one;

    /// Scale Methods
    /// <summary>
    /// Set Local Scale of Transform
    /// </summary>
    public static void SetScale(this Transform Target, float X = 1f, float Y = 1f, float Z = 1f) => Target.localScale = new Vector3(X, Y, Z); 

    /// <summary>
    /// Set Local Scale on X Axis and rest stay the same
    /// </summary>
    public static void SetLocalScaleX(this Transform Target, float ScaleX) => Target.localScale = new Vector3(ScaleX, Target.localScale.y, Target.localScale.z);

    /// <summary>
    /// Set Local Scale on Y Axis and rest stay the same
    /// </summary>
    public static void SetLocalScaleY(this Transform Target, float ScaleY) => Target.localScale = new Vector3(Target.localScale.x, ScaleY, Target.localScale.z);

    /// <summary>
    /// Set Local Scale on Z Axis and rest stay the same
    /// </summary>
    public static void SetLocalScaleZ(this Transform Target, float ScaleZ) => Target.localScale = new Vector3(Target.localScale.x, Target.localScale.y, ScaleZ);

}//class end