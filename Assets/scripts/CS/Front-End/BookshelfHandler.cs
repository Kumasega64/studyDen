using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookshelfHandler : MonoBehaviour
{
    private Initializer initializer = null;
    public Book rootBook = null;

    void Start()
    {
        rootBook = new Book("Test");
        initializer = GameObject.Find("Initializer").GetComponent<Initializer>();
    }

        // Linked list for books.
    public void LinkBooks(Book currentBookNode, Book newBookNode)
    {
        // Root Node.
        if(currentBookNode == null) // FIX WHEN THERE IS NO ROOT NODE.
        {
            currentBookNode.bookID = 1;

            // Makes all Books children of the GUI canvas and sets initial position.
            currentBookNode.bookObject.transform.parent = initializer.BSBG.transform;
            currentBookNode.bookObject.transform.localPosition = new Vector2(0, 245);
        }

        else if(currentBookNode.nextBook == null)
        {
            // Makes the new book nodes GameObject the child of the current book nodes Gameobject.
            newBookNode.bookObject.transform.parent = currentBookNode.bookObject.transform;

            // If the current node is the root node, then we set the next node position to be much lower.
            if(currentBookNode.previousBook == null)
            {
                newBookNode.bookObject.transform.localPosition = new Vector2(0, currentBookNode.bookObject.transform.localPosition.y - 490);
            }

            // If this is just the next node, the we just the position to be the previous nodes position.
            else
            {
                newBookNode.bookObject.transform.localPosition = new Vector2(0, currentBookNode.bookObject.transform.localPosition.y);
            }


            // Links the books.
            currentBookNode.nextBook = newBookNode;
            currentBookNode.nextBook.previousBook = currentBookNode;

            // Sets ID for new book node. 
            currentBookNode.nextBook.bookID = currentBookNode.bookID + 1;
        }

        // Progresses further into the list until empty node found.
        else
        {
            LinkBooks(currentBookNode.nextBook, newBookNode);
        }

        currentBookNode.BulkPopupDisable();
    }

    // Locates a book baseed on its name.
    private Book Locatebook(Book currentBookNode, string targetname)
    {
        // No more books are present.
        if(currentBookNode == null)
        {
            return null;
        }

        // Book Found.
        else if (currentBookNode.BookName.Equals(targetname))
        {
            return currentBookNode;
        }

        // Book not found, will continue to traverse.
        else
        {
            return Locatebook(currentBookNode.nextBook, targetname);
        }
    }

        // Locates a book baseed on its ID.
    private Book Locatebook(Book currentBookNode, int targetID)
    {
        // No more books are present.
        if(currentBookNode == null)
        {
            return null;
        }

        // Book Found.
        else if (currentBookNode.bookID == targetID)
        {
            return currentBookNode;
        }

        // Book not found, will continue to traverse.
        else
        {
            return Locatebook(currentBookNode.nextBook, targetID);
        }
    }

    // Locates and destroys target book while re-linking book list to keep integrity.
    public void UnlinkBook(string bookName)
    {
        Book bookToDestroy = Locatebook(rootBook, bookName);

        // If the target book has a next book, we will relink the next book the the target books previous book.
        if(bookToDestroy.nextBook != null)
        {
            // Makes the target book nodes previous book nodes gameobject the parent of the target books next book nodes gameobject.
            bookToDestroy.nextBook.bookObject.transform.parent = bookToDestroy.previousBook.bookObject.transform;
            
            // Unlinks the target book node form list and relinks previous and next book nodes.
            bookToDestroy.previousBook.nextBook = bookToDestroy.nextBook;
        }

        // Else if this is the last or only book in the list.
        else
        {
            // Points the previous book nodes next book to null and sets target book node to null.
            bookToDestroy.previousBook.nextBook = null;
            bookToDestroy = null;
        }

        // Destroys the target book nodes GameObject to avoid memory leak.
        Object.Destroy(bookToDestroy.bookObject);
    }
}

// Book Tree.
public class Book
{
    private string bookName = null;
    public int bookID = 0;
    private Color bookColor;
    public GameObject bookObject = null;
    public Book previousBook = null;
    public Book nextBook = null;
    public GameObject popup = null;

    public Book(string bookName)
    {
        this.bookName = bookName;
        
        bookColor = PickColor();

        bookObject = new GameObject(bookName);
        bookObject.transform.parent = GameObject.Find("Initializer").GetComponent<Initializer>().BSBG.transform;
        bookObject.transform.localScale = new Vector2(1, 1);
        bookObject.transform.localPosition = new Vector2(0, 245);


        bookObject.AddComponent<Image>();
        bookObject.AddComponent<Button>();

        bookObject.GetComponent<Button>().onClick.AddListener(delegate{ TogglePopup();});

        bookObject.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 110);
        bookObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/Bookpng");
        bookObject.GetComponent<Image>().color = bookColor;

        GameObject shelf = new GameObject("Shelf");
        
        shelf.AddComponent<Image>();
        
        shelf.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/Bookshelf");

        shelf.transform.parent = bookObject.transform;
        shelf.transform.localPosition = new Vector2(0, -110);
        shelf.transform.localScale = new Vector2(1, 1);
        shelf.GetComponent<RectTransform>().sizeDelta = new Vector2(370, 110);

        GameObject bookText = new GameObject("Book Text");
        
        bookText.AddComponent<TextMeshProUGUI>();
        bookText.transform.parent = bookObject.transform;

        bookText.transform.localPosition = new Vector2(0, 0);
        bookText.transform.localScale = new Vector2(1, 1);
        bookText.GetComponent<RectTransform>().sizeDelta = new Vector2(235.86f, 103.71f);

        // Creates a temporary variable for the text component to avoid repetetive fetches.
        TextMeshProUGUI tmp = bookText.GetComponent<TextMeshProUGUI>();            

            // Sets all necessary values for text.
            tmp.text = bookName;
            tmp.color = Color.white;
            tmp.fontSize = 30;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.verticalAlignment = VerticalAlignmentOptions.Middle;
            tmp.fontWeight = FontWeight.Bold;
            tmp.outlineWidth = 0.2f;

                // Shadow for the Bookshelf UI.
        popup = new GameObject("Popup Window");

            // Sets bsBtn as child of canvas and adds components for GUI button necessities.
            popup.transform.parent = bookObject.transform;
            popup.AddComponent<CanvasRenderer>();
            popup.AddComponent<Image>();
            popup.gameObject.tag = "popup";

            // Sets bsBtn sprite and transform.
            popup.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/Popup spritesheet");
            popup.GetComponent<RectTransform>().localPosition = new Vector2(0, -96.25f);
            popup.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 75);
            popup.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            popup.SetActive(false);

            // Adventure Button_____________________________________________________________________________________________
            GameObject gameBtn = new GameObject("Game Button");

                // Sets advBtn as child of canvas and adds components for GUI button necessities.
                gameBtn.transform.parent = popup.transform;
                gameBtn.AddComponent<CanvasRenderer>();
                gameBtn.AddComponent<Image>();
                gameBtn.AddComponent<Button>();

                // THIS DISABLES THE BUTTON COMPONENT TO AVOID MAKING IT FUNCTION. WILL ENABLE WHEN BUILDING LARGER SCALE.
                gameBtn.GetComponent<Button>().enabled = false;
                
                // Targets the MoveToAdventure function when button advBtn is pressed.
                // gameBtn.GetComponent<Button>().onClick.AddListener(delegate{ });

                // Sets advBtn sprite and transform.
                gameBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/popupBtn");

                // Sets appropriate size and position.
                gameBtn.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                gameBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(275, 65);
                gameBtn.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

                    // Adventure Button Text_____________________________________________
                    GameObject gmBtntxt = new GameObject("Text");
            
                        // Sets bsText as child of advBtn and adds components for GUI text necessities.
                        gmBtntxt.transform.parent = gameBtn.transform;
                        gmBtntxt.AddComponent<CanvasRenderer>();
                        gmBtntxt.AddComponent<TextMeshProUGUI>(); 

                        // Sets appropriate size and position.
                        gmBtntxt.GetComponent<RectTransform>().sizeDelta = new Vector2(266.955f, 57.5f);
                        gmBtntxt.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                        gmBtntxt.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                        
                        // Creates a temporary variable for the text component to avoid repetetive fetches.
                        TextMeshProUGUI tmpgui = gmBtntxt.GetComponent<TextMeshProUGUI>();            

                            // Sets all necessary values for text.
                            tmpgui.text = "Play Game";
                            tmpgui.color = Color.white;
                            tmpgui.fontWeight = FontWeight.Bold;
                            tmpgui.fontSize = 30;
                            tmpgui.alignment = TextAlignmentOptions.Center;
                            tmpgui.verticalAlignment = VerticalAlignmentOptions.Middle;        
    }

    private void TogglePopup()
    {
        BulkPopupDisable(popup.transform.parent.name);

        // If the popup is already open, we close it.
        if(popup.activeSelf)
        {
            popup.SetActive(false);
        }

        // Otherwise, we open it.
        else
        {
            popup.SetActive(true);
        }
    }

    // Collects all popup objects and disables them with the exception of the a particular popup if sent in.
    public void BulkPopupDisable(string currentPopupName = null)
    {
        GameObject[] allPopups = GameObject.FindGameObjectsWithTag("popup");

        foreach(GameObject pop in allPopups)
        {
            // We avoid disabling this object's popup to avoid conflics with logic below.
            if(currentPopupName == null || !pop.transform.parent.name.Equals(currentPopupName))
            {
                pop.SetActive(false);
            }

        }
    }

    // Returns a random color to assign.
    private Color PickColor()
    {
        // Switch for color choices.
        return Random.Range(1, 8) switch
        {
            1 => Color.blue,
            2 => Color.cyan,
            3 => Color.green,
            4 => Color.grey,
            5 => Color.red,
            6 => Color.white,
            7 => Color.yellow,
            _ => Color.magenta,
        };
    }

    public string BookName { get{ return bookName; } }
    public int BookCount { get{ return BookCount; } }
}