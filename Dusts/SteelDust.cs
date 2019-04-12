using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PrimordialSands.Dusts
{
    class SteelDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.scale = 1f;
            dust.velocity *= 0.5f;
            dust.noGravity = true;
            dust.noLight = true;
        }
    }
}