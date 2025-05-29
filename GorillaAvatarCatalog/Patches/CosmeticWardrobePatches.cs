using GorillaAvatarCatalog.Behaviours;
using GorillaAvatarCatalog.Models;
using HarmonyLib;

namespace GorillaAvatarCatalog.Patches
{
    [HarmonyPatch(typeof(CosmeticWardrobe))]
    internal class CosmeticWardrobePatches
    {
        [HarmonyPatch(nameof(CosmeticWardrobe.Start)), HarmonyPostfix, HarmonyWrapSafe]
        public static void StartPatch(CosmeticWardrobe __instance)
        {
            __instance.AddComponent<AvatarWardrobe>();
        }

        [HarmonyPatch(nameof(CosmeticWardrobe.HandleChangeCategory)), HarmonyPrefix, HarmonyWrapSafe]
        public static void ChangeCategoryPatch(CosmeticWardrobe __instance)
        {
            if (__instance.TryGetComponent(out AvatarWardrobe avatarWardrobe) && avatarWardrobe.currentState != EAvatarWardrobeState.None)
                avatarWardrobe.SwitchState(EAvatarWardrobeState.None);
        }

        [HarmonyPatch(nameof(CosmeticWardrobe.HandleCosmeticsUpdated)), HarmonyPostfix, HarmonyWrapSafe]
        public static void HandleCosmeticsUpdatedPatch(CosmeticWardrobe __instance)
        {
            if (__instance.TryGetComponent(out AvatarWardrobe avatarWardrobe) && avatarWardrobe.currentState != EAvatarWardrobeState.None)
                avatarWardrobe.HandleCosmeticsUpdated();
        }

        [HarmonyPatch(nameof(CosmeticWardrobe.HandlePressedNextSelection)), HarmonyPrefix, HarmonyWrapSafe]
        public static bool HandleNextSelectionPatch(CosmeticWardrobe __instance)
        {
            if (__instance.TryGetComponent(out AvatarWardrobe avatarWardrobe) && avatarWardrobe.currentState != EAvatarWardrobeState.None)
            {
                avatarWardrobe.HandlePressedNextSelection();
                return false;
            }

            return true;
        }

        [HarmonyPatch(nameof(CosmeticWardrobe.HandlePressedPrevSelection)), HarmonyPrefix, HarmonyWrapSafe]
        public static bool HandlePrevSelectionPatch(CosmeticWardrobe __instance)
        {
            if (__instance.TryGetComponent(out AvatarWardrobe avatarWardrobe) && avatarWardrobe.currentState != EAvatarWardrobeState.None)
            {
                avatarWardrobe.HandlePressedPrevSelection();
                return false;
            }

            return true;
        }

        [HarmonyPatch(nameof(CosmeticWardrobe.HandlePressedSelectCosmeticButton)), HarmonyPrefix, HarmonyWrapSafe]
        public static bool HandleSelectCosmeticPatch(CosmeticWardrobe __instance, GorillaPressableButton button)
        {
            if (__instance.TryGetComponent(out AvatarWardrobe avatarWardrobe) && avatarWardrobe.currentState != EAvatarWardrobeState.None)
            {
                avatarWardrobe.HandlePressedSelectCosmeticButton(button);
                return false;
            }

            return true;
        }
    }
}
