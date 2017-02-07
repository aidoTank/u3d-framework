using UnityEditor;
using UnityEngine;

/***
 * ExportWindowEditor.cs
 * 
 * @author abaojin
 */
namespace GameEditor
{
    public class ExportWindowEditor : EditorWindow
    {
        private bool isProcessing = false;

        private static readonly BuildTarget[] BUILD_TARGETS = 
        {
            BuildTarget.Android,
            BuildTarget.StandaloneWindows,
            BuildTarget.StandaloneWindows64,
            BuildTarget.iOS
        };

        private bool isShowBuildSetting = true;
        private bool isShowBuildBundle = true;
        private bool isShowBuildPacket = true;

        private bool isUpdateBundleName = true;
        private bool isUpdateBundle = true;
        private bool isUseCreateTime = true;

        private string appVersion;

        [MenuItem(MenuConfig.TOOLS_INTERFACE_PROJECTBUILD, false, 0)]
        private static void ShowWindow()
        {
            ExportWindowEditor window = GetWindow<ExportWindowEditor>("项目构建工具");
            window.minSize = new Vector2(960, 640);
        }

        private void OnGUI()
        {
            EditorGUI.BeginDisabledGroup(isProcessing);
            GUILayout.BeginVertical();

            GUILayout.Label("项目构建管理工具", EditorStyles.largeLabel);

            isShowBuildSetting = EditorGUILayout.Foldout(isShowBuildSetting, "构建设置");
            if (isShowBuildSetting) {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                if (GUILayout.Button("清空本地缓存", GUILayout.Height(30))) {
                    BeginProcess();
                    EditorApplication.delayCall = () => {
                        Caching.CleanCache();
                        EndProcess();
                    };
                }

                if (GUILayout.Button("清空用户设置", GUILayout.Height(30))) {
                    BeginProcess();
                    EditorApplication.delayCall = () => {
                        PlayerPrefs.DeleteAll();
                        EndProcess();
                    };
                }

                if (GUILayout.Button("清空持久化数据", GUILayout.Height(30))) {
                    BeginProcess();
                    EditorApplication.delayCall = () => {
                        EditorUtils.ClearPersistentData();
                        EndProcess();
                    };
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical();
                if (GUILayout.Button("更新BundleName", GUILayout.Height(30))) {
                    BeginProcess();
                    EditorApplication.delayCall = () => {
                        BundleCommand.UpdateAllBundleName(true);
                        EndProcess();
                    };
                }

                if (GUILayout.Button("清空BundleName", GUILayout.Height(30))) {
                    BeginProcess();
                    EditorApplication.delayCall = () => {
                        BundleCommand.ClearAllBundleName();
                        EndProcess();
                    };
                }

                if (GUILayout.Button("授权Meta文件", GUILayout.Height(30))) {
                    BeginProcess();
                    EditorApplication.delayCall = () => {
                        MetaUtils.UpdateMetaLicense();
                        EndProcess();
                    };
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical();
                if (GUILayout.Button("清空全部资源包", GUILayout.Height(30))) {
                    BeginProcess();
                    EditorApplication.delayCall = () => {
                        BundleCommand.ClearAllBuild();
                        EndProcess();
                    };
                }

                if (GUILayout.Button("清空所有运行包", GUILayout.Height(30))) {
                    BeginProcess();
                    EditorApplication.delayCall = () => {
                        BuildCommand.ClearAllBuild();
                        EndProcess();
                    };
                }

                if (GUILayout.Button("预留功能", GUILayout.Height(30))) {
                    // TODO
                }
                GUILayout.EndVertical();

                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                GUILayout.BeginVertical("box");
                isUpdateBundleName = EditorGUILayout.Toggle("是否更新Bundle名字", isUpdateBundleName);
                isUpdateBundle = EditorGUILayout.Toggle("是否重新生成资源包", isUpdateBundle);
                isUseCreateTime = EditorGUILayout.Toggle("程序包名是否带时间戳", isUseCreateTime);
                GUILayout.EndVertical();

                GUILayout.Space(10);

                GUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("版本号格式为：1.1.1");
                appVersion = EditorGUILayout.TextField("请输入版本号:", appVersion);
                if (GUILayout.Button("更新版本号", GUILayout.Height(30))) {
                    if (!string.IsNullOrEmpty(appVersion)) {
                        BeginProcess();
                        EditorApplication.delayCall = () => {
                            VersionCommand.UpdateVersion();
                            EndProcess();
                        };
                    }
                }
                GUILayout.EndVertical();
            }

            GUILayout.Space(10);

            isShowBuildBundle = EditorGUILayout.Foldout(isShowBuildBundle, "构建资源");
            if (isShowBuildBundle) {
                foreach (BuildTarget buildTarget in BUILD_TARGETS) {
                    if (GUILayout.Button(buildTarget.ToString(), GUILayout.Height(30))) {
                        BeginProcess();
                        EditorApplication.delayCall = () => {
                            if (isUpdateBundleName) {
                                BundleCommand.UpdateAllBundleName(true);
                            }
                            if (isUpdateBundle) {
                                BundleCommand.BuildAssetBundle(buildTarget, PathConfig.RES_OUTPUT);
                            }
                            EndProcess();
                        };
                    }
                }
            }

            GUILayout.Space(10);

            isShowBuildPacket = EditorGUILayout.Foldout(isShowBuildPacket, "构建程序");
            if (isShowBuildPacket) {
                foreach (BuildTarget buildTarget in BUILD_TARGETS) {
                    if (GUILayout.Button(buildTarget.ToString(), GUILayout.Height(30))) {
                        BeginProcess();
                        EditorApplication.delayCall = () => {
                            if (isUpdateBundleName) {
                                BundleCommand.UpdateAllBundleName(true);
                            }
                            if (isUpdateBundle) {
                                BundleCommand.BuildAssetBundleAndCopy(buildTarget, PathConfig.RES_OUTPUT);
                            }
                            BuildCommand.BuildApplication(buildTarget, PathConfig.APP_OUTPUT);
                            EndProcess();
                        };
                    }
                }
            }

            GUILayout.EndVertical();
            EditorGUI.EndDisabledGroup();
        }

        private void BeginProcess()
        {
            isProcessing = true;
            ShowNotification(new GUIContent("正在处理，请稍候..."));
        }

        private void EndProcess()
        {
            isProcessing = false;
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }
    }
}
