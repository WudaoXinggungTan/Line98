using UnityEngine;

namespace CompanyCoreScripts.MVC.UICamera
{
    public class UICameraView : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
    }
}