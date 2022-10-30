# バーチャルスティック

Unityパッケージのダウンロードはこちら: [VirtualStick.unitypackage](https://github.com/fluncle/UtilityAssets/raw/main/Assets/VirtualStick/VirtualStick.unitypackage)

# デモ動画

https://user-images.githubusercontent.com/3529208/198525225-d721e821-f3db-4ef6-9cca-ee6b9fc2a989.mp4

画面のドラッグで3Dスティック操作ができるコンポーネントです。  
主にモバイルでの利用を想定したものです。（EventSystemsで作っているので、一応PCでも使えます）

バーチャルスティックの入力を管理する`VirtualStickHandler`コンポーネントと、  
入力状態を表示するUIの`VirtualStickView`コンポーネントの２つが入っています。

どちらも独立したコンポーネントで、UIは不要だったり自前のコントローラーUIがあったりするなら`VirtualStickHandler`だけ、  
自前の入力管理クラスを使いたければ`VirtualStickView`だけ使うこともできます。

一定量スワイプしたらスティックの起点座標ごと移動して、  
大きくスワイプした後も反対方向の入力を入れやすい、最近多いタイプのバーチャルスティックです。

# 使い方

基本的な使い方は、`Demo/DemoScene`の中の`Player`クラスを見るとわかりやすいです。

## VirtualStickHandlerクラス

* OnBeginDragEvent
* OnDragEvent
* OnEndDragEvent

これら変数に操作開始〜操作終了時に実行したい処理を登録できます。  
AnimatorのSetTriggerなどの呼び出しに使えると思います。

`VirtualStickView`を連動させたい場合は、各イベントで対応したメソッドを呼び出す必要があります。  
`Demo/DemoScene`の中の`Player`クラスの`Awake`メソッドの記述を踏襲してください。

入力状態については

* Vector: 入力方向(Vector2)
* Rate: ドラッグ量の割合（0〜1）
* IsDrag: ドラッグ中か否か

を取得できます。

### ■入力量と入力範囲について

入力量は`Rate`メンバから 0〜1 で取得しますが、1 と判定するのに必要なドラッグ量は  
`_dragMaxDistance`メンバにピクセル量で指定します。  
※このピクセル量は端末の解像度ではなくCanvasの解像度に依存します

入力範囲は`VirtualStickHandler`のRectTransformの範囲です。

## VirtualStickViewクラス

* Begin
* End
* SetDragVector

が基本メソッドで、タッチ開始、タッチ終了、ドラッグ中にそれぞれ呼び出します。  
`Demo/DemoScene`の中の`Player`クラスの`Awake`メソッドの記述を参考にしてください。

大きく入力するとコントローラの表示が小さくなり、画面が見えやすくなって、  
小さい入力時は入力方向が見えやすいようにコントローラの表示が等倍になる特徴があります。

また、入力終了時はUIが`anchorPoaition= 0`の位置にスッと戻るアニメーションが入ります。  
そのため、`VirtualStickView`プレハブは何らかの親オブジェクトに入れて親オブジェクトの位置を動かすことで  
コントローラのデフォルト位置を設定してください。  
（`Demo/DemoScene`の`BasePosition`がその役目をしています）
