//
// Auto Generated Code By Text
// @Author abaojin
//

using GameEngine;

public class CreatePlayerTab  : IConfData
{
	// #唯一Id
	public int Id {
		get;
		set;
	}
	// 角色Id
	public int RoleType {
		get;
		set;
	}
	// 描述
	public string Desc {
		get;
		set;
	}
}

public class CreatePlayerTabConf : AbsTabConf
{
	public const string FILE_NAME = "createplayer.tab";

	public enum Cols
	{
		ID,
		ROLETYPE,
		DESC,
	}

	public override void Init()
	{
		ConfFactory.LoadConf<TabReaderImpl>(FILE_NAME, this);
	}

	public override void OnRow(ITabRow row) {
		CreatePlayerTab tab = new CreatePlayerTab();
		tab.Id = row.GetInt((int)Cols.ID);
		tab.RoleType = row.GetInt((int)Cols.ROLETYPE);
		tab.Desc = row.GetString((int)Cols.DESC);

		if (!ConfPool.ContainsKey(tab.Id.ToString())) {
			ConfPool.Add(tab.Id.ToString(), tab);
		}
	}
}
// End of Auto Generated Code
