using UnityEngine;

public class ElementsManager : MonoBehaviour
{
    [SerializeField] private Sprite fire0, fire1, fire2, fire3, fire4;
    [SerializeField] private Sprite ice0, ice1, ice2, ice3, ice4;
    [SerializeField] private Sprite death0, death1, death2, death3, death4;
    [SerializeField] private Sprite life0, life1, life2, life3, life4;

    public Sprite GetSlotSprite(Element element, int power)
    {
        switch (element)
        {
            case Element.Fire:
                switch (power)
                {
                    case 0:
                        return fire0;
                    case 1:
                        return fire1;
                    case 2:
                        return fire2;
                    case 3:
                        return fire3;
                    case 4:
                        return fire4;
                }

                break;
            case Element.Ice:
                switch (power)
                {
                    case 0:
                        return ice0;
                    case 1:
                        return ice1;
                    case 2:
                        return ice2;
                    case 3:
                        return ice3;
                    case 4:
                        return ice4;
                }

                break;
            case Element.Death:
                switch (power)
                {
                    case 0:
                        return death0;
                    case 1:
                        return death1;
                    case 2:
                        return death2;
                    case 3:
                        return death3;
                    case 4:
                        return death4;
                }

                break;
            case Element.Life:
                switch (power)
                {
                    case 0:
                        return life0;
                    case 1:
                        return life1;
                    case 2:
                        return life2;
                    case 3:
                        return life3;
                    case 4:
                        return life4;
                }

                break;
        }

        return null;
    }
}