using UnityEngine;
using UnityEngine.UI;
using System.Collections; 
public class CharacterSelection : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform characterGridParent; // Parent for character slots
    [SerializeField] private GameObject characterSlotPrefab; // Prefab for character slots

    [Header("Character Data")]
    [SerializeField] private CharacterSO[] characters; // Array of characters (ScriptableObjects)

    private int currentIndex = 0; // Currently hovered character
    private float holdTime = 0f; // Time space bar is held
    private const float holdDuration = 1f; // Hold time required to select a character

    private GameObject[] characterSlots; // Array for the UI character slots

    private void Start()
    {
        // Sort characters alphabetically (by name)
        System.Array.Sort(characters, (a, b) => string.Compare(a.name, b.name));

        // Generate the UI slots for characters
        characterSlots = new GameObject[characters.Length];
        for (int i = 0; i < characters.Length; i++)
        {
            GameObject slot = Instantiate(characterSlotPrefab, characterGridParent);
            Image charImage = slot.transform.Find("CharacterImage").GetComponent<Image>();
            Image nameImage = slot.transform.Find("NameImage").GetComponent<Image>();

            charImage.sprite = characters[i].characterImage; // Assign character image
            nameImage.sprite = characters[i].textImage; // Assign name artwork
            nameImage.enabled = false; // Disable name artwork by default

            characterSlots[i] = slot;
        }

        // Highlight the first character
        UpdateHover();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HoverNextCharacter();
            holdTime = 0f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            holdTime += Time.deltaTime;
            if (holdTime >= holdDuration)
            {
                SelectCharacter();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdTime = 0f;
        }
    }

    private void HoverNextCharacter()
    {
        SetCharacterHighlight(currentIndex, false); // Unhighlight current character

        currentIndex = (currentIndex + 1) % characters.Length; // Move to the next character

        SetCharacterHighlight(currentIndex, true); // Highlight new character
    }

    private void SetCharacterHighlight(int index, bool isHighlighted)
    {
        Image nameImage = characterSlots[index].transform.Find("NameImage").GetComponent<Image>();
        nameImage.enabled = isHighlighted; // Show or hide the name artwork
    }

    private void UpdateHover()
    {
        for (int i = 0; i < characterSlots.Length; i++)
        {
            SetCharacterHighlight(i, i == currentIndex); // Highlight only the current index
        }
    }

    private void SelectCharacter()
    {
        Debug.Log($"Selected Character: {characters[currentIndex].name}");
        // Add further game-start logic here
    }
}
