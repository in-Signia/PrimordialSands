using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PrimordialSands.Dusts
{
    class IndenwoodDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.alpha = 25;
            dust.scale = 1.5f;
            dust.velocity *= 0.75f;
            dust.noGravity = true;
            dust.noLight = true;
        }
    }
}