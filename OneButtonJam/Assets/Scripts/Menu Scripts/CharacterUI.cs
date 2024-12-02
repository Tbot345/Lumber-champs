using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public CharacterSO[] characters;
    [Header("UI References")]
    [SerializeField] private Transform topRowParent;     // Parent for the top row
    [SerializeField] private Transform middleRowParent;  // Parent for the middle row
    [SerializeField] private Transform bottomRowParent;  // Parent for the bottom row
    [SerializeField] private GameObject characterSlotPrefab; // Prefab for character slots
    [SerializeField] private TextMeshProUGUI choosingCounterText;

    [Header("Hover Color")]
    [SerializeField] private Color hoveredColor;

    private int currentIndex = 0; // Currently hovered character

    private float choosingDuration = 5f;
    private float choosingCounter;

    private GameObject[] characterSlots; // Array for the UI character slots

    private void Start()
    {
        choosingCounter = choosingDuration;
        characterSlots = new GameObject[characters.Length];

        // Assign characters to rows based on the keyboard layout
        int characterIndex = 0;

        // Add 10 characters to the top row
        for (int i = 0; i < 10 && characterIndex < characters.Length; i++)
        {
            CreateCharacterSlot(topRowParent, characterIndex++);
        }

        // Add 9 characters to the middle row
        for (int i = 0; i < 9 && characterIndex < characters.Length; i++)
        {
            CreateCharacterSlot(middleRowParent, characterIndex++);
        }

        // Add 7 characters to the bottom row
        for (int i = 0; i < 7 && characterIndex < characters.Length; i++)
        {
            CreateCharacterSlot(bottomRowParent, characterIndex++);
        }

        // Highlight the first character
        UpdateHover();
    }

    private void Update()
    {
        choosingCounterText.text = Mathf.Round(choosingCounter).ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            choosingCounter = choosingDuration;

            HoverNextCharacter();
        }

        if (!Input.GetKey(KeyCode.Space))
        {
            choosingCounter -= Time.deltaTime;

            if(choosingCounter <= 0)
            {
                SelectCharacter();
            }
        }
    }


    private void HoverNextCharacter()
    {
        SetCharacterHighlight(currentIndex, false); // Unhighlight current character

        currentIndex = (currentIndex + 1 + characters.Length) % characters.Length; // Move to the previous character, wrapping around

        SetCharacterHighlight(currentIndex, true); // Highlight new character
    }

    private void SetCharacterHighlight(int index, bool isHighlighted)
    {
        Image nameImage = characterSlots[index].transform.Find("CharacterText").GetComponent<Image>();
        Image characterImage = characterSlots[index].transform.Find("Character").GetComponent<Image>();

        if(isHighlighted)
        {
            nameImage.color = hoveredColor;
            characterImage.color = hoveredColor;
        } else
        {
            nameImage.color = Color.white;
            characterImage.color = Color.white;
        }

        nameImage.enabled = isHighlighted; // Show or hide the name artwork
    }

    private void UpdateHover()
    {
        for (int i = 0; i < characterSlots.Length; i++)
        {
            SetCharacterHighlight(i, i == currentIndex); // Highlight only the current index
        }
    }

    private void CreateCharacterSlot(Transform parent, int index)
    {
        GameObject slot = Instantiate(characterSlotPrefab, parent);
        Image charImage = slot.transform.Find("Character").GetComponent<Image>();
        Image nameImage = slot.transform.Find("CharacterText").GetComponent<Image>();

        charImage.sprite = characters[index].characterImage; // Assign character image
        nameImage.sprite = characters[index].textImage; // Assign name artwork
        nameImage.enabled = false; // Disable name artwork by default

        characterSlots[index] = slot;
    }

    private void SelectCharacter()
    {
        Debug.Log($"Selected Character: {characters[currentIndex].name}");

        // Save the selected character's index using PlayerPrefs
        PlayerPrefs.SetInt("SelectedCharacterIndex", currentIndex);
        PlayerPrefs.Save();

        // Load the game scene (replace "GameScene" with your scene name)
        SceneManager.LoadScene("MainGame");
    }
}
