//
// Auto Generated Code By Text
// @Author abaojin
//

using GameEngine;

public class PlayerDataTab  : IConfData
{
	// 编号
	public string ID {
		get;
		set;
	}
	// 名称
	public string Name {
		get;
		set;
	}
	// 资源编号
	public string AssetName {
		get;
		set;
	}
	// 血
	public int HP {
		get;
		set;
	}
	// 攻击
	public int Attack {
		get;
		set;
	}
	// 防御
	public int Defence {
		get;
		set;
	}
	// 战力
	public int ActPoints {
		get;
		set;
	}
	// 身法
	public int ActSpeed {
		get;
		set;
	}
	// 命中
	public int Hit {
		get;
		set;
	}
	// 闪避
	public int Dodge {
		get;
		set;
	}
	// 暴击
	public int Critical {
		get;
		set;
	}
}

public class PlayerDataTabConf : AbsTabConf
{
	public const string FILE_NAME = "playerdata.tab";

	public enum Cols
	{
		ID,
		NAME,
		ASSETNAME,
		HP,
		ATTACK,
		DEFENCE,
		ACTPOINTS,
		ACTSPEED,
		HIT,
		DODGE,
		CRITICAL,
	}

	public override void Init()
	{
		ConfFactory.LoadConf<TabReaderImpl>(FILE_NAME, this);
	}

	public override void OnRow(ITabRow row) {
		PlayerDataTab tab = new PlayerDataTab();
		tab.ID = row.GetString((int)Cols.ID);
		tab.Name = row.GetString((int)Cols.NAME);
		tab.AssetName = row.GetString((int)Cols.ASSETNAME);
		tab.HP = row.GetInt((int)Cols.HP);
		tab.Attack = row.GetInt((int)Cols.ATTACK);
		tab.Defence = row.GetInt((int)Cols.DEFENCE);
		tab.ActPoints = row.GetInt((int)Cols.ACTPOINTS);
		tab.ActSpeed = row.GetInt((int)Cols.ACTSPEED);
		tab.Hit = row.GetInt((int)Cols.HIT);
		tab.Dodge = row.GetInt((int)Cols.DODGE);
		tab.Critical = row.GetInt((int)Cols.CRITICAL);

		if (!ConfPool.ContainsKey(tab.ID.ToString())) {
			ConfPool.Add(tab.ID.ToString(), tab);
		}
	}
}
// End of Auto Generated Code
