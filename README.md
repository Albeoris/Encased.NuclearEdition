# Encased.NuclearEdition
Modification for the game "Encased"

# Install
1. Copy mod files to the %appdata%\..\LocalLow\Dark Crystal Games\Encased\Mods\NuclearEdition
2. Wait for official mod support. ;)

# Build
1. Open Encased.NuclearEdition.csproj via text editor
2. Change "C:\Program Files (x86)\Steam\steamapps\common\Encased" to your own game path
3. Change "..\..\..\..\Users\Admin\AppData\LocalLow" to your own %appdata%\..\LocalLow\ path
4. Build in Visual Studio 2017/2019

# Todo
1. Resolve game path via Windows Registry
2. Resolve %appdata%

# Feautures
1. Container highlighting and mass gathering

Hold Shift to highlight interacive objects.

Press Shift+Space to collect items from all highlighted containers or open any container while holding Shift.

The laid out items will be placed in the nearest container.


# Current loader

	string text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/../LocalLow/Dark Crystal Games/Encased/Mods";
	text = Path.GetFullPath(text);
	if (Directory.Exists(text))
	{
		Debug.Log("Looking for mods (" + text + ")...");
		foreach (string text2 in Directory.GetFiles(text, "*.dll", SearchOption.AllDirectories))
		{
			Debug.Log("Found assembly (" + text2 + "). Looking for entry points...");
			try
			{
				foreach (Type type in Assembly.LoadFrom(text2).GetTypes())
				{
					if (type.Name == "ModEntryPoint")
					{
						Debug.Log("Found entry point (" + type.FullName + "). Initializing...");
						GameObject gameObject = new GameObject(text2 + "_" + type.FullName);
						gameObject.AddComponent(type);
						UnityEngine.Object.DontDestroyOnLoad(gameObject);
					}
				}
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("Failed to load assembly {0}. Error: {1}", text2, arg));
			}
		}
		return;
	}
	Debug.Log("Mods directory is not exists (" + text + ")");