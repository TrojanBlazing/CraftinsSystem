using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private Item cItem;
    public Image customCursor;

    public Slot[] craftingSlots;

    public List<Item> itemList;
    public List<string> itemRecipes;
    public List<Item> itemResults;
    public Slot itemResultSlot;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (cItem != null)
            {
                customCursor.gameObject.SetActive(false);
                Slot nearSlot = null;
                float shortDistance = float.MaxValue;

                foreach (Slot slot in craftingSlots)
                {
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);

                    if (dist < shortDistance)
                    {
                        shortDistance = dist;
                        nearSlot = slot;
                    }
                }

                if (nearSlot != null)
                {
                    nearSlot.gameObject.SetActive(true);
                    nearSlot.GetComponent<Image>().sprite = cItem.GetComponent<Image>().sprite;
                    nearSlot.item = cItem;
                    itemList[nearSlot.index] = cItem;
                    cItem = null;

                    CheckRecipes();
                }
            }
        }
    }

    private void CheckRecipes()
    {
        itemResultSlot.gameObject.SetActive(false);
        itemResultSlot.item = null;

        List<string> currentRecipe = new List<string>();

        foreach (Item item in itemList)
        {
            if (item != null)
            {
                currentRecipe.Add(item.ID); 
            }
        }

        currentRecipe.Sort();

        foreach (string recipe in itemRecipes)
        {
            List<string> sortedRecipe = new List<string>(recipe.Split(','));
            sortedRecipe.Sort();

            if (AreListsEqual(currentRecipe, sortedRecipe))
            {
                int index = itemRecipes.IndexOf(recipe);
                if (index >= 0 && index < itemResults.Count)
                {
                    itemResultSlot.gameObject.SetActive(true);
                    itemResultSlot.GetComponent<Image>().sprite = itemResults[index].GetComponent<Image>().sprite;
                    itemResultSlot.item = itemResults[index];
                }
                break;
            }
        }
    }

    private bool AreListsEqual(List<string> list1, List<string> list2)
    {
        if (list1.Count != list2.Count) return false;
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i]) return false;
        }
        return true;
    }

    public void OnClickSlot(Slot slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckRecipes();
    }

    public void OnMouseDownItem(Item item)
    {
        if (cItem == null)
        {
            cItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = cItem.GetComponent<Image>().sprite;
        }
    }
}
