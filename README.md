## 低メモリ低処理速度Zloggerを即使用できるようにした個人的ライブラリ

### > [Zloggerリンク](https://github.com/Cysharp/ZLogger)
#### Dependent ```Zlogger, Microsoft.Extensions.Logging```

## 使用方法

```public static Jlogger Logger = new(FactoryBuilder.CreateLogger().CreateLogger<Jlogger>());```


以上のようにLoggerの新規インスタンスを作成する

```Logger.Log(["App started"], LogLevel.Information);```

そしてLogger内のLogメソッドを呼び出し引数1にstring[]型を引数2にLogLevelを設定する

logファイルはアプリケーションのlogsファイルに生成される
