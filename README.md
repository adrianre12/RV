# RV
Demo implementations of RV-SmartAI and RV-HonorAI 
To make this work you will need to install the [RV-SmartAI](https://assetstore.unity.com/packages/tools/ai/rv-smart-ai-146170) and [RV-HonorAI](https://assetstore.unity.com/packages/tools/ai/rv-honor-ai-188764) and [Unity Starter Assets](https://assetstore.unity.com/packages/essentials/starter-assets-third-person-character-controller-196526)

**This is work in progress**

## Scenes
- Avatar: Just a third person avatar.
- NPC: Two NPCs who fight each other.
- NPC-Player: NPC hostile to player. WIP

## Health Bar
RV-HonorAI comes with a health bar prefab called CharCanvas. If this is added as a child of a NPC the health bar will automatically be displayed and updated.
Unfortunatly in 1.0 it is broken due to a missing font and image. I have included a fixed version under prefabs called CharCanvasFixed.
The fix was;
- Open the CharCanvas prefab. Set the font in the "text" and "healthNumber". I used the Unity supplied Arial.
- In the "GameObject" (yes it is called that!) set the Source Image, I used UISprite and then set the Image Type to Filled and fill origin to Left.