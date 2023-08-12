using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Compass : MonoBehaviour
{
	public RawImage CompassImage;
	public Transform Player;
	public Text CompassDirectionText;
	Quaternion targetRotation;

	public float smoothSpeed = 30f;

	public enum CompassDirection
	{
		N = 0,
		NE = 45,
		E = 90,
		SE = 135,
		S = 180,
		SW = 225,
		W = 270,
		NW = 315
	}

	public Dictionary<CompassDirection, string> CompassDirectionTexts = new Dictionary<CompassDirection, string>()
	{
		{CompassDirection.N, "N"},
		{CompassDirection.NE, "NE"},
		{CompassDirection.E, "E"},
		{CompassDirection.SE, "SE"},
		{CompassDirection.S, "S"},
		{CompassDirection.SW, "SW"},
		{CompassDirection.W, "W"},
		{CompassDirection.NW, "NW"}
	};

	public void Update()
	{
        float headingAngle;

        headingAngle = Quaternion.LookRotation(Player.transform.forward).eulerAngles.y;

        targetRotation = Quaternion.Euler(0, 0, -headingAngle);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);

        CompassImage.rectTransform.rotation = Quaternion.Euler(0, 0, -Player.localEulerAngles.y);

        Vector3 forward = Player.transform.forward;

        forward.y = 0;

        headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;
        headingAngle = 5 * (Mathf.RoundToInt(headingAngle / 5.0f));

        int displayangle;
        displayangle = Mathf.RoundToInt(headingAngle);

        CompassDirection compassDirection = (CompassDirection)displayangle;

        if (CompassDirectionTexts.ContainsKey(compassDirection))
        {
            CompassDirectionText.text = CompassDirectionTexts[compassDirection];
        }
        else
        {
            CompassDirectionText.text = headingAngle.ToString();
        }
    }
}
