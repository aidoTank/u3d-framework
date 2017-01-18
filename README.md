# unity-framework

[![GitHub stars](https://img.shields.io/github/stars/hello-d/unity-framework.svg)](https://github.com/hello-d/unity-framework/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/hello-d/unity-framework.svg)](https://github.com/hello-d/unity-framework/network)
[![GitHub issues](https://img.shields.io/github/issues/hello-d/unity-framework.svg)](https://github.com/hello-d/unity-framework/issues)
[![GitHub release](https://img.shields.io/github/release/hello-d/unity-framework.svg)](https://github.com/hello-d/unity-framework/releases)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/hello-d/unity-framework/master/LICENSE)

## 介绍

一个简单的MVC-Unity大型项目开发框架。

## 框架

框架具体实现，这里暂且不讨论，稍后再补充，感兴趣可以先阅读源码。

## 使用方法

直接使用生成的dll，无需引用整个框架的源码

## 扩展方法

如需扩展框架，只需将项目添加至Unity项目默认生成的XXX.sln工作空间，
同时加入编译事件：

```
del "$(SolutionDir)Assets\Plugins\$(ProjectName).*"
Copy "$(ProjectDir)..\bin\$(ProjectName).*" "$(SolutionDir)Assets\Plugins\"
```

扩展开发完成后，直接编译便会自动将生成的dll导入到Unity项目Plugins文件夹下。

