using UnityEngine;

namespace Watermelon
{
    [RegisterModule("Notify Dialog", Core = true)]
    public class NotifyDialogInitModule : InitModule
    {
        [SerializeField] GameObject canvas;

        public override void CreateComponent(Initialiser Initialiser)
        {
            GameObject canvasGameObject = Instantiate(canvas, Initialiser.InitialiserGameObject.transform);
            canvasGameObject.transform.localScale = Vector3.one;
            canvasGameObject.transform.localPosition = Vector3.zero;
            canvasGameObject.transform.localRotation = Quaternion.identity;
            canvasGameObject.GetComponent<NotifyDialog>().Initialise();
        }

        public NotifyDialogInitModule()
        {
            moduleName = "Notify Dialog";
        }
    }
}