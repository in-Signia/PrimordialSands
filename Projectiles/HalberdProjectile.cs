using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.Projectiles
{
    public class HalberdProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Halberd");
        }

        public override void SetDefaults()
        {
            projectile.width = 140;
            projectile.height = 140;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.alpha = 0;
            projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
        }
        public override void AI() //Programmed by KittyKitCatCat, now owned by inSignia
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.direction = player.direction;
            player.heldProj = projectile.whoAmI;
            projectile.Center = vector;
            if (player.dead)
            {
                projectile.Kill();
                return;
            }
            if (!player.frozen)
            {
                projectile.spriteDirection = (projectile.direction = player.direction);
                projectile.alpha -= 127;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                if (projectile.localAI[0] > 0f)
                {
                    projectile.localAI[0] -= 1f;
                }
                float num = (float)player.itemAnimation / (float)player.itemAnimationMax;
                float num2 = 9f - num;
                float num3 = projectile.velocity.ToRotation();
                float num4 = projectile.velocity.Length();
                float num5 = 22f;
                Vector2 value = new Vector2(1f, 0f).RotatedBy((double)(3.14159274f + num2 * 6.28318548f), default(Vector2));
                Vector2 spinningpoint = value * new Vector2(num4, projectile.ai[0]);
                projectile.position += spinningpoint.RotatedBy((double)num3, default(Vector2)) + new Vector2(num4 + num5, 0f).RotatedBy((double)num3, default(Vector2));
                Vector2 destination = vector + spinningpoint.RotatedBy((double)num3, default(Vector2)) + new Vector2(num4 + num5 + 40f, 0f).RotatedBy((double)num3, default(Vector2));
                projectile.rotation = player.AngleTo(destination) + 0.7853982f * (float)player.direction;
                if (projectile.spriteDirection == -1)
                {
                    projectile.rotation += 3.14159274f;
                }
                player.DirectionTo(projectile.Center);
                Vector2 value2 = player.DirectionTo(destination);
                Vector2 vector2 = projectile.velocity.SafeNormalize(Vector2.UnitY);
            }
            if (player.itemAnimation == 2)
            {
                projectile.Kill();
                player.reuseDelay = 2;
            }
        }
    }
}