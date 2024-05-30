
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
    public string[] Itemrecipes;
    public Item[] Itemresults;
    public Slot ItemResultsSlots;

     private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(cItem!= null)
            {
                customCursor.gameObject.SetActive(false);
                Slot nearSlot = null;
                float shortDistance=float.MaxValue;

                foreach(Slot slot in craftingSlots)
                {
                    float dist=Vector2.Distance(Input.mousePosition, slot.transform.position);


                    if(dist<shortDistance)
                    {
                        shortDistance = dist;
                        nearSlot = slot;
                    }
                }
                nearSlot.gameObject.SetActive(true);    
                nearSlot.GetComponent<Image>().sprite=cItem.GetComponent<Image>().sprite;
                nearSlot.item = cItem;

                itemList[nearSlot.index] = cItem;
                cItem = null;

                CheckRecipes();
            }
        }
    }

    private void CheckRecipes()
    {
        ItemResultsSlots.gameObject.SetActive(false);
        ItemResultsSlots.item = null;
        string currentRecipe = "";

        foreach(Item item in itemList)
        {
            if(item!=null)
            {
                currentRecipe += item.Name;
            }
            else
            {
                currentRecipe += "null";
            }
        }

        for(int i =0;i<Itemrecipes.Length;i++)
        {
            if (Itemrecipes[i]==currentRecipe)
            {
                ItemResultsSlots.gameObject.SetActive(true);
                ItemResultsSlots.GetComponent<Image>().sprite = Itemresults[i].GetComponent<Image>().sprite;
                ItemResultsSlots.item = Itemresults[i]; 
            }
        }

    }

    public void OnclickSlot(Slot slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
       slot.gameObject.SetActive(false);
        CheckRecipes();


    }
    public void OnMouseDownItem(Item item)
    {
        if(cItem==null)
        {
            cItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = cItem.GetComponent<Image>().sprite;

        }
    }
}
