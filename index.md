## Unity-framework

一个简单又强大的unity-framework

# 框架实现

这里暂且不介绍，稍后再补充，感兴趣可以先阅读源码。

## 使用方法

直接使用生成的dll，无需引用整个框架的源码

## 框架扩展

如需扩展框架，只需将项目添加至Unity项目默认生成的XXX.sln工作空间，
同时加入编译事件：

```
del "$(SolutionDir)Assets\Plugins\$(ProjectName).*"
Copy "$(ProjectDir)..\bin\$(ProjectName).*" "$(SolutionDir)Assets\Plugins\"
```

扩展开发完成后，直接编译便会自动将生成的dll导入到Unity项目Plugins文件夹下。

## 支持一下

你可以请我喝杯咖啡，我将做的更好。

|                                 PayPal                                  |                                 Wechat Pay                                  |                                   Alipay                                    |
|:-----------------------------------------------------------------------:|:---------------------------------------------------------------------------:|:---------------------------------------------------------------------------:|
| [![PayPal](https://www.paypalobjects.com/webstatic/paypalme/images/pp_logo_small.png)<br>Donate via PayPal ](https://www.paypal.me/abaojin) | ![wechat](/unity-framework/media/weixin.png) | ![alipay](/unity-framework/media/zhifubao.png) |


## License
[MIT License](https://hello-d.github.io/unity-framework/LICENSE.md)
