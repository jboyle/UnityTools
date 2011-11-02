import System.IO;
@MenuItem("Assets/SaveFontTexture...")

static function SaveFontTexture () {
	var tex:Texture2D = Selection.activeObject as Texture2D;
	if(tex == null) {
		EditorUtility.DisplayDialog("No texture selected","Please select a texture", "Cancel");
		return;
	}
	if(tex.format != TextureFormat.Alpha8){
		EditorUtility.DisplayDialog("Wrong format", "Texture must be in uncompressed alpha8 format", "Cancel");
		return;
	}
	
	var texPixels:Color[] = tex.GetPixels();
	var tex2:Texture2D = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
	tex2.SetPixels(texPixels);
	
	//save texture
	var texBytes:byte[] = tex2.EncodeToPNG();
	var fileName:String = EditorUtility.SaveFilePanel("Save font texture", "", "font Texture", "png");
	if(fileName.Length > 0){
		var f:FileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
		var b :BinaryWriter = new BinaryWriter(f);
		for(var i = 0; i < texBytes.Length; i++){
			b.Write(texBytes[i]);
		}
		b.Close();
	}
	
	DestroyImmediate(tex2);
}
		
		