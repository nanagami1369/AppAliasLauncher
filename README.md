# App Alias Launcher (アプリ実行エイリアス ランチャー)

## 概要

プロセスを直接起動するタイプのアプリにおいて、アプリ実行エイリアスの解析に失敗し、アプリが起動しない事があります。

本プログラムはそのような際に間に入りアプリの起動を助ける薄いラッパーです。

```
AppAliasLauncher.exe <エイリアス名.exe> [引数...]
```

## 仕組み

このexeがエイリアスの解析→プロセスの起動までを代行する事で実現します。

```csharp
// UseShellExecute = true によりOSが再解析ポイントを解釈するため、
// コンソールウィンドウは表示されない
Process.Start(new ProcessStartInfo
{
    FileName = aliasPath, // <エイリアス名.exe>
    Arguments = arguments, // [引数...]
    UseShellExecute = true, // ← これを使う事でエイリアスを解析できる
});
```

## Q. cmd.exeで同じことはできるのでは？

```
cmd /c <エイリアス名.exe> [引数...]
```

A. 黒い画面が一瞬出るのは美しくない
