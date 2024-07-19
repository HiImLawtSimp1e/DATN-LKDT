# Website linh kiện điện tử

Đồ án tốt nghiệp nhóm SD-18: Website linh kiện điện tử

Bạn cần cài đặt .NET 6.0 SDK runtime & Node.js 18.0 [*tại đây*](https://nodejs.org/en/download/package-manager) 

## 🚀 Quick start

1.  **Step 1.**
    Clone project bằng [*git bash*](https://git-scm.com/downloads)
    ```sh
    git clone https://github.com/tonnbph26190/DATN_LKDT.git
    ```  
1.  **Step 2.**
    ```sh
    cd ./DATN_LKDT
    ```
    * Thay đổi connection string tại `shop.Infrastructure/appsettings.json` & `shop.BackendApi/appsettings.json`
    ```sh
     "ConnectionStrings": {
    "DefaultConnection": "server=localhost\\sqlexpress;database=shop_db;trusted_connection=true"
    },
    ```
    * Add migration in `shop.Infrastructure`
    ```
    add-migration InitialDb
    ```
    * Seeding data to database
    ```
    update-database
    ```
    * Run hosting backend with IIS(port 5000)
    ```
    http://localhost:5000
    ```
1.  **Step 3.**
     ```sh
    cd ./DATN_FE
    ```
    * Cài node_module packages cho frontend app:
     ```sh
    npm i
    ```
    * Chạy  frontend(port 3000):
    ```
    npm run dev
    ```
    * Shop page
    ```
    http://localhost:3000
    ```
    * Admin dashboard page
    ```
    http://localhost:3000/dashboard
    ```
    * Biến môi trường đã được config sẵn tại `.env.local`:

    ```
    NEXT_PUBLIC_TINYMCE_API_KEY=<Your TinyMCE Key>
    NEXT_PUBLIC_CLOUDINARY_CLOUD_NAME="<Your Cloudinary's Cloud Name>"
    CLOUDINARY_API_KEY="<Your Cloudniary API Key>"
    CLOUDINARY_API_SECRET="<Your Cloundinary API Secret>"
    ```
