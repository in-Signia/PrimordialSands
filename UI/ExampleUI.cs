﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;

namespace PrimordialSands.UI
{
	// ExampleUIs visibility is toggled by typing "/coin" in chat. (See CoinCommand.cs)
	// ExampleUI is a simple UI example showing how to use UIPanel, UIImageButton, and even a custom UIElement.
	class ExampleUI : UIState
	{
		public UIPanel coinCounterPanel;
		public UIMoneyDisplay moneyDiplay;
		public static bool visible = false;

		public override void OnInitialize()
		{
			coinCounterPanel = new UIPanel();
			coinCounterPanel.SetPadding(0);
			coinCounterPanel.Left.Set(400f, 0f);
			coinCounterPanel.Top.Set(100f, 0f);
			coinCounterPanel.Width.Set(170f, 0f);
			coinCounterPanel.Height.Set(70f, 0f);
			coinCounterPanel.BackgroundColor = new Color(73, 94, 171);

			coinCounterPanel.OnMouseDown += new UIElement.MouseEvent(DragStart);
			coinCounterPanel.OnMouseUp += new UIElement.MouseEvent(DragEnd);

			Texture2D buttonPlayTexture = ModLoader.GetTexture("Terraria/UI/ButtonPlay");
			UIImageButton playButton = new UIImageButton(buttonPlayTexture);
			playButton.Left.Set(110, 0f);
			playButton.Top.Set(10, 0f);
			playButton.Width.Set(22, 0f);
			playButton.Height.Set(22, 0f);
			playButton.OnClick += new MouseEvent(PlayButtonClicked);
			coinCounterPanel.Append(playButton);

			Texture2D buttonDeleteTexture = ModLoader.GetTexture("Terraria/UI/ButtonDelete");
			UIImageButton closeButton = new UIImageButton(buttonDeleteTexture);
			closeButton.Left.Set(140, 0f);
			closeButton.Top.Set(10, 0f);
			closeButton.Width.Set(22, 0f);
			closeButton.Height.Set(22, 0f);
			closeButton.OnClick += new MouseEvent(CloseButtonClicked);
			coinCounterPanel.Append(closeButton);

			moneyDiplay = new UIMoneyDisplay();
			moneyDiplay.Left.Set(15, 0f);
			moneyDiplay.Top.Set(20, 0f);
			moneyDiplay.Width.Set(100f, 0f);
			moneyDiplay.Height.Set(0, 1f);
			coinCounterPanel.Append(moneyDiplay);

			base.Append(coinCounterPanel);
		}

		private void PlayButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(SoundID.MenuOpen);
			moneyDiplay.ResetCoins();
		}

		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(SoundID.MenuOpen);
			visible = false;
		}

		Vector2 offset;
		public bool dragging = false;
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			offset = new Vector2(evt.MousePosition.X - coinCounterPanel.Left.Pixels, evt.MousePosition.Y - coinCounterPanel.Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			dragging = false;

			coinCounterPanel.Left.Set(end.X - offset.X, 0f);
			coinCounterPanel.Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Vector2 MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
			if (coinCounterPanel.ContainsPoint(MousePosition))
			{
				// Checking ContainsPoint and then setting mouseInterface to true is very common. This causes clicks to not cause the player to use current items. 
				Main.LocalPlayer.mouseInterface = true;
			}
			if (dragging)
			{
				coinCounterPanel.Left.Set(MousePosition.X - offset.X, 0f);
				coinCounterPanel.Top.Set(MousePosition.Y - offset.Y, 0f);
				Recalculate();
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime); // don't remove.

			// Here we check if the coinCounterPanel is outside the UIState rectangle. (in other words, the whole screen)
			// By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution.
			if (!coinCounterPanel.GetDimensions().ToRectangle().Intersects(GetDimensions().ToRectangle()))
			{
				var parentSpace = GetDimensions().ToRectangle();
				coinCounterPanel.Left.Pixels = Utils.Clamp(coinCounterPanel.Left.Pixels, 0, parentSpace.Right - coinCounterPanel.Width.Pixels);
				coinCounterPanel.Top.Pixels = Utils.Clamp(coinCounterPanel.Top.Pixels, 0, parentSpace.Bottom - coinCounterPanel.Height.Pixels);
				coinCounterPanel.Recalculate();
			}
		}

		public void updateValue(int pickedUp)
		{
			moneyDiplay.coins += pickedUp;
			moneyDiplay.addCPM(pickedUp);
		}
	}

	public class UIMoneyDisplay : UIElement
	{
		public long coins;

		public UIMoneyDisplay()
		{
			Width.Set(100, 0f);
			Height.Set(40, 0f);

			for (int i = 0; i < 60; i++)
			{
				coinBins[i] = -1;
			}
		}

		//DateTime dpsEnd;
		//DateTime dpsStart;
		//int dpsDamage;
		public bool dpsStarted;
		public DateTime dpsLastHit;

		// Array of ints 60 long.
		// "length" = seconds since reset
		// reset on button or 20 seconds of inactivity?
		// pointer to index so on new you can clear previous
		int[] coinBins = new int[60];
		int coinBinsIndex;

		public void addCPM(int coins)
		{
			int second = DateTime.Now.Second;
			if (second != coinBinsIndex)
			{
				coinBinsIndex = second;
				coinBins[coinBinsIndex] = 0;
			}
			coinBins[coinBinsIndex] += coins;
		}

		public int getCPM()
		{
			int second = DateTime.Now.Second;
			if (second != coinBinsIndex)
			{
				coinBinsIndex = second;
				coinBins[coinBinsIndex] = 0;
			}

			long sum = coinBins.Sum(a => a > -1 ? a : 0);
			int count = coinBins.Count(a => a > -1);
			if(count == 0)
			{
				return 0;
			}
			return (int)((sum * 60f) / count);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 drawPos = new Vector2(innerDimensions.X + 5f, innerDimensions.Y + 30f);

			float shopx = innerDimensions.X;
			float shopy = innerDimensions.Y;

			int[] coinsArray = Utils.CoinsSplit(coins);
			for (int j = 0; j < 4; j++)
			{
				int num = (j == 0 && coinsArray[3 - j] > 99) ? -6 : 0;
				spriteBatch.Draw(Main.itemTexture[74 - j], new Vector2(shopx + 11f + (float)(24 * j), shopy /*+ 75f*/), null, Color.White, 0f, Main.itemTexture[74 - j].Size() / 2f, 1f, SpriteEffects.None, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, Main.fontItemStack, coinsArray[3 - j].ToString(), shopx + (float)(24 * j) + (float)num, shopy/* + 75f*/, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
			}

			coinsArray = Utils.CoinsSplit(getCPM());
			for (int j = 0; j < 4; j++)
			{
				int num = (j == 0 && coinsArray[3 - j] > 99) ? -6 : 0;
				spriteBatch.Draw(Main.itemTexture[74 - j], new Vector2(shopx + 11f + (float)(24 * j), shopy + 25f), null, Color.White, 0f, Main.itemTexture[74 - j].Size() / 2f, 1f, SpriteEffects.None, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, Main.fontItemStack, coinsArray[3 - j].ToString(), shopx + (float)(24 * j) + (float)num, shopy + 25f, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
			}
			Utils.DrawBorderStringFourWay(spriteBatch, /*PrimordialSands.exampleFont*/ Main.fontItemStack, "CPM", shopx + (float)(24 * 4), shopy + 25f, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
		}

		internal void ResetCoins()
		{
			coins = 0;
			for (int i = 0; i < 60; i++)
			{
				coinBins[i] = -1;
			}
		}
	}

	public class MoneyCounterGlobalItem : GlobalItem
	{
		public override bool OnPickup(Item item, Player player)
		{
			if (item.type == ItemID.CopperCoin)
			{
				PrimordialSands.instance.exampleUI.updateValue(item.stack);
				// We can cast mod to PrimordialSands or just utilize PrimordialSands.instance.
				// (mod as PrimordialSands).exampleUI.updateValue(item.stack);
			}
			else if (item.type == ItemID.SilverCoin)
			{
				PrimordialSands.instance.exampleUI.updateValue(item.stack * 100);
			}
			else if (item.type == ItemID.GoldCoin)
			{
				PrimordialSands.instance.exampleUI.updateValue(item.stack * 10000);
			}
			else if (item.type == ItemID.PlatinumCoin)
			{
				PrimordialSands.instance.exampleUI.updateValue(item.stack * 1000000);
			}
			return base.OnPickup(item, player);
		}
	}
}
