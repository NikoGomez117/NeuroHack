#pragma strict
var crosshairTexture : Texture2D;
var position : Rect;
static var OriginalOn = true;
 
function Start()
{
    position = Rect((Screen.width - crosshairTexture.width/2) / 2, (Screen.height - crosshairTexture.height/2) /2, crosshairTexture.width/2, crosshairTexture.height/2);
}
 
function OnGUI()
{
    if(OriginalOn == true)
    {
        GUI.DrawTexture(position, crosshairTexture);
    }
}