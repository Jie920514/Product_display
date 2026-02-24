#商品展示系統

##使用框架及技術:
*後端: C# / ASP.NET Core MVC / .NET 8
*資料庫: SQL Server / Entity Framework Core (ORM)
*前端: Razor Pages / Bootstrap 5 / JavaScript (Fetch API) / Bootstrap Icons
*狀態管理: Session-based 登入驗證

##核心功能:
1.商品展示與搜尋
自定義 Notion 風格 卡片排版。
實作關鍵字搜尋功能，動態過濾商品清單。
具備圖片失效處理機制 (onerror fallback)。

2. 非同步收藏系統 (Ajax)
即時互動: 採用 Fetch API 呼叫後端接口，達成不跳轉網頁（No-refresh）即可切換收藏狀態。
智能判斷: 首頁載入時自動比對資料庫，動態渲染愛心狀態（實心/空心）。
UX 動畫: 在「我的最愛」頁面移除商品時，加入 CSS 漸隱與縮放動畫。

3. 資料庫關聯設計
使用 LINQ Join 語法優化查詢，單次請求即可取得使用者收藏之完整商品資訊。
實作多對多關係的簡易版中間表 (UserFavorites)。

