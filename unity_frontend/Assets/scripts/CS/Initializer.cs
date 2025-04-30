using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Initializer : MonoBehaviour
{
    private TextMeshPro nameText;
    private TextMeshPro levelText;
    private TextMeshPro xpText;
    private GameObject xpBar;

    void Start()
    {
        // Main Menu Background_____________________________________________________________________________________________
        GameObject BG = new GameObject("BG");

            // Sets BG position, assigned it a sprite, and changed the sorting order (Z-Axis) to be the lowest.
            BG.transform.position = new Vector2(0, 0);
            BG.AddComponent<SpriteRenderer>();
            BG.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("art/test_assets/BG");
            BG.GetComponent<SpriteRenderer>().sortingOrder = -1;

        // Icon Background_______________________________________________________________
        GameObject IconBG = new GameObject("Icon BG");

            // Sets IconBG position and assigns it a sprite.
            IconBG.transform.position = new Vector2(0, 3.5f);
            IconBG.AddComponent<SpriteRenderer>();
            IconBG.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("art/test_assets/CharacterBG");
        
            // Icon Background Child (Spritemask)________________________________________
            GameObject IconBGMask = new GameObject("Mask");

                // Makes IconBGMask the child of IconBG, sets its local position, and assigns it a spritemask.
                IconBGMask.transform.parent = IconBG.transform;
                IconBGMask.transform.localPosition = new Vector2(0, 0);
                IconBGMask.AddComponent<SpriteMask>();

                // Creates a temporary variable for the spritemask component to avoid repetetive fetches.
                SpriteMask sprt = IconBGMask.GetComponent<SpriteMask>();

                    // Sets spritemask shape and range (makes sure sprites do not poke out of IconBG)
                    sprt.sprite = Resources.Load<Sprite>("art/test_assets/CharacterBG");
                    sprt.isCustomRangeActive = true;
                    sprt.frontSortingOrder = 3;
                    sprt.backSortingOrder = 2;

            // Monster name Object_______________________________________________________
            GameObject monsterName = new GameObject("Monster Name");

                // Sets monsterName position and assigns it text.
                monsterName.transform.localPosition = new Vector2(0, 2);
                monsterName.AddComponent<TextMeshPro>();

                // Assigns textbox size.
                monsterName.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 0.7f);

                // Assigned to gloval variable to pass in other scripts.
                nameText = monsterName.GetComponent<TextMeshPro>();            

                    // Sets all necessary values for text.
                    nameText.text = "Greg";
                    nameText.color = Color.white;
                    nameText.fontSize = 5;
                    nameText.alignment = TextAlignmentOptions.Center;
                    nameText.verticalAlignment = VerticalAlignmentOptions.Middle;
                    nameText.fontWeight = TMPro.FontWeight.Bold;

            // Monster Sprite____________________________________________________________
            GameObject monsterSprite = new GameObject("Monster");

                // Makes monsterSprite as the child of IconBG, sets its local position, and assigns it a sprite renderer/Animator.
                monsterSprite.transform.parent = IconBG.transform;
                monsterSprite.transform.localPosition = new Vector2(0, 0);
                monsterSprite.AddComponent<SpriteRenderer>();
                monsterSprite.AddComponent<Animator>(); 

                // Set Sorting Order (Z-Axis), its interaction with masks, and assigned an animation controller.
                monsterSprite.GetComponent<SpriteRenderer>().sortingOrder = 3;
                monsterSprite.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                monsterSprite.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("art/Animation Controllers/Monsters/Pup/Pup"); 

            // Monster Level Object______________________________________________________
            GameObject monsterLevel = new GameObject("Monster Level");

                // Set monsterLevel transform and assigned it a sprite renderer.
                monsterLevel.transform.position = new Vector2(0, 0.5f);
                monsterLevel.transform.localScale = new Vector2(0.6f, 1);
                monsterLevel.AddComponent<SpriteRenderer>();

                // Assigned a meter sprite.
                monsterLevel.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("art/test_assets/Meter");

                    // Level Text Label__________________________________________________
                    GameObject levelLabel = new GameObject("Label");

                        // Makes levelLabel the child of monsterLevel, sets its local position, and assigns it text.
                        levelLabel.transform.parent = monsterLevel.transform;
                        levelLabel.transform.localPosition = new Vector2(0, 0.5686f);
                        levelLabel.AddComponent<TextMeshPro>();

                        // Assigns the textbox size.
                        levelLabel.GetComponent<RectTransform>().sizeDelta = new Vector2(3.2f, 0.5f);
                        levelLabel.GetComponent<RectTransform>().localScale = new Vector2(1.6f, 1);

                        // Creates a temporary variable for the text component to avoid repetetive fetches.
                        TextMeshPro tmp = levelLabel.GetComponent<TextMeshPro>();            

                            // Sets all necessary values for text.
                            tmp.text = "Current Level";
                            tmp.color = Color.white;
                            tmp.fontSize = 3.5f;
                            tmp.alignment = TextAlignmentOptions.Center;
                            tmp.verticalAlignment = VerticalAlignmentOptions.Middle;

                    // Level Text________________________________________________________
                    GameObject level = new GameObject("Level");

                        // Makes level the child of monsterLevel, sets its local position, and assigns it text.
                        level.transform.parent = monsterLevel.transform;
                        level.transform.localPosition = new Vector2(0, 0);
                        level.AddComponent<TextMeshPro>();

                        // Assigns the textbox size.
                        level.GetComponent<RectTransform>().sizeDelta = new Vector2(3.3623f, 0.586f);
                        level.GetComponent<RectTransform>().localScale = new Vector2(1.6f, 1);

                        // Creates a temporary variable for the text component to avoid repetetive fetches.
                        levelText = level.GetComponent<TextMeshPro>();            

                            // Sets all necessary values for text.
                            levelText.text = "0";
                            levelText.color = Color.white;
                            levelText.fontSize = 4;
                            levelText.alignment = TextAlignmentOptions.Center;
                            levelText.verticalAlignment = VerticalAlignmentOptions.Middle;
       
            // Monster Experience Points Meter Object_______________________________________________________________________
            GameObject xpMeter = new GameObject("XP Meter");
                xpMeter.AddComponent<SpriteRenderer>();

                xpMeter.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("art/test_assets/Meter");
                xpMeter.transform.position = new Vector2(0, -0.866f);

                xpMeter.GetComponent<Transform>().localScale = new Vector2(1, 1);

                // Experience Points Text_____________________________________________________
                GameObject xp = new GameObject("XP");

                    // Makes xpMeterText the child of xpMeter, sets its local position, and assigns it a spritemask.
                    xp.transform.parent = xpMeter.transform;
                    xp.transform.localPosition = new Vector2(0, 0);
                    xp.AddComponent<TextMeshPro>();

                    xp.GetComponent<RectTransform>().sizeDelta = new Vector2(3.362f, 0.587f);
                    xp.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    xp.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

                    // Creates a temporary variable for the TMP component to avoid repetetive fetches.
                    xpText = xp.GetComponent<TextMeshPro>();            

                        // Sets all necessary values for text.
                        xpText.text = "0 / 10";
                        xpText.color = Color.gray;
                        xpText.fontSize = 3.5f;
                        xpText.alignment = TextAlignmentOptions.Center;
                        xpText.verticalAlignment = VerticalAlignmentOptions.Middle;
                        xpText.sortingOrder = 6;

                // Experience Label______________________________________________________
                GameObject xpLabel = new GameObject("Label");

                    // Makes xpMeterText the child of xpMeter, sets its local position, and assigns it a spritemask.
                    xpLabel.transform.parent = xpMeter.transform;
                    xpLabel.transform.localPosition = new Vector2(0, 0);
                    xpLabel.AddComponent<TextMeshPro>();

                    xpLabel.GetComponent<RectTransform>().sizeDelta = new Vector2(3.362f, 0.5f);
                    xpLabel.GetComponent<RectTransform>().localPosition = new Vector2(0, 0.57f);
                    xpLabel.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

                    // Creates a temporary variable for the TMP component to avoid repetetive fetches.
                    tmp = xpLabel.GetComponent<TextMeshPro>();            

                        // Sets all necessary values for text.
                        tmp.text = "Experience Points";
                        tmp.color = Color.white;
                        tmp.fontSize = 3.5f;
                        tmp.alignment = TextAlignmentOptions.Center;
                        tmp.verticalAlignment = VerticalAlignmentOptions.Middle;

                // Exp Meter (Sprite Mask)_______________________________________________
                GameObject xpMeterMask = new GameObject("Mask");

                    // Makes xpMeterMask the child of xpMeter, sets its local position, and assigns it a spritemask.
                    xpMeterMask.transform.parent = xpMeter.transform;
                    xpMeterMask.transform.localPosition = new Vector2(0, 0);
                    xpMeterMask.AddComponent<SpriteMask>();

                    xpMeterMask.GetComponent<Transform>().localPosition = new Vector2(0, 0);
                    xpMeterMask.GetComponent<Transform>().localScale = new Vector2(1, 1);

                    // Creates a temporary variable for the spritemask component to avoid repetetive fetches.
                    sprt = xpMeterMask.GetComponent<SpriteMask>();

                        // Sets spritemask shape and range (makes sure sprites do not poke out of IconBG)
                        sprt.sprite = Resources.Load<Sprite>("art/test_assets/MeterMask");
                        sprt.isCustomRangeActive = true;
                        sprt.frontSortingOrder = 5;
                        sprt.backSortingOrder = 4;

                // Experience Points Meter Bar___________________________________________
                xpBar = new GameObject("XP Bar");

                    // Makes IconBGMask the child of IconBG, sets its local position, and assigns it a spritemask.
                    xpBar.transform.parent = xpMeter.transform;
                    xpBar.transform.localPosition = new Vector2(-3.364f, 0);
                    xpBar.AddComponent<SpriteRenderer>();

                    // Sets all necessary values for sprite.
                    xpBar.GetComponent<Transform>().localScale = new Vector2(1, 1);
                    xpBar.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("art/test_assets/XP Bar");
                    xpBar.GetComponent<SpriteRenderer>().sortingOrder = 5;
                    xpBar.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        // GUI Canvas_______________________________________________________________________________________________________
        GameObject canvas = new GameObject("Canvas");

            // Adds components for GUI canvas necessities.
            canvas.AddComponent<RectTransform>();
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();

            // Sets canvas to screen size and assigns the appropriate camera.
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            canvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

            // Bookshelf Button_____________________________________________________________________________________________
            GameObject bsBtn = new GameObject("Bookshelf Button");

                // Sets bsBtn as child of canvas and adds components for GUI button necessities.
                bsBtn.transform.parent = canvas.transform;
                bsBtn.AddComponent<RectTransform>();
                bsBtn.AddComponent<CanvasRenderer>();
                bsBtn.AddComponent<Image>();
                bsBtn.AddComponent<Button>();

                // Sets bsBtn sprite and transform.
                bsBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/Button");
                bsBtn.GetComponent<RectTransform>().localPosition = new Vector2(0, -230);
                bsBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 100);
                bsBtn.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

            // Bookshelf Button Text_____________________________________________________
            GameObject bsText = new GameObject("Text");
    
                // Sets bsText as child of bsBtn and adds components for GUI text necessities.
                bsText.transform.parent = bsBtn.transform;
                bsText.AddComponent<RectTransform>();
                bsText.AddComponent<CanvasRenderer>();
                bsText.AddComponent<TextMeshProUGUI>(); 

                // Sets appropriate size and position.
                bsText.GetComponent<RectTransform>().sizeDelta = new Vector2(340, 86.2f);
                bsText.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                bsText.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                
                // Creates a temporary variable for the text component to avoid repetetive fetches.
                TextMeshProUGUI tmpgui = bsText.GetComponent<TextMeshProUGUI>();            

                    // Sets all necessary values for text.
                    tmpgui.text = "Bookshelf";
                    tmpgui.color = Color.white;
                    tmpgui.fontSize = 36;
                    tmpgui.alignment = TextAlignmentOptions.Center;
                    tmpgui.verticalAlignment = VerticalAlignmentOptions.Middle;

            // Adventure Button_____________________________________________________________________________________________
            GameObject advBtn = new GameObject("Adventure Button");

                // Sets advBtn as child of canvas and adds components for GUI button necessities.
                advBtn.transform.parent = canvas.transform;
                advBtn.AddComponent<RectTransform>();
                advBtn.AddComponent<CanvasRenderer>();
                advBtn.AddComponent<Image>();
                advBtn.AddComponent<Button>();

                // THIS DISABLES THE BUTTON COMPONENT TO AVOID MAKING IT FUNCTION. WILL ENABLE WHEN BUILDING LARGER SCALE.
                advBtn.GetComponent<Button>().enabled = false;

                // Sets advBtn sprite and transform.
                advBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/DisabledBtn");

                // Sets appropriate size and position.
                advBtn.GetComponent<RectTransform>().localPosition = new Vector2(0, -345);
                advBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 100);
                advBtn.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

                    // Adventure Button Text_____________________________________________
                    GameObject advText = new GameObject("Text");
            
                        // Sets bsText as child of advBtn and adds components for GUI text necessities.
                        advText.transform.parent = advBtn.transform;
                        advText.AddComponent<RectTransform>();
                        advText.AddComponent<CanvasRenderer>();
                        advText.AddComponent<TextMeshProUGUI>(); 

                        // Sets appropriate size and position.
                        advText.GetComponent<RectTransform>().sizeDelta = new Vector2(340, 86.2f);
                        advText.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                        advText.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                        
                        // Creates a temporary variable for the text component to avoid repetetive fetches.
                        tmpgui = advText.GetComponent<TextMeshProUGUI>();            

                            // Sets all necessary values for text.
                            tmpgui.text = "Adventure";
                            tmpgui.color = Color.black;
                            tmpgui.fontSize = 36;
                            tmpgui.alignment = TextAlignmentOptions.Center;
                            tmpgui.verticalAlignment = VerticalAlignmentOptions.Middle;
    }

    // Getters.
    public TextMeshPro NameText { get { return nameText; } }
    public TextMeshPro LevelText { get { return LevelText; } }
    public TextMeshPro XpText { get { return XpText; } }
    public GameObject XpBar { get { return XpBar; } }
}
