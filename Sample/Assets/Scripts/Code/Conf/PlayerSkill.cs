//
// Auto Generated Code By Text
// @Author abaojin
//

using GameEngine;

public class PlayerSkillTabData  : IConfData
{
	// #唯一Id
	public int Id {
		get;
		set;
	}
	// 技能描述
	public string Desc {
		get;
		set;
	}
	// 技能名称
	public string Name {
		get;
		set;
	}
}

public class PlayerSkillTabConf : AbsTabConf
{
	public const string FILE_NAME = "playerskill.tab";

	public enum Cols
	{
		ID,
		DESC,
		NAME,
	}

	public override void Init()
	{
		ConfFactory.LoadConf<TabReaderImpl>(FILE_NAME, this);
	}

	public override void OnRow(ITabRow row) {
		PlayerSkillTabData tab = new PlayerSkillTabData();
		tab.Id = row.GetInt((int)Cols.ID);
		tab.Desc = row.GetString((int)Cols.DESC);
		tab.Name = row.GetString((int)Cols.NAME);

		if (!ConfPool.ContainsKey(tab.Id.ToString())) {
			ConfPool.Add(tab.Id.ToString(), tab);
		}
	}
}
// End of Auto Generated Code
