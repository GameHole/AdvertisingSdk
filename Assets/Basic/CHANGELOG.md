# 更新日志
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.0.1] - 2021-12-14

### 修复自动重载Bug

- 将RetryValue内对IReloader.onReloaded回调的注册移动至构造函数

###添加自动重载控制

- 添加自动重载控制接口IRetryCtrl,自定义加载时机。

###添加Banner设置

- 添加banner设置接口，Gravity定义。

### 添加更新日志.

- 

## [1.0.0] - 2021-12-09

### 首次发布到npm服务器.

- 包含激励视频广告接口
- 包含插屏视频广告接口
- 包含插屏图片广告接口
- 包含开屏广告接口
- 包含广告自动重载接口与实现
