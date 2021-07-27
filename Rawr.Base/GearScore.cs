using System;
using System.Collections.Generic;
using System.Text;

namespace Rawr
{
	class GearScore
	{
		private const float scale = 1.8618f;

		/*
		 * Switches are ugly, but supposedly they compile into something functionally equivalent to a constant hash-table.
		 * Performance-wise, this is significantly better than the rather expensive hash tables one would normally use.
		 */
		private static float slotModifier(ItemSlot slot)
		{
			switch(slot)
			{
				case ItemSlot.TwoHand:
					return 2f;
				case ItemSlot.MainHand:
				case ItemSlot.OneHand:
				case ItemSlot.OffHand:
				case ItemSlot.Head:
				case ItemSlot.Chest:
				case ItemSlot.Legs:
					return 1f;
				case ItemSlot.Shoulders:
				case ItemSlot.Waist:
				case ItemSlot.Feet:
				case ItemSlot.Hands:
					return 0.75f;
				case ItemSlot.Trinket:
				case ItemSlot.Neck:
				case ItemSlot.Wrist:
				case ItemSlot.Finger:
				case ItemSlot.Back:
					return 0.5625f;
				case ItemSlot.Ranged:
					return 0.3164f;
			}
            return 0f;
		}

		private static float qualityScale(ItemQuality quality)
        {
            switch (quality)
            {
				case ItemQuality.Legendary:
					return 1.3f;
				case ItemQuality.Uncommon:
				case ItemQuality.Poor:
					return 0.005f;
			}
			return 1f;
        }

		private static float qualityDeficit(ItemQuality quality)
        {
			switch(quality)
            {
				case ItemQuality.Epic:
					return 91.45f;
				case ItemQuality.Rare:
					return 81.375f;
				case ItemQuality.Common:
					return 73f;
			}
			return 0f;
        }

		private static float qualityDivisor(ItemQuality quality)
		{
			switch (quality)
			{
				case ItemQuality.Epic:
					return 0.65f;
				case ItemQuality.Rare:
					return 0.81f;
			}
			return 1f;
		}
		
		public static int GetGearScore(Item item)
        {
			return (int)(
				(item.ItemLevel - qualityDeficit(item.Quality))
				/ qualityDivisor(item.Quality)
				* slotModifier(item.Slot) * scale * qualityScale(item.Quality)
				);
        }
	}
}
