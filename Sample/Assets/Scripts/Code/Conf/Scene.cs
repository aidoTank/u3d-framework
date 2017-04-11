//
// Auto Generated Code By Text
// @Author abaojin
//

using GameEngine;

public class SceneTabData  : IConfData
{
	// #唯一标示
	public int Id {
		get;
		set;
	}
	// 场景名称
	public string Name {
		get;
		set;
	}
	// 场景描述
	public string Des {
		get;
		set;
	}
}

public class SceneTabConf : AbsTabConf
{
	public const string FILE_NAME = "scene.tab";

	public enum Cols
	{
		ID,
		NAME,
		DES,
	}

	public override void Init()
	{
		ConfFactory.LoadConf<TabReaderImpl>(FILE_NAME, this);
	}

	public override void OnRow(ITabRow row) {
		SceneTabData tab = new SceneTabData();
		tab.Id = row.GetInt((int)Cols.ID);
		tab.Name = row.GetString((int)Cols.NAME);
		tab.Des = row.GetString((int)Cols.DES);

		if (!ConfPool.ContainsKey(tab.Id.ToString())) {
			ConfPool.Add(tab.Id.ToString(), tab);
		}
	}
}
// End of Auto Generated Code
