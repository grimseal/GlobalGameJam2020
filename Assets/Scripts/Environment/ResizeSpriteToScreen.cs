using UnityEngine;

public class ResizeSpriteToScreen : MonoBehaviour
 {

     public Camera Camera;
     
     void Start() {
         var sr = gameObject.GetComponent<SpriteRenderer>();
         if (sr == null) return;
         transform.localScale = new Vector3(1,1,1);
         var width = sr.sprite.bounds.size.x;
         var height = sr.sprite.bounds.size.y;
         var worldScreenHeight = Camera.orthographicSize * 2.0;
         var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
         transform.localScale = new Vector3((float) (worldScreenWidth / width), (float) (worldScreenHeight / height));
     }
 }
 