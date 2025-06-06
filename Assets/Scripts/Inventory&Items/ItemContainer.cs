using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class ItemSlot
{
    public Item item;
    public int count;

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }
}

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;
    public bool isDirty;

    public void Add(Item item, int count = 1)
    {
        isDirty = true;

        if (item.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);

            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item == null);

                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            //add non stackable item to ours item container
            ItemSlot itemSlot = slots.Find(x => x.item == null);

            if (itemSlot != null)
            {
                itemSlot.item = item;
            }
        }
    }

    public void Remove(Item itemToRemove, int cnt = 1)
    {
        isDirty = true;

        if (itemToRemove.stackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
            if (itemSlot == null)
            {
                return;
            }

            itemSlot.count -= cnt;
            if(itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }
        else
        {
            while(cnt > 0)
            {
                cnt -= 1;

                ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
                if(itemSlot == null)
                {
                    return ;
                }

                itemSlot.Clear();
            }
        }
    }
    
    internal bool CheckItem(ItemSlot checkingItem)
    {
        ItemSlot itemSlot = slots.Find(x => x.item == checkingItem.item);

        if(itemSlot == null)
        {
            return false;
        }

        if(checkingItem.item.stackable)
        {
            return itemSlot.count >= checkingItem.count;
        }

        return true;
    }
    // 아이템 체크 함수 오버로딩
    internal bool CheckItem(Item checkingItem)
    {
        ItemSlot itemSlot = slots.Find(x => x.item == checkingItem);

        if (itemSlot == null)
        {
            return false;
        }

        if (checkingItem.stackable)
        {
            return itemSlot.count >= 1;
        }

        return true;
    }

    internal bool CheckSpace()
    {
        for(int i=0; i<slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                return true;
            }
        }

        return false;
    }
}