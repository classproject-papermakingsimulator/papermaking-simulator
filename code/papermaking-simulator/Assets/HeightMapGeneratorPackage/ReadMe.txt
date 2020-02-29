Depth & Height Generator Pack
by Martin Reintges
-----------------------------

This depth-texture and height-map package enables you to create a texture from your scene. 
Included are a script based texture-generator as well as a shader based texture renderer.


Showcase
--------
Assets/HeightMapGeneratorPackage/Scenes/BoxPresentationScene.unity -> How to create a depth texture and display it in UI
Assets/HeightMapGeneratorPackage/Scenes/HeightMapGeneratorScene.unity -> Details and examples for a generated depth texture
Assets/HeightMapGeneratorPackage/Scenes/HeightMapRendererScene.unity -> Details and examples for a rendered depth texture


Workflow
--------
Workflow for both types is explained in: Assets/HeightMapGeneratorPackage/Scenes/BoxPresentationScene.unity

Script access
-------------
Get the texture
"GetComponent<HeightMapGenerator>().HeightTexture" (Texture2D)
"GetComponent<HeightMapRenderer>().HeightTexture" (RenderTexture)

Update the Texture 
"GetComponent<HeightMapGenerator>().UpdateTextureNow();"
"GetComponent<HeightMapRenderer>().UpdateTextureNow();"

Event for new texture has been created
"GetComponent<HeightMapGenerator>().NewTextureCreated" (Action<Texture2D>)
"GetComponent<HeightMapRenderer>().NewTextureCreated" (Action<RenderTexture>)
Examples:
Assets/HeightMapGeneratorPackage/Scripts/Demos


Contact
-------
Mail: mailnightowl@gmail.com
Website: reintges.webs.com