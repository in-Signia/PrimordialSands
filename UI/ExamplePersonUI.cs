using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace PrimordialSands.UI
{ 
	class ExamplePersonUI : UIState
	{
		VanillaItemSlotWrapper vanillaItemSlot;
        private UIImage ArtifactImage1;

        public override void OnInitialize()
		{
			vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.ChestItem, 0.85f);
			vanillaItemSlot.Left.Pixels = 20;
			vanillaItemSlot.Top.Pixels = 270;
            vanillaItemSlot.validItem = item => item.IsAir || !item.IsAir && item.Prefix(-3);
            Append(vanillaItemSlot);
        }
		public override void OnDeactivate()
		{
			if (!vanillaItemSlot.item.IsAir)
			{
				Main.LocalPlayer.QuickSpawnClonedItem(vanillaItemSlot.item, vanillaItemSlot.item.stack);
                vanillaItemSlot.item.TurnToAir();
            }
		}
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (!Main.playerInventory)
			{

				PrimordialSands.instance.examplePersonUserInterface.SetState(null);
			}
		}

		bool tickPlayed = false;
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
            int slotX = -35;
			int slotY = 320;
            if (!vanillaItemSlot.item.IsAir)
            {
                int awesomePrice = Item.buyPrice(0, 1, 0, 0);

                string costText = Lang.inter[46].Value + ": ";
                string coinsText = "";
                int[] coins = Utils.CoinsSplit(awesomePrice);
                if (coins[3] > 0)
                {
                    coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinPlatinum).Hex3() + ":" + coins[3] + " " + Lang.inter[15].Value + "] ";
                }
                if (coins[2] > 0)
                {
                    coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinGold).Hex3() + ":" + coins[2] + " " + Lang.inter[16].Value + "] ";
                }
                if (coins[1] > 0)
                {
                    coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinSilver).Hex3() + ":" + coins[1] + " " + Lang.inter[17].Value + "] ";
                }
                if (coins[0] > 0)
                {
                    coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinCopper).Hex3() + ":" + coins[0] + " " + Lang.inter[18].Value + "] ";
                }
                ItemSlot.DrawSavings(Main.spriteBatch, slotX + 130, Main.instance.invBottom, true);
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, costText, new Vector2((slotX + 50), slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, coinsText, new Vector2((slotX + 50) + Main.fontMouseText.MeasureString(costText).X, (float)slotY), Color.White, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                int reforgeX = slotX + 70;
                int reforgeY = slotY + 40;
                bool hoveringOverReforgeButton = Main.mouseX > reforgeX - 15 && Main.mouseX < reforgeX + 15 && Main.mouseY > reforgeY - 15 && Main.mouseY < reforgeY + 15 && !PlayerInput.IgnoreMouseInterface;
                Texture2D reforgeTexture = Main.reforgeTexture[hoveringOverReforgeButton ? 1 : 0];
                Main.spriteBatch.Draw(reforgeTexture, new Vector2(reforgeX, reforgeY), null, Color.White, 0f, reforgeTexture.Size() / 2f, 0.8f, SpriteEffects.None, 0f);
                if (hoveringOverReforgeButton)
                {
                    Main.hoverItemName = Lang.inter[19].Value;
                    if (!tickPlayed)
                    {
                        Main.PlaySound(12, -1, -1, 1, 1f, 0f);
                    }
                    tickPlayed = true;
                    Main.LocalPlayer.mouseInterface = true;
                    if (Main.mouseLeftRelease && Main.mouseLeft && Main.LocalPlayer.CanBuyItem(awesomePrice, -1) && ItemLoader.PreReforge(vanillaItemSlot.item))
                    {
                        Main.LocalPlayer.BuyItem(awesomePrice, -1);
                        bool favorited = vanillaItemSlot.item.favorited;
                        int stack = vanillaItemSlot.item.stack;
                        Item reforgeItem = new Item();
                        reforgeItem.netDefaults(vanillaItemSlot.item.netID);
                        reforgeItem = reforgeItem.CloneWithModdedDataFrom(vanillaItemSlot.item);
                        // This is the main effect of this slot. Giving the Awesome prefix 90% of the time and the ReallyAwesome prefix the other 10% of the time. All for a constant 1 gold. Useless, but informative.
                        if (Main.rand.NextBool(10))
                        {
                            reforgeItem.Prefix(PrimordialSands.instance.PrefixType("Abused"));
                        }
                        else
                        {
                            reforgeItem.Prefix(PrimordialSands.instance.PrefixType("Molested"));
                        }
                        vanillaItemSlot.item = reforgeItem.Clone();
                        vanillaItemSlot.item.position.X = Main.player[Main.myPlayer].position.X + (float)(Main.player[Main.myPlayer].width / 2) - (float)(vanillaItemSlot.item.width / 2);
                        vanillaItemSlot.item.position.Y = Main.player[Main.myPlayer].position.Y + (float)(Main.player[Main.myPlayer].height / 2) - (float)(vanillaItemSlot.item.height / 2);
                        vanillaItemSlot.item.favorited = favorited;
                        vanillaItemSlot.item.stack = stack;
                        ItemLoader.PostReforge(vanillaItemSlot.item);
                        ItemText.NewText(vanillaItemSlot.item, vanillaItemSlot.item.stack, true, false);
                        Main.PlaySound(SoundID.Item37, -1, -1);
                    }
                }
                else
                {
                    tickPlayed = false;
                }
            }
            else
            {
				string message = "Artifact";
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message, new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}
		}
	}
}
