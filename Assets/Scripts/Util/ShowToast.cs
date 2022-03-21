using System.Threading.Tasks;
using UnityEngine;

public class ShowToast
{
    public async static void ShowFadingToast(GameObject ui, float time = 0.5f)
    {
        CanvasGroup cg = null;
        if ((cg = ui.GetComponent<CanvasGroup>()) == null)
        {
            cg = ui.AddComponent<CanvasGroup>();
        }
        cg.alpha = 1;
        while (cg.alpha != 0)
        {
            await Task.Delay((int)(1000 * time));
            cg.alpha -= 0.1f;
        }
    }

}
